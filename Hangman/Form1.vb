Imports System.IO
Imports System.Drawing.Imaging

Public Class picBackground

    '==========================================
    ' GAME INSTANCE
    '==========================================
    Private game As New BattleGame()

    '==========================================
    ' BUTTON GRID ARRAY
    '==========================================
    Private btnGrid(3, 3) As Button
    Private Sub UpdateGoldDisplay()
        lblGold.Text = "GOLD: " & game.Gold.ToString() & "g"
    End Sub
    '==========================================
    ' WORD BUILDING
    '==========================================
    Private selectedButtons As New List(Of Button)
    Private selectedPositions As New List(Of Point)
    Private currentWord As String = ""

    '==========================================
    ' DAMAGE FLICKER
    '==========================================
    Private flickerCount As Integer = 0
    Private flickerTarget As PictureBox = Nothing

    '==========================================
    ' UPDATE SCORE DISPLAY
    '==========================================
    Private Sub UpdateScoreDisplay()
        lblscore.Text = game.GetScoreString()
    End Sub

    '==========================================
    ' FORM LOAD
    '==========================================
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            game.PlayerName = MainMenu.PlayerName
            game.PlayerName = MainMenu.SelectedCharacter
            lblPlayerName.Text = MainMenu.SelectedCharacter

            ' Load selected character image
            Dim charPath As String = Path.Combine(
    Application.StartupPath,
    MainMenu.SelectedCharacterImage)

            If File.Exists(charPath) Then
                LexWorm.Image = Image.FromFile(charPath)
            End If

            SetupGrid()

            AddHandler btnAttack.Click, AddressOf btnAttack_Click
            AddHandler btnScramble.Click, AddressOf btnScramble_Click
            AddHandler lblWordDisplay.Click, AddressOf WordDisplay_Backspace
            AddHandler DamageFlickerTimer.Tick, AddressOf DamageFlickerTimer_Tick

            UpdateGridButtons()
            UpdateHPBars()
            UpdateScoreDisplay()
            UpdateGoldDisplay()  ' ADD THIS EVERY TIME
            UpdateEnemyDisplay()

            MessageBox.Show("Bookworm Adventures Ready!" & vbCrLf & vbCrLf &
                       "Click letter tiles to spell words (min 3 letters)" & vbCrLf &
                       "Then click ATTACK to deal damage!" & vbCrLf & vbCrLf &
                       game.GetScoreString(),
                       "Game Start", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("ERROR: " & ex.Message & vbCrLf & vbCrLf & ex.StackTrace,
                       "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '==========================================
    ' GET COLOR FOR ELEMENT
    '==========================================
    Private Function GetElementColor(element As String) As Color
        Select Case element
            Case "Fire" : Return Color.OrangeRed
            Case "Water" : Return Color.DodgerBlue
            Case "Nature" : Return Color.ForestGreen
            Case "Lightning" : Return Color.Gold
            Case "Dark" : Return Color.MediumPurple
            Case Else : Return Color.BurlyWood
        End Select
    End Function

    '==========================================
    ' GET ELEMENT EMOJI INDICATOR
    '==========================================
    Private Function GetElementIndicator(element As String) As String
        Select Case element
            Case "Fire" : Return "🔥"
            Case "Water" : Return "💧"
            Case "Nature" : Return "🌿"
            Case "Lightning" : Return "⚡"
            Case "Dark" : Return "💀"
            Case Else : Return ""
        End Select
    End Function

    '==========================================
    ' TINT IMAGE (for slime color variants)
    '==========================================
    Private Function TintImage(original As Image, tintColor As Color) As Image
        Dim bmp As New Bitmap(original.Width, original.Height)
        Dim g As Graphics = Graphics.FromImage(bmp)

        Dim r As Single = tintColor.R / 255.0F
        Dim gb As Single = tintColor.G / 255.0F
        Dim b As Single = tintColor.B / 255.0F

        Dim colorMatrix As New ColorMatrix(New Single()() {
            New Single() {r, 0, 0, 0, 0},
            New Single() {0, gb, 0, 0, 0},
            New Single() {0, 0, b, 0, 0},
            New Single() {0, 0, 0, 1, 0},
            New Single() {0, 0, 0, 0, 1}
        })

        Dim attrs As New ImageAttributes()
        attrs.SetColorMatrix(colorMatrix)
        g.DrawImage(original,
            New Rectangle(0, 0, bmp.Width, bmp.Height),
            0, 0, original.Width, original.Height,
            GraphicsUnit.Pixel, attrs)
        g.Dispose()
        Return bmp
    End Function

    '==========================================
    ' UPDATE ENEMY DISPLAY
    '==========================================
    Private Sub UpdateEnemyDisplay()
        lblEnemyName.Text = game.CurrentEnemy.Name & " (" & game.CurrentEnemy.Element & ")"
        lblEnemyName.BackColor = GetElementColor(game.CurrentEnemy.Element)
        lblEnemyName.ForeColor = Color.White
        '==========================================
        ' BACKGROUND CHANGE BASED ON ENEMY  
        '==========================================
        Dim bgFile As String = ""

        Select Case game.CurrentEnemy.Name
            Case "Green Slime", "Red Slime", "Blue Slime"
                bgFile = "Resources\Background\forestworld.jpg"
            Case "Zombie", "Golem"
                bgFile = "Resources\Background\Dungeon.jpg"
            Case "Shadow Lord", "Minotaur", "Evil Duck"
                bgFile = "Resources\Background\cave.jpg"
        End Select

        Dim bgPath As String = Path.Combine(Application.StartupPath, bgFile)
        If File.Exists(bgPath) Then
            Me.BackgroundImage = Image.FromFile(bgPath)
            Me.BackgroundImageLayout = ImageLayout.Stretch
        End If

        Dim imgPath As String = Path.Combine(
            Application.StartupPath,
            game.CurrentEnemy.ImageFile
        )

        If game.CurrentEnemy.ImageFile <> "" AndAlso File.Exists(imgPath) Then
            Dim baseImage As Image = Image.FromFile(imgPath)
            Select Case game.CurrentEnemy.Name
                Case "Red Slime"
                    EnemyMino.Image = TintImage(baseImage, Color.OrangeRed)
                Case "Blue Slime"
                    EnemyMino.Image = TintImage(baseImage, Color.DodgerBlue)
                Case Else
                    EnemyMino.Image = baseImage
            End Select
            EnemyMino.BackColor = Color.Transparent
        Else
            EnemyMino.Image = Nothing
            EnemyMino.BackColor = GetElementColor(game.CurrentEnemy.Element)
        End If

        UpdateHPBars()
    End Sub

    '==========================================
    ' SETUP GRID
    '==========================================
    Private Sub SetupGrid()
        btnGrid(0, 0) = btnGrid00
        btnGrid(0, 1) = btnGrid01
        btnGrid(0, 2) = btnGrid02
        btnGrid(0, 3) = btnGrid03
        btnGrid(1, 0) = btnGrid04
        btnGrid(1, 1) = btnGrid05
        btnGrid(1, 2) = btnGrid06
        btnGrid(1, 3) = btnGrid07
        btnGrid(2, 0) = btnGrid08
        btnGrid(2, 1) = btnGrid09
        btnGrid(2, 2) = btnGrid10
        btnGrid(2, 3) = btnGrid11
        btnGrid(3, 0) = btnGrid12
        btnGrid(3, 1) = btnGrid13
        btnGrid(3, 2) = btnGrid14
        btnGrid(3, 3) = btnGrid15

        For i As Integer = 0 To 3
            For j As Integer = 0 To 3
                btnGrid(i, j).Font = New Font("Arial", 16, FontStyle.Bold)
                btnGrid(i, j).BackColor = Color.BurlyWood
                btnGrid(i, j).FlatStyle = FlatStyle.Flat
                AddHandler btnGrid(i, j).Click, AddressOf GridButton_Click
            Next
        Next
    End Sub

    '==========================================
    ' UPDATE GRID BUTTONS
    '==========================================
    Private Sub UpdateGridButtons()
        For i As Integer = 0 To 3
            For j As Integer = 0 To 3
                Dim elem As String = game.GridElements(i, j)
                btnGrid(i, j).Enabled = True
                btnGrid(i, j).ForeColor = Color.White

                If elem = "Normal" Then
                    btnGrid(i, j).Text = game.Grid(i, j)
                    btnGrid(i, j).BackColor = Color.BurlyWood
                    btnGrid(i, j).ForeColor = Color.Black
                    btnGrid(i, j).Font = New Font("Arial", 24, FontStyle.Bold)
                Else
                    Dim indicator As String = GetElementIndicator(elem)
                    btnGrid(i, j).Text = game.Grid(i, j) & vbCrLf & indicator
                    btnGrid(i, j).BackColor = GetElementColor(elem)
                    btnGrid(i, j).ForeColor = Color.White
                    btnGrid(i, j).Font = New Font("Arial", 16, FontStyle.Bold)
                End If
            Next
        Next
    End Sub

    '==========================================
    ' UPDATE WORD DISPLAY
    '==========================================
    Private Sub UpdateWordDisplay()
        Dim displayWord As String = ""
        For Each c As Char In currentWord
            displayWord &= c.ToString() & " "
        Next
        lblWordDisplay.Text = displayWord.Trim()
    End Sub

    '==========================================
    ' VALIDATE WORD DISPLAY (color feedback)
    '==========================================
    Private Sub ValidateWordDisplay()
        If currentWord.Length = 0 Then
            lblWordDisplay.BackColor = Color.Black
            lblWordDisplay.ForeColor = Color.White
            Return
        End If

        If currentWord.Length < 3 Then
            lblWordDisplay.BackColor = Color.FromArgb(40, 40, 40)
            lblWordDisplay.ForeColor = Color.White
            Return
        End If

        Dim result As Tuple(Of Integer, String, Double) =
            game.ValidateWordWithElement(currentWord, selectedPositions)

        If result.Item1 > 0 Then
            If result.Item3 >= 2.0 Then
                ' Super effective - bright green
                lblWordDisplay.BackColor = Color.LimeGreen
                lblWordDisplay.ForeColor = Color.Black
            ElseIf result.Item3 <= 0.5 Then
                ' Not effective - dark orange
                lblWordDisplay.BackColor = Color.DarkOrange
                lblWordDisplay.ForeColor = Color.White
            Else
                ' Normal valid word - dark green
                lblWordDisplay.BackColor = Color.DarkGreen
                lblWordDisplay.ForeColor = Color.LimeGreen
            End If
        ElseIf result.Item1 = -2 Then
            lblWordDisplay.BackColor = Color.DarkRed
            lblWordDisplay.ForeColor = Color.Red
        Else
            lblWordDisplay.BackColor = Color.DarkOrange
            lblWordDisplay.ForeColor = Color.Orange
        End If
    End Sub

    '==========================================
    ' GRID BUTTON CLICK
    '==========================================
    Private Sub GridButton_Click(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)

        ' Find grid position of this button
        Dim pos As New Point(-1, -1)
        For i As Integer = 0 To 3
            For j As Integer = 0 To 3
                If btnGrid(i, j) Is btn Then
                    pos = New Point(i, j)
                End If
            Next
        Next

        ' Add letter (first line only in case of element tile)
        Dim letter As String = game.Grid(pos.X, pos.Y)
        currentWord &= letter
        selectedButtons.Add(btn)
        selectedPositions.Add(pos)

        ' Highlight selected button with element color (darker)
        btn.BackColor = Color.Orange
        btn.Enabled = False

        UpdateWordDisplay()
        ValidateWordDisplay()
    End Sub

    Private Sub RefreshUsedTiles()
        ' Only regenerate tiles that were used in the word
        Dim letters As String = "AAAAAABBCCDDDDEEEEEEEEFFGGGHHIIIIIJKLLLLMMNNNNNNOOOOOOOPPQRRRRRRSSSSSSTTTTTTUUUUVVWWXYYZ"
        Dim activeElements() As String = {"Fire", "Water", "Nature", "Lightning", "Dark", "Normal", "Normal", "Normal"}
        Dim rng As New Random()

        For Each pos As Point In selectedPositions
            ' New random letter
            game.Grid(pos.X, pos.Y) = letters.Substring(rng.Next(0, letters.Length), 1)

            ' Small chance to become element tile
            If rng.Next(0, 100) < 35 Then
                game.GridElements(pos.X, pos.Y) = activeElements(rng.Next(0, 5))
            Else
                game.GridElements(pos.X, pos.Y) = "Normal"
            End If
        Next
    End Sub

    '==========================================
    ' ATTACK BUTTON
    '==========================================
    Private Sub btnAttack_Click(sender As Object, e As EventArgs)
        If currentWord.Length = 0 Then
            MessageBox.Show("Select some letters first!", "No Word",
                MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        game.CurrentWord = currentWord

        Dim result As Tuple(Of Integer, String, Double) =
            game.ValidateWordWithElement(currentWord, selectedPositions)
        Dim damage As Integer = result.Item1
        Dim wordElement As String = result.Item2
        Dim multiplier As Double = result.Item3

        If damage > 0 Then
            game.AttackEnemy(damage)
            RefreshUsedTiles()
            UpdateGridButtons()
            UpdateScoreDisplay()
            UpdateGoldDisplay()  ' ADD THIS EVERY TIME
            UpdateGoldDisplay()
            StartDamageFlicker(EnemyMino)

            ' Build effectiveness message
            Dim elemMsg As String = ""
            If wordElement <> "Normal" Then
                If multiplier >= 2.0 Then
                    elemMsg = vbCrLf & "⚡ SUPER EFFECTIVE! (x2)"
                ElseIf multiplier <= 0.5 Then
                    elemMsg = vbCrLf & "❌ NOT VERY EFFECTIVE... (x0.5)"
                Else
                    elemMsg = vbCrLf & wordElement & " element (x1)"
                End If
            End If

            MessageBox.Show("Valid word: " & currentWord.ToUpper() & vbCrLf &
                           "Damage: " & damage.ToString() & elemMsg & vbCrLf &
                           game.GetScoreString(),
                           "Hit!", MessageBoxButtons.OK, MessageBoxIcon.Information)

            UpdateHPBars()

            ' Check if enemy is dead
            If game.IsEnemyDead() Then
                game.SaveHighscore(MainMenu.PlayerName)

                Dim killGold As Integer = game.GetEnemyKillGold()
                game.Gold += killGold
                MessageBox.Show("Victory! You defeated " & game.EnemyName & "!" & vbCrLf &
   game.GetScoreString() & vbCrLf &
   "+" & killGold.ToString() & "g earned!" & vbCrLf & vbCrLf &
   "New enemy incoming!",
   "You Win!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                game.LoadNextEnemy()
                UpdateEnemyDisplay()
                UpdateGridButtons()
                UpdateHPBars()
                UpdateScoreDisplay()
                UpdateGoldDisplay()
                ClearWord()
                Return
            End If

            ' Enemy attacks back
            Dim enemyDamage As Integer = game.EnemyAttack()
            If enemyDamage = -1 Then
                MessageBox.Show("🛡️ SHIELD BLOCKED the attack!" & vbCrLf &
                                "No damage taken!",
                                "Shield!", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show(game.EnemyName & " attacks back!" & vbCrLf &
                    "Damage taken: " & enemyDamage.ToString(),
                    "Enemy Turn", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                StartDamageFlicker(LexWorm)
            End If

            UpdateHPBars()

            ' Check if player is dead
            If game.IsPlayerDead() Then
                game.SaveHighscore(MainMenu.PlayerName)
                MessageBox.Show("Game Over! You were defeated!" & vbCrLf &
                   game.GetScoreString(),
                   "Defeat", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()
                Return
            End If
        ElseIf damage = -2 Then
            MessageBox.Show("NOT A VALID WORD!", "Invalid",
                MessageBoxButtons.OK, MessageBoxIcon.Warning)
        ElseIf damage = -1 Then
            MessageBox.Show("LETTERS NOT AVAILABLE!", "Invalid",
                MessageBoxButtons.OK, MessageBoxIcon.Warning)
        ElseIf damage = 0 Then
            MessageBox.Show("TOO SHORT! (Min 3 letters)", "Invalid",
                MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        ClearWord()
    End Sub

    '==========================================
    ' DAMAGE FLICKER
    '==========================================
    Private Sub StartDamageFlicker(target As PictureBox)
        flickerTarget = target
        flickerCount = 0
        DamageFlickerTimer.Interval = 100
        DamageFlickerTimer.Start()
    End Sub

    Private Sub DamageFlickerTimer_Tick(sender As Object, e As EventArgs)
        flickerCount += 1
        If flickerTarget IsNot Nothing Then
            flickerTarget.Visible = Not flickerTarget.Visible
        End If
        If flickerCount >= 6 Then
            If flickerTarget IsNot Nothing Then
                flickerTarget.Visible = True
            End If
            DamageFlickerTimer.Stop()
        End If
    End Sub

    '==========================================
    ' SCRAMBLE BUTTON
    '==========================================
    Private Sub btnScramble_Click(sender As Object, e As EventArgs) Handles btnScramble.Click
        Dim penalty As Integer = game.PenalizedScramble()
        ClearWord()
        UpdateGridButtons()
        UpdateHPBars()
        MessageBox.Show("Grid scrambled!" & vbCrLf &
                    "Ouch! Lost " & penalty.ToString() & " HP.",
                    "Scramble", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    '==========================================
    ' CLEAR WORD
    '==========================================
    Private Sub ClearWord()
        currentWord = ""
        game.CurrentWord = ""
        lblWordDisplay.Text = ""
        lblWordDisplay.BackColor = Color.Black
        lblWordDisplay.ForeColor = Color.White

        For Each btn As Button In selectedButtons
            btn.Enabled = True
        Next

        selectedButtons.Clear()
        selectedPositions.Clear()

        ' Refresh grid to restore element colors
        UpdateGridButtons()

        Me.Text = "Bookworm Adventures"
    End Sub

    '==========================================
    ' BACKSPACE
    '==========================================
    Private Sub WordDisplay_Backspace(sender As Object, e As EventArgs)
        If currentWord.Length = 0 Then Return

        currentWord = currentWord.Substring(0, currentWord.Length - 1)

        If selectedButtons.Count > 0 Then
            Dim lastButton As Button = selectedButtons(selectedButtons.Count - 1)
            Dim lastPos As Point = selectedPositions(selectedPositions.Count - 1)

            ' Restore element color
            Dim elem As String = game.GridElements(lastPos.X, lastPos.Y)
            If elem = "Normal" Then
                lastButton.BackColor = Color.BurlyWood
                lastButton.ForeColor = Color.Black
            Else
                lastButton.BackColor = GetElementColor(elem)
                lastButton.ForeColor = Color.White
            End If

            lastButton.Enabled = True
            selectedButtons.RemoveAt(selectedButtons.Count - 1)
            selectedPositions.RemoveAt(selectedPositions.Count - 1)
        End If

        UpdateWordDisplay()
        ValidateWordDisplay()
    End Sub

    '==========================================
    ' INSTAKILL BUTTON (DEMO/PRESENTATION ONLY)
    '==========================================
    Private Sub btnInstakill_Click(sender As Object, e As EventArgs) Handles btnInstaKill.Click
        ' Confirm so you don't accidentally press it
        Dim confirm As DialogResult = MessageBox.Show(
        "DEMO MODE: Instantly kill " & game.CurrentEnemy.Name & "?" & vbCrLf &
        "(For presentation purposes only)",
        "Instakill", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If confirm = DialogResult.No Then Return

        ' Set enemy HP to 0
        game.CurrentEnemy.HP = 0
        UpdateHPBars()

        ' Trigger the same victory logic as normal kill
        game.SaveHighscore(MainMenu.PlayerName)

        Dim killGold As Integer = game.GetEnemyKillGold()
        game.Gold += killGold

        MessageBox.Show("💀 INSTAKILL! " & game.EnemyName & " defeated!" & vbCrLf &
       game.GetScoreString() & vbCrLf &
       "+" & killGold.ToString() & "g earned!",
       "Enemy Defeated!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        game.LoadNextEnemy()
        UpdateEnemyDisplay()
        UpdateGridButtons()
        UpdateHPBars()
        UpdateScoreDisplay()
        UpdateGoldDisplay()
        ClearWord()
    End Sub

    '==========================================
    ' POTION CLICK
    '==========================================
    Private Sub Potion_Click(sender As Object, e As EventArgs) Handles Potion.Click
        Dim result As String = game.BuyPotion()
        UpdateHPBars()
        UpdateGoldDisplay()
        MessageBox.Show(result & vbCrLf & vbCrLf &
                game.GetPlayerHPString() & vbCrLf &
                "Gold: " & game.Gold.ToString() & "g",
                "Potion", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Shield_Click(sender As Object, e As EventArgs) Handles Shield.Click
        Dim result As String = game.BuyShield()
        UpdateGoldDisplay()
        MessageBox.Show(result & vbCrLf & vbCrLf &
                "Gold: " & game.Gold.ToString() & "g",
                "Shield", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    '==========================================
    ' UPDATE HP BARS
    '==========================================
    Private Sub UpdateHPBars()
        PlayerHPBar.Minimum = 0
        PlayerHPBar.Maximum = game.PlayerMaxHP
        PlayerHPBar.Value = Math.Max(0, Math.Min(game.PlayerHP, game.PlayerMaxHP))

        EnemyHPBar.Minimum = 0
        EnemyHPBar.Maximum = game.CurrentEnemy.MaxHP
        EnemyHPBar.Value = game.CurrentEnemy.MaxHP
        EnemyHPBar.Value = Math.Max(0, Math.Min(game.CurrentEnemy.HP, game.CurrentEnemy.MaxHP))

        If game.GetPlayerHPPercent() < 30 Then
            PlayerHPBar.ForeColor = Color.Red
        ElseIf game.GetPlayerHPPercent() < 60 Then
            PlayerHPBar.ForeColor = Color.Orange
        Else
            PlayerHPBar.ForeColor = Color.LimeGreen
        End If

        If game.GetEnemyHPPercent() < 30 Then
            EnemyHPBar.ForeColor = Color.Red
        ElseIf game.GetEnemyHPPercent() < 60 Then
            EnemyHPBar.ForeColor = Color.Orange
        Else
            EnemyHPBar.ForeColor = Color.LimeGreen
        End If

        ' Shield visual feedback
        If game.ShieldActive Then
            Shield.BorderStyle = BorderStyle.Fixed3D
            Shield.BackColor = Color.CornflowerBlue
        Else
            Shield.BorderStyle = BorderStyle.None
            Shield.BackColor = Color.Transparent
        End If
    End Sub

    '==========================================
    ' RESET GAME
    '==========================================
    Private Sub ResetGame()
        game.ResetGame()
        ClearWord()
        UpdateGridButtons()
        UpdateHPBars()
        UpdateEnemyDisplay()
        MessageBox.Show("Game Reset!" & vbCrLf & "Good luck!",
                       "New Game", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub lblEnemyName_Click(sender As Object, e As EventArgs) Handles lblEnemyName.Click

    End Sub
End Class