Imports System.Runtime.CompilerServices
Imports System.Threading
Imports System.IO
Imports Zuby.ADGV
Imports Microsoft.Office.Interop.Excel
Imports Zuby

Public Class Form1

    Private XcelApp As New Microsoft.Office.Interop.Excel.Application

    Dim globaltable As New System.Data.DataTable
    Dim bsglobal As New BindingSource
    Private Sub btn_exec_Click(sender As Object, e As EventArgs) Handles btn_exec.Click
        'Try

        If Me.txt_entrada.Text = "" Or Me.txt_pag.Text = "" Or Me.txt_pag.Text = "0" Or Not IsNumeric(Me.txt_pag.Text) Then
            MessageBox.Show("O campo de entrada, saída e N° de Linhas na página precisam estar preenchidos.", "Campo em branco", MessageBoxButtons.OK)
            Me.txt_entrada.Focus()
        ElseIf Me.dg_linhas.RowCount = 0 Then
            MessageBox.Show("É necessárioa adicionar as linhas na grid dos campos a serem procurados.", "Linhas em branco", MessageBoxButtons.OK)
            Me.dg_linhas.Focus()
        ElseIf File.Exists(txt_entrada.Text) = False Then
            MessageBox.Show("O arquivo que está tentando abrir não existe.", "Linhas em branco", MessageBoxButtons.OK)
        Else
            dg_result.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            'LIMPA TABLE E O GRID
            Me.dg_result.Rows.Clear()
            globaltable.Rows.Clear()
            Dim num1 = 0
            Dim columnCount As Integer = Me.dg_result.ColumnCount
            While columnCount > num1
                Me.dg_result.Columns.RemoveAt(0)
                Interlocked.Increment(num1)
            End While
            num1 = 0
            Dim columnCount2 As Integer = globaltable.Columns.Count
            While columnCount > num1
                globaltable.Columns.RemoveAt(0)
                Interlocked.Increment(num1)
            End While

            Dim index1 = 0
            Dim num2 As Integer = Me.dg_linhas.RowCount - 1
            While num2 > index1
                '>>>>Me.dg_result.Columns.Add("Col_" & index1.ToString(), (Me.dg_linhas.Rows(index1).Cells(0).Value.ToString()))
                globaltable.Columns.Add(Me.dg_linhas.Rows(index1).Cells(0).Value.ToString())
                '>>>>Me.dg_result.Columns(index1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                Interlocked.Increment(index1)
            End While
            Me.dg_result.Refresh()
            Me.dg_result.RefreshEdit()
            If Me.RadioButton2.Checked Then

                Dim index2 = 0
                Dim num3 = 0
                While Me.dg_linhas.RowCount - 1 > index2
                    Dim [integer] As Integer = Convert.ToInt64(Me.txt_pag.Text)
                    Dim Left = 1
                    Dim streamReader = File.OpenText(Me.txt_entrada.Text)
                    Me.ToolStripProgressBar1.Maximum = [integer]
                    Dim str As String = streamReader.ReadLine()
                    While Not streamReader.EndOfStream
                        Me.lbl_status.Text = "Lendo..."
                        If Left < [integer] Then
                            If Me.dg_linhas.Rows(index2).Cells(1).Value = Left And str.Length >= (Convert.ToInt64(Me.dg_linhas.Rows(index2).Cells(2).Value) + Convert.ToInt64(Me.dg_linhas.Rows(index2).Cells(3).Value)) Then
                                ', Operators.CompareObjectEqual(Left, Me.dg_linhas.Rows(index2).Cells(1).Value, False))) Then
                                If index2 > 0 Then
                                    'Me.dg_result.Rows(Left - 1).Cells(index2).Value = str.Substring(Me.dg_linhas.Rows(index2).Cells(2).Value, Me.dg_linhas.Rows(index2).Cells(3).Value).ToString()
                                    Me.dg_result.Rows(Left - 1).Cells(index2).Value = str.Substring(Convert.ToInt64(Me.dg_linhas.Rows(index2).Cells(CInt(2)).Value), Convert.ToInt64(Me.dg_linhas.Rows(index2).Cells(CInt(3)).Value)).ToString()
                                Else
                                    Me.dg_result.Rows.Add(New Object(0) {CObj(str.Substring(Convert.ToInt32(RuntimeHelpers.GetObjectValue(Me.dg_linhas.Rows(CInt(index2)).Cells(CInt(2)).Value)), Convert.ToInt64(Convert.ToInt32(RuntimeHelpers.GetObjectValue(Me.dg_linhas.Rows(CInt(index2)).Cells(CInt(3)).Value)).ToString())))})

                                End If
                            End If
                        Else
                            Left = 0
                            Interlocked.Increment(num3)
                            Me.lbl_paglida.Text = num3.ToString()
                            Me.ToolStripProgressBar1.Value = 0
                        End If
                        Interlocked.Increment(Left)
                        Interlocked.Increment(Me.ToolStripProgressBar1.Value)
                        str = streamReader.ReadLine()
                    End While
                    streamReader.Close()
                    Interlocked.Increment(index2)
                End While
                Me.lbl_paglida.Text = (num3 / index2).ToString()
                Me.lbl_status.Text = "Lido."

            Else
                If Not Me.RadioButton1.Checked Then Return
                Me.dg_result.Rows.Clear()
                Dim index3 = 0
                Dim num4 = 0
                While Me.dg_linhas.RowCount - 1 > index3
                    Dim Left = 1
                    Dim flag = False
                    Dim index4 = 0
                    Dim streamReader = File.OpenText(Me.txt_entrada.Text)
                    Me.lbl_status.Text = "Lendo..."
                    Dim str As String = streamReader.ReadLine()
                    While Not streamReader.EndOfStream
                        If str.Contains("PAGE    ") And Not str.Contains("PAGE    " & Me.dg_linhas.Rows(index3).Cells(4).Value.ToString()) Then
                            flag = False
                        ElseIf str.Contains("PAGE    " & Me.dg_linhas.Rows(index3).Cells(4).Value.ToString()) Then
                            flag = True
                            Left = 0
                            Interlocked.Increment(num4)
                            Me.ToolStripProgressBar1.Value = 0
                        End If
                        If flag = True And Me.dg_linhas.Rows(index3).Cells(1).Value = Left And str.Length >= (Convert.ToInt64(Me.dg_linhas.Rows(index3).Cells(2).Value) + Convert.ToInt64(Me.dg_linhas.Rows(index3).Cells(3).Value)) Then
                            If index3 > 0 Then
                                '>>>>Me.dg_result.Rows(index4).Cells(index3).Value = CObj(str.Substring(Convert.ToInt64(Me.dg_linhas.Rows(CInt(index3)).Cells(CInt(2)).Value), Convert.ToInt64(Me.dg_linhas.Rows(CInt(index3)).Cells(CInt(3)).Value)).ToString())
                                globaltable.Rows(index4)(index3) = CObj(str.Substring(Convert.ToInt64(Me.dg_linhas.Rows(CInt(index3)).Cells(CInt(2)).Value), Convert.ToInt64(Me.dg_linhas.Rows(CInt(index3)).Cells(CInt(3)).Value)).ToString())
                                Interlocked.Increment(index4)
                            ElseIf index3 = 0 Then
                                '>>>>Me.dg_result.Rows.Add(New Object(0) {CObj(str.Substring(Convert.ToInt32(RuntimeHelpers.GetObjectValue(Me.dg_linhas.Rows(CInt(index3)).Cells(CInt(2)).Value)), Convert.ToInt64(Convert.ToInt32(RuntimeHelpers.GetObjectValue(Me.dg_linhas.Rows(CInt(index3)).Cells(CInt(3)).Value)).ToString())))})
                                globaltable.Rows.Add(New Object(0) {CObj(str.Substring(Convert.ToInt32(RuntimeHelpers.GetObjectValue(Me.dg_linhas.Rows(CInt(index3)).Cells(CInt(2)).Value)), Convert.ToInt64(Convert.ToInt32(RuntimeHelpers.GetObjectValue(Me.dg_linhas.Rows(CInt(index3)).Cells(CInt(3)).Value)).ToString())))})
                            End If
                        End If
                        Interlocked.Increment(Left)
                        str = streamReader.ReadLine()
                    End While
                    streamReader.Close()
                    Interlocked.Increment(index3)
                End While


                Me.lbl_paglida.Text = (num4 / index3).ToString()
                Me.lbl_status.Text = "Lido."



                bsglobal.DataSource = globaltable
                dg_result.DataSource = bsglobal.DataSource
                num1 = 0
                columnCount2 = Me.dg_result.ColumnCount
                While columnCount > num1
                    Me.dg_result.Columns(num1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    Interlocked.Increment(num1)
                End While
                dg_result.Refresh()

            End If
        End If

        'Catch ex As Exception
        'MsgBox(ex.Message)
        'End Try
    End Sub


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.dg_linhas.Rows.Add("HoraInicio", "1", "71", "19", "5")
        Me.dg_linhas.Rows.Add("HoraFim", "2", "71", "19", "5")
        Me.dg_linhas.Rows.Add("CP", "5", "36", "4", "5")
        Me.txt_entrada.Text = "C:\Users\MatheusPorsch\Downloads\RMFCP25.txt"
    End Sub

    Private Sub dg_result_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs)
        If Me.dg_result.RowCount - 1 > 0 Then
            Me.lblresult.Text = Convert.ToString(Me.dg_result.RowCount - 1)
        Else
            Me.lblresult.Text = "0"
        End If
    End Sub

    Private Sub dg_result_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs)
        If Me.dg_result.RowCount - 1 > 0 Then
            Me.lblresult.Text = Convert.ToString(Me.dg_result.RowCount - 1)
        Else
            Me.lblresult.Text = "0"
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Me.dg_result.Rows.Count <= 0 Then Return
        Try
            ' ISSUE: reference to a compiler-generated method
            Me.XcelApp.Application.Workbooks.Add(RuntimeHelpers.GetObjectValue(Type.Missing))
            Dim count As Integer = Me.dg_result.Columns.Count
            Dim ColumnIndex = 1
            While ColumnIndex <= count
                Me.XcelApp.Cells(1, ColumnIndex) = CObj(Me.dg_result.Columns(ColumnIndex - 1).HeaderText)
                Interlocked.Increment(ColumnIndex)
            End While
            Dim num1 As Integer = Me.dg_result.Rows.Count - 2
            Dim index1 = 0
            While index1 <= num1
                Dim num2 As Integer = Me.dg_result.Columns.Count - 1
                Dim index2 = 0
                While index2 <= num2
                    Me.XcelApp.Cells(index1 + 2, index2 + 1) = CObj(Me.dg_result.Rows(index1).Cells(index2).Value.ToString())
                    Interlocked.Increment(index2)
                End While
                Interlocked.Increment(index1)
            End While

            Me.XcelApp.Columns.AutoFit()
            Me.XcelApp.Visible = True
        Catch ex As Exception
            MessageBox.Show("Erro : " & ex.Message)
            Me.XcelApp.Quit()
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        OpenFileDialog1.ShowDialog()
        If Me.OpenFileDialog1.CheckFileExists Then
            Return
        Else
            MessageBox.Show("O arquivo não existe.", "Não existe", MessageBoxButtons.OK)
        End If

        Me.txt_entrada.Text = Me.OpenFileDialog1.FileName.ToString()

    End Sub

    Public Function GetDataTable(ByVal pDataGridView As DataGridView, Optional ByVal pColumnNames As Boolean = True) As System.Data.DataTable
        Dim table As New System.Data.DataTable

        For Each column As DataGridViewColumn In pDataGridView.Columns
            If column.Visible Then
                If pColumnNames Then
                    table.Columns.Add(New DataColumn() With {.ColumnName = column.Name})
                Else
                    table.Columns.Add()
                End If
            End If
        Next

        Dim cellValues(pDataGridView.Columns.Count - 1) As Object

        For Each row As DataGridViewRow In pDataGridView.Rows
            For i As Integer = 0 To row.Cells.Count - 1
                cellValues(i) = row.Cells(i).Value
            Next
            table.Rows.Add(cellValues)
        Next

        Return table

    End Function
    Private Sub dg_result_FilterStringChanged(sender As Object, e As Zuby.ADGV.AdvancedDataGridView.FilterEventArgs) Handles dg_result.FilterStringChanged

        bsglobal.Filter = dg_result.FilterString

    End Sub

    Private Sub dg_result_SortStringChanged(sender As Object, e As AdvancedDataGridView.SortEventArgs) Handles dg_result.SortStringChanged
        bsglobal.Sort = dg_result.SortString
    End Sub
End Class
