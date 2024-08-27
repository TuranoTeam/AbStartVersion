Imports System.Collections.Specialized.BitVector32
Imports System.Data.SqlClient
Imports System.IO
Imports Infragistics.Win.UltraWinGrid
Imports WinSCP

Public Class StartForm
    Dim INIPath As String = Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments).Trim & "\Abacus\abacus.ini"
    Dim ConnectionSting As String = "Data Source=~DATASOURCE~;Initial Catalog=~INITIALCATALOG~;User ID=~USER~;Password=~PASSWORD~;TrustServerCertificate=True"
    Dim SitoEsecuzione As String = "."
    Dim _HostName As String = "ftp.canella.vr.it"
    Dim _UserName As String = "1632033@aruba.it"
    Dim _Password As String = "Arubolina2021"
    Dim VersioneAggiornata As Boolean = False
    Dim AggiornamentoDisponibile As Boolean = False
    Dim Apertura As Boolean = True
    Public Sub New()

        ' La chiamata è richiesta dalla finestra di progettazione.
        InitializeComponent()

        ' Aggiungere le eventuali istruzioni di inizializzazione dopo la chiamata a InitializeComponent().

    End Sub

    Private Sub StartForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Text = "(" & Environ("USERNAME") & ")"

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
                If str.Contains("SITOESECUZIONE=") Then
                    SitoEsecuzione = str.Replace("SITOESECUZIONE=", "")
                End If
            Next
        Catch ex As Exception
            MsgBox("ini non trovato o in formato non valido " & ex.Message)
        End Try

        TtmExeVerTableAdapter.Connection = New SqlClient.SqlConnection(ConnectionSting)
        Me.TtmExeVerTableAdapter.Fill(Me.AbsDataSet.TtmExeVer, SitoEsecuzione)

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
        If ngrdVersionStart.Rows.Count = 1 Then
            LanciaExe(ngrdVersionStart.Rows(0))
        Else
            Apertura = False
        End If
    End Sub
    Dim versione As String = ""
    Dim versioneOL As String = "0"
    Dim AbacusExePath As String = ""
    Private Sub ngrdVersionStart_ClickCellButton(sender As Object, e As CellEventArgs) Handles ngrdVersionStart.ClickCellButton
        LanciaExe(e.Cell.Row)
    End Sub
    Private Sub LanciaExe(Rg As UltraGridRow)
        AbacusExePath = NxaNvl(Rg.Cells("TverExePath").Value).Trim

        If Not VersioneAggiornata Then
            If NxaNvl(Rg.Cells("TverVersionPath").Value).Trim.Length > 0 Then
                If System.IO.File.Exists(Rg.Cells("TverVersionPath").Text.Trim) Then
                    ScaricaVersione()
                    versione = System.IO.File.ReadAllText(Rg.Cells("TverVersionPath").Text.Trim)
                    If CInt(versioneOL) > CInt(versione) Then
                        If Not Apertura Then
                            Me.Text = "downloading new version " & versioneOL & "......"
                            ScaricaAggiornamento()
                            AggiornaVersioneAziende()
                            VersioneAggiornata = True
                            AggiornamentoDisponibile = False
                        Else
                            AggiornamentoDisponibile = True
                            VersioneAggiornata = False
                        End If
                    Else
                            Me.Text = "local version is up to date starting ABACUS......"
                        VersioneAggiornata = True
                    End If
                End If
            End If
        End If

        ' necessario solo in caso di più aziende gestite in DB differenti
        If NxaNvl(Rg.Cells("TverIniPath").Text).Trim <> String.Empty Then
            Try
                System.IO.File.Copy(Rg.Cells("TverIniPath").Text.Trim & "\Abacus.ini", Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments).Trim & "\Abacus\Abacus.ini", True)
            Catch ex As Exception
                MsgBox("cambio azienda non riuscito - " & ex.Message)
            End Try
        End If

        If NxaNvl(versioneOL).Trim <> String.Empty AndAlso NxaNvl(versioneOL).Trim.Length > 4 Then
            If AggiornamentoDisponibile Then
                Me.Text = "Abacus new version " & versioneOL.Substring(0, 1) & "." & versioneOL.Substring(1, 2) & "." & versioneOL.Substring(3) & " (" & Environ("USERNAME") & ")"
            Else
                Me.Text = "Abacus " & versioneOL.Substring(0, 1) & "." & versioneOL.Substring(1, 2) & "." & versioneOL.Substring(3) & " (" & Environ("USERNAME") & ")"
            End If
        End If

        If Apertura And AggiornamentoDisponibile Then
            ' prima apertura con aggiornamento disponibile 
            Apertura = False
        Else
            Apertura = False
            If NxaNvl(AbacusExePath).Trim <> String.Empty Then
                Try
                    Process.Start(AbacusExePath)
                Catch ex As Exception
                    MsgBox("esecuzione " & NxaNvl(Rg.Cells("TverDes").Value).Trim & " non riuscita - " & ex.Message)
                End Try
            Else
                MsgBox("percorso eseguibile vuoto !")
            End If
        End If

    End Sub

    Dim exeFTP As String = ""
    Private Sub AggiornaVersioneAziende()

        Dim queryString As String = "update tbaaziende set DaziSmsID =" & CInt(versioneOL) & ";"
        Dim connection As New SqlClient.SqlConnection(ConnectionSting)
        Dim command As New SqlCommand(queryString, connection)
        connection.Open()
        command.ExecuteNonQuery()
        connection.Close()

    End Sub
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
            .TimeoutInMilliseconds = 50000
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
            MsgBox("file versione non trovato " & ex.Message)
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
            MsgBox("aggiornamento non riuscito  " & ex.Message)
        End Try

    End Sub
    Public Shared Function NxaNvl(ByVal wStringa As Object) As String
        If IsDBNull(wStringa) OrElse wStringa Is Nothing Then
            NxaNvl = ""
        Else
            NxaNvl = wStringa.ToString
        End If

    End Function

    Public Shared Function NxaNvlNum(ByVal wNum As Object) As Object
        If IsDBNull(wNum) OrElse wNum Is Nothing OrElse (TypeOf (wNum) Is String AndAlso CStr(wNum).Trim.Length = 0) Then
            NxaNvlNum = 0
        Else
            NxaNvlNum = wNum
        End If
    End Function

    Private Sub StartForm_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        'If ngrdVersionStart.Rows.Count = 1 Then
        '    LanciaExe(ngrdVersionStart.Rows(0))
        'End If
    End Sub
End Class
