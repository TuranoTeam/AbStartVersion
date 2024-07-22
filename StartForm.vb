Imports Infragistics.Win.UltraWinGrid

Public Class StartForm
    Dim INIPath As String = Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments).Trim & "\Abacus\abacus.ini"
    Dim ConnectionSting As String = "Data Source=~DATASOURCE~;Initial Catalog=~INITIALCATALOG~;User ID=~USER~;Password=~PASSWORD~;TrustServerCertificate=True"
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

    Private Sub ngrdVersionStart_ClickCellButton(sender As Object, e As CellEventArgs) Handles ngrdVersionStart.ClickCellButton
        System.IO.File.Copy(e.Cell.Row.Cells("TverIniPath").Text.Trim & "\Abacus.ini", Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments).Trim & "\Abacus\Abacus.ini", True)

        Process.Start(e.Cell.Row.Cells("TverExePath").Text)
    End Sub

    Private Sub btnChiusura_Click(sender As Object, e As EventArgs) Handles btnChiusura.Click
        Me.Close()
    End Sub
End Class
