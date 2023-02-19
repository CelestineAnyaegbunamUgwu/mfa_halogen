Public Class manageregion
    Inherits System.Web.UI.Page

    Dim le As New mfa1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        le.Checksession()
        le.ReplaceText(Me)
        'Me.BtnDelete.Attributes.Add("onclick", "window.showModalDialog('ResultTemplate.aspx','mywindow','width=760,height=600,scrollbar=yes')")
        Me.BtnDelete.Attributes.Add("onclick", "return confirm('Are You Sure You Want To Delete This Dept? ');")

        If Me.IsPostBack = False Then
            Me.srcDelete()
            le.MustBe("Gmgr", "", "", "", "", "") 'only CEO,Gmgr and RMgr




        End If
    End Sub
    Private Sub pRegister()

        'Try
        Dim xFields As String = "nRegion,remarks,Recordedby,dCode"
        Dim xTexts As String = "'" & Me.txtRegionName.Text & "'," &
          "'" & Me.txtRemarks.Text & "'," &
          "'" & CType(Session("UserName"), String) & "'," &
          "'" & CType(Session("dcode"), String) & "'"
        le.InsertData("tblregions", xFields, xTexts)

        ' Me.RegCanBranch()
        ''' le.ActivityANDEmailLog("Registered a new  region  called " + Me.txtRegionName.Text)
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
        Dim xFields As String = "nRegion as [Regions],Remarks as [Remarks]"

        'le.AndCondition =le.WhereBranchCode
        le.DT = le.ReportSelectedDataTable("tblregions", xFields, " where dCode='" & CType(Session("dcode"), String) & "'")
        Me.GridUser.DataSource = le.DT
        Me.GridUser.DataBind()


    End Sub

    Protected Sub btnRegister_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        If Me.txtRegionName.Text.Trim.Length > 0 Then
            If le.ReturnCheckCondition("tblregions", "nRegion", " where nRegion='" + Me.txtRegionName.Text + "'" + le.AnddCode) = True Then
                le.lmsg = "Region Already Existing"
                ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
                Exit Sub
            Else

                Me.pRegister()


            End If
        Else
            le.lmsg = "Enter Region Name"
            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)

        End If
    End Sub

    Protected Sub GridUser_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridUser.SelectedIndexChanged
        Try


            Dim i As Integer
            Dim procid As String
            Dim row As GridViewRow = Me.GridUser.SelectedRow
            procid = row.Cells(1).Text
            Me.txtRegionName.Text = le.ReturnConditionNA("tblregions", "nregion", " where nregion='" + procid + "'")

            Me.txtRemarks.Text = le.ReturnConditionNA("tblregions", "remarks", " where nregion='" + procid + "'")

            'Me.txtAddressofresidence.Text = le.ReturnConditionNA("tbllog", "Addressofresidence", " where username='" + procid + "'and nCode='" + Me.dropStation.Text + "'")
            'Me.txtAddressoforigin.Text = le.ReturnConditionNA("tbllog", "Addressoforigin", " where username='" + procid + "'and nCode='" + Me.dropStation.Text + "'")
            le.ActivityANDEmailLog("Started edit on region registration data for " + Me.txtRegionName.Text)
            activate()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub activate()
        Me.btnRegister.Visible = False
        Me.btnUpdate.Visible = True
        Me.BtnDelete.Visible = True
        Me.txtRegionName.ReadOnly = True
    End Sub
    Private Sub deactivate()
        Me.btnRegister.Visible = True
        Me.btnUpdate.Visible = False
        Me.BtnDelete.Visible = False
        Me.txtRegionName.ReadOnly = False
        le.ClearText(Me)
    End Sub
    Private Sub Updated()
        Try
            le.AndCondition = "where [nregion] = '" & Me.txtRegionName.Text.Trim & "'" + le.AnddCode ' and dCode='" + CType(Session("dCode"), String) + "'"

            Dim xTexts As String = " [remarks] = '" & Me.txtRemarks.Text & "'"


            le.UpdateData("tblregions", xTexts, le.AndCondition)
            le.lmsg = "Updated Successfully!"
            ' RegExist = False
            le.ActivityANDEmailLog("Updated regions registration data for " + Me.txtRegionName.Text)
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
                le.ReturnDelete("tblregions", " where nregion='" & Me.txtRegionName.Text & "'" + le.AnddCode)
                Me.srcDelete()
                le.ActivityANDEmailLog("Deleted region registration data for " + Me.txtRegionName.Text)
                deactivate()
                ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)

            End If
        Catch ex As Exception
            le.forceclosedatabase()
        End Try

    End Sub

    Protected Sub BtnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClear.Click
        Response.Redirect("manageregion.aspx")
    End Sub
End Class