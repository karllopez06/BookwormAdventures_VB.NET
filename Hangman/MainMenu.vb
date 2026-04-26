Imports System.IO
Imports System.Media

Public Class MainMenu

    '==========================================
    ' PLAYER NAME (shared with Form1)
    '==========================================
    Public Shared PlayerName As String = "Player"
    Public Shared SelectedCharacter As String = "Lex"
    Public Shared SelectedCharacterImage As String = "Resources\MainCharacters\Lex.png"
    Private bgMusic As SoundPlayer
    '==========================================
    ' START GAME BUTTON - Get name and launch game
    '==========================================

    Private Sub btnChangeCharacter_Click(sender As Object, e As EventArgs) Handles btnChangeCharacter.Click
        Dim choice As String = InputBox(
        "Choose your character:" & vbCrLf & vbCrLf &
        "Type  1  for Lex" & vbCrLf &
        "Type  2  for Lyka",
        "Change Character", "1")

        Select Case choice.Trim()
            Case "1"
                SelectedCharacter = "Lex"
                SelectedCharacterImage = "Resources\MainCharacters\Lex.png"
                MessageBox.Show("Character set to LEX!",
                "Character Selected", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Case "2"
                SelectedCharacter = "Lyka"
                SelectedCharacterImage = "Resources\MainCharacters\Lyka.png"
                MessageBox.Show("Character set to LYKA!",
                "Character Selected", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Case Else
                If choice.Trim() <> "" Then
                    MessageBox.Show("Invalid choice! Keeping current character.",
                    "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
        End Select
    End Sub
    Private Sub btnStartGame_Click(sender As Object, e As EventArgs) Handles btnStartGame.Click
        ' Show input box for player name (String Functions!)
        Dim inputName As String = InputBox("Enter your name for the highscore:",
                                           "Player Name",
                                           "Player")

        ' Validate and clean the name (String Functions: Trim, Length)
        inputName = inputName.Trim()

        If inputName.Length = 0 Then
            ' User cancelled or entered nothing
            MessageBox.Show("Name cannot be empty! Using default name.",
                           "Invalid Name",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Warning)
            PlayerName = "Player"
        ElseIf inputName.Length > 20 Then
            ' Name too long (String Function: Substring)
            MessageBox.Show("Name too long! Trimming to 20 characters.",
                           "Name Trimmed",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Information)
            PlayerName = inputName.Substring(0, 20)
        Else
            ' Valid name (String Function: ToUpper for first letter)
            PlayerName = inputName
        End If

        ' Hide main menu
        Me.Hide()

        ' Show Form1 (the game)
        Dim gameForm As New picBackground()
        gameForm.ShowDialog()

        ' When game closes, show main menu again
        Me.Show()
    End Sub

    '==========================================
    ' HOW TO PLAY BUTTON - Show instructions
    '==========================================
    Private Sub btnH2P_Click(sender As Object, e As EventArgs) Handles btnH2P.Click
        ' Create instruction text (String Functions: concatenation, vbCrLf)
        Dim instructions As String = ""

        instructions &= "╔══════════════════════════════════════╗" & vbCrLf
        instructions &= "    BOOKWORM ADVENTURES - HOW TO PLAY    " & vbCrLf
        instructions &= "╚══════════════════════════════════════╝" & vbCrLf & vbCrLf

        instructions &= "OBJECTIVE:" & vbCrLf
        instructions &= "Defeat all 8 enemies by spelling powerful words!" & vbCrLf & vbCrLf

        instructions &= "HOW TO PLAY:" & vbCrLf
        instructions &= "1. Click letter tiles to spell a word (min 3 letters)" & vbCrLf
        instructions &= "2. Watch the word display change color:" & vbCrLf
        instructions &= "   • DARK  = Still typing (less than 3 letters)" & vbCrLf
        instructions &= "   • GREEN = Valid word!" & vbCrLf
        instructions &= "   • BRIGHT GREEN = Super Effective word!" & vbCrLf
        instructions &= "   • ORANGE = Not very effective word" & vbCrLf
        instructions &= "   • RED = Not a real word" & vbCrLf
        instructions &= "3. Click ATTACK to submit your word and deal damage" & vbCrLf
        instructions &= "4. Longer words = more damage!" & vbCrLf & vbCrLf

        instructions &= "★ NEW: ELEMENT SYSTEM ★" & vbCrLf
        instructions &= "Colored tiles have elements — use them strategically!" & vbCrLf
        instructions &= "   🔥 Fire   beats 🌿 Nature" & vbCrLf
        instructions &= "   💧 Water  beats 🔥 Fire" & vbCrLf
        instructions &= "   🌿 Nature beats 💧 Water" & vbCrLf
        instructions &= "   ⚡ Lightning beats 💧 Water" & vbCrLf
        instructions &= "   💀 Dark   beats ⚡ Lightning" & vbCrLf
        instructions &= "More matching element tiles in your word = more bonus!" & vbCrLf
        instructions &= "   1 tile = x1.2 | 2 tiles = x1.5 | 3+ tiles = x2.0!" & vbCrLf & vbCrLf

        instructions &= "★ NEW: GOLD SYSTEM ★" & vbCrLf
        instructions &= "Earn Gold by attacking and defeating enemies!" & vbCrLf
        instructions &= "Spend Gold on powerups — your Score stays safe!" & vbCrLf & vbCrLf

        instructions &= "★ NEW: POWERUPS ★" & vbCrLf
        instructions &= "   🧪 Potion (50g)  = Restore 40 HP" & vbCrLf
        instructions &= "   🛡️ Shield (80g)  = Block next enemy attack" & vbCrLf & vbCrLf

        instructions &= "DAMAGE FORMULA:" & vbCrLf
        instructions &= "• Base: 5 damage per letter" & vbCrLf
        instructions &= "• Rare letters (Q, Z, X, J, K) = bonus damage" & vbCrLf
        instructions &= "• 6+ letter words: +10 damage" & vbCrLf
        instructions &= "• 8+ letter words: +20 damage" & vbCrLf
        instructions &= "• Element bonus multiplier applied on top!" & vbCrLf & vbCrLf

        instructions &= "CONTROLS:" & vbCrLf
        instructions &= "• Click tiles to build your word" & vbCrLf
        instructions &= "• Click word display to backspace" & vbCrLf
        instructions &= "• ATTACK = submit word" & vbCrLf
        instructions &= "• SCRAMBLE = new letters (-5 HP penalty!)" & vbCrLf & vbCrLf

        instructions &= "ENEMY ROSTER:" & vbCrLf
        instructions &= "   1. Green Slime (Nature)  → weak to Fire" & vbCrLf
        instructions &= "   2. Red Slime (Fire)      → weak to Water" & vbCrLf
        instructions &= "   3. Blue Slime (Water)    → weak to Nature" & vbCrLf
        instructions &= "   4. Zombie (Dark)         → weak to Lightning" & vbCrLf
        instructions &= "   5. Golem (Nature)        → weak to Fire" & vbCrLf
        instructions &= "   6. Minotaur (Fire)       → weak to Water" & vbCrLf
        instructions &= "   7. Evil Duck (Water)     → weak to Nature" & vbCrLf
        instructions &= "   8. Shadow Lord (Dark)    → weak to Lightning" & vbCrLf & vbCrLf

        instructions &= "TIPS:" & vbCrLf
        instructions &= "• Plan your element tiles across turns — grid only" & vbCrLf
        instructions &= "  refreshes the tiles YOU used, not the whole board!" & vbCrLf
        instructions &= "• Save Gold for Shield before tough enemies!" & vbCrLf
        instructions &= "• After Shadow Lord — enemies loop back harder!" & vbCrLf

        ' Show instructions (String display!)
        MessageBox.Show(instructions,
                       "How To Play",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Information)
    End Sub

    '==========================================
    ' HIGHSCORES BUTTON - Display highscores from file
    '==========================================
    Private Sub btnhscores_Click(sender As Object, e As EventArgs) Handles btnhscores.Click
        Try
            ' Path to highscores file (String Function: Combine)
            Dim filePath As String = Path.Combine(Application.StartupPath, "highscores.txt")

            ' Check if file exists
            If Not File.Exists(filePath) Then
                MessageBox.Show("No highscores yet!" & vbCrLf & vbCrLf &
                               "Play the game to set a highscore!",
                               "Highscores",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Information)
                Return
            End If

            ' Read all lines from file (String Array!)
            Dim lines() As String = File.ReadAllLines(filePath)

            ' Build highscore display (String Functions: concatenation, Format, ToString)
            Dim display As String = ""
            display &= "╔══════════════════════════════════════╗" & vbCrLf
            display &= "          TOP 10 HIGHSCORES             " & vbCrLf
            display &= "╚══════════════════════════════════════╝" & vbCrLf & vbCrLf

            If lines.Length = 0 Then
                display &= "No scores recorded yet!" & vbCrLf
            Else
                ' Display top 10 (or fewer if less than 10 exist)
                Dim count As Integer = Math.Min(10, lines.Length)

                For i As Integer = 0 To count - 1
                    ' Parse line (String Functions: Split, Trim)
                    Dim parts() As String = lines(i).Split("|"c)

                    If parts.Length = 2 Then
                        Dim playerName As String = parts(0).Trim()
                        Dim score As String = parts(1).Trim()

                        ' Format display (String Functions: Format, PadRight, ToString)
                        display &= String.Format("{0}. {1} - {2} pts",
                                                (i + 1).ToString(),
                                                playerName.PadRight(15),
                                                score.PadLeft(6, "0"c)) & vbCrLf
                    End If
                Next
            End If

            display &= vbCrLf & "Play to beat these scores!"

            ' Show highscores
            MessageBox.Show(display,
                           "Highscores",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error loading highscores:" & vbCrLf & ex.Message,
                           "Error",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error)
        End Try
    End Sub

    '==========================================
    ' EXIT BUTTON - Close application
    '==========================================
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        ' Confirm exit (String message!)
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to exit?",
                                                     "Exit Game",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            ' Close the entire application
            Application.Exit()
        End If
    End Sub

    '==========================================
    ' PLAY BACKGROUND MUSIC (looping)
    '==========================================
    Private Sub PlayBackgroundMusic()
        Try
            ' Path to your wav file
            Dim musicPath As String = Path.Combine(Application.StartupPath, "C:\Users\Karl Lopez\source\repos\Hangman\Hangman\Resources\bookwormost.wav")

            ' Check if file exists
            If File.Exists(musicPath) Then
                bgMusic = New SoundPlayer(musicPath)
                bgMusic.PlayLooping()
            Else
                MessageBox.Show("Background music file not found at:" & vbCrLf & musicPath,
                           "Music Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading music: " & ex.Message,
                       "Music Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    '==========================================
    ' STOP BACKGROUND MUSIC
    '==========================================
    Private Sub StopBackgroundMusic()
        If bgMusic IsNot Nothing Then
            bgMusic.Stop()
        End If
    End Sub

    '==========================================
    ' MAIN MENU LOAD - Optional welcome message
    '==========================================
    Private Sub MainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PlayBackgroundMusic()
    End Sub

End Class