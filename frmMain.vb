Imports System.IO
Imports System.Xml.Serialization
Imports System.Runtime.Serialization.Formatters.Binary

Public Class Form1
    Inherits System.Windows.Forms.Form
    Implements IMessageFilter

    Dim ShapeEdit As New frmShapeEdit
    Dim TrackEdit As New frmTrackEdit
    Dim SeqEdit As New frmSeqEdit
    Dim InstrEdit As New frmInstrEdit

    Dim AtariTFMX As New AtariTFMX
    Public Shared WaveGen As New WaveGen
    Dim shapes As New Shape
    Dim instrs As New Instr
    Dim samples As New Sample
    Dim sequences As New clSequence
    Dim tracks As New clTracks(256)
    Public Shared interpreter As New interpreter
    Dim RawYM As New YMImport
    Friend WithEvents cmdInfo As System.Windows.Forms.Button

    Public sndInfo() As Interpreter._SND_INFO


#Region " Vom Windows Form Designer generierter Code "

    Public Sub New()
        MyBase.New()

        ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
        InitializeComponent()

        ' Initialisierungen nach dem Aufruf InitializeComponent() hinzufügen

        myNew()
    End Sub


    Public Sub status(ByRef status As String)
        Me.StatusBar1.Text = status
    End Sub

    ' Die Form überschreibt den Löschvorgang der Basisklasse, um Komponenten zu bereinigen.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        WaveGen.close()

        MyBase.Dispose(disposing)

    End Sub

    ' Für Windows Form-Designer erforderlich
    Private components As System.ComponentModel.IContainer

    'HINWEIS: Die folgende Prozedur ist für den Windows Form-Designer erforderlich
    'Sie kann mit dem Windows Form-Designer modifiziert werden.
    'Verwenden Sie nicht den Code-Editor zur Bearbeitung.
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents mnViewShapEdit As System.Windows.Forms.MenuItem
    Friend WithEvents mnViewTrackEdit As System.Windows.Forms.MenuItem
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents Timer1 As System.Timers.Timer
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents mnFileOpen As System.Windows.Forms.MenuItem
    Friend WithEvents mnViewSeqEdit As System.Windows.Forms.MenuItem
    Friend WithEvents cmdPlay As System.Windows.Forms.Button
    Friend WithEvents cmdStop As System.Windows.Forms.Button
    Friend WithEvents cmbNr As System.Windows.Forms.ComboBox
    Friend WithEvents txtStart As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtLast As System.Windows.Forms.TextBox
    Friend WithEvents txtSpeed As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents mnViewInstrEdit As System.Windows.Forms.MenuItem
    Friend WithEvents mnQuit As System.Windows.Forms.MenuItem
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lbType1 As System.Windows.Forms.Label
    Friend WithEvents lbType2 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lbType3 As System.Windows.Forms.Label
    Friend WithEvents mnFileSave As System.Windows.Forms.MenuItem
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.mnFileOpen = New System.Windows.Forms.MenuItem()
        Me.mnFileSave = New System.Windows.Forms.MenuItem()
        Me.mnQuit = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.mnViewTrackEdit = New System.Windows.Forms.MenuItem()
        Me.mnViewSeqEdit = New System.Windows.Forms.MenuItem()
        Me.mnViewShapEdit = New System.Windows.Forms.MenuItem()
        Me.mnViewInstrEdit = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.Timer1 = New System.Timers.Timer()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.cmdPlay = New System.Windows.Forms.Button()
        Me.cmdStop = New System.Windows.Forms.Button()
        Me.cmbNr = New System.Windows.Forms.ComboBox()
        Me.txtStart = New System.Windows.Forms.TextBox()
        Me.txtLast = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtSpeed = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lbType1 = New System.Windows.Forms.Label()
        Me.lbType2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lbType3 = New System.Windows.Forms.Label()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.cmdInfo = New System.Windows.Forms.Button()
        CType(Me.Timer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem3})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnFileOpen, Me.mnFileSave, Me.mnQuit})
        Me.MenuItem1.Text = "File"
        '
        'mnFileOpen
        '
        Me.mnFileOpen.Index = 0
        Me.mnFileOpen.Text = "Open"
        '
        'mnFileSave
        '
        Me.mnFileSave.Index = 1
        Me.mnFileSave.Text = "Save"
        '
        'mnQuit
        '
        Me.mnQuit.Index = 2
        Me.mnQuit.Text = "Quit"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnViewTrackEdit, Me.mnViewSeqEdit, Me.mnViewShapEdit, Me.mnViewInstrEdit})
        Me.MenuItem2.Text = "View"
        '
        'mnViewTrackEdit
        '
        Me.mnViewTrackEdit.Index = 0
        Me.mnViewTrackEdit.Text = "Track Editor"
        '
        'mnViewSeqEdit
        '
        Me.mnViewSeqEdit.Index = 1
        Me.mnViewSeqEdit.Text = "Seq Editor"
        '
        'mnViewShapEdit
        '
        Me.mnViewShapEdit.Index = 2
        Me.mnViewShapEdit.Text = "Shape Editor"
        '
        'mnViewInstrEdit
        '
        Me.mnViewInstrEdit.Index = 3
        Me.mnViewInstrEdit.Text = "Instr Editor"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 2
        Me.MenuItem3.Text = "About"
        '
        'StatusBar1
        '
        Me.StatusBar1.Font = New System.Drawing.Font("Arial", 7.471698!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusBar1.Location = New System.Drawing.Point(0, 239)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Size = New System.Drawing.Size(292, 24)
        Me.StatusBar1.TabIndex = 0
        Me.StatusBar1.Text = "StatusBar1"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.SynchronizingObject = Me
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Filter = "MadMaxMusic|*.mmm|YMMusic|*.YMm"
        '
        'cmdPlay
        '
        Me.cmdPlay.Location = New System.Drawing.Point(24, 32)
        Me.cmdPlay.Name = "cmdPlay"
        Me.cmdPlay.Size = New System.Drawing.Size(64, 24)
        Me.cmdPlay.TabIndex = 1
        Me.cmdPlay.Text = "Play"
        '
        'cmdStop
        '
        Me.cmdStop.Location = New System.Drawing.Point(112, 32)
        Me.cmdStop.Name = "cmdStop"
        Me.cmdStop.Size = New System.Drawing.Size(56, 24)
        Me.cmdStop.TabIndex = 2
        Me.cmdStop.Text = "Stop"
        '
        'cmbNr
        '
        Me.cmbNr.Location = New System.Drawing.Point(24, 96)
        Me.cmbNr.Name = "cmbNr"
        Me.cmbNr.Size = New System.Drawing.Size(48, 21)
        Me.cmbNr.TabIndex = 3
        Me.cmbNr.Text = "ComboBox1"
        '
        'txtStart
        '
        Me.txtStart.Location = New System.Drawing.Point(88, 96)
        Me.txtStart.Name = "txtStart"
        Me.txtStart.Size = New System.Drawing.Size(40, 20)
        Me.txtStart.TabIndex = 4
        Me.txtStart.Text = "TextBox1"
        '
        'txtLast
        '
        Me.txtLast.Location = New System.Drawing.Point(144, 96)
        Me.txtLast.Name = "txtLast"
        Me.txtLast.Size = New System.Drawing.Size(40, 20)
        Me.txtLast.TabIndex = 5
        Me.txtLast.Text = "TextBox2"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(24, 80)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 16)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Nr"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(88, 80)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 16)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Start"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(144, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 16)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "End"
        '
        'txtSpeed
        '
        Me.txtSpeed.Location = New System.Drawing.Point(200, 96)
        Me.txtSpeed.Name = "txtSpeed"
        Me.txtSpeed.Size = New System.Drawing.Size(40, 20)
        Me.txtSpeed.TabIndex = 9
        Me.txtSpeed.Text = "TextBox2"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(200, 80)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 16)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Speed"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(24, 152)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 16)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Type1:"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(24, 176)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 16)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Type2:"
        '
        'lbType1
        '
        Me.lbType1.Location = New System.Drawing.Point(96, 152)
        Me.lbType1.Name = "lbType1"
        Me.lbType1.Size = New System.Drawing.Size(64, 16)
        Me.lbType1.TabIndex = 13
        '
        'lbType2
        '
        Me.lbType2.Location = New System.Drawing.Point(96, 176)
        Me.lbType2.Name = "lbType2"
        Me.lbType2.Size = New System.Drawing.Size(64, 16)
        Me.lbType2.TabIndex = 14
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(24, 200)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(64, 16)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "Type3:"
        '
        'lbType3
        '
        Me.lbType3.Location = New System.Drawing.Point(96, 200)
        Me.lbType3.Name = "lbType3"
        Me.lbType3.Size = New System.Drawing.Size(64, 16)
        Me.lbType3.TabIndex = 16
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.Filter = "YMMusic (YMm)|*.YMm"
        '
        'cmdInfo
        '
        Me.cmdInfo.Location = New System.Drawing.Point(184, 32)
        Me.cmdInfo.Name = "cmdInfo"
        Me.cmdInfo.Size = New System.Drawing.Size(56, 24)
        Me.cmdInfo.TabIndex = 17
        Me.cmdInfo.Text = "Info"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 263)
        Me.Controls.Add(Me.cmdInfo)
        Me.Controls.Add(Me.lbType3)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.lbType2)
        Me.Controls.Add(Me.lbType1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtSpeed)
        Me.Controls.Add(Me.txtLast)
        Me.Controls.Add(Me.txtStart)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbNr)
        Me.Controls.Add(Me.cmdStop)
        Me.Controls.Add(Me.cmdPlay)
        Me.Controls.Add(Me.StatusBar1)
        Me.Menu = Me.MainMenu1
        Me.Name = "Form1"
        Me.Text = "YM Your Music"
        CType(Me.Timer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Sub myNew()
        ' RawYM.Open("C:\Dokumente und Einstellungen\bm\Eigene Dateien\Atari\YM\prehis.bin")


        Timer1.Enabled = False
        WaveGen.reset()
        WaveGen.init(Me, RawYM, interpreter, 2, samples)

        'Standard-Verzeichnis (Eigene Dateien)  setzen
        OpenFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
        SaveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)


#If DEBUG Then
        'LoadTFMX("C:\WorkSpace\YMEmu\YMEmu\Sounds\OH_MONGO.mmm")
        'LoadTFMX("C:\Dokumente und Einstellungen\bm\Eigene Dateien\Atari\YM\mmm\Wings_Of_Death\wings_of_death.02.mmm")
        ' LoadTFMX("D:\Users\Dunkelwind\Visual Studio 2010\Projects\ymPlay1\mmm\Last_Hero.mmm")
        LoadTFMX("D:\Users\Dunkelwind\Visual Studio 2010\Projects\ymPlay1\mmm\elands8.mmm")
#End If
        Timer1.Enabled = True
        Application.AddMessageFilter(Me)

    End Sub

    Private Sub mnViewShapEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnViewShapEdit.Click
        ShapeEdit.SetWorkShapes(shapes, sequences)
        ShapeEdit.Show()
    End Sub

    Private Sub mnViewTrackEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnViewTrackEdit.Click
        TrackEdit.Show()
    End Sub

    Private Sub Timer1_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles Timer1.Elapsed
        Static lastTrackPos As Integer = -1
        Dim trackPos As Integer
        Static lastSeqPos As Integer = -1
        Dim seqPos As Integer

        trackPos = interpreter.trackPos
        seqPos = interpreter.VoiceSet(0).seq_index

        If seqPos <> lastSeqPos Then
            Me.SeqEdit.follow(trackPos, seqPos, interpreter.PlayMode)
            lastSeqPos = seqPos
        End If
        If trackPos <> lastTrackPos Then
            Me.StatusBar1.Text = Format(trackPos)
            Me.TrackEdit.follow(trackPos)
            lastTrackPos = trackPos
        End If


    End Sub


    Private Sub MenuItem3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItem3.Click
        Dim a As New frmAbout
        a.ShowDialog()

    End Sub

    Private Sub mnFileOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnFileOpen.Click
        If My.Settings.LastPathLoad <> "" Then
            OpenFileDialog1.InitialDirectory = My.Settings.LastPathLoad
        End If

        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            WaveGen.PlayFlag = False

            If OpenFileDialog1.FileName.EndsWith(".mmm") Then
                LoadTFMX(OpenFileDialog1.FileName)
                My.Settings.LastPathLoad = Path.GetDirectoryName(OpenFileDialog1.FileName)
            End If
            If OpenFileDialog1.FileName.EndsWith(".YMm", StringComparison.CurrentCultureIgnoreCase) Then
                LoadYMm(OpenFileDialog1.FileName)
            End If

            WaveGen.PlayFlag = True
        End If
    End Sub

    Private Sub mnFileSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnFileSave.Click

        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            SaveYMm(SaveFileDialog1.FileName)
        End If

    End Sub

    Private Sub SaveYMm(ByVal fname As String)
        Dim YMm As New clYMm
        Dim fs As New FileStream(fname, FileMode.Create)

        YMm.id1 = lbType1.Text
        YMm.id2 = lbType2.Text
        YMm.id3 = lbType3.Text
        YMm.instr = instrs
        YMm.shapes = shapes
        YMm.seqs = sequences
        YMm.tracks = tracks
        YMm.sndInfo = sndInfo

        'Dim serializer As New BinaryFormatter
        Dim xmlSer As New Xml.Serialization.XmlSerializer(YMm.GetType)
        xmlSer.Serialize(fs, YMm)


        'xmlSer = New Xml.Serialization.XmlSerializer(sequences.seqs.GetType)
        'xmlSer.Serialize(fs, sequences.seqs)

        'xmlSer = New Xml.Serialization.XmlSerializer(shapes.GetType)
        'xmlSer.Serialize(fs, shapes)

        'tracks.save(fs)

        'xmlSer = New Xml.Serialization.XmlSerializer(sndInfo.GetType)
        'xmlSer.Serialize(fs, sndInfo)



        fs.Close()
    End Sub

    Private Sub LoadYMm(ByVal fname As String)
        Dim fs As New FileStream(fname, FileMode.Open)
        Dim YMm As New clYMm
        Dim serializer As New BinaryFormatter
        Dim si As Interpreter._SND_INFO
        Dim i As Short

        Dim xmlSer As New Xml.Serialization.XmlSerializer(YMm.GetType)
        YMm = xmlSer.Deserialize(fs)

        lbType1.Text = YMm.id1
        lbType2.Text = YMm.id2
        lbType3.Text = YMm.id3
        instrs = YMm.instr
        sequences = YMm.seqs
        tracks = YMm.tracks
        sndInfo = YMm.sndInfo

        'lbType2.Text = xmlSer.Deserialize(fs)
        'lbType3.Text = xmlSer.Deserialize(fs)

        'xmlSer = New Xml.Serialization.XmlSerializer(instrs.instr.GetType)
        'instrs.instr = xmlSer.Deserialize(fs)

        'xmlSer = New Xml.Serialization.XmlSerializer(sequences.seqs.GetType)
        'sequences.seqs = xmlSer.Deserialize(fs)

        'xmlSer = New Xml.Serialization.XmlSerializer(shapes.GetType)
        'shapes = xmlSer.Deserialize(fs)

        '' tracks.load(fs)

        'xmlSer = New Xml.Serialization.XmlSerializer(sndInfo.GetType)
        'sndInfo = xmlSer.Deserialize(fs)
        fs.Close()

        'lbType1.Text = serializer.Deserialize(fs)
        'lbType2.Text = serializer.Deserialize(fs)
        'lbType3.Text = serializer.Deserialize(fs)


        'instrs.instr = serializer.Deserialize(fs)
        'sequences.seqs = serializer.Deserialize(fs)
        'shapes = serializer.Deserialize(fs)
        'tracks.load(fs)
        'sndInfo = serializer.Deserialize(fs)


        interpreter.init(lbType2.Text, instrs, sequences, shapes, tracks)
        interpreter.init_voices(sndInfo(0), interpreter.Mode.PlayTrack)

        WaveGen.reset()

        i = 0
        Me.cmbNr.Items.Clear()
        For Each si In sndInfo
            Me.cmbNr.Items.Add(String.Format("{0}", i))
            i += 1
        Next
        Me.cmbNr.Text = Me.cmbNr.Items(0)
    End Sub


    Private Sub LoadTFMX(ByVal fname As String)
        Dim si As Interpreter._SND_INFO
        Dim i As Short
        Dim t1, t2, t3 As String

        AtariTFMX.Open(fname)
        AtariTFMX.LoadShapes(shapes)
        AtariTFMX.LoadTracks(tracks)
        AtariTFMX.LoadInstr(instrs)
        AtariTFMX.LoadSeq(sequences)
        AtariTFMX.LoadSndInfo(sndInfo)
        AtariTFMX.LoadSamples(samples)

        AtariTFMX.type(t1, t2, t3)
        lbType1.Text = t1
        lbType2.Text = t2
        lbType3.Text = t3


        interpreter.init(t2, instrs, sequences, shapes, tracks)
        interpreter.init_voices(sndInfo(0), interpreter.Mode.PlayTrack)

        WaveGen.reset()
#If dump Then
        WaveGen.openFile("D:\ymTest.bin")
#End If
        i = 0
        Me.cmbNr.Items.Clear()
        For Each si In sndInfo
            Me.cmbNr.Items.Add(String.Format("{0}", i))
            i += 1
        Next
        Me.cmbNr.Text = Me.cmbNr.Items(0)
    End Sub

    Private Sub mnViewSeqEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnViewSeqEdit.Click
        SeqEdit.Show()
    End Sub

    Private Sub mnViewInstrEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnViewInstrEdit.Click
        InstrEdit.SetWorkInstr(instrs)
        InstrEdit.Show()
    End Sub

    Private Sub cmdStop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdStop.Click
        WaveGen.PlayFlag = False
        interpreter.init_voices(sndInfo(0), interpreter.Mode.PlayTrack)
    End Sub

    Private Sub cmdPlay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPlay.Click
        TrackEdit.SetWorkTrack(tracks)
        SeqEdit.SetWorkSeq(tracks, sequences)
        interpreter.init_voices(sndInfo(0), interpreter.Mode.PlayTrack)
        WaveGen.PlayFlag = True
    End Sub

    Private Sub cmbNr_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbNr.SelectedIndexChanged
        Dim si As Interpreter._SND_INFO
        Timer1.Enabled = False
        WaveGen.PlayFlag = False
        TrackEdit.SetWorkTrack(tracks)
        SeqEdit.SetWorkSeq(tracks, sequences)

        si = sndInfo(Me.cmbNr.SelectedIndex)
        Me.txtStart.Text = Format("###", si.start)
        Me.txtLast.Text = Format("###", si.last)
        Me.txtSpeed.Text = Format("###", si.speed)
        If si.last > 0 Then
            WaveGen.PlayFlag = False
            interpreter.init_voices(si, interpreter.Mode.PlayTrack)
            WaveGen.PlayFlag = True
            Timer1.Enabled = True
        End If
    End Sub


    Private Sub mnQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnQuit.Click
        Me.Close()
    End Sub

    Public Function PreFilterMessage(ByRef m As System.Windows.Forms.Message) As Boolean Implements System.Windows.Forms.IMessageFilter.PreFilterMessage
        Dim f As Boolean = False
        Dim key As Integer

        If m.Msg = &H100 Then
            key = m.WParam.ToInt32
            If TrackEdit.ContainsFocus Then
                f = TrackEdit.handleKey(key)
            End If
            If SeqEdit.ContainsFocus Then
                f = SeqEdit.handleKey(key)
            End If

        End If

        Return f
    End Function


    Private Sub cmdInfo_Click(sender As System.Object, e As System.EventArgs) Handles cmdInfo.Click
        Dim ymm As New clYMm
        Dim info As New frmInfo

        info.Show()
        ymm.id1 = lbType1.Text
        ymm.id2 = lbType2.Text
        ymm.id3 = lbType3.Text
        ymm.instr = instrs
        ymm.shapes = shapes
        ymm.seqs = sequences
        ymm.tracks = tracks
        ymm.sndInfo = sndInfo

        info.print(ymm)
    End Sub
End Class
