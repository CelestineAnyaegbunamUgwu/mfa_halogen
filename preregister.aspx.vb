Public Class preregister
    Inherits System.Web.UI.Page

    Dim le As New mfa1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        le.PreChecksession()
        le.ReplaceText(Me)
        'Me.BtnDelete.Attributes.Add("onclick", "window.showModalDialog('ResultTemplate.aspx','mywindow','width=760,height=600,scrollbar=yes')")
        Me.BtnDelete.Attributes.Add("onclick", "return confirm('Are You Sure You Want To Delete This? ');")

        Session.Remove("dCode")
        Session("dCode") = "Halogen"
        If Me.IsPostBack = False Then

            le.ListAnyThing(Me.Dropbranchcode, "cmstblCoy", "BranchCode", " where dCode='" + CType(Session("dcode"), String) + "'")

            le.ListAnyThing(Me.dropdept, "cmstblDept", "DeptName", " where dCode='" + CType(Session("dcode"), String) + "'")




        End If
    End Sub

    Private branch As String
    Private Sub pRegister()

        'Try


        If Dropbranchcode.Text.Length > 0 Then
            branch = Dropbranchcode.Text
        Else
            le.lmsg = "You cannot register user without branch name!"
            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
            Exit Sub
        End If
        Session("BranchCode") = branch

        le.region = le.BranchCodeRegion(branch)
        Dim stat As String = "No"
        Dim xFields As String = "Username,fullname,pwd,Dept,email,phone,officeAddress,AddressofResidence,IsApproved,Recordedby,BranchCode,dcode"
        Dim xTexts As String = "'" & Me.txtnUserName.Text & "'," &
          "'" & Me.txtfullname.Text & "'," &
           "'" & Me.txtPwd.Text & "'," &
            "'" & Me.dropdept.Text & "'," &
            "'" & Me.txtnUserName.Text & "'," &
             "'" & Me.txtphone.Text & "'," &
             "'" & Me.txtOfficeAddress.Text & "'," &
             "'" & Me.txtResidentialAddress.Text & "'," &
              "'" & stat & "'," &
          "'" & txtnUserName.Text & "'," &
           "'" & branch & "'," &
          "'" & CType(Session("dcode"), String) & "'"

        le.InsertData("cmstbllog", xFields, xTexts)
        Dim coy As String = le.ReturnConditionNA("cmstblCoy", "CompanyName", le.WhereBranchCode)
        Dim coyaddress As String = le.ReturnConditionNA("cmstblCoy", "address", le.WhereBranchCode)

        Dim bodi As String = " Dear " & txtfullname.Text & ", " & vbCrLf &
        vbCrLf & "This is to inform you that your PRE-REGISTRATION for the use of S3 on Halogen server is successful!" & "." &
        vbCrLf & "The respective authorities have been alerted, and after due considerations, they will inform you via email the outcome." & "." &
        vbCrLf & vbCrLf & " Your Branch Office Code is: " & branch & "  and its full name is " & coy & " and Address is " & coyaddress & vbCrLf &
        vbCrLf & " Your username is: " & txtnUserName.Text & " " & vbCrLf &
        vbCrLf & " Your Password is: " & txtPwd.Text & " " & vbCrLf & "  This password must be changed after management approval" & vbCrLf &
        vbCrLf & " Thank you. " & vbCrLf &
              vbCrLf & vbCrLf & " Best Regards, " & vbCrLf &
               "MANAGEMENT " & vbCrLf &
       "  For: " & coy & " " & vbCrLf & ""
        le.SendEmailto(Me.txtnUserName.Text, "S3 Account Creation", bodi)
        'le.SendEmailto(le.EmailList("BMgr", " where BranchCode='" & branch & "'"), "S3 Account Creation", bodi)

        le.ActivityANDEmailLog("A new PRE-REGISTRATION by " + Me.txtfullname.Text)

        le.ClearText(Me)
        le.lmsg = "Pre-registration was sent and received successfully! Check your email now to know the next step. You will receive yet another email from the management team when approved. Thank you for registering."

        ' & vbCrLf &
        '        "They will confirm your pre-regisration and get back to you via email. " & vbCrLf &
        '"  You submitted to : " & coy & " " & vbCrLf & ""
        ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
        'Catch ex As Exception

        'End Try
    End Sub



    Protected Sub btnRegister_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        'If Me.txtnUserName.Text.Trim.Length > 0 And Me.dropdept.Text.Trim.Length > 0 Then
        If Me.txtnUserName.Text.Trim.Length > 0 Then

            If le.ReturnCheckCondition("cmstbllog", "Username", " where Username='" + Me.txtnUserName.Text.Trim + "'") = True Then
                le.lmsg = "Username Already Existing"
                ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
                Exit Sub
            Else

                Me.pRegister()


            End If
        Else
            le.lmsg = "Enter Username Name, Choose Department, and TRY AGAIN."
            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)

        End If
    End Sub



    Protected Sub BtnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClear.Click
        Response.Redirect("manageusers.aspx")
    End Sub
End Class