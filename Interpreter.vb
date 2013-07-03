

Public Class Interpreter
    Enum Mode
        PlayTrack
        PlayTrackOnce
        PlaySeq
        PlayShape
    End Enum
    Enum _fType
        TFMX
        MMME
    End Enum

    Structure _VOICE_SET
        Dim nr As Short
        Dim instr_count As Short
        Dim instr_current As Short
        Dim instr_index As Short
        Dim instr_note As Short
        Dim shape_count As Short
        Dim shape_current As Short
        Dim shape_index As Short
        Dim shape_time As Short
        Dim shape_time_init As Short
        Dim shape_amplitude As Byte
        Dim seq_shape As Byte
        Dim seq_instr As Byte
        Dim seq_note As Byte
        Dim seq_time As Short
        Dim seq_time_init As Short
        Dim seq_current As Short
        Dim seq_index As Short
        Dim track_current As Short
        Dim track_index As Short
        Dim track_count As Short
        Dim track_note As Short
        Dim track_shape As Byte
        Dim track_reduce As Short
        Dim flags As Byte
        Dim delta_f As Short
        Dim mod_f_work As Short
        Dim mod_f_max As Short
        Dim tune_noise As Byte
        Dim noise_freq As Byte
        Dim triangle_fine As Byte
        Dim triangle_restart As Boolean
        Dim tfmx_count As Short
        Dim bend_var As Integer
        Dim reduce As Short
        Dim div As Short
        Dim shape_sync As Byte
        Dim sid_mode As Byte
        Dim digi_drum As Short
    End Structure

    <Serializable()> Structure _SND_INFO
        Dim start As Short
        Dim last As Short
        Dim speed As Short
    End Structure

    Dim track_length As Short
    Dim play_speed As Short
    Dim play_speed_count As Short

    Dim instr As instr
    Dim seq As clSequence
    Dim shape As shape
    Dim Tracks As clTracks

    Public VoiceSet(2) As _VOICE_SET
    Public trackPos As Short = 0

    Dim div() As Short = {3822, 3607, 3405, 3214, 3033, 2863, 2702, 2551, 2407, 2272, 2145, 2024, _
                          1911, 1803, 1702, 1607, 1516, 1431, 1351, 1275, 1203, 1136, 1072, 1012, _
                          955, 901, 851, 803, 758, 715, 675, 637, 601, 568, 536, 506, _
                          477, 450, 425, 401, 379, 357, 337, 318, 300, 284, 268, 253, _
                          238, 225, 212, 200, 189, 178, 168, 159, 150, 142, 134, 126, _
                          119, 112, 106, 100, 94, 89, 84, 79, 75, 71, 67, 63, _
                          59, 56, 53, 50, 47, 44, 42, 39, 37, 35, 33, 31, _
                          29, 28, 26, 25, 23, 22, 21, 19, 18, 17, 16, 15, _
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, _
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, _
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}


    Dim mask() As Byte = {&HFE, &HF7, &HFD, &HEF, &HFB, &HDF}

    Dim ym_enable, ym_noise_f As Byte

    Private ftype As _fType
    Public PlayMode As Mode

    Sub init(ByVal f As String, ByRef i As Instr, ByRef se As clSequence, ByRef sh As Shape, ByRef t As clTracks)

        Select Case f
            Case "TFMX"
                ftype = _fType.TFMX
            Case "MMME"
                ftype = _fType.MMME
        End Select
        instr = i
        seq = se
        shape = sh
        Tracks = t
    End Sub

    Sub tick(ByRef ym As WaveGen._YM)
        Dim i As Short
        Dim ym_div As Short
        Dim amplitude As Byte
        Dim b As Byte

        '        ym.eshape = &HFF


        For i = 0 To 2
            VoiceSet(i).triangle_fine = &HFF
            VoiceSet(i).triangle_restart = False

            work_voice(VoiceSet(i), ym_div, amplitude)
            ym.chn(i).coars = ym_div >> 8
            ym.chn(i).fine = ym_div And &HFF
            ym.chn(i).volume = amplitude

            If VoiceSet(i).triangle_fine <> &HFF Then
                ym.efine = VoiceSet(i).triangle_fine
            End If

            If VoiceSet(i).triangle_restart Then
                ym.ecoars = 0
                ym.eshape = 10      'Triangle
                ym.shape.counter = 0    'Restart
            End If

            '---SID on Channel A

            If i = 0 Then
                b = VoiceSet(0).sid_mode
                Select Case b
                    Case 0
                        ym.sidvol = &HFF                'SID OFF
                    Case 4
                        ym.sidvol = amplitude
                    Case 1, 2
                        ym_div = VoiceSet(b - 1).div
                        If ym_div <= 16 Then
                            ym.sidvol = &HFF                'SID OFF
                        Else
                            ym.sidfreq = ym_div
                        End If
                End Select
            End If

            '--- Digi-Drum on Channel C
            If i = 2 Then
                If VoiceSet(i).digi_drum >= 0 Then
                    ym.digidrum = VoiceSet(i).digi_drum
                    VoiceSet(i).digi_drum = -1
                    ym.digipos = 0
                End If
            End If
        Next
        ym.enable = ym_enable
        ym.noisep = ym_noise_f

        If PlayMode <> Mode.PlayShape Then
            play_speed_count -= 1
            If play_speed_count = 0 Then
                play_speed_count = play_speed
                For i = 0 To 2
                    sequencer(VoiceSet(i))
                Next
            End If
        End If

    End Sub


    Sub work_voice(ByRef vs As _VOICE_SET, ByRef ym_div As Short, ByRef amplitude As Byte)
        Dim b, b2 As Byte
        Dim note As Short
        Dim bender As Integer
        Dim f_max, f_work As Short
        Dim flag As Byte
        Dim i As Short
        Dim l1, l2 As Integer

icount: If vs.instr_count > 0 Then
            vs.instr_count -= 1
        Else

next_i:     i = vs.instr_index
            b = instr.instr(vs.instr_current)(i)
            i += 1
            If b < &HE0 Then
                vs.instr_note = b
                vs.instr_index += 1
            Else
                Select Case b
                    Case &HE0   'absolute JMP
                        vs.instr_index = instr.instr(vs.instr_current)(i)
                        GoTo next_i

                    Case &HE1   ' End
                        vs.instr_note = instr.instr(vs.instr_current)(i - 2) 'stay on last note

                    Case &HE2 ' reset shape index
                        vs.shape_index = 0
                        vs.shape_time = 1
                        vs.instr_index += 1
                        GoTo next_i

                    Case &HE3 'parameters for f modulation
                        vs.delta_f = instr.instr(vs.instr_current)(i)
                        i += 1
                        vs.mod_f_max = instr.instr(vs.instr_current)(i)
                        i += 1
                        vs.instr_index += 3
                        GoTo next_i
                    Case &HE4   'noise & tune on
                        vs.tune_noise = 0
                        vs.noise_freq = instr.instr(vs.instr_current)(i)
                        i = i + 1
                        vs.instr_index += 2
                        GoTo next_i
                    Case &HE5   'noise on, tune off
                        vs.tune_noise = 1
                        vs.instr_index += 1
                        GoTo next_i
                    Case &HE6   'noise off, tune on
                        vs.tune_noise = 2
                        vs.instr_index += 1
                        GoTo next_i
                    Case &HE7   'select instr group
                        vs.instr_index += 2
                        ' MsgBox("InstrGroup")
                        GoTo next_i
                    Case &HE8 'set instr counter
                        vs.instr_count = instr.instr(vs.instr_current)(i)
                        vs.instr_index += 2
                        GoTo icount
                    Case &HE9   'triangle evelope
                        vs.triangle_fine = instr.instr(vs.instr_current)(i)
                        vs.triangle_restart = True
                        vs.instr_index += 2
                        GoTo next_i


                    Case &HEA   '???
                        vs.seq_shape = &H20
                        vs.seq_instr = instr.instr(vs.instr_current)(i)
                        vs.instr_index += 2
                        Debug.WriteLine("Drum: " & CStr(vs.seq_instr))
                        GoTo next_i

                    Case &HEB       ' shape vibrato
                        vs.shape_sync = instr.instr(vs.instr_current)(i)
                        i = i + 1
                        vs.instr_index += 2
                        GoTo next_i

                    Case &HEC       ' digi drum
                        vs.digi_drum = instr.instr(vs.instr_current)(i)
                        vs.tune_noise = 3   'noise off, tune off
                        vs.instr_index += 4 '4
                        Debug.WriteLine("Drum:2 " & CStr(vs.digi_drum))
                        GoTo next_i

                    Case &HEE       ' sid mode
                        vs.sid_mode = instr.instr(vs.instr_current)(i)
                        i = i + 1
                        vs.instr_index += 2
                        GoTo next_i
                End Select
            End If
        End If

        ' --- Shape handling ---

scount: If vs.shape_count > 0 Then
            vs.shape_count -= 1
        Else
            vs.shape_time -= 1
            If vs.shape_time = 0 Then
                vs.shape_time = vs.shape_time_init
next_s:         i = vs.shape_index
                b = shape.shape_set(vs.shape_current).data(i)
                i += 1
                If b < &HE0 Then
                    vs.shape_amplitude = b
                    vs.shape_index += 1
                Else
                    Select Case b
                        Case &HE0   'absolute JMP
                            i = shape.shape_set(vs.shape_current).data(i)    'dest
                            vs.shape_index = i
                            GoTo next_s
                        Case &HE1   'end
                            vs.shape_amplitude = shape.shape_set(vs.shape_current).data(i - 2) ' stay on last amplitude
                        Case &HE2 To &HE7   'not supported
                            Exit Sub
                        Case &HE8   ' set shape_counter
                            vs.shape_count = shape.shape_set(vs.shape_current).data(i)
                            vs.shape_index += 2
                            GoTo scount
                    End Select
                End If
            End If
        End If

        '--- divider for ym freq ---

        note = vs.instr_note
        If (note And &H80) = 0 Then
            note += vs.seq_note + vs.track_note
        End If
        note = note And &H7F
        ym_div = div(note)

        '--- Tune/Noise on/off ---

        Select Case vs.tune_noise
            Case 0  'tune & noise on
                ym_enable = ym_enable And mask(2 * vs.nr)   ' tune on
                ym_enable = ym_enable And mask(2 * vs.nr + 1) ' noise on
            Case 1 'tune off, noise on
                ym_enable = ym_enable Or Not mask(2 * vs.nr)   ' tune off
                ym_enable = ym_enable And mask(2 * vs.nr + 1) ' noise on
                vs.noise_freq = vs.seq_note
                note = vs.instr_note
                If note And &H80 Then
                    note = note Or &HFF00
                End If
                If note < 0 Then
                    vs.noise_freq = note And &H7F
                Else
                    vs.noise_freq += note
                End If
            Case 2 ' tune on, noise off
                ym_enable = ym_enable And mask(2 * vs.nr)   ' tune on
                ym_enable = ym_enable Or Not mask(2 * vs.nr + 1) ' noise off
        End Select

        If vs.noise_freq <> 0 Then
            ym_noise_f = (Not vs.noise_freq) And &H1F
        End If

        '--- TFMX ---

        If ftype = _fType.TFMX Then
            If vs.tfmx_count = 0 Then
                flag = vs.flags

                f_max = vs.mod_f_max

                'Wertebereich mod_f_max: 0...127   entspricht 0...254 in 2er Schritten
                '                        128...255 entspricht 0... 127

                If (f_max And &H80) = &H80 Then
                    f_max = f_max And &H7F
                Else
                    f_max *= 2
                End If

                f_work = vs.mod_f_work
                If ((flag And &H80) = &H0) Or ((flag And &H81) = &H80) Then
                    If (flag And &H20) = 0 Then
                        'b5=0, subtract
                        f_work -= vs.delta_f
                        If f_work < 0 Then
                            f_work = 0
                            flag = flag Or &H20 'b5=1 -> add
                        End If
                    Else
                        'b5=1, add
                        f_work += vs.delta_f
                        If f_work > f_max Then
                            f_work = f_max
                            flag = flag And Not &H20 'b5=0 -> sub
                        End If
                    End If
                    vs.mod_f_work = f_work
                End If

                f_work -= f_max \ 2

                Do While note < 4 * 12
                    f_work += f_work
                    note += 12
                Loop
                ym_div += f_work
                vs.flags = flag Xor 1
            Else
                vs.tfmx_count -= 1
            End If

            ' freq-bender

            If (vs.seq_shape And &H20) = &H20 Then
                bender = vs.seq_instr
                bender *= 4096
                If (bender And &H80000) > 0 Then
                    bender = bender Or &HFFF00000
                End If
                bender += vs.bend_var
                vs.bend_var = bender
                ym_div -= bender \ 65536
            End If

            '--- amplitude ---

            i = vs.shape_amplitude
            i -= vs.track_reduce
            i -= vs.reduce
            If i < 0 Then
                i = 0
            End If
            amplitude = i


        Else
            '--- MMME ---------------------------------------------------------

            If vs.tfmx_count = 0 Then
                flag = vs.flags
                f_max = vs.mod_f_max


                f_work = vs.mod_f_work
                If (flag And &H20) = &H0 Then   'addieren
                    f_work += vs.delta_f
                    If f_work > 2 * f_max Then
                        f_work = 2 * f_max
                        flag = flag Or &H20     'auf Sub umschalten
                    End If
                Else                            'subtrahieren
                    f_work -= vs.delta_f
                    If f_work < 0 Then
                        f_work = 0
                        flag = flag And &HDF    'auf Add umschalten
                    End If
                End If
                vs.mod_f_work = f_work
                vs.flags = flag

                l1 = f_work - f_max             ' convert Short to Integer
                l1 = (l1 * ym_div) >> 10
                ym_div += l1
            Else
                vs.tfmx_count -= 1
            End If


            '--- Bender
            If (vs.seq_shape And &H20) = &H20 Then
                b = vs.seq_instr           ' convert unsigned Byte to Integer
                l2 = b
                If (b And &H80) > 0 Then
                    l2 -= 256
                End If
                vs.bend_var += l2
                l1 = ym_div
                ym_div = l1 - ((l1 * vs.bend_var) >> 10)
            End If

            '--- amplitude ---

            i = vs.shape_amplitude
            i -= vs.track_reduce
            i -= vs.reduce
            If i < 0 Then
                i = 0
            End If
            amplitude = i

            '--- shapeform sync

            b = vs.shape_sync
            If b <> 0 Then
                If (b And &H80) > 0 Then
                    vs.triangle_restart = True
                End If

                If (b And &H2) > 0 Then
                    ym_div = 0                  ' Ton aus
                End If

                If (b And &H8) > 0 Then
                    vs.triangle_fine = ym_div \ 8
                Else
                    vs.triangle_fine = ym_div \ 16
                End If

            End If
        End If
        vs.div = ym_div
    End Sub

    ' sequencer for uncompressed data

    Sub sequencer(ByRef vs As _VOICE_SET)
        Dim j As Short
        Dim b As Byte
        Dim se As ym.clSequence._SEQENTRY


        Do
            se = seq.seqs(vs.seq_current).seq(vs.seq_index)
            vs.seq_index += 1

            Select Case se.note
                Case -1             ' nix tun
                    Exit Sub
                Case &HFF           ' end of sequence
                    '--- Track-Interpeter ---

                    If PlayMode = Mode.PlaySeq Then
                        vs.track_note = 0
                        vs.track_shape = 0
                        vs.seq_current = 0      'seq 0 must be always "pause"
                        vs.seq_index = 0
                        Form1.WaveGen.reset()
                        Form1.WaveGen.PlayFlag = False
                        Exit Sub
                    End If

                    If vs.nr = 0 Then
                        vs.track_count -= 1
                        If vs.track_count < 0 Then
                            If PlayMode = Mode.PlayTrackOnce Then
                                vs.track_note = 0
                                vs.track_shape = 0
                                vs.seq_current = 0      'seq 0 must be always "pause"
                                vs.seq_index = 0
                                Form1.WaveGen.reset()
                                Form1.WaveGen.PlayFlag = False
                            End If
                            vs.track_count = track_length           'play it again
                            VoiceSet(0).track_index = 0
                            VoiceSet(1).track_index = 0
                            VoiceSet(2).track_index = 0
                        End If
                    End If
                    j = vs.track_current + vs.track_index
                    trackPos = j
                    vs.track_note = Tracks.GetNote(vs.nr, j)
                    vs.track_shape = Tracks.GetInstr(vs.nr, j)
                    b = Tracks.GetCmd(vs.nr, j)
                    If (b And &HF0) = &HF0 Then
                        vs.track_reduce = b And &HF
                    ElseIf (b And &HE0) = &HE0 Then
                        play_speed = b And &HF
                    End If
                    vs.seq_current = Tracks.GetSeq(vs.nr, j)
                    vs.seq_index = 0
                    vs.track_index += 1

                    '--- End Track-Interpeter ---

                Case Else
                    vs.seq_note = se.note                         ' Tonhöhe
                    b = se.shape
                    vs.seq_shape = b                        ' hkurve
                    If (b And &HE0) > 0 Then
                        vs.seq_instr = se.xtra   'Instrument
                    End If
                    vs.bend_var = 0

                    If (vs.seq_note And &H80) = 0 Then                  'Tonhöhe positiv
                        b = (vs.seq_shape And &H1F) + vs.track_shape
                        vs.shape_time = shape.shape_set(b).para1
                        vs.shape_time_init = shape.shape_set(b).para1
                        vs.delta_f = shape.shape_set(b).para3

                        vs.flags = &H40
                        vs.mod_f_work = shape.shape_set(b).para4
                        vs.mod_f_max = shape.shape_set(b).para4
                        vs.tfmx_count = shape.shape_set(b).para5
                        vs.shape_current = b
                        vs.shape_index = 0

                        If (vs.seq_shape And &H40) > 0 Then
                            b = vs.seq_instr
                        Else
                            b = shape.shape_set(b).para2
                        End If
                        vs.instr_current = b
                        vs.instr_count = 0
                        vs.instr_index = 0
                        vs.shape_count = 0
                    End If
                    Exit Sub
            End Select
        Loop
    End Sub


    Sub init_voices(ByRef snd_info As _SND_INFO, ByVal pm As Mode)
        init_voices(snd_info, pm, Nothing)
    End Sub

    Sub init_voices(ByRef snd_info As _SND_INFO, ByVal pm As Mode, ByVal seq() As Short)
        Dim i, j As Short
        Dim b As Byte

        PlayMode = pm

        track_length = snd_info.last - snd_info.start
        play_speed = snd_info.speed

        For i = 0 To 2
            VoiceSet(i).instr_current = instr.dummy                 ' Instrument 64
            VoiceSet(i).instr_index = 0
            VoiceSet(i).shape_current = shape.dummy
            VoiceSet(i).shape_index = 0
            VoiceSet(i).seq_note = 0
            VoiceSet(i).seq_shape = 0
            VoiceSet(i).instr_note = 0
            VoiceSet(i).shape_amplitude = 0
            VoiceSet(i).shape_time_init = 1
            VoiceSet(i).shape_time = 1
            VoiceSet(i).shape_count = 0
            VoiceSet(i).instr_count = 0
            VoiceSet(i).delta_f = 0
            VoiceSet(i).mod_f_max = 0
            VoiceSet(i).mod_f_work = 0
            VoiceSet(i).tfmx_count = 0
            VoiceSet(i).seq_instr = 0
            VoiceSet(i).noise_freq = 0
            VoiceSet(i).nr = i

            VoiceSet(i).track_current = snd_info.start
            VoiceSet(i).track_index = 1
            VoiceSet(i).track_count = track_length

            VoiceSet(i).reduce = 0
            VoiceSet(i).track_reduce = 0

            VoiceSet(i).seq_index = 0
            VoiceSet(i).seq_time_init = 0
            VoiceSet(i).seq_time = 0

            VoiceSet(i).shape_sync = 0
            VoiceSet(i).sid_mode = 0

            VoiceSet(i).digi_drum = -1

            Select Case pm
                Case Mode.PlayTrack, Mode.PlayTrackOnce
                    j = VoiceSet(i).track_current
                    trackPos = j
                    VoiceSet(i).seq_current = Tracks.GetSeq(i, j)
                    VoiceSet(i).track_note = Tracks.GetNote(i, j)
                    VoiceSet(i).track_shape = Tracks.GetInstr(i, j)
                    b = Tracks.GetCmd(i, j)
                    If (b And &HF0) = &HF0 Then
                        VoiceSet(i).track_reduce = b And &HF
                    End If
                Case Mode.PlaySeq
                    VoiceSet(i).seq_current = seq(i)
                    VoiceSet(i).track_note = 0
                    VoiceSet(i).track_shape = 0
            End Select

            VoiceSet(i).bend_var = 0
        Next
        play_speed_count = 1
        ym_enable = &HFF

    End Sub

End Class
