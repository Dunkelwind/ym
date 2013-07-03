Imports System.IO

' imports an Atari TFMX music file

Public Class AtariTFMX
    Dim type1, type2, type3 As String
    Dim offsets(6) As Integer
    Dim sizes(13) As Short

    Private fs As FileStream
    Private br As BinaryReader

    Private Declare Function htonl Lib "wsock32.dll" (ByVal a As Integer) As Integer
    Private Declare Function htons Lib "wsock32.dll" (ByVal a As Integer) As Integer

    Public Sub type(ByRef t1 As String, ByRef t2 As String, ByRef t3 As String)
        t1 = type1
        t2 = type2
        t3 = type3
    End Sub

    Public Function Open(ByVal fname As String) As Boolean
        Dim i As Short

        fs = New FileStream(fname, FileMode.Open, FileAccess.Read)
        br = New BinaryReader(fs)
        type1 = br.ReadChars(4)
        type2 = ""

        If type1 = "COSO" Or type1 = "MMME" Then
            For i = 0 To offsets.GetUpperBound(0)
                offsets(i) = htonl(br.ReadInt32)
            Next

            type2 = br.ReadChars(4)
        End If
        For i = 0 To sizes.GetUpperBound(0)
            sizes(i) = htons(br.ReadInt16)
        Next

        'sizes(0) = 22 - 1   'Instr
        'sizes(1) = 32 - 1   'Shapes

    End Function

    Public Function LoadShapes(ByRef shapes As Shape) As Boolean
        Dim i, j, s As Short
        Dim b As Byte
        Dim f As Boolean

        If sizes(1) > shapes.n Then
            MsgBox("maximal 64 Shapes")
            Return False
        End If

        Dim s_offsets(sizes(1) + 1) As Integer    'sizes(1): Shape-Offsets-1

        Select Case type1
            Case "COSO", "MMME"
                readOffsets(offsets(1), s_offsets, sizes(1))
                s_offsets(sizes(1) + 1) = offsets(2) ' end of shapes is start of sequences



            Case "TFMX"
                fs.Position = 32 + 64 * (sizes(0) + 1)  ' skip header & instruments

                For i = 0 To sizes(1) + 1   ' shapes always 64 Bytes long
                    s_offsets(i) = i * 64
                Next
        End Select


        For i = 0 To sizes(1)
            'If s_offsets(i + 1) = 0 Then
            '    Exit For
            'End If
            shapes.shape_set(i).para1 = br.ReadByte
            shapes.shape_set(i).para2 = br.ReadByte
            shapes.shape_set(i).para3 = br.ReadByte
            shapes.shape_set(i).para4 = br.ReadByte
            shapes.shape_set(i).para5 = br.ReadByte
            s = 0   ' data size
            f = True
            For j = 0 To s_offsets(i + 1) - (s_offsets(i) + 5) - 1
                b = br.ReadByte
                shapes.shape_set(i).data(j) = b
                If f And b < &HE0 Then
                    s += 1
                Else
                    f = False
                End If
            Next
            shapes.shape_set(i).size = s
            shapes.shape_set(i).name = "Shape " & String.Format("{0:00}", i)
        Next

        Return True
    End Function

    Public Function LoadInstr(ByRef instrs As Instr) As Boolean
        Dim i, j As Short

        If sizes(0) > instrs.n Then
            Return False
        End If
        Dim i_offsets(sizes(0) + 1) As Integer     'Instruments-Offsets

        Select Case type1
            Case "COSO", "MMME"
                readOffsets(offsets(0), i_offsets, sizes(0))
                i_offsets(sizes(0) + 1) = offsets(1) ' end of instruments is start of shapes

            Case "TFMX"
                fs.Position = 32

                For i = 0 To sizes(0) + 1  ' instr always 64 Bytes long
                    i_offsets(i) = i * 64
                Next
        End Select

        For i = 0 To sizes(0)
            For j = 0 To i_offsets(i + 1) - i_offsets(i) - 1
                instrs.instr(i)(j) = br.ReadByte
            Next
        Next
        Return True
    End Function

    Public Function LoadSeq(ByRef seqs As clSequence) As Boolean
        Dim i, j As Short
        Dim size As Integer
        Dim b As Byte
        Dim buffer(255) As Byte
        Dim m16_32 As Short

        If sizes(2) > seqs.n Then
            Return False
        End If
        Dim s_offsets(sizes(2) + 1) As Integer    'seq-Offsets

        seqs.clear()

        Select Case type1
            Case "COSO", "MMME"

                readOffsets(offsets(2), s_offsets, sizes(2))
                s_offsets(sizes(2) + 1) = offsets(3) ' end of last seq is start of tracks

            Case "TFMX"
                fs.Position = 32 + 64 * (sizes(0) + 1) + 64 * (sizes(1) + 1)
                For i = 0 To sizes(2) + 1   ' seqs always 64 Bytes long
                    s_offsets(i) = i * 64
                Next
        End Select

        For i = 0 To sizes(2)
            For j = 0 To s_offsets(i + 1) - s_offsets(i) - 1
                b = br.ReadByte
                '                seqs.Seq(i, j) = b
                buffer(j) = b
            Next

            If type1 = "TFMX" Then
                seqs.coyp(i, buffer)
            Else
                seqs.depack(i, buffer)
            End If


        Next
        seqs.count = sizes(2) + 1
        'If type1 = "TFMX" Then
        '    seqs.compress(sizes(2))
        'End If

        Return True
    End Function

    Private Sub readOffsets(ByVal pos As Integer, ByRef o() As Integer, ByVal max As Short)
        Dim i As Short
        Dim m16_32 As Short

        fs.Position = pos
        m16_32 = htons(br.ReadInt16)
        fs.Position = pos

        If m16_32 = 0 Then                              ' some music files use 32-Bit offsets
            For i = 0 To max
                o(i) = htonl(br.ReadInt32)
            Next
        Else
            For i = 0 To max
                o(i) = htons(br.ReadInt16)
            Next
        End If
    End Sub

    Public Sub LoadTracks(ByRef tracks As clTracks)
        Dim n, i, j, k As Integer
        Dim d(3) As Byte

        Select Case type1
            Case "COSO", "MMME"
                fs.Position = offsets(3)
                n = (offsets(4) - offsets(3)) / (3 * 4)
            Case "TFMX"
                fs.Position = 32 + (64 * (sizes(0) + 1 + sizes(1) + 1 + sizes(2) + 1))
                n = sizes(3)
                ' special case
                ' fs.Position = &HDA0

        End Select

        For i = 0 To n - 1
            For j = 0 To 2
                For k = 0 To 3
                    d(k) = br.ReadByte
                Next
                tracks.set_entry(j, i, d)
            Next
        Next
        tracks.MaxRow = n - 1
    End Sub

    Public Sub LoadSndInfo(ByRef si() As Interpreter._SND_INFO)
        Dim n, i As Integer
        Dim d As Integer

        Select Case type1
            Case "COSO", "MMME"
                fs.Position = offsets(4)                    'Soundinfo Table
                If offsets(5) = 0 Then
                    n = (fs.Length - offsets(4))
                Else
                    n = (offsets(5) - offsets(4))
                End If

                d = fs.Length - offsets(4)

                If d < n Then
                    MsgBox("Missing " & Format(n - d, "0") & " Bytes")
                    n = d
                End If

                n = n \ (3 * 2) - 1
                ReDim si(n)
                For i = 0 To n
                    si(i).start = htons(br.ReadInt16)
                    si(i).last = htons(br.ReadInt16)
                    si(i).speed = htons(br.ReadInt16)
                Next
            Case "TFMX"
                ReDim si(0)
                si(0).start = 0
                si(0).last = 100
                si(0).speed = 5
        End Select

    End Sub

    Public Function LoadSamples(ByRef digi As Sample) As Boolean
        Dim s(30) As Integer
        Dim i, j, k, l As Integer
        Dim d As Short

        type3 = ""

        If offsets(6) = 0 Then
            Return False
        End If

        If (offsets(6) + 256) > fs.Length Then
            Return False
        End If
        fs.Position = offsets(6) + 248                  'Samples

        type3 = br.ReadChars(4)
        'If type3 <> "MARC" Then
        '    type3 = ""
        '    Return False
        'End If

        fs.Position = offsets(6)
        j = 0
        For i = 0 To 30                                 ' max 31 Samples
            s(i) = htons(br.ReadInt16)                  ' Sample Start-Offset
            d = htons(br.ReadInt16)                     ' Flags
            j += 1
            If d = 16 Then
                Exit For
            End If

            br.ReadBytes(4)                             ' skip
        Next

        j -= 2                                          ' letzter Wert in s(x) ist nur der End-Offset (kein Sample)
        ReDim digi.samp(j)

        For i = 0 To j
            l = (s(i + 1) - s(i)) - 1

            digi.samp(i) = New Sample.wave(l)
            fs.Position = offsets(6) + s(i)
            For k = 0 To l
                d = br.ReadByte()

                d = (d Xor &H80) * 20

                digi.samp(i).data(k) = d
            Next
        Next

        Return True
    End Function

    Public Function close()
        br.Close()
        Return True
    End Function



    ' Destructor

    Protected Overrides Sub Finalize()
        If Not IsNothing(br) Then
            br.Close()
        End If
        MyBase.Finalize()
    End Sub
End Class
