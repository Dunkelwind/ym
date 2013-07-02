﻿Public Class frmInfo

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


        Dim usedSeqs As New Generic.SortedDictionary(Of Integer, Integer)
        Dim seq As Integer
        Dim track As Integer

        For i = 0 To ymm.track.MaxRow
            For track = 0 To 2
                seq = ymm.track.GetSeq(track, i)
                If usedSeqs.ContainsKey(seq) Then
                    usedSeqs.Item(seq) += 1
                Else
                    usedSeqs.Add(seq, 1)
                End If
            Next

        Next

        s &= CStr(usedSeqs.Count) & " sequences used" & vbCrLf
        s &= "used sequences: "


        For Each seq In usedSeqs.Keys
            s &= CStr(seq) & ": " & CStr(usedSeqs.Item(seq)) & "x ;"
        Next
        s &= vbCrLf

        txtInfo.Text = s
    End Sub
End Class