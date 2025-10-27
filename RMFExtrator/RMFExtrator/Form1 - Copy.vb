
Imports System.Buffers
Imports System.ComponentModel
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Threading
Imports ScottPlot
Imports ScottPlot.TickGenerators

Public Class Form1

    Private Sub ApplyScottPlotDarkMode()
        ' figure & data backgrounds
        FormsPlot1.Plot.FigureBackground.Color = ScottPlot.Color.FromHex("#181818")
        FormsPlot1.Plot.DataBackground.Color = ScottPlot.Color.FromHex("#1f1f1f")

        ' axes (ticks, labels, frames)
        FormsPlot1.Plot.Axes.Color(ScottPlot.Color.FromHex("#d7d7d7"))

        ' grid
        FormsPlot1.Plot.Grid.MajorLineColor = ScottPlot.Color.FromHex("#404040")
        ' (optional) minor grid if you enabled it somewhere:
        ' FormsPlot1.Plot.Grid.MinorLineColor = ScottPlot.Color.FromHex("#303030")

        ' legend (if you use it)
        FormsPlot1.Plot.Legend.BackgroundColor = ScottPlot.Color.FromHex("#404040")
        FormsPlot1.Plot.Legend.FontColor = ScottPlot.Color.FromHex("#d7d7d7")
        FormsPlot1.Plot.Legend.OutlineColor = ScottPlot.Color.FromHex("#d7d7d7")
    End Sub

    Private XcelApp As New Microsoft.Office.Interop.Excel.Application
    'Dim frmconfig As New frm_config_chartvb
    Dim globaltable As New System.Data.DataTable
    Dim bsglobal As New BindingSource
    Public pccom As New frm_pcomm

    ' Local grid used to configure chart mappings (rebuilt at runtime)
    Dim dg_colunas = New DataGridView()

    Private Sub createDGV_Chart()
        ' Rebuild columns locally (do NOT depend on Designer-only members)
        dg_colunas.AllowUserToAddRows = False
        dg_colunas.AllowUserToDeleteRows = False
        dg_colunas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize

        Dim colFieldName As New DataGridViewTextBoxColumn() With {
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
            .HeaderText = "Field Name",
            .MinimumWidth = 6,
            .Name = "colFieldName",
            .ReadOnly = True
        }

        Dim colTypeLocal As New DataGridViewComboBoxColumn() With {
            .HeaderText = "Type",
            .MinimumWidth = 6,
            .Name = "colTypeLocal",
            .Width = 125
        }
        colTypeLocal.Items.AddRange("Data", "Hora", "DataHora", "Inteiro", "Decimal", "Texto")

        Dim colAxisLocal As New DataGridViewComboBoxColumn() With {
            .HeaderText = "Axis",
            .MinimumWidth = 6,
            .Name = "colAxisLocal",
            .Width = 125
        }
        colAxisLocal.Items.AddRange("Não Aplicado", "X", "Y", "Y Categorizado", "Y Linha Fixa (Max)", "X Sombra")

        dg_colunas.Columns.Clear()
        dg_colunas.Columns.AddRange(New DataGridViewColumn() {colFieldName, colTypeLocal, colAxisLocal})

        dg_colunas.MultiSelect = False
        dg_colunas.Name = "dg_colunas"
        dg_colunas.RowHeadersWidth = 30
        dg_colunas.RowTemplate.Height = 29
        dg_colunas.Size = New Size(731, 601)
        dg_colunas.TabIndex = 4
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        If BackgroundWorker1.CancellationPending = True Then
            e.Cancel = True
            Exit Sub
        End If
        Importar(BackgroundWorker1, txt_entrada.Text)
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        If (e.Cancelled = True) Then
            MsgBox("Import canceled.", MsgBoxStyle.Information, "Canceled")
            Me.lbl_status.Text = "Canceled."
            Me.ToolStripProgressBar1.Value = 0
        Else
            Me.lbl_status.Text = "Loaded."
            bsglobal.DataSource = globaltable
            dg_result.DataSource = bsglobal.DataSource
            Dim num1 = 0
            Dim columnCount2 = 0
            columnCount2 = Me.dg_result.ColumnCount
            While columnCount2 > num1
                Me.dg_result.Columns(num1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                Interlocked.Increment(num1)
            End While
            dg_result.Refresh()

            lblresult.Text = (dg_result.Rows.Count - 1)

            If pccom.CheckBox1.Checked = True Then
                Me.lbl_status.Text = "Removing duplicates..."
                Me.ToolStripProgressBar1.Value = 0

                If Me.BackgroundWorker2.IsBusy = True Then
                    Me.BackgroundWorker2.CancelAsync()
                    While Me.BackgroundWorker2.IsBusy
                        Application.DoEvents()
                    End While
                End If

                BackgroundWorker2.RunWorkerAsync()
            End If
        End If
    End Sub

    Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        ToolStripProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub Importar(ByVal processo As BackgroundWorker, ByVal arquivo As String)
        Try
            ' read total number of lines in the file
            Dim linhas_totais As Integer = 0
            Dim streamReader2 = File.OpenText(arquivo)
            Dim str2 As String = streamReader2.ReadLine()
            While Not streamReader2.EndOfStream
                str2 = streamReader2.ReadLine()
                Interlocked.Increment(linhas_totais)
            End While
            streamReader2.Close()

            If Me.RadioButton3.Checked Then
                Me.lbl_status.Text = "Reading... "
                Me.dg_result.Rows.Clear()
                Dim linhagrid = 0
                Dim linhagrid2 = 0
                Dim num_linha As Integer = 0
                Dim poslinha As Integer = 0
                Dim poslinha2 As Integer = 0
                Dim pag As Integer = 0

                streamReader2 = File.OpenText(arquivo)
                str2 = streamReader2.ReadLine()

                Dim streamReader = File.OpenText(arquivo)
                Dim str As String = streamReader.ReadLine()

                ' OPEN TWO READERS
                While Not streamReader.EndOfStream Or Not streamReader2.EndOfStream
                    If processo.CancellationPending = True Then
                        Exit Sub
                    End If

                    If IsNothing(str) Or IsNothing(str2) Then
                        Exit While
                    End If

                    ' create the header temp table
                    Dim tablecabeca As New System.Data.DataTable
                    tablecabeca.Clear()
                    tablecabeca.Columns.Add()

                    If dg_table.Rows.Count > 1 Then
                        ' fixed fields first (str)
                        If str.Contains(txtseparador.Text) And str <> Nothing And dg_table.Rows.Count > 1 Then
                            poslinha = 1
                            str = streamReader.ReadLine()
                            Interlocked.Increment(num_linha)

                            While Not str.Contains(txtseparador.Text) And Not streamReader.EndOfStream
                                Interlocked.Increment(pag)
                                linhagrid = 0

                                While linhagrid < Me.dg_linhas.RowCount - 1
                                    If Me.dg_linhas.Rows(linhagrid).Cells(1).Value = poslinha AndAlso str.Length >= (Convert.ToInt64(Me.dg_linhas.Rows(linhagrid).Cells(2).Value) + Convert.ToInt64(Me.dg_linhas.Rows(linhagrid).Cells(3).Value)) Then
                                        If ValidateType(New Object(0) {CObj(Trim(str.Substring(Convert.ToInt32(RuntimeHelpers.GetObjectValue(Me.dg_linhas.Rows(linhagrid).Cells(2).Value)), Convert.ToInt64(Convert.ToInt32(RuntimeHelpers.GetObjectValue(Me.dg_linhas.Rows(linhagrid).Cells(3).Value)).ToString()))))}, Me.dg_linhas.Rows(linhagrid).Cells(5).Value) = True Then
                                            tablecabeca.Rows.Add(New Object(0) {CObj(Trim(str.Substring(Convert.ToInt32(RuntimeHelpers.GetObjectValue(Me.dg_linhas.Rows(linhagrid).Cells(2).Value)), Convert.ToInt64(Convert.ToInt32(RuntimeHelpers.GetObjectValue(Me.dg_linhas.Rows(linhagrid).Cells(3).Value)).ToString()))))})
                                        Else
                                            tablecabeca.Rows.Add(New Object(0) {CObj(Trim(""))})
                                        End If
                                    End If
                                    Interlocked.Increment(linhagrid)
                                End While

                                Interlocked.Increment(poslinha)
                                str = streamReader.ReadLine()
                                Interlocked.Increment(num_linha)
                            End While
                        End If

                        ' table fields (str2)
                        If str2.Contains(txtseparador.Text) And str2 <> Nothing And dg_table.Rows.Count > 1 Then
                            poslinha2 = 1
                            str2 = streamReader2.ReadLine()

                            If IsNothing(str) Or IsNothing(str2) Then
                                Exit While
                            End If

                            While Not str2.Contains(txtseparador.Text) And Not streamReader2.EndOfStream
                                linhagrid2 = 0
                                Dim i As Integer = 0

                                If Trim(str2).ToString() <> "" Then
                                    While linhagrid2 < Me.dg_table.RowCount - 1
                                        If IsNumeric(Me.dg_table.Rows(linhagrid2).Cells(2).Value) Then
                                            Dim filter As String() = Nothing
                                            If Trim(Convert.ToString(Me.dg_table.Rows(linhagrid2).Cells(7).Value)) <> "" Then
                                                filter = Trim(Convert.ToString(Me.dg_table.Rows(linhagrid2).Cells(7).Value)).Split(";"c)
                                            End If

                                            If (Me.dg_table.Rows(linhagrid2).Cells(1).Value <= poslinha2 And Me.dg_table.Rows(linhagrid2).Cells(2).Value >= poslinha2) AndAlso str2.Length >= (Convert.ToInt64(Me.dg_table.Rows(linhagrid2).Cells(3).Value) + Convert.ToInt64(Me.dg_table.Rows(linhagrid2).Cells(4).Value)) Then
                                                If linhagrid2 > 0 Then
                                                    If ValidateType(New Object(0) {CObj(Trim(str2.Substring(Convert.ToInt64(Me.dg_table.Rows(CInt(linhagrid2)).Cells(CInt(3)).Value), Convert.ToInt64(Me.dg_table.Rows(CInt(linhagrid2)).Cells(CInt(4)).Value)).ToString()))}, Me.dg_table.Rows(linhagrid2).Cells(5).Value) = True Then
                                                        If filter IsNot Nothing Then
                                                            For Each word As String In filter
                                                                Dim STT As String = Trim(str2.Substring(Convert.ToInt64(Me.dg_table.Rows(CInt(linhagrid2)).Cells(CInt(3)).Value), Convert.ToInt64(Me.dg_table.Rows(CInt(linhagrid2)).Cells(CInt(4)).Value)).ToString())
                                                                If word Is Nothing Then
                                                                    globaltable.Rows(globaltable.Rows.Count - 1)(i) = CObj(Trim(str2.Substring(Convert.ToInt64(Me.dg_table.Rows(CInt(linhagrid2)).Cells(CInt(3)).Value), Convert.ToInt64(Me.dg_table.Rows(CInt(linhagrid2)).Cells(CInt(4)).Value)).ToString()))
                                                                Else
                                                                    If Trim(word) = STT Then
                                                                        globaltable.Rows(globaltable.Rows.Count - 1)(i) = CObj(Trim(str2.Substring(Convert.ToInt64(Me.dg_table.Rows(CInt(linhagrid2)).Cells(CInt(3)).Value), Convert.ToInt64(Me.dg_table.Rows(CInt(linhagrid2)).Cells(CInt(4)).Value)).ToString()))
                                                                        Exit For
                                                                    Else
                                                                        globaltable.Rows(globaltable.Rows.Count - 1)(i) = ""
                                                                    End If
                                                                End If
                                                            Next
                                                        Else
                                                            globaltable.Rows(globaltable.Rows.Count - 1)(i) = CObj(Trim(str2.Substring(Convert.ToInt64(Me.dg_table.Rows(CInt(linhagrid2)).Cells(CInt(3)).Value), Convert.ToInt64(Me.dg_table.Rows(CInt(linhagrid2)).Cells(CInt(4)).Value)).ToString()))
                                                        End If
                                                    Else
                                                        globaltable.Rows(globaltable.Rows.Count - 1)(i) = ""
                                                    End If
                                                    Interlocked.Increment(i)
                                                Else
                                                    ' first step: add fixed header values
                                                    If tablecabeca.Rows.Count > 0 Then
                                                        While i < tablecabeca.Rows.Count
                                                            If i = 0 Then
                                                                globaltable.Rows.Add(tablecabeca.Rows(i)(0))
                                                            Else
                                                                globaltable.Rows(globaltable.Rows.Count - 1)(i) = tablecabeca.Rows(i)(0)
                                                            End If
                                                            Interlocked.Increment(i)
                                                        End While
                                                    End If

                                                    If ValidateType(New Object(0) {CObj(Trim(str2.Substring(Me.dg_table.Rows(linhagrid2).Cells(3).Value, Me.dg_table.Rows(linhagrid2).Cells(4).Value)))}, Me.dg_table.Rows(linhagrid2).Cells(5).Value) = True Then
                                                        If filter IsNot Nothing Then
                                                            For Each word As String In filter
                                                                Dim STT As String = Trim(str2.Substring(Me.dg_table.Rows(linhagrid2).Cells(3).Value, Me.dg_table.Rows(linhagrid2).Cells(4).Value))
                                                                If word Is Nothing Then
                                                                    globaltable.Rows(globaltable.Rows.Count - 1)(i) = Trim(str2.Substring(Me.dg_table.Rows(linhagrid2).Cells(3).Value, Me.dg_table.Rows(linhagrid2).Cells(4).Value))
                                                                Else
                                                                    If Trim(word) = STT Then
                                                                        globaltable.Rows(globaltable.Rows.Count - 1)(i) = Trim(str2.Substring(Me.dg_table.Rows(linhagrid2).Cells(3).Value, Me.dg_table.Rows(linhagrid2).Cells(4).Value))
                                                                        Exit For
                                                                    Else
                                                                        globaltable.Rows(globaltable.Rows.Count - 1)(i) = ""
                                                                    End If
                                                                End If
                                                            Next
                                                        Else
                                                            globaltable.Rows(globaltable.Rows.Count - 1)(i) = Trim(str2.Substring(Me.dg_table.Rows(linhagrid2).Cells(3).Value, Me.dg_table.Rows(linhagrid2).Cells(4).Value))
                                                        End If
                                                    Else
                                                        globaltable.Rows(globaltable.Rows.Count - 1)(i) = ""
                                                    End If

                                                    Interlocked.Increment(i)
                                                End If
                                            End If
                                        End If
                                        Interlocked.Increment(linhagrid2)
                                    End While
                                End If

                                Interlocked.Increment(poslinha2)
                                str2 = streamReader2.ReadLine()
                            End While
                        End If
                    Else
                        ' only fixed fields
                        If str.Contains(txtseparador.Text) And str <> Nothing And dg_table.Rows.Count = 1 Then
                            poslinha = 1
                            str = streamReader.ReadLine()
                            str2 = streamReader2.ReadLine()
                            Interlocked.Increment(num_linha)

                            While Not str.Contains(txtseparador.Text) And Not streamReader.EndOfStream
                                Interlocked.Increment(pag)
                                linhagrid = 0

                                While linhagrid < Me.dg_linhas.RowCount - 1
                                    If Me.dg_linhas.Rows(linhagrid).Cells(1).Value = poslinha AndAlso str.Length >= (Convert.ToInt64(Me.dg_linhas.Rows(linhagrid).Cells(2).Value) + Convert.ToInt64(Me.dg_linhas.Rows(linhagrid).Cells(3).Value)) Then
                                        If linhagrid > 0 Then
                                            globaltable.Rows(globaltable.Rows.Count - 1)(linhagrid) = CObj(Trim(str.Substring(Convert.ToInt64(Me.dg_linhas.Rows(CInt(linhagrid)).Cells(CInt(2)).Value), Convert.ToInt64(Me.dg_linhas.Rows(CInt(linhagrid)).Cells(CInt(3)).Value)).ToString()))
                                        ElseIf linhagrid = 0 Then
                                            globaltable.Rows.Add(New Object(0) {CObj(Trim(str.Substring(Convert.ToInt32(RuntimeHelpers.GetObjectValue(Me.dg_linhas.Rows(linhagrid).Cells(2).Value)), Convert.ToInt64(Convert.ToInt32(RuntimeHelpers.GetObjectValue(Me.dg_linhas.Rows(linhagrid).Cells(3).Value)).ToString()))))})
                                        End If
                                    End If
                                    Interlocked.Increment(linhagrid)
                                End While

                                Interlocked.Increment(poslinha)
                                str = streamReader.ReadLine()
                                str2 = streamReader2.ReadLine()
                                Interlocked.Increment(num_linha)
                            End While
                        End If
                    End If

                    ' read next
                    If Not str.Contains(txtseparador.Text) And Not str2.Contains(txtseparador.Text) And Not streamReader.EndOfStream And Not streamReader2.EndOfStream Then
                        str = streamReader.ReadLine()
                        str2 = streamReader2.ReadLine()
                        Interlocked.Increment(num_linha)
                    End If

                    BackgroundWorker1.ReportProgress((num_linha / Math.Max(1, linhas_totais)) * 100)
                End While

                streamReader.Close()
                streamReader2.Close()
                Me.lbl_paglida.Text = pag.ToString()
            End If

            ' remove rows with null/empty fields
            Me.lbl_status.Text = "Removing rows with null or inconsistent fields..."

            For rowIndex As Integer = globaltable.Rows.Count - 1 To 0 Step -1
                Dim row As DataRow = globaltable.Rows(rowIndex)
                Dim isEmpty As Boolean = False
                For Each column As DataColumn In globaltable.Columns
                    Dim v = Convert.ToString(row(column))
                    If String.IsNullOrEmpty(v) OrElse Trim(v) = "" Then
                        isEmpty = True
                        Exit For
                    End If
                Next
                If isEmpty Then
                    globaltable.Rows.RemoveAt(rowIndex)
                End If
            Next

            ' grouping placeholder (left as original sketch)

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Exclamation, "Error")
        End Try
    End Sub

    Function ValidateType(ByVal variable As Object, ByVal type As String) As Boolean
        ' 0-DataHora, 1-Data, 2-Hora, 3-Decimal, 4-Texto
        Try
            Dim result As Boolean
            Select Case type
                Case "DataHora"
                    Dim dateValue As DateTime
                    result = DateTime.TryParse(variable(0).ToString().Replace(".", ":"), dateValue)
                Case "Data"
                    Dim dateValue As Date
                    result = Date.TryParse(variable(0).ToString(), dateValue)
                Case "Hora"
                    Dim timeSpan As TimeSpan
                    result = TimeSpan.TryParse(variable(0).ToString().Replace(".", ":"), timeSpan)
                Case "Decimal"
                    Dim decVal As Decimal
                    result = Decimal.TryParse(variable(0).ToString().Replace(".", ","), decVal)
                Case "Texto"
                    result = Trim(variable(0).ToString()) <> ""
                Case Else
                    result = True
            End Select
            Return result
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub AbrirTXT(ByVal arquivo As String, ByVal realtime As Boolean)
        If Me.BackgroundWorker1.IsBusy = True Then
            Me.BackgroundWorker1.CancelAsync()
            If realtime = False Then
                Exit Sub
            Else
                While Me.BackgroundWorker1.IsBusy
                    Application.DoEvents()
                End While
            End If
        End If

        If arquivo = "" Or Me.txt_pag.Text = "" Or Me.txt_pag.Text = "0" Or Not IsNumeric(Me.txt_pag.Text) Then
            MessageBox.Show("Input file, output and number of lines per page must be filled.", "Blank field", MessageBoxButtons.OK)
            Me.txt_entrada.Focus()
            Exit Sub
        ElseIf Me.dg_linhas.RowCount = 0 Then
            MessageBox.Show("You must add rows to the 'fields to search' grid.", "Empty rows", MessageBoxButtons.OK)
            Me.dg_linhas.Focus()
            Exit Sub
        ElseIf File.Exists(arquivo) = False Then
            MessageBox.Show("The file you're trying to open does not exist.", "File missing", MessageBoxButtons.OK)
            Exit Sub
        End If

        If RadioButton3.Checked And txtseparador.Text = "" Then
            MessageBox.Show("The separator must be provided.", "Blank field", MessageBoxButtons.OK)
            Exit Sub
        End If

        If (RadioButton2.Checked And Not IsNumeric(txt_pag.Text)) Or (RadioButton2.Checked And Not IsNumeric(txtoffset.Text)) Then
            MessageBox.Show("Check page size and offset.", "Blank field", MessageBoxButtons.OK)
            Exit Sub
        End If

        If RadioButton3.Checked And dg_table.RowCount = 0 Then
            MessageBox.Show("Add rows to the Table Grid to search for tables in the file", "Blank rows", MessageBoxButtons.OK)
            Exit Sub
        End If

        ' normalize order
        For Each dgvr As DataGridViewRow In dg_linhas.Rows
            Dim r As Integer
            If Integer.TryParse(Convert.ToString(dgvr.Cells(1).Value), r) AndAlso Convert.ToString(dgvr.Cells(0).Value) <> "" Then
                dgvr.Cells(1).Value = r
            End If
        Next

        For Each dgvr As DataGridViewRow In dg_table.Rows
            Dim r As Integer
            If Integer.TryParse(Convert.ToString(dgvr.Cells(1).Value), r) AndAlso Convert.ToString(dgvr.Cells(0).Value) <> "" Then
                dgvr.Cells(1).Value = r
            End If
        Next

        dg_linhas.Sort(dg_linhas.Columns(1), ListSortDirection.Ascending)
        dg_table.Sort(dg_table.Columns(1), ListSortDirection.Ascending)

        If File.Exists(arquivo) Then
            Dim myStreamReader As System.IO.StreamReader = Nothing
            Try
                myStreamReader = System.IO.File.OpenText(arquivo)
                RichTextBox1.Text = myStreamReader.ReadToEnd()
                RichTextBox1.SelectionStart = RichTextBox1.Text.Length
                myStreamReader.Close()
            Catch ex As Exception
                If myStreamReader IsNot Nothing Then myStreamReader.Close()
                MsgBox(ex.Message, MsgBoxStyle.Information)
            End Try
        End If

        bsglobal.Filter = Nothing
        bsglobal.Sort = Nothing

        dg_linhas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dg_table.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dg_result.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        ' clear table and grid
        Me.lbl_status.Text = "Cleaning..."
        Me.lblresult.Text = "0"
        Me.lbl_paglida.Text = "0"
        dg_result.DataSource = Nothing
        dg_result.Rows.Clear()
        globaltable.Rows.Clear()
        Me.ToolStripProgressBar1.Value = 0

        ' clear dg_result columns
        Dim removeCount As Integer = Me.dg_result.ColumnCount
        For i As Integer = removeCount - 1 To 0 Step -1
            Me.dg_result.Columns.RemoveAt(i)
        Next

        ' clear globaltable columns
        For i As Integer = globaltable.Columns.Count - 1 To 0 Step -1
            globaltable.Columns.RemoveAt(i)
        Next

        ' add columns by configuration
        Dim index1 = 0
        While (Me.dg_linhas.RowCount - 1) > index1
            globaltable.Columns.Add(Convert.ToString(Me.dg_linhas.Rows(index1).Cells(0).Value))
            Interlocked.Increment(index1)
        End While

        If RadioButton3.Checked Then
            index1 = 0
            While (Me.dg_table.RowCount - 1) > index1
                globaltable.Columns.Add(Convert.ToString(Me.dg_table.Rows(index1).Cells(0).Value))
                Interlocked.Increment(index1)
            End While
        End If

        Me.dg_result.Refresh()
        Me.dg_result.RefreshEdit()

        Me.BackgroundWorker1.RunWorkerAsync()
    End Sub

    Public Sub btn_exec_Click(sender As Object, e As EventArgs) Handles btn_exec.Click
        AbrirTXT(txt_entrada.Text, pccom.CheckBox1.Checked)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' defaults
        txtseparador.Text = "RMF V2R4"

        dg_linhas.Rows.Clear()
        dg_table.Rows.Clear()

        createDGV_Chart()

        Me.txt_entrada.Text = "C:\Users\MatheusPorsch\Desktop\hardcopy\RMF CPC PROD1.txt"

        ApplyScottPlotDarkMode()


        If File.Exists(Application.StartupPath.ToString() & "\" & "Buffer.txt") Then
            IO.File.Delete(Application.StartupPath.ToString() & "\" & "Buffer.txt")
        End If

        ' --- Runtime wiring: replace Handles to avoid designer coupling issues ---
        ' ADGV (Zuby.ADGV)
        AddHandler dg_result.FilterStringChanged, AddressOf dg_result_FilterStringChanged
        AddHandler dg_result.SortStringChanged, AddressOf dg_result_SortStringChanged

        ' DataGridViews
        AddHandler dg_linhas.RowsAdded, AddressOf dg_linhas_RowsAdded
        AddHandler dg_linhas.RowsRemoved, AddressOf dg_linhas_RowsRemoved
        AddHandler dg_linhas.RowLeave, AddressOf dg_linhas_RowLeave

        AddHandler dg_table.RowsAdded, AddressOf dg_table_RowsAdded
        AddHandler dg_table.RowsRemoved, AddressOf dg_table_RowsRemoved
        AddHandler dg_table.RowLeave, AddressOf dg_table_RowLeave
    End Sub

    ' removed Handles: we wire in Form1_Load
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
        AtualizarGridChart()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OpenFileDialog1.ShowDialog()
        Me.txt_entrada.Text = Me.OpenFileDialog1.FileName.ToString()
        If Me.OpenFileDialog1.CheckFileExists Then
            Return
        Else
            MessageBox.Show("The file does not exist.", "Missing file", MessageBoxButtons.OK)
        End If
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

    ' removed Handles; wired at runtime
    Private Sub dg_result_FilterStringChanged(sender As Object, e As Zuby.ADGV.AdvancedDataGridView.FilterEventArgs)
        bsglobal.Filter = dg_result.FilterString
    End Sub

    Private Sub dg_result_SortStringChanged(sender As Object, e As Zuby.ADGV.AdvancedDataGridView.SortEventArgs)
        bsglobal.Sort = dg_result.SortString
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If File.Exists(txt_entrada.Text) = False Then
            MessageBox.Show("The file you're trying to open does not exist.", "File missing", MessageBoxButtons.OK)
            Exit Sub
        End If
        System.Diagnostics.Process.Start("notepad.exe", txt_entrada.Text)

        If File.Exists(txt_entrada.Text) Then
            Dim myStreamReader As System.IO.StreamReader = Nothing
            Try
                myStreamReader = System.IO.File.OpenText(txt_entrada.Text)
                RichTextBox1.Text = myStreamReader.ReadToEnd()
                RichTextBox1.SelectionStart = RichTextBox1.Text.Length
                myStreamReader.Close()
            Catch ex As Exception
                If myStreamReader IsNot Nothing Then myStreamReader.Close()
                MsgBox(ex.Message, MsgBoxStyle.Information)
            End Try
        End If
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged, RadioButton2.CheckedChanged, RadioButton1.CheckedChanged
        If RadioButton3.Checked = True Then
            dg_table.Enabled = True
            txtoffset.Enabled = False
            txt_pag.Enabled = False
            txtseparador.Enabled = True
        ElseIf RadioButton2.Checked = True Then
            dg_table.Enabled = False
            txtoffset.Enabled = True
            txt_pag.Enabled = True
            txtseparador.Enabled = False
        ElseIf RadioButton1.Checked = True Then
            dg_table.Enabled = False
            txtoffset.Enabled = False
            txt_pag.Enabled = False
            txtseparador.Enabled = False
        End If
    End Sub

    Private Sub SelecionarCampoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelecionarCampoToolStripMenuItem.Click
        Me.dg_linhas.Rows.Add("Coluna_" & dg_linhas.RowCount, (RichTextBox1.GetLineFromCharIndex(RichTextBox1.SelectionStart)).ToString(), (RichTextBox1.SelectionStart - RichTextBox1.GetFirstCharIndexOfCurrentLine()).ToString(), RichTextBox1.SelectionLength.ToString(), "0")
    End Sub

    Private Sub RichTextBox1_SelectionChanged(sender As Object, e As EventArgs) Handles RichTextBox1.SelectionChanged
        txtposi.Text = "Position: " & RichTextBox1.GetLineFromCharIndex(RichTextBox1.SelectionStart) & " line / " & (RichTextBox1.SelectionStart - RichTextBox1.GetFirstCharIndexOfCurrentLine()).ToString() & " column / " & RichTextBox1.SelectionLength.ToString() & " length"
    End Sub

    Private Sub FormsPlot1_DoubleClick_1(sender As Object, e As EventArgs) Handles FormsPlot1.DoubleClick
        FormsPlot1.Refresh()
    End Sub

    Private Sub TabControl1_Selected(sender As Object, e As TabControlEventArgs) Handles TabControl1.Selected
        If TabControl1.SelectedIndex = 1 Then
            If File.Exists(txt_entrada.Text) Then
                Dim myStreamReader As System.IO.StreamReader = Nothing
                Try
                    myStreamReader = System.IO.File.OpenText(txt_entrada.Text)
                    RichTextBox1.Text = myStreamReader.ReadToEnd()
                    RichTextBox1.SelectionStart = RichTextBox1.Text.Length
                    myStreamReader.Close()
                Catch ex As Exception
                    If myStreamReader IsNot Nothing Then myStreamReader.Close()
                    MsgBox(ex.Message, MsgBoxStyle.Information)
                End Try
            End If
        End If
    End Sub

    Private Sub RemoverDuplicados(ByRef dgvData As DataGridView, ByVal processo As BackgroundWorker)
        Dim rowsToRemove As New List(Of DataGridViewRow)()
        Dim i2 As Integer = 0

        For i As Integer = 0 To dgvData.Rows.Count - 2
            Interlocked.Increment(i2)
            If processo.CancellationPending = True Then
                Exit Sub
            End If

            Dim currentRow As DataGridViewRow = dgvData.Rows(i)

            For j As Integer = i + 1 To dgvData.Rows.Count - 2
                Dim comparisonRow As DataGridViewRow = dgvData.Rows(j)
                Dim duplicate As Boolean = True

                For Each cell As DataGridViewCell In currentRow.Cells
                    Dim a = Convert.ToString(cell.Value)
                    Dim b = Convert.ToString(comparisonRow.Cells(cell.ColumnIndex).Value)
                    If Not String.Equals(a, b) Then
                        duplicate = False
                        Exit For
                    End If
                Next

                If duplicate AndAlso Not rowsToRemove.Contains(comparisonRow) Then
                    rowsToRemove.Add(comparisonRow)
                End If
            Next

            BackgroundWorker2.ReportProgress(CInt((i2 / Math.Max(1.0, dgvData.Rows.Count)) * 100))
        Next

        For Each row As DataGridViewRow In rowsToRemove
            dgvData.Rows.Remove(row)
        Next
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If BackgroundWorker2.IsBusy = True Then
            BackgroundWorker2.CancelAsync()
            Exit Sub
        End If

        If dg_result.Rows.Count > 0 Then
            Me.lbl_status.Text = "Removing duplicates..."
            Me.ToolStripProgressBar1.Value = 0
            BackgroundWorker2.RunWorkerAsync()
        End If
    End Sub

    Private Sub BackgroundWorker2_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        RemoverDuplicados(dg_result, BackgroundWorker2)
    End Sub

    Private Sub BackgroundWorker2_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker2.RunWorkerCompleted
        If (e.Cancelled = True) Then
            MsgBox("Removal canceled.", MsgBoxStyle.Information, "Canceled")
            Me.lbl_status.Text = "Canceled."
            Me.ToolStripProgressBar1.Value = 0
        Else
            Me.lbl_status.Text = "Duplicates removed."
            Me.ToolStripProgressBar1.Value = 100

            Me.lbl_status.Text = "Plotting..."
            Me.ToolStripProgressBar1.Value = 0

            If pccom.CheckBox1.Checked = True Then
                If Me.BackgroundWorker3.IsBusy = True Then
                    Me.BackgroundWorker3.CancelAsync()
                    While Me.BackgroundWorker3.IsBusy
                        Application.DoEvents()
                    End While
                End If
                BackgroundWorker3.RunWorkerAsync()
            End If
        End If
    End Sub

    Private Sub BackgroundWorker2_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles BackgroundWorker2.ProgressChanged
        ToolStripProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub BackgroundWorker3_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker3.DoWork
        Plotar(BackgroundWorker3)
    End Sub

    Private Sub BackgroundWorker3_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker3.RunWorkerCompleted
        If (e.Cancelled = True) Then
            MsgBox("Removal canceled.", MsgBoxStyle.Information, "Canceled")
            Me.lbl_status.Text = "Canceled."
            Me.ToolStripProgressBar1.Value = 0
        Else
            FormsPlot1.Refresh()
            Me.lbl_status.Text = "Plot completed."
            Me.ToolStripProgressBar1.Value = 100
        End If
    End Sub

    Private Sub BackgroundWorker3_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles BackgroundWorker3.ProgressChanged
        ToolStripProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If BackgroundWorker3.IsBusy = True Then
            BackgroundWorker3.CancelAsync()
            Exit Sub
        End If

        ' If there is a plain Y, drop categorized config
        For i2 As Integer = 0 To (dg_linhas.RowCount - 1)
            If Convert.ToString(dg_linhas.Rows(i2).Cells(6).Value) = "Y" Then
                dg_table.Rows.Clear()
            End If
        Next

        Me.lbl_status.Text = "Plotting..."
        Me.ToolStripProgressBar1.Value = 0
        BackgroundWorker3.RunWorkerAsync()
    End Sub

    Private Sub Plotar(ByVal processo As BackgroundWorker)
        AtualizarGridChart()

        Try
            FormsPlot1.Plot.Clear()

            Dim con As New DoubleConverter
            Dim datacol As Integer = -1
            Dim horacol As Integer = -1
            Dim categcol As Integer = -1
            Dim linhasycol As Integer = -1
            Dim sombrax As Integer = -1

            Dim y = New Double(dg_result.RowCount - 2) {}
            Dim x = New Double(dg_result.RowCount - 2) {}
            Array.Clear(y, 0, y.Length)
            Array.Clear(x, 0, x.Length)

            ' detect roles from dg_colunas
            For i2 As Integer = 0 To (dg_colunas.RowCount - 1)
                Dim axis = Convert.ToString(dg_colunas.Rows(i2).Cells(2).Value)
                Dim typ = Convert.ToString(dg_colunas.Rows(i2).Cells(1).Value)
                If axis = "X" Then
                    If typ = "Data" Then datacol = i2
                    If typ = "Hora" Then horacol = i2
                End If
                If axis = "Y Categorizado" Then categcol = i2
                If axis = "Y Linha Fixa (Max)" Then linhasycol = i2
                If axis = "X Sombra" Then sombrax = i2
            Next

            ' X values
            For i2 As Integer = 0 To (dg_colunas.RowCount - 1)
                If Convert.ToString(dg_colunas.Rows(i2).Cells(2).Value) = "X" Then
                    For i As Integer = 0 To (dg_result.RowCount - 2)
                        If processo.CancellationPending = True Then Exit Sub
                        processo.ReportProgress(CInt(((i + 1) / Math.Max(1, (dg_result.RowCount - 1))) * 100))

                        If datacol = -1 AndAlso horacol = -1 AndAlso Convert.ToString(dg_colunas.Rows(i2).Cells(1).Value) = "DataHora" AndAlso dg_result.Rows(i).Cells(i2).Value IsNot Nothing Then
                            Dim dates As DateTime
                            x(i) = Nothing
                            Dim str As String = Convert.ToString(dg_result.Rows(i).Cells(i2).Value).Replace("-", " ").Replace(".", ":")
                            dates = Convert.ToDateTime(str)
                            x(i) = dates.ToOADate()
                            'FormsPlot1.Plot.DateTimeTicksBottom()
                            FormsPlot1.Plot.Axes.Bottom.TickGenerator = New DateTimeAutomatic()
                        ElseIf Convert.ToString(dg_colunas.Rows(i2).Cells(1).Value) = "Inteiro" AndAlso dg_result.Rows(i).Cells(i2).Value IsNot Nothing Then
                            Dim dou As Double
                            x(i) = Nothing
                            Dim str As String = Convert.ToString(dg_result.Rows(i).Cells(i2).Value)
                            dou = Math.Round(CDbl(con.ConvertFrom(str)))
                            x(i) = dou
                        ElseIf datacol > -1 AndAlso horacol > -1 Then
                            Dim dates As DateTime
                            x(i) = Nothing
                            Dim sData As String = Convert.ToString(dg_result.Rows(i).Cells(datacol).Value).Replace("-", " ").Replace(".", ":")
                            Dim sHora As String = Convert.ToString(dg_result.Rows(i).Cells(horacol).Value).Replace(".", ":")
                            Dim str As String = (sData & " " & sHora)
                            dates = Convert.ToDateTime(str)
                            x(i) = dates.ToOADate()
                            'FormsPlot1.Plot.Axes.DateTimeTicks(Edge.Bottom)
                            FormsPlot1.Plot.Axes.Bottom.TickGenerator = New DateTimeAutomatic()
                        End If
                    Next
                End If
            Next

            ' Y single
            For i2 As Integer = 0 To (dg_colunas.RowCount - 1)
                If Convert.ToString(dg_colunas.Rows(i2).Cells(2).Value) = "Y" AndAlso categcol = -1 Then
                    For i As Integer = 0 To (dg_result.RowCount - 2)
                        processo.ReportProgress(CInt(((i + 1) / Math.Max(1, (dg_result.RowCount - 1))) * 100))
                        If processo.CancellationPending = True Then Exit Sub

                        If Convert.ToString(dg_colunas.Rows(i2).Cells(1).Value) = "Decimal" AndAlso dg_result.Rows(i).Cells(i2).Value IsNot Nothing Then
                            Dim dou As Double
                            y(i) = Nothing
                            Dim str As String = Convert.ToString(dg_result.Rows(i).Cells(i2).Value)
                            dou = CDbl(con.ConvertFrom(str))
                            y(i) = dou
                        ElseIf Convert.ToString(dg_colunas.Rows(i2).Cells(1).Value) = "Inteiro" AndAlso dg_result.Rows(i).Cells(i2).Value IsNot Nothing Then
                            Dim dou As Double
                            y(i) = Nothing
                            Dim str As String = Convert.ToString(dg_result.Rows(i).Cells(i2).Value)
                            dou = Math.Round(CDbl(con.ConvertFrom(str)))
                            y(i) = dou
                        End If
                    Next

                    If x.Length > 0 AndAlso y.Length > 0 Then
                        FormsPlot1.Plot.Axes.SetLimits(x.Min(), x.Max(), y.Min(), y.Min())
                        FormsPlot1.Plot.Add.Scatter(x, y).LegendText = "Busy"
                        FormsPlot1.Plot.Legend.IsVisible = True
                    End If
                    Exit For
                End If
            Next

            ' Y categorized
            For i2 As Integer = 0 To (dg_colunas.RowCount - 1)
                If Convert.ToString(dg_colunas.Rows(i2).Cells(2).Value) = "Y Categorizado" AndAlso categcol > -1 Then
                    Dim LINHAY As Integer = -1
                    For i3 As Integer = 0 To (dg_colunas.RowCount - 1)
                        If Convert.ToString(dg_colunas.Rows(i3).Cells(2).Value) = "Y" Then
                            LINHAY = i3
                            Exit For
                        End If
                    Next

                    Dim categoria() = New String(dg_result.RowCount - 2) {}
                    For i3 As Integer = 0 To (dg_result.RowCount - 2)
                        categoria(i3) = Convert.ToString(dg_result.Rows(i3).Cells(i2).Value)
                    Next
                    Dim categoriafiltrada() As String = categoria.Distinct().ToArray()

                    For i3 As Integer = 0 To (categoriafiltrada.Length - 1)
                        Dim y2 = New Double(dg_result.RowCount - 2) {}
                        For i4 As Integer = 0 To (dg_result.RowCount - 2)
                            If processo.CancellationPending = True Then Exit Sub
                            processo.ReportProgress(CInt(((i4 + 1) / Math.Max(1, (dg_result.RowCount - 1))) * 100))
                            y2(i4) = 0
                            If categoriafiltrada(i3) = Convert.ToString(dg_result.Rows(i4).Cells(i2).Value) AndAlso Convert.ToString(dg_colunas.Rows(i2).Cells(1).Value) = "Texto" Then
                                Dim dou As Double
                                Dim str As String = Convert.ToString(dg_result.Rows(i4).Cells(LINHAY).Value)
                                dou = CDbl(con.ConvertFrom(str))
                                y2(i4) = dou
                            End If
                        Next

                        Dim table As New System.Data.DataTable
                        table.Columns.Add("x")
                        table.Columns.Add("y")
                        For i4 As Integer = 0 To (y2.Length - 1)
                            table.Rows.Add(x(i4), y2(i4))
                        Next
                        For i4 As Integer = table.Rows.Count - 1 To 0 Step -1
                            If Convert.ToString(table.Rows(i4)("y")) = "0" Then
                                table.Rows.RemoveAt(i4)
                            End If
                        Next

                        If table.Rows.Count > 0 Then
                            Dim y3 = New Double(table.Rows.Count - 1) {}
                            Dim x3 = New Double(table.Rows.Count - 1) {}
                            For i4 As Integer = table.Rows.Count - 1 To 0 Step -1
                                y3(i4) = CDbl(table.Rows(i4)("y"))
                                x3(i4) = CDbl(table.Rows(i4)("x"))
                            Next
                            FormsPlot1.Plot.Axes.SetLimits(x3.Min(), x3.Max(), y3.Min(), y3.Min())
                            FormsPlot1.Plot.Add.Scatter(x3, y3).LegendText = categoriafiltrada(i3)
                            FormsPlot1.Plot.Legend.IsVisible = True
                        End If
                    Next
                    Exit For
                End If
            Next

            ' Y fixed line (max)
            For i2 As Integer = 0 To (dg_colunas.RowCount - 1)
                If Convert.ToString(dg_colunas.Rows(i2).Cells(2).Value) = "Y Linha Fixa (Max)" AndAlso linhasycol > -1 Then
                    Dim vals = New Double(dg_result.RowCount - 2) {}
                    For i3 As Integer = 0 To (dg_result.RowCount - 2)
                        Dim s As String = Convert.ToString(dg_result.Rows(i3).Cells(i2).Value)
                        Dim v As Double
                        Double.TryParse(s, v)
                        vals(i3) = v
                    Next
                    Dim maxV = vals.Max()
                    Dim linhafx = FormsPlot1.Plot.Add.HorizontalLine(maxV)
                    linhafx.Text = Convert.ToString(dg_colunas.Rows(i2).Cells(0).Value)
                    linhafx.LinePattern = LinePattern.Dashed
                    linhafx.LabelOppositeAxis = True
                End If
            Next

            ' X shadow (band)
            For i2 As Integer = 0 To (dg_colunas.RowCount - 1)
                If Convert.ToString(dg_colunas.Rows(i2).Cells(2).Value) = "X Sombra" AndAlso sombrax > -1 Then
                    Dim v1 = New Double(dg_result.RowCount - 2) {}
                    Dim v2 = New Double(dg_result.RowCount - 2) {}
                    For i3 As Integer = 0 To (dg_result.RowCount - 2)
                        Dim s As String = Convert.ToString(dg_result.Rows(i3).Cells(i2).Value)
                        Dim vv As Double
                        Double.TryParse(s, vv)
                        v2(i3) = vv
                        v1(i3) = 0
                    Next
                    If x.Length <> 0 OrElse v1.Length <> 0 OrElse v2.Length <> 0 Then
                        Dim sombra = FormsPlot1.Plot.Add.FillY(x, v1, v2)
                        sombra.LineStyle.IsVisible = True
                        sombra.LineStyle.Pattern = LinePattern.Dashed
                        sombra.LineStyle.Width = 1
                        sombra.LineStyle.Color = Colors.Yellow
                        sombra.LineStyle.AntiAlias = False
                        sombra.LegendText = Convert.ToString(dg_colunas.Rows(i2).Cells(0).Value)
                        sombra.FillStyle.Color = Colors.Yellow.WithAlpha(0.1)
                        sombra.IsVisible = True
                    End If
                End If
            Next

            If CheckBox1.Checked = True Then
                FormsPlot1.Plot.ShowLegend()
            Else
                FormsPlot1.Plot.HideLegend()
            End If

            FormsPlot1.Plot.Axes.Bottom.Label.Text = TextBox3.Text
            FormsPlot1.Plot.Axes.Left.Label.Text = TextBox2.Text
            FormsPlot1.Plot.Title(TextBox1.Text)

            ApplyScottPlotDarkMode()

            ' axis and legend colors
            FormsPlot1.Plot.Axes.Bottom.TickLabelStyle.ForeColor = ScottPlot.Color.FromHex("#d7d7d7")
            FormsPlot1.Plot.Axes.Left.TickLabelStyle.ForeColor = ScottPlot.Color.FromHex("#d7d7d7")
            FormsPlot1.Plot.Legend.BackgroundColor = ScottPlot.Color.FromHex("#404040")
            FormsPlot1.Plot.Legend.FontColor = ScottPlot.Color.FromHex("#d7d7d7")

            FormsPlot1.Refresh()

        Catch ex As Exception
            MsgBox("Error - " & ex.Message.ToString(), MsgBoxStyle.Exclamation, "Error")
        End Try
    End Sub

    Private Sub Exportar(ByVal processo As BackgroundWorker)
        If Me.dg_result.Rows.Count <= 0 Then Return
        Try
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
                If processo.CancellationPending = True Then
                    Exit Sub
                End If
                processo.ReportProgress(CInt(((index1 + 1) / Math.Max(1, num1)) * 100))
                Dim num2 As Integer = Me.dg_result.Columns.Count - 1
                Dim index2 = 0
                While index2 <= num2
                    Me.XcelApp.Cells(index1 + 2, index2 + 1) = CObj(Convert.ToString(Me.dg_result.Rows(index1).Cells(index2).Value))
                    Interlocked.Increment(index2)
                End While
                Interlocked.Increment(index1)
            End While

            Me.XcelApp.Columns.AutoFit()
            Me.XcelApp.Visible = True
        Catch ex As Exception
            MessageBox.Show("Error : " & ex.Message)
            Me.XcelApp.Quit()
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If BackgroundWorker4.IsBusy = True Then
            BackgroundWorker4.CancelAsync()
            Exit Sub
        End If

        Me.lbl_status.Text = "Exporting..."
        Me.ToolStripProgressBar1.Value = 0
        BackgroundWorker4.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker4_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker4.DoWork
        Exportar(BackgroundWorker4)
    End Sub

    Private Sub BackgroundWorker4_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker4.RunWorkerCompleted
        If (e.Cancelled = True) Then
            MsgBox("Export canceled.", MsgBoxStyle.Information, "Canceled")
            Me.lbl_status.Text = "Canceled."
            Me.ToolStripProgressBar1.Value = 0
        Else
            Me.lbl_status.Text = "Values exported."
            Me.ToolStripProgressBar1.Value = 100
        End If
    End Sub

    Private Sub BackgroundWorker4_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles BackgroundWorker4.ProgressChanged
        ToolStripProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub ExtrairDaPCOMMToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExtrairDaPCOMMToolStripMenuItem.Click
        txt_entrada.Text = Application.StartupPath.ToString() & "Buffer.txt"
        pccom.Show()
    End Sub

    ' ---- runtime-wired handlers (no Handles keyword) ----
    Private Sub dg_linhas_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs)
        AtualizarGridChart()
    End Sub

    Private Sub dg_table_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs)
        AtualizarGridChart()
    End Sub

    Private Sub dg_table_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs)
        AtualizarGridChart()
    End Sub

    Private Sub dg_table_RowLeave(sender As Object, e As DataGridViewCellEventArgs)
        AtualizarGridChart()
    End Sub

    Private Sub dg_linhas_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs)
        AtualizarGridChart()
    End Sub

    Private Sub dg_linhas_RowLeave(sender As Object, e As DataGridViewCellEventArgs)
        AtualizarGridChart()
    End Sub

    Private Sub AtualizarGridChart()
        dg_colunas.Rows.Clear()
        If dg_colunas.Columns.Count > 2 Then
            For i As Integer = 0 To (dg_linhas.RowCount - 2)
                dg_colunas.Rows.Add(dg_linhas.Rows(i).Cells(0).Value, dg_linhas.Rows(i).Cells(5).Value, dg_linhas.Rows(i).Cells(6).Value)
            Next

            For i As Integer = 0 To (dg_table.RowCount - 2)
                dg_colunas.Rows.Add(dg_table.Rows(i).Cells(0).Value, dg_table.Rows(i).Cells(5).Value, dg_table.Rows(i).Cells(6).Value)
            Next

            For i As Integer = 0 To (dg_colunas.RowCount - 1)
                If Convert.ToString(dg_colunas.Rows(i).Cells(2).Value) = "X" Then
                    TextBox3.Text = Convert.ToString(dg_colunas.Rows(i).Cells(0).Value)
                    Exit For
                End If
            Next

            For i As Integer = 0 To (dg_colunas.RowCount - 1)
                If Convert.ToString(dg_colunas.Rows(i).Cells(2).Value) = "Y" Then
                    TextBox2.Text = Convert.ToString(dg_colunas.Rows(i).Cells(0).Value)
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' intentionally left minimal
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If pccom.NumericUpDown2.Value <> 0 AndAlso pccom.CheckBox1.Checked = True AndAlso pccom.BackgroundWorker1.IsBusy = True Then
            Dim tmp As Integer
            If Integer.TryParse(txttime.Text, tmp) AndAlso tmp > 0 Then
                txttime.Text = (tmp - 1).ToString()
            End If
        End If
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        dg_linhas.Rows.Clear()
        dg_table.Rows.Clear()
        txtseparador.Text = "  RMF "
        Me.dg_linhas.Rows.Add("DateTime", "3", "38", "8", "0", "Data", "X", "Sem operação")
        Me.dg_linhas.Rows.Add("Hora", "3", "54", "8", "0", "Hora", "X", "Sem operação")
        Me.dg_linhas.Rows.Add("System", "3", "26", "4", "0", "Texto", "Não Aplicado", "Sem operação")
        Me.dg_linhas.Rows.Add("Range", "3", "71", "3", "0", "Decimal", "Não Aplicado", "Sem operação")
        Me.dg_linhas.Rows.Add("CPUModel", "5", "36", "3", "0", "Decimal", "Não Aplicado", "Sem operação")
        Me.dg_linhas.Rows.Add("zModel", "5", "25", "5", "0", "Decimal", "Não Aplicado", "Sem operação")
        Me.dg_linhas.Rows.Add("CPCCpct", "6", "18", "4", "0", "Decimal", "Não Aplicado", "Sem operação")
        Me.dg_linhas.Rows.Add("4HRAAVG", "6", "56", "5", "0", "Decimal", "Não Aplicado", "Sem operação")
        Me.dg_linhas.Rows.Add("ImageCpct", "7", "16", "6", "0", "Decimal", "Não Aplicado", "Sem operação")
        Me.dg_linhas.Rows.Add("WLMCapp", "7", "41", "5", "0", "Decimal", "Não Aplicado", "Sem operação")
        Me.dg_table.Rows.Add("LPAR", "14", "30", "1", "8", "Texto", "Y Categorizado", "", "Sem operação")
        Me.dg_table.Rows.Add("MSU", "14", "30", "19", "4", "Decimal", "Y", "", "Sem operação")
    End Sub

    Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton5.CheckedChanged
        dg_linhas.Rows.Clear()
        dg_table.Rows.Clear()
        txtseparador.Text = "  RMF "
        Me.dg_linhas.Rows.Add("DateTime", "3", "38", "8", "0", "Data", "X", "Sem operação")
        Me.dg_linhas.Rows.Add("Hora", "3", "54", "8", "0", "Hora", "X", "Sem operação")
        Me.dg_linhas.Rows.Add("System", "3", "26", "4", "0", "Texto", "Não Aplicado", "Sem operação")
        Me.dg_linhas.Rows.Add("Range", "3", "71", "3", "0", "Decimal", "Não Aplicado", "Sem operação")
        Me.dg_table.Rows.Add("Job", "8", "300", "1", "8", "Texto", "Y Categorizado", "", "Sem operação")
        Me.dg_table.Rows.Add("Busy", "8", "300", "23", "6", "Decimal", "Y", "", "Sem operação")
        Me.dg_table.Rows.Add("ServClass", "8", "300", "13", "8", "Texto", "Não Aplicado", "", "Sem operação")
        Me.dg_table.Rows.Add("SX", "8", "300", "10", "2", "Texto", "Não Aplicado", "", "Sem operação")
    End Sub

    Private Sub RadioButton6_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton6.CheckedChanged
        dg_linhas.Rows.Clear()
        dg_table.Rows.Clear()
        txtseparador.Text = "  RMF "
        Me.dg_linhas.Rows.Add("DateTime", "3", "38", "8", "0", "Data", "X", "Sem operação")
        Me.dg_linhas.Rows.Add("Hora", "3", "54", "8", "0", "Hora", "X", "Sem operação")
        Me.dg_linhas.Rows.Add("System", "3", "26", "4", "0", "Texto", "Não Aplicado", "Sem operação")
        Me.dg_linhas.Rows.Add("Range", "3", "71", "3", "0", "Decimal", "Não Aplicado", "Sem operação")

        Me.dg_table.Rows.Add("ID", "8", "40", "1", "2", "Texto", "Y Categorizado", "", "Sem operação")
        Me.dg_table.Rows.Add("Type", "8", "40", "11", "4", "Decimal", "Não Aplicado", "", "Sem operação")
        Me.dg_table.Rows.Add("Part%", "8", "40", "21", "4", "Texto", "Y", "", "Sem operação")
        Me.dg_table.Rows.Add("Tot%", "8", "40", "26", "4", "Texto", "Não Aplicado", "", "Sem operação")
    End Sub

    Private Sub RadioButton7_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton7.CheckedChanged
        dg_linhas.Rows.Clear()
        dg_table.Rows.Clear()
        txtseparador.Text = "  RMF "
        Me.dg_linhas.Rows.Add("DateTime", "3", "38", "8", "0", "Data", "X", "Sem operação")
        Me.dg_linhas.Rows.Add("Hora", "3", "54", "8", "0", "Hora", "X", "Sem operação")
        Me.dg_linhas.Rows.Add("System", "3", "26", "4", "0", "Texto", "Não Aplicado", "Sem operação")
        Me.dg_linhas.Rows.Add("Range", "3", "71", "3", "0", "Decimal", "Não Aplicado", "Sem operação")
        Me.dg_linhas.Rows.Add("Kernel", "5", "18", "11", "0", "Texto", "Não Aplicado", "Sem operação")
        Me.dg_linhas.Rows.Add("BPXPRM", "6", "8", "40", "0", "Texto", "Não Aplicado", "Sem operação")

        Me.dg_table.Rows.Add("Job", "11", "300", "1", "8", "Texto", "Y Categorizado", "", "Sem operação")
        Me.dg_table.Rows.Add("User", "11", "300", "11", "8", "Texto", "Não Aplicado", "", "Sem operação")
        Me.dg_table.Rows.Add("ASID", "11", "300", "21", "4", "Texto", "Não Aplicado", "", "Sem operação")
        Me.dg_table.Rows.Add("Apply%", "11", "300", "60", "5", "Decimal", "Não Aplicado", "", "Sem operação")
        Me.dg_table.Rows.Add("TotalSRB", "11", "300", "67", "5", "Decimal", "Y", "", "Sem operação")
        Me.dg_table.Rows.Add("Server", "11", "300", "75", "4", "Texto", "Não Aplicado", "", "Sem operação")
    End Sub

    Private Sub RadioButton8_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton8.CheckedChanged
        dg_linhas.Rows.Clear()
        dg_table.Rows.Clear()
        txtseparador.Text = "  RMF "
        Me.dg_linhas.Rows.Add("DateTime", "3", "40", "8", "0", "Data", "X", "Sem operação")
        Me.dg_linhas.Rows.Add("Hora", "3", "55", "8", "0", "Hora", "X", "Sem operação")
        Me.dg_linhas.Rows.Add("Samples", "3", "14", "4", "0", "Texto", "Não Aplicado", "Sem operação")
        Me.dg_linhas.Rows.Add("Policy", "8", "21", "20", "0", "Texto", "Não Aplicado", "Sem operação")
        Me.dg_linhas.Rows.Add("Activaded", "8", "57", "20", "0", "Texto", "Não Aplicado", "Sem operação")

        Me.dg_table.Rows.Add("Name", "14", "300", "1", "8", "Texto", "Y Categorizado", "", "Sem operação")
        Me.dg_table.Rows.Add("ExecGoal", "14", "300", "16", "4", "Decimal", "Não Aplicado", "", "Sem operação")
        Me.dg_table.Rows.Add("VelAct", "14", "300", "21", "3", "Decimal", "Não Aplicado", "", "Sem operação")
        Me.dg_table.Rows.Add("RespTimeGoal", "14", "300", "31", "3", "Decimal", "Não Aplicado", "", "Sem operação")
        Me.dg_table.Rows.Add("RespTimeActual", "14", "300", "43", "3", "Decimal", "Não Aplicado", "", "Sem operação")
        Me.dg_table.Rows.Add("PerfIndx", "14", "300", "49", "4", "Decimal", "Y", "", "Sem operação")
        Me.dg_table.Rows.Add("TransEnded", "14", "300", "55", "5", "Decimal", "Não Aplicado", "", "Sem operação")
        Me.dg_table.Rows.Add("AvgWait", "14", "300", "61", "5", "Decimal", "Não Aplicado", "", "Sem operação")
        Me.dg_table.Rows.Add("RespExec", "14", "300", "68", "5", "Decimal", "Não Aplicado", "", "Sem operação")
        Me.dg_table.Rows.Add("TimeActual", "14", "300", "75", "5", "Decimal", "Não Aplicado", "", "Sem operação")
    End Sub

End Class
