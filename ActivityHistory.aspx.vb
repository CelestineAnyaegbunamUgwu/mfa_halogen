Public Class ActivityHistory
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
        Dim xFields As String = "Id as [Id], Username as [User Name], Activities as [Action Performed], Recordtime  as [Date Time of Action],(select fullname from cmstbllog where username=cmstblactivity.username) as [Full Name],Device as [Device Used],IPAddress as [ISP IP],BranchCode as [Branch]"

        'le.AndCondition =le.WhereBranchCode
        le.DT = le.ReportSelectedDataTable("cmstblActivity", xFields, " where BranchCode='" & CType(Session("BranchCode"), String) & "' order by recordtime desc")
        Me.GridUser.DataSource = le.DT
        Me.GridUser.DataBind()


    End Sub



    Protected Sub GridUser_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridUser.SelectedIndexChanged
        Try


            Dim i As Integer
            Dim procid As String
            Dim row As GridViewRow = Me.GridUser.SelectedRow
            procid = row.Cells(1).Text
            le.ReturnDelete("cmstblActivity", " where id=" & CInt(procid))

            le.ActivityANDEmailLog("Deleted Activities History at " + le.BranchCode)
            srcDelete()
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
                le.ReturnDelete("cmstblActivity", " where BranchCode='" & le.BranchCode & "'")

                le.ActivityANDEmailLog("Deleted all activities histories  for " + le.BranchCode)
                Me.srcDelete()
            End If
        Catch ex As Exception
            le.forceclosedatabase()
        End Try

    End Sub


End Class