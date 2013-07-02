Imports System.IO

Public Class YMImport
    Dim type1, type2 As String
    Dim frames As Integer
    Dim attributes As Integer
    Dim n_digidrums As Integer
    Dim clock As Integer
    Dim player_f As Integer
    Dim LoopFrame As Integer
    Dim xdata As Integer
    Dim SongName, AuthorName, SongComment As String

    Dim i, j As Integer

    Private fs As FileStream
    Private br As BinaryReader

    Public YMData(,) As Byte

    Private Declare Function htonl Lib "wsock32.dll" (ByVal a As Integer) As Integer
    Private Declare Function htons Lib "wsock32.dll" (ByVal a As Integer) As Integer


    Public Function Open(ByVal fname As String) As Boolean
        Dim i As Short

        fs = New FileStream(fname, FileMode.Open, FileAccess.Read)
        br = New BinaryReader(fs)
        type1 = br.ReadChars(4)
        type2 = br.ReadChars(8)
        frames = htonl(br.ReadInt32)
        attributes = htonl(br.ReadInt32)
        n_digidrums = htons(br.ReadInt16)
        clock = htonl(br.ReadInt32)
        player_f = htons(br.ReadInt16)
        LoopFrame = htonl(br.ReadInt32)
        xdata = htons(br.ReadInt16)


        GetString(SongName)
        GetString(AuthorName)
        GetString(SongComment)

        ReDim YMData(15, frames - 1)
        For i = 0 To 15
            For j = 0 To frames - 1
                YMData(i, j) = br.ReadByte
            Next
        Next

    End Function

    Public Function close()
        br.Close()
        Return True
    End Function

    Property MaxFrame() As Integer
        Get
            Return Me.frames
        End Get
        Set(ByVal Value As Integer)

        End Set
    End Property

    Public Function GetYMSet(ByVal frame As Integer, ByRef ymset() As Byte)
        Dim i As Integer

        For i = 0 To 15
            ymset(i) = YMData(i, frame)
        Next
    End Function

    Public Function PlayVbl(ByVal ym As WaveGen._YM)

    End Function

    Function GetString(ByRef s As String)
        Dim c As Byte

        Do While 1
            c = br.ReadByte()
            If c = 0 Then
                Exit Do
            End If
            s = s & String.Format(c)
        Loop
    End Function
End Class
