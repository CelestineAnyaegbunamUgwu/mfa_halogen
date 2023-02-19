Public Class changepassword
    Inherits System.Web.UI.Page

    Dim le As New mfa1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        le.Checksession()
        le.ReplaceText(Me)
        'Me.BtnDelete.Attributes.Add("onclick", "window.showModalDialog('ResultTemplate.aspx','mywindow','width=760,height=600,scrollbar=yes')")
        'Me.btnUpdate.Attributes.Add("onclick", "return confirm('Are You Sure You Want To change ? ');")

        If Me.IsPostBack = False Then
            le.ActivityANDEmailLog("Visited  Password Change page. ")
            If le.IsJohn(le.USerName) = True Then
                'disable txts
                btnUpdate.Enabled = False
            End If

            'le.lmsg = "this " & le.ReturnConditionNA("cmstbllog", "pwd", " where username='" & le.USerName & "'").ToString
            'ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
            'Exit Sub

        End If
    End Sub

    Private branch As String

    Private Sub Updated()
        Try


            le.AndCondition = " where [username] = '" & le.USerName & "'" ' + le.AndBranchCode ' and dCode='" + CType(Session("dCode"), String) + "'"

            Dim xTexts As String = " [pwd] = '" & Me.txtNewPassword.Text & "'"


            le.UpdateData("cmstbllog", xTexts, le.AndCondition)
            le.lmsg = "Password Changed Successfully!"
            ' RegExist = False
            le.ActivityANDEmailLog("Successfully changed password ")

            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)


        Catch ex As Exception
            le.forceclosedatabase()
        End Try
    End Sub
    Protected Sub BtnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If txtoldpassword.Text.Trim.Length > 0 And txtNewPassword.Text.Trim.Length > 0 And txtRepeatNewPassword.Text.Trim.Length > 0 Then
            If txtNewPassword.Text.Trim = txtRepeatNewPassword.Text.Trim Then

                'ppppppppppppppppppppppppppppppppppppppppp
                If txtoldpassword.Text.Trim = le.ReturnConditionNA("cmstbllog", "pwd", " where username='" & le.USerName & "'").ToString.Trim Then
                    'change
                    Updated()
                Else
                    le.lmsg = "Wrong Password!"
                    le.ActivityANDEmailLog("Enter wrong password during password change ")
                    ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
                    Exit Sub
                End If
                'pppppppppppppppppppppppppppppppppppppp
            Else
                'the passwords are not same
                le.lmsg = "The Passwords are not same!"
            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
            Exit Sub
        End If


        Else
                le.lmsg = "Enter values for all fields!"
            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
            Exit Sub

        End If

    End Sub


    Protected Sub BtnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClear.Click
        Response.Redirect("changepassword.aspx")
    End Sub
End Class