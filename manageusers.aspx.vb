Public Class manageusers
    Inherits System.Web.UI.Page

    Dim le As New mfa1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        le.Checksession()
        le.ReplaceText(Me)
        'Me.BtnDelete.Attributes.Add("onclick", "window.showModalDialog('ResultTemplate.aspx','mywindow','width=760,height=600,scrollbar=yes')")
        Me.BtnDelete.Attributes.Add("onclick", "return confirm('Are You Sure You Want To Delete This? ');")

        If Me.IsPostBack = False Then
            Me.srcDelete()
            le.MustBe("Gmgr", "Rmgr", "BMgr", "", "", "") 'only CEO,Gmgr and RMgr
            le.ActivityANDEmailLog("Visited User registration page. ")
            ''If le.IsRoleBOOL("Gmgr", "", "", "", "", "") = True Then
            ''    Me.Dropbranchcode.Visible = True
            ''End If
            '  le.ListAnyThing(Me.Dropbranchcode, "cmstblCoy", "BranchCode", " where dCode='" + CType(Session("dcode"), String) + "'")

            le.ListAnyThing(Me.dropdept, "cmstblDept", "DeptName", " where dCode='" + CType(Session("dcode"), String) + "'")

            'le.lmsg = "Yo name" & CType(Session("dcode"), String)
            'ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
            'Exit Sub


        End If
    End Sub

    'Private branch As String = CType(Session("Branchcode"), String)
    Private Sub pRegister()

        'Try


        'If le.IsRoleBOOL("Gmgr", "", "", "", "", "") = True Then
        '    If Dropbranchcode.Text.Length > 0 Then
        '        branch = Dropbranchcode.Text
        '    Else
        '        le.lmsg = "You cannot register user without branch name!"
        '        ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
        '        Exit Sub
        '    End If
        'Else
        '    branch = CType(Session("Branchcode"), String)
        'End If

        Dim xFields As String = "Username,fullname,pwd,Dept,email,phone,officeAddress,AddressofResidence,IsApproved,Recordedby,BranchCode,dcode"
        Dim xTexts As String = "'" & Me.txtnUserName.Text & "'," &
          "'" & Me.txtfullname.Text & "'," &
           "'" & Me.txtPwd.Text & "'," &
            "'" & Me.dropdept.Text & "'," &
            "'" & Me.txtnUserName.Text & "'," &
             "'" & Me.txtphone.Text & "'," &
             "'" & Me.txtOfficeAddress.Text & "'," &
             "'" & Me.txtResidentialAddress.Text & "'," &
             "'" & Me.DropIsApproved.Text & "'," &
          "'" & CType(Session("UserName"), String) & "'," &
           "'" & CType(Session("Branchcode"), String) & "'," &
          "'" & CType(Session("dcode"), String) & "'"

        le.InsertData("cmstbllog", xFields, xTexts)
        Dim coy As String = le.ReturnConditionNA("cmstblCoy", "CompanyName", le.WhereBranchCode)
        Dim coyaddress As String = le.ReturnConditionNA("cmstblCoy", "address", le.WhereBranchCode)

        Dim bodi As String = " Dear " & le.Userfullname(txtnUserName.Text) & ", " & vbCrLf &
        vbCrLf & "This is to inform you that a new account creation for the use of S3 on Halogen server is successful!" & "." &
        vbCrLf & vbCrLf & " Your Branch Office Code is: " & le.BranchCode & "  and its full name is " & coy & " and Address is " & coyaddress & vbCrLf &
        vbCrLf & " Your username is: " & txtnUserName.Text & " " & vbCrLf &
        vbCrLf & " Your Password is: " & txtPwd.Text & " " & vbCrLf & "  Endavor to login and change this password immediately." & vbCrLf &
        vbCrLf & " The S3 login URL is: https://halogen.veracelservers.com " & vbCrLf &
        vbCrLf & " Thank you, and warm welcome onboard. " & vbCrLf &
              vbCrLf & vbCrLf & " Best Regards, " & vbCrLf &
            vbCrLf & "" & le.Userfullname(le.USerName) & " " & vbCrLf &
               "REGISTRANT " & vbCrLf &
       "  For: " & coy & " " & vbCrLf & ""
        le.SendEmailto(Me.txtnUserName.Text, "S3 Account Creation", bodi)
        le.ActivityANDEmailLog("Registered a new  user by name  called " + Me.txtfullname.Text)
        Me.srcDelete()
        le.ClearText(Me)
        le.lmsg = "Saved Successfully!"
        ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub srcDelete()
        'Try
        '' Dim xFields As String = "[ID],[Drug] as [DISPENSED DRUGS],[Qty] as [QTY],[Username],[ARID],[SalesID] as [DSP ID],[Sprice] as [Username PRICE (N)],(Qty * Sprice) as [PRICE (N)]"
        Dim xFields As String = "Username as [User],fullname as [Full Name],IsApproved"

        'If le.IsJohn(le.USerName) = True Then
        '    le.AndCondition = " where dCode='" + CType(Session("dcode"), String) + "'"
        'Else
        '    le.AndCondition = " where BranchCode='" + le.BranchCode + "'"
        'End If

        le.AndCondition = " where BranchCode='" + le.BranchCode + "'"

        le.DT = le.ReportSelectedDataTable("cmstbllog", xFields, le.AndCondition)
        Me.GridUser.DataSource = le.DT
        Me.GridUser.DataBind()
        Dim ISapp As String

        If GridUser.Rows.Count > 0 Then
            For i As Integer = 0 To GridUser.Rows.Count - 1
                ISapp = le.DT.Rows(i)("IsApproved")
                If ISapp = "No" Then
                    GridUser.Rows(i).BackColor = Drawing.Color.LightPink
                    'Else
                    '    GridUser.Rows(i).BackColor = Drawing.Color.LemonChiffon
                End If
            Next
        End If

    End Sub

    Protected Sub btnRegister_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        'If Me.txtnUserName.Text.Trim.Length > 0 And Me.dropdept.Text.Trim.Length > 0 Then
        If Me.txtnUserName.Text.Trim.Length > 0 Then

            If le.ReturnCheckCondition("cmstbllog", "Username", " where Username='" + Me.txtnUserName.Text + "'" + le.AnddCode) = True Then
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

    Protected Sub GridUser_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridUser.SelectedIndexChanged
        Try


            Dim i As Integer
            Dim procid As String
            Dim row As GridViewRow = Me.GridUser.SelectedRow
            procid = row.Cells(1).Text
            Me.txtnUserName.Text = le.ReturnConditionNA("cmstbllog", "Username", " where Username='" + procid + "'" + le.AnddCode)

            Me.txtfullname.Text = le.ReturnConditionNA("cmstbllog", "fullname", " where Username='" + procid + "'" + le.AnddCode)
            Me.txtResidentialAddress.Text = le.ReturnConditionNA("cmstbllog", "AddressofResidence", " where Username='" + procid + "'" + le.AnddCode)
            Me.txtOfficeAddress.Text = le.ReturnConditionNA("cmstbllog", "officeAddress", " where Username='" + procid + "'" + le.AnddCode)
            Me.txtemail.Text = le.ReturnConditionNA("cmstbllog", "email", " where Username='" + procid + "'" + le.AnddCode)

            Me.txtphone.Text = le.ReturnConditionNA("cmstbllog", "Phone", " where Username='" + procid + "'" + le.AnddCode)

            le.ListAnyThing(Me.dropdept, "cmstblDept", "DeptName", " where BranchCode='" + le.BranchCode + "'")
            activate()

            If le.ReturnCheckCondition("cmstbllog", "IsApproved", " where username='" & procid & "' and IsApproved='No'") = True Then
                btnapprove.Visible = True
                DropIsApproved.Enabled = False
            Else
                btnapprove.Visible = False
                DropIsApproved.Enabled = True
            End If
            Me.DropIsApproved.Text = le.ReturnConditionNA("cmstbllog", "IsApproved", " where Username='" + procid + "'" + le.AnddCode)

            Me.dropdept.Text = le.ReturnConditionNA("cmstbllog", "Dept", " where Username='" + procid + "'" + le.AnddCode)

            ' If le.IsJohn(le.USerName) = True Then
            '  Me.Dropbranchcode.Text = le.ReturnConditionNA("cmstbllog", "BranchCode", " where Username='" + procid + "'" + le.AnddCode)

            ' End If

            le.ActivityANDEmailLog("Started edit on Username data for " + Me.txtnUserName.Text)

        Catch ex As Exception

        End Try
    End Sub
    Private Sub activate()
        Me.btnRegister.Visible = False
        Me.btnUpdate.Visible = True
        Me.BtnDelete.Visible = True
        btnapprove.Visible = True
        Me.txtnUserName.ReadOnly = True
        DropIsApproved.Enabled = True
    End Sub
    Private Sub deactivate()
        Me.btnRegister.Visible = True
        Me.btnUpdate.Visible = False
        Me.BtnDelete.Visible = False
        btnapprove.Visible = False
        Me.txtnUserName.ReadOnly = False
        DropIsApproved.Enabled = True
        le.ClearText(Me)
    End Sub
    Private Sub Updated()
        Try

            'If le.IsRoleBOOL("Gmgr", "", "", "", "", "") = True Then

            'Else
            'End If
            ' branch = Dropbranchcode.Text
            le.AndCondition = " where [Username] = '" & Me.txtnUserName.Text.Trim & "'" + le.AnddCode


            Dim xTexts As String = " [email] = '" & Me.txtemail.Text & "'," &
            " [Phone] = '" & Me.txtphone.Text & "'," &
             " [Dept] = '" & Me.dropdept.Text & "'," &
              " [IsApproved] = '" & Me.DropIsApproved.Text & "'," &
             " [BranchCode] = '" & CType(Session("Branchcode"), String) & "'," &
                " [AddressofResidence] = '" & Me.txtResidentialAddress.Text & "'," &
                " [OfficeAddress] = '" & Me.txtOfficeAddress.Text & "'," &
            " [fullname] = '" & Me.txtfullname.Text & "'"

            ' check that the unit to be changed is not a whole sale unit, and it has not been used in sales.
            'If le.ReturnCheckCondition("UsernameSales", "unit", " where Username='" + Me.txtnUserName.Text + "' and unit='" + Me.Dropunit.Text + "'" + le.AndBranchCode) = False And le.ReturnCheckCondition("UsernameUnitPrice", "unit", " where Username='" + Me.txtnUserName.Text + "' and unit='" + Me.Dropunit.Text + "'" + le.AndBranchCode) = False Then
            le.UpdateData("cmstbllog", xTexts, le.AndCondition)
            le.lmsg = "Updated Successfully!"
            ' RegExist = False
            le.ActivityANDEmailLog("Updated Username data for " + Me.txtnUserName.Text)
            Me.srcDelete()
            deactivate()
            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
            'end check

            'Else
            '    le.lmsg = "You cannot change retail unit to " + Me.Dropunit.Text + ". It is already unitized as a whole sale unit; or Sales have been made with the original retail unit."
            '    ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
            '    Exit Sub
            'End If


        Catch ex As Exception
            le.forceclosedatabase()
        End Try
    End Sub
    Protected Sub BtnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Me.Updated()
    End Sub



    Protected Sub BtnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Try
            'ensure nobody 
            '  If le.ReturnCheckCondition("UsernameSales", "unit", " where Username='" + Me.txtnUserName.Text + "'" + le.AndBranchCode) = False And le.ReturnCheckCondition("UsernameUnitPrice", "unit", " where Username='" + Me.txtnUserName.Text + "'" + le.AndBranchCode) = False Then

            le.ReturnDelete("cmstbllog", " where Username='" & Me.txtnUserName.Text & "'" + le.AnddCode)
            Me.srcDelete()
            le.ActivityANDEmailLog("Deleted Username registration data for " + Me.txtnUserName.Text)
            deactivate()
            le.lmsg = Me.txtnUserName.Text + " User successrully deleted."
            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)

            'Else
            '    le.lmsg = "Username already Sold or unitized.Deletion Not Possible."
            '    ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
            '    Exit Sub
            'End If
        Catch ex As Exception
            le.forceclosedatabase()
        End Try

    End Sub

    Protected Sub BtnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClear.Click
        Response.Redirect("manageusers.aspx")
    End Sub

    Private Sub btnApprove2_Click(sender As Object, e As EventArgs) Handles btnApprove2.Click
        'Approve the pre-registered user
        le.AndCondition = " where [Username] = '" & Me.txtnUserName.Text.Trim & "'" + le.AnddCode

        Dim appr As String = "Yes"
        Dim xTexts As String = " [email] = '" & Me.txtemail.Text & "'," &
        " [Phone] = '" & Me.txtphone.Text & "'," &
         " [Dept] = '" & Me.dropdept.Text & "'," &
          " [IsApproved] = '" & appr & "'," &
         " [BranchCode] = '" & CType(Session("Branchcode"), String) & "'," &
            " [AddressofResidence] = '" & Me.txtResidentialAddress.Text & "'," &
            " [OfficeAddress] = '" & Me.txtOfficeAddress.Text & "'," &
        " [fullname] = '" & Me.txtfullname.Text & "'"

        le.UpdateData("cmstbllog", xTexts, le.AndCondition)


        Dim coy As String = le.ReturnConditionNA("cmstblCoy", "CompanyName", le.WhereBranchCode)
        Dim coyaddress As String = le.ReturnConditionNA("cmstblCoy", "address", le.WhereBranchCode)

        Dim bodi As String = " Dear " & le.Userfullname(txtnUserName.Text) & ", " & vbCrLf &
         vbCrLf & "This is to inform you that an APPROVAL for your  new account registration for the use of S3 on Halogen server is successful!" & vbCrLf &
         vbCrLf & vbCrLf & " Your Branch Office Code is: " & le.BranchCode & "  and its full name is " & coy & " and Address is " & coyaddress & vbCrLf &
         vbCrLf & " Your username is: " & txtnUserName.Text & " " & vbCrLf &
         vbCrLf & " Your Password is: " & le.ReturnConditionNA("cmstbllog", "pwd", " where Username='" + txtnUserName.Text.Trim + "'") & " " & vbCrLf & "  Endavor to login and change this password immediately." & vbCrLf &
         vbCrLf & " The S3 login URL is: https://halogen.veracelservers.com " & vbCrLf &
         vbCrLf & " Thank you, and warm welcome onboard. " & vbCrLf &
               vbCrLf & vbCrLf & " Best Regards, " & vbCrLf &
             vbCrLf & "" & le.Userfullname(le.USerName) & " " & vbCrLf &
                "REGISTRANT " & vbCrLf &
        "  For: " & coy & " " & vbCrLf & ""
        le.SendEmailto(Me.txtnUserName.Text, "S3 Account Registration  Approval", bodi)
        le.ActivityANDEmailLog("Approved a Registered new  user by name  called " + Me.txtfullname.Text)
        Me.srcDelete()
        le.ClearText(Me)
        le.lmsg = "Saved Successfully!"
        ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
    End Sub
End Class