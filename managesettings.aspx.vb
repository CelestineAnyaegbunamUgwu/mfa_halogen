Public Class managesettings
    Inherits System.Web.UI.Page

    Dim le As New mfa1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        le.Checksession()
        le.ReplaceText(Me)

        If Me.IsPostBack = False Then
            'Me.srcDelete()
            le.MustBe("Gmgr", "Rmgr", "", "", "", "") 'only CEO,Gmgr and RMgr
            Me.loadCred()
            'Me.lbluser.Text = le.Userfullname(le.USerName)
            'le.UserPhoto(Me.userpho, le.USerName)

        End If
    End Sub

    Private Sub loadCred()
        'If le.ReturnCheckCondition("cmstblCoy", "MailFrom", "") = True Then


        Me.txtMailHost.Text = le.ReturnConditionNA("cmstblCoy", "MailHost", le.WhereBranchCode)

        Me.txtmailfrom.Text = le.ReturnConditionNA("cmstblCoy", "MailFrom", le.WhereBranchCode)
        Me.txtPwd.Text = le.ReturnConditionNA("cmstblCoy", "MailPassword", le.WhereBranchCode)
        'Me.txtMailsubject.Text = le.ReturnConditionNA("cmstblCoy", "MailSubject", le.WhereBranchCode)
        'Me.txtMailToList.Text = le.ReturnConditionNA("cmstblCoy", "MailToList", le.WhereBranchCode)
        Me.txtCredential.Text = le.ReturnConditionNA("cmstblCoy", "MailCredential", le.WhereBranchCode)
        If le.ReturnCheckCondition("cmstblCoy", "EnableEmail", " where EnableEmail='Yes'" + le.AndBranchCode) = True Then
            Me.CheckEmail.Checked = True
        Else
            Me.CheckEmail.Checked = False
        End If

        'enable activity history
        If le.ReturnCheckCondition("cmstblCoy", "EnableActivity", " where EnableActivity='Yes'" + le.AndBranchCode) = True Then
            Me.CheckAct.Checked = True
        Else
            Me.CheckAct.Checked = False
        End If

        'Activate MFA
        If le.ReturnCheckCondition("cmstblCoy", "EnableMFA", " where EnableMFA='Yes'" + le.AndBranchCode) = True Then
            Me.Checkmfa.Checked = True
        Else
            Me.Checkmfa.Checked = False
        End If

        txtNologin.Text = le.ReturnCondition("cmstblCoy", "mfaLastloginTime", le.WhereBranchCode)
        txtresponse.Text = le.ReturnCondition("cmstblCoy", "mfaResponseTime", le.WhereBranchCode)

        activate()












    End Sub

    Private Sub activate()
        Me.btnRegister.Visible = False
        Me.btnUpdate.Visible = True
        'Me.BtnDelete.Visible = True

    End Sub
    Private Sub deactivate()
        Me.btnRegister.Visible = True
        Me.btnUpdate.Visible = False
        'Me.BtnDelete.Visible = False
        Me.CheckEmail.Checked = False
        Me.CheckAct.Checked = False
        Me.Checkmfa.Checked = False
    End Sub
    Private Sub Updated()
        Try
            le.AndCondition = ""

            Dim xTexts As String = " [MailHost] = '" & Me.txtMailHost.Text & "'," &
                       " [MailCredential] = '" & Me.txtCredential.Text & "'," &
                       " [MailFrom] = '" & Me.txtmailfrom.Text & "'," &
" [MailPassword] = '" & Me.txtPwd.Text & "'"
            le.UpdateData("cmstblCoy", xTexts, le.WhereBranchCode)
            le.lmsg = "Updated Successfully!"
            ' RegExist = False

            Me.loadCred()
            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
        Catch ex As Exception
            le.forceclosedatabase()
        End Try
    End Sub
    Protected Sub BtnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Me.Updated()
    End Sub



    Protected Sub CheckEmail_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckEmail.CheckedChanged
        Try


            Dim y As String

            If Me.CheckEmail.Checked = True Then

                'Dim Host, Mcredential, Passw, Mailfrom, Msubject, MailToList As String
                'Host = le.ReturnCondition("cmstblCoy", "MailHost", " Where EnableEmail='Yes'" & le.AndBranchCode)
                'Mcredential = le.ReturnCondition("cmstblCoy", "MailCredential", " Where EnableEmail='Yes'" & le.AndBranchCode)
                'Passw = le.ReturnCondition("cmstblCoy", "MailPassword", " Where EnableEmail='Yes'" & le.AndBranchCode)
                'Mailfrom = le.ReturnCondition("cmstblCoy", "MailFrom", " Where EnableEmail='Yes'" & le.AndBranchCode)
                'Msubject = le.ReturnCondition("cmstblCoy", "MailSubject", " Where EnableEmail='Yes'" & le.AndBranchCode)
                'MailToList = le.ReturnCondition("cmstblCoy", "MailToList", " Where EnableEmail='Yes'" & le.AndBranchCode)
                'le.sendmail(Host, Mcredential, Passw, Mailfrom, MailToList, Msubject & " (" & CType(Session("UserName"), String), "Error Configuring Emailing Credentials")

                'Catch ex As Exception
                '    le.AndCondition = " where BranchCode='" & le.BranchCode & "'"
                '    y = "No"
                '    Dim xText2 As String = " [EnableEmail] = '" & y & "'"

                '    le.UpdateData("cmstblCoy", xText2, le.AndCondition)
                '    le.lmsg = "Error With Email Configuration. So Emailing Disabled."
                '    Me.loadCred()
                '    ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)

                'End Try
                y = "Yes"
                le.AndCondition = " where BranchCode='" & le.BranchCode & "'"

                Dim xTexts As String = " [EnableEmail] = '" & y & "'"

                le.UpdateData("cmstblCoy", xTexts, le.AndCondition)
                ' le.lmsg = "Updated Successfully!"
                Me.loadCred()

            Else
                y = "No"
                Dim xTexts As String = " [EnableEmail] = '" & y & "'"

                le.UpdateData("cmstblCoy", xTexts, le.AndCondition)
                ' le.lmsg = "Updated Successfully!"
                Me.loadCred()
            End If



        Catch ex As Exception
            le.forceclosedatabase()
        End Try
    End Sub

    Protected Sub CheckAct_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckAct.CheckedChanged
        Try


            Dim y As String

            If Me.CheckAct.Checked = True Then
                y = "Yes"
            Else
                y = "No"
            End If
            le.AndCondition = ""

            Dim xTexts As String = " [EnableActivity] = '" & y & "'"

            le.UpdateData("cmstblCoy", xTexts, le.WhereBranchCode)
            'le.lmsg = "Updated Successfully!"
            Me.loadCred()
            'ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
        Catch ex As Exception
            le.forceclosedatabase()
        End Try
    End Sub


    Protected Sub Checkmfa_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Checkmfa.CheckedChanged
        Try


            Dim y As String

            If Me.Checkmfa.Checked = True Then
                y = "Yes"
            Else
                y = "No"
            End If
            le.AndCondition = ""

            Dim xTexts As String = " [EnableMFA] = '" & y & "'"

            le.UpdateData("cmstblCoy", xTexts, le.WhereBranchCode)
            'le.lmsg = "Updated Successfully!"
            Me.loadCred()
            'ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
        Catch ex As Exception
            le.forceclosedatabase()
        End Try
    End Sub
    Private Sub btnNologi_Click(sender As Object, e As EventArgs) Handles btnNologi.Click
        If Val(txtNologin.Text) > 0 Then
            le.UpdateData("cmstblCoy", "mfaLastloginTime=" & txtNologin.Text, le.WhereBranchCode)

            le.lmsg = "Updated Successfully!"
            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)

        Else
            le.lmsg = "Ebter valid number"
            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)

        End If
    End Sub

    Private Sub btnResponse_Click(sender As Object, e As EventArgs) Handles btnResponse.Click
        If Val(txtresponse.Text) > 0 Then
            le.UpdateData("cmstblCoy", "mfaResponseTime=" & txtresponse.Text, le.WhereBranchCode)

            le.lmsg = "Updated Successfully!"
            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)

        Else
            le.lmsg = "Ebter valid number"
            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)

        End If
    End Sub
End Class