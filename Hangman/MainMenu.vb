Imports System.IO
Imports System.Media

Public Class MainMenu

    '==========================================
    ' PLAYER NAME (shared with Form1)
    '==========================================
    Public Shared PlayerName As String = "Player"
    Private bgMusic As SoundPlayer
    '==========================================
    ' START GAME BUTTON - Get name and launch game
    '==========================================
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
        Dim gameForm As New Form1()
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
        instructions &= "Defeat the enemy by spelling words!" & vbCrLf & vbCrLf

        instructions &= "HOW TO PLAY:" & vbCrLf
        instructions &= "1. Click letter tiles to spell a word (minimum 3 letters)" & vbCrLf
        instructions &= "2. Watch the word display change color:" & vbCrLf
        instructions &= "   • WHITE = Still typing (less than 3 letters)" & vbCrLf
        instructions &= "   • GREEN = Valid word!" & vbCrLf
        instructions &= "   • RED = Not a real word" & vbCrLf
        instructions &= "3. Click the ATTACK button to submit your word" & vbCrLf
        instructions &= "4. Longer words = More damage!" & vbCrLf & vbCrLf

        instructions &= "SCORING:" & vbCrLf
        instructions &= "• Base damage: 5 points per letter" & vbCrLf
        instructions &= "• Rare letters (Q, Z, X, J, K) give bonus damage" & vbCrLf
        instructions &= "• 6+ letter words: +10 damage" & vbCrLf
        instructions &= "• 8+ letter words: +20 damage" & vbCrLf & vbCrLf

        instructions &= "CONTROLS:" & vbCrLf
        instructions &= "• Click letter tiles to build word" & vbCrLf
        instructions &= "• Click word display to backspace (remove last letter)" & vbCrLf
        instructions &= "• ATTACK button: Submit word and attack enemy" & vbCrLf
        instructions &= "• SCRAMBLE button: Get new random letters" & vbCrLf & vbCrLf

        instructions &= "TIPS:" & vbCrLf
        instructions &= "• Enemy attacks back after each of your attacks!" & vbCrLf
        instructions &= "• Grid refreshes after successful attacks" & vbCrLf
        instructions &= "• Watch your HP bar carefully!" & vbCrLf

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