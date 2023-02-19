Partial Class Aboutintranet
    Inherits Page
    Dim le As New mfa1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        le.Checksession()
        le.ReplaceText(Me)
        Me.LinkBtnbranches.Attributes.Add("onclick", "return confirm('Are You Sure You Want exit this branch and visit another? ');")

        If Me.IsPostBack = False Then


            'If le.IsJohn(le.USerName) = False Then
            Me.lblcoy.Text = le.ReturnConditionNA("cmstblCoy", "CompanyName", le.WhereBranchCode)
            Me.lbladdress.Text = le.ReturnConditionNA("cmstblCoy", "address", le.WhereBranchCode)
            le.BranchLogo(Imgbtnlogo, le.BranchCode)
            'Else
            '    Imgbtnlogo.ImageUrl = "photo/logo" & "/" & "logo.png"
            '    Imgbtnlogo.DataBind()
            'End If
            Lbluser.Text = le.Userfullname(le.USerName)
            If le.IsRoleBOOL("Gmgr", "GAccount", "RMgr", "RAccount", "", "") = True Then
                LinkBtnbranches.Visible = True


            End If
            srcDelete()

        End If
    End Sub

    Private Sub LinkBtnbranches_Click(sender As Object, e As EventArgs) Handles LinkBtnbranches.Click
        If le.IsRoleBOOL("Gmgr", "GAccount", "RMgr", "RAccount", "", "") = True Then
            Response.Redirect("Choosebranch.aspx")
        Else
            le.lmsg = "So sorry, you might have lost the power to visit other branches!"
            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)

        End If
    End Sub

    Private Sub srcDelete()
        'Try
        '' Dim xFields As String = "[ID],[Drug] as [DISPENSED DRUGS],[Qty] as [QTY],[UNIT],[ARID],[SalesID] as [DSP ID],[Sprice] as [UNIT PRICE (N)],(Qty * Sprice) as [PRICE (N)]"
        Dim xFields As String = "ApplicationName as [Application Name],AppDescription as [App Description]"

        'le.AndCondition =le.WhereBranchCode
        le.DT = le.ReportSelectedDataTable("mfatblApplications", xFields, " where dCode='" & CType(Session("dcode"), String) & "'")
        Me.Gridapps.DataSource = le.DT
        Me.Gridapps.DataBind()


    End Sub

End Class