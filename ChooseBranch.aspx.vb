Public Class ChooseBranch
    Inherits System.Web.UI.Page

    Dim le As New mfa1

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        le.Checksession()
        If Me.IsPostBack = False Then
            If Not IsPostBack Then


                srcDelete()

            End If

        End If
    End Sub

    Private Sub Gridregion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Gridregion.SelectedIndexChanged
        Dim i As Integer
        Dim procid As String
        Dim row As GridViewRow = Me.Gridregion.SelectedRow
        procid = row.Cells(1).Text

        Dim mybranch As String
        mybranch = procid

        Session.Remove("BranchCode")

        Session("BranchCode") = mybranch

        le.BranchCode = mybranch


        Session.Remove("AndBranchCode")

        Session("AndBranchCode") = " and BranchCode = '" + mybranch + "'"


        le.AndBranchCode = " and BranchCode = '" + mybranch + "'"



        Session.Remove("whereBranchCode")

        Session("whereBranchCode") = " where BranchCode = '" + mybranch + "'"

        le.WhereBranchCode = " Where BranchCode = '" + mybranch + "'"

        Session.Remove("region")
        Session("region") = le.ReturnConditionNA("cmstblcoy", "region", le.WhereBranchCode)
        Response.Redirect("aboutintranet.aspx")

    End Sub

    Private Sub srcDelete()
        'Try
        '' Dim xFields As String = "[ID],[Drug] as [DISPENSED DRUGS],[Qty] as [QTY],[UNIT],[ARID],[SalesID] as [DSP ID],[Sprice] as [UNIT PRICE (N)],(Qty * Sprice) as [PRICE (N)]"
        'le.AndCondition = ""

        Dim cond As String
        Dim xFields As String = "BranchCode as [Code],CompanyName as [Terminal Name],address as [Terminal Address], region as [Region],city as [City], State as [State]"


        'If le.MustbeBOOL("", "", "", "", "", "") = False Then
        '    cond = le.WheredCode
        '    le.DT = le.ReportSelectedDataTable("cmstblcoy", xFields, cond)
        'ElseIf le.MustbeBOOL("GMgr", "", "", "", "", "") = False Then
        '    cond = le.WheredCode
        '    le.DT = le.ReportSelectedDataTable("cmstblcoy", xFields, cond)
        'ElseIf le.MustbeBOOL("", "", "RMgr", "", "", "") = False Then
        '    'cond = le.WheredCode
        '    le.DT = le.ReportSelectedDataTable("cmstblcoy", xFields, " where Region='" & le.UserRegion(le.USerName) & "'")
        'End If


        If le.IsRoleBOOL("Gmgr", "", "", "", "", "") = True Then
            cond = le.WheredCode
            le.DT = le.ReportSelectedDataTable("cmstblcoy", xFields, cond)
            Me.Gridregion.DataSource = le.DT
            Me.Gridregion.DataBind()
        ElseIf le.IsRoleBOOL("", "", "Rmgr", "", "", "") = True Then
            cond = " where Region='" & le.UserRegion(le.USerName) & "'"
            le.DT = le.ReportSelectedDataTable("cmstblcoy", xFields, cond)
            Me.Gridregion.DataSource = le.DT
            Me.Gridregion.DataBind()
        End If




    End Sub

End Class