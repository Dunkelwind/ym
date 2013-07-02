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
End Class
