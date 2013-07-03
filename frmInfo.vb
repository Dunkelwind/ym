Public Class frmInfo

    Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

    End Sub
    Sub print(ByRef ymm As clYMm)

        Dim s As String = ""

        s &= "ID1: " & ymm.id1 & vbCrLf
        s &= "ID2: " & ymm.id2 & vbCrLf
        s &= "ID3: " & ymm.id3 & vbCrLf


        Dim i As Integer = 0
        For Each si As Interpreter._SND_INFO In ymm.sndInfo
            s &= CStr(i) & "= Start: " & CStr(si.start) & " Last: " & CStr(si.last) & " Speed: " & CStr(si.speed) & vbCrLf
            i += 1
        Next


        Dim usedSeqs As New Generic.SortedList(Of Integer, Integer)
        For i = 0 To 2
            ymm.tracks.Tracks(i).usedSeqs(usedSeqs, 0, ymm.tracks.MaxRow)
        Next



        s &= CStr(usedSeqs.Count) & " sequences used" & vbCrLf
        s &= "used sequences: "


        For Each seq As Integer In usedSeqs.Keys
            s &= CStr(seq) & ": " & CStr(usedSeqs.Item(seq)) & "x ;"
        Next
        s &= vbCrLf

        txtInfo.Text = s
    End Sub

    'Public Shared Function Max( Integer))( source As IEnumerable() ) As TSource
End Class