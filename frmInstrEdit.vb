Imports System.Windows.Forms.View

Public Class frmInstrEdit
    Inherits System.Windows.Forms.Form

    Dim WorkInstr As Instr

    WithEvents dtInstr As New DataTable("tblInstr")
    Dim dsInstr As New DataSet("DataSetInstr")
    Dim dgts As New DataGridTableStyle()

    Structure _cmd
        Dim name As String
        Dim n As Short
        Dim x As Boolean
        Public Sub New(ByVal name As String, ByVal n As Short, ByVal x As Boolean)
            Me.name = name
            Me.n = n
            Me.x = x
        End Sub
    End Structure

    Dim cmd() As _cmd = {New _cmd("Jmp", 1, True), _
                         New _cmd("End", 0, True), _
                         New _cmd("ResetShape", 0, False), _
                         New _cmd("FreqMod", 2, False), _
                         New _cmd("TuneNoise", 1, False), _
                         New _cmd("Noise", 0, False), _
                         New _cmd("Tune", 0, False), _
                         New _cmd("InstrGroup", 1, False), _
                         New _cmd("Speed", 1, False), _
                         New _cmd("Triangle", 1, False), _
                         New _cmd("Unknown0xa", 1, False), _
                         New _cmd("ShapeVibrato", 1, False), _
                         New _cmd("Unknown0xc", 1, False), _
                         New _cmd("Unknown0xd", 1, False), _
                         New _cmd("SidFreq", 1, False), _
                         New _cmd("Buzzer", 1, False) _
                        }

#Region " Vom Windows Form Designer generierter Code "

    Public Sub New()
        MyBase.New()

        ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
        InitializeComponent()

        ' Initialisierungen nach dem Aufruf InitializeComponent() hinzufügen

        dsInstr.Tables.Add(dtInstr)

        dtInstr.Columns.Add("Tick", System.Type.GetType("System.Int16"))
        dtInstr.Columns(0).ReadOnly = True

        dtInstr.Columns.Add("Code")

        dgInstr.SetDataBinding(dsInstr, "tblInstr")
        dgInstr.RowHeadersVisible = True

        dgts.MappingName = "tblInstr"
        dgInstr.TableStyles.Add(dgts)

        dgInstr.TableStyles("tblInstr").GridColumnStyles(0).Width = 30
        dgInstr.TableStyles("tblInstr").GridColumnStyles(1).Width = 40


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
    Friend WithEvents cmbInstrName As System.Windows.Forms.ComboBox
    Friend WithEvents dgInstr As System.Windows.Forms.DataGrid
    Friend WithEvents rtbInstr As System.Windows.Forms.RichTextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmbInstrName = New System.Windows.Forms.ComboBox()
        Me.dgInstr = New System.Windows.Forms.DataGrid()
        Me.rtbInstr = New System.Windows.Forms.RichTextBox()
        CType(Me.dgInstr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmbInstrName
        '
        Me.cmbInstrName.Location = New System.Drawing.Point(32, 8)
        Me.cmbInstrName.Name = "cmbInstrName"
        Me.cmbInstrName.Size = New System.Drawing.Size(216, 21)
        Me.cmbInstrName.TabIndex = 0
        Me.cmbInstrName.Text = "ComboBox1"
        '
        'dgInstr
        '
        Me.dgInstr.CaptionVisible = False
        Me.dgInstr.DataMember = ""
        Me.dgInstr.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgInstr.Location = New System.Drawing.Point(32, 48)
        Me.dgInstr.Name = "dgInstr"
        Me.dgInstr.Size = New System.Drawing.Size(216, 200)
        Me.dgInstr.TabIndex = 1
        '
        'rtbInstr
        '
        Me.rtbInstr.Location = New System.Drawing.Point(296, 48)
        Me.rtbInstr.Name = "rtbInstr"
        Me.rtbInstr.Size = New System.Drawing.Size(216, 200)
        Me.rtbInstr.TabIndex = 2
        Me.rtbInstr.Text = "RichTextBox1"
        '
        'frmInstrEdit
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(552, 271)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.rtbInstr, Me.dgInstr, Me.cmbInstrName})
        Me.Name = "frmInstrEdit"
        Me.Text = "frmInstrEdit"
        CType(Me.dgInstr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmInstrEdit_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        e.Cancel = True
        Me.Hide()
    End Sub

    Sub SetWorkInstr(ByRef wi As Instr)
        Dim i As Short

        WorkInstr = wi
        For i = 0 To wi.n
            Me.cmbInstrName.Items.Add("Instr" & String.Format("{0,3}", i))
        Next
        If Me.cmbInstrName.SelectedIndex = -1 Then
            Me.cmbInstrName.SelectedItem = 0
        End If
        Me.cmbInstrName.Text = Me.cmbInstrName.Items(0)
    End Sub

    Sub View()
        Dim instr_nr As Short
        Dim b As Byte
        Dim i, j, k, nr As Short
        Dim duration As Short = -1
        Dim myRow As DataRow
        Dim s As String

        RemoveHandler dtInstr.ColumnChanging, New DataColumnChangeEventHandler(AddressOf OnColumnChanging)

        dtInstr.Clear()

        instr_nr = Me.cmbInstrName.SelectedIndex
        i = 0
        nr = 0

        Do
            b = WorkInstr.instr(instr_nr)(i)
            i += 1
            myRow = dtInstr.NewRow()
            myRow.Item(0) = nr
            myRow.Item(1) = String.Format("{0:X2}", b)
            nr += 1
            dtInstr.Rows.Add(myRow)
            If b = &HE1 Or b = &HE0 Then
                Exit Do
            End If
        Loop
        AddHandler dtInstr.ColumnChanging, New DataColumnChangeEventHandler(AddressOf OnColumnChanging)

        rtbInstr.Clear()
        instr_nr = Me.cmbInstrName.SelectedIndex
        i = 0
        nr = 0
        s = ""
        Do

            b = WorkInstr.instr(instr_nr)(i)
            i += 1


            If b >= &HE0 Then
                b -= &HE0
                s &= cmd(b).name
                j = cmd(b).n
                If j > 0 Then
                    s &= "("
                    For k = 1 To j
                        s &= String.Format("{0},", WorkInstr.instr(instr_nr)(i))
                        i += 1
                    Next
                    s &= ")"
                End If
                If cmd(b).x Then
                    rtbInstr.AppendText(s & Chr(13))
                    Exit Do
                End If
                s &= " "
            Else
                s &= String.Format("{0},", b) & Chr(13)
                rtbInstr.AppendText(s)
                s = ""
            End If
        Loop


    End Sub

    Private Sub cmbInstrName_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbInstrName.SelectedIndexChanged
        View()
    End Sub



    Private Sub OnColumnChanging(ByVal sender As Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles dtInstr.ColumnChanging
        Dim b As Byte
        Dim instr_nr As Short

        instr_nr = Me.cmbInstrName.SelectedIndex

        Try
            b = CShort("&h" & e.ProposedValue)
        Catch ex As Exception
            b = WorkInstr.instr(instr_nr)(e.Row(0))
        End Try

        WorkInstr.instr(instr_nr)(e.Row(0)) = b
        e.ProposedValue = String.Format("{0:X2}", b)
        Debug.WriteLine(b)
        Debug.WriteLine(e.Row(0))
        Debug.WriteLine(dgInstr.CurrentCell.RowNumber)

        '        Debug.WriteLine(e.Row(0, DataRowVersion.Original))
    End Sub
End Class
