Imports System.Drawing.Imaging.PixelFormat
Imports System.Runtime.InteropServices
Imports ym.Interpreter

' bITmASTER was here.....
' all dies Zeug ist (c)oderight by bITmASTER


Public Class frmTrackEdit
    Inherits System.Windows.Forms.Form

    Private WorkTrack As clTracks
    Private f As New Font("Courier New", 10)
    Private f2 As New Font("Courier New", 10, FontStyle.Bold)

    Private stringformat As New stringformat

    Private fontWidth1 As Integer
    Private fontWidth3 As Integer

    Private ColumnPos(3) As Short
    Private WorkLine As Short
    Private Const VisibleLines As Integer = 7

    Private bmp As Bitmap
    Private myGfx As Graphics
    Private mySize As Size
    Private myPen = New Pen(Color.Black)

    Private editFormat As String = "00_111_22_33"



    Enum ValueType
        dec = 0
        hex
    End Enum

    Private caretX, caretY, caretC, caretEx As Integer

    Class caret
        Structure _Point
            Dim x As Integer
            Dim y As Integer
        End Structure

        Public Declare Function GetCaretBlinkTime Lib "user32.dll" () As Integer
        Public Declare Function SetCaretBlinkTime Lib "user32.dll" (ByVal wMSeconds As Integer)
        Public Declare Function GetCaretPos Lib "user32.dll" (ByRef pnt As _Point)
        Public Declare Function SetCaretPos Lib "user32.dll" (ByVal x As Integer, ByVal y As Integer) As Integer
        Public Declare Function DestroyCaret Lib "user32.dll" () As Integer
        Public Declare Function CreateCaret Lib "user32.dll" (ByVal hwnd As IntPtr, ByVal hBitmap As IntPtr, ByVal w As Integer, ByVal h As Integer) As Integer
        Public Declare Function ShowCaret Lib "user32.dll" (ByVal hwnd As IntPtr) As Integer
        Public Declare Function HideCaret Lib "user32.dll" (ByVal hwnd As IntPtr) As Integer
    End Class


#Region " Vom Windows Form Designer generierter Code "

    Public Sub New()
        MyBase.New()

        ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
        InitializeComponent()
        ' Initialisierungen nach dem Aufruf InitializeComponent() hinzufügen

        mynew()
    End Sub

    ' Die Form überschreibt den Löschvorgang der Basisklasse, um Komponenten zu bereinigen.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Application.RemoveMessageFilter(Me)
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
    Friend WithEvents VScrollBar1 As System.Windows.Forms.VScrollBar
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkFollow As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdSingle As System.Windows.Forms.Button
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents mnLoadTrack As System.Windows.Forms.MenuItem
    Friend WithEvents mnSaveTrack As System.Windows.Forms.MenuItem
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.VScrollBar1 = New System.Windows.Forms.VScrollBar()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmdSingle = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkFollow = New System.Windows.Forms.CheckBox()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu()
        Me.mnLoadTrack = New System.Windows.Forms.MenuItem()
        Me.mnSaveTrack = New System.Windows.Forms.MenuItem()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'VScrollBar1
        '
        Me.VScrollBar1.Cursor = System.Windows.Forms.Cursors.Default
        Me.VScrollBar1.Dock = System.Windows.Forms.DockStyle.Right
        Me.VScrollBar1.Location = New System.Drawing.Point(530, 0)
        Me.VScrollBar1.Name = "VScrollBar1"
        Me.VScrollBar1.Size = New System.Drawing.Size(16, 293)
        Me.VScrollBar1.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.Controls.Add(Me.cmdSingle)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.chkFollow)
        Me.Panel1.Location = New System.Drawing.Point(0, 248)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(552, 44)
        Me.Panel1.TabIndex = 1
        '
        'cmdSingle
        '
        Me.cmdSingle.Location = New System.Drawing.Point(64, 8)
        Me.cmdSingle.Name = "cmdSingle"
        Me.cmdSingle.Size = New System.Drawing.Size(72, 24)
        Me.cmdSingle.TabIndex = 2
        Me.cmdSingle.Text = "SingleStep"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(24, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "follow"
        '
        'chkFollow
        '
        Me.chkFollow.Checked = True
        Me.chkFollow.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkFollow.Location = New System.Drawing.Point(8, 8)
        Me.chkFollow.Name = "chkFollow"
        Me.chkFollow.Size = New System.Drawing.Size(16, 16)
        Me.chkFollow.TabIndex = 0
        Me.chkFollow.Text = "CheckBox1"
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.Filter = "YM Track List|*.ymt"
        Me.SaveFileDialog1.Title = "Save Track List"
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnLoadTrack, Me.mnSaveTrack})
        '
        'mnLoadTrack
        '
        Me.mnLoadTrack.Index = 0
        Me.mnLoadTrack.Text = "Load Track"
        '
        'mnSaveTrack
        '
        Me.mnSaveTrack.Index = 1
        Me.mnSaveTrack.Text = "Save Track"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Filter = "YM Track List|*.ymt"
        Me.OpenFileDialog1.Title = "Open Track List"
        '
        'frmTrackEdit
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(546, 293)
        Me.ContextMenu = Me.ContextMenu1
        Me.Controls.Add(Me.VScrollBar1)
        Me.Controls.Add(Me.Panel1)
        Me.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmTrackEdit"
        Me.Text = "frmTrackEdit"
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
        '       Panel1.Location = New Point(0, mySize.Height - Panel1.Height)
        bmp = New Bitmap(mySize.Width, mySize.Height, Format16bppRgb565)
        myGfx = Graphics.FromImage(bmp)
        Dim fSize As New SizeF    '

        stringformat = stringformat.GenericTypographic

        fSize = myGfx.MeasureString("0000000000", f, -1, stringformat)
        fontWidth1 = Int(fSize.Width / 10)
        fSize = myGfx.MeasureString("000", f, -1, stringformat)
        fontWidth3 = Int(fSize.Width)


        caretC = 0
        caretEx = 0
        caretX = ColumnPos(1)
        caretY = VisibleLines * f.Height



        '        Application.AddMessageFilter(Me)

    End Sub

    Public Sub SetWorkTrack(ByRef wTrack As clTracks)
        WorkTrack = wTrack
        Me.VScrollBar1.SmallChange = 1
        Me.VScrollBar1.LargeChange = 10
        Me.VScrollBar1.Maximum = WorkTrack.MaxRow + Me.VScrollBar1.LargeChange
        Me.Refresh()

    End Sub

    Public Sub follow(ByVal i As Integer)
        If chkFollow.Checked = True And Me.WindowState = FormWindowState.Normal Then
            Me.VScrollBar1.Value = i
        End If
    End Sub

    Private Sub frmTrackEdit_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        ' only hide window
        e.Cancel = True
        Me.Hide()
    End Sub


    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        DrawTrack(e.Graphics)
    End Sub

    Private Sub DrawTrack(ByRef gfx As Graphics)
        Dim i, min, max, k, x, y As Short
        Dim fnt As Font
        Dim s As String
        Dim b As Byte
        Dim brush As Brush

        myGfx.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height)    'alles löschen

        min = Me.VScrollBar1.Value - VisibleLines
        y = 0
        If min < 0 Then
            y = -min * f.Height
            min = 0
        End If

        max = Me.VScrollBar1.Value + VisibleLines
        If max > WorkTrack.MaxRow Then
            max = WorkTrack.MaxRow
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
            myGfx.DrawString(String.Format("{0,3}", k), fnt, Brushes.Green, ColumnPos(0), y, stringformat)

            ' 3 Tracks
            For i = 0 To 2
                s = String.Format("{0,2} ", WorkTrack.GetSeq(i, k))
                s &= String.Format("{0,3} ", WorkTrack.GetNote(i, k))
                s &= String.Format("{0:X2} ", WorkTrack.GetInstr(i, k))
                s &= String.Format("{0:X2} ", WorkTrack.GetCmd(i, k))

                myGfx.DrawString(s, fnt, brush, ColumnPos(i + 1), y, stringformat)
            Next i
            y += f.Height
        Next k
        gfx.DrawImage(bmp, 0, 0)
    End Sub

    Private Sub setCursor(ByVal key As Integer)
        Dim x As Short

        Select Case key
            Case 37 'left
                caretEx -= 1
                If caretEx < 0 Then
                    If caretC > 0 Then
                        caretC -= 1
                        caretEx = editFormat.Length - 1
                    Else
                        caretEx = 0
                    End If
                End If
            Case 39 'right
                caretEx += 1
                If caretEx >= editFormat.Length Then
                    If caretC < 2 Then
                        caretC += 1
                        caretEx = 0
                    Else
                        caretEx = editFormat.Length - 1
                    End If
                End If
        End Select

        Dim fSize As New SizeF    '
        fSize = myGfx.MeasureString(editFormat.Substring(0, caretEx), f, 100, stringformat)

        '        Debug.WriteLine(editFormat.Substring(0, caretEx) & " W:" & fSize.Width)

        x = ColumnPos(caretC + 1) + fSize.Width
        caretX = x

        caret.SetCaretPos(caretX, caretY)

    End Sub


    Private Function editTrackEntry(ByVal key As Char) As Boolean
        Dim x, y As Short
        Dim s() As Char
        Dim s1 As String
        Dim b As Byte
        Dim v As Integer
        Dim flag As Boolean
        Dim k As Integer
        Dim gfx As Graphics

        If editFormat.Substring(caretEx, 1) = "_" Then
            setCursor(39)
            Return False
        End If

        k = Me.VScrollBar1.Value

        ReDim s(editFormat.Length - 1)
        s = String.Format("{0:X2} ", WorkTrack.GetSeq(caretC, k))
        s &= String.Format("{0,3} ", WorkTrack.GetNote(caretC, k))
        s &= String.Format("{0:X2} ", WorkTrack.GetInstr(caretC, k))
        s &= String.Format("{0:X2} ", WorkTrack.GetCmd(caretC, k))

        s(caretEx) = key

        s1 = s
        flag = False
        Select Case editFormat.Substring(caretEx, 1)
            Case "0"
                flag = toDec(s1.Substring(0, 2), b)
                If flag = True Then
                    WorkTrack.SetSeq(caretC, k, b)
                End If
            Case "1"
                flag = toDec(s1.Substring(3, 3), v)
                If flag = True Then
                    WorkTrack.SetNote(caretC, k, v)
                End If
            Case "2"
                flag = toHex(s1.Substring(7, 2), b)
                If flag = True Then
                    WorkTrack.SetInstr(caretC, k, b)
                End If
            Case "3"
                flag = toHex(s1.Substring(10, 2), b)
                If flag = True Then
                    WorkTrack.SetCmd(caretC, k, b)
                End If
        End Select

        If flag = True Then
            y = VisibleLines * f.Height
            gfx = Me.CreateGraphics

            caret.HideCaret(Me.Handle)

            gfx.FillRectangle(Brushes.White, ColumnPos(caretC + 1), y, ColumnPos(2) - ColumnPos(1), f.Height)   'alles löschen
            gfx.DrawString(s, f2, Brushes.Black, ColumnPos(caretC + 1), y)

            caret.ShowCaret(Me.Handle)
            setCursor(39)
        End If
        Return flag
    End Function

    Private Function toHex(ByVal s As String, ByRef v As Byte) As Boolean
        toHex = True
        s = "&H" & s
        Try
            v = CByte(s)
        Catch ex As Exception
            toHex = False
        End Try
    End Function
    Private Function toDec(ByVal s As String, ByRef v As Integer) As Boolean
        toDec = True
        Try
            v = CInt(s)
            If v > 127 Or v < -128 Then
                toDec = False
            End If
        Catch ex As Exception
            toDec = False
        End Try
    End Function

    Private Sub VScrollBar1_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles VScrollBar1.ValueChanged
        DrawTrack(Me.CreateGraphics())
        '        Console.WriteLine(Me.VScrollBar1.Value)
    End Sub


    Private Sub frmTrackEdit_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        caret.CreateCaret(Me.Handle, IntPtr.Zero, fontWidth1, f.Height)
        caret.ShowCaret(Me.Handle)
        caret.SetCaretPos(caretX, caretY)
    End Sub

    Private Sub frmTrackEdit_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Deactivate
        caret.DestroyCaret()
    End Sub

    Private Sub frmTrackEdit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        If editTrackEntry(e.KeyChar) = True Then
            e.Handled = True
        End If

    End Sub

    Public Function handleKey(ByVal key As Integer) As Boolean
        Select Case key
            Case 37, 39 'left,right
                setCursor(key)
                Return True
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

    Private Sub cmdSingle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSingle.Click
        Dim si As _SND_INFO

        si.start = Me.VScrollBar1.Value
        si.last = si.start
        si.speed = 4
        Form1.interpreter.init_voices(si, Interpreter.Mode.PlayTrackOnce)
        Form1.WaveGen.PlayFlag = True

    End Sub

    Private Sub mnSaveTrack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnSaveTrack.Click
        Dim path As String

        path = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
        Me.SaveFileDialog1.InitialDirectory = path

        If Me.SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            '       WorkTrack.XmlSave(Me.SaveFileDialog1.FileName)
        End If


    End Sub


    Private Sub mnLoadTrack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnLoadTrack.Click
        Dim path As String

        path = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
        Me.OpenFileDialog1.InitialDirectory = path

        If Me.OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            Form1.WaveGen.PlayFlag = False
            '    WorkTrack.XmlLoad(Me.OpenFileDialog1.FileName)
        End If

    End Sub

End Class
