Imports System.IO

Public Class Dictionary

    '==========================================
    ' WORD LIST STORAGE
    '==========================================
    Private wordList As New HashSet(Of String)  ' Fast lookup
    Private isLoaded As Boolean = False

    '==========================================
    ' CONSTRUCTOR - Load dictionary on creation
    '==========================================
    Public Sub New()
        LoadDictionary()
    End Sub

    '==========================================
    ' LOAD DICTIONARY FROM FILE
    '==========================================
    Private Sub LoadDictionary()
        Try
            ' Path to dictionary.txt in same folder as exe
            Dim filePath As String = "C:\Users\Karl Lopez\source\repos\Hangman\Hangman\Resources\dictionary.txt"

            ' Check if file exists
            If Not File.Exists(filePath) Then
                MessageBox.Show("Dictionary file not found at: " & filePath & vbCrLf &
                               "Please add dictionary.txt to your project folder.",
                               "Dictionary Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Read all lines from file
            Dim lines() As String = File.ReadAllLines(filePath)

            ' Add each word to HashSet (converts to uppercase for easy comparison)
            For Each line As String In lines
                Dim word As String = line.Trim().ToUpper()

                ' Only add if word is 3+ letters (String Function: Length!)
                If word.Length >= 3 Then
                    wordList.Add(word)
                End If
            Next

            isLoaded = True

            ' Show success message with word count (String Functions!)
            MessageBox.Show("Dictionary loaded successfully!" & vbCrLf &
                           "Total words: " & wordList.Count.ToString() & vbCrLf &
                           "Minimum word length: 3 letters",
                           "Dictionary Ready", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error loading dictionary: " & ex.Message,
                           "Dictionary Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '==========================================
    ' CHECK IF WORD EXISTS (Main validation!)
    '==========================================
    Public Function IsValidWord(word As String) As Boolean
        If Not isLoaded Then Return False

        ' Clean the word (String Functions: ToUpper, Trim)
        word = word.ToUpper().Trim()

        ' Check length minimum (String Function: Length)
        If word.Length < 3 Then
            Return False
        End If

        ' Check if word exists in dictionary
        Return wordList.Contains(word)
    End Function

    '==========================================
    ' GET WORD INFO (for debugging/testing)
    '==========================================
    Public Function GetWordInfo(word As String) As String
        word = word.ToUpper().Trim()

        If IsValidWord(word) Then
            ' String Functions: Format, ToString, Length
            Return String.Format("'{0}' is VALID (Length: {1})", word, word.Length.ToString())
        Else
            Return String.Format("'{0}' is INVALID", word)
        End If
    End Function

    '==========================================
    ' GET TOTAL WORD COUNT
    '==========================================
    Public Function GetWordCount() As Integer
        Return wordList.Count
    End Function

    '==========================================
    ' CHECK IF DICTIONARY IS LOADED
    '==========================================
    Public Function IsDictionaryLoaded() As Boolean
        Return isLoaded
    End Function

End Class