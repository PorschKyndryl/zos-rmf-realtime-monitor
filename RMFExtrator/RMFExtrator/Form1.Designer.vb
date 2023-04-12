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
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(Form1))
        SplitContainer1 = New SplitContainer()
        GroupBox3 = New GroupBox()
        dg_linhas = New DataGridView()
        Col_Nome = New DataGridViewTextBoxColumn()
        Col_NLinha = New DataGridViewTextBoxColumn()
        Col_PosiInicio = New DataGridViewTextBoxColumn()
        Col_QTDChar = New DataGridViewTextBoxColumn()
        Col_Page = New DataGridViewTextBoxColumn()
        GroupBox4 = New GroupBox()
        dg_result = New Zuby.ADGV.AdvancedDataGridView()
        Button2 = New Button()
        GroupBox2 = New GroupBox()
        txt_pag = New TextBox()
        btn_exec = New Button()
        RadioButton1 = New RadioButton()
        RadioButton2 = New RadioButton()
        GroupBox1 = New GroupBox()
        txt_entrada = New TextBox()
        Button1 = New Button()
        StatusStrip1 = New StatusStrip()
        ToolStripProgressBar1 = New ToolStripProgressBar()
        lbl_status = New ToolStripStatusLabel()
        ToolStripStatusLabel1 = New ToolStripStatusLabel()
        lbl_paglida = New ToolStripStatusLabel()
        ToolStripStatusLabel2 = New ToolStripStatusLabel()
        lblresult = New ToolStripStatusLabel()
        ToolStripStatusLabel3 = New ToolStripStatusLabel()
        OpenFileDialog1 = New OpenFileDialog()
        ToolTip1 = New ToolTip(components)
        CType(SplitContainer1, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer1.Panel1.SuspendLayout()
        SplitContainer1.Panel2.SuspendLayout()
        SplitContainer1.SuspendLayout()
        GroupBox3.SuspendLayout()
        CType(dg_linhas, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox4.SuspendLayout()
        CType(dg_result, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox2.SuspendLayout()
        GroupBox1.SuspendLayout()
        StatusStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' SplitContainer1
        ' 
        SplitContainer1.Dock = DockStyle.Fill
        SplitContainer1.Location = New Point(0, 173)
        SplitContainer1.Name = "SplitContainer1"
        ' 
        ' SplitContainer1.Panel1
        ' 
        SplitContainer1.Panel1.Controls.Add(GroupBox3)
        ' 
        ' SplitContainer1.Panel2
        ' 
        SplitContainer1.Panel2.Controls.Add(GroupBox4)
        SplitContainer1.Size = New Size(1345, 586)
        SplitContainer1.SplitterDistance = 492
        SplitContainer1.TabIndex = 8
        ' 
        ' GroupBox3
        ' 
        GroupBox3.Controls.Add(dg_linhas)
        GroupBox3.Dock = DockStyle.Fill
        GroupBox3.Location = New Point(0, 0)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Size = New Size(492, 586)
        GroupBox3.TabIndex = 0
        GroupBox3.TabStop = False
        GroupBox3.Text = "Campos"
        ' 
        ' dg_linhas
        ' 
        dg_linhas.AllowUserToOrderColumns = True
        dg_linhas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dg_linhas.Columns.AddRange(New DataGridViewColumn() {Col_Nome, Col_NLinha, Col_PosiInicio, Col_QTDChar, Col_Page})
        dg_linhas.Dock = DockStyle.Fill
        dg_linhas.Location = New Point(3, 23)
        dg_linhas.MultiSelect = False
        dg_linhas.Name = "dg_linhas"
        dg_linhas.RowHeadersWidth = 30
        dg_linhas.RowTemplate.Height = 29
        dg_linhas.Size = New Size(486, 560)
        dg_linhas.TabIndex = 0
        ' 
        ' Col_Nome
        ' 
        Col_Nome.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Col_Nome.HeaderText = "Nome do Campo"
        Col_Nome.MinimumWidth = 6
        Col_Nome.Name = "Col_Nome"
        ' 
        ' Col_NLinha
        ' 
        Col_NLinha.HeaderText = "N° Linha"
        Col_NLinha.MinimumWidth = 6
        Col_NLinha.Name = "Col_NLinha"
        Col_NLinha.Width = 80
        ' 
        ' Col_PosiInicio
        ' 
        Col_PosiInicio.HeaderText = "Posição"
        Col_PosiInicio.MinimumWidth = 6
        Col_PosiInicio.Name = "Col_PosiInicio"
        Col_PosiInicio.Width = 80
        ' 
        ' Col_QTDChar
        ' 
        Col_QTDChar.HeaderText = "Tam. Campo"
        Col_QTDChar.MinimumWidth = 6
        Col_QTDChar.Name = "Col_QTDChar"
        Col_QTDChar.Width = 80
        ' 
        ' Col_Page
        ' 
        Col_Page.HeaderText = "N° da Page"
        Col_Page.MinimumWidth = 6
        Col_Page.Name = "Col_Page"
        Col_Page.Width = 80
        ' 
        ' GroupBox4
        ' 
        GroupBox4.Controls.Add(Button2)
        GroupBox4.Controls.Add(dg_result)
        GroupBox4.Dock = DockStyle.Fill
        GroupBox4.Location = New Point(0, 0)
        GroupBox4.Name = "GroupBox4"
        GroupBox4.Size = New Size(849, 586)
        GroupBox4.TabIndex = 0
        GroupBox4.TabStop = False
        GroupBox4.Text = "Resultado"
        ' 
        ' dg_result
        ' 
        dg_result.AllowUserToOrderColumns = True
        dg_result.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dg_result.Dock = DockStyle.Fill
        dg_result.FilterAndSortEnabled = True
        dg_result.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        dg_result.Location = New Point(3, 23)
        dg_result.Name = "dg_result"
        dg_result.RightToLeft = RightToLeft.No
        dg_result.RowHeadersWidth = 51
        dg_result.RowTemplate.Height = 29
        dg_result.Size = New Size(843, 560)
        dg_result.SortStringChangedInvokeBeforeDatasourceUpdate = True
        dg_result.TabIndex = 3
        ' 
        ' Button2
        ' 
        Button2.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button2.BackgroundImage = CType(resources.GetObject("Button2.BackgroundImage"), Image)
        Button2.BackgroundImageLayout = ImageLayout.Zoom
        Button2.Location = New Point(792, 505)
        Button2.Name = "Button2"
        Button2.Size = New Size(50, 50)
        Button2.TabIndex = 2
        ToolTip1.SetToolTip(Button2, "Exportar para Excel")
        Button2.UseVisualStyleBackColor = True
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(txt_pag)
        GroupBox2.Controls.Add(btn_exec)
        GroupBox2.Controls.Add(RadioButton1)
        GroupBox2.Controls.Add(RadioButton2)
        GroupBox2.Dock = DockStyle.Top
        GroupBox2.Location = New Point(0, 91)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(1345, 82)
        GroupBox2.TabIndex = 7
        GroupBox2.TabStop = False
        GroupBox2.Text = "Definicação de Página - Quebra de Página"
        ' 
        ' txt_pag
        ' 
        txt_pag.Enabled = False
        txt_pag.Location = New Point(385, 37)
        txt_pag.Name = "txt_pag"
        txt_pag.Size = New Size(53, 27)
        txt_pag.TabIndex = 6
        txt_pag.Text = "113"
        txt_pag.TextAlign = HorizontalAlignment.Center
        ' 
        ' btn_exec
        ' 
        btn_exec.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        btn_exec.BackgroundImage = CType(resources.GetObject("btn_exec.BackgroundImage"), Image)
        btn_exec.BackgroundImageLayout = ImageLayout.Zoom
        btn_exec.Location = New Point(1261, 16)
        btn_exec.Name = "btn_exec"
        btn_exec.Size = New Size(66, 55)
        btn_exec.TabIndex = 0
        ToolTip1.SetToolTip(btn_exec, "Extrair")
        btn_exec.UseVisualStyleBackColor = False
        ' 
        ' RadioButton1
        ' 
        RadioButton1.AutoSize = True
        RadioButton1.Checked = True
        RadioButton1.Location = New Point(35, 38)
        RadioButton1.Name = "RadioButton1"
        RadioButton1.Size = New Size(148, 24)
        RadioButton1.TabIndex = 5
        RadioButton1.TabStop = True
        RadioButton1.Text = "Buscar por Página"
        RadioButton1.UseVisualStyleBackColor = True
        ' 
        ' RadioButton2
        ' 
        RadioButton2.AutoSize = True
        RadioButton2.Enabled = False
        RadioButton2.Location = New Point(189, 38)
        RadioButton2.Name = "RadioButton2"
        RadioButton2.Size = New Size(190, 24)
        RadioButton2.TabIndex = 5
        RadioButton2.Text = "Tamanho Fixo de Página"
        RadioButton2.UseVisualStyleBackColor = True
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(txt_entrada)
        GroupBox1.Controls.Add(Button1)
        GroupBox1.Dock = DockStyle.Top
        GroupBox1.Location = New Point(0, 0)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(1345, 91)
        GroupBox1.TabIndex = 6
        GroupBox1.TabStop = False
        GroupBox1.Text = "Abrir Arquivo"
        ' 
        ' txt_entrada
        ' 
        txt_entrada.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txt_entrada.Location = New Point(24, 40)
        txt_entrada.Name = "txt_entrada"
        txt_entrada.Size = New Size(1218, 27)
        txt_entrada.TabIndex = 0
        ' 
        ' Button1
        ' 
        Button1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        Button1.BackgroundImage = CType(resources.GetObject("Button1.BackgroundImage"), Image)
        Button1.BackgroundImageLayout = ImageLayout.Zoom
        Button1.Location = New Point(1261, 26)
        Button1.Name = "Button1"
        Button1.Size = New Size(66, 55)
        Button1.TabIndex = 1
        ToolTip1.SetToolTip(Button1, "Abrir arquivo RMF TXT")
        Button1.UseVisualStyleBackColor = True
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.ImageScalingSize = New Size(20, 20)
        StatusStrip1.Items.AddRange(New ToolStripItem() {ToolStripProgressBar1, lbl_status, ToolStripStatusLabel1, lbl_paglida, ToolStripStatusLabel2, lblresult, ToolStripStatusLabel3})
        StatusStrip1.Location = New Point(0, 733)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Size = New Size(1345, 26)
        StatusStrip1.TabIndex = 9
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' ToolStripProgressBar1
        ' 
        ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        ToolStripProgressBar1.Size = New Size(100, 18)
        ' 
        ' lbl_status
        ' 
        lbl_status.Name = "lbl_status"
        lbl_status.Size = New Size(36, 20)
        lbl_status.Text = "N/A"
        ' 
        ' ToolStripStatusLabel1
        ' 
        ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        ToolStripStatusLabel1.Size = New Size(203, 20)
        ToolStripStatusLabel1.Text = "Quantidade de Paginas Lidas:"
        ' 
        ' lbl_paglida
        ' 
        lbl_paglida.Name = "lbl_paglida"
        lbl_paglida.Size = New Size(17, 20)
        lbl_paglida.Text = "0"
        ' 
        ' ToolStripStatusLabel2
        ' 
        ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        ToolStripStatusLabel2.Size = New Size(173, 20)
        ToolStripStatusLabel2.Text = "Qtd de Linhas Resultado:"
        ' 
        ' lblresult
        ' 
        lblresult.Name = "lblresult"
        lblresult.Size = New Size(17, 20)
        lblresult.Text = "0"
        ' 
        ' ToolStripStatusLabel3
        ' 
        ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        ToolStripStatusLabel3.Size = New Size(782, 20)
        ToolStripStatusLabel3.Spring = True
        ToolStripStatusLabel3.Text = "Matheus Porsch"
        ToolStripStatusLabel3.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' OpenFileDialog1
        ' 
        OpenFileDialog1.DefaultExt = "txt"
        OpenFileDialog1.FileName = "RMFPP.txt"
        OpenFileDialog1.Title = "Selecione o Arquivo de entrada (.txt)..."
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1345, 759)
        Controls.Add(StatusStrip1)
        Controls.Add(SplitContainer1)
        Controls.Add(GroupBox2)
        Controls.Add(GroupBox1)
        Name = "Form1"
        Text = "ExtratorRMF"
        SplitContainer1.Panel1.ResumeLayout(False)
        SplitContainer1.Panel2.ResumeLayout(False)
        CType(SplitContainer1, ComponentModel.ISupportInitialize).EndInit()
        SplitContainer1.ResumeLayout(False)
        GroupBox3.ResumeLayout(False)
        CType(dg_linhas, ComponentModel.ISupportInitialize).EndInit()
        GroupBox4.ResumeLayout(False)
        CType(dg_result, ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents Col_Nome As DataGridViewTextBoxColumn
    Friend WithEvents Col_NLinha As DataGridViewTextBoxColumn
    Friend WithEvents Col_PosiInicio As DataGridViewTextBoxColumn
    Friend WithEvents Col_QTDChar As DataGridViewTextBoxColumn
    Friend WithEvents Col_Page As DataGridViewTextBoxColumn
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents txt_pag As TextBox
    Friend WithEvents Button2 As Button
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txt_entrada As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents btn_exec As Button
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
End Class
