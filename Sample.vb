Public Class Sample
    Public Class wave
        Public len As Integer
        Public data() As Short

        Sub New(ByVal n As Integer)
            ReDim data(n)
            len = n
        End Sub
    End Class


    Public samp() As wave

End Class
