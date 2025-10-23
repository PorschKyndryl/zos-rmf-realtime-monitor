Public Class frm_config_chartvb
    Private Sub frm_config_chartvb_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        dg_colunas.Rows.Clear()

        For i As Integer = 0 To (Form1.dg_linhas.RowCount - 2)
            dg_colunas.Rows.Add(Form1.dg_linhas.Rows(i).Cells(0).Value, "Inteiro", "Não Aplicado")
        Next

        For i As Integer = 0 To (Form1.dg_linhas.RowCount - 2)
            dg_colunas.Rows.Add(Form1.dg_table.Rows(i).Cells(0).Value, "Decimal", "Não Aplicado")
        Next



    End Sub

    Private Sub dg_colunas_SelectionChanged(sender As Object, e As EventArgs) Handles dg_colunas.SelectionChanged, dg_colunas.CellLeave

        For i As Integer = 0 To (dg_colunas.RowCount - 1)
            If dg_colunas.Rows(i).Cells(2).Value = "X" Then
                TextBox3.Text = dg_colunas.Rows(i).Cells(0).Value
                Exit For
            End If
        Next

        For i As Integer = 0 To (dg_colunas.RowCount - 1)
            If dg_colunas.Rows(i).Cells(2).Value = "Y" Then
                TextBox2.Text = dg_colunas.Rows(i).Cells(0).Value
                Exit For
            End If
        Next

    End Sub

    Private Sub frm_config_chartvb_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        Me.DialogResult = DialogResult.Cancel

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        Me.DialogResult = DialogResult.Yes
    End Sub
End Class