Imports System.Drawing.Imaging.PixelFormat
Imports ym.Interpreter

Public Class frmSeqEdit
    Inherits System.Windows.Forms.Form

    Const seqLines As Integer = 32

    Private WorkTrack As clTracks
    Private WorkSeq As Sequence

    Private f As New Font("Courier New", 10)
    Private f2 As New Font("Courier New", 10, FontStyle.Bold)

    Private fontWidth1 As Integer
    Private fontWidth3 As Integer

    Private ColumnPos(3) As Short
    Public viewedSeqs() As Short = {4, 1, 2}

    Private WorkLine As Short
    Private Const VisibleLines As Integer = 7

    Private bmp As Bitmap
    Private myGfx As Graphics
    Private mySize As Size
    Private myPen = New Pen(Color.Black)



    Dim NoteTable() As String = { _
    "C-1", "C#1", "D-1", "D#1", "E-1", "F-1", "F#1", "G-1", "G#1", "A-1", "A#1", "H-1", _
    "C-2", "C#2", "D-2", "D#2", "E-2", "F-2", "F#2", "G-2", "G#2", "A-2", "A#2", "H-2", _
    "C-3", "C#3", "D-3", "D#3", "E-3", "F-3", "F#3", "G-3", "G#3", "A-3", "A#3", "H-3", _
    "C-4", "C#4", "D-4", "D#4", "E-4", "F-4", "F#4", "G-4", "G#4", "A-4", "A#4", "H-4", _
    "C-5", "C#5", "D-5", "D#5", "E-5", "F-5", "F#5", "G-5", "G#5", "A-5", "A#5", "H-5", _
    "C-6", "C#6", "D-6", "D#6", "E-6", "F-6", "F#6", "G-6", "G#6", "A-6", "A#6", "H-6"}

#Region " Vom Windows Form Designer generierter Code "

    Public Sub New()
        MyBase.New()

        ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
        InitializeComponent()

        ' Initialisierungen nach dem Aufruf InitializeComponent() hinzufügen
        myNew()


    End Sub

    ' Die Form überschreibt den Löschvorgang der Basisklasse, um Komponenten zu bereinigen.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    ' Für Windows Form-Designer erforderlich
    Private components As System.ComponentModel.IContainer

    'HINWEIS: Die folgende Prozedur ist für den Windows Form-Designer erforderlich
    'Sie kann mit dem Windows Form-Designer modifiziert werden.
    'Verwenden Sie nicht den Code-Editor zur Bearbeitung.
    Friend WithEvents cmdPlay As System.Windows.Forms.Button
    Friend WithEvents VScrollBar1 As System.Windows.Forms.VScrollBar
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkFollow As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TimerCursor As System.Windows.Forms.Timer
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cbSeq3 As System.Windows.Forms.ComboBox
    Friend WithEvents cbSeq2 As System.Windows.Forms.ComboBox
    Friend WithEvents cbSeq1 As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.cmdPlay = New System.Windows.Forms.Button()
        Me.VScrollBar1 = New System.Windows.Forms.VScrollBar()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cbSeq3 = New System.Windows.Forms.ComboBox()
        Me.cbSeq2 = New System.Windows.Forms.ComboBox()
        Me.cbSeq1 = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkFollow = New System.Windows.Forms.CheckBox()
        Me.TimerCursor = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdPlay
        '
        Me.cmdPlay.Location = New System.Drawing.Point(410, 0)
        Me.cmdPlay.Name = "cmdPlay"
        Me.cmdPlay.Size = New System.Drawing.Size(40, 24)
        Me.cmdPlay.TabIndex = 2
        Me.cmdPlay.Text = "Play"
        '
        'VScrollBar1
        '
        Me.VScrollBar1.Dock = System.Windows.Forms.DockStyle.Right
        Me.VScrollBar1.Location = New System.Drawing.Point(530, 0)
        Me.VScrollBar1.Name = "VScrollBar1"
        Me.VScrollBar1.Size = New System.Drawing.Size(16, 293)
        Me.VScrollBar1.TabIndex = 3
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.Controls.Add(Me.cbSeq3)
        Me.Panel1.Controls.Add(Me.cbSeq2)
        Me.Panel1.Controls.Add(Me.cbSeq1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.chkFollow)
        Me.Panel1.Controls.Add(Me.cmdPlay)
        Me.Panel1.Location = New System.Drawing.Point(0, 248)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(546, 44)
        Me.Panel1.TabIndex = 4
        '
        'cbSeq3
        '
        Me.cbSeq3.FormattingEnabled = True
        Me.cbSeq3.Location = New System.Drawing.Point(174, 3)
        Me.cbSeq3.Name = "cbSeq3"
        Me.cbSeq3.Size = New System.Drawing.Size(53, 21)
        Me.cbSeq3.TabIndex = 11
        '
        'cbSeq2
        '
        Me.cbSeq2.FormattingEnabled = True
        Me.cbSeq2.Location = New System.Drawing.Point(115, 3)
        Me.cbSeq2.Name = "cbSeq2"
        Me.cbSeq2.Size = New System.Drawing.Size(53, 21)
        Me.cbSeq2.TabIndex = 10
        '
        'cbSeq1
        '
        Me.cbSeq1.FormattingEnabled = True
        Me.cbSeq1.Location = New System.Drawing.Point(46, 3)
        Me.cbSeq1.Name = "cbSeq1"
        Me.cbSeq1.Size = New System.Drawing.Size(53, 21)
        Me.cbSeq1.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(3, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 18)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Seq:"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(456, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 16)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "follow"
        '
        'chkFollow
        '
        Me.chkFollow.Checked = True
        Me.chkFollow.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkFollow.Location = New System.Drawing.Point(502, 5)
        Me.chkFollow.Name = "chkFollow"
        Me.chkFollow.Size = New System.Drawing.Size(16, 16)
        Me.chkFollow.TabIndex = 3
        Me.chkFollow.Text = "CheckBox1"
        '
        'TimerCursor
        '
        Me.TimerCursor.Interval = 500
        '
        'frmSeqEdit
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(546, 293)
        Me.Controls.Add(Me.VScrollBar1)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmSeqEdit"
        Me.Text = "frmSeqEdit"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region


    Sub mynew()

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

        Dim i As Integer


        For i = 0 To 63
            cbSeq1.Items.Add(String.Format("{0,2}", i))
            cbSeq2.Items.Add(String.Format("{0,2}", i))
            cbSeq3.Items.Add(String.Format("{0,2}", i))
        Next



    End Sub

    Private Sub frmSeqEdit_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        e.Cancel = True
        Me.Hide()
    End Sub

    Sub SetWorkSeq(ByRef wt As clTracks, ByRef ws As Sequence)
        Dim i As Short

        WorkTrack = wt
        WorkSeq = ws
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

    Public Sub follow(ByVal i As Integer, ByVal seqPos As Integer, pm As Interpreter.Mode)
        Dim j As Integer


        If chkFollow.Checked = True And Me.WindowState = FormWindowState.Normal Then

            If pm <> Mode.PlaySeq Then
                For j = 0 To 2
                    viewedSeqs(j) = WorkTrack.GetSeq(j, i)
                Next
                cbSeq1.SelectedIndex = viewedSeqs(0)
                cbSeq2.SelectedIndex = viewedSeqs(1)
                cbSeq3.SelectedIndex = viewedSeqs(2)
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

    '    Sub ViewSeq()
    '        Dim seq_nr As Short
    '        Dim b As Byte
    '        Dim i, j, nr As Short
    '        Dim duration As Short = -1
    '        Dim cr_f As Boolean = False
    '        Dim s As String
    '        Dim myRow As DataRow

    '        dtSeq.Clear()

    '        seq_nr = Me.cmbSeqName.SelectedIndex
    '        i = 0
    '        nr = 0
    '        Do
    'wait:       For j = 0 To duration
    '                myRow = dtSeq.NewRow()
    '                myRow.Item(0) = nr
    '                myRow.Item(1) = ""
    '                myRow.Item(2) = ""
    '                myRow.Item(3) = ""
    '                nr += 1
    '                dtSeq.Rows.Add(myRow)
    '            Next

    '            b = WorkSeq.Seq(seq_nr, i)
    '            Select Case b
    '                Case &HFF
    '                    Exit Do
    '                Case &HFD
    '                    duration = WorkSeq.Seq(seq_nr, i + 1)
    '                    i += 2
    '                    GoTo wait
    '                Case &HFE
    '                    duration = WorkSeq.Seq(seq_nr, i + 1)
    '                    i += 2
    '            End Select

    '            myRow = dtSeq.NewRow()
    '            myRow.Item(0) = nr
    '            nr += 1
    '            b = WorkSeq.Seq(seq_nr, i)
    '            i += 1
    '            If b < 6 * 12 Then
    '                myRow.Item(1) = NoteTable(b)
    '            Else
    '                myRow.Item(1) = String.Format("{0:X2}", b)
    '            End If
    '            ' shape
    '            b = WorkSeq.Seq(seq_nr, i)
    '            i += 1
    '            myRow.Item(2) = String.Format("{0}", b)
    '            If (b And &HE0) <> 0 Then
    '                'seq_instr
    '                myRow.Item(3) = String.Format("{0:X2}, ", WorkSeq.Seq(seq_nr, i))
    '                i += 1
    '            Else
    '                myRow.Item(3) = ""
    '            End If
    '            dtSeq.Rows.Add(myRow)
    '        Loop


    'Do
    '    b = WorkSeq.Seq(seq_nr, i)
    '    i += 1
    '    Select Case b
    '        Case &HFD To &HFF
    '            'If cr_f Then
    '            '    Me.RichTextBox1.AppendText(Chr(13))
    '            '    cr_f = False
    '            'End If
    '            Me.RichTextBox1.SelectionFont = f2
    '            Select Case b
    '                Case &HFF
    '                    Me.RichTextBox1.AppendText("end" & Chr(13))
    '                    Exit Do
    '                Case &HFD
    '                    Me.RichTextBox1.AppendText("FD: " & String.Format("{0,2}", WorkSeq.Seq(seq_nr, i)) & Chr(13))
    '                    i += 1
    '                Case &HFE
    '                    Me.RichTextBox1.AppendText(String.Format("duration({0,2}), ", WorkSeq.Seq(seq_nr, i)))
    '                    i += 1
    '            End Select
    '        Case Else
    '            Me.RichTextBox1.SelectionFont = f
    '            ' note
    '            If b < 6 * 12 Then
    '                s = NoteTable(b)
    '            Else
    '                s = String.Format("{0:X2}", b)
    '            End If
    '            ' shape
    '            b = WorkSeq.Seq(seq_nr, i)
    '            i += 1
    '            s &= ", " & String.Format("{0}", b)
    '            If (b And &HE0) <> 0 Then
    '                'seq_instr
    '                s &= ", " & String.Format("{0:X2}, ", WorkSeq.Seq(seq_nr, i))
    '                i += 1
    '            End If
    '            Me.RichTextBox1.AppendText(s & Chr(13))
    '            cr_f = True
    '    End Select

    'Loop

    '    End Sub


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
        Dim se As ym.Sequence._SEQENTRY
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
                            s = NoteTable(d)
                        Else
                            s = String.Format(" {0:X2}", d)
                        End If
                End Select

                'Shape
                d = se.shape
                s &= "  " & String.Format("{0}", d)

                If (d And &HE0) <> 0 Then
                    'seq_instr
                    s &= "  " & String.Format("{0:X2}", se.xtra)
                End If

                myGfx.DrawString(s, fnt, brush, ColumnPos(i + 1), y)
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

    Private Sub frmSeqEdit_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        AddHandler cbSeq1.SelectedIndexChanged, AddressOf cbSeq1_SelectedIndexChanged
        AddHandler cbSeq2.SelectedIndexChanged, AddressOf cbSeq1_SelectedIndexChanged
        AddHandler cbSeq3.SelectedIndexChanged, AddressOf cbSeq1_SelectedIndexChanged
    End Sub
End Class
