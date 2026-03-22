<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainMenu
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        btnStartGame = New Button()
        btnH2P = New Button()
        btnhscores = New Button()
        btnExit = New Button()
        SuspendLayout()
        ' 
        ' btnStartGame
        ' 
        btnStartGame.Font = New Font("Wide Latin", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnStartGame.Location = New Point(450, 300)
        btnStartGame.Name = "btnStartGame"
        btnStartGame.Size = New Size(282, 56)
        btnStartGame.TabIndex = 0
        btnStartGame.Text = "PLAY NOW"
        btnStartGame.UseVisualStyleBackColor = True
        ' 
        ' btnH2P
        ' 
        btnH2P.Font = New Font("Wide Latin", 12F, FontStyle.Bold)
        btnH2P.Location = New Point(450, 381)
        btnH2P.Name = "btnH2P"
        btnH2P.Size = New Size(282, 56)
        btnH2P.TabIndex = 1
        btnH2P.Text = "HOW TO PLAY"
        btnH2P.UseVisualStyleBackColor = True
        ' 
        ' btnhscores
        ' 
        btnhscores.Font = New Font("Wide Latin", 12F, FontStyle.Bold)
        btnhscores.Location = New Point(450, 459)
        btnhscores.Name = "btnhscores"
        btnhscores.Size = New Size(282, 56)
        btnhscores.TabIndex = 2
        btnhscores.Text = "HIGH SCORES"
        btnhscores.UseVisualStyleBackColor = True
        ' 
        ' btnExit
        ' 
        btnExit.Font = New Font("Wide Latin", 12F, FontStyle.Bold)
        btnExit.Location = New Point(450, 531)
        btnExit.Name = "btnExit"
        btnExit.Size = New Size(282, 56)
        btnExit.TabIndex = 3
        btnExit.Text = "EXIT"
        btnExit.UseVisualStyleBackColor = True
        ' 
        ' MainMenu
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = My.Resources.Resources.image
        BackgroundImageLayout = ImageLayout.Stretch
        ClientSize = New Size(1184, 761)
        Controls.Add(btnExit)
        Controls.Add(btnhscores)
        Controls.Add(btnH2P)
        Controls.Add(btnStartGame)
        Name = "MainMenu"
        Text = "MainMenu"
        ResumeLayout(False)
    End Sub

    Friend WithEvents btnStartGame As Button
    Friend WithEvents btnH2P As Button
    Friend WithEvents btnhscores As Button
    Friend WithEvents btnExit As Button
End Class
