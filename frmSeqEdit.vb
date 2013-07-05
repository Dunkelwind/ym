Imports System.Drawing.Imaging.PixelFormat
Imports ym.Interpreter

Public Class frmSeqEdit
    Inherits System.Windows.Forms.Form

    Const seqLines As Integer = 32

    Private WorkTrack As clTracks
    Private WorkSeq As clSequence

    Private f As New Font("Courier New", 10)
    Private f2 As New Font("Courier New", 10, FontStyle.Bold)

    Private fontWidth1 As Integer
    Private fontWidth3 As Integer

    Private ColumnPos(3) As Short
    Dim trackTranspose(3) As Short

    Dim viewedSeqs() As Short = {4, 1, 2}

    Private WorkLine As Short
    Private Const VisibleLines As Integer = 7

    Private bmp As Bitmap
    Private myGfx As Graphics
    Private mySize As Size
    Private myPen = New Pen(Color.Black)

    Dim trackPos As Integer = 0

    Dim NoteTable() As String = { _
    "C-1", "C#1", "D-1", "D#1", "E-1", "F-1", "F#1", "G-1", "G#1", "A-1", "A#1", "H-1", _
    "C-2", "C#2", "D-2", "D#2", "E-2", "F-2", "F#2", "G-2", "G#2", "A-2", "A#2", "H-2", _
    "C-3", "C#3", "D-3", "D#3", "E-3", "F-3", "F#3", "G-3", "G#3", "A-3", "A#3", "H-3", _
    "C-4", "C#4", "D-4", "D#4", "E-4", "F-4", "F#4", "G-4", "G#4", "A-4", "A#4", "H-4", _
    "C-5", "C#5", "D-5", "D#5", "E-5", "F-5", "F#5", "G-5", "G#5", "A-5", "A#5", "H-5", _
    "C-6", "C#6", "D-6", "D#6", "E-6", "F-6", "F#6", "G-6", "G#6", "A-6", "A#6", "H-6"}




    Sub New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

        ColumnPos(0) = 0
        ColumnPos(1) = 40
        ColumnPos(2) = 180
        ColumnPos(3) = 320

        mySize = New Size(600, (2 * VisibleLines + 1) * f.Height + Panel1.Height)
        Me.ClientSize = mySize                                           'set Windowsize
        bmp = New Bitmap(mySize.Width, mySize.Height, Format16bppRgb565)
        myGfx = Graphics.FromImage(bmp)
        Dim fSize As New SizeF    '
        fSize = myGfx.MeasureString("0123456789", f)
        fontWidth1 = Int(fSize.Width / 10)
        fSize = myGfx.MeasureString("000", f)
        fontWidth3 = Int(fSize.Width)


        Dim p As Point = cbSeq1.Location
        p.X = ColumnPos(1)
        cbSeq1.Location = p
        p.X = ColumnPos(2)
        cbSeq2.Location = p
        p.X = ColumnPos(3)
        cbSeq3.Location = p

        Array.Clear(trackTranspose, 0, 3)

    End Sub

    Private Sub frmSeqEdit_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        e.Cancel = True
        Me.Hide()
    End Sub

    Sub SetWorkSeq(ByRef wt As clTracks, ByRef ws As clSequence)
        Dim i As Short

        WorkTrack = wt
        WorkSeq = ws
        RemoveHandler cbSeq1.SelectedIndexChanged, AddressOf cbSeq1_SelectedIndexChanged
        RemoveHandler cbSeq2.SelectedIndexChanged, AddressOf cbSeq1_SelectedIndexChanged
        RemoveHandler cbSeq3.SelectedIndexChanged, AddressOf cbSeq1_SelectedIndexChanged
        cbSeq1.Items.Clear()
        cbSeq2.Items.Clear()
        cbSeq3.Items.Clear()
        For i = 0 To ws.count - 1
            cbSeq1.Items.Add(String.Format("{0,2}", i))
            cbSeq2.Items.Add(String.Format("{0,2}", i))
            cbSeq3.Items.Add(String.Format("{0,2}", i))
        Next
        AddHandler cbSeq1.SelectedIndexChanged, AddressOf cbSeq1_SelectedIndexChanged
        AddHandler cbSeq2.SelectedIndexChanged, AddressOf cbSeq1_SelectedIndexChanged
        AddHandler cbSeq3.SelectedIndexChanged, AddressOf cbSeq1_SelectedIndexChanged

        Me.VScrollBar1.SmallChange = 1
        Me.VScrollBar1.LargeChange = 10
        Me.VScrollBar1.Maximum = seqLines + Me.VScrollBar1.LargeChange
        Me.Refresh()

        'For i = 0 To ws.n
        '    Me.cmbSeqName.Items.Add("Seq" & Format("{0,3}", i))
        'Next
        'If Me.cmbSeqName.SelectedIndex = -1 Then
        '    Me.cmbSeqName.SelectedItem = 0
        'End If
        'Me.cmbSeqName.Text = Me.cmbSeqName.Items(0)
    End Sub

    Public Sub follow(ByVal row As Integer, ByVal seqPos As Integer, pm As Interpreter.Mode)
        Dim track As Integer

        trackPos = row
        If chkFollow.Checked = True And Me.WindowState = FormWindowState.Normal Then

            If pm <> Mode.PlaySeq Then
                For track = 0 To 2
                    viewedSeqs(track) = WorkTrack.GetSeq(track, row)
                Next


                RemoveHandler cbSeq1.SelectedIndexChanged, AddressOf cbSeq1_SelectedIndexChanged
                RemoveHandler cbSeq2.SelectedIndexChanged, AddressOf cbSeq1_SelectedIndexChanged
                RemoveHandler cbSeq3.SelectedIndexChanged, AddressOf cbSeq1_SelectedIndexChanged

                cbSeq1.SelectedIndex = viewedSeqs(0)
                cbSeq2.SelectedIndex = viewedSeqs(1)
                cbSeq3.SelectedIndex = viewedSeqs(2)

                AddHandler cbSeq1.SelectedIndexChanged, AddressOf cbSeq1_SelectedIndexChanged
                AddHandler cbSeq2.SelectedIndexChanged, AddressOf cbSeq1_SelectedIndexChanged
                AddHandler cbSeq3.SelectedIndexChanged, AddressOf cbSeq1_SelectedIndexChanged
            Else
                viewedSeqs(0) = cbSeq1.SelectedIndex
                viewedSeqs(1) = cbSeq2.SelectedIndex
                viewedSeqs(2) = cbSeq3.SelectedIndex
            End If


            Me.VScrollBar1.Value = seqPos
        End If
    End Sub


    Private Sub cmbSeqName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cmdPlay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPlay.Click
        Dim si As _SND_INFO

        si.start = 0
        si.last = 1
        si.speed = CShort(Form1.txtSpeed.Text)

        Dim s() As Short = {cbSeq1.SelectedIndex, cbSeq2.SelectedIndex, cbSeq3.SelectedIndex}
        Form1.interpreter.init_voices(si, Interpreter.Mode.PlaySeq, s)
        Form1.WaveGen.PlayFlag = True

    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        DrawSeq(e.Graphics)
    End Sub

    Private Sub DrawSeq(ByRef gfx As Graphics)
        Dim i, min, max, k, x, y As Short
        Dim fnt As Font
        Dim s As String
        Dim d As Short
        Dim se As ym.clSequence._SEQENTRY
        Dim brush As Brush


        myGfx.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height)    'alles löschen

        min = Me.VScrollBar1.Value - VisibleLines
        y = 0
        If min < 0 Then
            y = -min * f.Height
            min = 0
        End If

        max = Me.VScrollBar1.Value + VisibleLines
        If max > seqLines Then
            max = seqLines
        End If

        '--- Backgroud for Track-Nr
        myGfx.FillRectangle(Brushes.Tan, 0, y, fontWidth3, (max - min + 1) * f.Height)  'löschen


        '--- Spalten-Linien

        For i = 1 To 3
            x = ColumnPos(i) - fontWidth1
            myGfx.DrawLine(myPen, x, y, x, y + (max - min + 1) * f.Height)
        Next


        For k = min To max
            If y = VisibleLines * f.Height Then
                fnt = f2
                brush = Brushes.Blue
            Else
                fnt = f
                brush = Brushes.Black
            End If

            ' Line Number
            myGfx.DrawString(String.Format("{0,3}", k), fnt, Brushes.Green, ColumnPos(0), y)

            ' 3 Sequences
            For i = 0 To 2
                se = WorkSeq.seqs(viewedSeqs(i)).seq(k)
                'Note
                d = se.note

                Select Case d
                    Case -1
                        s = "---"
                    Case &HFF
                        s = "End"
                    Case Else
                        If d < 6 * 12 Then
                            d += trackTranspose(i)                   ' transposed by track
                            s = NoteTable(d)
                        Else
                            s = String.Format(" {0:X2}", d)         ' 
                        End If
                End Select
                myGfx.DrawString(s, fnt, brush, ColumnPos(i + 1), y)

                'Shape
                d = se.shape
                s = String.Format("{0}", d)

                If (d And &HE0) <> 0 Then
                    'seq_instr
                    s &= "  " & String.Format("{0:X2}", se.xtra)
                End If

                myGfx.DrawString(s, fnt, brush, ColumnPos(i + 1) + fontWidth1 * 4, y)
            Next i
            y += f.Height
        Next k
        gfx.DrawImage(bmp, 0, 0)
    End Sub

    Private Sub VScrollBar1_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles VScrollBar1.ValueChanged
        DrawSeq(Me.CreateGraphics())
    End Sub
    Public Function handleKey(ByVal key As Integer) As Boolean
        Select Case key
            'Case 37, 39 'left,right
            '    setCursor(key)
            '    Return True
            Case 38 'up
                If Me.VScrollBar1.Value > 0 Then
                    Me.VScrollBar1.Value -= 1
                End If
                Return True
            Case 40 'down
                If Me.VScrollBar1.Maximum > Me.VScrollBar1.Value Then
                    Me.VScrollBar1.Value += 1
                End If
                Return True
        End Select
        Return False
    End Function


    Private Sub TimerCursor_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerCursor.Tick
        Static state As Byte = 0

        Dim gfx As Graphics
        Dim p As Pen
        gfx = Me.CreateGraphics


        If state = 0 Then
            p = New Pen(Color.Black)
        Else
            p = New Pen(Color.White)
        End If
        gfx.DrawRectangle(p, ColumnPos(1), VisibleLines * f.Height, fontWidth1, f.Height)

        state = state Xor 1

    End Sub


    Private Sub frmSeqEdit_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        TimerCursor.Enabled = True
    End Sub

    Private Sub frmSeqEdit_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Deactivate
        TimerCursor.Enabled = False
    End Sub

    Private Sub cbSeq1_SelectedIndexChanged(sender As Object, e As System.EventArgs)

        viewedSeqs(0) = cbSeq1.SelectedIndex
        viewedSeqs(1) = cbSeq2.SelectedIndex
        viewedSeqs(2) = cbSeq3.SelectedIndex

        DrawSeq(Me.CreateGraphics())

    End Sub

  
    Private Sub chkBoxTranspose_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkBoxTranspose.CheckedChanged
        If chkBoxTranspose.Checked Then
            For track As Integer = 0 To 2
                trackTranspose(track) = WorkTrack.GetNote(track, trackPos)
            Next
        Else
            Array.Clear(trackTranspose, 0, 3)
        End If
        DrawSeq(Me.CreateGraphics())
    End Sub
End Class
