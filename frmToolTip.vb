Public Class frmToolTip
    Inherits System.Windows.Forms.Form

    Private form As form
    Private mouseHeight As Integer
    Private state = 0


#Region " Vom Windows Form Designer generierter Code "

    Public Sub New(ByVal f As Form)
        MyBase.New()

        ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
        InitializeComponent()
        mynew(f)
        ' Initialisierungen nach dem Aufruf InitializeComponent() hinzufügen

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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.Label1 = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(152, 88)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Label1"
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'frmToolTip
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(152, 88)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label1)
        Me.Enabled = False
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.Name = "frmToolTip"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "frmToolTip"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub

#End Region


    Sub mynew(ByVal f As Form)
        form = f
        mouseHeight = form.Cursor.Size.Height
        Me.TabIndex = 0
        Timer1.Enabled = True
        Timer1.Start()

    End Sub

    Sub showTip()
        Me.Location = New Point(form.MousePosition.X, form.MousePosition.Y + mouseHeight)
        Me.Show()
    End Sub


    Sub retrig()
        If state = 3 Then
            Timer1.Start()
        state = 0
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Select Case state
            Case 0
                If form.Visible = False Then
                    Timer1.Start()
                Else
                    Me.Location = New Point(form.MousePosition.X, form.MousePosition.Y + mouseHeight)
                    Me.Show()

                    form.Focus()
                    Timer1.Start()
                    state = 1
                End If
            Case 1
                Me.Hide()
                Timer1.Start()
                state = 2
            Case 2
                Timer1.Stop()
                state = 3
        End Select
    End Sub


End Class
