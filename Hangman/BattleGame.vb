Imports System.IO

Public Class BattleGame

    '==========================================
    ' DICTIONARY
    '==========================================
    Private dict As New Dictionary()

    '==========================================
    ' GAME PROPERTIES
    '==========================================
    Public PlayerHP As Integer = 100
    Public PlayerMaxHP As Integer = 100
    Public EnemyHP As Integer = 80
    Public EnemyMaxHP As Integer = 80
    Public PlayerName As String = "Lex"
    Public EnemyName As String = "Minotaur"

    '==========================================
    ' GRID AND WORD SYSTEM
    '==========================================
    Public Grid(3, 3) As String   ' 4x4 grid of letters (0-3 indices)
    Public CurrentWord As String = ""
    Public Score As Integer = 0

    '==========================================
    ' GEMS SYSTEM
    '==========================================
    Public Gems As New List(Of String)

    '==========================================
    ' RANDOM GENERATOR
    '==========================================
    Private rng As New Random()

    '==========================================
    ' CONSTRUCTOR
    '==========================================
    Public Sub New()
        GenerateGrid()
    End Sub

    '==========================================
    ' GENERATE RANDOM LETTER GRID
    '==========================================
    Public Sub GenerateGrid()
        ' Letter frequency similar to Scrabble
        ' More common letters appear more often
        Dim letters As String = "AAAAAABBCCDDDDEEEEEEEEFFGGGHHIIIIIJKLLLLMMNNNNNNOOOOOOOPPQRRRRRRSSSSSSTTTTTTUUUUVVWWXYYZ"

        For i As Integer = 0 To 3
            For j As Integer = 0 To 3
                Dim randomIndex As Integer = rng.Next(0, letters.Length)
                Grid(i, j) = letters.Substring(randomIndex, 1)
            Next
        Next
    End Sub

    '==========================================
    ' SCRAMBLE GRID (Re-randomize letters)
    '==========================================
    Public Sub ScrambleGrid()
        GenerateGrid()
        CurrentWord = ""  ' Clear current word when scrambling
    End Sub

    '==========================================
    ' VALIDATE WORD (Check if word is valid)
    ' Returns: 
    '   -2 = Not a real word (dictionary check failed)
    '   -1 = Invalid letters (letter not available in grid)
    '    0 = Too short (less than 3 letters)
    '   >0 = Valid! Returns damage amount
    '==========================================
    Public Function ValidateWord(word As String) As Integer
        ' Clean up the word (String Functions!)
        word = word.ToUpper().Trim()

        ' Check minimum length (String Function: Length)
        If word.Length < 3 Then
            Return 0  ' Too short
        End If

        ' CHECK DICTIONARY FIRST! (String Functions in Dictionary class)
        If Not dict.IsValidWord(word) Then
            Return -2  ' Not a real word
        End If

        ' Check if word only uses available letters
        ' String Functions: Join, Cast, IndexOf, Remove, ToString
        Dim tempGrid As String = String.Join("", Grid.Cast(Of String)())

        For Each c As Char In word
            Dim charStr As String = c.ToString()
            If tempGrid.IndexOf(charStr) = -1 Then
                Return -1  ' Invalid - letter not available
            End If
            ' Remove the used letter
            Dim pos As Integer = tempGrid.IndexOf(charStr)
            tempGrid = tempGrid.Remove(pos, 1)
        Next

        ' Word is valid - return damage calculation
        Return CalculateDamage(word)
    End Function

    '==========================================
    ' CALCULATE DAMAGE FROM WORD
    '==========================================
    Private Function CalculateDamage(word As String) As Integer
        Dim damage As Integer = 0

        ' Base damage: 5 points per letter (Calculation + String.Length)
        damage = word.Length * 5

        ' Bonus for rare letters (String Function: Contains)
        If word.Contains("Q") Or word.Contains("Z") Then
            damage += 15
        End If
        If word.Contains("X") Then
            damage += 10
        End If
        If word.Contains("J") Or word.Contains("K") Then
            damage += 8
        End If

        ' Bonus for long words (String.Length + Conditions)
        If word.Length >= 6 Then
            damage += 10
        End If
        If word.Length >= 8 Then
            damage += 20
        End If

        ' Bonus for words with specific patterns (String Functions: StartsWith, EndsWith)
        If word.StartsWith("RE") Then
            damage += 5
        End If
        If word.EndsWith("ED") Or word.EndsWith("ING") Then
            damage += 5
        End If

        Return damage
    End Function

    '==========================================
    ' DEAL DAMAGE TO ENEMY
    '==========================================

    Public Sub AttackEnemy(damage As Integer)
        EnemyHP -= damage
        If EnemyHP < 0 Then EnemyHP = 0

        ' Update score (Calculation)
        Score += damage * 10  ' MAKE SURE THIS LINE EXISTS!

        ' Chance to earn gem for long words (String.Length)
        If CurrentWord.Length >= 6 AndAlso rng.Next(0, 100) > 50 Then
            Dim gemType As String = GetRandomGem()
            Gems.Add(gemType)
        End If

        ' Clear current word after attack
        CurrentWord = ""
    End Sub

    '==========================================
    ' ENEMY ATTACK
    '==========================================
    Public Function EnemyAttack() As Integer
        ' Random damage between 5-20 (Calculation)
        Dim damage As Integer = rng.Next(5, 21)
        PlayerHP -= damage
        If PlayerHP < 0 Then PlayerHP = 0
        Return damage
    End Function

    '==========================================
    ' GEM FUNCTIONS
    '==========================================
    Private Function GetRandomGem() As String
        Dim gemTypes() As String = {"Fire", "Ice", "Lightning", "Heal", "Shield"}
        Return gemTypes(rng.Next(0, gemTypes.Length))
    End Function

    Public Function UseGem(gemType As String) As Integer
        ' String Function: ToLower
        Select Case gemType.ToLower()
            Case "fire"
                Return 30
            Case "ice"
                Return 25
            Case "lightning"
                Return 35
            Case "heal"
                PlayerHP = Math.Min(PlayerHP + 30, PlayerMaxHP)
                Return 0
            Case "shield"
                ' Shield logic would go here
                Return 0
            Case Else
                Return 0
        End Select
    End Function

    '==========================================
    ' GAME STATE CHECKS
    '==========================================
    Public Function IsPlayerDead() As Boolean
        Return PlayerHP <= 0
    End Function

    Public Function IsEnemyDead() As Boolean
        Return EnemyHP <= 0
    End Function

    '==========================================
    ' GET PLAYER HP AS STRING (String Functions!)
    '==========================================
    Public Function GetPlayerHPString() As String
        Return String.Format("HP: {0}/{1}", PlayerHP.ToString(), PlayerMaxHP.ToString())
    End Function

    '==========================================
    ' GET ENEMY HP AS STRING (String Functions!)
    '==========================================
    Public Function GetEnemyHPString() As String
        Return String.Format("HP: {0}/{1}", EnemyHP.ToString(), EnemyMaxHP.ToString())
    End Function

    '==========================================
    ' GET SCORE STRING (String Functions!)
    '==========================================
    Public Function GetScoreString() As String
        ' Debug: Make sure Score variable has a value
        Dim scoreValue As Integer = Score
        Dim scoreText As String = scoreValue.ToString()
        Dim paddedScore As String = scoreText.PadLeft(6, "0"c)
        Return "Score: " & paddedScore
    End Function

    '==========================================
    ' GET PLAYER HP PERCENTAGE (for progress bar)
    '==========================================
    Public Function GetPlayerHPPercent() As Integer
        Return CInt((PlayerHP / PlayerMaxHP) * 100)
    End Function

    '==========================================
    ' GET ENEMY HP PERCENTAGE (for progress bar)
    '==========================================
    Public Function GetEnemyHPPercent() As Integer
        Return CInt((EnemyHP / EnemyMaxHP) * 100)
    End Function

    '==========================================
    ' GET VALIDATION MESSAGE (String Functions!)
    '==========================================
    Public Function GetValidationMessage(resultCode As Integer) As String
        Select Case resultCode
            Case -2
                Return "NOT A VALID WORD!"
            Case -1
                Return "LETTERS NOT AVAILABLE!"
            Case 0
                Return "TOO SHORT! (Min 3 letters)"
            Case Else
                ' String Functions: Format, ToString
                Return String.Format("VALID! Damage: {0}", resultCode.ToString())
        End Select
    End Function

    '==========================================
    ' SAVE HIGHSCORE TO FILE
    '==========================================
    Public Sub SaveHighscore(playerName As String)
        Try
            Dim filePath As String = Path.Combine(Application.StartupPath, "highscores.txt")

            ' Format: PlayerName|Score (String Functions!)
            Dim entry As String = playerName & "|" & Score.ToString()

            ' Append to file
            File.AppendAllText(filePath, entry & vbCrLf)

        Catch ex As Exception
            ' Silent fail - don't interrupt game
        End Try
    End Sub

    '==========================================
    ' RESET GAME
    '==========================================
    Public Sub ResetGame()
        PlayerHP = PlayerMaxHP
        EnemyHP = EnemyMaxHP
        Score = 0
        CurrentWord = ""
        Gems.Clear()
        GenerateGrid()
    End Sub

    '==========================================
    ' CHECK IF DICTIONARY LOADED
    '==========================================
    Public Function IsDictionaryReady() As Boolean
        Return dict.IsDictionaryLoaded()
    End Function

End Class