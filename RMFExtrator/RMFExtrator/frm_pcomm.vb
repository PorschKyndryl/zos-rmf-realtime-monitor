Imports System.IO
Imports System.ComponentModel
Imports System.Threading
Imports System.Text
Imports RMFExtrator.EHLLAPI

' NOTE:
' - Public names (frm_pcomm and all control names) are preserved to avoid designer issues.
' - All user-facing text and comments are in English.
' - Risky IO patterns were replaced with Using blocks.
' - Obvious magic numbers (80 columns, 24/25 rows) are documented.
' - Minor robustness improvements were added, but behavior is unchanged.

Public Class frm_pcomm

    ' ─────────────────────────────────────────────────────────────────────────────
    ' • Form lifecycle
    ' ─────────────────────────────────────────────────────────────────────────────

    Private Sub frm_pcomm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' When the tool opens, show whatever was captured last time (Buffer.txt)
        Dim bufferPath As String = Path.Combine(Application.StartupPath, "Buffer.txt")
        If File.Exists(bufferPath) Then
            Try
                Using sr As New StreamReader(bufferPath, Encoding.UTF8, detectEncodingFromByteOrderMarks:=True)
                    RichTextBox1.Text = sr.ReadToEnd()
                End Using
                RichTextBox1.SelectionStart = RichTextBox1.TextLength
                RichTextBox1.ScrollToCaret()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Read Buffer", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try
        End If
    End Sub

    Private Sub frm_pcomm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Keep the window alive (hide instead of dispose)
        Me.Hide()
        e.Cancel = True
    End Sub

    Private Sub frm_pcomm_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
        ' If a buffer exists, reflect it back to the main form’s input path
        Dim bufferPath As String = Path.Combine(Application.StartupPath, "Buffer.txt")
        If File.Exists(bufferPath) Then
            Form1.txt_entrada.Text = bufferPath
        End If
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' • Connection status poll (PCOMM EHLLAPI)
    ' ─────────────────────────────────────────────────────────────────────────────

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' Poll session connection status every tick and color the status accordingly
        ' EHLLAPI Connect result codes (typical):
        '  0 = OK (unlocked), 4 = establishing / timing, others = offline
        Dim sessionId As String = ToolStripComboBox1.Text
        Dim resultCode As Integer = EhllapiWrapper.Connect(sessionId)

        Select Case resultCode
            Case 0
                lblstatus.Text = "Connected"
                lblstatus.ForeColor = Color.Green
            Case 4
                lblstatus.Text = "Establishing connection..."
                lblstatus.ForeColor = Color.Yellow
            Case Else
                lblstatus.Text = "Offline"
                lblstatus.ForeColor = Color.Red
        End Select
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' • Manual screen capture (whole 80x lines)
    ' ─────────────────────────────────────────────────────────────────────────────

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Captures a full screen (80 columns x up to 80 lines as in legacy code)
        Const COLS As Integer = 80

        Dim bufferPath As String = Path.Combine(Application.StartupPath, "Buffer.txt")
        ' Append mode = True (keeps previous content)
        Using sw As New StreamWriter(bufferPath, append:=True, encoding:=Encoding.UTF8)
            For row As Integer = 1 To 80
                Dim lineText As String = ""
                ' ReadScreen(startPos, length, outText)
                ' startPos is 1-based position in a linear 80-col grid: row*80 - 79
                EhllapiWrapper.ReadScreen((row * COLS) - 79, COLS, lineText)
                If lineText.Length >= COLS Then
                    sw.WriteLine(lineText.Substring(0, COLS))
                End If
            Next
        End Using

        ' Refresh UI with the new buffer content
        Try
            Using sr As New StreamReader(bufferPath, Encoding.UTF8, detectEncodingFromByteOrderMarks:=True)
                RichTextBox1.Text = sr.ReadToEnd()
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Read Buffer", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

        RichTextBox1.SelectionStart = RichTextBox1.TextLength
        RichTextBox1.ScrollToCaret()
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' • Start/Stop automated capture (BackgroundWorker)
    ' ─────────────────────────────────────────────────────────────────────────────

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If BackgroundWorker1.IsBusy Then
            ' Request cancellation
            BackgroundWorker1.CancelAsync()
            Button3.Text = "Start Capture"
            Form1.Timer1.Stop()
            Form1.Timer2.Stop()
            Exit Sub
        End If

        BackgroundWorker1.RunWorkerAsync()
        Button3.Text = "Stop..."
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' • Cursor helpers (get current row into start/end selectors)
    ' ─────────────────────────────────────────────────────────────────────────────

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' Get current cursor line and place into NumInicio
        Const COLS As Integer = 80

        Dim linearPos As Integer = 0
        EhllapiWrapper.GetCursorPos(linearPos)

        Dim row As Integer = linearPos \ COLS
        Dim col As Integer = linearPos Mod COLS
        ' EHLLAPI positions are 1-based for many operations; UI shows rows 1..N
        NumInicio.Value = row + 1
        ' col is not used here but kept for clarity
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ' Get current cursor line and place into NumFim
        Const COLS As Integer = 80

        Dim linearPos As Integer = 0
        EhllapiWrapper.GetCursorPos(linearPos)

        Dim row As Integer = linearPos \ COLS
        Dim col As Integer = linearPos Mod COLS
        NumFim.Value = row + 1
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' • Clear buffer (also bound to Button7)
    ' ─────────────────────────────────────────────────────────────────────────────

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click, Button7.Click
        Dim bufferPath As String = Path.Combine(Application.StartupPath, "Buffer.txt")

        Try
            If File.Exists(bufferPath) Then
                File.Delete(bufferPath)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Delete Buffer", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

        RichTextBox1.Clear()

        ' If deletion failed for some reason but file exists, reload it to UI
        If File.Exists(bufferPath) Then
            Try
                Using sr As New StreamReader(bufferPath, Encoding.UTF8, detectEncodingFromByteOrderMarks:=True)
                    RichTextBox1.Text = sr.ReadToEnd()
                End Using
                RichTextBox1.SelectionStart = RichTextBox1.TextLength
                RichTextBox1.ScrollToCaret()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Read Buffer", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try
        End If
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' • Safe UI updates from worker thread
    ' ─────────────────────────────────────────────────────────────────────────────

    Friend Delegate Sub SetNumericUpDownCallback(ByVal value As Integer)
    Private Sub SetNumericUpDownSafe(ByVal value As Integer)
        If NumericUpDown2.InvokeRequired Then
            Dim d As New SetNumericUpDownCallback(AddressOf SetNumericUpDownSafe)
            Me.Invoke(d, New Object() {value})
        Else
            NumericUpDown2.Value = value
        End If
    End Sub

    Friend Delegate Sub SetRichTextBoxCallback(ByVal text As String)
    Private Sub SetRichTextBoxSafe(ByVal text As String)
        If RichTextBox1.InvokeRequired Then
            Dim d As New SetRichTextBoxCallback(AddressOf SetRichTextBoxSafe)
            Me.Invoke(d, New Object() {text})
        Else
            RichTextBox1.Text = text
            RichTextBox1.SelectionStart = RichTextBox1.TextLength
            RichTextBox1.ScrollToCaret()
        End If
    End Sub

    Friend Delegate Sub SetMainFormInputCallback(ByVal text As String)
    Private Sub SetMainFormInputSafe(ByVal text As String)
        If Form1.txt_entrada.InvokeRequired Then
            Dim d As New SetMainFormInputCallback(AddressOf SetMainFormInputSafe)
            Me.Invoke(d, New Object() {text})
        Else
            Form1.txt_entrada.Text = text
        End If
    End Sub

    Friend Delegate Sub SetStatusTextCallback(ByVal text As String)
    Private Sub SetStatusTextSafe(ByVal text As String)
        If ToolStripStatusLabel3.GetCurrentParent()?.InvokeRequired = True Then
            Dim d As New SetStatusTextCallback(AddressOf SetStatusTextSafe)
            Me.Invoke(d, New Object() {text})
        Else
            ToolStripStatusLabel3.Text = text
        End If
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' • EHLLAPI synchronization (Wait)
    ' ─────────────────────────────────────────────────────────────────────────────

    ''' <summary>
    ''' Waits until EHLLAPI returns the expected host state code.
    ''' Common values: 0 = ready/unlocked, 1 = not connected, 4 = timeout in XCLOCK/XSTATUS, 5 = keyboard locked, 9 = system error.
    ''' </summary>
    Private Sub WaitForHost(ByVal expectedCode As Integer)
        Dim code As Integer = -1
        Do
            code = EhllapiWrapper.Wait()
        Loop While code <> expectedCode
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' • Automated extraction for PROC/U-like screens (background thread)
    '   - Detects "Date available only from ..." end condition
    '   - Uses line range selection (NumInicio..NumFim)
    '   - Optionally runs continuously on a timer ("real-time capture")
    ' ─────────────────────────────────────────────────────────────────────────────

    Private Sub CaptureProcU(sender As Object, e As EventArgs)
        Const COLS As Integer = 80          ' Screen width (columns)
        Const ROWS_SCAN As Integer = 25     ' Scan rows (the legacy code used 25 lines per pass)
        Const HEADER_STOP_TEXT As String = "Date available only from"
        Const RANGE_LABEL As String = "Range: "

        Dim reachedEndOfData As Boolean = False
        Dim stopLoop As Boolean = False
        Dim firstPage As Boolean = True
        Dim rangeSeconds As Integer = 0

        While Not stopLoop
            If BackgroundWorker1.CancellationPending Then Exit Sub

            SetStatusTextSafe("Extracting...")

            Dim y As Integer = 1
            Dim line As String = String.Empty
            reachedEndOfData = False
            firstPage = True

            ' ── Scan current screen for the "Range" and end-of-data message
            While y < ROWS_SCAN
                EhllapiWrapper.ReadScreen((y * COLS) - 79, COLS, line)

                If line.Length >= COLS Then
                    ' Extract "Range: NNNNN"
                    If line.Contains(RANGE_LABEL) Then
                        Dim idx As Integer = line.IndexOf(RANGE_LABEL, StringComparison.Ordinal)
                        If idx >= 0 AndAlso (idx + 6 + 5) <= line.Length Then
                            Dim raw As String = line.Substring(idx + RANGE_LABEL.Length, 5)
                            Dim parsed As Integer
                            If Integer.TryParse(raw.Trim(), parsed) Then
                                rangeSeconds = parsed
                            End If
                        End If
                    End If

                    ' Detect end-of-data message
                    If line.Contains(HEADER_STOP_TEXT) Then
                        reachedEndOfData = True

                        If CheckBox1.Checked Then
                            ' Real-time capture:
                            ' 1) Trigger downstream extractor in Form1
                            ' 2) Wait the detected "Range" time
                            ' 3) Issue the "CURRENT@E" sequence (as in original code)
                            ' 4) Loop again

                            SetNumericUpDownSafe(rangeSeconds)

                            ' Find main Form1 (if open) and trigger actions
                            Dim mainForm As Form1 = Nothing
                            For Each f As Form In Application.OpenForms
                                If TypeOf f Is Form1 Then
                                    mainForm = DirectCast(f, Form1)
                                    Exit For
                                End If
                            Next

                            If mainForm IsNot Nothing Then
                                ' Call Form1 event-like method safely on UI thread
                                mainForm.Invoke(Sub() mainForm.btn_exec_Click(sender, e))
                                mainForm.Invoke(Sub() mainForm.Timer2.Start())
                                mainForm.Invoke(Sub() mainForm.txttime.Text = rangeSeconds.ToString())
                            End If

                            ' Wait for next update window
                            SetStatusTextSafe("Waiting for new data...")
                            Thread.Sleep(Math.Max(0, rangeSeconds) * 1000)

                            If BackgroundWorker1.CancellationPending Then Exit Sub

                            ' Send: C U R R E N T + Enter (@E)
                            EhllapiWrapper.SendStr("C")
                            EhllapiWrapper.SendStr("U")
                            EhllapiWrapper.SendStr("R")
                            EhllapiWrapper.SendStr("R")
                            EhllapiWrapper.SendStr("E")
                            EhllapiWrapper.SendStr("N")
                            EhllapiWrapper.SendStr("T")
                            EhllapiWrapper.SendStr("@E")

                            WaitForHost(0)
                            Thread.Sleep(1500)

                            Exit While ' re-scan the new screen
                        End If

                        ' Not real-time: stop after reaching end
                        Exit Sub
                    End If
                End If

                y += 1
            End While

            ' ── Collect lines into Buffer.txt
            Dim bufferPath As String = Path.Combine(Application.StartupPath, "Buffer.txt")

            If firstPage Then
                ' First page: keep the header and body up to line 22 (legacy behavior)
                Using sw As New StreamWriter(bufferPath, append:=True, encoding:=Encoding.UTF8)
                    For r As Integer = 1 To ROWS_SCAN
                        Dim rowText As String = ""
                        EhllapiWrapper.ReadScreen((r * COLS) - 79, COLS, rowText)
                        If rowText.Length >= COLS AndAlso r < 23 Then
                            sw.WriteLine(rowText.Substring(0, COLS))
                        End If
                    Next
                End Using
                firstPage = False
            Else
                ' Subsequent pages: only the selected region (NumInicio..NumFim)
                Dim lastPageBlankCandidate As Boolean = False
                Using sw As New StreamWriter(bufferPath, append:=True, encoding:=Encoding.UTF8)
                    For r As Integer = 1 To ROWS_SCAN
                        Dim rowText As String = ""
                        EhllapiWrapper.ReadScreen((r * COLS) - 79, COLS, rowText)

                        ' Heuristic used in the legacy code to detect a "fake" last page
                        If r = (CInt(NumInicio.Value) + 1) Then
                            If rowText.Trim().Length = 0 Then
                                lastPageBlankCandidate = True
                            End If
                        ElseIf r = CInt(NumFim.Value) AndAlso lastPageBlankCandidate Then
                            Exit For
                        End If

                        If rowText.Length >= COLS Then
                            If r >= CInt(NumInicio.Value) AndAlso r <= CInt(NumFim.Value) Then
                                Dim slice As String = rowText.Substring(0, COLS)
                                If slice.Trim().Length > 0 Then
                                    sw.WriteLine(slice)
                                End If
                            End If
                        End If
                    Next
                End Using
            End If

            WaitForHost(0)

            ' Update the preview box with the new Buffer.txt content
            If File.Exists(bufferPath) Then
                Try
                    Using sr As New StreamReader(bufferPath, Encoding.UTF8, detectEncodingFromByteOrderMarks:=True)
                        SetRichTextBoxSafe(sr.ReadToEnd())
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Read Buffer", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Try
            End If

            ' If "Only first page" is unchecked, try to advance pages until an empty end-line is found
            If Not CheckBox2.Checked Then
                Dim reachedPageEnd As Boolean = False
                Dim endLine As Integer = CInt(NumFim.Value)

                For r As Integer = 1 To ROWS_SCAN
                    Dim rowText As String = ""
                    EhllapiWrapper.ReadScreen((r * COLS) - 79, COLS, rowText)

                    If r = endLine Then
                        If rowText.Trim().Length = 0 Then
                            reachedPageEnd = True
                            Exit For
                        End If
                    End If
                Next

                WaitForHost(0)

                If Not reachedPageEnd Then
                    ' PF8 (page down)
                    EhllapiWrapper.SendStr("@8")
                    WaitForHost(0)
                    Thread.Sleep(1500)
                Else
                    stopLoop = True
                End If
            Else
                ' "Only first page" is ON: exit after the first extraction
                stopLoop = True
            End If
        End While

        ' Go back PF11 (as in the original code)
        EhllapiWrapper.SendStr("@b")
        WaitForHost(0)
        Thread.Sleep(1500)
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' • BackgroundWorker plumbing
    ' ─────────────────────────────────────────────────────────────────────────────

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        ' Validate selection range before starting
        If NumFim.Value = 1D AndAlso NumInicio.Value = 1D Then
            MessageBox.Show("Define the start and end lines for the table copy.", "Selection Required",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            NumInicio.Focus()
            Exit Sub
        End If

        CaptureProcU(sender, e)
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        If e.Cancelled Then
            SetStatusTextSafe("Extraction stopped")
        Else
            SetStatusTextSafe("Extraction completed")
        End If

        ' Restore start button text if needed
        If Button3.InvokeRequired Then
            Button3.Invoke(Sub() Button3.Text = "Start Capture")
        Else
            Button3.Text = "Start Capture"
        End If
    End Sub

End Class
