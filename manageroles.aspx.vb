Public Class manageroles
    Inherits System.Web.UI.Page

    Dim le As New mfa1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        le.Checksession()
        le.ReplaceText(Me)
        'Me.BtnDelete.Attributes.Add("onclick", "window.showModalDialog('ResultTemplate.aspx','mywindow','width=760,height=600,scrollbar=yes')")
        'Me.BtnDelete.Attributes.Add("onclick", "return confirm('Are You Sure You Want To Delete This User? ');")

        le.MustBe("Gmgr", "GAccount", "Rmgr", "RAccount", "BMgr", "BAccount") 'managers and accountants only
        If Me.IsPostBack = False Then
            Me.srcDelete()




            'Me.lbluser.Text = le.Userfullname(le.USerName)
            'le.UserPhoto(Me.userpho, le.USerName)


        End If
    End Sub



    Private Sub srcDelete()
        If le.IsJohn(le.USerName) = True Then
            le.DT = le.ReportDataTable2FROMQUERY("cmstbllog", "Select username as [Login Name] from cmstbllog where dcode='" & CType(Session("dcode"), String) & "'")
            le.AndBranchCode = ""

        Else
            le.DT = le.ReportDataTable2FROMQUERY("cmstbllog", "Select username as [Login Name] from cmstbllog where Branchcode='" & le.BranchCode & "'")

        End If

        Me.GridUser.DataSource = le.DT
        Me.GridUser.DataBind()
        Dim f As Integer
        Dim zx As String
        For f = 0 To GridUser.Rows.Count - 1

            zx = GridUser.Rows(f).Cells(10).Text

            Dim lbluser As Label = DirectCast(GridUser.Rows.Item(f).FindControl("lbluser"), Label)
            lbluser.Text = le.Userfullname(zx)



            Dim DropDirector As DropDownList = DirectCast(GridUser.Rows.Item(f).FindControl("DropDirector"), DropDownList)
            DropDirector.Text = le.ReturnConditionNA("cmstbllog", "CEO", " where username='" & zx & "'" & le.AndBranchCode)
            checkrole(DropDirector)

            Dim DropGMgr As DropDownList = DirectCast(GridUser.Rows.Item(f).FindControl("DropGMgr"), DropDownList)
            DropGMgr.Text = le.ReturnConditionNA("cmstbllog", "Gmgr", " where username='" & zx & "'" & le.AndBranchCode)
            checkrole(DropGMgr)

            Dim DropGAccount As DropDownList = DirectCast(GridUser.Rows.Item(f).FindControl("DropGAccount"), DropDownList)
            DropGAccount.Text = le.ReturnConditionNA("cmstbllog", "GAccount", " where username='" & zx & "'" & le.AndBranchCode)
            checkrole(DropGAccount)

            Dim DropRMgr As DropDownList = DirectCast(GridUser.Rows.Item(f).FindControl("DropRMgr"), DropDownList)
            DropRMgr.Text = le.ReturnConditionNA("cmstbllog", "RMgr", " where username='" & zx & "'" & le.AndBranchCode)
            checkrole(DropRMgr)

            Dim DropRAccount As DropDownList = DirectCast(GridUser.Rows.Item(f).FindControl("DropRAccount"), DropDownList)
            DropRAccount.Text = le.ReturnConditionNA("cmstbllog", "RAccount", " where username='" & zx & "'" & le.AndBranchCode)
            checkrole(DropRAccount)


            Dim DropBMgr As DropDownList = DirectCast(GridUser.Rows.Item(f).FindControl("DropBMgr"), DropDownList)
            DropBMgr.Text = le.ReturnConditionNA("cmstbllog", "BMgr", " where username='" & zx & "'" & le.AndBranchCode)
            checkrole(DropBMgr)

            Dim DropBAccount As DropDownList = DirectCast(GridUser.Rows.Item(f).FindControl("DropBAccount"), DropDownList)
            DropBAccount.Text = le.ReturnConditionNA("cmstbllog", "BAccount", " where username='" & zx & "'" & le.AndBranchCode)
            checkrole(DropBAccount)






            Dim Drophod As DropDownList = DirectCast(GridUser.Rows.Item(f).FindControl("Drophod"), DropDownList)
            Drophod.Text = le.ReturnConditionNA("cmstbllog", "hod", " where username='" & zx & "'" & le.AndBranchCode)
            checkrole(Drophod)


            'Disable your senior edit ability

            If le.MustbeBOOL("", "", "", "", "", "") = False Then
                DropDirector.Enabled = False
                DropGMgr.Enabled = False
                DropGAccount.Enabled = False
                ' Drophod.Enabled = False
            End If
            If le.MustbeBOOL("GMgr", "", "", "", "", "") = False Then
                DropRMgr.Enabled = False
                DropRAccount.Enabled = False
                Drophod.Enabled = False
            End If

            If le.MustbeBOOL("RMgr", "", "", "", "", "") = False Then
                DropBMgr.Enabled = False
                DropBAccount.Enabled = False
            End If

        Next
        GridUser.PageIndex = 0

    End Sub
    Private Function checkrole(ByVal dropd As DropDownList)
        If dropd.Text = "Yes" Then
            dropd.BackColor = Drawing.Color.Lavender
        Else
            dropd.BackColor = Drawing.Color.AntiqueWhite

        End If
    End Function







    Private Sub Griduser_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridUser.RowCommand
        If e.CommandName = "LinkBtnupdate" Then
            'Determine the RowIndex of the Row whose Button was clicked.

            GridUser.PageIndex = 0
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            'Reference the GridView Row.
            Dim row As GridViewRow = GridUser.Rows(rowIndex)

            Dim zx As String = row.Cells(10).Text
            Dim DropDirector As DropDownList = DirectCast(GridUser.Rows.Item(rowIndex).FindControl("DropDirector"), DropDownList)

            Dim DropGMgr As DropDownList = DirectCast(GridUser.Rows.Item(rowIndex).FindControl("DropGMgr"), DropDownList)

            Dim DropGAccount As DropDownList = DirectCast(GridUser.Rows.Item(rowIndex).FindControl("DropGAccount"), DropDownList)

            Dim DropRMgr As DropDownList = DirectCast(GridUser.Rows.Item(rowIndex).FindControl("DropRMgr"), DropDownList)
            Dim DropRAccount As DropDownList = DirectCast(GridUser.Rows.Item(rowIndex).FindControl("DropRAccount"), DropDownList)
            Dim DropBMgr As DropDownList = DirectCast(GridUser.Rows.Item(rowIndex).FindControl("DropBMgr"), DropDownList)
            Dim DropBAccount As DropDownList = DirectCast(GridUser.Rows.Item(rowIndex).FindControl("DropBAccount"), DropDownList)


            Dim Drophod As DropDownList = DirectCast(GridUser.Rows.Item(rowIndex).FindControl("Drophod"), DropDownList)


            le.AndCondition = "where [Username] = '" & zx & "'" '& le.AndBranchCode

            Dim xTexts As String = " [CEO] = '" & DropDirector.Text & "'," &
                       " [GMgr] = '" & DropGMgr.Text & "'," &
                       " [GAccount] = '" & DropGAccount.Text & "'," &
                       " [RMgr] = '" & DropRMgr.Text & "'," &
                       " [RAccount] = '" & DropRAccount.Text & "'," &
                       " [BMgr] = '" & DropBMgr.Text & "'," &
                       " [BAccount] = '" & DropBAccount.Text & "'," &
                          " [hod] = '" & Drophod.Text & "'"

            le.UpdateData("cmstbllog", xTexts, le.AndCondition)
            le.lmsg = "Role Updated Successfully for " & le.Userfullname(zx)
            ' RegExist = False
            Me.srcDelete()

            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)

        End If
    End Sub






    Private AndCond As String


    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)


        GridUser.PageIndex = e.NewPageIndex
        Me.srcDelete()

    End Sub

    Private Sub GridUser_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles GridUser.RowUpdating
        srcDelete()
    End Sub


End Class