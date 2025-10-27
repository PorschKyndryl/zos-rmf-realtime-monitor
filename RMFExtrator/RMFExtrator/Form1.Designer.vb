<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        SplitContainer1 = New SplitContainer()
        GroupBox3 = New GroupBox()
        TabControl2 = New TabControl()
        TabPage5 = New TabPage()
        SplitContainer2 = New SplitContainer()
        dg_linhas = New DataGridView()
        Label2 = New Label()
        dg_table = New DataGridView()
        Label3 = New Label()
        Panel1 = New Panel()
        GroupBox6 = New GroupBox()
        RadioButton8 = New RadioButton()
        RadioButton7 = New RadioButton()
        RadioButton6 = New RadioButton()
        RadioButton5 = New RadioButton()
        RadioButton4 = New RadioButton()
        GroupBox4 = New GroupBox()
        TabControl1 = New TabControl()
        TabPage2 = New TabPage()
        RichTextBox1 = New RichTextBox()
        ContextMenuStrip1 = New ContextMenuStrip(components)
        SelecionarCampoToolStripMenuItem = New ToolStripMenuItem()
        ToolStripMenuItem1 = New ToolStripSeparator()
        ExtrairDaPCOMMToolStripMenuItem = New ToolStripMenuItem()
        StatusStrip2 = New StatusStrip()
        txtposi = New ToolStripStatusLabel()
        TabPage1 = New TabPage()
        dg_result = New Zuby.ADGV.AdvancedDataGridView()
        Button4 = New Button()
        Button2 = New Button()
        TabPage3 = New TabPage()
        FormsPlot1 = New ScottPlot.WinForms.FormsPlot()
        GroupBox7 = New GroupBox()
        Button5 = New Button()
        CheckBox1 = New CheckBox()
        TextBox3 = New TextBox()
        Label7 = New Label()
        Label5 = New Label()
        TextBox1 = New TextBox()
        TextBox2 = New TextBox()
        Label6 = New Label()
        DataGridViewTextBoxColumn6 = New DataGridViewTextBoxColumn()
        col_type = New DataGridViewComboBoxColumn()
        col_eixe = New DataGridViewComboBoxColumn()
        GroupBox2 = New GroupBox()
        Label4 = New Label()
        txtseparador = New TextBox()
        RadioButton3 = New RadioButton()
        Label1 = New Label()
        txtoffset = New TextBox()
        txt_pag = New TextBox()
        btn_exec = New Button()
        RadioButton1 = New RadioButton()
        RadioButton2 = New RadioButton()
        GroupBox1 = New GroupBox()
        Button3 = New Button()
        txt_entrada = New TextBox()
        Button1 = New Button()
        StatusStrip1 = New StatusStrip()
        ToolStripProgressBar1 = New ToolStripProgressBar()
        lbl_status = New ToolStripStatusLabel()
        ToolStripStatusLabel1 = New ToolStripStatusLabel()
        lbl_paglida = New ToolStripStatusLabel()
        ToolStripStatusLabel2 = New ToolStripStatusLabel()
        lblresult = New ToolStripStatusLabel()
        ToolStripStatusLabel4 = New ToolStripStatusLabel()
        txttime = New ToolStripStatusLabel()
        ToolStripStatusLabel3 = New ToolStripStatusLabel()
        OpenFileDialog1 = New OpenFileDialog()
        ToolTip1 = New ToolTip(components)
        BackgroundWorker1 = New ComponentModel.BackgroundWorker()
        MenuStrip1 = New MenuStrip()
        BackgroundWorker2 = New ComponentModel.BackgroundWorker()
        BackgroundWorker3 = New ComponentModel.BackgroundWorker()
        BackgroundWorker4 = New ComponentModel.BackgroundWorker()
        Timer1 = New Timer(components)
        Timer2 = New Timer(components)
        Col_Nome = New DataGridViewTextBoxColumn()
        Col_NLinha = New DataGridViewTextBoxColumn()
        Col_PosiInicio = New DataGridViewTextBoxColumn()
        Col_QTDChar = New DataGridViewTextBoxColumn()
        Col_Page = New DataGridViewTextBoxColumn()
        colTipo = New DataGridViewComboBoxColumn()
        ColEixo = New DataGridViewComboBoxColumn()
        col_operacao1 = New DataGridViewComboBoxColumn()
        DataGridViewTextBoxColumn1 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn2 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn3 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn4 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn5 = New DataGridViewTextBoxColumn()
        colType = New DataGridViewComboBoxColumn()
        colEixo2 = New DataGridViewComboBoxColumn()
        col_filter = New DataGridViewTextBoxColumn()
        col_operacao = New DataGridViewComboBoxColumn()
        CType(SplitContainer1, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer1.Panel1.SuspendLayout()
        SplitContainer1.Panel2.SuspendLayout()
        SplitContainer1.SuspendLayout()
        GroupBox3.SuspendLayout()
        TabControl2.SuspendLayout()
        TabPage5.SuspendLayout()
        CType(SplitContainer2, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer2.Panel1.SuspendLayout()
        SplitContainer2.Panel2.SuspendLayout()
        SplitContainer2.SuspendLayout()
        CType(dg_linhas, ComponentModel.ISupportInitialize).BeginInit()
        CType(dg_table, ComponentModel.ISupportInitialize).BeginInit()
        Panel1.SuspendLayout()
        GroupBox6.SuspendLayout()
        GroupBox4.SuspendLayout()
        TabControl1.SuspendLayout()
        TabPage2.SuspendLayout()
        ContextMenuStrip1.SuspendLayout()
        StatusStrip2.SuspendLayout()
        TabPage1.SuspendLayout()
        CType(dg_result, ComponentModel.ISupportInitialize).BeginInit()
        TabPage3.SuspendLayout()
        GroupBox7.SuspendLayout()
        GroupBox2.SuspendLayout()
        GroupBox1.SuspendLayout()
        StatusStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' SplitContainer1
        ' 
        SplitContainer1.Dock = DockStyle.Fill
        SplitContainer1.Location = New Point(0, 130)
        SplitContainer1.Margin = New Padding(3, 2, 3, 2)
        SplitContainer1.Name = "SplitContainer1"
        ' 
        ' SplitContainer1.Panel1
        ' 
        SplitContainer1.Panel1.Controls.Add(GroupBox3)
        ' 
        ' SplitContainer1.Panel2
        ' 
        SplitContainer1.Panel2.Controls.Add(GroupBox4)
        SplitContainer1.Size = New Size(1606, 653)
        SplitContainer1.SplitterDistance = 750
        SplitContainer1.TabIndex = 8
        ' 
        ' GroupBox3
        ' 
        GroupBox3.Controls.Add(TabControl2)
        GroupBox3.Dock = DockStyle.Fill
        GroupBox3.Location = New Point(0, 0)
        GroupBox3.Margin = New Padding(3, 2, 3, 2)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Padding = New Padding(3, 2, 3, 2)
        GroupBox3.Size = New Size(750, 653)
        GroupBox3.TabIndex = 0
        GroupBox3.TabStop = False
        GroupBox3.Text = "Fields"
        ' 
        ' TabControl2
        ' 
        TabControl2.Controls.Add(TabPage5)
        TabControl2.Dock = DockStyle.Fill
        TabControl2.Location = New Point(3, 18)
        TabControl2.Margin = New Padding(3, 2, 3, 2)
        TabControl2.Name = "TabControl2"
        TabControl2.SelectedIndex = 0
        TabControl2.Size = New Size(744, 633)
        TabControl2.TabIndex = 3
        ' 
        ' TabPage5
        ' 
        TabPage5.Controls.Add(SplitContainer2)
        TabPage5.Controls.Add(Panel1)
        TabPage5.Location = New Point(4, 24)
        TabPage5.Margin = New Padding(3, 2, 3, 2)
        TabPage5.Name = "TabPage5"
        TabPage5.Padding = New Padding(3, 2, 3, 2)
        TabPage5.Size = New Size(736, 605)
        TabPage5.TabIndex = 0
        TabPage5.Text = "Fields"
        TabPage5.UseVisualStyleBackColor = True
        ' 
        ' SplitContainer2
        ' 
        SplitContainer2.Dock = DockStyle.Fill
        SplitContainer2.Location = New Point(3, 61)
        SplitContainer2.Margin = New Padding(3, 2, 3, 2)
        SplitContainer2.Name = "SplitContainer2"
        SplitContainer2.Orientation = Orientation.Horizontal
        ' 
        ' SplitContainer2.Panel1
        ' 
        SplitContainer2.Panel1.Controls.Add(dg_linhas)
        SplitContainer2.Panel1.Controls.Add(Label2)
        ' 
        ' SplitContainer2.Panel2
        ' 
        SplitContainer2.Panel2.Controls.Add(dg_table)
        SplitContainer2.Panel2.Controls.Add(Label3)
        SplitContainer2.Size = New Size(730, 542)
        SplitContainer2.SplitterDistance = 252
        SplitContainer2.SplitterWidth = 3
        SplitContainer2.TabIndex = 1
        ' 
        ' dg_linhas
        ' 
        dg_linhas.AllowUserToOrderColumns = True
        dg_linhas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dg_linhas.Columns.AddRange(New DataGridViewColumn() {Col_Nome, Col_NLinha, Col_PosiInicio, Col_QTDChar, Col_Page, colTipo, ColEixo, col_operacao1})
        dg_linhas.Dock = DockStyle.Fill
        dg_linhas.Location = New Point(0, 15)
        dg_linhas.Margin = New Padding(3, 2, 3, 2)
        dg_linhas.MultiSelect = False
        dg_linhas.Name = "dg_linhas"
        dg_linhas.RowHeadersWidth = 30
        dg_linhas.RowTemplate.Height = 29
        dg_linhas.Size = New Size(730, 237)
        dg_linhas.TabIndex = 0
        ' 
        ' Label2
        ' 
        Label2.Dock = DockStyle.Top
        Label2.Location = New Point(0, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(730, 15)
        Label2.TabIndex = 1
        Label2.Text = "Header / Fixed Point"
        Label2.TextAlign = ContentAlignment.TopCenter
        ' 
        ' dg_table
        ' 
        dg_table.AllowUserToOrderColumns = True
        dg_table.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dg_table.Columns.AddRange(New DataGridViewColumn() {DataGridViewTextBoxColumn1, DataGridViewTextBoxColumn2, DataGridViewTextBoxColumn3, DataGridViewTextBoxColumn4, DataGridViewTextBoxColumn5, colType, colEixo2, col_filter, col_operacao})
        dg_table.Dock = DockStyle.Fill
        dg_table.Location = New Point(0, 15)
        dg_table.Margin = New Padding(3, 2, 3, 2)
        dg_table.MultiSelect = False
        dg_table.Name = "dg_table"
        dg_table.RowHeadersWidth = 30
        dg_table.RowTemplate.Height = 29
        dg_table.Size = New Size(730, 272)
        dg_table.TabIndex = 1
        ' 
        ' Label3
        ' 
        Label3.Dock = DockStyle.Top
        Label3.Location = New Point(0, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(730, 15)
        Label3.TabIndex = 2
        Label3.Text = "Region / Table"
        Label3.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(GroupBox6)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(3, 2)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(730, 59)
        Panel1.TabIndex = 2
        ' 
        ' GroupBox6
        ' 
        GroupBox6.Controls.Add(RadioButton8)
        GroupBox6.Controls.Add(RadioButton7)
        GroupBox6.Controls.Add(RadioButton6)
        GroupBox6.Controls.Add(RadioButton5)
        GroupBox6.Controls.Add(RadioButton4)
        GroupBox6.Dock = DockStyle.Fill
        GroupBox6.Location = New Point(0, 0)
        GroupBox6.Name = "GroupBox6"
        GroupBox6.Size = New Size(730, 59)
        GroupBox6.TabIndex = 0
        GroupBox6.TabStop = False
        GroupBox6.Text = "RMF Templates"
        ' 
        ' RadioButton8
        ' 
        RadioButton8.AutoSize = True
        RadioButton8.Location = New Point(347, 26)
        RadioButton8.Name = "RadioButton8"
        RadioButton8.Size = New Size(66, 19)
        RadioButton8.TabIndex = 4
        RadioButton8.Text = "SysSum"
        RadioButton8.UseVisualStyleBackColor = True
        ' 
        ' RadioButton7
        ' 
        RadioButton7.AutoSize = True
        RadioButton7.Location = New Point(266, 26)
        RadioButton7.Name = "RadioButton7"
        RadioButton7.Size = New Size(58, 19)
        RadioButton7.TabIndex = 3
        RadioButton7.Text = "OMVS"
        RadioButton7.UseVisualStyleBackColor = True
        ' 
        ' RadioButton6
        ' 
        RadioButton6.AutoSize = True
        RadioButton6.Location = New Point(178, 26)
        RadioButton6.Name = "RadioButton6"
        RadioButton6.Size = New Size(69, 19)
        RadioButton6.TabIndex = 2
        RadioButton6.Text = "Channel"
        RadioButton6.UseVisualStyleBackColor = True
        ' 
        ' RadioButton5
        ' 
        RadioButton5.AutoSize = True
        RadioButton5.Location = New Point(93, 26)
        RadioButton5.Name = "RadioButton5"
        RadioButton5.Size = New Size(64, 19)
        RadioButton5.TabIndex = 1
        RadioButton5.Text = "PROCU"
        RadioButton5.UseVisualStyleBackColor = True
        ' 
        ' RadioButton4
        ' 
        RadioButton4.AutoSize = True
        RadioButton4.Checked = True
        RadioButton4.Location = New Point(24, 26)
        RadioButton4.Name = "RadioButton4"
        RadioButton4.Size = New Size(48, 19)
        RadioButton4.TabIndex = 0
        RadioButton4.TabStop = True
        RadioButton4.Text = "CPC"
        RadioButton4.UseVisualStyleBackColor = True
        ' 
        ' GroupBox4
        ' 
        GroupBox4.Controls.Add(TabControl1)
        GroupBox4.Dock = DockStyle.Fill
        GroupBox4.Location = New Point(0, 0)
        GroupBox4.Margin = New Padding(3, 2, 3, 2)
        GroupBox4.Name = "GroupBox4"
        GroupBox4.Padding = New Padding(3, 2, 3, 2)
        GroupBox4.Size = New Size(852, 653)
        GroupBox4.TabIndex = 0
        GroupBox4.TabStop = False
        GroupBox4.Text = "Result"
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(TabPage2)
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Controls.Add(TabPage3)
        TabControl1.Dock = DockStyle.Fill
        TabControl1.Location = New Point(3, 18)
        TabControl1.Margin = New Padding(3, 2, 3, 2)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(846, 633)
        TabControl1.TabIndex = 4
        ' 
        ' TabPage2
        ' 
        TabPage2.Controls.Add(RichTextBox1)
        TabPage2.Controls.Add(StatusStrip2)
        TabPage2.Location = New Point(4, 24)
        TabPage2.Margin = New Padding(3, 2, 3, 2)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3, 2, 3, 2)
        TabPage2.Size = New Size(838, 605)
        TabPage2.TabIndex = 1
        TabPage2.Text = "File"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' RichTextBox1
        ' 
        RichTextBox1.BackColor = Color.Black
        RichTextBox1.ContextMenuStrip = ContextMenuStrip1
        RichTextBox1.Dock = DockStyle.Fill
        RichTextBox1.Font = New Font("Consolas", 9F)
        RichTextBox1.ForeColor = Color.Aqua
        RichTextBox1.Location = New Point(3, 2)
        RichTextBox1.Margin = New Padding(3, 2, 3, 2)
        RichTextBox1.Name = "RichTextBox1"
        RichTextBox1.Size = New Size(832, 579)
        RichTextBox1.TabIndex = 0
        RichTextBox1.Text = ""
        RichTextBox1.WordWrap = False
        ' 
        ' ContextMenuStrip1
        ' 
        ContextMenuStrip1.ImageScalingSize = New Size(20, 20)
        ContextMenuStrip1.Items.AddRange(New ToolStripItem() {SelecionarCampoToolStripMenuItem, ToolStripMenuItem1, ExtrairDaPCOMMToolStripMenuItem})
        ContextMenuStrip1.Name = "ContextMenuStrip1"
        ContextMenuStrip1.Size = New Size(189, 54)
        ' 
        ' SelecionarCampoToolStripMenuItem
        ' 
        SelecionarCampoToolStripMenuItem.Name = "SelecionarCampoToolStripMenuItem"
        SelecionarCampoToolStripMenuItem.Size = New Size(188, 22)
        SelecionarCampoToolStripMenuItem.Text = "Select Fixed Field"
        ' 
        ' ToolStripMenuItem1
        ' 
        ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        ToolStripMenuItem1.Size = New Size(185, 6)
        ' 
        ' ExtrairDaPCOMMToolStripMenuItem
        ' 
        ExtrairDaPCOMMToolStripMenuItem.Name = "ExtrairDaPCOMMToolStripMenuItem"
        ExtrairDaPCOMMToolStripMenuItem.Size = New Size(188, 22)
        ExtrairDaPCOMMToolStripMenuItem.Text = "Extract from PCOMM"
        ' 
        ' StatusStrip2
        ' 
        StatusStrip2.ImageScalingSize = New Size(20, 20)
        StatusStrip2.Items.AddRange(New ToolStripItem() {txtposi})
        StatusStrip2.Location = New Point(3, 581)
        StatusStrip2.Name = "StatusStrip2"
        StatusStrip2.Padding = New Padding(1, 0, 12, 0)
        StatusStrip2.Size = New Size(832, 22)
        StatusStrip2.TabIndex = 1
        StatusStrip2.Text = "StatusStrip2"
        ' 
        ' txtposi
        ' 
        txtposi.Name = "txtposi"
        txtposi.Size = New Size(84, 17)
        txtposi.Text = "Position: 0/0/0"
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(dg_result)
        TabPage1.Controls.Add(Button4)
        TabPage1.Controls.Add(Button2)
        TabPage1.Location = New Point(4, 24)
        TabPage1.Margin = New Padding(3, 2, 3, 2)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3, 2, 3, 2)
        TabPage1.Size = New Size(837, 610)
        TabPage1.TabIndex = 0
        TabPage1.Text = "Structured Data"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' dg_result
        ' 
        dg_result.AllowUserToOrderColumns = True
        dg_result.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dg_result.Dock = DockStyle.Fill
        dg_result.FilterAndSortEnabled = True
        dg_result.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        dg_result.Location = New Point(3, 2)
        dg_result.Margin = New Padding(3, 2, 3, 2)
        dg_result.MaxFilterButtonImageHeight = 23
        dg_result.Name = "dg_result"
        dg_result.RightToLeft = RightToLeft.No
        dg_result.RowHeadersWidth = 51
        dg_result.RowTemplate.Height = 29
        dg_result.Size = New Size(831, 606)
        dg_result.SortStringChangedInvokeBeforeDatasourceUpdate = True
        dg_result.TabIndex = 3
        ' 
        ' Button4
        ' 
        Button4.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button4.BackgroundImage = CType(resources.GetObject("Button4.BackgroundImage"), Image)
        Button4.BackgroundImageLayout = ImageLayout.Zoom
        Button4.Location = New Point(722, 553)
        Button4.Margin = New Padding(3, 2, 3, 2)
        Button4.Name = "Button4"
        Button4.Size = New Size(44, 38)
        Button4.TabIndex = 4
        ToolTip1.SetToolTip(Button4, "Remove Duplicates")
        Button4.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button2.BackgroundImage = CType(resources.GetObject("Button2.BackgroundImage"), Image)
        Button2.BackgroundImageLayout = ImageLayout.Zoom
        Button2.Location = New Point(771, 553)
        Button2.Margin = New Padding(3, 2, 3, 2)
        Button2.Name = "Button2"
        Button2.Size = New Size(44, 38)
        Button2.TabIndex = 2
        ToolTip1.SetToolTip(Button2, "Export to Excel")
        Button2.UseVisualStyleBackColor = True
        ' 
        ' TabPage3
        ' 
        TabPage3.Controls.Add(FormsPlot1)
        TabPage3.Controls.Add(GroupBox7)
        TabPage3.Location = New Point(4, 24)
        TabPage3.Margin = New Padding(3, 2, 3, 2)
        TabPage3.Name = "TabPage3"
        TabPage3.Size = New Size(837, 610)
        TabPage3.TabIndex = 2
        TabPage3.Text = "Chart"
        TabPage3.UseVisualStyleBackColor = True
        ' 
        ' FormsPlot1
        ' 
        FormsPlot1.DisplayScale = 1F
        FormsPlot1.Dock = DockStyle.Fill
        FormsPlot1.Location = New Point(0, 78)
        FormsPlot1.Margin = New Padding(3, 2, 3, 2)
        FormsPlot1.Name = "FormsPlot1"
        FormsPlot1.Size = New Size(837, 532)
        FormsPlot1.TabIndex = 0
        ' 
        ' GroupBox7
        ' 
        GroupBox7.Controls.Add(Button5)
        GroupBox7.Controls.Add(CheckBox1)
        GroupBox7.Controls.Add(TextBox3)
        GroupBox7.Controls.Add(Label7)
        GroupBox7.Controls.Add(Label5)
        GroupBox7.Controls.Add(TextBox1)
        GroupBox7.Controls.Add(TextBox2)
        GroupBox7.Controls.Add(Label6)
        GroupBox7.Dock = DockStyle.Top
        GroupBox7.Location = New Point(0, 0)
        GroupBox7.Name = "GroupBox7"
        GroupBox7.Size = New Size(837, 78)
        GroupBox7.TabIndex = 1
        GroupBox7.TabStop = False
        GroupBox7.Text = "Config"
        ' 
        ' Button5
        ' 
        Button5.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Button5.Location = New Point(740, 43)
        Button5.Margin = New Padding(3, 2, 3, 2)
        Button5.Name = "Button5"
        Button5.Size = New Size(65, 22)
        Button5.TabIndex = 5
        Button5.Text = "Plot"
        Button5.UseVisualStyleBackColor = True
        ' 
        ' CheckBox1
        ' 
        CheckBox1.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        CheckBox1.AutoSize = True
        CheckBox1.Location = New Point(298, 37)
        CheckBox1.Margin = New Padding(3, 2, 3, 2)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(101, 34)
        CheckBox1.TabIndex = 5
        CheckBox1.Text = "Show Floating" & vbCrLf & "Legend"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' TextBox3
        ' 
        TextBox3.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        TextBox3.Location = New Point(600, 43)
        TextBox3.Margin = New Padding(3, 2, 3, 2)
        TextBox3.Name = "TextBox3"
        TextBox3.Size = New Size(134, 23)
        TextBox3.TabIndex = 3
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(21, 25)
        Label7.Name = "Label7"
        Label7.Size = New Size(32, 15)
        Label7.TabIndex = 6
        Label7.Text = "Title:"
        ' 
        ' Label5
        ' 
        Label5.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Label5.AutoSize = True
        Label5.Location = New Point(600, 26)
        Label5.Name = "Label5"
        Label5.Size = New Size(75, 15)
        Label5.TabIndex = 2
        Label5.Text = "X-Axis Label:"
        ' 
        ' TextBox1
        ' 
        TextBox1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TextBox1.Location = New Point(21, 43)
        TextBox1.Margin = New Padding(3, 2, 3, 2)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(231, 23)
        TextBox1.TabIndex = 7
        ' 
        ' TextBox2
        ' 
        TextBox2.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        TextBox2.Location = New Point(432, 43)
        TextBox2.Margin = New Padding(3, 2, 3, 2)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(150, 23)
        TextBox2.TabIndex = 3
        ' 
        ' Label6
        ' 
        Label6.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Label6.AutoSize = True
        Label6.Location = New Point(432, 26)
        Label6.Name = "Label6"
        Label6.Size = New Size(75, 15)
        Label6.TabIndex = 2
        Label6.Text = "Y-Axis Label:"
        ' 
        ' DataGridViewTextBoxColumn6
        ' 
        DataGridViewTextBoxColumn6.MinimumWidth = 6
        DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        DataGridViewTextBoxColumn6.Width = 125
        ' 
        ' col_type
        ' 
        col_type.MinimumWidth = 6
        col_type.Name = "col_type"
        col_type.Width = 125
        ' 
        ' col_eixe
        ' 
        col_eixe.MinimumWidth = 6
        col_eixe.Name = "col_eixe"
        col_eixe.Width = 125
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(Label4)
        GroupBox2.Controls.Add(txtseparador)
        GroupBox2.Controls.Add(RadioButton3)
        GroupBox2.Controls.Add(Label1)
        GroupBox2.Controls.Add(txtoffset)
        GroupBox2.Controls.Add(txt_pag)
        GroupBox2.Controls.Add(btn_exec)
        GroupBox2.Controls.Add(RadioButton1)
        GroupBox2.Controls.Add(RadioButton2)
        GroupBox2.Dock = DockStyle.Top
        GroupBox2.Location = New Point(0, 68)
        GroupBox2.Margin = New Padding(3, 2, 3, 2)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Padding = New Padding(3, 2, 3, 2)
        GroupBox2.Size = New Size(1606, 62)
        GroupBox2.TabIndex = 7
        GroupBox2.TabStop = False
        GroupBox2.Text = "Page Break"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(18, 30)
        Label4.Name = "Label4"
        Label4.Size = New Size(49, 15)
        Label4.TabIndex = 11
        Label4.Text = "Anchor:"
        ' 
        ' txtseparador
        ' 
        txtseparador.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtseparador.Location = New Point(75, 28)
        txtseparador.Margin = New Padding(3, 2, 3, 2)
        txtseparador.Name = "txtseparador"
        txtseparador.Size = New Size(1435, 23)
        txtseparador.TabIndex = 10
        txtseparador.TextAlign = HorizontalAlignment.Right
        ' 
        ' RadioButton3
        ' 
        RadioButton3.AutoSize = True
        RadioButton3.Checked = True
        RadioButton3.Location = New Point(520, 28)
        RadioButton3.Margin = New Padding(3, 2, 3, 2)
        RadioButton3.Name = "RadioButton3"
        RadioButton3.Size = New Size(180, 19)
        RadioButton3.TabIndex = 9
        RadioButton3.TabStop = True
        RadioButton3.Text = "Search by Header and Region"
        RadioButton3.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(388, 30)
        Label1.Name = "Label1"
        Label1.Size = New Size(42, 15)
        Label1.TabIndex = 8
        Label1.Text = "Offset:"
        Label1.Visible = False
        ' 
        ' txtoffset
        ' 
        txtoffset.Location = New Point(442, 28)
        txtoffset.Margin = New Padding(3, 2, 3, 2)
        txtoffset.Name = "txtoffset"
        txtoffset.Size = New Size(47, 23)
        txtoffset.TabIndex = 7
        txtoffset.Text = "0"
        txtoffset.TextAlign = HorizontalAlignment.Center
        txtoffset.Visible = False
        ' 
        ' txt_pag
        ' 
        txt_pag.Location = New Point(337, 28)
        txt_pag.Margin = New Padding(3, 2, 3, 2)
        txt_pag.Name = "txt_pag"
        txt_pag.Size = New Size(47, 23)
        txt_pag.TabIndex = 6
        txt_pag.Text = "24"
        txt_pag.TextAlign = HorizontalAlignment.Center
        txt_pag.Visible = False
        ' 
        ' btn_exec
        ' 
        btn_exec.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        btn_exec.BackgroundImage = CType(resources.GetObject("btn_exec.BackgroundImage"), Image)
        btn_exec.BackgroundImageLayout = ImageLayout.Zoom
        btn_exec.Location = New Point(1533, 12)
        btn_exec.Margin = New Padding(3, 2, 3, 2)
        btn_exec.Name = "btn_exec"
        btn_exec.Size = New Size(58, 41)
        btn_exec.TabIndex = 0
        ToolTip1.SetToolTip(btn_exec, "Extract")
        btn_exec.UseVisualStyleBackColor = False
        ' 
        ' RadioButton1
        ' 
        RadioButton1.AutoSize = True
        RadioButton1.Location = New Point(31, 28)
        RadioButton1.Margin = New Padding(3, 2, 3, 2)
        RadioButton1.Name = "RadioButton1"
        RadioButton1.Size = New Size(105, 19)
        RadioButton1.TabIndex = 5
        RadioButton1.Text = "Search by Page"
        RadioButton1.UseVisualStyleBackColor = True
        RadioButton1.Visible = False
        ' 
        ' RadioButton2
        ' 
        RadioButton2.AutoSize = True
        RadioButton2.Location = New Point(167, 28)
        RadioButton2.Margin = New Padding(3, 2, 3, 2)
        RadioButton2.Name = "RadioButton2"
        RadioButton2.Size = New Size(105, 19)
        RadioButton2.TabIndex = 5
        RadioButton2.Text = "Fixed Page Size"
        RadioButton2.UseVisualStyleBackColor = True
        RadioButton2.Visible = False
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(Button3)
        GroupBox1.Controls.Add(txt_entrada)
        GroupBox1.Controls.Add(Button1)
        GroupBox1.Dock = DockStyle.Top
        GroupBox1.Location = New Point(0, 0)
        GroupBox1.Margin = New Padding(3, 2, 3, 2)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(3, 2, 3, 2)
        GroupBox1.Size = New Size(1606, 68)
        GroupBox1.TabIndex = 6
        GroupBox1.TabStop = False
        GroupBox1.Text = "Open File"
        ' 
        ' Button3
        ' 
        Button3.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        Button3.BackgroundImage = CType(resources.GetObject("Button3.BackgroundImage"), Image)
        Button3.BackgroundImageLayout = ImageLayout.Zoom
        Button3.Location = New Point(1533, 20)
        Button3.Margin = New Padding(3, 2, 3, 2)
        Button3.Name = "Button3"
        Button3.Size = New Size(58, 41)
        Button3.TabIndex = 2
        ToolTip1.SetToolTip(Button3, "Edit File")
        Button3.UseVisualStyleBackColor = True
        ' 
        ' txt_entrada
        ' 
        txt_entrada.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txt_entrada.Location = New Point(21, 30)
        txt_entrada.Margin = New Padding(3, 2, 3, 2)
        txt_entrada.Name = "txt_entrada"
        txt_entrada.Size = New Size(1446, 23)
        txt_entrada.TabIndex = 0
        ' 
        ' Button1
        ' 
        Button1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        Button1.BackgroundImage = CType(resources.GetObject("Button1.BackgroundImage"), Image)
        Button1.BackgroundImageLayout = ImageLayout.Zoom
        Button1.Location = New Point(1472, 20)
        Button1.Margin = New Padding(3, 2, 3, 2)
        Button1.Name = "Button1"
        Button1.Size = New Size(58, 41)
        Button1.TabIndex = 1
        ToolTip1.SetToolTip(Button1, "Open RMF TXT file")
        Button1.UseVisualStyleBackColor = True
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.ImageScalingSize = New Size(20, 20)
        StatusStrip1.Items.AddRange(New ToolStripItem() {ToolStripProgressBar1, lbl_status, ToolStripStatusLabel1, lbl_paglida, ToolStripStatusLabel2, lblresult, ToolStripStatusLabel4, txttime, ToolStripStatusLabel3})
        StatusStrip1.Location = New Point(0, 783)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Padding = New Padding(1, 0, 12, 0)
        StatusStrip1.Size = New Size(1606, 25)
        StatusStrip1.TabIndex = 9
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' ToolStripProgressBar1
        ' 
        ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        ToolStripProgressBar1.Size = New Size(88, 19)
        ' 
        ' lbl_status
        ' 
        lbl_status.Name = "lbl_status"
        lbl_status.Size = New Size(29, 20)
        lbl_status.Text = "N/A"
        ' 
        ' ToolStripStatusLabel1
        ' 
        ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        ToolStripStatusLabel1.Size = New Size(83, 20)
        ToolStripStatusLabel1.Text = "Intervals Read:"
        ' 
        ' lbl_paglida
        ' 
        lbl_paglida.Name = "lbl_paglida"
        lbl_paglida.Size = New Size(13, 20)
        lbl_paglida.Text = "0"
        ' 
        ' ToolStripStatusLabel2
        ' 
        ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        ToolStripStatusLabel2.Size = New Size(73, 20)
        ToolStripStatusLabel2.Text = "Result Rows:"
        ' 
        ' lblresult
        ' 
        lblresult.Name = "lblresult"
        lblresult.Size = New Size(13, 20)
        lblresult.Text = "0"
        ' 
        ' ToolStripStatusLabel4
        ' 
        ToolStripStatusLabel4.Name = "ToolStripStatusLabel4"
        ToolStripStatusLabel4.Size = New Size(77, 20)
        ToolStripStatusLabel4.Text = "Next Refresh:"
        ' 
        ' txttime
        ' 
        txttime.Image = CType(resources.GetObject("txttime.Image"), Image)
        txttime.ImageAlign = ContentAlignment.MiddleRight
        txttime.Name = "txttime"
        txttime.RightToLeft = RightToLeft.Yes
        txttime.Size = New Size(33, 20)
        txttime.Text = "0"
        ' 
        ' ToolStripStatusLabel3
        ' 
        ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        ToolStripStatusLabel3.Size = New Size(1182, 20)
        ToolStripStatusLabel3.Spring = True
        ToolStripStatusLabel3.Text = "Developed by Matheus Porsch"
        ToolStripStatusLabel3.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' OpenFileDialog1
        ' 
        OpenFileDialog1.DefaultExt = "txt"
        OpenFileDialog1.FileName = "RMFPP.txt"
        OpenFileDialog1.Title = "Select input file (.txt)..."
        ' 
        ' BackgroundWorker1
        ' 
        BackgroundWorker1.WorkerReportsProgress = True
        BackgroundWorker1.WorkerSupportsCancellation = True
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.ImageScalingSize = New Size(20, 20)
        MenuStrip1.Location = New Point(0, 130)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Padding = New Padding(5, 2, 0, 2)
        MenuStrip1.RenderMode = ToolStripRenderMode.Professional
        MenuStrip1.Size = New Size(1606, 24)
        MenuStrip1.TabIndex = 10
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' BackgroundWorker2
        ' 
        BackgroundWorker2.WorkerReportsProgress = True
        BackgroundWorker2.WorkerSupportsCancellation = True
        ' 
        ' BackgroundWorker3
        ' 
        BackgroundWorker3.WorkerReportsProgress = True
        BackgroundWorker3.WorkerSupportsCancellation = True
        ' 
        ' BackgroundWorker4
        ' 
        BackgroundWorker4.WorkerReportsProgress = True
        BackgroundWorker4.WorkerSupportsCancellation = True
        ' 
        ' Timer1
        ' 
        Timer1.Interval = 3000
        ' 
        ' Timer2
        ' 
        Timer2.Enabled = True
        Timer2.Interval = 1000
        ' 
        ' Col_Nome
        ' 
        Col_Nome.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Col_Nome.HeaderText = "Field Name"
        Col_Nome.MinimumWidth = 6
        Col_Nome.Name = "Col_Nome"
        ' 
        ' Col_NLinha
        ' 
        Col_NLinha.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Col_NLinha.HeaderText = "Line No."
        Col_NLinha.MinimumWidth = 6
        Col_NLinha.Name = "Col_NLinha"
        ' 
        ' Col_PosiInicio
        ' 
        Col_PosiInicio.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Col_PosiInicio.HeaderText = "Position"
        Col_PosiInicio.MinimumWidth = 6
        Col_PosiInicio.Name = "Col_PosiInicio"
        ' 
        ' Col_QTDChar
        ' 
        Col_QTDChar.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Col_QTDChar.HeaderText = "Field Size"
        Col_QTDChar.MinimumWidth = 6
        Col_QTDChar.Name = "Col_QTDChar"
        ' 
        ' Col_Page
        ' 
        Col_Page.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Col_Page.HeaderText = "Page No."
        Col_Page.MinimumWidth = 6
        Col_Page.Name = "Col_Page"
        ' 
        ' colTipo
        ' 
        colTipo.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        colTipo.HeaderText = "Type"
        colTipo.Items.AddRange(New Object() {"DateTime", "Date", "Time", "Decimal", "Text"})
        colTipo.MinimumWidth = 6
        colTipo.Name = "colTipo"
        ' 
        ' ColEixo
        ' 
        ColEixo.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        ColEixo.HeaderText = "Axis"
        ColEixo.Items.AddRange(New Object() {"Not Applied", "X", "Y", "Y Categorized", "Y Fixed Line (Max)", "X Shade"})
        ColEixo.MinimumWidth = 6
        ColEixo.Name = "ColEixo"
        ' 
        ' col_operacao1
        ' 
        col_operacao1.HeaderText = "Operation"
        col_operacao1.Items.AddRange(New Object() {"No operation", "Group By"})
        col_operacao1.MinimumWidth = 6
        col_operacao1.Name = "col_operacao1"
        col_operacao1.Width = 125
        ' 
        ' DataGridViewTextBoxColumn1
        ' 
        DataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DataGridViewTextBoxColumn1.HeaderText = "Column Name"
        DataGridViewTextBoxColumn1.MinimumWidth = 6
        DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        ' 
        ' DataGridViewTextBoxColumn2
        ' 
        DataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DataGridViewTextBoxColumn2.HeaderText = "Start Line"
        DataGridViewTextBoxColumn2.MinimumWidth = 6
        DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        ' 
        ' DataGridViewTextBoxColumn3
        ' 
        DataGridViewTextBoxColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DataGridViewTextBoxColumn3.HeaderText = "End Line (Int/Char)"
        DataGridViewTextBoxColumn3.MinimumWidth = 6
        DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        ' 
        ' DataGridViewTextBoxColumn4
        ' 
        DataGridViewTextBoxColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DataGridViewTextBoxColumn4.HeaderText = "Position"
        DataGridViewTextBoxColumn4.MinimumWidth = 6
        DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        ' 
        ' DataGridViewTextBoxColumn5
        ' 
        DataGridViewTextBoxColumn5.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DataGridViewTextBoxColumn5.HeaderText = "Field Size"
        DataGridViewTextBoxColumn5.MinimumWidth = 6
        DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        ' 
        ' colType
        ' 
        colType.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        colType.HeaderText = "Type"
        colType.Items.AddRange(New Object() {"Decimal", "Text"})
        colType.MinimumWidth = 6
        colType.Name = "colType"
        ' 
        ' colEixo2
        ' 
        colEixo2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        colEixo2.HeaderText = "Axis"
        colEixo2.Items.AddRange(New Object() {"Not Applied", "X", "Y", "Y Categorized", "Y Fixed Line (Max)", "X Shade"})
        colEixo2.MinimumWidth = 6
        colEixo2.Name = "colEixo2"
        ' 
        ' col_filter
        ' 
        col_filter.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        col_filter.HeaderText = "Filter (;)"
        col_filter.MinimumWidth = 6
        col_filter.Name = "col_filter"
        ' 
        ' col_operacao
        ' 
        col_operacao.HeaderText = "Operation"
        col_operacao.Items.AddRange(New Object() {"No operation", "Group By", "Sum"})
        col_operacao.MinimumWidth = 6
        col_operacao.Name = "col_operacao"
        col_operacao.Width = 125
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1606, 808)
        Controls.Add(MenuStrip1)
        Controls.Add(SplitContainer1)
        Controls.Add(GroupBox2)
        Controls.Add(GroupBox1)
        Controls.Add(StatusStrip1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(3, 2, 3, 2)
        Name = "Form1"
        Text = "RMF Extractor - PCM Tool"
        SplitContainer1.Panel1.ResumeLayout(False)
        SplitContainer1.Panel2.ResumeLayout(False)
        CType(SplitContainer1, ComponentModel.ISupportInitialize).EndInit()
        SplitContainer1.ResumeLayout(False)
        GroupBox3.ResumeLayout(False)
        TabControl2.ResumeLayout(False)
        TabPage5.ResumeLayout(False)
        SplitContainer2.Panel1.ResumeLayout(False)
        SplitContainer2.Panel2.ResumeLayout(False)
        CType(SplitContainer2, ComponentModel.ISupportInitialize).EndInit()
        SplitContainer2.ResumeLayout(False)
        CType(dg_linhas, ComponentModel.ISupportInitialize).EndInit()
        CType(dg_table, ComponentModel.ISupportInitialize).EndInit()
        Panel1.ResumeLayout(False)
        GroupBox6.ResumeLayout(False)
        GroupBox6.PerformLayout()
        GroupBox4.ResumeLayout(False)
        TabControl1.ResumeLayout(False)
        TabPage2.ResumeLayout(False)
        TabPage2.PerformLayout()
        ContextMenuStrip1.ResumeLayout(False)
        StatusStrip2.ResumeLayout(False)
        StatusStrip2.PerformLayout()
        TabPage1.ResumeLayout(False)
        CType(dg_result, ComponentModel.ISupportInitialize).EndInit()
        TabPage3.ResumeLayout(False)
        GroupBox7.ResumeLayout(False)
        GroupBox7.PerformLayout()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents dg_linhas As DataGridView
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents txt_pag As TextBox
    Friend WithEvents Button2 As Button
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txt_entrada As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripProgressBar1 As ToolStripProgressBar
    Friend WithEvents lbl_status As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents lbl_paglida As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As ToolStripStatusLabel
    Friend WithEvents lblresult As ToolStripStatusLabel
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents ToolStripStatusLabel3 As ToolStripStatusLabel
    Friend WithEvents dg_result As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents Button3 As Button
    Friend WithEvents txtoffset As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents RadioButton3 As RadioButton
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents Label2 As Label
    Friend WithEvents dg_table As DataGridView
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txtseparador As TextBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents SelecionarCampoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StatusStrip2 As StatusStrip
    Friend WithEvents txtposi As ToolStripStatusLabel
    Friend WithEvents FormsPlot1 As ScottPlot.WinForms.FormsPlot
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TabControl2 As TabControl
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents Button4 As Button
    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Button5 As Button
    Friend WithEvents BackgroundWorker3 As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundWorker4 As System.ComponentModel.BackgroundWorker
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents ExtrairDaPCOMMToolStripMenuItem As ToolStripMenuItem
    Public WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Public WithEvents btn_exec As Button
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Timer2 As Timer
    Friend WithEvents ToolStripStatusLabel4 As ToolStripStatusLabel
    Friend WithEvents txttime As ToolStripStatusLabel
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
    Friend WithEvents col_type As DataGridViewComboBoxColumn
    Friend WithEvents col_eixe As DataGridViewComboBoxColumn
    Friend WithEvents Panel1 As Panel
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents RadioButton5 As RadioButton
    Friend WithEvents RadioButton4 As RadioButton
    Friend WithEvents RadioButton6 As RadioButton
    Friend WithEvents RadioButton8 As RadioButton
    Friend WithEvents RadioButton7 As RadioButton
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents Col_Nome As DataGridViewTextBoxColumn
    Friend WithEvents Col_NLinha As DataGridViewTextBoxColumn
    Friend WithEvents Col_PosiInicio As DataGridViewTextBoxColumn
    Friend WithEvents Col_QTDChar As DataGridViewTextBoxColumn
    Friend WithEvents Col_Page As DataGridViewTextBoxColumn
    Friend WithEvents colTipo As DataGridViewComboBoxColumn
    Friend WithEvents ColEixo As DataGridViewComboBoxColumn
    Friend WithEvents col_operacao1 As DataGridViewComboBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents colType As DataGridViewComboBoxColumn
    Friend WithEvents colEixo2 As DataGridViewComboBoxColumn
    Friend WithEvents col_filter As DataGridViewTextBoxColumn
    Friend WithEvents col_operacao As DataGridViewComboBoxColumn
End Class
