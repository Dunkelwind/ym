
<Serializable()> Public Class Shape

    Const MAX_SHAPE As Integer = 64

    <Serializable()> Structure _SHAPE
        Public name As String
        Public para1 As Byte
        Public para2 As Byte
        Public para3 As Byte
        Public para4 As Byte
        Public para5 As Byte
        Public size As Short
        Public data() As Byte

    End Structure

    Public n As Integer
    Public shape_set(MAX_SHAPE) As _SHAPE
    Public ReadOnly dummy As Integer

    Public Sub New()
        Dim i As Integer

        For i = 0 To MAX_SHAPE - 1
            ReDim shape_set(i).data(255)
            shape_set(i).name = "Empty"
        Next
        n = MAX_SHAPE - 1
        ' default shape

        ReDim shape_set(MAX_SHAPE).data(255)
        shape_set(n + 1).name = "_default_"
        shape_set(n + 1).para1 = 1
        shape_set(n + 1).para2 = 0
        shape_set(n + 1).para3 = 0
        shape_set(n + 1).para4 = 0
        shape_set(n + 1).para5 = 0
        shape_set(n + 1).data(0) = 0
        shape_set(n + 1).data(1) = 0
        shape_set(n + 1).data(2) = &HE1
        dummy = MAX_SHAPE

    End Sub
End Class
