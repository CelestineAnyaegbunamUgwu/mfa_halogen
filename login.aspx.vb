
Partial Class login
    Inherits Page
    Dim le As New mfa1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        le.PreChecksession()
        If Not IsPostBack Then
            Session.Clear()

        End If
    End Sub
    Private bran As String = "Halogen"
    Private Sub btnlogin_Click(sender As Object, e As EventArgs) Handles btnlogin.Click



        If le.IsJohn(username.Text) = True Then


            Session.Remove("Username")

            Session("Username") = Me.username.Text.Trim.ToString

            le.USerName = Me.username.Text.Trim.ToString

            Session.Remove("dCode")

            Session("dCode") = bran



            Session.Remove("AnddCode")

            Session("AnddCode") = " and dCode = '" + bran + "'"


            Session.Remove("wheredCode")

            Session("wheredCode") = " where dCode = '" + bran + "'"

            'if exist any branch, go to choose branch
            'else
            'go to create branch
            If le.ReturnCheckCondition("cmstblcoy", "BranchCode", " where dCode='" & bran & "'") = True Then
                Response.Redirect("choosebranch.aspx")
            Else
                Response.Redirect("managebranch.aspx")
            End If


            Exit Sub


        Else
            'it is not super userKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
            le.AndCondition = " where Username='" + username.Text.Trim + "' And pwd='" + password.Text.Trim + "' and dCode='" + bran + "' and IsApproved='Yes'"
            If le.ReturnCheckCondition("cmstbllog", "Username", le.AndCondition) = True Or le.ReturnCheckCondition("cmstbljohn", "Username", " where Username='" + username.Text.Trim + "'") = True Then



                'It is now established that theuser is geniun
                'let us know his branch
                Dim mybranch As String = le.ReturnConditionNA("cmstbllog", "BranchCode", " where Username='" + username.Text.Trim + "'" & " and dCode='" + bran + "'")

                'CHECK LAST LOGINSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS
                'check the date of last login
                'check the maximum hours set for returning users to pass through MFA

                'check the device from which he logged in last
                'check the ip address of the modem he used; this will give insight of distant relocation

                'It    
                'SESSIONSLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL
                Session.Remove("UserName")
                Session("UserName") = Me.username.Text.Trim.ToString
                le.USerName = Me.username.Text.Trim

                Session.Remove("dCode")
                Session("dCode") = bran
                le.dCode = bran
                Session.Remove("AnddCode")
                Session("AnddCode") = " and dCode = '" + bran + "'"
                Session.Remove("wheredCode")
                Session("wheredCode") = " where dCode = '" + bran + "'"

                'UPDATE UDATE DATE

                'just login
                'get my branch

                Session.Remove("BranchCode")

                Session("BranchCode") = mybranch
                le.BranchCode = mybranch



                Session.Remove("AndBranchCode")

                Session("AndBranchCode") = " and BranchCode = '" + mybranch + "'"
                le.AndBranchCode = " and BranchCode = '" + mybranch + "'"



                Session.Remove("whereBranchCode")

                Session("whereBranchCode") = " where BranchCode = '" + mybranch + "'"
                le.WhereBranchCode = " where BranchCode = '" + mybranch + "'"
                Session.Remove("region")
                Session("region") = le.ReturnConditionNA("cmstblcoy", "region", le.WhereBranchCode)
                le.region = le.ReturnConditionNA("cmstblcoy", "region", le.WhereBranchCode)

                Dim xTexts As String = " LastLoginDate =" + le.DH + Now + le.DH
                le.UpdateData("cmstbllog", xTexts, " where Username='" + Me.username.Text + "'")
                le.ActivityANDEmailLog(" successfully logged in  ")
                'LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL

                If le.IsMFARequired(username.Text, mybranch) = True Then
                    'insert login info to tblmfa without completed
                    InsertintotblmfaWithOUTComplete()

                    'send email
                    'take him to mfa page for validate

                    Response.Redirect("Authenticate.aspx")
                Else
                    'insert login details to tblmfa with completed info
                    InsertintotblmfawithComplete()
                    Response.Redirect("aboutintranet.aspx")
                End If



            Else

                le.ActivityANDEmailLog(" Entered Wrong Username and/or password.(Entered: USERNAME:" & Me.username.Text & " ; and Password: " & Me.password.Text & ")")
                le.ClearText(Me)
                le.lmsg = "Wrong Username and/or password."
                ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg & "');", True)

            End If

            'KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK


        End If




    End Sub

    Private Sub InsertintotblmfaWithOUTComplete()

        le.xCode = le.GetRandom(1, 1000000).ToString
        Session.Remove("xCode")
        Session("xCode") = le.xCode

        Dim xFields As String = "Username,emailCode,Device,LocationIP,Browser,BranchCode,dcode"
        Dim xTexts As String = "'" & Me.username.Text & "'," &
          "'" & le.xCode & "'," &
           "'" & le.mypc & "'," &
            "'" & le.cuIP & "'," &
            "'" & HttpContext.Current.Request.Browser.Browser & "'," &
           "'" & le.BranchCode & "'," &
          "'" & le.dCode & "'"

        le.InsertData("tblfma", xFields, xTexts)

        'send email
    End Sub



    Private Sub InsertintotblmfawithComplete()
        Dim sta As String = "Completed"
        Dim xFields As String = "Username,emailCode,Device,LocationIP,Browser,status,BranchCode,dcode"
        Dim xTexts As String = "'" & Me.username.Text & "'," &
          "'" & le.GetRandom(1, 1000000).ToString & "'," &
           "'" & le.mypc & "'," &
            "'" & le.cuIP & "'," &
            "'" & HttpContext.Current.Request.Browser.Browser & "'," &
           "'" & sta & "'," &
            "'" & CType(Session("BranchCode"), String) & "'," &
          "'" & CType(Session("dcode"), String) & "'"

        le.InsertData("tblfma", xFields, xTexts)
    End Sub

End Class