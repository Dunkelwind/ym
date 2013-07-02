<Serializable()> Public Class Instr
    Const MAX_INSTR As Integer = 64
    Public instr(MAX_INSTR)() As Byte
    Public n As Integer
    Public ReadOnly dummy As Integer

    Sub New()
        Dim i As Integer

        For i = 0 To MAX_INSTR - 1
            instr(i) = New Byte(255) {}

            instr(i)(0) = &HE1          'end
        Next
        ' default instr
        instr(MAX_INSTR) = New Byte(255) {}
        instr(MAX_INSTR)(0) = 1
        instr(MAX_INSTR)(1) = 0
        instr(MAX_INSTR)(2) = 0
        instr(MAX_INSTR)(3) = 0
        instr(MAX_INSTR)(4) = 0
        instr(MAX_INSTR)(5) = 0
        instr(MAX_INSTR)(6) = 0
        instr(MAX_INSTR)(7) = &HE1
        n = MAX_INSTR - 1
        dummy = MAX_INSTR
    End Sub

End Class
