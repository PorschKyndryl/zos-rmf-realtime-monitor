Imports System.Runtime.CompilerServices
Imports System.Threading
Imports System.IO
Imports Zuby.ADGV
Imports System.ComponentModel
Imports ScottPlot
Imports System.Buffers

Public Class Form1

    ' Create an Excel Application instance.
    Private XcelApp As New Microsoft.Office.Interop.Excel.Application  ' TODO: Document purpose and lifecycle.
    ' Global table used as data buffer for imports and exports.
    Dim globaltable As New System.Data.DataTable  ' TODO: Document purpose and lifecycle.
    ' BindingSource to link the global DataTable with DataGridView controls.
    Dim bsglobal As New BindingSource  ' TODO: Document purpose and lifecycle.
    ' Instance of communication form for PCOMM capture.
    Public pccom As New frm_pcomm  ' TODO: Document purpose and lifecycle.
    ' Temporary grid used to store columns for the chart.
    Dim dg_colunas = New DataGridView()

    ' Call this after you add your plottables
    ''' <summary>
    ''' ApplyScottPlotDarkMode procedure. Applies a dark theme style to the ScottPlot chart area.
    ''' </summary>
    Private Sub ApplyScottPlotDarkMode()
        ' figure & data backgrounds
        FormsPlot1.Plot.FigureBackground.Color = ScottPlot.Color.FromHex("#181818")
        FormsPlot1.Plot.DataBackground.Color = ScottPlot.Color.FromHex("#1f1f1f")

        ' axes (ticks, labels, frames)
        FormsPlot1.Plot.Axes.Color(ScottPlot.Color.FromHex("#d7d7d7"))

        ' grid
        FormsPlot1.Plot.Grid.MajorLineColor = ScottPlot.Color.FromHex("#404040")
        ' (optional) minor grid if you enable it somewhere:
        ' FormsPlot1.Plot.Grid.MinorLineColor = ScottPlot.Color.FromHex("#303030")

        ' legend (if used)
        FormsPlot1.Plot.Legend.BackgroundColor = ScottPlot.Color.FromHex("#404040")
        FormsPlot1.Plot.Legend.FontColor = ScottPlot.Color.FromHex("#d7d7d7")
        FormsPlot1.Plot.Legend.OutlineColor = ScottPlot.Color.FromHex("#d7d7d7")
    End Sub

    ''' <summary>
    ''' CreateDGV_Chart procedure. Creates and configures a DataGridView to display chart column configuration.
    ''' </summary>
    Private Sub CreateDGV_Chart()

        'CType(dg_colunas, ComponentModel.ISupportInitialize).BeginInit()

        ' 
        ' dg_colunas
        ' 
        dg_colunas.AllowUserToAddRows = False
        dg_colunas.AllowUserToDeleteRows = False
        dg_colunas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dg_colunas.Columns.AddRange(New DataGridViewColumn() {DataGridViewTextBoxColumn6, col_type, col_eixe})
        'dg_colunas.Dock = DockStyle.Fill
        'dg_colunas.Location = New Point(3, 2)
        'dg_colunas.Margin = New Padding(3, 2, 3, 2)
        dg_colunas.MultiSelect = False
        dg_colunas.Name = "dg_colunas"
        dg_colunas.RowHeadersWidth = 30
        dg_colunas.RowTemplate.Height = 29
        dg_colunas.Size = New Size(731, 601)
        dg_colunas.TabIndex = 4
        ' 
        ' DataGridViewTextBoxColumn6
        ' 
        DataGridViewTextBoxColumn6.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DataGridViewTextBoxColumn6.HeaderText = "Field Name"
        DataGridViewTextBoxColumn6.MinimumWidth = 6
        DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        DataGridViewTextBoxColumn6.ReadOnly = True
        ' 
        ' col_type
        ' 
        col_type.HeaderText = "Type"
        col_type.Items.AddRange(New Object() {"Date", "Time", "DateTime", "Integer", "Decimal", "Text"})
        col_type.MinimumWidth = 6
        col_type.Name = "col_type"
        col_type.Width = 125
        ' 
        ' col_eixe
        ' 
        col_eixe.HeaderText = "Axis"
        col_eixe.Items.AddRange(New Object() {"Not Applied", "X", "Y", "Y Categorized", "Y Fixed Line (Max)", "X Shadow"})
        col_eixe.MinimumWidth = 6
        col_eixe.Name = "col_eixe"
        col_eixe.Width = 125

        'CType(dg_colunas, ComponentModel.ISupportInitialize).EndInit()

    End Sub

    ''' <summary>
    ''' BackgroundWorker1_DoWork procedure. Executes the background import process asynchronously.
    ''' </summary>
    ''' <param name="sender">Sender object.</param>
    ''' <param name="e">DoWorkEventArgs parameter.</param>
    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork  ' TODO: Document purpose and lifecycle.

        If BackgroundWorker1.CancellationPending = True Then
            e.Cancel = True
            Exit Sub
        End If
        Importar(BackgroundWorker1, txt_entrada.Text)

    End Sub

    ''' <summary>
    ''' BackgroundWorker1_RunWorkerCompleted procedure. Handles the event when the import process finishes.
    ''' </summary>
    ''' <param name="sender">Sender object.</param>
    ''' <param name="e">RunWorkerCompletedEventArgs parameter.</param>
    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted  ' TODO: Document purpose and lifecycle.

        If (e.Cancelled = True) Then
            MsgBox("Import canceled.", MsgBoxStyle.Information, "Canceled")
            Me.lbl_status.Text = "Canceled."
            Me.ToolStripProgressBar1.Value = 0
        Else

            Me.lbl_status.Text = "Read."
            bsglobal.DataSource = globaltable
            dg_result.DataSource = bsglobal.DataSource
            Dim num1 = 0
            Dim columnCount2 = Me.dg_result.ColumnCount
            While columnCount2 > num1
                Me.dg_result.Columns(num1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                Interlocked.Increment(num1)
            End While
            dg_result.Refresh()

            lblresult.Text = (dg_result.Rows.Count - 1)

            If pccom.CheckBox1.Checked = True Then
                Me.lbl_status.Text = "Removing Duplicates..."
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

    ''' <summary>
    ''' BackgroundWorker1_ProgressChanged procedure. Updates the UI progress bar based on import progress.
    ''' </summary>
    ''' <param name="sender">Sender object.</param>
    ''' <param name="e">ProgressChangedEventArgs parameter.</param>
    Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged  ' TODO: Document purpose and lifecycle.
        ToolStripProgressBar1.Value = e.ProgressPercentage
    End Sub

    ''' <summary>
    ''' Importar procedure. Handles the logic for reading, parsing, and populating the global table from a text file.
    ''' </summary>
    ''' <param name="processo">BackgroundWorker process reference.</param>
    ''' <param name="arquivo">File path to import.</param>
    Private Sub Importar(ByVal processo As BackgroundWorker, ByVal arquivo As String)  ' TODO: Document purpose and lifecycle.

        Try

            'Read total number of lines in the file
            Dim linhas_totais As Integer = 0
            Dim streamReader2 = File.OpenText(arquivo)
            Dim str2 As String = streamReader2.ReadLine()
            While Not streamReader2.EndOfStream
                str2 = streamReader2.ReadLine()
                Interlocked.Increment(linhas_totais)
            End While
            streamReader2.Close()
            '--------------------------------
            '-----------------------------------------
            'ARCHIVED - RADIOBUTTON2 WAS DEACTIVATED
            '-----------------------------------------

            'If Me.RadioButton2.Checked Then
            '
            '   [old logic removed for legacy compatibility]
            '
            'End If

            '-----------------------------------------
            'BEGIN EXTRACTION LOGIC
            '==========================
            '
            '*** ADD VALIDATION: IF THE FIELD DOES NOT MATCH THE DECLARED TYPE OR IS NULL, DISCARD THE ROW
            '
            '-----------------------------------------

            If Me.RadioButton3.Checked Then

                Me.lbl_status.Text = "Reading..."
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

                'Opens two streams: one for reading fixed fields and another for table fields
                'Reading occurs in two passes: 
                'READER - Fixed fields
                'READER2 - Table fields
                While Not streamReader.EndOfStream Or Not streamReader2.EndOfStream

                    'Checks if the process was canceled
                    If processo.CancellationPending = True Then
                        Exit Sub
                    End If

                    'End of file validation
                    If IsNothing(str) Or IsNothing(str2) Then
                        Exit While
                    End If

                    'Creates a new temporary header DataTable
                    Dim tablecabeca As New System.Data.DataTable
                    tablecabeca.Clear()
                    tablecabeca.Columns.Add()

                    '---------------------------------------------------------------------------
                    'Determine how many fields will be captured in table format (dg_table)
                    '---------------------------------------------------------------------------

                    'Verifies whether to process fixed + table fields or only fixed fields
                    If dg_table.Rows.Count > 1 Then
                        '------------------------------------------

                        'There are two readers because reading is done in two passes
                        'STR  - reads fixed fields
                        'STR2 - reads table fields

                        If str.Contains(txtseparador.Text) And str <> Nothing And dg_table.Rows.Count > 1 Then

                            poslinha = 1
                            str = streamReader.ReadLine()
                            Interlocked.Increment(num_linha)

                            'This loop reads each line of the file to extract fixed fields first
                            While Not str.Contains(txtseparador.Text) And Not streamReader.EndOfStream

                                Interlocked.Increment(pag)
                                linhagrid = 0

                                'Loop through each fixed-field definition from dg_linhas
                                While linhagrid < Me.dg_linhas.RowCount - 1
                                    If Me.dg_linhas.Rows(linhagrid).Cells(1).Value = poslinha And str.Length >= (Convert.ToInt64(Me.dg_linhas.Rows(linhagrid).Cells(2).Value) + Convert.ToInt64(Me.dg_linhas.Rows(linhagrid).Cells(3).Value)) Then
                                        'Type validation check
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

                        '---------------------------------------------------------------------------
                        'Extracts table-style fields (str2)
                        '---------------------------------------------------------------------------
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
                                        'Verify line positions and string length
                                        If IsNumeric(Me.dg_table.Rows(linhagrid2).Cells(2).Value) Then

                                            Dim filter As String() = Array.Empty(Of String)()

                                            If filter IsNot Nothing AndAlso filter.Length > 0 Then
                                                Array.Clear(filter, 0, filter.Length)
                                            End If

                                            If Trim(Convert.ToString(Me.dg_table.Rows(linhagrid2).Cells(7).Value)) <> "" Then
                                                filter = Trim(Convert.ToString(Me.dg_table.Rows(linhagrid2).Cells(7).Value)).Split(";"c)
                                            End If

                                            'Verify that the line is within the desired table range
                                            If (Me.dg_table.Rows(linhagrid2).Cells(1).Value <= poslinha2 And Me.dg_table.Rows(linhagrid2).Cells(2).Value >= poslinha2) And str2.Length >= (Convert.ToInt64(Me.dg_table.Rows(linhagrid2).Cells(3).Value) + Convert.ToInt64(Me.dg_table.Rows(linhagrid2).Cells(4).Value)) Then

                                                If linhagrid2 > 0 Then
                                                    'Insert value in existing row (non-first field)
                                                    If ValidateType(New Object(0) {CObj(Trim(str2.Substring(Convert.ToInt64(Me.dg_table.Rows(CInt(linhagrid2)).Cells(CInt(3)).Value), Convert.ToInt64(Me.dg_table.Rows(CInt(linhagrid2)).Cells(CInt(4)).Value)).ToString()))}, Me.dg_table.Rows(linhagrid2).Cells(5).Value) = True Then

                                                        'Filter validation
                                                        If IsNothing(filter) = False Then
                                                            For Each word As String In filter
                                                                Dim STT As String = Trim(str2.Substring(Convert.ToInt64(Me.dg_table.Rows(CInt(linhagrid2)).Cells(CInt(3)).Value), Convert.ToInt64(Me.dg_table.Rows(CInt(linhagrid2)).Cells(CInt(4)).Value)).ToString())
                                                                If word = Nothing Then
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
                                                    'First field (creates row and adds fixed values)
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
                                                        If IsNothing(filter) = False Then
                                                            For Each word As String In filter
                                                                Dim STT As String = Trim(str2.Substring(Me.dg_table.Rows(linhagrid2).Cells(3).Value, Me.dg_table.Rows(linhagrid2).Cells(4).Value))
                                                                If word = Nothing Then
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
                        '---------------------------------------------------------------------------
                        'END - FIXED + TABLE FIELD EXTRACTION
                        '---------------------------------------------------------------------------

                    Else
                        '---------------------------------------------------------------------------
                        'ONLY FIXED FIELDS (NO TABLE)
                        '---------------------------------------------------------------------------
                        If str.Contains(txtseparador.Text) And str <> Nothing And dg_table.Rows.Count = 1 Then

                            poslinha = 1
                            str = streamReader.ReadLine()
                            str2 = streamReader2.ReadLine()
                            Interlocked.Increment(num_linha)

                            While Not str.Contains(txtseparador.Text) And Not streamReader.EndOfStream

                                Interlocked.Increment(pag)
                                linhagrid = 0

                                While linhagrid < Me.dg_linhas.RowCount - 1
                                    If Me.dg_linhas.Rows(linhagrid).Cells(1).Value = poslinha And str.Length >= (Convert.ToInt64(Me.dg_linhas.Rows(linhagrid).Cells(2).Value) + Convert.ToInt64(Me.dg_linhas.Rows(linhagrid).Cells(3).Value)) Then

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
                        '---------------------------------------------------------------------------
                        'END - FIXED FIELDS ONLY
                        '---------------------------------------------------------------------------
                    End If

                    '---------------------------------------------------------------------------
                    'NEXT LINE READING CONTROL
                    '---------------------------------------------------------------------------
                    If Not str.Contains(txtseparador.Text) And Not str2.Contains(txtseparador.Text) And Not streamReader.EndOfStream And Not streamReader2.EndOfStream Then
                        str = streamReader.ReadLine()
                        str2 = streamReader2.ReadLine()
                        Interlocked.Increment(num_linha)
                    End If

                    BackgroundWorker1.ReportProgress((num_linha / linhas_totais) * 100)
                End While

                streamReader.Close()
                streamReader2.Close()
                Me.lbl_paglida.Text = pag.ToString()
            End If

            '-----------------------------------------
            'END OF EXTRACTION
            '-----------------------------------------

            '-----------------------------------------
            'VALIDATION AND REMOVAL OF NULL/EMPTY ROWS
            '-----------------------------------------

            Me.lbl_status.Text = "Removing rows with null or inconsistent fields..."

            For rowIndex As Integer = globaltable.Rows.Count - 1 To 0 Step -1
                Dim row As DataRow = globaltable.Rows(rowIndex)
                Dim isEmpty As Boolean = False
                For Each column As DataColumn In globaltable.Columns
                    If String.IsNullOrEmpty(row(column)) Or Trim(row(column)) = "" Then
                        isEmpty = True
                        Exit For
                    End If
                Next
                If isEmpty Then
                    globaltable.Rows.RemoveAt(rowIndex)
                End If
            Next

            '-----------------------------------------
            'ADDITIONAL FEATURE - GROUP BY PREPARATION
            '-----------------------------------------

            Dim groupby As New System.Data.DataTable
            For rowindex As Integer = dg_linhas.Rows.Count - 1 To 0 Step -1
                If dg_linhas.Rows(rowindex).Cells(7).Value = "Group By" Then
                    groupby.Columns.Add(dg_linhas.Rows(rowindex).Cells(0).Value)
                End If
            Next
            For rowindex As Integer = dg_table.Rows.Count - 1 To 0 Step -1
                If dg_table.Rows(rowindex).Cells(8).Value = "Group By" Then
                    groupby.Columns.Add(dg_table.Rows(rowindex).Cells(0).Value)
                End If
            Next

            Dim buffergrouped As New System.Data.DataTable
            For Each column As DataColumn In globaltable.Columns
                buffergrouped.Columns.Add(column.ColumnName, column.DataType)
            Next

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Exclamation, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' ValidateType function. Validates the type of a given variable against an expected data type.
    ''' </summary>
    ''' <param name="variable">Value to validate.</param>
    ''' <param name="type">Expected data type (Date, Time, Decimal, etc.).</param>
    ''' <returns>Boolean value indicating if the type is valid.</returns>
    Private Function ValidateType(ByVal variable As Object, ByVal type As String) As Boolean

        '0-DateTime
        '1-Date
        '2-Time
        '3-Decimal
        '4-Text

        Try
            Dim result As Boolean
            Select Case type
                Case "DateTime"
                    Dim dateValue As DateTime
                    If DateTime.TryParse(variable(0).ToString().Replace(".", ":"), dateValue) Then
                        result = True
                    Else
                        result = False
                    End If

                Case "Date"
                    Dim dateValue As Date
                    If Date.TryParse(variable(0).ToString(), dateValue) Then
                        result = True
                    Else
                        result = False
                    End If

                Case "Time"
                    Dim timeSpan As TimeSpan
                    If TimeSpan.TryParse(variable(0).ToString().Replace(".", ":"), timeSpan) Then
                        result = True
                    Else
                        result = False
                    End If

                Case "Decimal"
                    Dim timeSpan As Decimal
                    If Decimal.TryParse(variable(0).ToString().Replace(".", ","), timeSpan) Then
                        result = True
                    Else
                        result = False
                    End If

                Case "Text"
                    If Trim(variable(0).ToString()) = "" Then
                        result = False
                    Else
                        result = True
                    End If
            End Select
            Return result
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' AbrirTXT procedure. Handles UI logic or side-effects.
    ''' </summary>
    ''' <param name="arquivo">File path.</param>
    ''' <param name="realtime">Run in realtime mode (auto-refresh) if true.</param>
    Public Sub AbrirTXT(ByVal arquivo As String, ByVal realtime As Boolean)  ' TODO: Document purpose and lifecycle.

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
            MessageBox.Show("The input, output, and number of lines per page must be filled.", "Blank field", MessageBoxButtons.OK)
            Me.txt_entrada.Focus()
            Exit Sub
        ElseIf Me.dg_linhas.RowCount = 0 Then
            MessageBox.Show("You need to add rows in the grid for the fields to be searched.", "Empty rows", MessageBoxButtons.OK)
            Me.dg_linhas.Focus()
            Exit Sub
        ElseIf File.Exists(arquivo) = False Then
            MessageBox.Show("The file you are trying to open does not exist.", "Empty rows", MessageBoxButtons.OK)
            Exit Sub
        End If

        If RadioButton3.Checked And txtseparador.Text = "" Then
            MessageBox.Show("The separator must be informed.", "Blank field", MessageBoxButtons.OK)
            Exit Sub
        End If

        If (RadioButton2.Checked And Not IsNumeric(txt_pag.Text)) Or (RadioButton2.Checked And Not IsNumeric(txtoffset.Text)) Then
            MessageBox.Show("Check the page size and the offset.", "Blank field", MessageBoxButtons.OK)
            Exit Sub
        End If

        If RadioButton3.Checked And dg_table.RowCount = 0 Then
            MessageBox.Show("Add rows to the Table Grid to search for tables in the file.", "Empty rows", MessageBoxButtons.OK)
            Exit Sub
        End If

        '============== SORT BOTH DATAGRIDs BY LINE NUMBER

        For Each dgvr As DataGridViewRow In dg_linhas.Rows
            Dim r As Integer = Convert.ToInt32(dgvr.Cells(1).Value)
            If dgvr.Cells(0).Value <> "" Then
                dgvr.Cells(1).Value = r
            End If
        Next

        For Each dgvr As DataGridViewRow In dg_table.Rows
            Dim r As Integer = Convert.ToInt32(dgvr.Cells(1).Value)
            If dgvr.Cells(0).Value <> "" Then
                dgvr.Cells(1).Value = r
            End If
        Next

        dg_linhas.Sort(dg_linhas.Columns(1), ListSortDirection.Ascending)
        dg_table.Sort(dg_table.Columns(1), ListSortDirection.Ascending)

        '===============

        If File.Exists(arquivo) Then
            Try
                Using myStreamReader As New System.IO.StreamReader(arquivo)
                    RichTextBox1.Text = myStreamReader.ReadToEnd()
                    RichTextBox1.SelectionStart = RichTextBox1.Text.Length
                    RichTextBox1.ScrollToCaret()
                End Using
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information)
            End Try
        End If

        bsglobal.Filter = Nothing
        bsglobal.Sort = Nothing

        dg_linhas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dg_table.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dg_result.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        'CLEAR TABLE AND GRID
        Me.lbl_status.Text = "Clearing..."
        Me.lblresult.Text = "0"
        Me.lbl_paglida.Text = "0"
        dg_result.DataSource = Nothing
        dg_result.Rows.Clear()
        globaltable.Rows.Clear()
        Me.ToolStripProgressBar1.Value = 0

        '---------- COLUMNS ----------
        Dim num1 = 0
        Dim columnCount As Integer = Me.dg_result.ColumnCount

        While columnCount > num1
            Me.dg_result.Columns.RemoveAt(0)
            Interlocked.Increment(num1)
        End While
        num1 = 0

        Dim columnCount2 As Integer = globaltable.Columns.Count
        While columnCount2 > num1
            globaltable.Columns.RemoveAt(0)
            Interlocked.Increment(num1)
        End While

        Dim index1 = 0
        While (Me.dg_linhas.RowCount - 1) > index1
            globaltable.Columns.Add(Me.dg_linhas.Rows(index1).Cells(0).Value.ToString())
            Interlocked.Increment(index1)
        End While

        If RadioButton3.Checked Then
            index1 = 0
            While (Me.dg_table.RowCount - 1) > index1
                globaltable.Columns.Add(Me.dg_table.Rows(index1).Cells(0).Value.ToString())
                Interlocked.Increment(index1)
            End While
        End If

        Me.dg_result.Refresh()
        Me.dg_result.RefreshEdit()

        Me.BackgroundWorker1.RunWorkerAsync()

    End Sub

    ''' <summary>
    ''' Btn_exec_Click procedure. Triggers the import process.
    ''' </summary>
    ''' <param name="sender">Sender.</param>
    ''' <param name="e">Event args.</param>
    Public Sub Btn_exec_Click(sender As Object, e As EventArgs) Handles btn_exec.Click  ' TODO: Document purpose and lifecycle.
        AbrirTXT(txt_entrada.Text, pccom.CheckBox1.Checked)
    End Sub

    ''' <summary>
    ''' Form1_Load procedure. Initializes default UI state and settings.
    ''' </summary>
    ''' <param name="sender">Sender.</param>
    ''' <param name="e">Event args.</param>
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load  ' TODO: Document purpose and lifecycle.
        txtseparador.Text = "RMF V2R4"

        dg_linhas.Rows.Clear()
        dg_table.Rows.Clear()

        CreateDGV_Chart()

        Me.txt_entrada.Text = "C:\Users\MatheusPorsch\Desktop\hardcopy\RMF CPC PROD1.txt"

        ApplyScottPlotDarkMode()

        If File.Exists(Application.StartupPath.ToString() & "\" & "Buffer.txt") Then
            IO.File.Delete(Application.StartupPath.ToString() & "\" & "Buffer.txt")
        End If

        'TabControl1_Selected(sender, e) 
    End Sub

    ''' <summary>
    ''' Dg_result_RowsAdded procedure. Updates result counter when rows are added.
    ''' </summary>
    Private Sub Dg_result_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs)  ' TODO: Document purpose and lifecycle.
        If Me.dg_result.RowCount - 1 > 0 Then
            Me.lblresult.Text = Convert.ToString(Me.dg_result.RowCount - 1)
        Else
            Me.lblresult.Text = "0"
        End If
    End Sub

    ''' <summary>
    ''' Dg_result_RowsRemoved procedure. Updates result counter when rows are removed and refreshes chart grid.
    ''' </summary>
    Private Sub Dg_result_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles dg_linhas.RowsRemoved  ' TODO: Document purpose and lifecycle.
        If Me.dg_result.RowCount - 1 > 0 Then
            Me.lblresult.Text = Convert.ToString(Me.dg_result.RowCount - 1)
        Else
            Me.lblresult.Text = "0"
        End If

        AtualizarGridChart()
    End Sub

    ''' <summary>
    ''' Button1_Click procedure. Opens a file dialog and loads the selected file path.
    ''' </summary>
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click  ' TODO: Document purpose and lifecycle.

        OpenFileDialog1.ShowDialog()
        Me.txt_entrada.Text = Me.OpenFileDialog1.FileName.ToString()
        If Me.OpenFileDialog1.CheckFileExists Then
            Return
        Else
            MessageBox.Show("The file does not exist.", "Not found", MessageBoxButtons.OK)
        End If

    End Sub

    ''' <summary>
    ''' GetDataTable function. Converts a DataGridView into a DataTable.
    ''' </summary>
    ''' <param name="pDataGridView">DataGridView source.</param>
    ''' <param name="pColumnNames">Include column names if True.</param>
    Private Function GetDataTable(ByVal pDataGridView As DataGridView, Optional ByVal pColumnNames As Boolean = True) As System.Data.DataTable  ' TODO: Document purpose and lifecycle.

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

    ''' <summary>
    ''' Dg_result_FilterStringChanged procedure. Applies filter to the BindingSource when grid filter changes.
    ''' </summary>
    Private Sub Dg_result_FilterStringChanged(sender As Object, e As Zuby.ADGV.AdvancedDataGridView.FilterEventArgs) Handles dg_result.FilterStringChanged  ' TODO: Document purpose and lifecycle.
        bsglobal.Filter = dg_result.FilterString
    End Sub

    ''' <summary>
    ''' Dg_result_SortStringChanged procedure. Applies sort to the BindingSource when grid sort changes.
    ''' </summary>
    Private Sub Dg_result_SortStringChanged(sender As Object, e As AdvancedDataGridView.SortEventArgs) Handles dg_result.SortStringChanged  ' TODO: Document purpose and lifecycle.
        bsglobal.Sort = dg_result.SortString
    End Sub

    ''' <summary>
    ''' Button3_Click procedure. Opens the selected file in Notepad and reloads it into the RichTextBox.
    ''' </summary>
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click  ' TODO: Document purpose and lifecycle.
        If File.Exists(txt_entrada.Text) = False Then
            MessageBox.Show("The file you are trying to open does not exist.", "Empty rows", MessageBoxButtons.OK)
            Exit Sub
        End If
        System.Diagnostics.Process.Start("notepad.exe", txt_entrada.Text)

        If File.Exists(txt_entrada.Text) Then
            Try
                Using myStreamReader As New System.IO.StreamReader(txt_entrada.Text)
                    RichTextBox1.Text = myStreamReader.ReadToEnd()
                    RichTextBox1.SelectionStart = RichTextBox1.Text.Length
                    RichTextBox1.ScrollToCaret()
                End Using
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' RadioButton3_CheckedChanged procedure. Toggles UI fields according to the selected extraction mode.
    ''' </summary>
    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged, RadioButton2.CheckedChanged, RadioButton1.CheckedChanged  ' TODO: Document purpose and lifecycle.
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

    ''' <summary>
    ''' SelecionarCampoToolStripMenuItem_Click procedure. Adds a new fixed-field row based on current selection in RichTextBox.
    ''' </summary>
    Private Sub SelecionarCampoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelecionarCampoToolStripMenuItem.Click  ' TODO: Document purpose and lifecycle.
        Me.dg_linhas.Rows.Add("Column_" & dg_linhas.RowCount,
                              (RichTextBox1.GetLineFromCharIndex(RichTextBox1.SelectionStart)).ToString(),
                              (RichTextBox1.SelectionStart - RichTextBox1.GetFirstCharIndexOfCurrentLine()).ToString(),
                              RichTextBox1.SelectionLength.ToString(),
                              "0")
    End Sub

    ''' <summary>
    ''' RichTextBox1_SelectionChanged procedure. Displays current selection info (line, position, length).
    ''' </summary>
    Private Sub RichTextBox1_SelectionChanged(sender As Object, e As EventArgs) Handles RichTextBox1.SelectionChanged  ' TODO: Document purpose and lifecycle.
        txtposi.Text = "Position: " & RichTextBox1.GetLineFromCharIndex(RichTextBox1.SelectionStart) &
                       " line \ " & (RichTextBox1.SelectionStart - RichTextBox1.GetFirstCharIndexOfCurrentLine()).ToString() &
                       " position \ " & RichTextBox1.SelectionLength.ToString() & " length"
    End Sub

    ''' <summary>
    ''' FormsPlot1_DoubleClick_1 procedure. Forces a refresh on double-click.
    ''' </summary>
    Private Sub FormsPlot1_DoubleClick_1(sender As Object, e As EventArgs) Handles FormsPlot1.DoubleClick  ' TODO: Document purpose and lifecycle.
        FormsPlot1.Refresh()
    End Sub

    ''' <summary>
    ''' TabControl1_Selected procedure. Reloads file into the RichTextBox when switching to the second tab.
    ''' </summary>
    Private Sub TabControl1_Selected(sender As Object, e As TabControlEventArgs) Handles TabControl1.Selected  ' TODO: Document purpose and lifecycle.

        If TabControl1.SelectedIndex = 1 Then
            If File.Exists(txt_entrada.Text) Then
                Try
                    Using myStreamReader As New System.IO.StreamReader(txt_entrada.Text)
                        RichTextBox1.Text = myStreamReader.ReadToEnd()
                        RichTextBox1.SelectionStart = RichTextBox1.Text.Length
                        RichTextBox1.ScrollToCaret()
                    End Using
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Information)
                End Try
            End If
        End If

    End Sub

    ''' <summary>
    ''' RemoverDuplicados procedure. Removes duplicate rows from a DataGridView by comparing cell values.
    ''' </summary>
    ''' <param name="dgvData">DataGridView to process.</param>
    ''' <param name="processo">Background worker to report progress/cancel.</param>
    Private Sub RemoverDuplicados(ByRef dgvData As DataGridView, ByVal processo As BackgroundWorker)  ' TODO: Document purpose and lifecycle.
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
                    If Not cell.Value.Equals(comparisonRow.Cells(cell.ColumnIndex).Value) Then
                        duplicate = False
                        Exit For
                    End If
                Next

                If duplicate AndAlso Not rowsToRemove.Contains(comparisonRow) Then
                    rowsToRemove.Add(comparisonRow)
                End If
            Next

            BackgroundWorker2.ReportProgress((i2 / dgvData.Rows.Count) * 100)
        Next

        For Each row As DataGridViewRow In rowsToRemove
            dgvData.Rows.Remove(row)
        Next

    End Sub

    ''' <summary>
    ''' Button4_Click procedure. Starts or cancels the duplicate removal process.
    ''' </summary>
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click  ' TODO: Document purpose and lifecycle.
        If BackgroundWorker2.IsBusy = True Then
            BackgroundWorker2.CancelAsync()
            Exit Sub
        End If

        If dg_result.Rows.Count > 0 Then
            Me.lbl_status.Text = "Removing Duplicates..."
            Me.ToolStripProgressBar1.Value = 0
            BackgroundWorker2.RunWorkerAsync()
        End If
    End Sub

    ''' <summary>
    ''' BackgroundWorker2_DoWork procedure. Invokes duplicate removal.
    ''' </summary>
    Private Sub BackgroundWorker2_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker2.DoWork  ' TODO: Document purpose and lifecycle.
        RemoverDuplicados(dg_result, BackgroundWorker2)
    End Sub

    ''' <summary>
    ''' BackgroundWorker2_RunWorkerCompleted procedure. Updates UI when duplicate removal finishes.
    ''' </summary>
    Private Sub BackgroundWorker2_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker2.RunWorkerCompleted  ' TODO: Document purpose and lifecycle.
        If (e.Cancelled = True) Then
            MsgBox("Removal canceled.", MsgBoxStyle.Information, "Canceled")
            Me.lbl_status.Text = "Canceled."
            Me.ToolStripProgressBar1.Value = 0
        Else

            Me.lbl_status.Text = "Duplicates Removed."
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

    ''' <summary>
    ''' BackgroundWorker2_ProgressChanged procedure. Updates progress bar during duplicate removal.
    ''' </summary>
    Private Sub BackgroundWorker2_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles BackgroundWorker2.ProgressChanged  ' TODO: Document purpose and lifecycle.
        ToolStripProgressBar1.Value = e.ProgressPercentage
    End Sub

    ''' <summary>
    ''' BackgroundWorker3_DoWork procedure. Performs plotting in background.
    ''' </summary>
    Private Sub BackgroundWorker3_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker3.DoWork  ' TODO: Document purpose and lifecycle.
        Plotar(BackgroundWorker3)
    End Sub

    ''' <summary>
    ''' BackgroundWorker3_RunWorkerCompleted procedure. Finalizes plotting.
    ''' </summary>
    Private Sub BackgroundWorker3_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker3.RunWorkerCompleted  ' TODO: Document purpose and lifecycle.
        If (e.Cancelled = True) Then
            MsgBox("Removal canceled.", MsgBoxStyle.Information, "Canceled")
            Me.lbl_status.Text = "Canceled."
            Me.ToolStripProgressBar1.Value = 0
        Else
            FormsPlot1.Refresh()
            Me.lbl_status.Text = "Chart Plotted."
            Me.ToolStripProgressBar1.Value = 100
        End If
    End Sub

    ''' <summary>
    ''' BackgroundWorker3_ProgressChanged procedure. Updates progress bar during plotting.
    ''' </summary>
    Private Sub BackgroundWorker3_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles BackgroundWorker3.ProgressChanged  ' TODO: Document purpose and lifecycle.
        ToolStripProgressBar1.Value = e.ProgressPercentage
    End Sub

    ''' <summary>
    ''' Button5_Click procedure. Starts plotting and normalizes chart mode when no categorized Y is present.
    ''' </summary>
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click  ' TODO: Document purpose and lifecycle.
        If BackgroundWorker3.IsBusy = True Then
            BackgroundWorker3.CancelAsync()
            Exit Sub
        End If

        'Creates chart when there is no categorized Y — only one Y axis
        For i2 As Integer = 0 To (dg_linhas.RowCount - 1)
            If dg_linhas.Rows(i2).Cells(6).Value = "Y" Then
                dg_table.Rows.Clear()
            End If
        Next

        Me.lbl_status.Text = "Plotting..."
        Me.ToolStripProgressBar1.Value = 0
        BackgroundWorker3.RunWorkerAsync()
    End Sub

    ''' <summary>
    ''' Plotar procedure. Builds the chart from dg_result according to the column configuration in dg_colunas.
    ''' </summary>
    ''' <param name="processo">Background worker to report progress/cancel.</param>
    Private Sub Plotar(ByVal processo As BackgroundWorker)  ' TODO: Document purpose and lifecycle.

        AtualizarGridChart()

        Try
            'https://scottplot.net/quickstart/vb/

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

            'Check X axis column types (Date, Time, DateTime, Integer)
            For i2 As Integer = 0 To (dg_colunas.RowCount - 1)
                If dg_colunas.Rows(i2).Cells(2).Value = "X" Then
                    If dg_colunas.Rows(i2).Cells(1).Value = "Date" Then
                        datacol = i2
                    End If
                    If dg_colunas.Rows(i2).Cells(1).Value = "Time" Then
                        horacol = i2
                    End If
                End If
                If dg_colunas.Rows(i2).Cells(2).Value = "Y Categorizado" OrElse dg_colunas.Rows(i2).Cells(2).Value = "Y Categorized" Then
                    categcol = i2
                End If
                If dg_colunas.Rows(i2).Cells(2).Value = "Y Linha Fixa (Max)" OrElse dg_colunas.Rows(i2).Cells(2).Value = "Y Fixed Line (Max)" Then
                    linhasycol = i2
                End If
                If dg_colunas.Rows(i2).Cells(2).Value = "X Sombra" OrElse dg_colunas.Rows(i2).Cells(2).Value = "X Shadow" Then
                    sombrax = i2
                End If
            Next

            'Build X values
            For i2 As Integer = 0 To (dg_colunas.RowCount - 1)
                If dg_colunas.Rows(i2).Cells(2).Value = "X" Then

                    For i As Integer = 0 To (dg_result.RowCount - 2)

                        If processo.CancellationPending = True Then
                            Exit Sub
                        End If

                        processo.ReportProgress(((i + 1) / (dg_result.RowCount - 1)) * 100)

                        If datacol = -1 And horacol = -1 And dg_colunas.Rows(i2).Cells(1).Value = "DateTime" And IsNothing(dg_result.Rows(i).Cells(3).Value.ToString()) = False Then

                            Dim dates As DateTime
                            x(i) = Nothing
                            Dim str As String = dg_result.Rows(i).Cells(i2).Value.ToString().Replace("-", " ").Replace(".", ":")
                            dates = Convert.ToDateTime(str)
                            x(i) = dates.ToOADate()
                            FormsPlot1.Plot.Axes.Bottom.TickGenerator = New ScottPlot.TickGenerators.DateTimeAutomatic()

                        ElseIf dg_colunas.Rows(i2).Cells(1).Value = "Integer" And IsNothing(dg_result.Rows(i).Cells(3).Value.ToString()) = False Then

                            Dim dou As Double
                            x(i) = Nothing
                            Dim str As String = dg_result.Rows(i).Cells(i2).Value.ToString()
                            dou = Math.Round(con.ConvertFrom(str))
                            x(i) = dou

                        ElseIf datacol > -1 And horacol > -1 Then

                            Dim dates As DateTime
                            x(i) = Nothing

                            Dim str As String = dg_result.Rows(i).Cells(datacol).Value.ToString().Replace("-", " ").Replace(".", ":").ToString() &
                                                 " " &
                                                 dg_result.Rows(i).Cells(horacol).Value.ToString().Replace(".", ":").ToString()
                            dates = Convert.ToDateTime(str)
                            x(i) = dates.ToOADate()
                            FormsPlot1.Plot.Axes.Bottom.TickGenerator = New ScottPlot.TickGenerators.DateTimeAutomatic()
                        End If
                    Next
                End If
            Next

            'Single Y (no categorized series)
            For i2 As Integer = 0 To (dg_colunas.RowCount - 1)
                If dg_colunas.Rows(i2).Cells(2).Value = "Y" And categcol = -1 Then

                    For i As Integer = 0 To (dg_result.RowCount - 2)

                        processo.ReportProgress(((i + 1) / (dg_result.RowCount - 1)) * 100)

                        If processo.CancellationPending = True Then
                            Exit Sub
                        End If

                        If dg_colunas.Rows(i2).Cells(1).Value = "Decimal" And IsNothing(dg_result.Rows(i).Cells(3).Value.ToString()) = False Then

                            Dim dou As Double
                            y(i) = Nothing
                            Dim str As String = dg_result.Rows(i).Cells(i2).Value.ToString()
                            dou = con.ConvertFrom(str)
                            y(i) = dou

                        ElseIf dg_colunas.Rows(i2).Cells(1).Value = "Integer" And IsNothing(dg_result.Rows(i).Cells(3).Value.ToString()) = False Then

                            Dim dou As Double
                            y(i) = Nothing
                            Dim str As String = dg_result.Rows(i).Cells(i2).Value.ToString()
                            dou = Math.Round(con.ConvertFrom(str))
                            y(i) = dou
                        End If
                    Next

                    FormsPlot1.Plot.Axes.SetLimits(x.ToArray().Min, x.ToArray().Max, y.ToArray().Min, y.ToArray().Min)
                    FormsPlot1.Plot.Add.Scatter(x, y).LegendText = "Busy"

                    Exit For
                End If
            Next

            'Categorized Y (multiple series)
            For i2 As Integer = 0 To (dg_colunas.RowCount - 1)
                If (dg_colunas.Rows(i2).Cells(2).Value = "Y Categorizado" OrElse dg_colunas.Rows(i2).Cells(2).Value = "Y Categorized") And categcol > -1 Then

                    Dim LINHAY As Integer ' holds the column index for Y values
                    For i3 As Integer = 0 To (dg_colunas.RowCount - 1)
                        If dg_colunas.Rows(i3).Cells(2).Value = "Y" Then
                            LINHAY = i3
                            Exit For
                        End If
                    Next

                    Dim categoria() = New String(dg_result.RowCount - 2) {}
                    For i3 As Integer = 0 To (dg_result.RowCount - 2)
                        Dim str As String = dg_result.Rows(i3).Cells(i2).Value.ToString()
                        categoria(i3) = str
                    Next

                    Dim categoriafiltrada() As String = categoria.Distinct().ToArray()

                    For i3 As Integer = 0 To (categoriafiltrada.Length - 1)
                        Dim y2 = New Double(dg_result.RowCount - 2) {}

                        For i4 As Integer = 0 To (dg_result.RowCount - 2)

                            If processo.CancellationPending = True Then
                                Exit Sub
                            End If

                            processo.ReportProgress(((i4 + 1) / (dg_result.RowCount - 1)) * 100)

                            y2(i4) = 0

                            If categoriafiltrada(i3).ToString() = dg_result.Rows(i4).Cells(i2).Value.ToString() And dg_colunas.Rows(i2).Cells(1).Value = "Text" Then
                                Dim dou As Double
                                y2(i4) = Nothing
                                Dim str As String = dg_result.Rows(i4).Cells(LINHAY).Value.ToString()
                                dou = con.ConvertFrom(str)
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
                            If table.Rows(i4)("y") = "0" Then
                                table.Rows.RemoveAt(i4)
                            End If
                        Next

                        Dim y3 = New Double(table.Rows.Count - 1) {}
                        Dim x3 = New Double(table.Rows.Count - 1) {}

                        For i4 As Integer = table.Rows.Count - 1 To 0 Step -1
                            y3(i4) = table.Rows(i4)("y")
                            x3(i4) = table.Rows(i4)("x")
                        Next

                        If x3.Length <> 0 Or y3.Length <> 0 Then
                            FormsPlot1.Plot.Axes.SetLimits(x3.ToArray().Min, x3.ToArray().Max, y3.ToArray().Min, y3.ToArray().Min)
                            FormsPlot1.Plot.Add.Scatter(x3, y3).LegendText = categoriafiltrada(i3).ToString()
                        End If

                    Next

                    Exit For
                End If
            Next

            'Y fixed horizontal line (Max)
            For i2 As Integer = 0 To (dg_colunas.RowCount - 1)
                If (dg_colunas.Rows(i2).Cells(2).Value = "Y Linha Fixa (Max)" OrElse dg_colunas.Rows(i2).Cells(2).Value = "Y Fixed Line (Max)") And linhasycol > -1 Then

                    Dim qtdvaloreslinha() = New Double(dg_result.RowCount - 2) {}

                    For i3 As Integer = 0 To (dg_result.RowCount - 2)
                        Dim str As String = dg_result.Rows(i3).Cells(i2).Value.ToString()
                        qtdvaloreslinha(i3) = str
                    Next

                    Dim qtdvaloreslinhafiltrada() As Double = qtdvaloreslinha.Distinct().ToArray()

                    Dim linhafx = FormsPlot1.Plot.Add.HorizontalLine(qtdvaloreslinhafiltrada.Max())
                    linhafx.Text = dg_colunas.Rows(i2).Cells(0).Value.ToString()
                    linhafx.LinePattern = LinePattern.Dashed
                    linhafx.LabelOppositeAxis = True
                End If
            Next

            'X shadow (fill region)
            For i2 As Integer = 0 To (dg_colunas.RowCount - 1)
                If (dg_colunas.Rows(i2).Cells(2).Value = "X Sombra" OrElse dg_colunas.Rows(i2).Cells(2).Value = "X Shadow") And sombrax > -1 Then

                    Dim v1 = New Double(dg_result.RowCount - 2) {}
                    Dim v2 = New Double(dg_result.RowCount - 2) {}

                    For i3 As Integer = 0 To (dg_result.RowCount - 2)
                        Dim str As String = dg_result.Rows(i3).Cells(i2).Value.ToString()
                        v2(i3) = str
                        v1(i3) = 0
                    Next

                    If x.Length <> 0 Or v1.Length <> 0 Or v2.Length <> 0 Then
                        Dim sombra = FormsPlot1.Plot.Add.FillY(x, v1, v2)
                        sombra.LineStyle.IsVisible = True
                        sombra.LineStyle.Pattern = LinePattern.Dashed
                        sombra.LineStyle.Width = 1
                        sombra.LineStyle.Color = Colors.Yellow
                        sombra.LineStyle.AntiAlias = False
                        sombra.LegendText = dg_colunas.Rows(i2).Cells(0).Value.ToString()
                        sombra.FillStyle.Color = Colors.Yellow.WithAlpha(0.1)
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

        Catch ex As Exception
            MsgBox("Error - " & ex.Message.ToString(), MsgBoxStyle.Exclamation, "Error")
        End Try

    End Sub
    ''' <summary>
    ''' Exportar procedure. Exports dg_result data to a new Excel workbook via Interop.
    ''' </summary>
    ''' <param name="processo">Background worker for progress/cancel.</param>
    Private Sub Exportar(ByVal processo As BackgroundWorker)  ' TODO: Document purpose and lifecycle.
        If Me.dg_result.Rows.Count <= 0 Then Return
        Try
            ' Create workbook
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

                processo.ReportProgress(((index1 + 1) / Math.Max(1, num1)) * 100)

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
            MessageBox.Show("Error : " & ex.Message)
            Me.XcelApp.Quit()
        End Try
    End Sub

    ''' <summary>
    ''' Button2_Click procedure. Starts or cancels the Excel export.
    ''' </summary>
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click  ' TODO: Document purpose and lifecycle.
        If BackgroundWorker4.IsBusy = True Then
            BackgroundWorker4.CancelAsync()
            Exit Sub
        End If

        Me.lbl_status.Text = "Exporting..."
        Me.ToolStripProgressBar1.Value = 0
        BackgroundWorker4.RunWorkerAsync()
    End Sub

    ''' <summary>
    ''' BackgroundWorker4_DoWork procedure. Invokes Exportar in the background.
    ''' </summary>
    Private Sub BackgroundWorker4_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker4.DoWork  ' TODO: Document purpose and lifecycle.
        Exportar(BackgroundWorker4)
    End Sub

    ''' <summary>
    ''' BackgroundWorker4_RunWorkerCompleted procedure. Finalizes export UI.
    ''' </summary>
    Private Sub BackgroundWorker4_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker4.RunWorkerCompleted  ' TODO: Document purpose and lifecycle.
        If (e.Cancelled = True) Then
            MsgBox("Export canceled.", MsgBoxStyle.Information, "Canceled")
            Me.lbl_status.Text = "Canceled."
            Me.ToolStripProgressBar1.Value = 0
        Else
            Me.lbl_status.Text = "Values Exported."
            Me.ToolStripProgressBar1.Value = 100
        End If
    End Sub

    ''' <summary>
    ''' BackgroundWorker4_ProgressChanged procedure. Updates progress bar during export.
    ''' </summary>
    Private Sub BackgroundWorker4_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles BackgroundWorker4.ProgressChanged  ' TODO: Document purpose and lifecycle.
        ToolStripProgressBar1.Value = e.ProgressPercentage
    End Sub

    ''' <summary>
    ''' ExtrairDaPCOMMToolStripMenuItem_Click procedure. Sets buffer as input and opens PCOMM capture form.
    ''' </summary>
    Private Sub ExtrairDaPCOMMToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExtrairDaPCOMMToolStripMenuItem.Click  ' TODO: Document purpose and lifecycle.

        txt_entrada.Text = Application.StartupPath.ToString() & "Buffer.txt"
        'Timer1.Interval = 3000
        'Me.TImer1.Enabled = True
        'Timer1.Start()

        pccom.Show()

    End Sub

    ''' <summary>
    ''' Dg_linhas_RowsAdded procedure. Keeps chart column config in sync.
    ''' </summary>
    Private Sub Dg_linhas_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dg_linhas.RowsAdded  ' TODO: Document purpose and lifecycle.
        AtualizarGridChart()
    End Sub

    ''' <summary>
    ''' Dg_table_RowsAdded procedure. Keeps chart column config in sync.
    ''' </summary>
    Private Sub Dg_table_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dg_table.RowsAdded  ' TODO: Document purpose and lifecycle.
        AtualizarGridChart()
    End Sub

    ''' <summary>
    ''' Dg_table_RowsRemoved procedure. Keeps chart column config in sync.
    ''' </summary>
    Private Sub Dg_table_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles dg_table.RowsRemoved  ' TODO: Document purpose and lifecycle.
        AtualizarGridChart()
    End Sub

    ''' <summary>
    ''' Dg_table_RowLeave procedure. Keeps chart column config in sync when leaving a row.
    ''' </summary>
    Private Sub Dg_table_RowLeave(sender As Object, e As DataGridViewCellEventArgs) Handles dg_table.RowLeave  ' TODO: Document purpose and lifecycle.
        AtualizarGridChart()
    End Sub

    ''' <summary>
    ''' AtualizarGridChart procedure. Rebuilds the helper grid with columns used by charting.
    ''' </summary>
    Private Sub AtualizarGridChart()
        dg_colunas.Rows.Clear()
        If dg_colunas.Columns.Count > 2 Then
            For i As Integer = 0 To (dg_linhas.RowCount - 2)
                dg_colunas.Rows.Add(dg_linhas.Rows(i).Cells(0).Value,
                                    dg_linhas.Rows(i).Cells(5).Value,
                                    dg_linhas.Rows(i).Cells(6).Value)
            Next

            For i As Integer = 0 To (dg_table.RowCount - 2)
                dg_colunas.Rows.Add(dg_table.Rows(i).Cells(0).Value,
                                    dg_table.Rows(i).Cells(5).Value,
                                    dg_table.Rows(i).Cells(6).Value)
            Next

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

        End If
    End Sub

    ''' <summary>
    ''' Dg_linhas_RowLeave procedure. Keeps chart column config in sync when leaving a fixed-field row.
    ''' </summary>
    Private Sub Dg_linhas_RowLeave(sender As Object, e As DataGridViewCellEventArgs) Handles dg_linhas.RowLeave  ' TODO: Document purpose and lifecycle.
        AtualizarGridChart()
    End Sub

    ''' <summary>
    ''' Timer1_Tick procedure. Placeholder for timed auto-extract logic (currently disabled).
    ''' </summary>
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick  ' TODO: Document purpose and lifecycle.

        'If pccom.NumericUpDown2.Value <> 0 And pccom.CheckBox1.Checked = True And pccom.BackgroundWorker1.IsBusy = True Then
        '  btn_exec_Click(sender, e)
        '  Timer1.Interval = (pccom.NumericUpDown2.Value * 1000)
        '  Timer2.Interval = 1000
        '  txttime.Text = Timer1.Interval / 1000
        '  Timer2.Stop()
        '  Timer2.Start()
        'End If

        'If Timer1.Enabled = False Then
        '  Timer2.Stop()
        '  txttime.Text = "0"
        'End If

    End Sub

    ''' <summary>
    ''' Timer2_Tick procedure. Decrements countdown (display-only) when in realtime capture mode.
    ''' </summary>
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick  ' TODO: Document purpose and lifecycle.

        If pccom.NumericUpDown2.Value <> 0 And pccom.CheckBox1.Checked = True And pccom.BackgroundWorker1.IsBusy = True Then
            If txttime.Text <> 0 Then
                txttime.Text = Convert.ToInt32(txttime.Text) - 1
            End If
        End If
    End Sub

    ''' <summary>
    ''' RadioButton4_CheckedChanged procedure. Loads a preset mapping for CPU/LPAR MSU view.
    ''' </summary>
    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged  ' TODO: Document purpose and lifecycle.
        dg_linhas.Rows.Clear()
        dg_table.Rows.Clear()
        txtseparador.Text = "  RMF "
        Me.dg_linhas.Rows.Add("DateTime", "3", "38", "8", "0", "Date", "X", "No operation")
        Me.dg_linhas.Rows.Add("Time", "3", "54", "8", "0", "Time", "X", "No operation")
        Me.dg_linhas.Rows.Add("System", "3", "26", "4", "0", "Text", "Not Applied", "No operation")
        Me.dg_linhas.Rows.Add("Range", "3", "71", "3", "0", "Decimal", "Not Applied", "No operation")
        Me.dg_linhas.Rows.Add("CPUModel", "5", "36", "3", "0", "Decimal", "Not Applied", "No operation")
        Me.dg_linhas.Rows.Add("zModel", "5", "25", "5", "0", "Decimal", "Not Applied", "No operation")
        Me.dg_linhas.Rows.Add("CPCCpct", "6", "18", "4", "0", "Decimal", "Not Applied", "No operation")
        Me.dg_linhas.Rows.Add("4HRAAVG", "6", "56", "5", "0", "Decimal", "Not Applied", "No operation")
        Me.dg_linhas.Rows.Add("ImageCpct", "7", "16", "6", "0", "Decimal", "Not Applied", "No operation")
        Me.dg_linhas.Rows.Add("WLMCapp", "7", "41", "5", "0", "Decimal", "Not Applied", "No operation")
        Me.dg_table.Rows.Add("LPAR", "14", "30", "1", "8", "Text", "Y Categorized", "", "No operation")
        Me.dg_table.Rows.Add("MSU", "14", "30", "19", "4", "Decimal", "Y", "", "No operation")
    End Sub

    ''' <summary>
    ''' RadioButton5_CheckedChanged procedure. Loads a preset mapping for Job vs Busy view.
    ''' </summary>
    Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton5.CheckedChanged  ' TODO: Document purpose and lifecycle.
        dg_linhas.Rows.Clear()
        dg_table.Rows.Clear()
        txtseparador.Text = "  RMF "
        Me.dg_linhas.Rows.Add("DateTime", "3", "38", "8", "0", "Date", "X", "No operation")
        Me.dg_linhas.Rows.Add("Time", "3", "54", "8", "0", "Time", "X", "No operation")
        Me.dg_linhas.Rows.Add("System", "3", "26", "4", "0", "Text", "Not Applied", "No operation")
        Me.dg_linhas.Rows.Add("Range", "3", "71", "3", "0", "Decimal", "Not Applied", "No operation")
        Me.dg_table.Rows.Add("Job", "8", "300", "1", "8", "Text", "Y Categorized", "", "No operation")
        Me.dg_table.Rows.Add("Busy", "8", "300", "23", "6", "Decimal", "Y", "", "No operation")
        Me.dg_table.Rows.Add("ServClass", "8", "300", "13", "8", "Text", "Not Applied", "", "No operation")
        Me.dg_table.Rows.Add("SX", "8", "300", "10", "2", "Text", "Not Applied", "", "No operation")
    End Sub

    ''' <summary>
    ''' RadioButton6_CheckedChanged procedure. Loads a preset mapping for ID/Type/Part%/Tot% view.
    ''' </summary>
    Private Sub RadioButton6_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton6.CheckedChanged  ' TODO: Document purpose and lifecycle.
        dg_linhas.Rows.Clear()
        dg_table.Rows.Clear()
        txtseparador.Text = "  RMF "
        Me.dg_linhas.Rows.Add("DateTime", "3", "38", "8", "0", "Date", "X", "No operation")
        Me.dg_linhas.Rows.Add("Time", "3", "54", "8", "0", "Time", "X", "No operation")
        Me.dg_linhas.Rows.Add("System", "3", "26", "4", "0", "Text", "Not Applied", "No operation")
        Me.dg_linhas.Rows.Add("Range", "3", "71", "3", "0", "Decimal", "Not Applied", "No operation")

        Me.dg_table.Rows.Add("ID", "8", "40", "1", "2", "Text", "Y Categorized", "", "No operation")
        Me.dg_table.Rows.Add("Type", "8", "40", "11", "4", "Decimal", "Not Applied", "", "No operation")
        Me.dg_table.Rows.Add("Part%", "8", "40", "21", "4", "Text", "Y", "", "No operation")
        Me.dg_table.Rows.Add("Tot%", "8", "40", "26", "4", "Text", "Not Applied", "", "No operation")
    End Sub

    ''' <summary>
    ''' RadioButton7_CheckedChanged procedure. Loads a preset mapping for OMVS job/server SRB view.
    ''' </summary>
    Private Sub RadioButton7_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton7.CheckedChanged  ' TODO: Document purpose and lifecycle.
        dg_linhas.Rows.Clear()
        dg_table.Rows.Clear()
        txtseparador.Text = "  RMF "
        Me.dg_linhas.Rows.Add("DateTime", "3", "38", "8", "0", "Date", "X", "No operation")
        Me.dg_linhas.Rows.Add("Time", "3", "54", "8", "0", "Time", "X", "No operation")
        Me.dg_linhas.Rows.Add("System", "3", "26", "4", "0", "Text", "Not Applied", "No operation")
        Me.dg_linhas.Rows.Add("Range", "3", "71", "3", "0", "Decimal", "Not Applied", "No operation")
        Me.dg_linhas.Rows.Add("Kernel", "5", "18", "11", "0", "Text", "Not Applied", "No operation")
        Me.dg_linhas.Rows.Add("BPXPRM", "6", "8", "40", "0", "Text", "Not Applied", "No operation")

        Me.dg_table.Rows.Add("Job", "11", "300", "1", "8", "Text", "Y Categorized", "", "No operation")
        Me.dg_table.Rows.Add("User", "11", "300", "11", "8", "Text", "Not Applied", "", "No operation")
        Me.dg_table.Rows.Add("ASID", "11", "300", "21", "4", "Text", "Not Applied", "", "No operation")
        Me.dg_table.Rows.Add("Apply%", "11", "300", "60", "5", "Decimal", "Not Applied", "", "No operation")
        Me.dg_table.Rows.Add("TotalSRB", "11", "300", "67", "5", "Decimal", "Y", "", "No operation")
        Me.dg_table.Rows.Add("Server", "11", "300", "75", "4", "Text", "Not Applied", "", "No operation")
    End Sub

    ''' <summary>
    ''' RadioButton8_CheckedChanged procedure. Loads a preset mapping for WLM policy goals view.
    ''' </summary>
    Private Sub RadioButton8_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton8.CheckedChanged  ' TODO: Document purpose and lifecycle.
        dg_linhas.Rows.Clear()
        dg_table.Rows.Clear()
        txtseparador.Text = "  RMF "
        Me.dg_linhas.Rows.Add("DateTime", "3", "40", "8", "0", "Date", "X", "No operation")
        Me.dg_linhas.Rows.Add("Time", "3", "55", "8", "0", "Time", "X", "No operation")
        Me.dg_linhas.Rows.Add("Samples", "3", "14", "4", "0", "Text", "Not Applied", "No operation")
        Me.dg_linhas.Rows.Add("Policy", "8", "21", "20", "0", "Text", "Not Applied", "No operation")
        Me.dg_linhas.Rows.Add("Activated", "8", "57", "20", "0", "Text", "Not Applied", "No operation")

        Me.dg_table.Rows.Add("Name", "14", "300", "1", "8", "Text", "Y Categorized", "", "No operation")
        Me.dg_table.Rows.Add("ExecGoal", "14", "300", "16", "4", "Decimal", "Not Applied", "", "No operation")
        Me.dg_table.Rows.Add("VelAct", "14", "300", "21", "3", "Decimal", "Not Applied", "", "No operation")
        Me.dg_table.Rows.Add("RespTimeGoal", "14", "300", "31", "3", "Decimal", "Not Applied", "", "No operation")
        Me.dg_table.Rows.Add("RespTimeActual", "14", "300", "43", "3", "Decimal", "Not Applied", "", "No operation")
        Me.dg_table.Rows.Add("PerfIndx", "14", "300", "49", "4", "Decimal", "Y", "", "No operation")
        Me.dg_table.Rows.Add("TransEnded", "14", "300", "55", "5", "Decimal", "Not Applied", "", "No operation")
        Me.dg_table.Rows.Add("AvgWait", "14", "300", "61", "5", "Decimal", "Not Applied", "", "No operation")
        Me.dg_table.Rows.Add("RespExec", "14", "300", "68", "5", "Decimal", "Not Applied", "", "No operation")
        Me.dg_table.Rows.Add("TimeActual", "14", "300", "75", "5", "Decimal", "Not Applied", "", "No operation")
    End Sub

End Class