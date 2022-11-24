Public Class managedept
    Inherits System.Web.UI.Page

    Dim le As New mfa1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        le.Checksession()
        le.ReplaceText(Me)
        'Me.BtnDelete.Attributes.Add("onclick", "window.showModalDialog('ResultTemplate.aspx','mywindow','width=760,height=600,scrollbar=yes')")
        Me.BtnDelete.Attributes.Add("onclick", "return confirm('Are You Sure You Want To Delete This Dept? ');")

        If Me.IsPostBack = False Then
            Me.srcDelete()
            le.MustBe("Gmgr", "Rmgr", "", "", "", "") 'only CEO,Gmgr and RMgr
            le.ActivityANDEmailLog("Visisted  Department registration page. ")
            If le.IsJohn(le.USerName) = True Then
                le.ListAnyThing(Me.Dropbranchcode, "cmstblCoy", "BranchCode", " where dCode='" + CType(Session("dcode"), String) + "'")
                Me.Dropbranchcode.Visible = True
            End If



        End If
    End Sub

    Private branch As String
    Private Sub pRegister()
        If le.IsJohn(le.USerName) = True Then
            branch = Dropbranchcode.Text
        Else
            branch = CType(Session("Branchcode"), String)
        End If
        'Try
        Dim xFields As String = "DeptName,email,phone,IsApproved,Recordedby,dCode,BranchCode"
        Dim xTexts As String = "'" & Me.txtnUserName.Text & "'," &
          "'" & Me.txtemail.Text & "'," &
           "'" & Me.txtphone.Text & "'," &
          "'" & "Yes" & "'," &
          "'" & CType(Session("UserName"), String) & "'," &
           "'" & CType(Session("dCode"), String) & "'," &
          "'" & branch & "'"
        le.InsertData("cmstblDept", xFields, xTexts)

        ' Me.RegCanBranch()
        le.ActivityANDEmailLog("Registered a new  Department  called " + Me.txtnUserName.Text)
        Me.srcDelete()
        le.ClearText(Me)
        le.lmsg = "Saved Successfully!"
        ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub srcDelete()
        'Try
        '' Dim xFields As String = "[ID],[Drug] as [DISPENSED DRUGS],[Qty] as [QTY],[UNIT],[ARID],[SalesID] as [DSP ID],[Sprice] as [UNIT PRICE (N)],(Qty * Sprice) as [PRICE (N)]"
        Dim xFields As String = "id as [ID],DeptName as [Departments],BranchCode as [Branch Code],Phone"

        'le.AndCondition =le.WhereBranchCode
        le.DT = le.ReportSelectedDataTable("cmstblDept", xFields, le.WhereBranchCode)
        Me.GridUser.DataSource = le.DT
        Me.GridUser.DataBind()


    End Sub

    Protected Sub btnRegister_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        If le.IsJohn(le.USerName) = True Then
            le.AndBranchCode = " and BranchCode='" & Dropbranchcode.Text & "'"

        End If

        If Me.txtnUserName.Text.Trim.Length > 0 Then
            If le.ReturnCheckCondition("cmstblDept", "Deptname", " where deptname='" + Me.txtnUserName.Text + "'" + le.AndBranchCode) = True Then
                le.lmsg = "Department Already Existing"
                ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
                Exit Sub
            Else

                Me.pRegister()


            End If
        Else
            le.lmsg = "Enter Department Name"
            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)

        End If
    End Sub

    Protected Sub GridUser_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridUser.SelectedIndexChanged
        Try

            If le.IsJohn(le.USerName) = True Then
                le.AndBranchCode = " and BranchCode='" & Dropbranchcode.Text & "'"

            End If

            Dim i As Integer
            Dim procid As String
            Dim row As GridViewRow = Me.GridUser.SelectedRow
            procid = row.Cells(1).Text
            lblID.Text = procid
            Me.txtnUserName.Text = le.ReturnConditionNA("cmstbldept", "deptname", " where id='" + procid + "'")

            Me.txtphone.Text = le.ReturnConditionNA("cmstbldept", "phone", " where id='" + procid + "'")
            Me.txtemail.Text = le.ReturnConditionNA("cmstbldept", "email", " where id='" + procid + "'")

            If le.IsJohn(le.USerName) = True Then
                Me.Dropbranchcode.Text = le.ReturnConditionNA("cmstbldept", "BranchCode", " where id='" + procid + "'")

            End If
            'Me.txtAddressofresidence.Text = le.ReturnConditionNA("tbllog", "Addressofresidence", " where username='" + procid + "'and nCode='" + Me.dropStation.Text + "'")
            'Me.txtAddressoforigin.Text = le.ReturnConditionNA("tbllog", "Addressoforigin", " where username='" + procid + "'and nCode='" + Me.dropStation.Text + "'")
            le.ActivityANDEmailLog("Started edit on Department registration data for " + Me.txtnUserName.Text)
            activate()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub activate()
        Me.btnRegister.Visible = False
        Me.btnUpdate.Visible = True
        Me.BtnDelete.Visible = True
        Me.txtnUserName.ReadOnly = True
    End Sub
    Private Sub deactivate()
        Me.btnRegister.Visible = True
        Me.btnUpdate.Visible = False
        Me.BtnDelete.Visible = False
        Me.txtnUserName.ReadOnly = False
        le.ClearText(Me)
    End Sub
    Private Sub Updated()
        Try
            If le.IsJohn(le.USerName) = True Then
                branch = Dropbranchcode.Text
                ' le.AndBranchCode = " and BranchCode='" & Dropbranchcode.Text & "'"
            Else
                branch = CType(Session("Branchcode"), String)
            End If

            le.AndCondition = "where [id] = '" & Me.lblID.Text.Trim & "'" '+ le.AndBranchCode ' and dCode='" + CType(Session("dCode"), String) + "'"

            Dim xTexts As String = " [email] = '" & Me.txtemail.Text & "'," &
                " [BranchCode] = '" & branch & "'," &
                       " [phone] = '" & Me.txtphone.Text & "'"

            le.UpdateData("cmstbldept", xTexts, le.AndCondition)
            le.lmsg = "Updated Successfully!"
            ' RegExist = False
            le.ActivityANDEmailLog("Updated Department registration data for " + Me.txtnUserName.Text)
            Me.srcDelete()
            deactivate()
            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
        Catch ex As Exception
            le.forceclosedatabase()
        End Try
    End Sub
    Protected Sub BtnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Me.Updated()
    End Sub

    'Protected Sub dropStation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dropStation.SelectedIndexChanged
    '    'le.ListAnyThing(Me.dropStation, "tblStations", "nCode", " where dCode='" + CType(Session("dCode"), String) + "'")
    '    ' le.ListAnyThing(Me.DropTank, "tbltank", "nTankCode", " where nCode='" + Me.dropStation.Text + "'")

    '    Me.srcDelete()
    '    Me.btnRegister.Visible = True
    '    Me.btnUpdate.Visible = False
    '    Me.txtnUserName.ReadOnly = False
    'End Sub

    Protected Sub BtnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Try
            'ensure nobody 
            If le.ReturnCheckCondition("cmstbllog", "Dept", " where dept='" + Me.txtnUserName.Text + "'" + le.AndBranchCode) = True Then
                le.lmsg = "Active User Found In This Department.Deletion Not Possible."
                ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
                Exit Sub

            Else
                le.ReturnDelete("cmstbldept", " where deptname='" & Me.txtnUserName.Text & "'" + le.AndBranchCode)
                Me.srcDelete()
                le.ActivityANDEmailLog("Deleted Department registration data for " + Me.txtnUserName.Text)
                deactivate()
                ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)

            End If
        Catch ex As Exception
            le.forceclosedatabase()
        End Try

    End Sub

    Protected Sub BtnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClear.Click
        Response.Redirect("managedept.aspx")
    End Sub
End Class