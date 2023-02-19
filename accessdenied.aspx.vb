Partial Class Accessdenied
    Inherits Page
    Dim le As New mfa1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Me.IsPostBack = False Then
        End If
    End Sub
End Class