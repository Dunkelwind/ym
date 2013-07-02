<Serializable()> Public Class clTracks
    Public Track(2) As clTrack
    Private pMaxRow As Short

    Sub New()
        Track(0) = Nothing
    End Sub
    Sub New(ByVal n As Short)
        Track(0) = New clTrack(n)
        Track(1) = New clTrack(n)
        Track(2) = New clTrack(n)
        pMaxRow = 0
    End Sub

    Property MaxRow() As Short
        Get
            Return pMaxRow
        End Get
        Set(ByVal Value As Short)
            pMaxRow = Value
        End Set
    End Property

    Public Sub set_entry(ByVal t As Short, ByVal r As Short, ByRef d() As Byte)
        Dim s16 As Short

        Track(t).row(r).seq = d(0)
        s16 = d(1)
        If s16 And &H80 Then
            s16 = s16 Or &HFF00
        End If
        Track(t).row(r).note = s16
        Track(t).row(r).instr = d(2)
        Track(t).row(r).cmd = d(3)
    End Sub

    Public Function GetSeq(ByVal t As Short, ByVal r As Short) As Byte
        Return Track(t).row(r).seq
    End Function

    Public Sub SetSeq(ByVal t As Short, ByVal r As Short, ByVal v As Byte)
        Track(t).row(r).seq = v
    End Sub

    Public Function GetNote(ByVal t As Short, ByVal r As Short) As Short
        Return Convert.ToInt16(Track(t).row(r).note)
    End Function

    Public Sub SetNote(ByVal t As Short, ByVal r As Short, ByVal v As Short)
        Track(t).row(r).note = v
    End Sub

    Public Function GetInstr(ByVal t As Short, ByVal r As Short) As Byte
        Return Track(t).row(r).instr
    End Function

    Public Sub SetInstr(ByVal t As Short, ByVal r As Short, ByVal v As Byte)
        Track(t).row(r).instr = v
    End Sub

    Public Function GetCmd(ByVal t As Short, ByVal r As Short) As Byte
        Return Track(t).row(r).cmd
    End Function

    Public Sub SetCmd(ByVal t As Short, ByVal r As Short, ByVal v As Byte)
        Track(t).row(r).cmd = v
    End Sub
End Class
