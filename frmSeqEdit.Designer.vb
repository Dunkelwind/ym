<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmSeqEdit
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
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
End Class

