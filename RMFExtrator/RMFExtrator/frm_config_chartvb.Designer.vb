<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_config_chartvb
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Label1 = New Label()
        TextBox1 = New TextBox()
        TextBox2 = New TextBox()
        Label2 = New Label()
        GroupBox1 = New GroupBox()
        TextBox3 = New TextBox()
        dg_colunas = New DataGridView()
        Col_Nome = New DataGridViewTextBoxColumn()
        col_type = New DataGridViewComboBoxColumn()
        col_eixe = New DataGridViewComboBoxColumn()
        Label3 = New Label()
        CheckBox1 = New CheckBox()
        Button1 = New Button()
        GroupBox1.SuspendLayout()
        CType(dg_colunas, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 23)
        Label1.Name = "Label1"
        Label1.Size = New Size(50, 20)
        Label1.TabIndex = 0
        Label1.Text = "Título:"
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(12, 46)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(499, 27)
        TextBox1.TabIndex = 1
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(22, 72)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(312, 27)
        TextBox2.TabIndex = 3
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(22, 49)
        Label2.Name = "Label2"
        Label2.Size = New Size(113, 20)
        Label2.TabIndex = 2
        Label2.Text = "Legenda Eixo Y:"
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(TextBox3)
        GroupBox1.Controls.Add(dg_colunas)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(TextBox2)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Location = New Point(12, 95)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(678, 404)
        GroupBox1.TabIndex = 4
        GroupBox1.TabStop = False
        GroupBox1.Text = "Eixo"
        ' 
        ' TextBox3
        ' 
        TextBox3.Location = New Point(346, 72)
        TextBox3.Name = "TextBox3"
        TextBox3.Size = New Size(312, 27)
        TextBox3.TabIndex = 3
        ' 
        ' dg_colunas
        ' 
        dg_colunas.AllowUserToAddRows = False
        dg_colunas.AllowUserToDeleteRows = False
        dg_colunas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dg_colunas.Columns.AddRange(New DataGridViewColumn() {Col_Nome, col_type, col_eixe})
        dg_colunas.Dock = DockStyle.Bottom
        dg_colunas.Location = New Point(3, 137)
        dg_colunas.MultiSelect = False
        dg_colunas.Name = "dg_colunas"
        dg_colunas.RowHeadersWidth = 30
        dg_colunas.RowTemplate.Height = 29
        dg_colunas.Size = New Size(672, 264)
        dg_colunas.TabIndex = 4
        ' 
        ' Col_Nome
        ' 
        Col_Nome.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Col_Nome.HeaderText = "Nome do Campo"
        Col_Nome.MinimumWidth = 6
        Col_Nome.Name = "Col_Nome"
        Col_Nome.ReadOnly = True
        ' 
        ' col_type
        ' 
        col_type.HeaderText = "Tipo"
        col_type.Items.AddRange(New Object() {"Data", "Hora", "DataHora", "Inteiro", "Decimal", "Texto"})
        col_type.MinimumWidth = 6
        col_type.Name = "col_type"
        col_type.Width = 125
        ' 
        ' col_eixe
        ' 
        col_eixe.HeaderText = "Eixo"
        col_eixe.Items.AddRange(New Object() {"X", "Y", "Y Categorizado", "Não Aplicado"})
        col_eixe.MinimumWidth = 6
        col_eixe.Name = "col_eixe"
        col_eixe.Width = 125
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(346, 49)
        Label3.Name = "Label3"
        Label3.Size = New Size(114, 20)
        Label3.TabIndex = 2
        Label3.Text = "Legenda Eixo X:"
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Location = New Point(530, 38)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(160, 44)
        CheckBox1.TabIndex = 0
        CheckBox1.Text = "Adicionar Legenda " & vbCrLf & "Suspensa"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(593, 515)
        Button1.Name = "Button1"
        Button1.Size = New Size(94, 29)
        Button1.TabIndex = 5
        Button1.Text = "OK"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' frm_config_chartvb
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(702, 558)
        Controls.Add(Button1)
        Controls.Add(CheckBox1)
        Controls.Add(GroupBox1)
        Controls.Add(TextBox1)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "frm_config_chartvb"
        Text = "Config. Gráfico"
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        CType(dg_colunas, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents dg_colunas As DataGridView
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents Col_Nome As DataGridViewTextBoxColumn
    Friend WithEvents col_type As DataGridViewComboBoxColumn
    Friend WithEvents col_eixe As DataGridViewComboBoxColumn
    Friend WithEvents Button1 As Button
End Class
