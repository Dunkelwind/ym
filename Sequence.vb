Imports System.IO

<Serializable()> Public Class Sequence
    Const MAX_SEQ As Integer = 256
    Public n As Short
    Public temp As Short

    Structure _SEQENTRY
        Public note As Short
        Public shape As Byte
        Public xtra As Byte
    End Structure

    Structure _SEQ
        Public seq() As _SEQENTRY
    End Structure


    Public seqs(MAX_SEQ - 1 + 1) As _SEQ




    Sub New()
        Dim i, j As Integer
        Dim se As _SEQENTRY

        n = MAX_SEQ - 1

        se.note = -1
        se.shape = 0
        se.xtra = 0

        For i = 0 To MAX_SEQ - 1 + 1
            ReDim seqs(i).seq(255) '            seqs.A
            For j = 0 To 255
                seqs(i).seq(j) = se
            Next
        Next

        ' temporäre Dummy-Sequence

        temp = MAX_SEQ - 1 + 1

        For i = 0 To 30
            seqs(temp).seq(i).note = 0
            seqs(temp).seq(i).shape = 0
            seqs(temp).seq(i).xtra = 0
        Next
        i += 1
        seqs(temp).seq(i).note = 1 * 12
        seqs(temp).seq(i).shape = 0
        seqs(temp).seq(i).xtra = 0
        i += 1
        seqs(temp).seq(i).note = &HFF               ' End
        seqs(temp).seq(i).shape = 0
        seqs(temp).seq(i).xtra = 0


    End Sub

    Public Sub clear()
        Dim i, j As Integer
        Dim se As _SEQENTRY

        se.note = -1
        se.shape = 0
        se.xtra = 0

        For i = 0 To MAX_SEQ - 1 + 1
            For j = 0 To 255
                seqs(i).seq(j) = se
            Next
        Next
    End Sub
    Public Sub save(ByVal fs As FileStream)
        Dim i, j As Integer

        For i = 0 To MAX_SEQ - 1
            For j = 0 To 255

                fs.WriteByte((seqs(i).seq(j).note >> 8) And &HFF)
                fs.WriteByte(seqs(i).seq(j).note And &HFF)
                fs.WriteByte(seqs(i).seq(j).shape)
                fs.WriteByte(seqs(i).seq(j).xtra)
            Next
        Next
    End Sub
    Function depack(ByVal n As Integer, ByRef packed() As Byte) As Integer
        Dim i As Integer = 0
        Dim pi As Integer = 0
        Dim b As Byte
        Dim counter As Short = 0
        Dim counterInit As Short = 0

        Dim se As _SEQENTRY



        For Each se In seqs(n).seq
            se.note = -1
            se.shape = 0
            se.xtra = 0
        Next

        Do While pi < 64                                        ' max 64 Bytes
            counter -= 1
            If counter < 0 Then
                counter = counterInit
                Do
                    b = packed(pi)
                    pi += 1
                    Select Case b
                        Case &HFF
                            seqs(n).seq(i).note = &HFF
                            Debug.WriteLine(n.ToString & ": " & i.ToString)
                            Return (i)
                        Case &HFE                               'nach &Hfe, &Hxx folgt sofort Note
                            counter = packed(pi)
                            pi += 1
                            counterInit = counter

                        Case &HFD                               'Pause
                            counter = packed(pi)
                            pi += 1
                            counterInit = counter
                            Exit Do

                        Case Else
                            seqs(n).seq(i).note = b
                            b = packed(pi)
                            pi += 1
                            seqs(n).seq(i).shape = b
                            If (b And &HE0) > 0 Then
                                seqs(n).seq(i).xtra = packed(pi)
                                pi += 1
                            End If
                            Exit Do

                    End Select
                Loop
            End If
            i += 1
        Loop
        seqs(n).seq(i).note = &HFF          ' end
    End Function

    'for unpacked sequences (TFMX)
    Function coyp(ByVal n As Integer, ByRef packed() As Byte) As Integer
        Dim i As Integer
        Dim pi As Integer = 0

        For i = 0 To 31
            seqs(n).seq(i).note = packed(pi) - 1
            pi += 1
            seqs(n).seq(i).shape = packed(pi)
            pi += 1
        Next
        seqs(n).seq(i).note = &HFF          ' end
    End Function
    ' sequence compress, needed by TFMX-import because TFMX-sequences are straight datas

    'Sub compress(ByVal n As Short)
    '    Dim i, j, k, l As Short
    '    Dim note, shape As Byte
    '    Dim cseq(63) As Byte

    '    For i = 0 To n
    '        k = 0
    '        note = Seq(i, 0)
    '        shape = Seq(i, 1)
    '        j = 2
    '        If (note = 0) And (shape = 0) Then
    '            Do While (Seq(i, j) = 0) And (Seq(i, j + 1) = 0) And (j < 32)
    '                j += 2
    '            Loop
    '            If j = 32 Then
    '                cseq(k) = &HFD
    '                cseq(k + 1) = 31
    '                cseq(k + 2) = &HFF
    '                k += 3
    '            Else
    '                cseq(k) = &HFD
    '                cseq(k + 1) = j \ 2
    '                k += 2
    '                note = Seq(i, j)
    '                shape = Seq(i, j + 1)
    '                j += 2
    '            End If
    '        End If

    '        Do While j < 32
    '            l = 0
    '            Do While (Seq(i, j) = 0) And (Seq(i, j + 1) = 0) And (j < 32)
    '                j += 2
    '                l += 1
    '            Loop
    '            If l > 0 Then
    '                cseq(k) = &HFE
    '                cseq(k + 1) = l
    '                k += 2
    '            End If
    '            cseq(k) = note
    '            cseq(k + 1) = shape
    '            k += 2
    '            note = Seq(i, j)
    '            shape = Seq(i, j + 1)
    '            j += 2
    '        Loop
    '        cseq(k) = &HFF
    '        For j = 0 To k
    '            Seq(i, j) = cseq(j)
    '        Next
    '    Next

    'End Sub
End Class
