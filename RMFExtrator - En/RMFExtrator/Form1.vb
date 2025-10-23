Imports System.Runtime.CompilerServices
Imports System.Threading
Imports System.IO
Imports Zuby.ADGV
Imports System.ComponentModel
Imports RMFExtrator.EHLLAPI
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MSChart20Lib
Imports ScottPlot
Imports ScottPlot.Plottables
Imports ScottPlot.WinForms
Imports System.Reflection
Imports System.Buffers
'Imports Microsoft.Office.Interop.Excel
Imports System.Windows.Forms
Imports System.ComponentModel.Design.Serialization
Imports OpenTK.Graphics.ES30

Public Class Form1

    Private XcelApp As New Microsoft.Office.Interop.Excel.Application
    Dim frmconfig As New frm_config_chartvb
    Dim globaltable As New System.Data.DataTable
    Dim bsglobal As New BindingSource
    Public pccom As New frm_pcomm

    Dim dg_colunas = New DataGridView()
    Private Sub createDGV_Chart()

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
        col_type.HeaderText = "Data Type"
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

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        If BackgroundWorker1.CancellationPending = True Then
            e.Cancel = True
            Exit Sub
        End If
        Importar(BackgroundWorker1, txt_entrada.Text)

    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted

        If (e.Cancelled = True) Then
            MsgBox("Import cancelled.", MsgBoxStyle.Information, "Cancelled")
            Me.lbl_status.Text = "Cancelled."
            Me.ToolStripProgressBar1.Value = 0
        Else

            Me.lbl_status.Text = "Read."
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
                Me.lbl_status.Text = "Removing Duplicate Rows..."
                Me.ToolStripProgressBar1.Value = 0

                If Me.BackgroundWorker2.IsBusy = True Then
                    Me.BackgroundWorker2.CancelAsync()
                    'If realtime = False Then
                    'Exit Sub
                    'Else
                    'Me.BackgroundWorker1.CancellationPending()
                    'If
                    While Me.BackgroundWorker2.IsBusy
                        Application.DoEvents()
                    End While
                    'End If
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


            'ler totais de linhas no arquivo
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
            'ARQUIVADO - RADIOBUTTON2 FOI DESATIVADO
            '-----------------------------------------

            'If Me.RadioButton2.Checked Then

            '    Dim index2 = 0
            '    Dim NUM_PAG = 0
            '    Dim num_linha_result As Integer = 0
            '    Me.lbl_status.Text = "Lendo... "

            '    While Me.dg_linhas.RowCount - 1 > index2


            '        Dim tam_pag As Integer = (Convert.ToInt64(Me.txt_pag.Text) - 1)
            '        Dim Left = 0 'LINHA DA PAGINA
            '        Dim offset As Integer = 0
            '        Dim num_linha As Integer = 0
            '        Dim num_linha_add_result As Integer = 0

            '        Me.ToolStripProgressBar1.Maximum = 100 '[integer]
            '        Me.lbl_status.Text = "Lendo... Buscando Campo '" & Me.dg_linhas.Rows(index2).Cells(0).Value.ToString() & "' (" & (index2 + 1).ToString() & "/" & (dg_linhas.RowCount - 1).ToString() & ")"


            '        Dim streamReader = File.OpenText(arquivo)
            '        Dim str As String = streamReader.ReadLine()

            '        While Not streamReader.EndOfStream
            '            If processo.CancellationPending = True Then
            '                Exit Sub
            '            End If

            '            Interlocked.Increment(num_linha)

            '            If offset < txtoffset.Text And txtoffset.Text <> "0" Then
            '                Interlocked.Increment(offset)
            '            Else

            '                'Me.lbl_status.Text = "Lendo... (" & num_linha.ToString() & "/" & linhas_totais.ToString() & ")"

            '                If Left < tam_pag Then
            '                    If Me.dg_linhas.Rows(index2).Cells(1).Value = Left And str.Length >= (Convert.ToInt64(Me.dg_linhas.Rows(index2).Cells(2).Value) + Convert.ToInt64(Me.dg_linhas.Rows(index2).Cells(3).Value)) Then
            '                        'index2 = campo (numero da linha no dg_linha)
            '                        If index2 > 0 Then
            '                            globaltable.Rows(num_linha_add_result)(index2) = CObj(Trim(str.Substring(Convert.ToInt64(Me.dg_linhas.Rows(CInt(index2)).Cells(CInt(2)).Value), Convert.ToInt64(Me.dg_linhas.Rows(CInt(index2)).Cells(CInt(3)).Value)).ToString()))
            '                            Interlocked.Increment(num_linha_add_result)
            '                        Else
            '                            globaltable.Rows.Add(New Object(0) {CObj(Trim(str.Substring(Convert.ToInt32(RuntimeHelpers.GetObjectValue(Me.dg_linhas.Rows(index2).Cells(2).Value)), Convert.ToInt64(Convert.ToInt32(RuntimeHelpers.GetObjectValue(Me.dg_linhas.Rows(index2).Cells(3).Value)).ToString()))))})
            '                            Interlocked.Increment(num_linha_result)
            '                        End If
            '                    End If
            '                    Interlocked.Increment(Left)
            '                Else

            '                    Left = 0
            '                    Interlocked.Increment(NUM_PAG)
            '                    Me.lbl_paglida.Text = NUM_PAG.ToString()

            '                End If

            '                'Interlocked.Increment(Me.ToolStripProgressBar1.Value)

            '            End If

            '            BackgroundWorker1.ReportProgress((num_linha / linhas_totais) * 100)
            '            str = streamReader.ReadLine()

            '        End While

            '        streamReader.Close()
            '        Interlocked.Increment(index2)

            '    End While

            '    Me.lbl_paglida.Text = (NUM_PAG / index2).ToString()



            '    '-----------------------------------------
            '    'ARQUIVADO - RADIOBUTTON1 FOI DESATIVADO
            '    '-----------------------------------------

            'ElseIf Me.RadioButton1.Checked Then

            '    Me.lbl_status.Text = "Lendo... "
            '    If Not Me.RadioButton1.Checked Then Return
            '    Me.dg_result.Rows.Clear()
            '    Dim index3 = 0
            '    Dim num4 = 0

            '    While Me.dg_linhas.RowCount - 1 > index3
            '        Dim Left = 1
            '        Dim flag = False
            '        Dim index4 = 0
            '        Me.lbl_status.Text = "Lendo... Buscando Campo '" & Me.dg_linhas.Rows(index3).Cells(0).Value.ToString() & "' (" & (index3 + 1).ToString() & "/" & (dg_linhas.RowCount - 1).ToString() & ")"

            '        Dim num_linha As Integer = 0
            '        Dim streamReader = File.OpenText(arquivo)
            '        Dim str As String = streamReader.ReadLine()
            '        While Not streamReader.EndOfStream
            '            If processo.CancellationPending = True Then
            '                Exit Sub
            '            End If
            '            Interlocked.Increment(num_linha)
            '            If str.Contains("PAGE    ") And Not str.Contains("PAGE    " & Me.dg_linhas.Rows(index3).Cells(4).Value.ToString()) Then
            '                flag = False
            '            ElseIf str.Contains("PAGE    " & Me.dg_linhas.Rows(index3).Cells(4).Value.ToString()) Then
            '                flag = True
            '                Left = 0
            '                Interlocked.Increment(num4)
            '                lbl_paglida.Text = num4.ToString()
            '            End If
            '            If flag = True And Me.dg_linhas.Rows(index3).Cells(1).Value = Left And str.Length >= (Convert.ToInt64(Me.dg_linhas.Rows(index3).Cells(2).Value) + Convert.ToInt64(Me.dg_linhas.Rows(index3).Cells(3).Value)) Then
            '                If index3 > 0 Then
            '                    '>>>>Me.dg_result.Rows(index4).Cells(index3).Value = CObj(str.Substring(Convert.ToInt64(Me.dg_linhas.Rows(CInt(index3)).Cells(CInt(2)).Value), Convert.ToInt64(Me.dg_linhas.Rows(CInt(index3)).Cells(CInt(3)).Value)).ToString())
            '                    globaltable.Rows(index4)(index3) = CObj(Trim(str.Substring(Convert.ToInt64(Me.dg_linhas.Rows(CInt(index3)).Cells(CInt(2)).Value), Convert.ToInt64(Me.dg_linhas.Rows(CInt(index3)).Cells(CInt(3)).Value)).ToString()))
            '                    Interlocked.Increment(index4)
            '                ElseIf index3 = 0 Then
            '                    '>>>>Me.dg_result.Rows.Add(New Object(0) {CObj(str.Substring(Convert.ToInt32(RuntimeHelpers.GetObjectValue(Me.dg_linhas.Rows(CInt(index3)).Cells(CInt(2)).Value)), Convert.ToInt64(Convert.ToInt32(RuntimeHelpers.GetObjectValue(Me.dg_linhas.Rows(CInt(index3)).Cells(CInt(3)).Value)).ToString())))})
            '                    globaltable.Rows.Add(New Object(0) {CObj(Trim(str.Substring(Convert.ToInt32(RuntimeHelpers.GetObjectValue(Me.dg_linhas.Rows(index3).Cells(2).Value)), Convert.ToInt64(Convert.ToInt32(RuntimeHelpers.GetObjectValue(Me.dg_linhas.Rows(index3).Cells(3).Value)).ToString()))))})
            '                End If
            '            End If
            '            BackgroundWorker1.ReportProgress((num_linha / linhas_totais) * 100)

            '            Interlocked.Increment(Left)
            '            str = streamReader.ReadLine()
            '        End While
            '        streamReader.Close()
            '        Interlocked.Increment(index3)

            '    End While


            '-----------------------------------------
            'COMEÇA AQUI A EXTRAÇÃO
            '==========================
            '
            '*** ADICIONAR QUE SE O CAMPO NAO PERTENCER AO TIPO DECLARADO, OU SE FOR NULO, DESCARTA A LINHA
            '
            '-----------------------------------------

            If Me.RadioButton3.Checked Then

                Me.lbl_status.Text = "Reading... "
                'If Not Me.RadioButton3.Checked Then Return
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

                'ABRE DOIS ARQUIVOS UM PARA LER OS FIXOS E OUTRO EM FORMA DE TABELA
                'LEITURA ANDA EM DOIS PASSOS, UMA LE OS CAMPOS FIXO E MONTA A ESTRUTURA DE SAIDA E O OUTRO PEGA OS CAMPOS DE TABELA
                'READER - FIXOS
                'READER2 - TABELA
                While Not streamReader.EndOfStream Or Not streamReader2.EndOfStream

                    'VERIFICA SE O PROCESSO FOI CANCELADO
                    If processo.CancellationPending = True Then
                        Exit Sub
                    End If

                    'VERIFICA SE CHEGOU AO FIM DA LEITURA
                    If IsNothing(str) Or IsNothing(str2) Then
                        Exit While
                    End If

                    'CRIA A TABLE - ADICIONA A COLUNA NOVA QUE VAI POPULAR
                    Dim tablecabeca As New System.Data.DataTable
                    tablecabeca.Clear()
                    tablecabeca.Columns.Add()


                    '--------------------------------------------------------------------------
                    'PEGA A QUANTIDADE DE CAMPOS QUE QUER BUSCAR EM FORMATO DE TABLE
                    '
                    'DG_TABLE - PARAMETRS QUE SERAM CAPTURADOS EM FORMATO DE TABELA
                    '--------------------------------------------------------------------------

                    '--------------------------------------------------------------------------
                    'VERIFICA SE VAI RODAR CAMPOS FIXOS + CAMPOS TABLES OOOUUU APENAS CAMPOS FIXOS
                    If dg_table.Rows.Count > 1 Then
                        '------------------------------------------

                        'HA 2 STR PORQUE A LEITURA ANDA EM DOIS PASSOS, UMA LE OS CAMPOS FIXO E MONTA A ESTRUTURA DE SAIDA E A STR2 PEGA OS CAMPOS DE TABELA DEPOIS DA STR
                        'STR - PEGA OS CAMPOS FIXOS
                        'STR2 - PEGA OS CAMPOS TABELA

                        If str.Contains(txtseparador.Text) And str <> Nothing And dg_table.Rows.Count > 1 Then


                            poslinha = 1

                            str = streamReader.ReadLine()
                            Interlocked.Increment(num_linha)


                            'WHILE - Esse bloco faz a leitura de cada linha do arquivo para pegar os campos fixos primeiro e montar a estrutura da grid de result
                            While Not str.Contains(txtseparador.Text) And Not streamReader.EndOfStream

                                Interlocked.Increment(pag)

                                'linhagrid = a linha que esta no grid de campos fixo
                                linhagrid = 0

                                'pega campos do datagrid de campos fixos

                                'WHILE - Verifica se aquela linha que ele esta lendo precisa ser coletada baseada na grid de campos fixos
                                'ele faz isso comparando a posição da linha se é iguais
                                While linhagrid < Me.dg_linhas.RowCount - 1
                                    If Me.dg_linhas.Rows(linhagrid).Cells(1).Value = poslinha And str.Length >= (Convert.ToInt64(Me.dg_linhas.Rows(linhagrid).Cells(2).Value) + Convert.ToInt64(Me.dg_linhas.Rows(linhagrid).Cells(3).Value)) Then
                                        'adicionar a verificacao de tipo aqui <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
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

                        '--------------------------------------------------------------------------
                        '--------------------------------------------------------------------------

                        'PEGA OS CAMPOS EM FORMA DE TABELA (STR2)

                        If str2.Contains(txtseparador.Text) And str2 <> Nothing And dg_table.Rows.Count > 1 Then


                            poslinha2 = 1

                            str2 = streamReader2.ReadLine()

                            If IsNothing(str) Or IsNothing(str2) Then
                                Exit While
                            End If

                            While Not str2.Contains(txtseparador.Text) And Not streamReader2.EndOfStream

                                linhagrid2 = 0
                                'index4 = 0
                                Dim i As Integer = 0

                                If Trim(str2).ToString() <> "" Then

                                    'linhagrid2 - posicao de linha na grid de campos tabelas
                                    While linhagrid2 < Me.dg_table.RowCount - 1
                                        'VERIFICA SE O FIM DA LINHA É UM NUMÉRICO '?'
                                        If IsNumeric(Me.dg_table.Rows(linhagrid2).Cells(2).Value) Then

                                            Dim filter As String()
                                            If IsNothing(filter) = False Then
                                                Array.Clear(filter, 0, filter.Length)
                                            End If

                                            If Trim(Me.dg_table.Rows(linhagrid2).Cells(7).Value) <> "" Then
                                                filter = Trim(Me.dg_table.Rows(linhagrid2).Cells(7).Value).Split(";")
                                            End If

                                            'VERIFICA SE A LINHA QUE ESTA POSICIONADA ESTA DENTRO DA TABELA PROCURADA
                                            If (Me.dg_table.Rows(linhagrid2).Cells(1).Value <= poslinha2 And Me.dg_table.Rows(linhagrid2).Cells(2).Value >= poslinha2) And str2.Length >= (Convert.ToInt64(Me.dg_table.Rows(linhagrid2).Cells(3).Value) + Convert.ToInt64(Me.dg_table.Rows(linhagrid2).Cells(4).Value)) Then

                                                'linhagrid2 - posicao de linha na grid de campos tabelas
                                                'VERIFICA SE É O PRIMEIRO CAMPO TABELA - PORQUE SE FOR, PRECISA ADICIONAR AS LINHAS PARA A EXTRUTURA
                                                'SE FOR A SEGUNDA LINHA EM DIANTE, ENTÃO

                                                If linhagrid2 > 0 Then

                                                    'INSERE O VALOR NA COLUNA EM UMA LINHA QUE JA FOI CRIADA ANTES
                                                    If ValidateType(New Object(0) {CObj(Trim(str2.Substring(Convert.ToInt64(Me.dg_table.Rows(CInt(linhagrid2)).Cells(CInt(3)).Value), Convert.ToInt64(Me.dg_table.Rows(CInt(linhagrid2)).Cells(CInt(4)).Value)).ToString()))}, Me.dg_table.Rows(linhagrid2).Cells(5).Value) = True Then

                                                        'FAZ A VERIFICACAO DO FILTRO
                                                        '<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>

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

                                                    'i - INDICE DO CAMPO FIXO - SE FOR MAIOR, INDICA QUE LEU TODOS CAMPOS
                                                    Interlocked.Increment(i)

                                                Else

                                                    'PRIMEIRO PASSO ELE COLOCA OS VALORES FIXOS 
                                                    If tablecabeca.Rows.Count > 0 Then
                                                        'LOOP PARA ADICIONAR TODOS CABEÇALHOS (FIXO)
                                                        While i < tablecabeca.Rows.Count
                                                            If i = 0 Then
                                                                'O PRIMEIRO CAMPO FIXO SEMPRE TERÁ QUE DAR INICIO NA LINHA
                                                                globaltable.Rows.Add(tablecabeca.Rows(i)(0))
                                                            Else '(linha)(coluna)
                                                                'JA OS DEMAIS SÓ ADICIONA NOVA COLUNA (CAMPO FIXO) A FRENTE NA LINHA 
                                                                globaltable.Rows(globaltable.Rows.Count - 1)(i) = tablecabeca.Rows(i)(0)
                                                            End If
                                                            Interlocked.Increment(i)
                                                            'I=INDICE DO CAMPO FIXO
                                                        End While
                                                    End If


                                                    If ValidateType(New Object(0) {CObj(Trim(str2.Substring(Me.dg_table.Rows(linhagrid2).Cells(3).Value, Me.dg_table.Rows(linhagrid2).Cells(4).Value)))}, Me.dg_table.Rows(linhagrid2).Cells(5).Value) = True Then
                                                        'SEGUNDO PASSO ELE ADICIONA O VALOR PROCURADO DE CAMPOS TABELA NA LINHA
                                                        'globaltable.Rows(globaltable.Rows.Count - 1)(i) = Trim(str2.Substring(Me.dg_table.Rows(linhagrid2).Cells(3).Value, Me.dg_table.Rows(linhagrid2).Cells(4).Value))
                                                        'Else
                                                        'globaltable.Rows(globaltable.Rows.Count - 1)(i) = ""
                                                        'End If

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

                                                    '------------------------------

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
                        '--------------------------------------------------------------------------
                        'FIM DE PEGAR CAMPOS FIXOS + CAMPOS TABELA
                        '--------------------------------------------------------------------------

                    Else
                        '--------------------------------------------------------------------------
                        'VERIFICA SE VAI RODAR CAMPOS FIXOS + CAMPOS TABLES OOOUUU APENAS CAMPOS FIXOS
                        'NESSE CASO - APENAS CAMPOS FIXOS
                        '--------------------------------------------------------------------------
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
                                        'globaltable.Rows.Add(New Object(0) {CObj(Trim(str.Substring(Convert.ToInt32(RuntimeHelpers.GetObjectValue(Me.dg_linhas.Rows(linhagrid).Cells(2).Value)), Convert.ToInt64(Convert.ToInt32(RuntimeHelpers.GetObjectValue(Me.dg_linhas.Rows(linhagrid).Cells(3).Value)).ToString()))))})

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
                        '--------------------------------------------------------------------------
                        'FIM DE PEGAR APENAS OS CAMPOS FIXOS
                        '--------------------------------------------------------------------------

                    End If



                    '--------------------------------------------------------------------------
                    'LE PROXIMA LINHA E AUMENTA O CONTADOR 
                    '--------------------------------------------------------------------------
                    If Not str.Contains(txtseparador.Text) And Not str2.Contains(txtseparador.Text) And Not streamReader.EndOfStream And Not streamReader2.EndOfStream Then
                        str = streamReader.ReadLine()
                        str2 = streamReader2.ReadLine()
                        Interlocked.Increment(num_linha)
                    End If

                    BackgroundWorker1.ReportProgress((num_linha / linhas_totais) * 100)

                    '--------------------------------------------------------------------------
                    '--------------------------------------------------------------------------

                End While

                '--------------------------------------------------------------------------
                'NÃO HAVENDO MAIS LINHA PARA LER, ELE ENCERRA 
                '--------------------------------------------------------------------------

                streamReader.Close()
                streamReader2.Close()

                Me.lbl_paglida.Text = pag.ToString()

            End If

            '-----------------------------------------
            'ACABA AQUI A EXTRAÇÃO
            '-----------------------------------------

            '-----------------------------------------
            'VALIDA OS CAMPOS - REMOVE OS NULOS - CAMPOS VAZIOS
            '-----------------------------------------

            Me.lbl_status.Text = "Removing rows with null and inconsistent fields..."

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
            'ADICIONAR ABAIXO = SOMAR POR SERV. CLASS
            '-----------------------------------------

            'Crio a estrutura dos dois buffers

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


            'como saber qual os campos para usar a operação?
            'como usar mais de um group by?

            'criar um grid buffer com a linha dos os campos dos group by
            'exemplo
            '-------------------------------------------
            '|   Data   |    HORA    |    SERV CLASS   |
            '-------------------------------------------
            '|00/00/0000|   00:00    |  ABCD           |
            '|00/00/0000|   00:00    |  ABCD1          |
            '-------------------------------------------

            'Assim toda vez q passar de linha no globaltable verifica se ja fez a varredura para esses group bys


            'criar um grid buffer na memoria e ir alimentando ele onde achar os iguais (group by)
            'e ir somando os valores quando ler linha por linha do globaltable que bater / repetir para cada linha todo o processo
            'ao final substituir o conteudo do globaltable pelo conteudo dele


            'for rowindex as integer = dg_table.rows.count - 1 to 0 step -1
            '    dim row as datarow = globaltable.rows(rowindex)
            '    for each column as datacolumn in globaltable.columns
            '        if string.isnullorempty(row(column)) or trim(row(column)) = "" then
            '        end if
            '    next
            '    if isempty then
            '        globaltable.rows.removeat(rowindex)
            '    end if
            'next


        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Exclamation, "Error")
        End Try

    End Sub

    Function ValidateType(ByVal variable As Object, ByVal type As String) As Boolean

        '0-DataHora
        '1-Data
        '2-Hora
        '3-Decimal
        '4-Texto

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
                        '    If TypeOf variable Is String Then
                        '    result = True
                        '    Else
                        '    result = False
                        ' End If
                    End If

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
            MessageBox.Show("The input, output and No. of Lines fields on the page must be filled in.", "Blank field", MessageBoxButtons.OK)
            Me.txt_entrada.Focus()
            Exit Sub
        ElseIf Me.dg_linhas.RowCount = 0 Then
            MessageBox.Show("It is necessary to add the lines in the grid of the fields to be searched.", "Blank lines", MessageBoxButtons.OK)
            Me.dg_linhas.Focus()
            Exit Sub
        ElseIf File.Exists(arquivo) = False Then
            MessageBox.Show("The file you are trying to open does not exist.", "File does not exist", MessageBoxButtons.OK)
            Exit Sub
        End If

        If RadioButton3.Checked And txtseparador.Text = "" Then
            MessageBox.Show("The word anchor needs to be informed.", "Blank anchor", MessageBoxButtons.OK)
            Exit Sub
        End If

        If RadioButton3.Checked And dg_table.RowCount = 0 Then
            MessageBox.Show("Add rows to the Table Grid to search for tables in the file", "Blank table", MessageBoxButtons.OK)
            Exit Sub
        End If

        '============== ORDENAR POR LINHA OS DATAGRID

        For Each dgvr As DataGridViewRow In dg_linhas.Rows
            'Dim r As DataGridViewRow = TryCast(dgvr.Clone(), DataGridViewRow)
            Dim r As Integer = Convert.ToInt32(dgvr.Cells(1).Value)
            If dgvr.Cells(0).Value <> "" Then
                dgvr.Cells(1).Value = r
            End If
        Next

        For Each dgvr As DataGridViewRow In dg_table.Rows
            'Dim r As DataGridViewRow = TryCast(dgvr.Clone(), DataGridViewRow)
            Dim r As Integer = Convert.ToInt32(dgvr.Cells(1).Value)
            If dgvr.Cells(0).Value <> "" Then
                dgvr.Cells(1).Value = r
            End If
        Next

        dg_linhas.Sort(dg_linhas.Columns(1), ListSortDirection.Ascending)
        dg_table.Sort(dg_table.Columns(1), ListSortDirection.Ascending)

        '===============

        If File.Exists(arquivo) Then
            Dim myStreamReader As System.IO.StreamReader
            Try
                myStreamReader = System.IO.File.OpenText(arquivo)
                RichTextBox1.Text = myStreamReader.ReadToEnd()
                RichTextBox1.SelectionStart = RichTextBox1.Text.Length
                myStreamReader.Close()

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information)
                myStreamReader.Close()
            End Try
        End If


        bsglobal.Filter = Nothing
        bsglobal.Sort = Nothing

        dg_linhas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dg_table.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dg_result.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        'LIMPA TABLE E O GRID
        Me.lbl_status.Text = "Cleaning..."
        Me.lblresult.Text = "0"
        Me.lbl_paglida.Text = "0"
        dg_result.DataSource = Nothing
        dg_result.Rows.Clear()
        globaltable.Rows.Clear()
        Me.ToolStripProgressBar1.Value = 0
        '---------- COLUNA ----------

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
                globaltable.Columns.Add(Me.dg_table.Rows(index1).Cells(0).Value.ToString()) '& index1.ToString())
                Interlocked.Increment(index1)
            End While
        End If

        'dg_table.Rows(linhagrid2).Cells(1).Value - dg_table.Rows(linhagrid2).Cells(1).Value
        Me.dg_result.Refresh()
        Me.dg_result.RefreshEdit()

        Me.BackgroundWorker1.RunWorkerAsync()

    End Sub
    Public Sub btn_exec_Click(sender As Object, e As EventArgs) Handles btn_exec.Click

        AbrirTXT(txt_entrada.Text, pccom.CheckBox1.Checked)

    End Sub


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'txtver.Text = Application.ProductVersion.ToString()

        ''Me.dg_linhas.Rows.Add("HoraInicio", "1", "71", "19", "5", "DataHora", "X")
        ''Me.dg_linhas.Rows.Add("HoraFim", "2", "71", "19", "5", "DataHora", "Not Applied")
        ''Me.dg_table.Rows.Add("CP", "9", "13", "2", "2", "Texto", "Y Categorizado")
        ''Me.dg_table.Rows.Add("Busy", "9", "13", "23", "6", "Decimal", "Y")
        txtseparador.Text = "RMF V2R4"
        'Me.dg_linhas.Rows.Add("Data", "3", "38", "8", "0", "Data", "X")
        'Me.dg_linhas.Rows.Add("Hora", "3", "54", "8", "0", "Hora", "X")
        'Me.dg_table.Rows.Add("Job", "8", "300", "1", "8", "Texto", "Y Categorizado")
        'Me.dg_table.Rows.Add("Busy", "8", "300", "23", "6", "Decimal", "Y")
        ''Me.dg_linhas.Rows.Add("CP", "5", "36", "4", "5")
        ''Me.txt_entrada.Text = "C:\Users\MatheusPorsch\Downloads\RMFCP25.txt"

        dg_linhas.Rows.Clear()
        dg_table.Rows.Clear()

        createDGV_Chart()

        Me.txt_entrada.Text = Application.CommonAppDataPath.ToString() & "\" & "Buffer.txt"

        FormsPlot1.Plot.Style.DarkMode()


        'MsgBox(Application.CommonAppDataPath.ToString())

        If File.Exists(Application.CommonAppDataPath.ToString() & "\" & "Buffer.txt") Then
            IO.File.Delete(Application.CommonAppDataPath.ToString() & "\" & "Buffer.txt")
        End If

        'TabControl1_Selected(sender, e) 

    End Sub

    Private Sub dg_result_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs)
        If Me.dg_result.RowCount - 1 > 0 Then
            Me.lblresult.Text = Convert.ToString(Me.dg_result.RowCount - 1)
        Else
            Me.lblresult.Text = "0"
        End If
    End Sub

    Private Sub dg_result_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles dg_linhas.RowsRemoved
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
            MessageBox.Show("The file does not exist.", "Exist", MessageBoxButtons.OK)
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
    Private Sub dg_result_FilterStringChanged(sender As Object, e As Zuby.ADGV.AdvancedDataGridView.FilterEventArgs) Handles dg_result.FilterStringChanged
        bsglobal.Filter = dg_result.FilterString
    End Sub

    Private Sub dg_result_SortStringChanged(sender As Object, e As AdvancedDataGridView.SortEventArgs) Handles dg_result.SortStringChanged
        bsglobal.Sort = dg_result.SortString
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If File.Exists(txt_entrada.Text) = False Then
            MessageBox.Show("The file you are trying to open does not exist.", "File does not exist", MessageBoxButtons.OK)
            Exit Sub
        End If
        System.Diagnostics.Process.Start("notepad.exe", txt_entrada.Text)

        If File.Exists(txt_entrada.Text) Then
            Dim myStreamReader As System.IO.StreamReader
            Try
                myStreamReader = System.IO.File.OpenText(txt_entrada.Text)
                RichTextBox1.Text = myStreamReader.ReadToEnd()
                RichTextBox1.SelectionStart = RichTextBox1.Text.Length
                myStreamReader.Close()
            Catch ex As Exception
                myStreamReader.Close()
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

        Me.dg_linhas.Rows.Add("Column_" & dg_linhas.RowCount, (RichTextBox1.GetLineFromCharIndex(RichTextBox1.SelectionStart)).ToString(), (RichTextBox1.SelectionStart - RichTextBox1.GetFirstCharIndexOfCurrentLine()).ToString(), RichTextBox1.SelectionLength.ToString(), "0")

    End Sub

    Private Sub RichTextBox1_SelectionChanged(sender As Object, e As EventArgs) Handles RichTextBox1.SelectionChanged
        txtposi.Text = "Position: " & RichTextBox1.GetLineFromCharIndex(RichTextBox1.SelectionStart) & " Line \ " & (RichTextBox1.SelectionStart - RichTextBox1.GetFirstCharIndexOfCurrentLine()).ToString() & " Position \ " & RichTextBox1.SelectionLength.ToString() & " Length"
    End Sub


    Private Sub FormsPlot1_DoubleClick_1(sender As Object, e As EventArgs) Handles FormsPlot1.DoubleClick
        'ormsPlot1.aut
        FormsPlot1.Refresh()
    End Sub

    Private Sub TabControl1_Selected(sender As Object, e As TabControlEventArgs) Handles TabControl1.Selected

        If TabControl1.SelectedIndex = 1 Then

            If File.Exists(txt_entrada.Text) Then
                Dim myStreamReader As System.IO.StreamReader
                Try
                    myStreamReader = System.IO.File.OpenText(txt_entrada.Text)
                    RichTextBox1.Text = myStreamReader.ReadToEnd()
                    RichTextBox1.SelectionStart = RichTextBox1.Text.Length
                    myStreamReader.Close()

                Catch ex As Exception
                    myStreamReader.Close()
                    MsgBox(ex.Message, MsgBoxStyle.Information)
                End Try
            End If
        End If

    End Sub

    Private Sub RemoverDuplicados(ByRef dgvData As DataGridView, ByVal processo As BackgroundWorker)
        ' Assume dgvData is the DataGridView control
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

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
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

    Private Sub BackgroundWorker2_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker2.DoWork

        RemoverDuplicados(dg_result, BackgroundWorker2)
    End Sub

    Private Sub BackgroundWorker2_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker2.RunWorkerCompleted
        If (e.Cancelled = True) Then
            MsgBox("Removal Cancelled.", MsgBoxStyle.Information, "Cancelled")
            Me.lbl_status.Text = "Cancelled."
            Me.ToolStripProgressBar1.Value = 0
        Else

            Me.lbl_status.Text = "Duplicates Removed."
            Me.ToolStripProgressBar1.Value = 100

            Me.lbl_status.Text = "Ploting..."
            Me.ToolStripProgressBar1.Value = 0

            If pccom.CheckBox1.Checked = True Then
                If Me.BackgroundWorker3.IsBusy = True Then
                    Me.BackgroundWorker3.CancelAsync()
                    'If realtime = False Then
                    'Exit Sub
                    'Else
                    'Me.BackgroundWorker1.CancellationPending()
                    'If
                    While Me.BackgroundWorker3.IsBusy
                        Application.DoEvents()
                    End While
                    'End If
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
            MsgBox("Removal Cancelled.", MsgBoxStyle.Information, "Cancelled")
            Me.lbl_status.Text = "Cancelled."
            Me.ToolStripProgressBar1.Value = 0
        Else

            'FormsPlot1.Plot.AutoScale()
            FormsPlot1.Refresh()
            Me.lbl_status.Text = "Chart Plotted."
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

        'CRIA GRAFICO QUANDO NÃO HÁ Y CATEGORIZADO - APENAS UM EIXO NO Y
        For i2 As Integer = 0 To (dg_linhas.RowCount - 1)
            If dg_linhas.Rows(i2).Cells(6).Value = "Y" Then
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

            'Interact with a specific axis
            'myPlot.XAxis.Min = -100;
            'myPlot.XAxis.Max = 150;
            'myPlot.YAxis.Min = -5;
            'myPlot.YAxis.Max = 5;
            'myPlot.SetAxisLimits(-100, 150, -5, 5);

            'https://scottplot.net/quickstart/vb/


            FormsPlot1.Plot.Clear()
            'FormsPlot1.Plot.Style.Background(Colors.Black, Colors.White)
            'FormsPlot1.Plot.Style.ColorGrids(Colors.Gray)
            'FormsPlot1.Plot.Style.DarkMode()


            Dim con As New DoubleConverter
            Dim datacol As Integer = -1
            Dim horacol As Integer = -1
            Dim categcol As Integer = -1
            Dim linhasycol As Integer = -1
            Dim sombrax As Integer = -1

            Dim y = New Double(dg_result.RowCount - 2) {}
            Dim x = New Double(dg_result.RowCount - 2) {}
            y.ToArray().Clear(y, 0, y.Length)
            x.ToArray().Clear(x, 0, x.Length)

            'verifica se tem data e hora separado para unir depois
            'VERIFICA PARA VER QUAL TIPO DE GRAFICO MONTAR = VAI USAR O CAATEGCOL PARA CHECAR LA NA FRENTE
            'I2 - REFERECE A POSIÇÃO DA COLUNA NO RESULTS - NO FUTURO PEGAR OS DADOS
            For i2 As Integer = 0 To (dg_colunas.RowCount - 1)
                If dg_colunas.Rows(i2).Cells(2).Value = "X" Then
                    If dg_colunas.Rows(i2).Cells(1).Value = "Date" Then
                        datacol = i2
                    End If

                    If dg_colunas.Rows(i2).Cells(1).Value = "Time" Then
                        horacol = i2
                    End If
                End If
                If dg_colunas.Rows(i2).Cells(2).Value = "Y Categorized" Then
                    categcol = i2
                End If
                If dg_colunas.Rows(i2).Cells(2).Value = "Y Fixed Line (Max)" Then
                    linhasycol = i2
                End If
                If dg_colunas.Rows(i2).Cells(2).Value = "X Shadow" Then
                    sombrax = i2
                End If
            Next

            'VERIFICA OS VALORES DO EIXO X SE ENCAIXAM PARA TAL
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
                            Dim str As String = dg_result.Rows(i).Cells(i2).Value.ToString().Replace("-", " ").Replace(".", ":") 'dg_result.Rows(i).Cells(i2).Value.ToString()
                            dates = Convert.ToDateTime(str)
                            x(i) = dates.ToOADate()
                            FormsPlot1.Plot.Axes.DateTimeTicks(Edge.Bottom)

                            'ElseIf dg_colunas.Rows(i2).Cells(1).Value = "Decimal" And IsNothing(dg_result.Rows(i).Cells(3).Value.ToString()) = False Then

                            '    Dim dou As Double
                            '    x(i) = Nothing
                            '    Dim str As String = dg_result.Rows(i).Cells(i2).Value.ToString()
                            '    dou = con.ConvertFrom(str)
                            '    x(i) = dou

                        ElseIf dg_colunas.Rows(i2).Cells(1).Value = "Integer" And IsNothing(dg_result.Rows(i).Cells(3).Value.ToString()) = False Then

                            Dim dou As Double
                            x(i) = Nothing
                            Dim str As String = dg_result.Rows(i).Cells(i2).Value.ToString()
                            dou = Math.Round(con.ConvertFrom(str))
                            x(i) = dou

                        ElseIf datacol > -1 And horacol > -1 Then

                            Dim dates As DateTime
                            x(i) = Nothing

                            Dim str As String = dg_result.Rows(i).Cells(datacol).Value.ToString().Replace("-", " ").Replace(".", ":").ToString() & " " & dg_result.Rows(i).Cells(horacol).Value.ToString().Replace(".", ":").ToString()
                            dates = Convert.ToDateTime(str)
                            x(i) = dates.ToOADate()
                            FormsPlot1.Plot.Axes.DateTimeTicks(Edge.Bottom)

                            'ElseIf dg_colunas.Rows(i2).Cells(1).Value = "Data" And IsNothing(dg_result.Rows(i).Cells(3).Value.ToString()) = False Then

                            '    Dim dou As Double
                            '    Dim dates As Date
                            '    x(i) = Nothing
                            '    Dim str As String = dg_result.Rows(i).Cells(0).Value.ToString().Replace("-", " ").Replace(".", ":")
                            '    dates = Convert.ToDateTime(str)
                            '    x(i) = dates.ToOADate()
                            '    FormsPlot1.Plot.Axes.DateTimeTicks(Edge.Bottom)

                            'ElseIf dg_colunas.Rows(i2).Cells(1).Value = "Hora" And IsNothing(dg_result.Rows(i).Cells(3).Value.ToString()) = False Then

                            '    Dim dou As Double
                            '    Dim dates As DateTime
                            '    x(i) = Nothing
                            '    Dim str As String = dg_result.Rows(i).Cells(0).Value.ToString().Replace(".", ":")
                            '    dates = Convert.ToDateTime(str)
                            '    x(i) = dates.ToOADate()
                            '    FormsPlot1.Plot.Axes.DateTimeTicks(Edge.Bottom)

                        End If

                    Next

                End If
            Next


            'CRIA GRAFICO QUANDO NÃO HÁ Y CATEGORIZADO - APENAS UM EIXO NO Y
            For i2 As Integer = 0 To (dg_colunas.RowCount - 1)

                If dg_colunas.Rows(i2).Cells(2).Value = "Y" And categcol = -1 Then

                    For i As Integer = 0 To (dg_result.RowCount - 2)

                        processo.ReportProgress(((i + 1) / (dg_result.RowCount - 1)) * 100)


                        If processo.CancellationPending = True Then
                            Exit Sub
                        End If

                        'If dg_colunas.Rows(i2).Cells(1).Value = "DataHora" And IsNothing(dg_result.Rows(i).Cells(3).Value.ToString()) = False Then

                        '    Dim dates As DateTime
                        '    y(i) = Nothing
                        '    Dim str As String = dg_result.Rows(i).Cells(0).Value.ToString().Replace("-", " ").Replace(".", ":") 'dg_result.Rows(i).Cells(i2).Value.ToString()
                        '    dates = Convert.ToDateTime(str)
                        '    y(i) = dates.ToOADate()

                        'Else
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

                            'ElseIf dg_colunas.Rows(i2).Cells(1).Value = "Data" And IsNothing(dg_result.Rows(i).Cells(3).Value.ToString()) = False Then

                            '    Dim dates As Date
                            '    y(i) = Nothing
                            '    Dim str As String = dg_result.Rows(i).Cells(0).Value.ToString().Replace("-", " ").Replace(".", ":")
                            '    dates = Convert.ToDateTime(str)
                            '    y(i) = dates.ToOADate()

                            'ElseIf dg_colunas.Rows(i2).Cells(1).Value = "Hora" And IsNothing(dg_result.Rows(i).Cells(3).Value.ToString()) = False Then

                            '    Dim dates As DateTime
                            '    y(i) = Nothing
                            '    Dim str As String = dg_result.Rows(i).Cells(0).Value.ToString().Replace(".", ":")
                            '    dates = Convert.ToDateTime(str)
                            '    y(i) = dates.ToOADate()

                        End If

                    Next

                    'FormsPlot1.Plot.SetAxisLimits(x.ToArray().Min, x.ToArray().Max)
                    FormsPlot1.Plot.Axes.SetLimits(x.ToArray().Min, x.ToArray().Max, y.ToArray().Min, y.ToArray().Min)
                    FormsPlot1.Plot.Add.Scatter(x, y).Label = "Busy"

                    Exit For
                End If

            Next


            'CRIA GRAFICO QUANDO HÁ Y CATEGORIZADO
            For i2 As Integer = 0 To (dg_colunas.RowCount - 1)

                If dg_colunas.Rows(i2).Cells(2).Value = "Y Categorized" And categcol > -1 Then

                    '*****************************
                    '----  começando ------------
                    '*****************************

                    Dim LINHAY As Integer 'coluna/linha que contem os valores para adicionar em cada categoria
                    Dim Tam_Array As Integer = 0
                    Dim Tam_Arrayx As Integer = 0



                    For i3 As Integer = 0 To (dg_colunas.RowCount - 1)

                        If dg_colunas.Rows(i3).Cells(2).Value = "Y" Then
                            LINHAY = i3
                            Exit For
                        End If
                    Next

                    Dim categoria() = New String(dg_result.RowCount - 2) {}
                    'Dim x2 = New Double(x.Distinct().ToArray().Count) {}
                    'x2 = x.Distinct().ToArray()

                    For i3 As Integer = 0 To (dg_result.RowCount - 2)
                        Dim str As String = dg_result.Rows(i3).Cells(i2).Value.ToString()
                        categoria(i3) = str
                    Next

                    Dim categoriafiltrada() As String = categoria.Distinct().ToArray()


                    For i3 As Integer = 0 To (categoriafiltrada.Length - 1)


                        Dim pos As Integer = 0

                        '-------- DESCOBRIR O TAMANHO DA ARRAY

                        'For i5 As Integer = 0 To (dg_result.RowCount - 2)

                        '    If categoriafiltrada(i3).ToString() = dg_result.Rows(i5).Cells(i2).Value.ToString() And dg_colunas.Rows(i2).Cells(1).Value = "Texto" Then
                        '        Interlocked.Increment(Tam_Array)
                        '    End If

                        'Next


                        'Dim y2 = New Double(Tam_Array) {}
                        '------------------------------------
                        Dim y2 = New Double(dg_result.RowCount - 2) {}
                        '-----------------------------------

                        For i4 As Integer = 0 To (dg_result.RowCount - 2)

                            If processo.CancellationPending = True Then
                                Exit Sub
                            End If

                            processo.ReportProgress(((i4 + 1) / (dg_result.RowCount - 1)) * 100)


                            y2(i4) = 0

                            If categoriafiltrada(i3).ToString() = dg_result.Rows(i4).Cells(i2).Value.ToString() And dg_colunas.Rows(i2).Cells(1).Value = "Text" Then

                                Dim dou As Double
                                'y2(pos) = Nothing
                                y2(i4) = Nothing
                                Dim str As String = dg_result.Rows(i4).Cells(LINHAY).Value.ToString()
                                dou = con.ConvertFrom(str)
                                'y2(pos) = dou
                                y2(i4) = dou
                                Interlocked.Increment(pos)

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
                            FormsPlot1.Plot.Add.Scatter(x3, y3).Label = categoriafiltrada(i3).ToString()
                        End If
                        'FormsPlot1.Plot.Add.Scatter(x.Distinct().ToArray(), y2).Label = categoriafiltrada(i3).ToString()

                    Next

                    Exit For

                    ' ElseIf dg_colunas.Rows(i2).Cells(2).Value = "Not Applied" Then

                End If

            Next

            For i2 As Integer = 0 To (dg_colunas.RowCount - 1)

                If dg_colunas.Rows(i2).Cells(2).Value = "Y Fixed Line (Max)" And linhasycol > -1 Then

                    Dim qtdvaloreslinha() = New Double(dg_result.RowCount - 2) {}

                    For i3 As Integer = 0 To (dg_result.RowCount - 2)
                        Dim str As String = dg_result.Rows(i3).Cells(i2).Value.ToString()
                        qtdvaloreslinha(i3) = str
                    Next

                    Dim qtdvaloreslinhafiltrada() As Double = qtdvaloreslinha.Distinct().ToArray()

                    Dim linhafx = FormsPlot1.Plot.Add.HorizontalLine(qtdvaloreslinhafiltrada.Max())
                    linhafx.Text = dg_colunas.Rows(i2).Cells(0).Value.ToString()
                    linhafx.LinePattern = LinePattern.Dashed
                    ' Define que a legenda deve ser exibida no lado oposto do eixo y
                    linhafx.LabelOppositeAxis = True

                End If

            Next


            For i2 As Integer = 0 To (dg_colunas.RowCount - 1)

                If dg_colunas.Rows(i2).Cells(2).Value = "X Shadow" And sombrax > -1 Then


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
                        sombra.Label = dg_colunas.Rows(i2).Cells(0).Value.ToString()
                        sombra.FillStyle.Color = Colors.Yellow.WithAlpha(0.1)
                        'sombra.MarkerStyle.IsVisible = True
                        'sombra.MarkerStyle.Shape = MarkerShape.Asterisk
                        'sombra.MarkerStyle.Size = 8
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
            'FormsPlot1.Plot.YAxis.Label.Text = TextBox2.Text
            FormsPlot1.Plot.Title(TextBox1.Text)

            FormsPlot1.Plot.Style.ColorAxes(Color.FromHex("#d7d7d7"))
            FormsPlot1.Plot.Style.ColorGrids(Color.FromHex("#404040"))
            FormsPlot1.Plot.Style.Background(Color.FromHex("#181818"), Color.FromHex("#1f1f1f"))
            FormsPlot1.Plot.Style.ColorLegend(Color.FromHex("#404040"), Color.FromHex("#d7d7d7"), Color.FromHex("#d7d7d7"))



        Catch ex As Exception
            MsgBox("Erro - " & ex.Message.ToString(), MsgBoxStyle.Exclamation, "Erro")
        End Try

    End Sub

    Private Sub Exportar(ByVal processo As BackgroundWorker)
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

                If processo.CancellationPending = True Then
                    Exit Sub
                End If

                processo.ReportProgress(((index1 + 1) / num1) * 100)


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
            MsgBox("Export Cancelled.", MsgBoxStyle.Information, "Cancelled")
            Me.lbl_status.Text = "Cancelled."
            Me.ToolStripProgressBar1.Value = 0
        Else

            Me.lbl_status.Text = "Data exported."
            Me.ToolStripProgressBar1.Value = 100
        End If
    End Sub

    Private Sub BackgroundWorker4_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles BackgroundWorker4.ProgressChanged
        ToolStripProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub ExtrairDaPCOMMToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExtrairDaPCOMMToolStripMenuItem.Click

        txt_entrada.Text = Application.CommonAppDataPath.ToString() & "\" & "Buffer.txt"
        'Timer1.Interval = 3000
        'Me.Timer1.Enabled = True
        'Timer1.Start()

        pccom.Show()

    End Sub

    Private Sub dg_linhas_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dg_linhas.RowsAdded
        AtualizarGridChart()
    End Sub

    Private Sub dg_table_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dg_table.RowsAdded
        AtualizarGridChart()
    End Sub

    Private Sub dg_table_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles dg_table.RowsRemoved
        AtualizarGridChart()
    End Sub

    Private Sub dg_table_RowLeave(sender As Object, e As DataGridViewCellEventArgs) Handles dg_table.RowLeave
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

    Private Sub dg_linhas_RowLeave(sender As Object, e As DataGridViewCellEventArgs) Handles dg_linhas.RowLeave
        AtualizarGridChart()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        'If pccom.NumericUpDown2.Value <> 0 And pccom.CheckBox1.Checked = True And pccom.BackgroundWorker1.IsBusy = True Then
        '  btn_exec_Click(sender, e)
        '  Timer1.Interval = (pccom.NumericUpDown2.Value * 1000)
        '  Timer2.Interval = 1000
        '  txttime.Text = Timer1.Interval / 1000
        '  Timer2.Stop()
        '  Timer2.Start()
        'End If

        'If Timer1.Enabled = False Then
        'Timer2.Stop()
        'txttime.Text = "0"
        'End If

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick

        If pccom.NumericUpDown2.Value <> 0 And pccom.CheckBox1.Checked = True And pccom.BackgroundWorker1.IsBusy = True Then
            If txttime.Text <> 0 Then
                txttime.Text = Convert.ToInt32(txttime.Text) - 1
            End If
        End If
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        dg_linhas.Rows.Clear()
        dg_table.Rows.Clear()
        txtseparador.Text = "  RMF "
        Me.dg_linhas.Rows.Add("DateTime", "3", "38", "8", "0", "Date", "X")
        Me.dg_linhas.Rows.Add("Hora", "3", "54", "8", "0", "Time", "X")
        Me.dg_linhas.Rows.Add("System", "3", "26", "4", "0", "Text", "Not Applied")
        Me.dg_linhas.Rows.Add("Range", "3", "71", "3", "0", "Decimal", "Not Applied")
        Me.dg_linhas.Rows.Add("CPUModel", "5", "36", "3", "0", "Decimal", "Not Applied")
        Me.dg_linhas.Rows.Add("zModel", "5", "25", "5", "0", "Decimal", "Not Applied")
        Me.dg_linhas.Rows.Add("CPCCpct", "6", "18", "4", "0", "Decimal", "Not Applied")
        Me.dg_linhas.Rows.Add("4HRAAVG", "6", "56", "5", "0", "Decimal", "Not Applied")
        Me.dg_linhas.Rows.Add("ImageCpct", "7", "16", "6", "0", "Decimal", "Not Applied")
        Me.dg_linhas.Rows.Add("WLMCapp", "7", "41", "5", "0", "Decimal", "Not Applied")
        Me.dg_table.Rows.Add("LPAR", "14", "30", "1", "8", "Text", "Y Categorized", "")
        Me.dg_table.Rows.Add("MSU", "14", "30", "19", "4", "Decimal", "Y", "")
        TextBox1.Text = "CPC"
    End Sub

    Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton5.CheckedChanged
        dg_linhas.Rows.Clear()
        dg_table.Rows.Clear()
        txtseparador.Text = "  RMF "
        Me.dg_linhas.Rows.Add("DateTime", "3", "38", "8", "0", "Date", "X")
        Me.dg_linhas.Rows.Add("Hora", "3", "54", "8", "0", "Time", "X")
        Me.dg_linhas.Rows.Add("System", "3", "26", "4", "0", "Text", "Not Applied")
        Me.dg_linhas.Rows.Add("Range", "3", "71", "3", "0", "Decimal", "Not Applied")
        Me.dg_table.Rows.Add("Job", "8", "300", "1", "8", "Text", "Y Categorized", "")
        Me.dg_table.Rows.Add("Busy", "8", "300", "23", "6", "Decimal", "Y", "")
        Me.dg_table.Rows.Add("ServClass", "8", "300", "13", "8", "Text", "Not Applied", "")
        Me.dg_table.Rows.Add("SX", "8", "300", "10", "2", "Text", "Not Applied", "")
        TextBox1.Text = "PROCU"
    End Sub

    Private Sub RadioButton6_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton6.CheckedChanged
        dg_linhas.Rows.Clear()
        dg_table.Rows.Clear()
        txtseparador.Text = "  RMF "
        Me.dg_linhas.Rows.Add("DateTime", "3", "38", "8", "0", "Date", "X")
        Me.dg_linhas.Rows.Add("Hora", "3", "54", "8", "0", "Time", "X")
        Me.dg_linhas.Rows.Add("System", "3", "26", "4", "0", "Text", "Not Applied")
        Me.dg_linhas.Rows.Add("Range", "3", "71", "3", "0", "Decimal", "Not Applied")

        Me.dg_table.Rows.Add("ID", "8", "40", "1", "2", "Text", "Y Categorized", "")
        Me.dg_table.Rows.Add("Type", "8", "40", "11", "4", "Decimal", "Not Applied", "")
        Me.dg_table.Rows.Add("Part%", "8", "40", "21", "4", "Text", "Y", "")
        Me.dg_table.Rows.Add("Tot%", "8", "40", "26", "4", "Text", "Not Applied", "")
        TextBox1.Text = "Channel"
    End Sub

    Private Sub RadioButton7_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton7.CheckedChanged
        dg_linhas.Rows.Clear()
        dg_table.Rows.Clear()
        txtseparador.Text = "  RMF "
        Me.dg_linhas.Rows.Add("DateTime", "3", "38", "8", "0", "Date", "X")
        Me.dg_linhas.Rows.Add("Hora", "3", "54", "8", "0", "Time", "X")
        Me.dg_linhas.Rows.Add("System", "3", "26", "4", "0", "Text", "Not Applied")
        Me.dg_linhas.Rows.Add("Range", "3", "71", "3", "0", "Decimal", "Not Applied")
        Me.dg_linhas.Rows.Add("Kernel", "5", "18", "11", "0", "Text", "Not Applied")
        Me.dg_linhas.Rows.Add("BPXPRM", "6", "8", "40", "0", "Text", "Not Applied")

        Me.dg_table.Rows.Add("Job", "11", "300", "1", "8", "Text", "Y Categorized", "")
        Me.dg_table.Rows.Add("User", "11", "300", "11", "8", "Text", "Not Applied", "")
        Me.dg_table.Rows.Add("ASID", "11", "300", "21", "4", "Text", "Not Applied", "")
        Me.dg_table.Rows.Add("Apply%", "11", "300", "60", "5", "Decimal", "Not Applied", "")
        Me.dg_table.Rows.Add("TotalSRB", "11", "300", "67", "5", "Decimal", "Y", "")
        Me.dg_table.Rows.Add("Server", "11", "300", "75", "4", "Text", "Not Applied", "")
        TextBox1.Text = "OMVS"
    End Sub

    Private Sub RadioButton8_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton8.CheckedChanged
        dg_linhas.Rows.Clear()
        dg_table.Rows.Clear()
        txtseparador.Text = "  RMF "
        Me.dg_linhas.Rows.Add("DateTime", "3", "40", "8", "0", "Date", "X")
        Me.dg_linhas.Rows.Add("Hora", "3", "55", "8", "0", "Time", "X")
        Me.dg_linhas.Rows.Add("Samples", "3", "14", "4", "0", "Text", "Not Applied")
        Me.dg_linhas.Rows.Add("Policy", "8", "21", "20", "0", "Text", "Not Applied")
        Me.dg_linhas.Rows.Add("Activaded", "8", "57", "20", "0", "Text", "Not Applied")

        Me.dg_table.Rows.Add("Name", "14", "300", "1", "8", "Text", "Y Categorized", "")
        Me.dg_table.Rows.Add("ExecGoal", "14", "300", "16", "4", "Decimal", "Not Applied", "")
        Me.dg_table.Rows.Add("VelAct", "14", "300", "21", "3", "Decimal", "Not Applied", "")
        Me.dg_table.Rows.Add("RespTimeGoal", "14", "300", "31", "3", "Decimal", "Not Applied", "")
        Me.dg_table.Rows.Add("RespTimeActual", "14", "300", "43", "3", "Decimal", "Not Applied", "")
        Me.dg_table.Rows.Add("PerfIndx", "14", "300", "49", "4", "Decimal", "Y", "")
        Me.dg_table.Rows.Add("TransEnded", "14", "300", "55", "5", "Decimal", "Not Applied", "")
        Me.dg_table.Rows.Add("AvgWait", "14", "300", "61", "5", "Decimal", "Not Applied", "")
        Me.dg_table.Rows.Add("RespExec", "14", "300", "68", "5", "Decimal", "Not Applied", "")
        Me.dg_table.Rows.Add("TimeActual", "14", "300", "75", "5", "Decimal", "Not Applied", "")
        TextBox1.Text = "SysSum"

    End Sub


End Class
