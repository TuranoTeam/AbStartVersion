Imports System.Collections.Specialized.BitVector32
Imports System.IO
Imports Infragistics.Win.UltraWinGrid
Imports WinSCP

Public Class StartForm
    Dim INIPath As String = Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments).Trim & "\Abacus\abacus.ini"
    Dim ConnectionSting As String = "Data Source=~DATASOURCE~;Initial Catalog=~INITIALCATALOG~;User ID=~USER~;Password=~PASSWORD~;TrustServerCertificate=True"
    Dim _HostName As String = "ftp.canella.vr.it"
    Dim _UserName As String = "1632033@aruba.it"
    Dim _Password As String = "Arubolina2021"
    Dim VersioneAggiornata As Boolean = False
    Public Sub New()

        ' La chiamata è richiesta dalla finestra di progettazione.
        InitializeComponent()

        ' Aggiungere le eventuali istruzioni di inizializzazione dopo la chiamata a InitializeComponent().

    End Sub

    Private Sub StartForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'My.Settings.C5B05_StrutturaCommesse
        'Me.ControlBox = False
        Me.Text = String.Empty

        Dim param As String()
        Try
            param = IO.File.ReadAllLines(INIPath)

            For Each str As String In param
                If str.Contains("PASSWORD=") Then
                    ConnectionSting = ConnectionSting.Replace("~PASSWORD~", str.Replace("PASSWORD=", ""))
                End If
                If str.Contains("USER=") Then
                    ConnectionSting = ConnectionSting.Replace("~USER~", str.Replace("USER=", ""))
                End If
                If str.Contains("DATASOURCE=") Then
                    ConnectionSting = ConnectionSting.Replace("~DATASOURCE~", str.Replace("DATASOURCE=", ""))
                End If
                If str.Contains("INITIALCATALOG=") Then
                    ConnectionSting = ConnectionSting.Replace("~INITIALCATALOG~", str.Replace("INITIALCATALOG=", ""))
                End If
            Next
        Catch ex As Exception
            MsgBox("ini non trovato o in formato non valido")
        End Try

        Me.Text = ""
        TtmExeVerTableAdapter.Connection = New SqlClient.SqlConnection(ConnectionSting)
        Me.TtmExeVerTableAdapter.Fill(Me.AbsDataSet.TtmExeVer)
        '   MsgBox(Application.StartupPath)
        If System.IO.File.Exists(Application.StartupPath.Replace("bin\Debug", "Resources") & "\Abacus.isl") Then
            Infragistics.Win.AppStyling.StyleManager.Load(Application.StartupPath.Replace("bin\Debug", "Resources") & "\Abacus.isl")
        Else
            Infragistics.Win.AppStyling.StyleManager.Load(Application.StartupPath & "\External\Abacus.isl")
        End If
        ngrdVersionStart.Text = ""
        For Each col As Infragistics.Win.UltraWinGrid.UltraGridColumn In ngrdVersionStart.DisplayLayout.Bands(0).Columns
            If col.Key.ToUpper = "TVERDES" Then
                col.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button
                col.Width = 400
                col.Header.Caption = "scelta versione / azienda"
                col.Header.Appearance.TextHAlign = Infragistics.Win.HAlign.Center
            Else
                col.Hidden = True
            End If
        Next

    End Sub
    Dim versione As String = ""
    Dim versioneOL As String = "0"
    Dim AbacusExePath As String = ""
    Private Sub ngrdVersionStart_ClickCellButton(sender As Object, e As CellEventArgs) Handles ngrdVersionStart.ClickCellButton

        AbacusExePath = e.Cell.Row.Cells("TverExePath").Text.Trim

        If Not VersioneAggiornata Then
            If e.Cell.Row.Cells("TverVersionPath").Text.Trim.Length > 0 Then
                If System.IO.File.Exists(e.Cell.Row.Cells("TverVersionPath").Text.Trim) Then
                    ScaricaVersione()
                    versione = System.IO.File.ReadAllText(e.Cell.Row.Cells("TverVersionPath").Text.Trim)
                    If CInt(versioneOL) > CInt(versione) Then
                        Me.Text = "downloading new version " & versioneOL & "......"
                        ScaricaAggiornamento()
                        Me.Text = ""
                    Else
                        Me.Text = "local version is up to date starting ABACUS......"
                        VersioneAggiornata = True
                    End If
                End If
            End If
        End If

        System.IO.File.Copy(e.Cell.Row.Cells("TverIniPath").Text.Trim & "\Abacus.ini", Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments).Trim & "\Abacus\Abacus.ini", True)

        Process.Start(AbacusExePath)

        Me.Text = "Abacus " & versioneOL.Substring(0, 1) & "." & versioneOL.Substring(1, 2) & "." & versioneOL.Substring(3)
    End Sub

    Dim exeFTP As String = ""
    Private Sub ScaricaVersione()

        If IO.File.Exists(Application.StartupPath & "\External\WinSCP.exe") Then
            exeFTP = Application.StartupPath & "\External\WinSCP.exe"
        Else
            If IO.File.Exists(Application.StartupPath.Replace("bin\Debug", "Resources") & "\WinSCP.exe") Then
                exeFTP = Application.StartupPath.Replace("bin\Debug", "Resources") & "\WinSCP.exe"
            Else
                versioneOL = 0
                Exit Sub
            End If
        End If
        Dim SessionOptions As SessionOptions = New SessionOptions
        With SessionOptions
            .Protocol = Protocol.Ftp
            .HostName = _HostName
            .UserName = _UserName
            .Password = _Password
        End With
        Try
            Using session As Session = New Session()
                session.ExecutablePath = exeFTP

                session.Open(SessionOptions)

                Dim strFileStream As Stream = session.GetFile("/canella.vr.it/Deposito/AB_NEW/Versione.txt")
                Dim srFile As StreamReader = New StreamReader(strFileStream)
                versioneOL = srFile.ReadToEnd()

            End Using

        Catch ex As Exception
            MsgBox("file versione non trovato")
        End Try

    End Sub
    Dim AbacusExeFolder As String = ""
    Private Sub ScaricaAggiornamento()

        AbacusExeFolder = AbacusExePath.Substring(0, AbacusExePath.LastIndexOf("\"))
        Dim SessionOptions As SessionOptions = New SessionOptions
        With SessionOptions
            .Protocol = Protocol.Ftp
            .HostName = _HostName
            .UserName = _UserName
            .Password = _Password
        End With
        Try
            Using session As Session = New Session()
                session.ExecutablePath = exeFTP

                session.Open(SessionOptions)

                session.GetFiles("/canella.vr.it/Deposito/AB_NEW/*", AbacusExeFolder & "\*").Check()
            End Using
        Catch ex As Exception
            MsgBox("aggiornamento non riuscito")
        End Try


    End Sub
End Class
