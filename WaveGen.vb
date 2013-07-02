Imports ym.Form1
Imports System.IO
Imports System.Threading
Imports Microsoft.DirectX.DirectSound

Public Class WaveGen

    Structure _VOICE
        Dim counter As Integer
        Dim incr As Integer
    End Structure

    Structure _SHAPE
        Dim counter As Integer
        Dim incr As Integer
    End Structure

    Structure _NOISE
        Dim counter As Integer
        Dim incr As Integer
        Dim gen As Integer
        Dim state As Integer
    End Structure

    Structure _CHN
        Dim fine As Byte
        Dim coars As Byte
        Dim volume As Byte
    End Structure

    Structure _YM
        Dim chn() As _CHN
        Dim shape As _SHAPE
        Dim noise As _NOISE
        Dim noisep As Byte
        Dim enable As Byte
        Dim efine As Byte
        Dim ecoars As Byte
        Dim eshape As Byte
        Dim voice() As _VOICE
        Dim sidvoice As _VOICE
        Dim sidvol As Byte
        Dim sidfreq As Short
        Dim digidrum As Short
        Dim digipos As Integer
    End Structure

#Region "YMEnvForms"

    Const L0 As Short = 0
    Const L1 As Short = 32
    Const L2 As Short = 45
    Const L3 As Short = 64
    Const L4 As Short = 91
    Const L5 As Short = 128
    Const L6 As Short = 181
    Const L7 As Short = 256
    Const L8 As Short = 362
    Const L9 As Short = 512
    Const L10 As Short = 724
    Const L11 As Short = 1024
    Const L12 As Short = 1448
    Const L13 As Short = 2048
    Const L14 As Short = 2896
    Const L15 As Short = 4096

    Dim YMEnvForms(,) As Short = { _
    {L15, L14, L13, L12, L11, L10, L9, L8, L7, L6, L5, L4, L3, L2, L1, L0, _
      L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0}, _
    {L15, L14, L13, L12, L11, L10, L9, L8, L7, L6, L5, L4, L3, L2, L1, L0, _
      L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0}, _
    {L15, L14, L13, L12, L11, L10, L9, L8, L7, L6, L5, L4, L3, L2, L1, L0, _
     L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0}, _
    {L15, L14, L13, L12, L11, L10, L9, L8, L7, L6, L5, L4, L3, L2, L1, L0, _
     L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0}, _
    {L0, L1, L2, L3, L4, L5, L6, L7, L8, L9, L10, L11, L12, L13, L14, L15, _
     L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0}, _
    {L0, L1, L2, L3, L4, L5, L6, L7, L8, L9, L10, L11, L12, L13, L14, L15, _
     L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0}, _
    {L0, L1, L2, L3, L4, L5, L6, L7, L8, L9, L10, L11, L12, L13, L14, L15, _
     L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0}, _
    {L0, L1, L2, L3, L4, L5, L6, L7, L8, L9, L10, L11, L12, L13, L14, L15, _
     L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0}, _
    {L15, L14, L13, L12, L11, L10, L9, L8, L7, L6, L5, L4, L3, L2, L1, L0, _
    L15, L14, L13, L12, L11, L10, L9, L8, L7, L6, L5, L4, L3, L2, L1, L0}, _
    {L15, L14, L13, L12, L11, L10, L9, L8, L7, L6, L5, L4, L3, L2, L1, L0, _
     L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0}, _
    {L15, L14, L13, L12, L11, L10, L9, L8, L7, L6, L5, L4, L3, L2, L1, L0, _
     L0, L1, L2, L3, L4, L5, L6, L7, L8, L9, L10, L11, L12, L13, L14, L15}, _
    {L15, L14, L13, L12, L11, L10, L9, L8, L7, L6, L5, L4, L3, L2, L1, L0, _
    L15, L15, L15, L15, L15, L15, L15, L15, L15, L15, L15, L15, L15, L15, L15, L15}, _
    {L0, L1, L2, L3, L4, L5, L6, L7, L8, L9, L10, L11, L12, L13, L14, L15, _
     L0, L1, L2, L3, L4, L5, L6, L7, L8, L9, L10, L11, L12, L13, L14, L15}, _
    {L0, L1, L2, L3, L4, L5, L6, L7, L8, L9, L10, L11, L12, L13, L14, L15, _
    L15, L15, L15, L15, L15, L15, L15, L15, L15, L15, L15, L15, L15, L15, L15, L15}, _
    {L0, L1, L2, L3, L4, L5, L6, L7, L8, L9, L10, L11, L12, L13, L14, L15, _
    L15, L14, L13, L12, L11, L10, L9, L8, L7, L6, L5, L4, L3, L2, L1, L0}, _
    {L0, L1, L2, L3, L4, L5, L6, L7, L8, L9, L10, L11, L12, L13, L14, L15, _
     L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0, L0} _
}
#End Region

    Dim RawYM As YMImport
    Dim interpreter As Interpreter

    Private MyDevice As Device = Nothing
    Private bd As BufferDescription
    Private wf As WaveFormat

    Dim PrimBuf As Buffer
    Dim SecBuf As SecondaryBuffer
    Dim oThread As Thread
    Dim ThreadFlag As Boolean

    Dim wave(8819) As Short
    Dim Count44100 As Integer
    Dim frame As Integer

    Dim ym As _YM

    Dim samples As Sample
    Const digi_incr As Integer = 9317 '(6269.3877 / 44100.0) * 65536


    Sub New()
        ReDim ym.chn(2)
        ReDim ym.voice(2)
    End Sub

    Dim mode As Byte
    Public PlayFlag As Boolean = False
    Private fs As FileStream
    Private br As BinaryWriter



    Sub reset()
        Dim i As Short

        For i = 0 To 2
            ym.voice(i).counter = 0
            ym.voice(i).incr = 0
            ym.chn(i).coars = 0
            ym.chn(i).fine = 0
            ym.chn(i).volume = 0
        Next
        ym.sidvoice.counter = 0
        ym.sidvoice.incr = 0

        ym.shape.counter = 0
        ym.shape.incr = 0

        ym.noise.counter = 0
        ym.noise.incr = 0
        ym.noise.gen = 1

        ym.ecoars = 0
        ym.efine = 0
        ym.noisep = 0
        ym.enable = &H3F

        ym.sidvol = &HFF

        ym.digidrum = -1
    End Sub

    Sub init(ByVal ctrl As Control, ByVal ymtest As YMImport, ByRef interp As interpreter, ByVal m As Byte, ByRef smp As Sample)

        samples = smp

        Dim i As Short
        Dim dc As New DevicesCollection

        RawYM = ymtest
        interpreter = interp
        mode = m

        Count44100 = 0
        frame = 0

        For i = 0 To dc.Count - 1
             Console.WriteLine("{0}: " & " " & dc.Item(i).Description, i)
        Next

        MyDevice = New Device

        MyDevice.SetCooperativeLevel(ctrl, CooperativeLevel.Priority)

        wf = New WaveFormat
        wf.BitsPerSample = 16
        wf.Channels = 1
        wf.SamplesPerSecond = 44100
        wf.FormatTag = WaveFormatTag.Pcm
        wf.BlockAlign = wf.BitsPerSample / 8 * wf.Channels
        wf.AverageBytesPerSecond = wf.SamplesPerSecond * wf.BlockAlign


        bd = New BufferDescription
        bd.BufferBytes = 0
        bd.StickyFocus = True
        bd.PrimaryBuffer = True
        bd.Format = wf
        PrimBuf = New Buffer(bd, MyDevice)
        PrimBuf.Format = wf                         'set Format primary buffer

        bd.CanGetCurrentPosition = True
        bd.BufferBytes = 882 * 2 * 10
        bd.PrimaryBuffer = False
        SecBuf = New SecondaryBuffer(bd, MyDevice)

        For i = 0 To 8819
            '            wave(i) = Math.Sin(i * 2 * Math.PI / 882) * 16000
            wave(i) = 0
        Next


        Dim otter As New ThreadStart(AddressOf tt)
        ' Create a Thread object. 
        oThread = New Thread(otter)
        oThread.Name = "SoundThread"
        oThread.Priority = ThreadPriority.AboveNormal
        ' Starting the thread invokes the ThreadStart delegate.
        ThreadFlag = True
        oThread.Start()

        '        buf.SetCurrentPosition(8820)
        '       buf.Write(0, wave, LockFlag.FromWriteCursor)

        reset()

        '        ym.voice(0).incr = 380436
        '        ym.voice(1).incr = 300000

        ym.chn(0).volume = 0
        ym.chn(1).volume = 0
        ym.chn(2).volume = 0

        ym.enable = &HFF

        '        ym.voice(1).incr = 1001000
        ym.eshape = 0                      '14
        ym.shape.incr = 380436 * 1

        ym.noise.incr = 1000000

        SecBuf.Play(0, BufferPlayFlags.Looping)

        '        MsgBox("Hehe")
        '        close()
    End Sub

    Sub close()
        SecBuf.Stop()
        ThreadFlag = False
        Do While oThread.ThreadState <> ThreadState.Stopped
        Loop

    End Sub

    Private Sub tt()
        Dim PlayPos, WritePos, MyWritePos As Integer
        Dim LastPlayPos As Integer = 0
        Dim CurLen, CurLen2 As Integer

        Dim v As Integer = 0
        Dim vtmp As Integer
        Dim i, j As Integer
        Dim sample, sample2 As Single
        Dim dd() As Short
        Dim l As Integer
        Dim shape As Single
        Dim vol As Single
        Dim noise As Single
        Dim c As Single
        Dim incr As Integer
        Dim sp As Integer
        Dim m1 As Byte
        Static ticker As Integer = 0

        Dim ym_set(15) As Byte

        '        Console.WriteLine("tt-test")

        SecBuf.GetCurrentPosition(PlayPos, MyWritePos)
        MyWritePos += 8820

        Do While ThreadFlag
            SecBuf.GetCurrentPosition(PlayPos, WritePos)
            If LastPlayPos < PlayPos Then
                CurLen = PlayPos - LastPlayPos
            Else
                CurLen = 8820 * 2 - (LastPlayPos - PlayPos)
            End If

            'Console.WriteLine("{0} {1} {2}", LastPlayPos, PlayPos, CurLen)
            LastPlayPos = PlayPos



            CurLen2 = CurLen / 2 - 1
            ReDim wave(CurLen2)
            If PlayFlag = False Then
                wave.Clear(wave, 0, wave.GetLength(0))
            End If

            ' a simple limited bandwith square wave generator

            If PlayFlag = True Then
                For i = 0 To CurLen2

                    Count44100 += 1
                    If Count44100 = 882 Then
                        Count44100 = 0                      ' all 20 ms


                        interpreter.tick(ym)

                        ticker += 1

                        'test_ym()
#If dump Then
                        writeFile()
#End If
                        '--- set frequency

                        For j = 0 To 2
                            ym.voice(j).incr = 95108934.24 / (ym.chn(j).fine Or (ym.chn(j).coars * 256))
                        Next
                        ym.noise.incr = 95108934.24 / ym.noisep
                        ym.shape.incr = (95108934.24 / 2) / ((ym.ecoars * 256) Or ym.efine)
                        ym.sidvoice.incr = (95108934.24 * 1) / ym.sidfreq
                    End If

                    '--- Noise Generator ---

                    ym.noise.counter += ym.noise.incr
                    If ym.noise.counter And &H1000000 Then
                        ym.noise.counter = ym.noise.counter And &HFFFFFF
                        ym.noise.gen *= 2
                        If ym.noise.gen And &H80000000 Then
                            ym.noise.gen = ym.noise.gen Xor &H40001
                        End If
                    End If
                    If ym.noise.gen And 1 Then
                        noise = 1
                    Else
                        noise = -1
                    End If

                    '--- Envelope ---

                    v = ym.shape.counter
                    sp = (v \ &H1000000) And &H1F
                    shape = YMEnvForms(ym.eshape, sp)
                    If (sp = &H1F) And (Not ((ym.eshape And 9) = 8)) Then
                        ym.shape.incr = 0                       ' stop enevelope
                    End If
                    ym.shape.counter += ym.shape.incr


                    '--- SID ---

                    If ym.sidvol <> &HFF Then
                        v = ym.sidvoice.counter
                        incr = ym.sidvoice.incr
                        vtmp = v + incr
                        If v And &H1000000 Then
                            sample = 1
                        Else
                            sample = -1
                        End If

                        If (v Xor vtmp) And &H1000000 Then  ' edge changed
                            c = 1 - 2 * (vtmp And &HFFFFFF) / incr
                            sample = sample * c
                        End If
                        ym.sidvoice.counter = vtmp

                        vol = (sample + 1) / 2 * YMEnvForms(4, ym.sidvol)
                    End If

                    '--- 3 square waves ---

                    m1 = 1
                    sample2 = 0
                    For j = 0 To 2
                        '--- volume source ---
                        If (ym.chn(j).volume And &H10) < &H10 Then
                            If j > 0 Or ym.sidvol = &HFF Then
                                vol = YMEnvForms(4, ym.chn(j).volume) 'Kanal B&c kein SID
                            End If
                        Else
                            vol = shape
                        End If

                        v = ym.voice(j).counter
                        incr = ym.voice(j).incr
                        vtmp = v + incr
                        If v And &H1000000 Then
                            sample = 1
                        Else
                            sample = -1
                        End If

                        If (v Xor vtmp) And &H1000000 Then  ' edge changed
                            c = 1 - 2 * (vtmp And &HFFFFFF) / incr
                            sample = sample * c
                        End If
                        ym.voice(j).counter = vtmp

                        If (ym.enable And m1) > 0 Then
                            sample2 += vol
                        Else
                            sample2 += sample * vol
                        End If
                        If (ym.enable And (m1 * 8)) = 0 Then
                            sample2 += noise * vol
                        End If
                        m1 *= 2
                    Next j
                    wave(i) = sample2
                Next i

                If ym.digidrum >= 0 Then
                    dd = samples.samp(ym.digidrum).data
                    l = samples.samp(ym.digidrum).len

                    For i = 0 To CurLen2
                        j = ym.digipos >> 16
                        If j >= l Then
                            ym.digidrum = -1
                            Exit For
                        End If
                        wave(i) += dd(j)                    ' add digi-drum
                        ym.digipos += digi_incr
                    Next
                End If
            End If

            SecBuf.Write(MyWritePos, wave, LockFlag.EntireBuffer)

            MyWritePos += CurLen
            If MyWritePos >= 8820 * 2 Then
                MyWritePos -= 8820 * 2
            End If

            oThread.Sleep(50)

        Loop

    End Sub

    Sub test_ym()
        Static frame = 1
        Dim ymset(15) As Byte
        Dim flag As Boolean = False


        RawYM.GetYMSet(frame, ymset)
        frame += 1

        If ymset(0) <> ym.chn(0).fine Then
            Debug.Write("A fine,")
            flag = True
        End If


        If ymset(1) <> ym.chn(0).coars Then
            Debug.Write("A coars,")
            flag = True
        End If

        If ymset(2) <> ym.chn(1).fine Then
            Debug.Write("B fine,")
            flag = True
        End If

        If ymset(3) <> ym.chn(1).coars Then
            Debug.Write("B coars,")
            flag = True
        End If

        If ymset(4) <> ym.chn(2).fine Then
            Debug.Write("C fine,")
            flag = True
        End If

        If ymset(5) <> ym.chn(2).coars Then
            Debug.Write("C coars,")
            flag = True
        End If

        If ymset(8) <> ym.chn(0).volume Then
            Debug.Write("A volume,")
            flag = True
        End If

        If ymset(9) <> ym.chn(1).volume Then
            Debug.Write("B volume,")
            flag = True

        End If
        If ymset(10) <> ym.chn(2).volume Then
            Debug.Write("c volume,")
            flag = True
        End If

        If flag Then
            Debug.WriteLine("frame: " & CStr(frame))
        End If

        'If frame = RawYM.MaxFrame Then
        'frame = 0
        'End If
        'For j = 0 To 2
        '    ym.voice(j).incr = 95108934.24 / (ym_set(2 * j) Or (ym_set(2 * j + 1) * 256))
        '    ym.chn(j).volume = ym_set(j + 8)
        'Next

        'ym.noise.incr = 95108934.24 / ym_set(6)

        'ym.enable = ym_set(7)
        'ym.shape.incr = (95108934.24 * 16) / (ym_set(11) Or (ym_set(12) * 256))
        'If ym_set(13) <> &HFF Then
        '    ym.eshape = ym_set(13)
        '    ym.shape.counter = 0
        'End If

    End Sub

    Sub openFile(ByVal fname As String)
        fs = New FileStream(fname, FileMode.Create, FileAccess.Write)
        br = New BinaryWriter(fs)
    End Sub

    Sub closeFile()
        br.Close()
    End Sub


    Sub writeFile()
        br.Write(ym.chn(0).fine)
        br.Write(ym.chn(0).coars)
        br.Write(ym.chn(1).fine)
        br.Write(ym.chn(1).coars)
        br.Write(ym.chn(2).fine)
        br.Write(ym.chn(2).coars)

        br.Write(ym.noisep)
        br.Write(Convert.ToByte(ym.enable Or &HC0))

        br.Write(ym.chn(0).volume)
        br.Write(ym.chn(1).volume)
        br.Write(ym.chn(2).volume)

        br.Write(ym.efine)
        br.Write(ym.ecoars)
        br.Write(ym.eshape)

    End Sub

    ' Destructor

    Protected Overrides Sub Finalize()
        If Not IsNothing(br) Then
            br.Close()
        End If
        MyBase.Finalize()
    End Sub

End Class
