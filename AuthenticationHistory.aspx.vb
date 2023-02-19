Public Class AuthenticationHistory
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


    Private Sub srcDelete()
        'Try
        '' Dim xFields As String = "[ID],[Drug] as [DISPENSED DRUGS],[Qty] as [QTY],[UNIT],[ARID],[SalesID] as [DSP ID],[Sprice] as [UNIT PRICE (N)],(Qty * Sprice) as [PRICE (N)]"
        Dim xFields As String = "Id as [Id], Username as [User Name], nTime as [Date Time],emailCode as [Sent Code],Device as [Device Used],LocationIP as [ISP IP],Browser,BranchCode as [Branch],Status"

        'le.AndCondition =le.WhereBranchCode
        le.DT = le.ReportSelectedDataTable("tblfma", xFields, " where BranchCode='" & CType(Session("BranchCode"), String) & "'")
        Me.GridUser.DataSource = le.DT
        Me.GridUser.DataBind()


    End Sub



    Protected Sub GridUser_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridUser.SelectedIndexChanged
        Try


            Dim i As Integer
            Dim procid As String
            Dim row As GridViewRow = Me.GridUser.SelectedRow
            procid = row.Cells(1).Text
            le.ReturnDelete("tblfma", " where id=" & CInt(procid))
            srcDelete()
            le.ActivityANDEmailLog("Deleted Authentication History at " + le.BranchCode)

        Catch ex As Exception

        End Try
    End Sub






    Protected Sub BtnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Try
            'ensure nobody 
            If le.MustbeBOOL("", "", "", "", "", "") = False Then
                le.lmsg = "You are not a CEO.Deletion Not Possible."
                ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
                Exit Sub

            Else
                le.ReturnDelete("tblfma", " where BranchCode='" & le.BranchCode & "'" + le.AnddCode)
                Me.srcDelete()
                le.ActivityANDEmailLog("Deleted all authenications  for " + le.BranchCode)

            End If
        Catch ex As Exception
            le.forceclosedatabase()
        End Try

    End Sub


End Class