Imports System.IO

Public Class EnemyData
    Public Name As String
    Public HP As Integer
    Public MaxHP As Integer
    Public MinAttack As Integer
    Public MaxAttack As Integer
    Public Element As String
    Public ImageFile As String

    Public Sub New(n As String, startHP As Integer, minAtk As Integer, maxAtk As Integer, elem As String, img As String)
        Name = n
        HP = startHP
        MaxHP = startHP
        MinAttack = minAtk
        MaxAttack = maxAtk
        Element = elem
        ImageFile = img
    End Sub
End Class

Public Class BattleGame

    '==========================================
    ' DICTIONARY
    '==========================================
    Private dict As New Dictionary()

    '==========================================
    ' RANDOM GENERATOR (must be first!)
    '==========================================
    Private rng As New Random()

    '==========================================
    ' ENEMY SYSTEM
    '==========================================
    Public EnemyRoster As List(Of EnemyData)
    Public CurrentEnemy As EnemyData
    Public EnemyKillCount As Integer = 0

    Public Property EnemyHP As Integer
        Get
            Return CurrentEnemy.HP
        End Get
        Set(value As Integer)
            CurrentEnemy.HP = value
        End Set
    End Property

    Public Property EnemyMaxHP As Integer
        Get
            Return CurrentEnemy.MaxHP
        End Get
        Set(value As Integer)
            CurrentEnemy.MaxHP = value
        End Set
    End Property

    Public Property EnemyName As String
        Get
            Return CurrentEnemy.Name
        End Get
        Set(value As String)
            CurrentEnemy.Name = value
        End Set
    End Property

    '==========================================
    ' GAME PROPERTIES
    '==========================================
    Public PlayerHP As Integer = 100
    Public PlayerMaxHP As Integer = 100
    Public PlayerName As String = "Lex"

    '==========================================
    ' GRID AND WORD SYSTEM
    '==========================================
    Public Grid(3, 3) As String
    Public CurrentWord As String = ""
    Public Score As Integer = 0
    Public Gold As Integer = 0

    '==========================================
    ' ELEMENT TILE SYSTEM
    '==========================================
    Public GridElements(3, 3) As String  ' stores element per tile
    Private ReadOnly ElementList() As String = {"Fire", "Water", "Nature", "Lightning", "Dark", "Normal"}

    ' Effectiveness chart
    Public Function GetStackedEffectiveness(selectedPositions As List(Of Point), enemyElement As String) As Double
        Dim effectiveCount As Integer = 0
        Dim weakCount As Integer = 0

        For Each pos As Point In selectedPositions
            Dim tileElem As String = GridElements(pos.X, pos.Y)
            If tileElem = "Normal" Then Continue For

            Dim flat As Double = GetEffectiveness(tileElem, enemyElement)
            If flat >= 2.0 Then
                effectiveCount += 1
            ElseIf flat <= 0.5 Then
                weakCount += 1
            End If
        Next

        ' Effective stacking
        If effectiveCount >= 3 Then Return 2.0
        If effectiveCount = 2 Then Return 1.5
        If effectiveCount = 1 Then Return 1.2

        ' Weak stacking
        If weakCount >= 3 Then Return 0.5
        If weakCount = 2 Then Return 0.6
        If weakCount = 1 Then Return 0.8

        Return 1.0 ' neutral
    End Function

    ' Keep original for single tile reference
    Public Function GetEffectiveness(attackElement As String, enemyElement As String) As Double
        Select Case attackElement.ToLower()
            Case "fire"
                If enemyElement = "Nature" Then Return 2.0
                If enemyElement = "Water" Then Return 0.5
            Case "water"
                If enemyElement = "Fire" Then Return 2.0
                If enemyElement = "Lightning" Then Return 0.5
            Case "nature"
                If enemyElement = "Water" Then Return 2.0
                If enemyElement = "Fire" Then Return 0.5
            Case "lightning"
                If enemyElement = "Water" Then Return 2.0
                If enemyElement = "Nature" Then Return 0.5
            Case "dark"
                If enemyElement = "Lightning" Then Return 2.0
                If enemyElement = "Normal" Then Return 0.5
        End Select
        Return 1.0
    End Function

    '==========================================
    ' GEMS SYSTEM
    '==========================================
    Public Gems As New List(Of String)

    '==========================================
    ' POWERUP STATE
    '==========================================
    Public ShieldActive As Boolean = False

    '==========================================
    ' CONSTRUCTOR
    '==========================================
    Public Sub New()
        EnemyRoster = New List(Of EnemyData) From {
    New EnemyData("Green Slime", 60, 3, 8, "Nature", "Resources\EnemyPNG\Slime.png"),
    New EnemyData("Red Slime", 75, 5, 10, "Fire", "Resources\EnemyPNG\Slime.png"),
    New EnemyData("Blue Slime", 75, 5, 10, "Water", "Resources\EnemyPNG\Slime.png"),
    New EnemyData("Zombie", 90, 8, 13, "Dark", "Resources\EnemyPNG\Zombie.png"),
    New EnemyData("Golem", 130, 10, 18, "Nature", "Resources\EnemyPNG\Golem.png"),
    New EnemyData("Minotaur", 150, 13, 22, "Fire", "Resources\EnemyPNG\Minotaur.png"),
    New EnemyData("Evil Duck", 170, 15, 25, "Water", "Resources\EnemyPNG\EvilDuck.png"),
    New EnemyData("Shadow Lord", 220, 18, 35, "Dark", "Resources\EnemyPNG\ShadowLord.png")
}
        LoadNextEnemy()
        GenerateGrid()
    End Sub

    '==========================================
    ' LOAD NEXT ENEMY
    '==========================================
    Public Sub LoadNextEnemy()
        ' Sequential — EnemyKillCount starts at 0, first call makes it index 0
        Dim index As Integer = EnemyKillCount Mod EnemyRoster.Count

        ' After full loop, scale up difficulty
        Dim loopCount As Integer = EnemyKillCount \ EnemyRoster.Count
        Dim bonusHP As Integer = loopCount * 50
        Dim bonusAtk As Integer = loopCount * 10

        Dim picked As EnemyData = EnemyRoster(index)

        CurrentEnemy = New EnemyData(
        picked.Name,
        picked.MaxHP + bonusHP,
        picked.MinAttack + bonusAtk,
        picked.MaxAttack + bonusAtk,
        picked.Element,
        picked.ImageFile
    )

        EnemyKillCount += 1
    End Sub

    '==========================================
    ' BUY POWERUPS
    '==========================================
    Public Function BuyPotion() As String
        If Gold < 50 Then
            Return "NOT ENOUGH GOLD! (Need 50g)"
        End If
        Gold -= 50
        PlayerHP = Math.Min(PlayerHP + 40, PlayerMaxHP)
        Return "POTION USED! +40 HP restored."
    End Function

    Public Function BuyShield() As String
        If Gold < 80 Then
            Return "NOT ENOUGH GOLD! (Need 80g)"
        End If
        If ShieldActive Then
            Return "SHIELD ALREADY ACTIVE!"
        End If
        Gold -= 80
        ShieldActive = True
        Return "SHIELD ACTIVE! Next attack blocked."
    End Function

    '==========================================
    ' GENERATE RANDOM LETTER GRID
    '==========================================
    Public Sub GenerateGrid()
        Dim letters As String = "AAAAAABBCCDDDDEEEEEEEEFFGGGHHIIIIIJKLLLLMMNNNNNNOOOOOOOPPQRRRRRRSSSSSSTTTTTTUUUUVVWWXYYZ"

        ' Reset all tiles to Normal first
        For i As Integer = 0 To 3
            For j As Integer = 0 To 3
                Dim randomIndex As Integer = rng.Next(0, letters.Length)
                Grid(i, j) = letters.Substring(randomIndex, 1)
                GridElements(i, j) = "Normal"
            Next
        Next

        ' Place 4-6 random colored element tiles
        Dim coloredTiles As Integer = rng.Next(4, 7)
        Dim placedTiles As Integer = 0
        Dim activeElements() As String = {"Fire", "Water", "Nature", "Lightning", "Dark"}

        Do While placedTiles < coloredTiles
            Dim ri As Integer = rng.Next(0, 4)
            Dim rj As Integer = rng.Next(0, 4)
            If GridElements(ri, rj) = "Normal" Then
                GridElements(ri, rj) = activeElements(rng.Next(0, activeElements.Length))
                placedTiles += 1
            End If
        Loop
    End Sub

    '==========================================
    ' SCRAMBLE GRID
    '==========================================
    Public Sub ScrambleGrid()
        GenerateGrid()
        CurrentWord = ""
    End Sub

    ' Penalized scramble (called from button)
    Public Function PenalizedScramble() As Integer
        Dim penalty As Integer = 5
        PlayerHP -= penalty
        If PlayerHP < 0 Then PlayerHP = 0
        ScrambleGrid()
        Return penalty
    End Function

    '==========================================
    ' VALIDATE WORD
    '==========================================
    Public Function ValidateWord(word As String) As Integer
        word = word.ToUpper().Trim()

        If word.Length < 3 Then
            Return 0
        End If

        If Not dict.IsValidWord(word) Then
            Return -2
        End If

        Dim tempGrid As String = String.Join("", Grid.Cast(Of String)())

        For Each c As Char In word
            Dim charStr As String = c.ToString()
            If tempGrid.IndexOf(charStr) = -1 Then
                Return -1
            End If
            Dim pos As Integer = tempGrid.IndexOf(charStr)
            tempGrid = tempGrid.Remove(pos, 1)
        Next

        Return CalculateDamage(word)
    End Function

    '==========================================
    ' CALCULATE DAMAGE
    '==========================================
    Private Function CalculateDamage(word As String, Optional elementMultiplier As Double = 1.0) As Integer
        Dim damage As Integer = 0

        damage = word.Length * 5

        If word.Contains("Q") Or word.Contains("Z") Then damage += 15
        If word.Contains("X") Then damage += 10
        If word.Contains("J") Or word.Contains("K") Then damage += 8
        If word.Length >= 6 Then damage += 10
        If word.Length >= 8 Then damage += 20
        If word.StartsWith("RE") Then damage += 5
        If word.EndsWith("ED") Or word.EndsWith("ING") Then damage += 5

        ' Apply element multiplier
        damage = CInt(damage * elementMultiplier)

        Return damage
    End Function

    '==========================================
    ' DETECT WORD ELEMENT FROM SELECTED TILES
    '==========================================
    Public Function GetWordElement(selectedPositions As List(Of Point)) As String
        If selectedPositions.Count = 0 Then Return "Normal"

        Dim firstElem As String = GridElements(selectedPositions(0).X, selectedPositions(0).Y)
        If firstElem = "Normal" Then Return "Normal"

        ' Check if ALL selected tiles share the same non-Normal element
        For Each pos As Point In selectedPositions
            If GridElements(pos.X, pos.Y) <> firstElem Then
                Return "Normal"  ' Mixed = no bonus
            End If
        Next

        Return firstElem  ' All same element!
    End Function

    '==========================================
    ' VALIDATE WORD WITH ELEMENT
    '==========================================
    Public Function ValidateWordWithElement(word As String, selectedPositions As List(Of Point)) As Tuple(Of Integer, String, Double)
        word = word.ToUpper().Trim()

        If word.Length < 3 Then Return Tuple.Create(0, "Normal", 1.0)
        If Not dict.IsValidWord(word) Then Return Tuple.Create(-2, "Normal", 1.0)

        Dim tempGrid As String = String.Join("", Grid.Cast(Of String)())
        For Each c As Char In word
            Dim charStr As String = c.ToString()
            If tempGrid.IndexOf(charStr) = -1 Then Return Tuple.Create(-1, "Normal", 1.0)
            Dim pos As Integer = tempGrid.IndexOf(charStr)
            tempGrid = tempGrid.Remove(pos, 1)
        Next

        ' Use stacked multiplier
        Dim multiplier As Double = GetStackedEffectiveness(selectedPositions, CurrentEnemy.Element)

        ' Dominant element for display
        Dim wordElement As String = GetWordElement(selectedPositions)
        Dim damage As Integer = CalculateDamage(word, multiplier)

        Return Tuple.Create(damage, wordElement, multiplier)
    End Function


    '==========================================
    ' ATTACK ENEMY
    '==========================================
    Public Sub AttackEnemy(damage As Integer)
        CurrentEnemy.HP -= damage
        If CurrentEnemy.HP < 0 Then CurrentEnemy.HP = 0

        Score += damage * 10

        ' Earn gold per attack based on word length
        Gold += Math.Max(1, CurrentWord.Length * 2)

        If CurrentWord.Length >= 6 AndAlso rng.Next(0, 100) > 50 Then
            Dim gemType As String = GetRandomGem()
            Gems.Add(gemType)
        End If

        CurrentWord = ""
    End Sub

    Public Function GetEnemyKillGold() As Integer
        ' More gold for harder enemies
        Select Case CurrentEnemy.Name
            Case "Green Slime" : Return 20
            Case "Red Slime" : Return 25
            Case "Blue Slime" : Return 25
            Case "Zombie" : Return 35
            Case "Golem" : Return 50
            Case "Minotaur" : Return 60
            Case "Evil Duck" : Return 75
            Case "Shadow Lord" : Return 100
            Case Else : Return 30
        End Select
    End Function
    '==========================================
    ' ENEMY ATTACK
    '==========================================
    Public Function EnemyAttack() As Integer
        If ShieldActive Then
            ShieldActive = False
            Return -1
        End If

        Dim damage As Integer = rng.Next(CurrentEnemy.MinAttack, CurrentEnemy.MaxAttack + 1)
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
        Return CurrentEnemy.HP <= 0
    End Function

    '==========================================
    ' STRING DISPLAY FUNCTIONS
    '==========================================
    Public Function GetPlayerHPString() As String
        Return String.Format("HP: {0}/{1}", PlayerHP.ToString(), PlayerMaxHP.ToString())
    End Function

    Public Function GetEnemyHPString() As String
        Return String.Format("HP: {0}/{1}", CurrentEnemy.HP.ToString(), CurrentEnemy.MaxHP.ToString())
    End Function

    Public Function GetScoreString() As String
        Dim paddedScore As String = Score.ToString().PadLeft(6, "0"c)
        Return "Score: " & paddedScore
    End Function

    Public Function GetPlayerHPPercent() As Integer
        Return CInt((PlayerHP / PlayerMaxHP) * 100)
    End Function

    Public Function GetEnemyHPPercent() As Integer
        If CurrentEnemy.MaxHP = 0 Then Return 0
        Return CInt((CurrentEnemy.HP / CurrentEnemy.MaxHP) * 100)
    End Function

    Public Function GetValidationMessage(resultCode As Integer) As String
        Select Case resultCode
            Case -2
                Return "NOT A VALID WORD!"
            Case -1
                Return "LETTERS NOT AVAILABLE!"
            Case 0
                Return "TOO SHORT! (Min 3 letters)"
            Case Else
                Return String.Format("VALID! Damage: {0}", resultCode.ToString())
        End Select
    End Function

    '==========================================
    ' SAVE HIGHSCORE
    '==========================================
    Public Sub SaveHighscore(playerName As String)
        Try
            Dim filePath As String = Path.Combine(Application.StartupPath, "highscores.txt")
            Dim entry As String = playerName & "|" & Score.ToString()
            File.AppendAllText(filePath, entry & vbCrLf)
        Catch ex As Exception
        End Try
    End Sub

    '==========================================
    ' RESET GAME
    '==========================================
    Public Sub ResetGame()
        PlayerHP = PlayerMaxHP
        EnemyKillCount = 0
        Score = 0
        Gold = 0
        CurrentWord = ""
        Gems.Clear()
        ShieldActive = False
        LoadNextEnemy()
        GenerateGrid()
    End Sub

    '==========================================
    ' DICTIONARY CHECK
    '==========================================
    Public Function IsDictionaryReady() As Boolean
        Return dict.IsDictionaryLoaded()
    End Function

End Class