<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_pcomm
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(frm_pcomm))
        StatusStrip1 = New StatusStrip()
        ToolStripDropDownButton1 = New ToolStripDropDownButton()
        ToolStripComboBox1 = New ToolStripComboBox()
        ToolStripStatusLabel1 = New ToolStripStatusLabel()
        lblstatus = New ToolStripStatusLabel()
        ToolStripStatusLabel2 = New ToolStripStatusLabel()
        ToolStripStatusLabel3 = New ToolStripStatusLabel()
        GroupBox2 = New GroupBox()
        Label1 = New Label()
        CheckBox2 = New CheckBox()
        Label4 = New Label()
        NumericUpDown2 = New NumericUpDown()
        CheckBox1 = New CheckBox()
        Button6 = New Button()
        Button3 = New Button()
        Button5 = New Button()
        Label2 = New Label()
        Button4 = New Button()
        NumInicio = New NumericUpDown()
        Label3 = New Label()
        NumFim = New NumericUpDown()
        Button1 = New Button()
        RichTextBox1 = New RichTextBox()
        Timer1 = New Timer(components)
        ToolTip1 = New ToolTip(components)
        TabControl1 = New TabControl()
        TabPage3 = New TabPage()
        TabPage1 = New TabPage()
        Button7 = New Button()
        BackgroundWorker1 = New ComponentModel.BackgroundWorker()
        AToolStripMenuItem = New ToolStripMenuItem()
        BToolStripMenuItem = New ToolStripMenuItem()
        CToolStripMenuItem = New ToolStripMenuItem()
        StatusStrip1.SuspendLayout()
        GroupBox2.SuspendLayout()
        CType(NumericUpDown2, ComponentModel.ISupportInitialize).BeginInit()
        CType(NumInicio, ComponentModel.ISupportInitialize).BeginInit()
        CType(NumFim, ComponentModel.ISupportInitialize).BeginInit()
        TabControl1.SuspendLayout()
        TabPage3.SuspendLayout()
        TabPage1.SuspendLayout()
        SuspendLayout()

        ' StatusStrip1
        StatusStrip1.ImageScalingSize = New Size(20, 20)
        StatusStrip1.Items.AddRange(New ToolStripItem() {ToolStripDropDownButton1, ToolStripStatusLabel1, lblstatus, ToolStripStatusLabel2, ToolStripStatusLabel3})
        StatusStrip1.Location = New Point(0, 546)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Padding = New Padding(1, 0, 12, 0)
        StatusStrip1.Size = New Size(717, 22)
        StatusStrip1.TabIndex = 2
        StatusStrip1.Text = "StatusStrip1"

        ' ToolStripDropDownButton1
        ToolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Text
        ToolStripDropDownButton1.DropDownItems.AddRange(New ToolStripItem() {ToolStripComboBox1})
        ToolStripDropDownButton1.Image = CType(resources.GetObject("ToolStripDropDownButton1.Image"), Image)
        ToolStripDropDownButton1.ImageTransparentColor = Color.Magenta
        ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
        ToolStripDropDownButton1.Size = New Size(59, 20)
        ToolStripDropDownButton1.Text = "Session"

        ' ToolStripComboBox1
        ToolStripComboBox1.Items.AddRange(New Object() {"A", "B", "C", "D"})
        ToolStripComboBox1.Name = "ToolStripComboBox1"
        ToolStripComboBox1.Size = New Size(121, 23)
        ToolStripComboBox1.Text = "A"

        ' ToolStripStatusLabel1
        ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        ToolStripStatusLabel1.Size = New Size(42, 17)
        ToolStripStatusLabel1.Text = "Status:"

        ' lblstatus
        lblstatus.ForeColor = Color.Red
        lblstatus.Name = "lblstatus"
        lblstatus.Size = New Size(43, 17)
        lblstatus.Text = "Offline"

        ' ToolStripStatusLabel2
        ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        ToolStripStatusLabel2.Size = New Size(109, 17)
        ToolStripStatusLabel2.Text = "Import Status:"

        ' ToolStripStatusLabel3
        ToolStripStatusLabel3.Font = New Font("Segoe UI", 9.0F, FontStyle.Italic Or FontStyle.Underline, GraphicsUnit.Point)
        ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        ToolStripStatusLabel3.Size = New Size(65, 17)
        ToolStripStatusLabel3.Text = "Stopped"

        ' GroupBox2
        GroupBox2.Controls.Add(Label1)
        GroupBox2.Controls.Add(CheckBox2)
        GroupBox2.Controls.Add(Label4)
        GroupBox2.Controls.Add(NumericUpDown2)
        GroupBox2.Controls.Add(CheckBox1)
        GroupBox2.Controls.Add(Button6)
        GroupBox2.Controls.Add(Button3)
        GroupBox2.Controls.Add(Button5)
        GroupBox2.Controls.Add(Label2)
        GroupBox2.Controls.Add(Button4)
        GroupBox2.Controls.Add(NumInicio)
        GroupBox2.Controls.Add(Label3)
        GroupBox2.Controls.Add(NumFim)
        GroupBox2.Dock = DockStyle.Fill
        GroupBox2.Location = New Point(3, 2)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(703, 114)
        GroupBox2.TabIndex = 0
        GroupBox2.TabStop = False
        GroupBox2.Text = "HardCopy"
        ToolTip1.SetToolTip(GroupBox2, "The trigger to detect the end of the page is the blank line at position 22.")

        ' Label1
        Label1.AutoSize = True
        Label1.Location = New Point(155, 24)
        Label1.Name = "Label1"
        Label1.Size = New Size(163, 15)
        Label1.TabIndex = 15
        Label1.Text = "Lines that detect the table"

        ' CheckBox2
        CheckBox2.Location = New Point(417, 70)
        CheckBox2.Name = "CheckBox2"
        CheckBox2.Size = New Size(139, 38)
        CheckBox2.TabIndex = 14
        CheckBox2.Text = "Only first Page"
        CheckBox2.UseVisualStyleBackColor = True

        ' Label4
        Label4.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label4.Location = New Point(302, 65)
        Label4.Name = "Label4"
        Label4.Size = New Size(99, 15)
        Label4.TabIndex = 13
        Label4.Text = "Range:"

        ' NumericUpDown2
        NumericUpDown2.Location = New Point(302, 85)
        NumericUpDown2.Maximum = New [Decimal](New Integer() {9999, 0, 0, 0})
        NumericUpDown2.Name = "NumericUpDown2"
        NumericUpDown2.ReadOnly = True
        NumericUpDown2.Size = New Size(99, 23)
        NumericUpDown2.TabIndex = 12
        NumericUpDown2.TextAlign = HorizontalAlignment.Center

        ' CheckBox1
        CheckBox1.Location = New Point(417, 45)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(139, 20)
        CheckBox1.TabIndex = 11
        CheckBox1.Text = "Real-time capture"
        CheckBox1.UseVisualStyleBackColor = True

        ' Button6
        Button6.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Button6.Location = New Point(572, 20)
        Button6.Name = "Button6"
        Button6.Size = New Size(116, 88)
        Button6.TabIndex = 10
        Button6.Text = "Clear"
        Button6.UseVisualStyleBackColor = True

        ' Button3
        Button3.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Button3.Location = New Point(14, 20)
        Button3.Name = "Button3"
        Button3.Size = New Size(116, 88)
        Button3.TabIndex = 0
        Button3.Text = "Start Capture"
        Button3.UseVisualStyleBackColor = True

        ' Button5
        Button5.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Button5.Location = New Point(216, 85)
        Button5.Name = "Button5"
        Button5.Size = New Size(54, 23)
        Button5.TabIndex = 4
        Button5.Text = "Get"
        Button5.UseVisualStyleBackColor = True

        ' Label2
        Label2.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label2.AutoSize = True
        Label2.Location = New Point(155, 43)
        Label2.Name = "Label2"
        Label2.Size = New Size(37, 15)
        Label2.TabIndex = 6
        Label2.Text = "Start:"

        ' Button4
        Button4.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Button4.Location = New Point(155, 85)
        Button4.Name = "Button4"
        Button4.Size = New Size(54, 23)
        Button4.TabIndex = 3
        Button4.Text = "Get"
        Button4.UseVisualStyleBackColor = True

        ' NumInicio
        NumInicio.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        NumInicio.Location = New Point(155, 60)
        NumInicio.Maximum = New [Decimal](New Integer() {24, 0, 0, 0})
        NumInicio.Minimum = New [Decimal](New Integer() {1, 0, 0, 0})
        NumInicio.Name = "NumInicio"
        NumInicio.Size = New Size(54, 23)
        NumInicio.TabIndex = 1
        NumInicio.TextAlign = HorizontalAlignment.Center
        NumInicio.Value = New [Decimal](New Integer() {1, 0, 0, 0})

        ' Label3
        Label3.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label3.AutoSize = True
        Label3.Location = New Point(214, 43)
        Label3.Name = "Label3"
        Label3.Size = New Size(29, 15)
        Label3.TabIndex = 9
        Label3.Text = "End:"

        ' NumFim
        NumFim.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        NumFim.Location = New Point(214, 60)
        NumFim.Maximum = New [Decimal](New Integer() {24, 0, 0, 0})
        NumFim.Minimum = New [Decimal](New Integer() {1, 0, 0, 0})
        NumFim.Name = "NumFim"
        NumFim.Size = New Size(56, 23)
        NumFim.TabIndex = 2
        NumFim.TextAlign = HorizontalAlignment.Center
        NumFim.Value = New [Decimal](New Integer() {1, 0, 0, 0})

        ' Button1
        Button1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button1.Location = New Point(6, 4)
        Button1.Name = "Button1"
        Button1.Size = New Size(116, 109)
        Button1.TabIndex = 0
        Button1.Text = "Capture Screen"
        Button1.UseVisualStyleBackColor = True

        ' RichTextBox1
        RichTextBox1.BackColor = Color.Black
        RichTextBox1.Dock = DockStyle.Fill
        RichTextBox1.Font = New Font("Consolas", 10.8F, FontStyle.Regular, GraphicsUnit.Point)
        RichTextBox1.ForeColor = Color.Aqua
        RichTextBox1.Location = New Point(0, 146)
        RichTextBox1.Name = "RichTextBox1"
        RichTextBox1.Size = New Size(717, 400)
        RichTextBox1.TabIndex = 1
        RichTextBox1.Text = ""
        RichTextBox1.WordWrap = False

        ' Timer1
        Timer1.Enabled = True
        Timer1.Interval = 1000

        ' TabControl1
        TabControl1.Controls.Add(TabPage3)
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Dock = DockStyle.Top
        TabControl1.Location = New Point(0, 0)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(717, 146)
        TabControl1.TabIndex = 0

        ' TabPage3
        TabPage3.BackColor = SystemColors.Control
        TabPage3.Controls.Add(GroupBox2)
        TabPage3.Location = New Point(4, 24)
        TabPage3.Name = "TabPage3"
        TabPage3.Size = New Size(709, 118)
        TabPage3.TabIndex = 2
        TabPage3.Text = "Automatic"

        ' TabPage1
        TabPage1.BackColor = SystemColors.Control
        TabPage1.Controls.Add(Button7)
        TabPage1.Controls.Add(Button1)
        TabPage1.Location = New Point(4, 24)
        TabPage1.Name = "TabPage1"
        TabPage1.Size = New Size(709, 118)
        TabPage1.TabIndex = 0
        TabPage1.Text = "Manual"

        ' Button7
        Button7.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Button7.Location = New Point(585, 4)
        Button7.Name = "Button7"
        Button7.Size = New Size(116, 109)
        Button7.TabIndex = 11
        Button7.Text = "Clear"
        Button7.UseVisualStyleBackColor = True

        ' BackgroundWorker1
        BackgroundWorker1.WorkerSupportsCancellation = True

        ' frm_pcomm
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(717, 568)
        Controls.Add(RichTextBox1)
        Controls.Add(TabControl1)
        Controls.Add(StatusStrip1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "frm_pcomm"
        Text = "Extract from PCOMM"
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        CType(NumericUpDown2, ComponentModel.ISupportInitialize).EndInit()
        CType(NumInicio, ComponentModel.ISupportInitialize).EndInit()
        CType(NumFim, ComponentModel.ISupportInitialize).EndInit()
        TabControl1.ResumeLayout(False)
        TabPage3.ResumeLayout(False)
        TabPage1.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents lblstatus As ToolStripStatusLabel
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Button1 As Button
    Friend WithEvents ToolStripDropDownButton1 As ToolStripDropDownButton
    Friend WithEvents ToolStripComboBox1 As ToolStripComboBox
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents NumFim As NumericUpDown
    Friend WithEvents NumInicio As NumericUpDown
    Friend WithEvents Label2 As Label
    Friend WithEvents Button5 As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Button6 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents NumericUpDown2 As NumericUpDown
    Friend WithEvents ToolStripStatusLabel2 As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel3 As ToolStripStatusLabel
    Friend WithEvents Label4 As Label
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents AToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CToolStripMenuItem As ToolStripMenuItem
End Class
