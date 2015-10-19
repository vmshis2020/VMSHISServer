Imports System.Management
Imports System.IO
Imports System.Windows.Forms

Public Class MHardKey
    Private AppType As String
    Public GenKey As String
    Public RegKey As String
    Public pwd As String = ""
    Public loopNum As Integer = 1
    Delegate Sub _CheckKing()
    Public Sub New(ByVal _AppType As String, ByVal _loopNum As Integer, ByVal ShowCheckingKey As Boolean)
        loopNum = _loopNum
        Application.DoEvents()
        Me.AppType = _AppType
        GenKey = Security.HardWare.Value(_AppType)
        RegKey = Security.HardWare.GetKey(GenKey)
    End Sub


End Class
