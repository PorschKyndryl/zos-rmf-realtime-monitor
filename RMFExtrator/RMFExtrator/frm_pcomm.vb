
Imports System.IO
Imports Zuby.ADGV
Imports System.ComponentModel
Imports RMFExtrator.EHLLAPI
Imports System.Threading
Imports System.Text
Imports OpenTK.Graphics.OpenGL

Public Class frm_pcomm
    Private Sub frm_pcomm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If File.Exists(Application.StartupPath.ToString() & "\" & "Buffer.txt") Then
            Dim myStreamReader As System.IO.StreamReader
            Try
                myStreamReader = System.IO.File.OpenText(Application.StartupPath.ToString() & "\" & "Buffer.txt")
                RichTextBox1.Text = myStreamReader.ReadToEnd()
                RichTextBox1.SelectionStart = RichTextBox1.Text.Length
                RichTextBox1.ScrollToCaret()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information)
            End Try
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If EHLLAPI.EhllapiWrapper.Connect(ToolStripComboBox1.Text).ToString() = 0 Then
            lblstatus.Text = "Conectado"
            lblstatus.ForeColor = Color.Green
        ElseIf EHLLAPI.EhllapiWrapper.Connect(ToolStripComboBox1.Text).ToString() = 4 Then
            lblstatus.Text = "Estabelecendo conexão..."
            lblstatus.ForeColor = Color.Yellow
        Else
            lblstatus.Text = "Offline"
            lblstatus.ForeColor = Color.Red
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        Dim y As Integer = 0
        Dim str As String = ""


        Dim streamWriter As New IO.StreamWriter(Application.StartupPath.ToString() & "\" & "Buffer.txt", True)

        While y < 80
            Interlocked.Increment(y)
            EhllapiWrapper.ReadScreen(Convert.ToInt32((y * 80) - 79), Convert.ToInt32(80), str)
            If str.Length >= 80 Then
                'IO.File.AppendAllText(Application.StartupPath.ToString() & "\" & "Buffer.txt", str.Substring(0, 80) + ControlChars.Cr)
                streamWriter.WriteLine(str.Substring(0, 80))
            End If
        End While

        streamWriter.Close()

        Dim myStreamReader As System.IO.StreamReader

        Try
            myStreamReader = System.IO.File.OpenText(Application.StartupPath.ToString() & "\" & "Buffer.txt")
            RichTextBox1.Text = myStreamReader.ReadToEnd()
            myStreamReader.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try



        RichTextBox1.SelectionStart = RichTextBox1.Text.Length
        RichTextBox1.ScrollToCaret()

        'Data available only from 04/30/23 07.26.40 to 05/04/23 08.51.40.
        '     EssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssN       
        'e Data available only from 04/30/23 07.26.40 to 05/04/23 08.53.20. e       
        'F1 DssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssM LE    

        '

    End Sub

    Private Sub frm_pcomm_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
        If File.Exists(Application.StartupPath.ToString() & "\" & "Buffer.txt") Then
            Form1.txt_entrada.Text = Application.StartupPath.ToString() & "\" & "Buffer.txt"
        End If
    End Sub

    Private Sub Aguardar(ByVal Ate As Integer)

        '0 The function was successful; host PS is unlocked and ready for input.
        '1 The application was Not connected with a host PS.
        '4 Host session timed out while in XCLOCK Or XSTATUS state.
        '5 Keyboard Is locked.
        '9 A system error occurred.

        Dim i As Integer = 0
        While i <> Ate
            i = EHLLAPI.EhllapiWrapper.Wait()
        End While
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If BackgroundWorker1.IsBusy = True Then
            BackgroundWorker1.CancelAsync()
            Button3.Text = "Iniciar Captura"
            Form1.Timer1.Stop()
            Form1.Timer2.Stop()
            Exit Sub
        End If
        BackgroundWorker1.RunWorkerAsync()
        Button3.Text = "Parar..."
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Dim p As Integer
        Dim linha As Integer = 0
        Dim col As Integer = 0
        EhllapiWrapper.GetCursorPos(p)
        linha = p \ 80
        col = Math.Round(p Mod 80)
        NumInicio.Value = linha + 1

        'EhllapiWrapper.SetCursorPos(Convert.ToInt32(Me.txtCursorPos.Text))

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        Dim p As Integer
        Dim linha As Integer = 0
        Dim col As Integer = 0
        EhllapiWrapper.GetCursorPos(p)
        linha = p \ 80
        col = Math.Round(p Mod 80)
        NumFim.Value = linha + 1

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click, Button7.Click
        If File.Exists(Application.StartupPath.ToString() & "\" & "Buffer.txt") Then
            IO.File.Delete(Application.StartupPath.ToString() & "\" & "Buffer.txt")
            RichTextBox1.Text = ""
        End If
        If File.Exists(Application.StartupPath.ToString() & "\" & "Buffer.txt") Then
            Dim myStreamReader As System.IO.StreamReader
            Try
                myStreamReader = System.IO.File.OpenText(Application.StartupPath.ToString() & "\" & "Buffer.txt")
                RichTextBox1.Text = myStreamReader.ReadToEnd()
                RichTextBox1.SelectionStart = RichTextBox1.Text.Length
                RichTextBox1.ScrollToCaret()
                myStreamReader.Close()
            Catch ex As Exception
                myStreamReader.Close()
                MsgBox(ex.Message, MsgBoxStyle.Information)
            End Try
        End If

    End Sub

    Friend Delegate Sub SetNumericUpDownCallback(ByVal text As Integer)

    Private Sub SetNumericUpDown(ByVal text As Integer)
        If Me.NumericUpDown2.InvokeRequired Then
            Dim d As SetNumericUpDownCallback = New SetNumericUpDownCallback(AddressOf SetNumericUpDown)
            Me.Invoke(d, New Object() {text})
        Else
            Me.NumericUpDown2.Value = text
        End If
    End Sub

    Friend Delegate Sub SetRichTextBox1HereCallback(ByVal text As String)
    Private Sub SetRichTextBox1Here(ByVal text As String)
        If Me.RichTextBox1.InvokeRequired Then
            Dim d As SetRichTextBox1HereCallback = New SetRichTextBox1HereCallback(AddressOf SetRichTextBox1Here)
            Me.Invoke(d, New Object() {text})
        Else
            Me.RichTextBox1.Text = text
            RichTextBox1.SelectionStart = RichTextBox1.Text.Length
            RichTextBox1.ScrollToCaret()
        End If
    End Sub

    Friend Delegate Sub Settxt_entradaCallback(ByVal text As String)
    Private Sub Settxt_entrada(ByVal text As String)
        If Form1.txt_entrada.InvokeRequired Then
            Dim d As Settxt_entradaCallback = New Settxt_entradaCallback(AddressOf Settxt_entrada)
            Me.Invoke(d, New Object() {text})
        Else
            Form1.txt_entrada.Text = text
        End If
    End Sub

    Friend Delegate Sub SetToolStripStatusLabel3Callback(ByVal text As String)
    Private Sub SetToolStripStatusLabel3(ByVal text As String)
        If Me.RichTextBox1.InvokeRequired Then
            Dim d As SetToolStripStatusLabel3Callback = New SetToolStripStatusLabel3Callback(AddressOf SetToolStripStatusLabel3)
            Me.Invoke(d, New Object() {text})
        Else
            Me.ToolStripStatusLabel3.Text = text
        End If
    End Sub
    Private Sub CapturarProcU(sender As Object, e As EventArgs)

        Dim flag As Boolean = False
        Dim flag2 As Boolean = False
        Dim primeirapag As Boolean = True
        Dim range As Integer = 0


        While flag2 = False

            If BackgroundWorker1.CancellationPending = True Then
                Exit Sub
            End If

            SetToolStripStatusLabel3("Extraindo...")

            Dim I As Integer = 0
            Dim y As Integer = 1
            Dim str As String = ""
            flag = False
            primeirapag = True

            '-----------------------------------------
            'ADICIONAR ESSE TRABALHO EM BACKGROUDWORK
            '-----------------------------------------

            '-----------------------------------------
            'VERIFICA SE RECEBI A TELA SEM DADOS E PARA
            '-----------------------------------------

            While y < 25
                EhllapiWrapper.ReadScreen(Convert.ToInt32((y * 80) - 79), Convert.ToInt32(80), str)
                If str.Length >= 80 Then
                    If str.Substring(0, 80).Contains("Range: ") Then
                        range = Convert.ToInt16(Trim(str.Substring((str.IndexOf("Range: ") + 6), 5)))

                    End If
                    If str.Substring(0, 80).Contains("Data available only from") Then
                        flag2 = True
                        If CheckBox1.Checked = True Then
                            '
                            'Roda o extrator, duplicados e plotagem
                            '
                            flag = False
                            flag2 = False
                            primeirapag = True
                            SetNumericUpDown(range)

                            'Settxt_entrada(Application.StartupPath.ToString() & "Buffer.txt")
                            'Form1.AbrirTXT(Application.StartupPath.ToString() & "Buffer.txt", True)
                            'Dim sender As Object
                            'Dim e As EventArgs

                            ' Obter uma referência ao formulário desejado
                            Dim form As Form1 = Nothing
                            For Each f As Form In Application.OpenForms
                                If TypeOf f Is Form1 Then
                                    form = f
                                    Exit For
                                End If
                            Next

                            ' Verificar se o formulário foi encontrado
                            If form IsNot Nothing Then
                                ' Chamar o método desejado no formulário
                                form.Invoke(Sub() form.btn_exec_Click(sender, e))
                                form.Invoke(Sub() form.Timer2.Start())
                                form.Invoke(Sub() form.txttime.Text = range.ToString())
                            End If



                            SetToolStripStatusLabel3("Aguardando novos dados...")
                            Thread.Sleep(1000 * range)

                            If BackgroundWorker1.CancellationPending = True Then
                                Exit Sub
                            End If

                            EHLLAPI.EhllapiWrapper.SendStr("C")
                            EHLLAPI.EhllapiWrapper.SendStr("U")
                            EHLLAPI.EhllapiWrapper.SendStr("R")
                            EHLLAPI.EhllapiWrapper.SendStr("R")
                            EHLLAPI.EhllapiWrapper.SendStr("E")
                            EHLLAPI.EhllapiWrapper.SendStr("N")
                            EHLLAPI.EhllapiWrapper.SendStr("T")
                            EHLLAPI.EhllapiWrapper.SendStr("@E")

                            Aguardar(0)
                            Thread.Sleep(1500)

                            Exit While

                        End If
                        Exit Sub
                    End If
                End If
                Interlocked.Increment(y)
            End While

            '-----------------------------------------
            'APOS O TEMPO - COLETA AS INFO
            '-----------------------------------------

            While flag = False

                If BackgroundWorker1.CancellationPending = True Then
                    Exit Sub
                End If

                Aguardar(0)

                If primeirapag = True Then

                    '*****************************************
                    'COPIAR APENAS AS LINHAS QUE QUER
                    '*****************************************

                    Dim y2 As Integer = 0
                    Dim str2 As String = ""


                    Dim streamWriter2 As New IO.StreamWriter(Application.StartupPath.ToString() & "\" & "Buffer.txt", True)

                    While y2 < 25
                        Interlocked.Increment(y2)
                        EhllapiWrapper.ReadScreen(Convert.ToInt32((y2 * 80) - 79), Convert.ToInt32(80), str2)
                        If str2.Length >= 80 Then
                            If y2 < 23 Then
                                streamWriter2.WriteLine(str2.Substring(0, 80))
                            End If
                        End If
                    End While

                    streamWriter2.Close()

                    primeirapag = False

                Else

                    '*****************************************
                    'COPIAR APENAS AS LINHAS QUE QUER
                    '*****************************************

                    Dim y2 As Integer = 0
                    Dim str2 As String = ""
                    Dim ultimafalsa As Boolean = False
                    Dim streamWriter2 As New IO.StreamWriter(Application.StartupPath.ToString() & "\" & "Buffer.txt", True)

                    While y2 < 25
                        Interlocked.Increment(y2)
                        EhllapiWrapper.ReadScreen(Convert.ToInt32((y2 * 80) - 79), Convert.ToInt32(80), str2)
                        If y2 = (NumInicio.Value + 1) Then
                            If Trim(str2.Substring(0, 80)) = "" Then
                                '  PODE SER ULTIMA PAGINA FALSA
                                ultimafalsa = True
                            End If
                        ElseIf y2 = (NumFim.Value) And ultimafalsa = True Then
                            Exit While
                        End If
                        If str2.Length >= 80 Then
                            If y2 >= NumInicio.Value And y2 <= NumFim.Value Then
                                If Trim(str2.Substring(0, 80)) <> "" Then
                                    streamWriter2.WriteLine(str2.Substring(0, 80))
                                End If
                            End If
                        End If
                    End While

                    streamWriter2.Close()

                End If

                Aguardar(0)

                '*****************************************
                'PLOTAR
                '*****************************************

                If File.Exists(Application.StartupPath.ToString() & "\" & "Buffer.txt") Then
                    Dim myStreamReader As System.IO.StreamReader
                    Try
                        myStreamReader = System.IO.File.OpenText(Application.StartupPath.ToString() & "\" & "Buffer.txt")
                        SetRichTextBox1Here(myStreamReader.ReadToEnd())

                        myStreamReader.Close()
                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.Information)
                    End Try
                End If

                If Not CheckBox2.Checked Then
                    '-----------------------------------------
                    'VERIFICA SE A ULTIMA LINHA É VAZIA PARA PODER PASSAR PARA PROXIMA PAGINA (PF11) OU DESCE A PAGINA (PF8)
                    '-----------------------------------------
                    y = 1
                    While y < 25
                        EhllapiWrapper.ReadScreen(Convert.ToInt32((y * 80) - 79), Convert.ToInt32(80), str)
                        If y = NumFim.Value Then
                            If Trim(str.Substring(0, 80)) = "" Then
                                flag = True

                                '-----------------------------------------
                                'PODERIA CRIAR UMA SEGUNDA VERIFICACAO DANDO MAIS UN PF8 E COMPARAR DE APARECE 2 CAMPOS VAZIOS, E SE O PRIMEIRO CAMPO, É IGUAL AO PENULTIMO
                                'SERIA UMA DUPLA SEGURANÇA DE QUE PEGOU TUDO CERTINHO
                                '-----------------------------------------

                                '************************************
                                '************************************
                                '************************************
                                '************************************
                                '************************************
                                '************************************
                                '************************************


                                Interlocked.Increment(y)
                                Exit While
                            End If
                        End If
                        Interlocked.Increment(y)
                    End While


                    Aguardar(0)
                    If flag = False Then
                        EHLLAPI.EhllapiWrapper.SendStr("@8")
                        Aguardar(0)
                        Thread.Sleep(1500)
                    End If
                Else
                    Exit While
                End If

                Interlocked.Increment(I)

            End While

            EHLLAPI.EhllapiWrapper.SendStr("@b")
            Aguardar(0)
            Thread.Sleep(1500)

        End While
    End Sub
    Private Sub BackgroundWorker1_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        If NumFim.Value = 1 And NumInicio.Value = 1 Then
            MsgBox("Defina os campos de inicio e fim da tabela para copy.")
            NumInicio.Focus()
            Exit Sub
        End If

        CapturarProcU(sender, e)

        'Timer2.Stop()

        'http://www.novell.com/documentation/securelogin61/nsl61_application_definition_guide/data/bcdh7ho.html


    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        If (e.Cancelled = True) Then
            SetToolStripStatusLabel3("Extracao parada")
        Else
            SetToolStripStatusLabel3("Extracao Concluida")
        End If
    End Sub

    Private Sub frm_pcomm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Me.Hide()
        e.Cancel = True
    End Sub
End Class