Public Class clVScrollBar
    Inherits System.Windows.Forms.VScrollBar


    Sub New()
        MyBase.New()
    End Sub

    Protected Overloads Overrides Function IsInputkey(ByVal keydata As Keys) As Boolean
        Return True
    End Function
End Class
