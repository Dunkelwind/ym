Imports ym.Interpreter

Public Class frmShapeEdit
    Inherits System.Windows.Forms.Form

    Private Const START_X = 4, START_Y = 4
    Private p1 As Point
    Private w, h As Short
    Private ystep As Short
    Private WorkShapes As Shape
    Friend WithEvents btnHAs As System.Windows.Forms.Button
    Friend WithEvents btnHGs As System.Windows.Forms.Button
    Friend WithEvents btnHFs As System.Windows.Forms.Button
    Friend WithEvents btnHDs As System.Windows.Forms.Button
    Friend WithEvents btnHCs As System.Windows.Forms.Button
    Friend WithEvents btnHB As System.Windows.Forms.Button
    Friend WithEvents btnHA As System.Windows.Forms.Button
    Friend WithEvents btnHG As System.Windows.Forms.Button
    Friend WithEvents btnHF As System.Windows.Forms.Button
    Friend WithEvents btnHE As System.Windows.Forms.Button
    Friend WithEvents btnHD As System.Windows.Forms.Button
    Friend WithEvents btnHC As System.Windows.Forms.Button
    Friend WithEvents btnMAs As System.Windows.Forms.Button
    Friend WithEvents btnMGs As System.Windows.Forms.Button
    Friend WithEvents btnMFs As System.Windows.Forms.Button
    Friend WithEvents btnMDs As System.Windows.Forms.Button
    Friend WithEvents btnMCs As System.Windows.Forms.Button
    Friend WithEvents btnMB As System.Windows.Forms.Button
    Friend WithEvents btnMA As System.Windows.Forms.Button
    Friend WithEvents btnMG As System.Windows.Forms.Button
    Friend WithEvents btnMF As System.Windows.Forms.Button
    Friend WithEvents btnME As System.Windows.Forms.Button
    Friend WithEvents btnMD As System.Windows.Forms.Button
    Friend WithEvents btnMC As System.Windows.Forms.Button
    Friend WithEvents btnLAs As System.Windows.Forms.Button
    Friend WithEvents btnLGs As System.Windows.Forms.Button
    Friend WithEvents btnLFs As System.Windows.Forms.Button
    Friend WithEvents btnLDs As System.Windows.Forms.Button
    Friend WithEvents btnLCs As System.Windows.Forms.Button
    Friend WithEvents btnLB As System.Windows.Forms.Button
    Friend WithEvents btnLA As System.Windows.Forms.Button
    Friend WithEvents btnLG As System.Windows.Forms.Button
    Friend WithEvents btnLF As System.Windows.Forms.Button
    Friend WithEvents btnLE As System.Windows.Forms.Button
    Friend WithEvents btnLD As System.Windows.Forms.Button
    Friend WithEvents btnLC As System.Windows.Forms.Button
    Private WorkSeq As Sequence

#Region " Vom Windows Form Designer generierter Code "

    Public Sub New()
        MyBase.New()


        ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
        InitializeComponent()

        ' Initialisierungen nach dem Aufruf InitializeComponent() hinzufügen

        ' Edit-Bereich festlegen

        p1 = Me.fraShape.Location
        p1.X += Me.fraShape.DisplayRectangle.X + START_X
        p1.Y += Me.fraShape.DisplayRectangle.Y + START_Y
        w = Me.fraShape.DisplayRectangle.Width - 2 * START_X
        h = Me.fraShape.DisplayRectangle.Height - 2 * START_Y
        ystep = h / 16

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
    Friend WithEvents HScrollBar1 As System.Windows.Forms.HScrollBar
    Friend WithEvents cmbShapeName As System.Windows.Forms.ComboBox
    Friend WithEvents fraShape As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtPara1 As System.Windows.Forms.TextBox
    Friend WithEvents txtPara2 As System.Windows.Forms.TextBox
    Friend WithEvents txtPara3 As System.Windows.Forms.TextBox
    Friend WithEvents txtPara4 As System.Windows.Forms.TextBox
    Friend WithEvents txtPara5 As System.Windows.Forms.TextBox
    Friend WithEvents nUpDownShapeNr As System.Windows.Forms.NumericUpDown
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.HScrollBar1 = New System.Windows.Forms.HScrollBar()
        Me.fraShape = New System.Windows.Forms.GroupBox()
        Me.cmbShapeName = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtPara1 = New System.Windows.Forms.TextBox()
        Me.txtPara2 = New System.Windows.Forms.TextBox()
        Me.txtPara3 = New System.Windows.Forms.TextBox()
        Me.txtPara4 = New System.Windows.Forms.TextBox()
        Me.txtPara5 = New System.Windows.Forms.TextBox()
        Me.nUpDownShapeNr = New System.Windows.Forms.NumericUpDown()
        Me.btnHAs = New System.Windows.Forms.Button()
        Me.btnHGs = New System.Windows.Forms.Button()
        Me.btnHFs = New System.Windows.Forms.Button()
        Me.btnHDs = New System.Windows.Forms.Button()
        Me.btnHCs = New System.Windows.Forms.Button()
        Me.btnHB = New System.Windows.Forms.Button()
        Me.btnHA = New System.Windows.Forms.Button()
        Me.btnHG = New System.Windows.Forms.Button()
        Me.btnHF = New System.Windows.Forms.Button()
        Me.btnHE = New System.Windows.Forms.Button()
        Me.btnHD = New System.Windows.Forms.Button()
        Me.btnHC = New System.Windows.Forms.Button()
        Me.btnMAs = New System.Windows.Forms.Button()
        Me.btnMGs = New System.Windows.Forms.Button()
        Me.btnMFs = New System.Windows.Forms.Button()
        Me.btnMDs = New System.Windows.Forms.Button()
        Me.btnMCs = New System.Windows.Forms.Button()
        Me.btnMB = New System.Windows.Forms.Button()
        Me.btnMA = New System.Windows.Forms.Button()
        Me.btnMG = New System.Windows.Forms.Button()
        Me.btnMF = New System.Windows.Forms.Button()
        Me.btnME = New System.Windows.Forms.Button()
        Me.btnMD = New System.Windows.Forms.Button()
        Me.btnMC = New System.Windows.Forms.Button()
        Me.btnLAs = New System.Windows.Forms.Button()
        Me.btnLGs = New System.Windows.Forms.Button()
        Me.btnLFs = New System.Windows.Forms.Button()
        Me.btnLDs = New System.Windows.Forms.Button()
        Me.btnLCs = New System.Windows.Forms.Button()
        Me.btnLB = New System.Windows.Forms.Button()
        Me.btnLA = New System.Windows.Forms.Button()
        Me.btnLG = New System.Windows.Forms.Button()
        Me.btnLF = New System.Windows.Forms.Button()
        Me.btnLE = New System.Windows.Forms.Button()
        Me.btnLD = New System.Windows.Forms.Button()
        Me.btnLC = New System.Windows.Forms.Button()
        CType(Me.nUpDownShapeNr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'HScrollBar1
        '
        Me.HScrollBar1.Cursor = System.Windows.Forms.Cursors.Default
        Me.HScrollBar1.Location = New System.Drawing.Point(351, 232)
        Me.HScrollBar1.Name = "HScrollBar1"
        Me.HScrollBar1.Size = New System.Drawing.Size(296, 16)
        Me.HScrollBar1.TabIndex = 0
        '
        'fraShape
        '
        Me.fraShape.BackColor = System.Drawing.Color.Transparent
        Me.fraShape.Cursor = System.Windows.Forms.Cursors.Cross
        Me.fraShape.Location = New System.Drawing.Point(351, 8)
        Me.fraShape.Name = "fraShape"
        Me.fraShape.Size = New System.Drawing.Size(296, 224)
        Me.fraShape.TabIndex = 1
        Me.fraShape.TabStop = False
        Me.fraShape.Text = "Shape Edit"
        '
        'cmbShapeName
        '
        Me.cmbShapeName.Location = New System.Drawing.Point(8, 8)
        Me.cmbShapeName.Name = "cmbShapeName"
        Me.cmbShapeName.Size = New System.Drawing.Size(224, 21)
        Me.cmbShapeName.TabIndex = 2
        Me.cmbShapeName.Text = "ComboBox1"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(16, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 16)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Speed:"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(16, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 16)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "instr:"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(16, 96)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 16)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "delta f:"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(16, 120)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 16)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "mod f:"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(16, 144)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 16)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "tfmx count:"
        '
        'txtPara1
        '
        Me.txtPara1.Location = New System.Drawing.Point(88, 48)
        Me.txtPara1.Name = "txtPara1"
        Me.txtPara1.Size = New System.Drawing.Size(64, 20)
        Me.txtPara1.TabIndex = 8
        Me.txtPara1.Text = "TextBox1"
        '
        'txtPara2
        '
        Me.txtPara2.Location = New System.Drawing.Point(88, 72)
        Me.txtPara2.Name = "txtPara2"
        Me.txtPara2.Size = New System.Drawing.Size(64, 20)
        Me.txtPara2.TabIndex = 9
        Me.txtPara2.Text = "TextBox1"
        '
        'txtPara3
        '
        Me.txtPara3.Location = New System.Drawing.Point(88, 96)
        Me.txtPara3.Name = "txtPara3"
        Me.txtPara3.Size = New System.Drawing.Size(64, 20)
        Me.txtPara3.TabIndex = 10
        Me.txtPara3.Text = "TextBox1"
        '
        'txtPara4
        '
        Me.txtPara4.Location = New System.Drawing.Point(88, 120)
        Me.txtPara4.Name = "txtPara4"
        Me.txtPara4.Size = New System.Drawing.Size(64, 20)
        Me.txtPara4.TabIndex = 11
        Me.txtPara4.Text = "TextBox1"
        '
        'txtPara5
        '
        Me.txtPara5.Location = New System.Drawing.Point(88, 144)
        Me.txtPara5.Name = "txtPara5"
        Me.txtPara5.Size = New System.Drawing.Size(64, 20)
        Me.txtPara5.TabIndex = 12
        Me.txtPara5.Text = "TextBox1"
        '
        'nUpDownShapeNr
        '
        Me.nUpDownShapeNr.Location = New System.Drawing.Point(240, 8)
        Me.nUpDownShapeNr.Maximum = New Decimal(New Integer() {63, 0, 0, 0})
        Me.nUpDownShapeNr.Name = "nUpDownShapeNr"
        Me.nUpDownShapeNr.Size = New System.Drawing.Size(40, 20)
        Me.nUpDownShapeNr.TabIndex = 14
        '
        'btnHAs
        '
        Me.btnHAs.BackColor = System.Drawing.Color.Black
        Me.btnHAs.Location = New System.Drawing.Point(724, 261)
        Me.btnHAs.Name = "btnHAs"
        Me.btnHAs.Size = New System.Drawing.Size(29, 101)
        Me.btnHAs.TabIndex = 71
        Me.btnHAs.Tag = "35"
        Me.btnHAs.UseVisualStyleBackColor = False
        '
        'btnHGs
        '
        Me.btnHGs.BackColor = System.Drawing.Color.Black
        Me.btnHGs.Location = New System.Drawing.Point(686, 261)
        Me.btnHGs.Name = "btnHGs"
        Me.btnHGs.Size = New System.Drawing.Size(29, 101)
        Me.btnHGs.TabIndex = 70
        Me.btnHGs.Tag = "33"
        Me.btnHGs.UseVisualStyleBackColor = False
        '
        'btnHFs
        '
        Me.btnHFs.BackColor = System.Drawing.Color.Black
        Me.btnHFs.Location = New System.Drawing.Point(651, 261)
        Me.btnHFs.Name = "btnHFs"
        Me.btnHFs.Size = New System.Drawing.Size(29, 101)
        Me.btnHFs.TabIndex = 69
        Me.btnHFs.Tag = "31"
        Me.btnHFs.UseVisualStyleBackColor = False
        '
        'btnHDs
        '
        Me.btnHDs.BackColor = System.Drawing.Color.Black
        Me.btnHDs.Location = New System.Drawing.Point(577, 261)
        Me.btnHDs.Name = "btnHDs"
        Me.btnHDs.Size = New System.Drawing.Size(29, 101)
        Me.btnHDs.TabIndex = 68
        Me.btnHDs.Tag = "28"
        Me.btnHDs.UseVisualStyleBackColor = False
        '
        'btnHCs
        '
        Me.btnHCs.BackColor = System.Drawing.Color.Black
        Me.btnHCs.Location = New System.Drawing.Point(542, 261)
        Me.btnHCs.Name = "btnHCs"
        Me.btnHCs.Size = New System.Drawing.Size(29, 101)
        Me.btnHCs.TabIndex = 67
        Me.btnHCs.Tag = "26"
        Me.btnHCs.UseVisualStyleBackColor = False
        '
        'btnHB
        '
        Me.btnHB.BackColor = System.Drawing.Color.White
        Me.btnHB.Location = New System.Drawing.Point(738, 261)
        Me.btnHB.Name = "btnHB"
        Me.btnHB.Size = New System.Drawing.Size(39, 167)
        Me.btnHB.TabIndex = 66
        Me.btnHB.Tag = "36"
        Me.btnHB.UseVisualStyleBackColor = False
        '
        'btnHA
        '
        Me.btnHA.BackColor = System.Drawing.Color.White
        Me.btnHA.Location = New System.Drawing.Point(701, 261)
        Me.btnHA.Name = "btnHA"
        Me.btnHA.Size = New System.Drawing.Size(39, 167)
        Me.btnHA.TabIndex = 65
        Me.btnHA.Tag = "34"
        Me.btnHA.UseVisualStyleBackColor = False
        '
        'btnHG
        '
        Me.btnHG.BackColor = System.Drawing.Color.White
        Me.btnHG.Location = New System.Drawing.Point(664, 261)
        Me.btnHG.Name = "btnHG"
        Me.btnHG.Size = New System.Drawing.Size(39, 167)
        Me.btnHG.TabIndex = 64
        Me.btnHG.Tag = "32"
        Me.btnHG.UseVisualStyleBackColor = False
        '
        'btnHF
        '
        Me.btnHF.BackColor = System.Drawing.Color.White
        Me.btnHF.Location = New System.Drawing.Point(627, 261)
        Me.btnHF.Name = "btnHF"
        Me.btnHF.Size = New System.Drawing.Size(39, 167)
        Me.btnHF.TabIndex = 63
        Me.btnHF.Tag = "30"
        Me.btnHF.UseVisualStyleBackColor = False
        '
        'btnHE
        '
        Me.btnHE.BackColor = System.Drawing.Color.White
        Me.btnHE.Location = New System.Drawing.Point(591, 261)
        Me.btnHE.Name = "btnHE"
        Me.btnHE.Size = New System.Drawing.Size(39, 167)
        Me.btnHE.TabIndex = 62
        Me.btnHE.Tag = "29"
        Me.btnHE.UseVisualStyleBackColor = False
        '
        'btnHD
        '
        Me.btnHD.BackColor = System.Drawing.Color.White
        Me.btnHD.Location = New System.Drawing.Point(555, 261)
        Me.btnHD.Name = "btnHD"
        Me.btnHD.Size = New System.Drawing.Size(39, 167)
        Me.btnHD.TabIndex = 61
        Me.btnHD.Tag = "27"
        Me.btnHD.UseVisualStyleBackColor = False
        '
        'btnHC
        '
        Me.btnHC.BackColor = System.Drawing.Color.White
        Me.btnHC.Location = New System.Drawing.Point(518, 261)
        Me.btnHC.Name = "btnHC"
        Me.btnHC.Size = New System.Drawing.Size(39, 167)
        Me.btnHC.TabIndex = 60
        Me.btnHC.Tag = "25"
        Me.btnHC.UseVisualStyleBackColor = False
        '
        'btnMAs
        '
        Me.btnMAs.BackColor = System.Drawing.Color.Black
        Me.btnMAs.Location = New System.Drawing.Point(468, 261)
        Me.btnMAs.Name = "btnMAs"
        Me.btnMAs.Size = New System.Drawing.Size(29, 101)
        Me.btnMAs.TabIndex = 59
        Me.btnMAs.Tag = "23"
        Me.btnMAs.UseVisualStyleBackColor = False
        '
        'btnMGs
        '
        Me.btnMGs.BackColor = System.Drawing.Color.Black
        Me.btnMGs.Location = New System.Drawing.Point(430, 261)
        Me.btnMGs.Name = "btnMGs"
        Me.btnMGs.Size = New System.Drawing.Size(29, 101)
        Me.btnMGs.TabIndex = 58
        Me.btnMGs.Tag = "21"
        Me.btnMGs.UseVisualStyleBackColor = False
        '
        'btnMFs
        '
        Me.btnMFs.BackColor = System.Drawing.Color.Black
        Me.btnMFs.Location = New System.Drawing.Point(395, 261)
        Me.btnMFs.Name = "btnMFs"
        Me.btnMFs.Size = New System.Drawing.Size(29, 101)
        Me.btnMFs.TabIndex = 57
        Me.btnMFs.Tag = "19"
        Me.btnMFs.UseVisualStyleBackColor = False
        '
        'btnMDs
        '
        Me.btnMDs.BackColor = System.Drawing.Color.Black
        Me.btnMDs.Location = New System.Drawing.Point(321, 261)
        Me.btnMDs.Name = "btnMDs"
        Me.btnMDs.Size = New System.Drawing.Size(29, 101)
        Me.btnMDs.TabIndex = 56
        Me.btnMDs.Tag = "16"
        Me.btnMDs.UseVisualStyleBackColor = False
        '
        'btnMCs
        '
        Me.btnMCs.BackColor = System.Drawing.Color.Black
        Me.btnMCs.Location = New System.Drawing.Point(286, 261)
        Me.btnMCs.Name = "btnMCs"
        Me.btnMCs.Size = New System.Drawing.Size(29, 101)
        Me.btnMCs.TabIndex = 55
        Me.btnMCs.Tag = "14"
        Me.btnMCs.UseVisualStyleBackColor = False
        '
        'btnMB
        '
        Me.btnMB.BackColor = System.Drawing.Color.White
        Me.btnMB.Location = New System.Drawing.Point(482, 261)
        Me.btnMB.Name = "btnMB"
        Me.btnMB.Size = New System.Drawing.Size(39, 167)
        Me.btnMB.TabIndex = 54
        Me.btnMB.Tag = "24"
        Me.btnMB.UseVisualStyleBackColor = False
        '
        'btnMA
        '
        Me.btnMA.BackColor = System.Drawing.Color.White
        Me.btnMA.Location = New System.Drawing.Point(445, 261)
        Me.btnMA.Name = "btnMA"
        Me.btnMA.Size = New System.Drawing.Size(39, 167)
        Me.btnMA.TabIndex = 53
        Me.btnMA.Tag = "22"
        Me.btnMA.UseVisualStyleBackColor = False
        '
        'btnMG
        '
        Me.btnMG.BackColor = System.Drawing.Color.White
        Me.btnMG.Location = New System.Drawing.Point(408, 261)
        Me.btnMG.Name = "btnMG"
        Me.btnMG.Size = New System.Drawing.Size(39, 167)
        Me.btnMG.TabIndex = 52
        Me.btnMG.Tag = "20"
        Me.btnMG.UseVisualStyleBackColor = False
        '
        'btnMF
        '
        Me.btnMF.BackColor = System.Drawing.Color.White
        Me.btnMF.Location = New System.Drawing.Point(371, 261)
        Me.btnMF.Name = "btnMF"
        Me.btnMF.Size = New System.Drawing.Size(39, 167)
        Me.btnMF.TabIndex = 51
        Me.btnMF.Tag = "18"
        Me.btnMF.UseVisualStyleBackColor = False
        '
        'btnME
        '
        Me.btnME.BackColor = System.Drawing.Color.White
        Me.btnME.Location = New System.Drawing.Point(335, 261)
        Me.btnME.Name = "btnME"
        Me.btnME.Size = New System.Drawing.Size(39, 167)
        Me.btnME.TabIndex = 50
        Me.btnME.Tag = "17"
        Me.btnME.UseVisualStyleBackColor = False
        '
        'btnMD
        '
        Me.btnMD.BackColor = System.Drawing.Color.White
        Me.btnMD.Location = New System.Drawing.Point(299, 261)
        Me.btnMD.Name = "btnMD"
        Me.btnMD.Size = New System.Drawing.Size(39, 167)
        Me.btnMD.TabIndex = 49
        Me.btnMD.Tag = "15"
        Me.btnMD.UseVisualStyleBackColor = False
        '
        'btnMC
        '
        Me.btnMC.BackColor = System.Drawing.Color.White
        Me.btnMC.Location = New System.Drawing.Point(262, 261)
        Me.btnMC.Name = "btnMC"
        Me.btnMC.Size = New System.Drawing.Size(39, 167)
        Me.btnMC.TabIndex = 48
        Me.btnMC.Tag = "13"
        Me.btnMC.UseVisualStyleBackColor = False
        '
        'btnLAs
        '
        Me.btnLAs.BackColor = System.Drawing.Color.Black
        Me.btnLAs.Location = New System.Drawing.Point(212, 261)
        Me.btnLAs.Name = "btnLAs"
        Me.btnLAs.Size = New System.Drawing.Size(29, 101)
        Me.btnLAs.TabIndex = 47
        Me.btnLAs.Tag = "11"
        Me.btnLAs.UseVisualStyleBackColor = False
        '
        'btnLGs
        '
        Me.btnLGs.BackColor = System.Drawing.Color.Black
        Me.btnLGs.Location = New System.Drawing.Point(174, 261)
        Me.btnLGs.Name = "btnLGs"
        Me.btnLGs.Size = New System.Drawing.Size(29, 101)
        Me.btnLGs.TabIndex = 46
        Me.btnLGs.Tag = "9"
        Me.btnLGs.UseVisualStyleBackColor = False
        '
        'btnLFs
        '
        Me.btnLFs.BackColor = System.Drawing.Color.Black
        Me.btnLFs.Location = New System.Drawing.Point(139, 261)
        Me.btnLFs.Name = "btnLFs"
        Me.btnLFs.Size = New System.Drawing.Size(29, 101)
        Me.btnLFs.TabIndex = 45
        Me.btnLFs.Tag = "7"
        Me.btnLFs.UseVisualStyleBackColor = False
        '
        'btnLDs
        '
        Me.btnLDs.BackColor = System.Drawing.Color.Black
        Me.btnLDs.Location = New System.Drawing.Point(65, 261)
        Me.btnLDs.Name = "btnLDs"
        Me.btnLDs.Size = New System.Drawing.Size(29, 101)
        Me.btnLDs.TabIndex = 44
        Me.btnLDs.Tag = "4"
        Me.btnLDs.UseVisualStyleBackColor = False
        '
        'btnLCs
        '
        Me.btnLCs.BackColor = System.Drawing.Color.Black
        Me.btnLCs.Location = New System.Drawing.Point(30, 261)
        Me.btnLCs.Name = "btnLCs"
        Me.btnLCs.Size = New System.Drawing.Size(29, 101)
        Me.btnLCs.TabIndex = 43
        Me.btnLCs.Tag = "2"
        Me.btnLCs.UseVisualStyleBackColor = False
        '
        'btnLB
        '
        Me.btnLB.BackColor = System.Drawing.Color.White
        Me.btnLB.Location = New System.Drawing.Point(226, 261)
        Me.btnLB.Name = "btnLB"
        Me.btnLB.Size = New System.Drawing.Size(39, 167)
        Me.btnLB.TabIndex = 42
        Me.btnLB.Tag = "12"
        Me.btnLB.UseVisualStyleBackColor = False
        '
        'btnLA
        '
        Me.btnLA.BackColor = System.Drawing.Color.White
        Me.btnLA.Location = New System.Drawing.Point(189, 261)
        Me.btnLA.Name = "btnLA"
        Me.btnLA.Size = New System.Drawing.Size(39, 167)
        Me.btnLA.TabIndex = 41
        Me.btnLA.Tag = "10"
        Me.btnLA.UseVisualStyleBackColor = False
        '
        'btnLG
        '
        Me.btnLG.BackColor = System.Drawing.Color.White
        Me.btnLG.Location = New System.Drawing.Point(152, 261)
        Me.btnLG.Name = "btnLG"
        Me.btnLG.Size = New System.Drawing.Size(39, 167)
        Me.btnLG.TabIndex = 40
        Me.btnLG.Tag = "8"
        Me.btnLG.UseVisualStyleBackColor = False
        '
        'btnLF
        '
        Me.btnLF.BackColor = System.Drawing.Color.White
        Me.btnLF.Location = New System.Drawing.Point(115, 261)
        Me.btnLF.Name = "btnLF"
        Me.btnLF.Size = New System.Drawing.Size(39, 167)
        Me.btnLF.TabIndex = 39
        Me.btnLF.Tag = "6"
        Me.btnLF.UseVisualStyleBackColor = False
        '
        'btnLE
        '
        Me.btnLE.BackColor = System.Drawing.Color.White
        Me.btnLE.Location = New System.Drawing.Point(79, 261)
        Me.btnLE.Name = "btnLE"
        Me.btnLE.Size = New System.Drawing.Size(39, 167)
        Me.btnLE.TabIndex = 38
        Me.btnLE.Tag = "5"
        Me.btnLE.UseVisualStyleBackColor = False
        '
        'btnLD
        '
        Me.btnLD.BackColor = System.Drawing.Color.White
        Me.btnLD.Location = New System.Drawing.Point(43, 261)
        Me.btnLD.Name = "btnLD"
        Me.btnLD.Size = New System.Drawing.Size(39, 167)
        Me.btnLD.TabIndex = 37
        Me.btnLD.Tag = "3"
        Me.btnLD.UseVisualStyleBackColor = False
        '
        'btnLC
        '
        Me.btnLC.BackColor = System.Drawing.Color.White
        Me.btnLC.Location = New System.Drawing.Point(6, 261)
        Me.btnLC.Name = "btnLC"
        Me.btnLC.Size = New System.Drawing.Size(39, 167)
        Me.btnLC.TabIndex = 36
        Me.btnLC.Tag = "1"
        Me.btnLC.UseVisualStyleBackColor = False
        '
        'frmShapeEdit
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(792, 449)
        Me.Controls.Add(Me.btnHAs)
        Me.Controls.Add(Me.btnHGs)
        Me.Controls.Add(Me.btnHFs)
        Me.Controls.Add(Me.btnHDs)
        Me.Controls.Add(Me.btnHCs)
        Me.Controls.Add(Me.btnHB)
        Me.Controls.Add(Me.btnHA)
        Me.Controls.Add(Me.btnHG)
        Me.Controls.Add(Me.btnHF)
        Me.Controls.Add(Me.btnHE)
        Me.Controls.Add(Me.btnHD)
        Me.Controls.Add(Me.btnHC)
        Me.Controls.Add(Me.btnMAs)
        Me.Controls.Add(Me.btnMGs)
        Me.Controls.Add(Me.btnMFs)
        Me.Controls.Add(Me.btnMDs)
        Me.Controls.Add(Me.btnMCs)
        Me.Controls.Add(Me.btnMB)
        Me.Controls.Add(Me.btnMA)
        Me.Controls.Add(Me.btnMG)
        Me.Controls.Add(Me.btnMF)
        Me.Controls.Add(Me.btnME)
        Me.Controls.Add(Me.btnMD)
        Me.Controls.Add(Me.btnMC)
        Me.Controls.Add(Me.btnLAs)
        Me.Controls.Add(Me.btnLGs)
        Me.Controls.Add(Me.btnLFs)
        Me.Controls.Add(Me.btnLDs)
        Me.Controls.Add(Me.btnLCs)
        Me.Controls.Add(Me.btnLB)
        Me.Controls.Add(Me.btnLA)
        Me.Controls.Add(Me.btnLG)
        Me.Controls.Add(Me.btnLF)
        Me.Controls.Add(Me.btnLE)
        Me.Controls.Add(Me.btnLD)
        Me.Controls.Add(Me.btnLC)
        Me.Controls.Add(Me.nUpDownShapeNr)
        Me.Controls.Add(Me.txtPara5)
        Me.Controls.Add(Me.txtPara4)
        Me.Controls.Add(Me.txtPara3)
        Me.Controls.Add(Me.txtPara2)
        Me.Controls.Add(Me.txtPara1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbShapeName)
        Me.Controls.Add(Me.fraShape)
        Me.Controls.Add(Me.HScrollBar1)
        Me.Name = "frmShapeEdit"
        Me.Text = "Shape Editor"
        CType(Me.nUpDownShapeNr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Sub SetWorkShapes(ByRef shapes As Shape, ByRef ws As Sequence)
        Dim i As Integer

        WorkShapes = shapes
        WorkSeq = ws

        For i = 0 To shapes.n
            Me.cmbShapeName.Items.Add(WorkShapes.shape_set(i).name)
        Next
        If Me.cmbShapeName.SelectedIndex = -1 Then
            Me.cmbShapeName.SelectedItem = WorkShapes.shape_set(0).name
        End If

    End Sub

    Private Sub DrawShape(ByRef gfx As Graphics)
        Dim p As New Pen(Color.Blue, 1)
        Dim spoints() As Point
        Dim i, j, s As Short
        Dim cx, xstep As Single

        i = Me.cmbShapeName.SelectedIndex
        If i > -1 Then
            s = WorkShapes.shape_set(i).size - 1
            If s > 0 Then
                xstep = w / s
                cx = 0
            Else
                xstep = 0
                cx = w / 2
            End If
            ReDim spoints(s)
            For j = 0 To s
                spoints(j) = New Point(cx + p1.X, p1.Y + h - ystep * (WorkShapes.shape_set(i).data(j) And &HF))
                cx += xstep
            Next
            If s > 0 Then
                gfx.DrawLines(p, spoints)
            End If
            For j = 0 To s
                gfx.DrawRectangle(p, spoints(j).X - 2, spoints(j).Y - 2, 4, 4)
            Next
        End If
        '        gfx.DrawLine(p, p1.X, p1.Y, p1.X + w, p1.Y + h)
    End Sub

    Private Sub frmShapeEdit_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        ' only hide window
        e.Cancel = True
        Me.Hide()
    End Sub


    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        DrawShape(e.Graphics)
        '        e.Graphics.DrawLine(p, p1.X, p1.Y, p1.X + w, p1.Y + h)
    End Sub

    Private Sub cmbShapeName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbShapeName.SelectedIndexChanged
        Me.Refresh()
        Me.txtPara1.Text = Format("###", WorkShapes.shape_set(cmbShapeName.SelectedIndex).para1)
        Me.txtPara2.Text = Format("###", WorkShapes.shape_set(cmbShapeName.SelectedIndex).para2)
        Me.txtPara3.Text = Format("###", WorkShapes.shape_set(cmbShapeName.SelectedIndex).para3)
        Me.txtPara4.Text = Format("###", WorkShapes.shape_set(cmbShapeName.SelectedIndex).para4)
        Me.txtPara5.Text = Format("###", WorkShapes.shape_set(cmbShapeName.SelectedIndex).para5)

        Me.nUpDownShapeNr.Value = Me.cmbShapeName.SelectedIndex
    End Sub





    Private Sub Play_KeyDown(ByVal sender As Object, ByVal e As System.EventArgs) _
Handles btnMC.Click, btnHA.Click, btnHAs.Click, btnHB.Click, btnHC.Click, btnHCs.Click, btnHD.Click, btnHDs.Click, btnHE.Click, btnHF.Click, btnHFs.Click, btnHG.Click, btnHGs.Click, btnLA.Click, btnLAs.Click, btnLB.Click, btnLC.Click, btnLCs.Click, btnLD.Click, btnLDs.Click, btnLE.Click, btnLF.Click, btnLFs.Click, btnLG.Click, btnLGs.Click, btnMA.Click, btnMAs.Click, btnMB.Click, btnMC.Click, btnMCs.Click, btnMD.Click, btnMDs.Click, btnME.Click, btnMF.Click, btnMFs.Click, btnMG.Click, btnMGs.Click
        Dim b As Button

        b = sender

        Dim si As _SND_INFO

        WorkSeq.seqs(WorkSeq.temp).seq(0).shape = Me.cmbShapeName.SelectedIndex
        WorkSeq.seqs(WorkSeq.temp).seq(0).note = CInt(b.Tag) - 1
        si.start = 0
        si.last = 1
        si.speed = 3
        Dim s() As Short = {WorkSeq.temp, 0, 0}
        Form1.interpreter.init_voices(si, Interpreter.Mode.PlaySeq, s)
        Form1.WaveGen.PlayFlag = True

    End Sub

    Private Sub nUpDownShapeNr_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nUpDownShapeNr.ValueChanged
        Me.cmbShapeName.SelectedIndex = Me.nUpDownShapeNr.Value
    End Sub
End Class
