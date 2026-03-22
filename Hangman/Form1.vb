Public Class Form1

    '==========================================
    ' GAME INSTANCE
    '==========================================
    Private game As New BattleGame()


    '==========================================
    ' BUTTON GRID ARRAY
    '==========================================
    Private btnGrid(3, 3) As Button

    '==========================================
    ' WORD BUILDING
    '==========================================
    Private selectedButtons As New List(Of Button)
    Private currentWord As String = ""

    '==========================================
    ' UPDATE SCORE DISPLAY
    '==========================================
    Private Sub UpdateScoreDisplay()
        lblscore.Text = game.GetScoreString()
    End Sub

    '==========================================
    ' FORM LOAD - INITIALIZE EVERYTHING
    '==========================================
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Use the player name from MainMenu (String usage!)
            game.PlayerName = MainMenu.PlayerName

            ' Setup the grid buttons
            SetupGrid()

            ' Wire up button handlers
            AddHandler btnAttack.Click, AddressOf btnAttack_Click
            AddHandler btnScramble.Click, AddressOf btnScramble_Click
            AddHandler lblWordDisplay.Click, AddressOf WordDisplay_Backspace
            AddHandler DamageFlickerTimer.Tick, AddressOf DamageFlickerTimer_Tick

            ' Initialize the grid with letters
            UpdateGridButtons()

            ' Initialize HP bars
            UpdateHPBars()
            UpdateScoreDisplay()
            ' Show initial instructions
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
    ' SETUP GRID - Map designer buttons to array
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

        ' Style all buttons and add click handlers
        For i As Integer = 0 To 3
            For j As Integer = 0 To 3
                btnGrid(i, j).Font = New Font("Arial", 24, FontStyle.Bold)
                btnGrid(i, j).BackColor = Color.BurlyWood
                btnGrid(i, j).FlatStyle = FlatStyle.Flat
                AddHandler btnGrid(i, j).Click, AddressOf GridButton_Click
            Next
        Next
    End Sub

    '==========================================
    ' UPDATE GRID BUTTONS - Fill with letters from game
    '==========================================
    Private Sub UpdateGridButtons()
        For i As Integer = 0 To 3
            For j As Integer = 0 To 3
                btnGrid(i, j).Text = game.Grid(i, j)
                btnGrid(i, j).Enabled = True
                btnGrid(i, j).BackColor = Color.BurlyWood
                btnGrid(i, j).ForeColor = Color.Black
            Next
        Next
    End Sub

    '==========================================
    ' UPDATE WORD DISPLAY - Show letters with spacing
    '==========================================

    Private Sub UpdateWordDisplay()
        ' Create spaced out word display (String Functions!)
        Dim displayWord As String = ""
        For Each c As Char In currentWord
            displayWord &= c.ToString() & " "
        Next

        lblWordDisplay.Text = displayWord.Trim()
    End Sub

    '==========================================
    ' VALIDATE WORD DISPLAY - Color code the word
    '==========================================
    Private Sub ValidateWordDisplay()
        If currentWord.Length = 0 Then
            lblWordDisplay.BackColor = Color.Black
            lblWordDisplay.ForeColor = Color.White
            Return
        End If

        ' Check if less than 3 letters (white/yellow - still typing)
        If currentWord.Length < 3 Then
            lblWordDisplay.BackColor = Color.FromArgb(40, 40, 40)
            lblWordDisplay.ForeColor = Color.White
            Return
        End If

        ' Validate the word
        Dim result As Integer = game.ValidateWord(currentWord)

        If result > 0 Then
            ' VALID - Green
            lblWordDisplay.BackColor = Color.DarkGreen
            lblWordDisplay.ForeColor = Color.LimeGreen
        ElseIf result = -2 Then
            ' NOT A REAL WORD - Red
            lblWordDisplay.BackColor = Color.DarkRed
            lblWordDisplay.ForeColor = Color.Red
        Else
            ' OTHER ERROR - Orange/Yellow
            lblWordDisplay.BackColor = Color.DarkOrange
            lblWordDisplay.ForeColor = Color.Orange
        End If
    End Sub

    '==========================================
    ' GRID BUTTON CLICK - Build word with visual feedback
    '==========================================
    Private Sub GridButton_Click(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)

        ' Add letter to current word
        currentWord &= btn.Text

        ' Mark button as selected
        selectedButtons.Add(btn)
        btn.BackColor = Color.Orange
        btn.Enabled = False  ' Can't click same button twice

        ' Update word display with spacing
        UpdateWordDisplay()

        ' Check if word is valid in real-time
        ValidateWordDisplay()
    End Sub

    '==========================================
    ' ATTACK BUTTON - Submit word and attack
    '==========================================
    Private Sub btnAttack_Click(sender As Object, e As EventArgs)
        ' Check if player has selected any letters
        If currentWord.Length = 0 Then
            MessageBox.Show("Select some letters first!", "No Word", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Store the word in the game
        game.CurrentWord = currentWord

        ' Validate the word
        Dim damage As Integer = game.ValidateWord(currentWord)

        ' Handle result
        If damage > 0 Then
            ' Valid word! Attack enemy
            game.AttackEnemy(damage)

            ' ADD THIS LINE HERE:
            game.ScrambleGrid()
            UpdateGridButtons()
            UpdateScoreDisplay()
            StartDamageFlicker(EnemyMino)

            ' Show success message (String Functions!)
            MessageBox.Show("Valid word: " & currentWord.ToUpper() & vbCrLf &
                           "Damage: " & damage.ToString() & vbCrLf &
                           game.GetScoreString(),
                           "Hit!", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Update HP bars
            UpdateHPBars()

            ' Check if enemy is dead
            If game.IsEnemyDead() Then
                game.SaveHighscore(MainMenu.PlayerName)

                ' Show victory message
                MessageBox.Show("Victory! You defeated " & game.EnemyName & "!" & vbCrLf &
                   game.GetScoreString() & vbCrLf & vbCrLf &
                   "New enemy incoming!",
                   "You Win!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                ' Spawn new enemy (keep player HP!)
                game.EnemyHP = game.EnemyMaxHP  ' Reset enemy HP only
                game.ScrambleGrid()
                UpdateGridButtons()
                UpdateHPBars()
                UpdateScoreDisplay()
                Return
            End If

            ' Enemy attacks back
            Dim enemyDamage As Integer = game.EnemyAttack()
            MessageBox.Show(game.EnemyName & " attacks back!" & vbCrLf &
                           "Damage taken: " & enemyDamage.ToString(),
                           "Enemy Turn", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            StartDamageFlicker(LexWorm)

            ' Update HP bars again
            UpdateHPBars()

            ' Check if player is dead
            If game.IsPlayerDead() Then
                game.SaveHighscore(MainMenu.PlayerName)  ' ADD THIS
                MessageBox.Show("Game Over! You were defeated!" & vbCrLf &
                   game.GetScoreString(),
                   "Defeat", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()  ' Close game and return to main menu
                Return
            End If
        End If

        ' Clear word and reset buttons
        ClearWord()
    End Sub

    ' At the top with other declarations
    Private flickerCount As Integer = 0
    Private flickerTarget As PictureBox = Nothing

    Private Sub StartDamageFlicker(target As PictureBox)
        flickerTarget = target
        flickerCount = 0
        DamageFlickerTimer.Interval = 100
        DamageFlickerTimer.Start()
    End Sub

    ' Add the timer handler
    Private Sub DamageFlickerTimer_Tick(sender As Object, e As EventArgs)
        flickerCount += 1

        ' Toggle visibility
        If flickerTarget IsNot Nothing Then
            flickerTarget.Visible = Not flickerTarget.Visible
        End If

        ' Stop after 6 flickers (3 on, 3 off)
        If flickerCount >= 6 Then
            If flickerTarget IsNot Nothing Then
                flickerTarget.Visible = True
            End If
            DamageFlickerTimer.Stop()
        End If
    End Sub

    '==========================================
    ' SCRAMBLE BUTTON - Regenerate grid
    '==========================================
    Private Sub btnScramble_Click(sender As Object, e As EventArgs) Handles btnScramble.Click
        ' Scramble the grid
        game.ScrambleGrid()

        ' Clear current word
        ClearWord()

        ' Update buttons with new letters
        UpdateGridButtons()

        ' Show message
        MessageBox.Show("Grid scrambled!" & vbCrLf & "New letters generated.",
                       "Scramble", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    '==========================================
    ' CLEAR WORD - Reset selection
    '==========================================
    Private Sub ClearWord()
        ' Clear the current word
        currentWord = ""
        game.CurrentWord = ""

        ' Clear the display
        lblWordDisplay.Text = ""
        lblWordDisplay.BackColor = Color.Black
        lblWordDisplay.ForeColor = Color.White

        ' Reset all selected buttons
        For Each btn As Button In selectedButtons
            btn.BackColor = Color.BurlyWood
            btn.Enabled = True
        Next
        selectedButtons.Clear()

        ' Update title
        Me.Text = "Bookworm Adventures"
    End Sub

    '==========================================
    ' BACKSPACE - Remove last letter
    '==========================================
    Private Sub WordDisplay_Backspace(sender As Object, e As EventArgs)
        If currentWord.Length = 0 Then Return

        ' Remove last letter (String Function: Substring!)
        currentWord = currentWord.Substring(0, currentWord.Length - 1)

        ' Get the last button that was clicked
        If selectedButtons.Count > 0 Then
            Dim lastButton As Button = selectedButtons(selectedButtons.Count - 1)
            lastButton.BackColor = Color.BurlyWood
            lastButton.Enabled = True
            selectedButtons.RemoveAt(selectedButtons.Count - 1)
        End If

        ' Update display
        UpdateWordDisplay()
        ValidateWordDisplay()
    End Sub

    '==========================================
    ' UPDATE HP BARS
    '==========================================
    Private Sub UpdateHPBars()
        ' Update player HP bar (Calculation!)
        PlayerHPBar.Maximum = game.PlayerMaxHP
        PlayerHPBar.Value = game.PlayerHP

        ' Update enemy HP bar (Calculation!)
        EnemyHPBar.Maximum = game.EnemyMaxHP
        EnemyHPBar.Value = game.EnemyHP

        ' Optional: Change color based on HP percentage
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
    End Sub

    '==========================================
    ' RESET GAME
    '==========================================
    Private Sub ResetGame()
        ' Reset game state
        game.ResetGame()

        ' Clear word
        ClearWord()

        ' Update everything
        UpdateGridButtons()
        UpdateHPBars()

        ' Show restart message
        MessageBox.Show("Game Reset!" & vbCrLf & "Good luck!",
                       "New Game", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub


End Class