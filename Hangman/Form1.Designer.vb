<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class picBackground
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(picBackground))
        GameTimer = New Timer(components)
        EnemyMino = New PictureBox()
        PictureBox3 = New PictureBox()
        LexWorm = New PictureBox()
        pnlGrid = New Panel()
        tblGrid = New TableLayoutPanel()
        btnGrid15 = New Button()
        btnGrid14 = New Button()
        btnGrid13 = New Button()
        btnGrid12 = New Button()
        btnGrid11 = New Button()
        btnGrid10 = New Button()
        btnGrid09 = New Button()
        btnGrid08 = New Button()
        btnGrid07 = New Button()
        btnGrid06 = New Button()
        btnGrid05 = New Button()
        btnGrid04 = New Button()
        btnGrid03 = New Button()
        btnGrid02 = New Button()
        btnGrid01 = New Button()
        btnGrid00 = New Button()
        btnAttack = New Button()
        btnScramble = New Button()
        PlayerHPBar = New ProgressBar()
        EnemyHPBar = New ProgressBar()
        lblWordDisplay = New Label()
        DamageFlickerTimer = New Timer(components)
        lblPlayerName = New Label()
        lblEnemyName = New Label()
        lblscore = New Label()
        Potion = New PictureBox()
        Shield = New PictureBox()
        Label1 = New Label()
        Label3 = New Label()
        lblGold = New Label()
        btnInstaKill = New Button()
        CType(EnemyMino, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox3, ComponentModel.ISupportInitialize).BeginInit()
        CType(LexWorm, ComponentModel.ISupportInitialize).BeginInit()
        pnlGrid.SuspendLayout()
        tblGrid.SuspendLayout()
        CType(Potion, ComponentModel.ISupportInitialize).BeginInit()
        CType(Shield, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' GameTimer
        ' 
        GameTimer.Interval = 16
        ' 
        ' EnemyMino
        ' 
        EnemyMino.BackColor = Color.Transparent
        EnemyMino.Image = CType(resources.GetObject("EnemyMino.Image"), Image)
        EnemyMino.Location = New Point(768, 121)
        EnemyMino.Name = "EnemyMino"
        EnemyMino.Size = New Size(274, 284)
        EnemyMino.SizeMode = PictureBoxSizeMode.Zoom
        EnemyMino.TabIndex = 4
        EnemyMino.TabStop = False
        ' 
        ' PictureBox3
        ' 
        PictureBox3.BackgroundImageLayout = ImageLayout.Stretch
        PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), Image)
        PictureBox3.Location = New Point(-15, 400)
        PictureBox3.Name = "PictureBox3"
        PictureBox3.Size = New Size(1207, 377)
        PictureBox3.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox3.TabIndex = 5
        PictureBox3.TabStop = False
        ' 
        ' LexWorm
        ' 
        LexWorm.BackColor = Color.Transparent
        LexWorm.Image = CType(resources.GetObject("LexWorm.Image"), Image)
        LexWorm.Location = New Point(158, 157)
        LexWorm.Name = "LexWorm"
        LexWorm.Size = New Size(209, 272)
        LexWorm.SizeMode = PictureBoxSizeMode.Zoom
        LexWorm.TabIndex = 3
        LexWorm.TabStop = False
        ' 
        ' pnlGrid
        ' 
        pnlGrid.BackColor = Color.Transparent
        pnlGrid.Controls.Add(tblGrid)
        pnlGrid.Location = New Point(395, 418)
        pnlGrid.Name = "pnlGrid"
        pnlGrid.Size = New Size(320, 320)
        pnlGrid.TabIndex = 7
        ' 
        ' tblGrid
        ' 
        tblGrid.BackColor = Color.Silver
        tblGrid.CausesValidation = False
        tblGrid.ColumnCount = 4
        tblGrid.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25F))
        tblGrid.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25F))
        tblGrid.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25F))
        tblGrid.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25F))
        tblGrid.Controls.Add(btnGrid15, 3, 3)
        tblGrid.Controls.Add(btnGrid14, 2, 3)
        tblGrid.Controls.Add(btnGrid13, 1, 3)
        tblGrid.Controls.Add(btnGrid12, 0, 3)
        tblGrid.Controls.Add(btnGrid11, 3, 2)
        tblGrid.Controls.Add(btnGrid10, 2, 2)
        tblGrid.Controls.Add(btnGrid09, 1, 2)
        tblGrid.Controls.Add(btnGrid08, 0, 2)
        tblGrid.Controls.Add(btnGrid07, 3, 1)
        tblGrid.Controls.Add(btnGrid06, 2, 1)
        tblGrid.Controls.Add(btnGrid05, 1, 1)
        tblGrid.Controls.Add(btnGrid04, 0, 1)
        tblGrid.Controls.Add(btnGrid03, 3, 0)
        tblGrid.Controls.Add(btnGrid02, 2, 0)
        tblGrid.Controls.Add(btnGrid01, 1, 0)
        tblGrid.Controls.Add(btnGrid00, 0, 0)
        tblGrid.Location = New Point(3, 0)
        tblGrid.Name = "tblGrid"
        tblGrid.RowCount = 4
        tblGrid.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        tblGrid.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        tblGrid.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        tblGrid.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        tblGrid.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        tblGrid.Size = New Size(320, 320)
        tblGrid.TabIndex = 8
        ' 
        ' btnGrid15
        ' 
        btnGrid15.Location = New Point(243, 243)
        btnGrid15.Name = "btnGrid15"
        btnGrid15.Size = New Size(74, 74)
        btnGrid15.TabIndex = 23
        btnGrid15.Text = "Button16"
        btnGrid15.UseVisualStyleBackColor = True
        ' 
        ' btnGrid14
        ' 
        btnGrid14.Location = New Point(163, 243)
        btnGrid14.Name = "btnGrid14"
        btnGrid14.Size = New Size(74, 74)
        btnGrid14.TabIndex = 22
        btnGrid14.Text = "Button15"
        btnGrid14.UseVisualStyleBackColor = True
        ' 
        ' btnGrid13
        ' 
        btnGrid13.Location = New Point(83, 243)
        btnGrid13.Name = "btnGrid13"
        btnGrid13.Size = New Size(74, 74)
        btnGrid13.TabIndex = 21
        btnGrid13.Text = "Button14"
        btnGrid13.UseVisualStyleBackColor = True
        ' 
        ' btnGrid12
        ' 
        btnGrid12.Location = New Point(3, 243)
        btnGrid12.Name = "btnGrid12"
        btnGrid12.Size = New Size(74, 74)
        btnGrid12.TabIndex = 20
        btnGrid12.Text = "Button13"
        btnGrid12.UseVisualStyleBackColor = True
        ' 
        ' btnGrid11
        ' 
        btnGrid11.Location = New Point(243, 163)
        btnGrid11.Name = "btnGrid11"
        btnGrid11.Size = New Size(74, 74)
        btnGrid11.TabIndex = 19
        btnGrid11.Text = "Button12"
        btnGrid11.UseVisualStyleBackColor = True
        ' 
        ' btnGrid10
        ' 
        btnGrid10.Location = New Point(163, 163)
        btnGrid10.Name = "btnGrid10"
        btnGrid10.Size = New Size(74, 74)
        btnGrid10.TabIndex = 18
        btnGrid10.Text = "Button11"
        btnGrid10.UseVisualStyleBackColor = True
        ' 
        ' btnGrid09
        ' 
        btnGrid09.Location = New Point(83, 163)
        btnGrid09.Name = "btnGrid09"
        btnGrid09.Size = New Size(74, 74)
        btnGrid09.TabIndex = 17
        btnGrid09.Text = "Button10"
        btnGrid09.UseVisualStyleBackColor = True
        ' 
        ' btnGrid08
        ' 
        btnGrid08.Location = New Point(3, 163)
        btnGrid08.Name = "btnGrid08"
        btnGrid08.Size = New Size(74, 74)
        btnGrid08.TabIndex = 16
        btnGrid08.Text = "Button9"
        btnGrid08.UseVisualStyleBackColor = True
        ' 
        ' btnGrid07
        ' 
        btnGrid07.Location = New Point(243, 83)
        btnGrid07.Name = "btnGrid07"
        btnGrid07.Size = New Size(74, 74)
        btnGrid07.TabIndex = 15
        btnGrid07.Text = "Button8"
        btnGrid07.UseVisualStyleBackColor = True
        ' 
        ' btnGrid06
        ' 
        btnGrid06.Location = New Point(163, 83)
        btnGrid06.Name = "btnGrid06"
        btnGrid06.Size = New Size(74, 74)
        btnGrid06.TabIndex = 14
        btnGrid06.Text = "Button7"
        btnGrid06.UseVisualStyleBackColor = True
        ' 
        ' btnGrid05
        ' 
        btnGrid05.Location = New Point(83, 83)
        btnGrid05.Name = "btnGrid05"
        btnGrid05.Size = New Size(74, 74)
        btnGrid05.TabIndex = 13
        btnGrid05.Text = "Button6"
        btnGrid05.UseVisualStyleBackColor = True
        ' 
        ' btnGrid04
        ' 
        btnGrid04.Location = New Point(3, 83)
        btnGrid04.Name = "btnGrid04"
        btnGrid04.Size = New Size(74, 74)
        btnGrid04.TabIndex = 12
        btnGrid04.Text = "Button5"
        btnGrid04.UseVisualStyleBackColor = True
        ' 
        ' btnGrid03
        ' 
        btnGrid03.Location = New Point(243, 3)
        btnGrid03.Name = "btnGrid03"
        btnGrid03.Size = New Size(74, 74)
        btnGrid03.TabIndex = 11
        btnGrid03.Text = "Button4"
        btnGrid03.UseVisualStyleBackColor = True
        ' 
        ' btnGrid02
        ' 
        btnGrid02.Location = New Point(163, 3)
        btnGrid02.Name = "btnGrid02"
        btnGrid02.Size = New Size(74, 74)
        btnGrid02.TabIndex = 10
        btnGrid02.Text = "Button3"
        btnGrid02.UseVisualStyleBackColor = True
        ' 
        ' btnGrid01
        ' 
        btnGrid01.Location = New Point(83, 3)
        btnGrid01.Name = "btnGrid01"
        btnGrid01.Size = New Size(74, 74)
        btnGrid01.TabIndex = 9
        btnGrid01.Text = "Button2"
        btnGrid01.UseVisualStyleBackColor = True
        ' 
        ' btnGrid00
        ' 
        btnGrid00.Location = New Point(3, 3)
        btnGrid00.Name = "btnGrid00"
        btnGrid00.Size = New Size(74, 74)
        btnGrid00.TabIndex = 8
        btnGrid00.Text = "Button1"
        btnGrid00.UseVisualStyleBackColor = True
        ' 
        ' btnAttack
        ' 
        btnAttack.BackColor = Color.Lime
        btnAttack.Font = New Font("Algerian", 20.25F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        btnAttack.Location = New Point(740, 501)
        btnAttack.Name = "btnAttack"
        btnAttack.Size = New Size(174, 50)
        btnAttack.TabIndex = 8
        btnAttack.Text = "Attack"
        btnAttack.UseVisualStyleBackColor = False
        ' 
        ' btnScramble
        ' 
        btnScramble.BackColor = Color.FromArgb(CByte(192), CByte(0), CByte(0))
        btnScramble.Font = New Font("Algerian", 20.25F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        btnScramble.Location = New Point(740, 581)
        btnScramble.Name = "btnScramble"
        btnScramble.Size = New Size(174, 50)
        btnScramble.TabIndex = 9
        btnScramble.Tag = "Scramble"
        btnScramble.Text = "Scramble"
        btnScramble.UseVisualStyleBackColor = False
        ' 
        ' PlayerHPBar
        ' 
        PlayerHPBar.Location = New Point(21, 12)
        PlayerHPBar.Name = "PlayerHPBar"
        PlayerHPBar.Size = New Size(346, 33)
        PlayerHPBar.TabIndex = 10
        ' 
        ' EnemyHPBar
        ' 
        EnemyHPBar.Location = New Point(826, 12)
        EnemyHPBar.Name = "EnemyHPBar"
        EnemyHPBar.Size = New Size(346, 33)
        EnemyHPBar.TabIndex = 11
        ' 
        ' lblWordDisplay
        ' 
        lblWordDisplay.AutoSize = True
        lblWordDisplay.BackColor = Color.Black
        lblWordDisplay.BorderStyle = BorderStyle.FixedSingle
        lblWordDisplay.Font = New Font("Arial", 32.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblWordDisplay.ForeColor = Color.White
        lblWordDisplay.Location = New Point(386, 218)
        lblWordDisplay.Name = "lblWordDisplay"
        lblWordDisplay.Size = New Size(2, 53)
        lblWordDisplay.TabIndex = 13
        ' 
        ' lblPlayerName
        ' 
        lblPlayerName.AutoSize = True
        lblPlayerName.Font = New Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblPlayerName.ForeColor = SystemColors.ControlLightLight
        lblPlayerName.Location = New Point(21, 48)
        lblPlayerName.Name = "lblPlayerName"
        lblPlayerName.Size = New Size(47, 30)
        lblPlayerName.TabIndex = 14
        lblPlayerName.Text = "Lex"
        ' 
        ' lblEnemyName
        ' 
        lblEnemyName.AutoSize = True
        lblEnemyName.Font = New Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblEnemyName.ForeColor = SystemColors.ControlLightLight
        lblEnemyName.Location = New Point(956, 48)
        lblEnemyName.Name = "lblEnemyName"
        lblEnemyName.Size = New Size(105, 30)
        lblEnemyName.TabIndex = 15
        lblEnemyName.Text = "Minotaur"
        ' 
        ' lblscore
        ' 
        lblscore.BackColor = Color.Transparent
        lblscore.Font = New Font("Ravie", 15.75F, FontStyle.Bold)
        lblscore.ForeColor = Color.Lime
        lblscore.Location = New Point(500, 12)
        lblscore.Name = "lblscore"
        lblscore.Size = New Size(254, 43)
        lblscore.TabIndex = 16
        lblscore.Text = "000000"
        ' 
        ' Potion
        ' 
        Potion.Image = My.Resources.Resources.potion
        Potion.Location = New Point(252, 421)
        Potion.Name = "Potion"
        Potion.Size = New Size(89, 75)
        Potion.SizeMode = PictureBoxSizeMode.StretchImage
        Potion.TabIndex = 17
        Potion.TabStop = False
        ' 
        ' Shield
        ' 
        Shield.BackColor = SystemColors.ActiveCaptionText
        Shield.BackgroundImage = My.Resources.Resources.shield
        Shield.BackgroundImageLayout = ImageLayout.Stretch
        Shield.Location = New Point(85, 421)
        Shield.Name = "Shield"
        Shield.Size = New Size(85, 75)
        Shield.SizeMode = PictureBoxSizeMode.StretchImage
        Shield.TabIndex = 18
        Shield.TabStop = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.White
        Label1.Font = New Font("Segoe UI Emoji", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(252, 497)
        Label1.Name = "Label1"
        Label1.Size = New Size(78, 16)
        Label1.TabIndex = 19
        Label1.Text = "Potion: 50g"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.BackColor = Color.White
        Label3.Font = New Font("Segoe UI Emoji", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(87, 497)
        Label3.Name = "Label3"
        Label3.Size = New Size(83, 17)
        Label3.TabIndex = 20
        Label3.Text = "Shield: 80g"
        ' 
        ' lblGold
        ' 
        lblGold.AutoSize = True
        lblGold.Font = New Font("Ravie", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblGold.ForeColor = Color.Yellow
        lblGold.Location = New Point(60, 545)
        lblGold.Name = "lblGold"
        lblGold.Size = New Size(105, 30)
        lblGold.TabIndex = 21
        lblGold.Text = "GOLD: "
        ' 
        ' btnInstaKill
        ' 
        btnInstaKill.Location = New Point(1071, 726)
        btnInstaKill.Name = "btnInstaKill"
        btnInstaKill.Size = New Size(75, 23)
        btnInstaKill.TabIndex = 23
        btnInstaKill.Text = "InstaKill"
        btnInstaKill.UseVisualStyleBackColor = True
        ' 
        ' picBackground
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.Black
        BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), Image)
        BackgroundImageLayout = ImageLayout.Stretch
        ClientSize = New Size(1184, 761)
        Controls.Add(btnInstaKill)
        Controls.Add(lblGold)
        Controls.Add(Label3)
        Controls.Add(Label1)
        Controls.Add(Shield)
        Controls.Add(Potion)
        Controls.Add(lblscore)
        Controls.Add(lblEnemyName)
        Controls.Add(lblPlayerName)
        Controls.Add(lblWordDisplay)
        Controls.Add(EnemyHPBar)
        Controls.Add(PlayerHPBar)
        Controls.Add(btnScramble)
        Controls.Add(btnAttack)
        Controls.Add(pnlGrid)
        Controls.Add(PictureBox3)
        Controls.Add(EnemyMino)
        Controls.Add(LexWorm)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Name = "picBackground"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Book Journey"
        CType(EnemyMino, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox3, ComponentModel.ISupportInitialize).EndInit()
        CType(LexWorm, ComponentModel.ISupportInitialize).EndInit()
        pnlGrid.ResumeLayout(False)
        tblGrid.ResumeLayout(False)
        CType(Potion, ComponentModel.ISupportInitialize).EndInit()
        CType(Shield, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents GameTimer As Timer
    Friend WithEvents EnemyMino As PictureBox
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents LexWorm As PictureBox
    Friend WithEvents pnlGrid As Panel
    Friend WithEvents tblGrid As TableLayoutPanel
    Friend WithEvents btnGrid15 As Button
    Friend WithEvents btnGrid14 As Button
    Friend WithEvents btnGrid13 As Button
    Friend WithEvents btnGrid12 As Button
    Friend WithEvents btnGrid11 As Button
    Friend WithEvents btnGrid10 As Button
    Friend WithEvents btnGrid09 As Button
    Friend WithEvents btnGrid08 As Button
    Friend WithEvents btnGrid07 As Button
    Friend WithEvents btnGrid06 As Button
    Friend WithEvents btnGrid05 As Button
    Friend WithEvents btnGrid04 As Button
    Friend WithEvents btnGrid03 As Button
    Friend WithEvents btnGrid02 As Button
    Friend WithEvents btnGrid01 As Button
    Friend WithEvents btnGrid00 As Button
    Friend WithEvents btnAttack As Button
    Friend WithEvents btnScramble As Button
    Friend WithEvents PlayerHPBar As ProgressBar
    Friend WithEvents EnemyHPBar As ProgressBar
    Friend WithEvents lblWordDisplay As Label
    Friend WithEvents DamageFlickerTimer As Timer
    Friend WithEvents lblPlayerName As Label
    Friend WithEvents lblEnemyName As Label
    Friend WithEvents lblscore As Label
    Friend WithEvents Potion As PictureBox
    Friend WithEvents Shield As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents lblGold As Label
    Friend WithEvents btnInstaKill As Button

End Class
