Partial Class Authenticate
    Inherits Page
    Dim le As New mfa1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        le.Checksession()
        le.ReplaceText(Me)
        Me.btnResend.Attributes.Add("onclick", "return confirm('Are You Sure You Want a RESEND of the code? ');")
        Me.Btnphone.Attributes.Add("onclick", "return confirm('Are You Sure You Want to get validated via phone? ');")

        If Me.IsPostBack = False Then
            If Not Session.Item("xCode") Is Nothing Then

                Me.lblcoy.Text = le.ReturnConditionNA("cmstblCoy", "CompanyName", le.WhereBranchCode)
                Me.lblemail.Text = le.UsernameEMAIL(le.USerName)
                Me.lbluser.Text = le.Userfullname(le.USerName)
                le.BranchLogo(Imgbtnlogo, le.BranchCode)

                Dim phon As String = le.ReturnConditionNA("cmstbllog", "phone", le.WhereBranchCode & " and username='" & le.USerName & "'")

                Btnphone.Text = " Alternatively, get validated using your phone number " & phon.ToString
                Dim minu As Integer = le.ReturnFromQuery("select mfaResponseTime from cmstblcoy where BranchCode='" & le.BranchCode & "'")

                le.SendEmailto(le.USerName, "Halogen Sandbox Account Authentication", "Here is your login validation code as: " & le.xCode & "  Note that this code expires after " & minu.ToString & " minutes.")

                'ClientScript.RegisterStartupScript(Me.GetType(), "MachineInfo", "GetInfo();", True)
                ' ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.cuIP.ToString & "');", True)


            Else
                Response.Redirect("login.aspx")
            End If

        End If
    End Sub

    Private Sub btnAuthenticate_Click(sender As Object, e As EventArgs) Handles btnAuthenticate.Click
        'ensure that something is there
        If txtCode.Text.Length > 0 Then
            'get the last incompleted insert
            Dim lastIncompletedMailCode = le.userlastInCompletedCode(le.USerName)
            'check if the allowd minutes have elapsed
            'No of minutes to wait set
            Dim mfaminutes As Integer
            mfaminutes = le.ReturnFromQuery("select mfaResponseTime from cmstblcoy where BranchCode='" & le.BranchCode & "'")

            'check the exact time in minutes when the code was sent
            Dim userlastminute As Date = le.userlastInCompletedMinutethecodewassent(le.USerName)

            'check if he is responding on time
            If DateDiff(DateInterval.Minute, userlastminute, Now) < mfaminutes Then
                'Go ahead to check if he got the exact email code
                If le.userlastInCompletedCode(le.USerName).Trim = txtCode.Text.Trim Then
                    'log him in, but complete the status first
                    le.UpdateData("tblfma", "status='Completed'", " where Id=" & le.userlastInCompletedCode_ID(le.USerName))
                    Response.Redirect("Aboutintranet.aspx")
                Else
                    'wrong email code
                    le.lmsg = "Invalid Code. Try again."
                    ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
                    Exit Sub
                End If
            Else
                le.lmsg = "Code has expired. Please login again."
                ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
                Exit Sub
            End If


        End If
    End Sub



    Private Sub Btngo_Click(sender As Object, e As EventArgs) Handles Btngo.Click
        btnAuthenticate_Click(sender, e)
    End Sub

    Private Sub Btnphone_Click(sender As Object, e As EventArgs) Handles Btnphone.Click
        'check if he has valid phone number
        Dim phon As String = le.ReturnConditionNA("cmstbllog", "phone", " where username='" & le.USerName & "'")
        If phon.ToString.Length = 13 And phon.Contains("+") = False Then
            'the phone is ok
            'InsertintotblmfaWithOUTCompletePHONE() No need for this. Just send the already generated emailcode to phone
            le.SendSMS(phon, le.xCode.ToString)

            le.lmsg = "Code successfully sent to your mobile phone number" & phon.ToString
            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)

        Else
            'your phone format is wrong
            le.lmsg = "Invalid Phone number format. It should be like +2348180000000 without spaces, and must begin with +"
            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
            Exit Sub
        End If
    End Sub

    Private Sub btnResend_Click(sender As Object, e As EventArgs) Handles btnResend.Click
        le.Checksession()

        Dim mfaminutes As Integer
        mfaminutes = le.ReturnFromQuery("select mfaResponseTime from cmstblcoy where BranchCode='" & le.BranchCode & "'")

        'check the exact time in minutes when the code was sent
        Dim userlastminute As Date = le.userlastInCompletedMinutethecodewassent(le.USerName)


        If DateDiff(DateInterval.Minute, userlastminute, Now) < mfaminutes Then
            'Go ahead to resend another email code
            InsertintotblmfaWithOUTComplete()
            Dim minu As Integer = le.ReturnFromQuery("select mfaResponseTime from cmstblcoy where BranchCode='" & le.BranchCode & "'")

            le.SendEmailto(le.USerName, "Halogen Sandbox Account Authentication", "Here is your login RESENT validation code as: " & le.xCode & "  Note that this code expires after " & minu.ToString & " minutes.")
            le.lmsg = "Code successfully resent to " & le.USerName
            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)

        Else
            le.lmsg = "Code has expired. Please login again."
            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
            Exit Sub
        End If
    End Sub

    Private Sub InsertintotblmfaWithOUTComplete()
        le.xCode = le.GetRandom(1, 1000000).ToString
        Session.Remove("xCode")
        Session("xCode") = le.xCode

        Dim xFields As String = "Username,emailCode,Device,LocationIP,Browser,BranchCode,dcode"
        Dim xTexts As String = "'" & le.USerName & "'," &
          "'" & le.xCode & "'," &
           "'" & le.mypc & "'," &
            "'" & le.cuIP & "'," &
            "'" & HttpContext.Current.Request.Browser.Browser & "'," &
           "'" & le.BranchCode & "'," &
          "'" & le.dCode & "'"

        le.InsertData("tblfma", xFields, xTexts)


    End Sub

    'Private Sub InsertintotblmfaWithOUTCompletePHONE()

    '    le.smsCode = le.GetRandom(1, 1000000).ToString
    '    Session.Remove("smsCode")
    '    Session("smsCode") = le.smsCode

    '    Dim xFields As String = "Username,smsCode,Device,LocationIP,Browser,BranchCode,dcode"
    '    Dim xTexts As String = "'" & le.USerName & "'," &
    '      "'" & le.xCode & "'," &
    '       "'" & le.mypc & "'," &
    '        "'" & le.cuIP & "'," &
    '        "'" & HttpContext.Current.Request.Browser.Browser & "'," &
    '       "'" & le.BranchCode & "'," &
    '      "'" & le.dCode & "'"

    '    le.InsertData("tblfma", xFields, xTexts)
    '    le.SendSMS(phon, le.smsCode)

    '    Response.Redirect("Authenticatesms.aspx")

    'End Sub
End Class