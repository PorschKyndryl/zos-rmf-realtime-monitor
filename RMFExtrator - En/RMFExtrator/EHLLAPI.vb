Imports System.Runtime.InteropServices
Imports System.Text

Namespace EHLLAPI
    Public Class EhllapiFunc
        <DllImport("PCSHLL32.dll")>
        Public Shared Function hllapi(<Out> ByRef Func As UInteger, ByVal Data As StringBuilder, <Out> ByRef Length As UInteger, <Out> ByRef RetC As UInteger) As UInteger
        End Function
    End Class

    Public Class EhllapiWrapper

        Const HA_CONNECT_PS As UInteger = 1 ' 000 Connect PS
        Const HA_DISCONNECT_PS As UInteger = 2 ' 000 Disconnect PS
        Const HA_SENDKEY As UInteger = 3 ' 000 Sendkey function
        Const HA_WAIT As UInteger = 4 ' 000 Wait function
        Const HA_COPY_PS As UInteger = 5 ' 000 Copy PS function
        Const HA_SEARCH_PS As UInteger = 6 ' 000 Search PS function
        Const HA_QUERY_CURSOR_LOC As UInteger = 7 ' 000 Query Cursor
        Const HA_COPY_PS_TO_STR As UInteger = 8 ' 000 Copy PS to String
        Const HA_SET_SESSION_PARMS As UInteger = 9 ' 000 Set Session
        Const HA_QUERY_SESSIONS As UInteger = 10 ' 000 Query Sessions
        Const HA_RESERVE As UInteger = 11 ' 000 Reserve function
        Const HA_RELEASE As UInteger = 12 ' 000 Release function
        Const HA_COPY_OIA As UInteger = 13 ' 000 Copy OIA function
        Const HA_QUERY_FIELD_ATTR As UInteger = 14 ' 000 Query Field
        Const HA_COPY_STR_TO_PS As UInteger = 15 ' 000 Copy string to PS
        Const HA_STORAGE_MGR As UInteger = 17 ' 000 Storage Manager
        Const HA_PAUSE As UInteger = 18 ' 000 Pause function
        Const HA_QUERY_SYSTEM As UInteger = 20 ' 000 Query System
        Const HA_RESET_SYSTEM As UInteger = 21 ' 000 Reset System
        Const HA_QUERY_SESSION_STATUS As UInteger = 22 ' 000 Query Session
        Const HA_START_HOST_NOTIFY As UInteger = 23 ' 000 Start Host
        Const HA_QUERY_HOST_UPDATE As UInteger = 24 ' 000 Query Host Update
        Const HA_STOP_HOST_NOTIFY As UInteger = 25 ' 000 Stop Host
        Const HA_SEARCH_FIELD As UInteger = 30 ' 000 Search Field
        Const HA_FIND_FIELD_POS As UInteger = 31 ' 000 Find Field
        Const HA_FIND_FIELD_LEN As UInteger = 32 ' 000 Find Field Length
        Const HA_COPY_STR_TO_FIELD As UInteger = 33 ' 000 Copy String to
        Const HA_COPY_FIELD_TO_STR As UInteger = 34 ' 000 Copy Field to
        Const HA_SET_CURSOR As UInteger = 40 ' 000 Set Cursor
        Const HA_START_CLOSE_INTERCEPT As UInteger = 41 ' 000 Start Close Intercept
        Const HA_QUERY_CLOSE_INTERCEPT As UInteger = 42 ' 000 Query Close Intercept
        Const HA_STOP_CLOSE_INTERCEPT As UInteger = 43 ' 000 Stop Close Intercept
        Const HA_START_KEY_INTERCEPT As UInteger = 50 ' 000 Start Keystroke
        Const HA_GET_KEY As UInteger = 51 ' 000 Get Key function
        Const HA_POST_INTERCEPT_STATUS As UInteger = 52 ' 000 Post Intercept
        Const HA_STOP_KEY_INTERCEPT As UInteger = 53 ' 000 Stop Keystroke
        Const HA_LOCK_PS As UInteger = 60 ' 000 Lock Presentation
        Const HA_LOCK_PMSVC As UInteger = 61 ' 000 Lock PM Window
        Const HA_SEND_FILE As UInteger = 90 ' 000 Send File function
        Const HA_RECEIVE_FILE As UInteger = 91 ' 000 Receive file
        Const HA_CONVERT_POS_ROW_COL As UInteger = 99 ' 000 Convert Position
        Const HA_CONNECT_PM_SRVCS As UInteger = 101 ' 000 Connect For
        Const HA_DISCONNECT_PM_SRVCS As UInteger = 102 ' 000 Disconnect From
        Const HA_QUERY_WINDOW_COORDS As UInteger = 103 ' 000 Query Presentation
        Const HA_PM_WINDOW_STATUS As UInteger = 104 ' 000 PM Window Status
        Const HA_CHANGE_SWITCH_NAME As UInteger = 105 ' 000 Change Switch List
        Const HA_CHANGE_WINDOW_NAME As UInteger = 106 ' 000 Change PS Window
        Const HA_START_PLAYING_MACRO As UInteger = 110 ' 000 Start playing macro
        Const HA_START_STRUCTURED_FLD As UInteger = 120 ' 000 Start Structured
        Const HA_STOP_STRUCTURED_FLD As UInteger = 121 ' 000 Stop Structured
        Const HA_QUERY_BUFFER_SIZE As UInteger = 122 ' 000 Query Communications
        Const HA_ALLOCATE_COMMO_BUFF As UInteger = 123 ' 000 Allocate
        Const HA_FREE_COMMO_BUFF As UInteger = 124 ' 000 Free Communications
        Const HA_GET_ASYNC_COMPLETION As UInteger = 125 ' 000 Get Asynchronous
        Const HA_READ_STRUCTURED_FLD As UInteger = 126 ' 000 Read Structured Field
        Const HA_WRITE_STRUCTURED_FLD As UInteger = 127 ' 000 Write Structured


        '********************************************************************/ 

        '******************* EHLLAPI RETURN CODES***************************/ 

        '********************************************************************/ 
        Const HARC_SUCCESS As UInteger = 0 ' 000 Good return code.
        Const HARC99_INVALID_INP As UInteger = 0 ' 000 Incorrect input
        Const HARC_INVALID_PS As UInteger = 1 ' 000 Invalid PS, Not
        Const HARC_BAD_PARM As UInteger = 2 ' 000 Bad parameter, or
        Const HARC_BUSY As UInteger = 4 ' 000 PS is busy return
        Const HARC_LOCKED As UInteger = 5 ' 000 PS is LOCKed, or
        Const HARC_TRUNCATION As UInteger = 6 ' 000 Truncation
        Const HARC_INVALID_PS_POS As UInteger = 7 ' 000 Invalid PS
        Const HARC_NO_PRIOR_START As UInteger = 8 ' 000 No prior start
        Const HARC_SYSTEM_ERROR As UInteger = 9 ' 000 A system error
        Const HARC_UNSUPPORTED As UInteger = 10 ' 000 Invalid or
        Const HARC_UNAVAILABLE As UInteger = 11 ' 000 Resource is
        Const HARC_SESSION_STOPPED As UInteger = 12 ' 000 Session has
        Const HARC_BAD_MNEMONIC As UInteger = 20 ' 000 Illegal mnemonic
        Const HARC_OIA_UPDATE As UInteger = 21 ' 000 A OIA update
        Const HARC_PS_UPDATE As UInteger = 22 ' 000 A PS update
        Const HARC_PS_AND_OIA_UPDATE As UInteger = 23 ' A PS and OIA update
        Const HARC_STR_NOT_FOUND_UNFM_PS As UInteger = 24 ' 000 String not found,
        Const HARC_NO_KEYS_AVAIL As UInteger = 25 ' 000 No keys available
        Const HARC_HOST_UPDATE As UInteger = 26 ' 000 A HOST update
        Const HARC_FIELD_LEN_ZERO As UInteger = 28 ' 000 Field length = 0
        Const HARC_QUEUE_OVERFLOW As UInteger = 31 ' 000 Keystroke queue
        Const HARC_ANOTHER_CONNECTION As UInteger = 32 ' 000 Successful. Another
        Const HARC_INBOUND_CANCELLED As UInteger = 34 ' 000 Inbound structured
        Const HARC_OUTBOUND_CANCELLED As UInteger = 35 ' 000 Outbound structured
        Const HARC_CONTACT_LOST As UInteger = 36 ' 000 Contact with the
        Const HARC_INBOUND_DISABLED As UInteger = 37 ' 000 Host structured field
        Const HARC_FUNCTION_INCOMPLETE As UInteger = 38 ' 000 Requested Asynchronous
        Const HARC_DDM_ALREADY_EXISTS As UInteger = 39 ' 000 Request for DDM
        Const HARC_ASYNC_REQUESTS_OUT As UInteger = 40 ' 000 Disconnect successful.
        Const HARC_MEMORY_IN_USE As UInteger = 41 ' 000 Memory cannot be freed
        Const HARC_NO_MATCH As UInteger = 42 ' 000 No pending
        Const HARC_OPTION_INVALID As UInteger = 43 ' 000 Option requested is
        Const HARC99_INVALID_PS As UInteger = 9998 ' 000 An invalid PS id
        Const HARC99_INVALID_CONV_OPT As UInteger = 9999 ' 000 Invalid convert


        Public Shared Function Connect(ByVal sessionID As String) As UInteger
            Dim Data As StringBuilder = New StringBuilder(4)
            Data.Append(sessionID)
            Dim rc As UInteger = 0
            Dim f = HA_CONNECT_PS
            Dim l As UInteger = 4
            Return EhllapiFunc.hllapi(f, Data, l, rc)
        End Function

        Public Shared Function Disconnect(ByVal sessionID As String) As UInteger
            Dim Data As StringBuilder = New StringBuilder(4)
            Data.Append(sessionID)
            Dim rc As UInteger = 0
            Dim f = HA_DISCONNECT_PS
            Dim l As UInteger = 4
            Return EhllapiFunc.hllapi(f, Data, l, rc)
        End Function

        Public Shared Function SetCursorPos(ByVal p As Integer) As UInteger
            Dim Data As StringBuilder = New StringBuilder(0)
            Dim rc As UInteger = p
            Dim f = HA_SET_CURSOR
            Dim l As UInteger = 0
            Return EhllapiFunc.hllapi(f, Data, l, rc)
        End Function

        Public Shared Function GetCursorPos(<Out> ByRef p As Integer) As UInteger
            Dim Data As StringBuilder = New StringBuilder(0)
            Dim rc As UInteger = 0
            Dim f = HA_QUERY_CURSOR_LOC
            Dim l As UInteger = 0 'return position
            Dim r = EhllapiFunc.hllapi(f, Data, l, rc)
            p = CInt(l)
            Return r
        End Function

        Public Shared Function SendStr(ByVal cmd As String) As UInteger
            Dim Data As StringBuilder = New StringBuilder(cmd.Length)
            Data.Append(cmd)
            Dim rc As UInteger = 0
            Dim f = HA_SENDKEY
            Dim l As UInteger = cmd.Length
            Return EhllapiFunc.hllapi(f, Data, l, rc)
        End Function

        Public Shared Function ReadScreen(ByVal position As Integer, ByVal len As Integer, <Out> ByRef txt As String) As UInteger
            Dim Data As StringBuilder = New StringBuilder(3000)
            Dim rc As UInteger = position
            Dim f = HA_COPY_PS_TO_STR
            Dim l As UInteger = len
            Dim r = EhllapiFunc.hllapi(f, Data, l, rc)
            txt = Data.ToString()
            Return r
        End Function

        Public Shared Function Wait() As UInteger
            Dim Data As StringBuilder = New StringBuilder(0)
            Dim rc As UInteger = 0
            Dim f = HA_WAIT
            Dim l As UInteger = 0
            Dim r = EhllapiFunc.hllapi(f, Data, l, rc)
            Return r
        End Function


    End Class
End Namespace

