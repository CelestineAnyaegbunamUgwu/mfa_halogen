Public Class manageapps
    Inherits System.Web.UI.Page

    Dim le As New mfa1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        le.Checksession()
        le.ReplaceText(Me)
        'Me.BtnDelete.Attributes.Add("onclick", "window.showModalDialog('ResultTemplate.aspx','mywindow','width=760,height=600,scrollbar=yes')")
        Me.BtnDelete.Attributes.Add("onclick", "return confirm('Are You Sure You Want To Delete This app? ');")

        If Me.IsPostBack = False Then
            Me.srcDelete()
            le.MustBe("Gmgr", "", "Rmgr", "", "", "") 'only CEO,Gmgr and RMgr




        End If
    End Sub
    Private Sub pRegister()

        'Try
        Dim xFields As String = "ApplicationName,AppDescription,Recordedby,dCode"
        Dim xTexts As String = "'" & Me.txtApplicationName.Text & "'," &
          "'" & Me.txtAppDescription.Text & "'," &
          "'" & CType(Session("UserName"), String) & "'," &
          "'" & CType(Session("dcode"), String) & "'"
        le.InsertData("mfatblApplications", xFields, xTexts)

        ' Me.RegCanBranch()
        ''' le.ActivityANDEmailLog("Registered a new  region  called " + Me.txtApplicationName.Text)
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
        Dim xFields As String = "ApplicationName as [Application Name],AppDescription as [App Description]"

        'le.AndCondition =le.WhereBranchCode
        le.DT = le.ReportSelectedDataTable("mfatblApplications", xFields, " where dCode='" & CType(Session("dcode"), String) & "'")
        Me.GridUser.DataSource = le.DT
        Me.GridUser.DataBind()


    End Sub

    Protected Sub btnRegister_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        If Me.txtApplicationName.Text.Trim.Length > 0 Then
            If le.ReturnCheckCondition("mfatblApplications", "ApplicationName", " where ApplicationName='" + Me.txtApplicationName.Text + "'" + le.AnddCode) = True Then
                le.lmsg = "App Already Existing"
                ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
                Exit Sub
            Else

                Me.pRegister()


            End If
        Else
            le.lmsg = "Enter App Name"
            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)

        End If
    End Sub

    Protected Sub GridUser_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridUser.SelectedIndexChanged
        Try


            Dim i As Integer
            Dim procid As String
            Dim row As GridViewRow = Me.GridUser.SelectedRow
            procid = row.Cells(1).Text
            Me.txtApplicationName.Text = le.ReturnConditionNA("mfatblApplications", "ApplicationName", " where ApplicationName='" + procid + "'")

            Me.txtAppDescription.Text = le.ReturnConditionNA("mfatblApplications", "AppDescription", " where ApplicationName='" + procid + "'")

            'Me.txtAddressofresidence.Text = le.ReturnConditionNA("tbllog", "Addressofresidence", " where username='" + procid + "'and nCode='" + Me.dropStation.Text + "'")
            'Me.txtAddressoforigin.Text = le.ReturnConditionNA("tbllog", "Addressoforigin", " where username='" + procid + "'and nCode='" + Me.dropStation.Text + "'")
            le.ActivityANDEmailLog("Started edit on app registration data for " + Me.txtApplicationName.Text)
            activate()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub activate()
        Me.btnRegister.Visible = False
        Me.btnUpdate.Visible = True
        Me.BtnDelete.Visible = True
        Me.txtApplicationName.ReadOnly = True
    End Sub
    Private Sub deactivate()
        Me.btnRegister.Visible = True
        Me.btnUpdate.Visible = False
        Me.BtnDelete.Visible = False
        Me.txtApplicationName.ReadOnly = False
        le.ClearText(Me)
    End Sub
    Private Sub Updated()
        Try
            le.AndCondition = "where [ApplicationName] = '" & Me.txtApplicationName.Text.Trim & "'" + le.AnddCode ' and dCode='" + CType(Session("dCode"), String) + "'"

            Dim xTexts As String = " [AppDescription] = '" & Me.txtAppDescription.Text & "'"


            le.UpdateData("mfatblApplications", xTexts, le.AndCondition)
            le.lmsg = "Updated Successfully!"
            ' RegExist = False
            le.ActivityANDEmailLog("Updated Application Name registration data for " + Me.txtApplicationName.Text)
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
            If le.MustbeBOOL("", "", "", "", "", "") = False Then
                le.lmsg = "You are not a CEO.Deletion Not Possible."
                ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
                Exit Sub

            Else
                le.ReturnDelete("mfatblApplications", " where ApplicationName='" & Me.txtApplicationName.Text & "'" + le.AnddCode)
                Me.srcDelete()
                le.ActivityANDEmailLog("Deleted app registration data for " + Me.txtApplicationName.Text)
                deactivate()
                ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)

            End If
        Catch ex As Exception
            le.forceclosedatabase()
        End Try

    End Sub

    Protected Sub BtnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClear.Click
        Response.Redirect("manageapps.aspx")
    End Sub
End Class