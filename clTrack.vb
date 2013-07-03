<Serializable()> Public Class clTrack
    Public row() As clTrackEntry

    Sub New()
        ReDim row(0)
    End Sub
    Sub New(ByVal n As Short)
        Dim i As Short
        n -= 1
        ReDim row(n)
        For i = 0 To n
            row(i) = New clTrackEntry
        Next
    End Sub

    Sub usedSeqs(ByRef list As Generic.SortedList(Of Integer, Integer), first As Integer, last As Integer)
        Dim seq As Integer


        Dim i As Integer

        For i = first To last
            seq = row(i).seq
            If list.ContainsKey(seq) Then
                list.Item(seq) += 1
            Else
                list.Add(seq, 1)
            End If
        Next


    End Sub
End Class
