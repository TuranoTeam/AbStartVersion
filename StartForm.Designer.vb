﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class StartForm
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla mediante l'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("TtmExeVer", -1)
        Dim UltraGridColumn28 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("TverCod_Id")
        Dim UltraGridColumn31 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("TverAttivo")
        Dim UltraGridColumn29 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("TverDes")
        Dim UltraGridColumn30 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("TverIniPath")
        Dim UltraGridColumn32 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("TverExePath")
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("TverVersionPath")
        Dim UltraGridColumn33 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("TverInsertDate")
        Dim UltraGridColumn34 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("TverInsertUser")
        Dim UltraGridColumn35 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("TverUpdateDate")
        Dim UltraGridColumn36 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("TverUpdateUser")
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StartForm))
        Me.ngrdVersionStart = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.TtmExeVerBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.AbsDataSet = New AbStart.AbsDataSet()
        Me.btnChiusura = New Infragistics.Win.Misc.UltraButton()
        Me.UltraPanel1 = New Infragistics.Win.Misc.UltraPanel()
        Me.UltraPanel2 = New Infragistics.Win.Misc.UltraPanel()
        Me.UltraGroupBox1 = New Infragistics.Win.Misc.UltraGroupBox()
        Me.TtmExeVerTableAdapter = New AbStart.AbsDataSetTableAdapters.TtmExeVerTableAdapter()
        CType(Me.ngrdVersionStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TtmExeVerBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AbsDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraPanel1.ClientArea.SuspendLayout()
        Me.UltraPanel1.SuspendLayout()
        Me.UltraPanel2.ClientArea.SuspendLayout()
        Me.UltraPanel2.SuspendLayout()
        CType(Me.UltraGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraGroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ngrdVersionStart
        '
        Me.ngrdVersionStart.DataSource = Me.TtmExeVerBindingSource
        UltraGridColumn28.Header.VisiblePosition = 0
        UltraGridColumn31.Header.VisiblePosition = 3
        UltraGridColumn29.Header.VisiblePosition = 1
        UltraGridColumn30.Header.VisiblePosition = 2
        UltraGridColumn32.Header.VisiblePosition = 4
        UltraGridColumn1.Header.VisiblePosition = 5
        UltraGridColumn33.Header.VisiblePosition = 6
        UltraGridColumn34.Header.VisiblePosition = 7
        UltraGridColumn35.Header.VisiblePosition = 8
        UltraGridColumn36.Header.VisiblePosition = 9
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn28, UltraGridColumn31, UltraGridColumn29, UltraGridColumn30, UltraGridColumn32, UltraGridColumn1, UltraGridColumn33, UltraGridColumn34, UltraGridColumn35, UltraGridColumn36})
        Me.ngrdVersionStart.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.ngrdVersionStart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ngrdVersionStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ngrdVersionStart.Location = New System.Drawing.Point(0, 0)
        Me.ngrdVersionStart.Name = "ngrdVersionStart"
        Me.ngrdVersionStart.Size = New System.Drawing.Size(442, 333)
        Me.ngrdVersionStart.TabIndex = 0
        Me.ngrdVersionStart.Text = "ngrdVersionStart"
        '
        'TtmExeVerBindingSource
        '
        Me.TtmExeVerBindingSource.DataMember = "TtmExeVer"
        Me.TtmExeVerBindingSource.DataSource = Me.AbsDataSet
        '
        'AbsDataSet
        '
        Me.AbsDataSet.DataSetName = "AbsDataSet"
        Me.AbsDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'btnChiusura
        '
        Me.btnChiusura.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat
        Me.btnChiusura.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnChiusura.Location = New System.Drawing.Point(0, 0)
        Me.btnChiusura.Name = "btnChiusura"
        Me.btnChiusura.Size = New System.Drawing.Size(442, 50)
        Me.btnChiusura.TabIndex = 1
        Me.btnChiusura.Text = "chiusura"
        Me.btnChiusura.UseFlatMode = Infragistics.Win.DefaultableBoolean.[True]
        '
        'UltraPanel1
        '
        '
        'UltraPanel1.ClientArea
        '
        Me.UltraPanel1.ClientArea.Controls.Add(Me.btnChiusura)
        Me.UltraPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UltraPanel1.Location = New System.Drawing.Point(1, 299)
        Me.UltraPanel1.Name = "UltraPanel1"
        Me.UltraPanel1.Size = New System.Drawing.Size(442, 50)
        Me.UltraPanel1.TabIndex = 2
        Me.UltraPanel1.Visible = False
        '
        'UltraPanel2
        '
        '
        'UltraPanel2.ClientArea
        '
        Me.UltraPanel2.ClientArea.Controls.Add(Me.ngrdVersionStart)
        Me.UltraPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraPanel2.Location = New System.Drawing.Point(1, 16)
        Me.UltraPanel2.Name = "UltraPanel2"
        Me.UltraPanel2.Size = New System.Drawing.Size(442, 333)
        Me.UltraPanel2.TabIndex = 3
        '
        'UltraGroupBox1
        '
        Me.UltraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid
        Me.UltraGroupBox1.Controls.Add(Me.UltraPanel1)
        Me.UltraGroupBox1.Controls.Add(Me.UltraPanel2)
        Me.UltraGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.UltraGroupBox1.Name = "UltraGroupBox1"
        Me.UltraGroupBox1.Size = New System.Drawing.Size(444, 350)
        Me.UltraGroupBox1.TabIndex = 4
        Me.UltraGroupBox1.Text = "versioni / aziende"
        '
        'TtmExeVerTableAdapter
        '
        Me.TtmExeVerTableAdapter.ClearBeforeFill = True
        '
        'StartForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(444, 350)
        Me.Controls.Add(Me.UltraGroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "StartForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        CType(Me.ngrdVersionStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TtmExeVerBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AbsDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraPanel1.ClientArea.ResumeLayout(False)
        Me.UltraPanel1.ResumeLayout(False)
        Me.UltraPanel2.ClientArea.ResumeLayout(False)
        Me.UltraPanel2.ResumeLayout(False)
        CType(Me.UltraGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraGroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ngrdVersionStart As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents AbsDataSet As AbsDataSet
    Friend WithEvents TtmExeVerBindingSource As BindingSource
    Friend WithEvents TtmExeVerTableAdapter As AbsDataSetTableAdapters.TtmExeVerTableAdapter
    Friend WithEvents btnChiusura As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraPanel1 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents UltraPanel2 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents UltraGroupBox1 As Infragistics.Win.Misc.UltraGroupBox
End Class
