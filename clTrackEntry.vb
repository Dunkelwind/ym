<Serializable()> Public Class clTrackEntry
    Public seq As Byte
    Public note As Short
    Public instr As Byte
    Public cmd As Byte

    Sub New()
        note = 0
    End Sub

End Class
