Imports System.IO
Imports System.Collections.Generic
Public Class managebranch
    Inherits System.Web.UI.Page
    Dim le As New mfa1
    Dim SavePath As String
    Dim filename As String
    Dim fsize As Double



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        le.Checksession()
        le.ReplaceText(Me)

        'Me.BtnDelete.Attributes.Add("onclick", "window.showModalDialog('ResultTemplate.aspx','mywindow','width=760,height=600,scrollbar=yes')")
        Me.BtnDelete.Attributes.Add("onclick", "return confirm('Are You Sure You Want To Delete This? ');")

        If Me.IsPostBack = False Then
            Me.lblARID.Visible = False
            Me.srcDelete()


            Me.SELCASE()
            '  srcDelete2()
            le.ListAnyThing(Dropregion, "tblregions", "nregion", le.WheredCode)
            le.MustBe("GMgr", "", "", "", "", "")
            le.ActivityANDEmailLog("Visited  branch update/ registration page. ")

            If le.MustbeBOOL("GMgr", "", "", "", "", "") = False Then
                btnRegister.Enabled = False
                ' BtnDelete.Enabled = False
            End If

            If le.IsJohn(le.USerName) = False Then

                BtnDelete.Enabled = False
            End If

        End If
    End Sub


    Private Sub generateBranchCode()
asd:
        Me.lblARID.Text = ""
        Dim joint As String
        joint = le.GetRandom(1, 1000).ToString
        If le.ReturnCheckCondition("cmstblCoy", "BranchCode", " where BranchCode='" & joint & "'") = False Then
            Me.lblARID.Text = joint

        Else
            GoTo asd
        End If
    End Sub
    Private Sub pRegister()

        'Try
        Me.generateBranchCode()

        Dim xFields As String = "CompanyName,Address,phone,email,region,city,state,RC,Recordedby,dcode,BranchCode"
        Dim xTexts As String = "'" & Me.txtcompanyname.Text & "'," &
            "'" & Me.txtaddress.Text & "'," &
            "'" & Me.txtphone.Text & "'," &
            "'" & Me.txtemail.Text & "'," &
             "'" & Me.Dropregion.Text & "'," &
              "'" & Me.dropLGAs.Text & "'," &
                   "'" & Me.dropnStates.Text & "'," &
                   "'" & Me.txtRC.Text & "'," &
          "'" & CType(Session("Username"), String) & "'," &
          "'" & CType(Session("dCode"), String) & "'," &
          "'" & Me.lblARID.Text & "'"
        'le.DH & Now.Date & le.DH & "," &
        'le.DH & Now & le.DH
        le.InsertData("cmstblCoy", xFields, xTexts)

        Session.Remove("BranchCode")
        Session("BranchCode") = lblARID.Text

        ' Me.RegCanBranch()
        le.ActivityANDEmailLog("Registered a new  branch  called " + Me.txtcompanyname.Text)
        Me.srcDelete()

        le.ClearText(Me)
        If Me.CheckEvidence.Checked = True Then
            'move to file upload
            'get the id from the arid
            divAll.Visible = False
            divUpload.Visible = True
            Me.btnSearch.Enabled = False

            Me.lblid.Text = le.ReturnConditionNA("cmstblCoy", "id", " where branchcode='" & Me.lblARID.Text & "'")
            Me.lblevidence.Text = le.ReturnConditionNA("cmstblCoy", "companyname", " where id=" & lblid.Text)
            le.lmsg = "Saved Successfully! Click OK to upload logo."
            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)

        Else
            le.lmsg = "Saved Successfully!, however, you can use view link to upload logo later."
            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)

        End If
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub srcDelete()
        'Try
        Dim xFields As String = "Id As [Br ID],BranchCode as [BC],CompanyName As [Company Name],address As [Address], region as [Region],city as [City], State as [State]" ', (select fullname from cmstbllog where branchcode=cmstblcoy.branchcode and BMgr='Yes') as [Terminal Manager],(select phone from cmstbllog where branchcode=cmstblcoy.branchcode and BMgr='Yes') as [Terminal Manager Phone],(select Email from cmstbllog where branchcode=cmstblcoy.branchcode and BMgr='Yes') as [Terminal Manager Email]"
        le.AndCondition = ""
        If le.IsJohn(CType(Session("Username"), String)) = True Then
            le.AndCondition = ""
        Else

            le.AndCondition = le.WheredCode
        End If
        le.DT = le.ReportSelectedDataTable("cmstblCoy", xFields, le.AndCondition)


        Me.Griduser.DataSource = le.DT
        Me.Griduser.DataBind()
        Dim zxt As String
        Dim t As Integer
        For t = 0 To Griduser.Rows.Count - 1
            zxt = Griduser.Rows(t).Cells(2).Text
            Dim link1 As HyperLink = DirectCast(Griduser.Rows.Item(t).FindControl("Link1"), HyperLink)
            Dim link2 As LinkButton = DirectCast(Griduser.Rows.Item(t).FindControl("link2"), LinkButton)

            If le.ReturnConditionNA("cmstblCoy", "Photo", " where id=" & zxt & "").Length > 3 Then
                link1.Visible = True
                link1.ToolTip = le.ReturnConditionNA("cmstblCoy", "Photo", " where id=" & zxt & "").ToString
                link1.NavigateUrl = "photo/logo/" & le.ReturnFromQuery("select photo from cmstblCoy where id=" + zxt + "").ToString
                link2.Text = "Replace"
                link1.Text = " Yes"
                link1.ToolTip = "View the file"
                link1.ForeColor = Drawing.Color.DarkGreen
            Else
                'link1.Visible = False
                link1.Text = "No"
                link1.ToolTip = "Click UPLOAD at your right to attach a logo"
                link1.ForeColor = Drawing.Color.Red
                link2.Text = "Upload"
            End If
        Next

    End Sub

    Protected Sub btnRegister_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        If Me.txtcompanyname.Text.Trim.Length > 0 And Me.dropLGAs.Text.Trim.Length > 0 And Me.dropnStates.Text.Trim.Length > 0 And Me.Dropregion.Text.Trim.Length > 0 Then


            Me.pRegister()


            'End If
        Else
            le.lmsg = "Check your entries for company, city and state, and TRY AGAIN."
            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)

        End If
    End Sub


    Private Sub activate()
        Me.btnRegister.Visible = False
        Me.CheckEvidence.Visible = False
        Me.btnUpdate.Visible = True
        Me.BtnDelete.Visible = True
        '  Me.txtExpenditure.Enabled = False
        Me.btnSearch.Enabled = False
        'ensure that the grid for search result is empty
        GridUser2.DataSource = Nothing
        GridUser2.DataBind()
    End Sub
    Private Sub deactivate()
        Me.btnRegister.Visible = True
        Me.CheckEvidence.Visible = True
        Me.btnUpdate.Visible = False
        Me.BtnDelete.Visible = False
        ' Me.txtExpenditure.Enabled = True
        Me.btnSearch.Enabled = True

        le.ClearText(Me)
    End Sub
    Private Sub Updated()
        Try
            le.AndCondition = " where [id] = " & Me.lblid.Text.Trim

            Dim xTexts As String = " [CompanyName] = '" & txtcompanyname.Text & "'," &
            " [Address] = '" & Me.txtaddress.Text & "'," &
            " [phone] = '" & Me.txtphone.Text & "'," &
             " [email] = '" & Me.txtemail.Text & "'," &
             " [region] = '" & Me.Dropregion.Text & "'," &
                " [city] = '" & Me.dropLGAs.Text & "'," &
                 " [State] = '" & Me.dropnStates.Text & "'," &
                  " [RC] = '" & Me.txtRC.Text & "'"

            ' check that the unit to be changed is not a whole sale unit, and it has not been Damaged in sales.
            If Me.txtcompanyname.Text.Trim.Length > 0 And Me.dropLGAs.Text.Trim.Length > 0 = True Then
                le.UpdateData("cmstblCoy", xTexts, le.AndCondition)
                le.lmsg = "Updated Successfully!"
                ' RegExist = False
                le.ActivityANDEmailLog("Updated branch data for " + Me.txtcompanyname.Text)
                Me.srcDelete()
                deactivate()
                ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
                'end check

            Else
                le.lmsg = "Check your entries for company name, city and state, and TRY AGAIN."
                ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
                Exit Sub
            End If


        Catch ex As Exception
            le.forceclosedatabase()
        End Try
    End Sub
    Protected Sub BtnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If Me.txtcompanyname.Text.Trim.Length > 0 And Me.dropLGAs.Text.Trim.Length > 0 = True Then


            Me.Updated()


            'End If
        Else
            le.lmsg = "Check your entries for company name, city and state, and TRY AGAIN."
            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)

        End If
    End Sub



    Protected Sub BtnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Try

            If le.IsJohn(le.USerName) = True Then
                DeleteExistingEvidence()
                le.ReturnDelete("cmstblCoy", " where id=" & Me.lblid.Text & "" + le.AndBranchCode)

                Me.srcDelete()
                le.lmsg = "Deletion Completed Successfully."
                le.ActivityANDEmailLog("Deleted branch entry data for " + Me.txtcompanyname.Text)
                deactivate()
                ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)

            Else
                le.lmsg = "Insufficient right.Deletion Not Possible."
                ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg.ToString & "');", True)
                Exit Sub
            End If
        Catch ex As Exception
            le.forceclosedatabase()
        End Try

    End Sub

    Protected Sub BtnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClear.Click
        Response.Redirect("managebranch.aspx")
    End Sub







    Protected Sub GridUser_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Griduser.SelectedIndexChanged
        Try


            Dim i As Integer
            Dim procid As String
            Dim row As GridViewRow = Me.Griduser.SelectedRow
            procid = row.Cells(2).Text
            Me.lblid.Text = procid.ToString


            Me.txtcompanyname.Text = le.ReturnConditionNA("cmstblCoy", "companyname", " where id=" + procid)
            'load the wholesale units
            ' AndCond add the retail unit 
            ' before selecting the stored unit


            Me.txtaddress.Text = le.ReturnConditionNA("cmstblCoy", "address", " where id=" + procid)
            Me.txtphone.Text = le.ReturnConditionNA("cmstblCoy", "phone", " where id=" + procid)
            Me.txtemail.Text = le.ReturnConditionNA("cmstblCoy", "email", " where id=" + procid)
            Me.txtRC.Text = le.ReturnConditionNA("cmstblCoy", "rc", " where id=" + procid)

            'show dept and staff
            Me.dropnStates.Text = le.ReturnConditionNA("cmstblCoy", "state", " where id=" + procid)
            SELCASE()

            Me.dropLGAs.Text = le.ReturnConditionNA("cmstblCoy", "city", " where id=" + procid)


            le.ActivityANDEmailLog("Started edit on branch data for " + Me.txtcompanyname.Text)
            activate()
            Me.Dropregion.Text = le.ReturnConditionNA("cmstblCoy", "region", " where id=" + procid)

        Catch ex As Exception

        End Try
    End Sub
    Private Sub ProductEditMode()
        'load the wholesale units
        ' AndCond add the retail unit 
        ' before selecting the stored unit
        Dim procid As String
        procid = lblid.Text

        Me.txtcompanyname.Text = le.ReturnConditionNA("cmstblCoy", "companyname", " where id=" + procid)
        'load the wholesale units
        ' AndCond add the retail unit 
        ' before selecting the stored unit
        le.ListAnyThing(Dropregion, "tblregions", "nregion", le.WheredCode)

        Me.txtaddress.Text = le.ReturnConditionNA("cmstblCoy", "address", " where id=" + procid)
        Me.txtphone.Text = le.ReturnConditionNA("cmstblCoy", "phone", " where id=" + procid)
        Me.txtemail.Text = le.ReturnConditionNA("cmstblCoy", "email", " where id=" + procid)
        Me.Dropregion.Text = le.ReturnConditionNA("cmstblCoy", "region", " where id=" + procid)
        Me.txtRC.Text = le.ReturnConditionNA("cmstblCoy", "rc", " where id=" + procid)

        'show dept and staff
        Me.dropnStates.Text = le.ReturnConditionNA("cmstblCoy", "state", " where id=" + procid)
        SELCASE()

        Me.dropLGAs.Text = le.ReturnConditionNA("cmstblCoy", "city", " where id=" + procid)


        le.ActivityANDEmailLog("Started edit on branch data for " + Me.txtcompanyname.Text)
        activate()
    End Sub
    'Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)


    '    GridUser2.PageIndex = e.NewPageIndex
    '    Me.srcDelete2()

    'End Sub
    'Private Sub srcDelete2()
    '    'Try
    '    '' Dim xFields As String = "[ID],[Drug] as [DISPENSED DRUGS],[Qty] as [QTY],[Product],[ARID],[SalesID] as [DSP ID],[Sprice] as [Product PRICE (N)],(Qty * Sprice) as [PRICE (N)]"
    '    Dim xFields As String = "recordtime as [Date],Expenditure as [Expenditure],Cost as [Cost]"

    '    le.AndCondition =le.WhereBranchCode
    '    le.DT = le.ReportSelectedDataTable("cmstblCoy", xFields, le.AndCondition)

    '    Me.GridUser2.DataSource = le.DT
    '    Me.GridUser2.DataBind()



    'End Sub
    'Protected Sub SortGriduser2(sender As Object, e As GridViewSortEventArgs)


    '    Dim xFields As String = "recordtime as [Date],Expenditure as [Expenditure],Category as [Category],cost as [Cost]"

    '    le.AndCondition =le.WhereBranchCode
    '    le.DT = le.ReportSelectedDataTable("cmstblCoy", xFields, le.AndCondition)

    '    'Me.GridUser2.DataSource = le.DT
    '    'Me.GridUser2.DataBind()

    '    Dim dv As System.Data.DataView = New System.Data.DataView(le.DT)


    '    dv.Sort = e.SortExpression '+ " " + (SortDirection = SortDirection.Ascending,  "DESC", "ASC")


    '    GridUser2.DataSource = dv
    '    GridUser2.DataBind()

    'End Sub



    ''Private Sub GridUser2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridUser2.SelectedIndexChanged
    ''    Dim i As Integer
    ''    Dim procid As String
    ''    Dim row As GridViewRow = Me.GridUser2.SelectedRow
    ''    procid = row.Cells(1).Text
    ''    Me.DropProduct.Text = procid

    ''    le.ListAnyThing(Dropunit, "ProductUnitPrice", "unit", " where Product='" & procid & "'")
    ''    'add the retail unit
    ''    Me.Dropunit.Items.Add(le.ProductRetailUnit(procid))
    ''    GridUser2.DataSource = Nothing

    ''    GridUser2.DataBind()
    ''End Sub

    'Private Sub Dropdept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Dropdept.SelectedIndexChanged
    '    'Load departments and staff to receive the goods

    '    le.ListAnyThing(Me.DropStaff, "cmstbllog", "Fullname", " where Dept='" + Me.Dropdept.Text + "' and edits='Yes'" & le.AndBranchCode)

    'End Sub

    Private Sub Griduser_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Griduser.RowCommand
        If e.CommandName = "upload" Then
            'Determine the RowIndex of the Row whose Button was clicked.
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            'Reference the GridView Row.
            Dim row As GridViewRow = Griduser.Rows(rowIndex)

            Dim id As String = row.Cells(2).Text
            divAll.Visible = False
            divUpload.Visible = True
            Me.btnSearch.Enabled = False
            Me.lblid.Text = id.ToString
            Me.lblevidence.Text = le.ReturnConditionNA("cmstblCoy", "companyname", " where id=" & id)
        End If

    End Sub



    Private Sub upload()


        ' Specify the path on the server to
        ' save the uploaded file to.
        DeleteExistingEvidence()

        Dim savePath As String = Me.Server.MapPath("photo/logo/")

        ' Before attempting to save the file, verify
        ' that the FileUpload control contains a file.d
        If (FileUpload1.HasFile) Then

            ' Get the name of the file to upload.
            filename = Server.HtmlEncode(FileUpload1.FileName)

            ' Get the extension of the uploaded file.
            If filename.Length < 2000000 Then
                Dim extension As String = System.IO.Path.GetExtension(filename)

                ' Allow only pictures
                ' to be uploaded.
                'If (extension = ".gif") Or (extension = ".jpeg") Or (extension = ".png") Or (extension = ".jpg") Then

                ' Append the name of the file to upload to the path.
                savePath += Me.lblid.Text + filename
                ' savePath += le.BranchCode + extension
                Dim fileSize As Integer = FileUpload1.PostedFile.ContentLength

                ' Allow only files less than 2,100,000 bytes (approximately 2 MB) to be uploaded.
                If (fileSize < 2000000) Then

                    'save the file
                    'Me.lblpic.Text = (CType(Session("nID"), String) + filename).ToString
                    'Me.UpdatePicture()
                    '  updateFileData()


                    FileUpload1.SaveAs(savePath)

                    le.UpdateData("cmstblCoy", "Photo='" & Me.lblid.Text + filename & "'", " where id=" & CInt(Me.lblid.Text) & "" & le.AndBranchCode)
                    divAll.Visible = True
                    divUpload.Visible = False
                    '  Me.btnSearch.Enabled = True

                    ProductEditMode()
                    Me.srcDelete()
                    le.lmsg = " Logo Uploaded Successfully. Review your entries and update."
                    ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg & "');", True)
                    '  Response.Redirect("cmstblCoy.aspx")


                    'If (fileName.ToString.Contains("'")) Then

                    '    Lblmsg.Text = "Apostrophe -'- Is Not Allowed in the Image Name.Please,Rename the Picture and Upload Again. "
                    '    ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & Lblmsg.Text & "');", True)

                    'Else

                    'End If

                Else


                    le.lmsg = "The Size exceeded the maximum uploadable size"
                    ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg & "');", True)


                End If

                'Else
                '    ' Notify the user why their file was not uploaded.

                '    le.lmsg = "Your file was not uploaded because " + _
                '                             "it does not have a right extension"
                '    ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg & "');", True)

                'End If
            Else
                le.lmsg = "The File Name Is Too Long."
                ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg & "');", True)

            End If
        Else
            le.lmsg = "You did not specify a file to upload."
            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg & "');", True)

        End If
    End Sub
    Private Sub DeleteExistingEvidence()
        Try

            'get the file in the databse for photo

            Dim ph As String
            ph = le.ReturnConditionNA("cmstblCoy", "Photo", " where id=" & CInt(Me.lblid.Text))
            Dim fn As String = Request.MapPath("photo/logo")
            fn &= "/" & ph


            Dim fs As New FileInfo(fn)

            If fs.Exists = True Then
                IO.File.Delete(fn)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        upload()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click

        Response.Redirect("managebranch.aspx")
    End Sub

    Protected Sub dropnStates_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dropnStates.SelectedIndexChanged
        Me.SELCASE()
    End Sub
    Private Sub SELCASE()
        Select Case dropnStates.SelectedIndex
            Case 0
                abia()
            Case 1
                FTC()
            Case 2
                adamawa()
            Case 3
                akwaibom()
            Case 4
                anambra()
            Case 5
                bauchi()
            Case 6
                bayelsa()
            Case 7
                benue()
            Case 8
                bornu()
            Case 9
                crossriver()
            Case 10
                delta()
            Case 11
                ebonye()
            Case 12
                edo()
            Case 13
                ekiti()
            Case 14
                enugu()
            Case 15
                FTC()
            Case 16
                gombe()
            Case 17
                imo()
            Case 18
                jigawa()
            Case 19
                kaduna()
            Case 20
                kano()
            Case 21
                katsina()
            Case 22
                kebbi()
            Case 23
                kogi()
            Case 24
                kwara()
            Case 25
                lagos()
            Case 26
                nasarawa()
            Case 27
                niger()
            Case 28
                ogun()
            Case 29
                ondo()
            Case 30
                osun()
            Case 31
                oyo()
            Case 32
                plateau()
            Case 33
                rivers()
            Case 34
                sokoto()
            Case 35
                taraba()
            Case 36
                yobe()
            Case 37
                zamfarawa()
        End Select
    End Sub
    Public Sub FTC()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Gwagwalada")
        dropLGAs.Items.Add("Kuje")
        dropLGAs.Items.Add("Abaji")
        dropLGAs.Items.Add("Abuja Municipal")
        dropLGAs.Items.Add("Bwari")
        dropLGAs.Items.Add("Kwali")
    End Sub

    Public Sub abia()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Aba North")
        dropLGAs.Items.Add("Aba South")
        dropLGAs.Items.Add("Arochukwu")
        dropLGAs.Items.Add("Bende")
        dropLGAs.Items.Add("Ikwuano")
        dropLGAs.Items.Add("Isiala-Ngwa North")
        dropLGAs.Items.Add("Isiala-Ngwa South")
        dropLGAs.Items.Add("Isuikwato")
        dropLGAs.Items.Add("Obi Nwa")
        dropLGAs.Items.Add("Ohafia")
        dropLGAs.Items.Add("Osisioma")
        dropLGAs.Items.Add("Ngwa")
        dropLGAs.Items.Add("Ugwunagbo")
        dropLGAs.Items.Add("Ukwa East")
        dropLGAs.Items.Add("Ukwa West")
        dropLGAs.Items.Add("Umuahia North")
        dropLGAs.Items.Add("Umuahia South")
        dropLGAs.Items.Add("Umu-Neochi")
    End Sub

    Public Sub adamawa()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Demsa")
        dropLGAs.Items.Add("Fufore")
        dropLGAs.Items.Add("Ganaye")
        dropLGAs.Items.Add("Gireri")
        dropLGAs.Items.Add("Gombi")
        dropLGAs.Items.Add("Guyuk")
        dropLGAs.Items.Add("Hong")
        dropLGAs.Items.Add("Jad")
        dropLGAs.Items.Add("Lamurde")
        dropLGAs.Items.Add("Madagali")
        dropLGAs.Items.Add("Maiha")
        dropLGAs.Items.Add("Mayo-Belwa")
        dropLGAs.Items.Add("Michika")
        dropLGAs.Items.Add("Mubi North")
        dropLGAs.Items.Add("Mubi South")
        dropLGAs.Items.Add("Numan")
        dropLGAs.Items.Add("Shelleng")
        dropLGAs.Items.Add("Song")
        dropLGAs.Items.Add("Toungo")
        dropLGAs.Items.Add("Yola North")
        dropLGAs.Items.Add("Yola South")
    End Sub

    Public Sub akwaibom()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Abak")
        dropLGAs.Items.Add("Eastern Obolo")
        dropLGAs.Items.Add("Eket")
        dropLGAs.Items.Add("Esit Eket")
        dropLGAs.Items.Add("Essien Udim")
        dropLGAs.Items.Add("Etim Ekpo")
        dropLGAs.Items.Add("Etinan")
        dropLGAs.Items.Add("Ibeno")
        dropLGAs.Items.Add("Ibesikp Asutan")
        dropLGAs.Items.Add("Ibiono Ibom")
        dropLGAs.Items.Add("Ika")
        dropLGAs.Items.Add("Ikono")
        dropLGAs.Items.Add("Ikot Abasi")
        dropLGAs.Items.Add("Ikot Ekpene")
        dropLGAs.Items.Add("Ini")
        dropLGAs.Items.Add("Itu")
        dropLGAs.Items.Add("Mbo")
        dropLGAs.Items.Add("Mkpat Enin")
        dropLGAs.Items.Add("Nsit Atai")
        dropLGAs.Items.Add("Nsit Ibom")
        dropLGAs.Items.Add("Nsit Ubium")
        dropLGAs.Items.Add("Obot Akara")
        dropLGAs.Items.Add("Okobo")
        dropLGAs.Items.Add("Onna")
        dropLGAs.Items.Add("Oron")
        dropLGAs.Items.Add("Oruk Anam")
        dropLGAs.Items.Add("Udung Uko")
        dropLGAs.Items.Add("Ukanafun")
        dropLGAs.Items.Add("Uruan")
        dropLGAs.Items.Add("Urue-Offong/Oruko")
        dropLGAs.Items.Add("Uyo")
    End Sub

    Public Sub anambra()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Aguata")
        dropLGAs.Items.Add("Anambra East")
        dropLGAs.Items.Add("Anambra West")
        dropLGAs.Items.Add("Anaocha")
        dropLGAs.Items.Add("Awka North")
        dropLGAs.Items.Add("Awka South")
        dropLGAs.Items.Add("Ayamelum")
        dropLGAs.Items.Add("Dunukofia")
        dropLGAs.Items.Add("Ekwusigo")
        dropLGAs.Items.Add("Idemili North")
        dropLGAs.Items.Add("Idemili South")
        dropLGAs.Items.Add("Ihiala")
        dropLGAs.Items.Add("Njikoka")
        dropLGAs.Items.Add("Nnewi North")
        dropLGAs.Items.Add("Nnewi South")
        dropLGAs.Items.Add("Ogbaru")
        dropLGAs.Items.Add("Onitsha North")
        dropLGAs.Items.Add("Onitsha South")
        dropLGAs.Items.Add("Orumba North")
        dropLGAs.Items.Add("Orumba South")
        dropLGAs.Items.Add("Oyi")
    End Sub

    Public Sub bauchi()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Alkaleri")
        dropLGAs.Items.Add("Bauchi")
        dropLGAs.Items.Add("Bogoro")
        dropLGAs.Items.Add("Damban")
        dropLGAs.Items.Add("Darazo")
        dropLGAs.Items.Add("Dass")
        dropLGAs.Items.Add("Ganjuwa")
        dropLGAs.Items.Add("Giade")
        dropLGAs.Items.Add("Itas/Gadau")
        dropLGAs.Items.Add("Jama'are")
        dropLGAs.Items.Add("Katagum")
        dropLGAs.Items.Add("Kirfi")
        dropLGAs.Items.Add("Misau")
        dropLGAs.Items.Add("Ningi")
        dropLGAs.Items.Add("Shira")
        dropLGAs.Items.Add("Tafawa-Balewa")
        dropLGAs.Items.Add("Toro")
        dropLGAs.Items.Add("Warji")
        dropLGAs.Items.Add("Zaki")
    End Sub

    Public Sub bayelsa()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Brass")
        dropLGAs.Items.Add("Ekeremor")
        dropLGAs.Items.Add("Kolokuma/Opokuma")
        dropLGAs.Items.Add("Nembe")
        dropLGAs.Items.Add("Ogbia")
        dropLGAs.Items.Add("Sagbama")
        dropLGAs.Items.Add("Southern Jaw")
        dropLGAs.Items.Add("Yenegoa")
    End Sub

    Public Sub benue()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Ado")
        dropLGAs.Items.Add("Agatu")
        dropLGAs.Items.Add("Apa")
        dropLGAs.Items.Add("Buruku")
        dropLGAs.Items.Add("Gboko")
        dropLGAs.Items.Add("Guma")
        dropLGAs.Items.Add("Gwer East")
        dropLGAs.Items.Add("Gwer West")
        dropLGAs.Items.Add("Katsina-Ala")
        dropLGAs.Items.Add("Konshisha")
        dropLGAs.Items.Add("Kwande")
        dropLGAs.Items.Add("Logo")
        dropLGAs.Items.Add("Makurdi")
        dropLGAs.Items.Add("Obi")
        dropLGAs.Items.Add("Ogbadibo")
        dropLGAs.Items.Add("Oju")
        dropLGAs.Items.Add("Okpokwu")
        dropLGAs.Items.Add("Ohimini")
        dropLGAs.Items.Add("Oturkpo")
        dropLGAs.Items.Add("Tarka")
        dropLGAs.Items.Add("Ukum")
        dropLGAs.Items.Add("Ushongo")
        dropLGAs.Items.Add("Vandeikya")
    End Sub

    Public Sub bornu()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Abadam")
        dropLGAs.Items.Add("Askira/Uba")
        dropLGAs.Items.Add("Bama")
        dropLGAs.Items.Add("Bayo")
        dropLGAs.Items.Add("Biu")
        dropLGAs.Items.Add("Chibok")
        dropLGAs.Items.Add("Damboa")
        dropLGAs.Items.Add("Dikwa")
        dropLGAs.Items.Add("Gubio")
        dropLGAs.Items.Add("Guzamala")
        dropLGAs.Items.Add("Gwoza")
        dropLGAs.Items.Add("Hawul")
        dropLGAs.Items.Add("Jere")
        dropLGAs.Items.Add("Kaga")
        dropLGAs.Items.Add("Kala/Balge")
        dropLGAs.Items.Add("Konduga")
        dropLGAs.Items.Add("Kukawa")
        dropLGAs.Items.Add("Kwaya Kusar")
        dropLGAs.Items.Add("Mafa")
        dropLGAs.Items.Add("Magumeri")
        dropLGAs.Items.Add("Maiduguri")
        dropLGAs.Items.Add("Marte")
        dropLGAs.Items.Add("Mobbar")
        dropLGAs.Items.Add("Monguno")
        dropLGAs.Items.Add("Ngala")
        dropLGAs.Items.Add("Nganzai")
        dropLGAs.Items.Add("Shani")
    End Sub

    Public Sub crossriver()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Akpabuyo")
        dropLGAs.Items.Add("Odukpani")
        dropLGAs.Items.Add("Akamkpa")
        dropLGAs.Items.Add("Biase")
        dropLGAs.Items.Add("abi")
        dropLGAs.Items.Add("Ikom")
        dropLGAs.Items.Add("Yarkur")
        dropLGAs.Items.Add("Odubra")
        dropLGAs.Items.Add("Boki")
        dropLGAs.Items.Add("Ogoja")
        dropLGAs.Items.Add("Yala")
        dropLGAs.Items.Add("Obanliku")
        dropLGAs.Items.Add("Obudu")
        dropLGAs.Items.Add("Calabar South")
        dropLGAs.Items.Add("Etung")
        dropLGAs.Items.Add("Bekwara")
        dropLGAs.Items.Add("Bakassi")
        dropLGAs.Items.Add("Calabar Municipality")
    End Sub

    Public Sub delta()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Oshimili")
        dropLGAs.Items.Add("Aniocha")
        dropLGAs.Items.Add("Aniocha South")
        dropLGAs.Items.Add("Ika South")
        dropLGAs.Items.Add("Ika North-East")
        dropLGAs.Items.Add("Ndokwa West")
        dropLGAs.Items.Add("Ndokwa East")
        dropLGAs.Items.Add("Isoko south")
        dropLGAs.Items.Add("Isoko North")
        dropLGAs.Items.Add("Bomadi")
        dropLGAs.Items.Add("Burutu")
        dropLGAs.Items.Add("Ughelli South")
        dropLGAs.Items.Add("Ughelli North")
        dropLGAs.Items.Add("Ethiope West")
        dropLGAs.Items.Add("Ethiope East")
        dropLGAs.Items.Add("Sapele")
        dropLGAs.Items.Add("Okpe")
        dropLGAs.Items.Add("Warri North")
        dropLGAs.Items.Add("Warri South")
        dropLGAs.Items.Add("Uvwie")
        dropLGAs.Items.Add("Udu")
        dropLGAs.Items.Add("Warri Central")
        dropLGAs.Items.Add("Ukwani")
        dropLGAs.Items.Add("Oshimili North")
        dropLGAs.Items.Add("Patani")
    End Sub

    Public Sub ebonye()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Afikpo South")
        dropLGAs.Items.Add("Afikpo North")
        dropLGAs.Items.Add("Onicha")
        dropLGAs.Items.Add("Ohaozara")
        dropLGAs.Items.Add("Abakaliki")
        dropLGAs.Items.Add("Ishielu")
        dropLGAs.Items.Add("lkwo")
        dropLGAs.Items.Add("Ezza")
        dropLGAs.Items.Add("Ezza South")
        dropLGAs.Items.Add("Ohaukwu")
        dropLGAs.Items.Add("Ebonyi")
        dropLGAs.Items.Add("Ivo")
    End Sub

    Public Sub edo()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Esan North-East")
        dropLGAs.Items.Add("Esan Central")
        dropLGAs.Items.Add("Esan West")
        dropLGAs.Items.Add("Egor")
        dropLGAs.Items.Add("Ukpoba")
        dropLGAs.Items.Add("Central")
        dropLGAs.Items.Add("Etsako Central")
        dropLGAs.Items.Add("Igueben")
        dropLGAs.Items.Add("Oredo")
        dropLGAs.Items.Add("Ovia SouthWest")
        dropLGAs.Items.Add("Ovia South-East")
        dropLGAs.Items.Add("Orhionwon")
        dropLGAs.Items.Add("Uhunmwonde")
        dropLGAs.Items.Add("Etsako East")
        dropLGAs.Items.Add("Esan South-East")
    End Sub

    Public Sub gombe()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Akko")
        dropLGAs.Items.Add("Balanga")
        dropLGAs.Items.Add("Billiri")
        dropLGAs.Items.Add("Dukku")
        dropLGAs.Items.Add("Kaltungo")
        dropLGAs.Items.Add("Kwami")
        dropLGAs.Items.Add("Shomgom")
        dropLGAs.Items.Add("Funakaye")
        dropLGAs.Items.Add("Gombe")
        dropLGAs.Items.Add("Nafada/Bajoga")
        dropLGAs.Items.Add("Yamaltu/Delta")
    End Sub

    Public Sub ekiti()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Ado")
        dropLGAs.Items.Add("Ekiti-East")
        dropLGAs.Items.Add("Ekiti-West")
        dropLGAs.Items.Add("Emure/Ise/Orun")
        dropLGAs.Items.Add("Ekiti South-West")
        dropLGAs.Items.Add("Ikare")
        dropLGAs.Items.Add("Irepodun")
        dropLGAs.Items.Add("Ijero")
        dropLGAs.Items.Add("Ido/Osi")
        dropLGAs.Items.Add("Oye")
        dropLGAs.Items.Add("Ikole")
        dropLGAs.Items.Add("Moba")
        dropLGAs.Items.Add("Gbonyin")
        dropLGAs.Items.Add("Efon")
        dropLGAs.Items.Add("Ise/Orun")
        dropLGAs.Items.Add("Ilejemeje")
    End Sub

    Public Sub enugu()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Enugu South")
        dropLGAs.Items.Add("Igbo-Eze South")
        dropLGAs.Items.Add("Enugu North")
        dropLGAs.Items.Add("Nkanu")
        dropLGAs.Items.Add("Udi Agwu")
        dropLGAs.Items.Add("Oji-River")
        dropLGAs.Items.Add("Ezeagu")
        dropLGAs.Items.Add("IgboEze North")
        dropLGAs.Items.Add("Isi-Uzo")
        dropLGAs.Items.Add("Nsukka")
        dropLGAs.Items.Add("Igbo-Ekiti")
        dropLGAs.Items.Add("Uzo-Uwani")
        dropLGAs.Items.Add("Enugu Eas")
        dropLGAs.Items.Add("Aninri")
        dropLGAs.Items.Add("Nkanu East")
        dropLGAs.Items.Add("Udenu")
    End Sub

    Public Sub imo()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Aboh-Mbaise")
        dropLGAs.Items.Add("Ahiazu-Mbaise")
        dropLGAs.Items.Add("Ehime-Mbano")
        dropLGAs.Items.Add("Ezinihitte")
        dropLGAs.Items.Add("Ideato North")
        dropLGAs.Items.Add("Ideato South")
        dropLGAs.Items.Add("Ihitte/Uboma")
        dropLGAs.Items.Add("Ikeduru")
        dropLGAs.Items.Add("Isiala Mbano")
        dropLGAs.Items.Add("Isu")
        dropLGAs.Items.Add("Mbaitoli")
        dropLGAs.Items.Add("Mbaitoli")
        dropLGAs.Items.Add("Ngor-Okpala")
        dropLGAs.Items.Add("Njaba")
        dropLGAs.Items.Add("Nwangele")
        dropLGAs.Items.Add("Nkwerre")
        dropLGAs.Items.Add("Obowo")
        dropLGAs.Items.Add("Oguta")
        dropLGAs.Items.Add("Ohaji/Egbema")
        dropLGAs.Items.Add("Okigwe")
        dropLGAs.Items.Add("Orlu")
        dropLGAs.Items.Add("Orsu")
        dropLGAs.Items.Add("Oru East")
        dropLGAs.Items.Add("Oru West")
        dropLGAs.Items.Add("Owerri-Municipal")
        dropLGAs.Items.Add("Owerri North")
        dropLGAs.Items.Add("Owerri West")
    End Sub

    Public Sub jigawa()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Auyo")
        dropLGAs.Items.Add("Babura")
        dropLGAs.Items.Add("Birni Kudu")
        dropLGAs.Items.Add("Biriniwa")
        dropLGAs.Items.Add("Buji")
        dropLGAs.Items.Add("Dutse")
        dropLGAs.Items.Add("Gagarawa")
        dropLGAs.Items.Add("Garki")
        dropLGAs.Items.Add("Gumel")
        dropLGAs.Items.Add("Guri")
        dropLGAs.Items.Add("Gwaram")
        dropLGAs.Items.Add("Gwiwa")
        dropLGAs.Items.Add("Hadejia")
        dropLGAs.Items.Add("Jahun")
        dropLGAs.Items.Add("Kafin Hausa")
        dropLGAs.Items.Add("Kaugama Kazaure")
        dropLGAs.Items.Add("Kiri Kasamma")
        dropLGAs.Items.Add("Kiyawa")
        dropLGAs.Items.Add("Maigatari")
        dropLGAs.Items.Add("Malam Madori")
        dropLGAs.Items.Add("Miga")
        dropLGAs.Items.Add("Ringim")
        dropLGAs.Items.Add("Roni")
        dropLGAs.Items.Add("Sule-Tankarkar")
        dropLGAs.Items.Add("Taura")
        dropLGAs.Items.Add("Yankwashi")
    End Sub

    Public Sub kaduna()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Birni-Gwari")
        dropLGAs.Items.Add("Chikun")
        dropLGAs.Items.Add("Giwa")
        dropLGAs.Items.Add("Igabi")
        dropLGAs.Items.Add("Ikara")
        dropLGAs.Items.Add("jaba")
        dropLGAs.Items.Add("Jema'a")
        dropLGAs.Items.Add("Kachia")
        dropLGAs.Items.Add("Kaduna North")
        dropLGAs.Items.Add("Kaduna South")
        dropLGAs.Items.Add("Kagarko")
        dropLGAs.Items.Add("Kajuru")
        dropLGAs.Items.Add("Kaura")
        dropLGAs.Items.Add("Kauru")
        dropLGAs.Items.Add("Kubau")
        dropLGAs.Items.Add("Kudan")
        dropLGAs.Items.Add("Lere")
        dropLGAs.Items.Add("Makarfi")
        dropLGAs.Items.Add("Sabon-Gari")
        dropLGAs.Items.Add("Sanga")
        dropLGAs.Items.Add("Soba")
        dropLGAs.Items.Add("Zango-Kataf")
        dropLGAs.Items.Add("Zaria")
    End Sub

    Public Sub kano()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Ajingi")
        dropLGAs.Items.Add("Albasu")
        dropLGAs.Items.Add("Bagwai")
        dropLGAs.Items.Add("Bebeji")
        dropLGAs.Items.Add("Bichi")
        dropLGAs.Items.Add("Bunkure")
        dropLGAs.Items.Add("Dala")
        dropLGAs.Items.Add("Dambatta")
        dropLGAs.Items.Add("Dawakin Kudu")
        dropLGAs.Items.Add("Dawakin Tofa")
        dropLGAs.Items.Add("Doguwa")
        dropLGAs.Items.Add("Fagge")
        dropLGAs.Items.Add("Gabasawa")
        dropLGAs.Items.Add("Garko")
        dropLGAs.Items.Add("Garum")
        dropLGAs.Items.Add("Mallam")
        dropLGAs.Items.Add("Gaya")
        dropLGAs.Items.Add("Gezawa")
        dropLGAs.Items.Add("Gwale")
        dropLGAs.Items.Add("Gwarzo")
        dropLGAs.Items.Add("Kabo")
        dropLGAs.Items.Add("Kano Municipal")
        dropLGAs.Items.Add("Karaye")
        dropLGAs.Items.Add("Kibiya")
        dropLGAs.Items.Add("Kiru")
        dropLGAs.Items.Add("Kumbotso")
        dropLGAs.Items.Add("Kunchi")
        dropLGAs.Items.Add("Kura")
        dropLGAs.Items.Add("Madobi")
        dropLGAs.Items.Add("Makoda")
        dropLGAs.Items.Add("Minjibir")
        dropLGAs.Items.Add("Nasarawa")
        dropLGAs.Items.Add("Rano")
        dropLGAs.Items.Add("Rimin Gado")
        dropLGAs.Items.Add("Rogo")
        dropLGAs.Items.Add("Shanono")
        dropLGAs.Items.Add("Sumaila")
        dropLGAs.Items.Add("Takali")
        dropLGAs.Items.Add("Tarauni")
        dropLGAs.Items.Add("Tofa")
        dropLGAs.Items.Add("Tsanyawa")
        dropLGAs.Items.Add("Tudun Wada")
        dropLGAs.Items.Add("Ungogo")
        dropLGAs.Items.Add("Warawa")
        dropLGAs.Items.Add("Wudil")
    End Sub

    Public Sub katsina()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Bakori")
        dropLGAs.Items.Add("Batagarawa")
        dropLGAs.Items.Add("Batsari")
        dropLGAs.Items.Add("Baure")
        dropLGAs.Items.Add("Bindawa")
        dropLGAs.Items.Add("Charanchi")
        dropLGAs.Items.Add("Dandume")
        dropLGAs.Items.Add("Danja")
        dropLGAs.Items.Add("Dan Musa")
        dropLGAs.Items.Add("Daura")
        dropLGAs.Items.Add("Dutsi")
        dropLGAs.Items.Add("Dutsin-Ma")
        dropLGAs.Items.Add("Faskari")
        dropLGAs.Items.Add("Funtua")
        dropLGAs.Items.Add("Ingawa")
        dropLGAs.Items.Add("Jibia")
        dropLGAs.Items.Add("Kafur")
        dropLGAs.Items.Add("Kaita")
        dropLGAs.Items.Add("Kankara")
        dropLGAs.Items.Add("Kankia")
        dropLGAs.Items.Add("Katsina")
        dropLGAs.Items.Add("Kurfi")
        dropLGAs.Items.Add("Kusada")
        dropLGAs.Items.Add("Mai 'Adua")
        dropLGAs.Items.Add("Malumfashi")
        dropLGAs.Items.Add("Mani")
        dropLGAs.Items.Add("Mashi")
        dropLGAs.Items.Add("Matazuu")
        dropLGAs.Items.Add("Musawa")
        dropLGAs.Items.Add("Rimi")
        dropLGAs.Items.Add("Sabuwa")
        dropLGAs.Items.Add("Safana")
        dropLGAs.Items.Add("Sandamu")
        dropLGAs.Items.Add("Zango")
    End Sub

    Public Sub kebbi()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Aleiro")
        dropLGAs.Items.Add("Arewa-Dandi")
        dropLGAs.Items.Add("Argungu")
        dropLGAs.Items.Add("Augie")
        dropLGAs.Items.Add("Bagudo")
        dropLGAs.Items.Add("Birnin Kebbi")
        dropLGAs.Items.Add("Bunza")
        dropLGAs.Items.Add("Dandi")
        dropLGAs.Items.Add("Fakai")
        dropLGAs.Items.Add("Gwandu")
        dropLGAs.Items.Add("Jega")
        dropLGAs.Items.Add("Kalgo")
        dropLGAs.Items.Add("Koko/Besse")
        dropLGAs.Items.Add("Maiyama")
        dropLGAs.Items.Add("Ngaski")
        dropLGAs.Items.Add("Sakaba")
        dropLGAs.Items.Add("Shanga")
        dropLGAs.Items.Add("Suru")
        dropLGAs.Items.Add("Wasagu/Danko")
        dropLGAs.Items.Add("Yauri")
        dropLGAs.Items.Add("Zuru")
    End Sub

    Public Sub kogi()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Adavi")
        dropLGAs.Items.Add("Ajaokuta")
        dropLGAs.Items.Add("Ankpa")
        dropLGAs.Items.Add("Bassa")
        dropLGAs.Items.Add("Dekina")
        dropLGAs.Items.Add("Ibaji")
        dropLGAs.Items.Add("Idah")
        dropLGAs.Items.Add("Igalamela-Odolu")
        dropLGAs.Items.Add("Ijumu")
        dropLGAs.Items.Add("Kabba/Bunu")
        dropLGAs.Items.Add("Kogi")
        dropLGAs.Items.Add("Lokoja")
        dropLGAs.Items.Add("Mopa-Muro")
        dropLGAs.Items.Add("Ofu")
        dropLGAs.Items.Add("Ogori/Mangongo")
        dropLGAs.Items.Add("Okehi")
        dropLGAs.Items.Add("Okene")
        dropLGAs.Items.Add("Olamabolo")
        dropLGAs.Items.Add("Omala")
        dropLGAs.Items.Add("Yagba East")
        dropLGAs.Items.Add("Yagba West")
    End Sub

    Public Sub kwara()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Asa")
        dropLGAs.Items.Add("Baruten")
        dropLGAs.Items.Add("Edu")
        dropLGAs.Items.Add("Ekiti")
        dropLGAs.Items.Add("Ifelodun")
        dropLGAs.Items.Add("Ilorin East")
        dropLGAs.Items.Add("Ilorin West")
        dropLGAs.Items.Add("Irepodun")
        dropLGAs.Items.Add("Isin")
        dropLGAs.Items.Add("Kaiama")
        dropLGAs.Items.Add("Moro")
        dropLGAs.Items.Add("Offa")
        dropLGAs.Items.Add("Oke-Ero")
        dropLGAs.Items.Add("Oyun")
        dropLGAs.Items.Add("Pategi")
    End Sub

    Public Sub lagos()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Agege")
        dropLGAs.Items.Add("Ajeromi-Ifelodun")
        dropLGAs.Items.Add("Alimosho")
        dropLGAs.Items.Add("Amuwo-Odofin")
        dropLGAs.Items.Add("Apapa")
        dropLGAs.Items.Add("Badagry")
        dropLGAs.Items.Add("Epe")
        dropLGAs.Items.Add("Eti-Osa")
        dropLGAs.Items.Add("Ibeju/Lekki")
        dropLGAs.Items.Add("Ifako-Ijaye")
        dropLGAs.Items.Add("Ikeja")
        dropLGAs.Items.Add("Ikorodu")
        dropLGAs.Items.Add("Kosofe")
        dropLGAs.Items.Add("Lagos Island")
        dropLGAs.Items.Add("Lagos Mainland")
        dropLGAs.Items.Add("Mushin")
        dropLGAs.Items.Add("Ojo")
        dropLGAs.Items.Add("Oshodi-Isolo")
        dropLGAs.Items.Add("Shomolu")
        dropLGAs.Items.Add("Surulere")
    End Sub

    Public Sub nasarawa()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Akwanga")
        dropLGAs.Items.Add("Awe")
        dropLGAs.Items.Add("Doma")
        dropLGAs.Items.Add("Karu")
        dropLGAs.Items.Add("Keana")
        dropLGAs.Items.Add("Keffi")
        dropLGAs.Items.Add("Kokona")
        dropLGAs.Items.Add("Lafia")
        dropLGAs.Items.Add("Nasarawa")
        dropLGAs.Items.Add("Nasarawa-Eggon")
        dropLGAs.Items.Add("Obi")
        dropLGAs.Items.Add("Toto")
        dropLGAs.Items.Add("Wamba")
    End Sub

    Public Sub niger()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Agaie")
        dropLGAs.Items.Add("Agwara")
        dropLGAs.Items.Add("Bida")
        dropLGAs.Items.Add("Borgu")
        dropLGAs.Items.Add("Bosso")
        dropLGAs.Items.Add("Chanchaga")
        dropLGAs.Items.Add("Edati")
        dropLGAs.Items.Add("Gbako")
        dropLGAs.Items.Add("Gurara")
        dropLGAs.Items.Add("Katcha")
        dropLGAs.Items.Add("Kontagora")
        dropLGAs.Items.Add("Lapai")
        dropLGAs.Items.Add("Lavun")
        dropLGAs.Items.Add("Magama")
        dropLGAs.Items.Add("Mariga")
        dropLGAs.Items.Add("Mashegu")
        dropLGAs.Items.Add("Mokwa")
        dropLGAs.Items.Add("Muya")
        dropLGAs.Items.Add("Pailoro")
        dropLGAs.Items.Add("Rafi")
        dropLGAs.Items.Add("Rijau")
        dropLGAs.Items.Add("Shiroro")
        dropLGAs.Items.Add("Suleja")
        dropLGAs.Items.Add("Tafa")
        dropLGAs.Items.Add("Wushishi")
    End Sub

    Public Sub ogun()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Abeokuta North")
        dropLGAs.Items.Add("Abeokuta South")
        dropLGAs.Items.Add("Ado-Odo/Ota")
        dropLGAs.Items.Add("Egbado North")
        dropLGAs.Items.Add("Egbado South")
        dropLGAs.Items.Add("Ewekoro")
        dropLGAs.Items.Add("IFormatProvider")
        dropLGAs.Items.Add("Ijebu East")
        dropLGAs.Items.Add("Ijebu North")
        dropLGAs.Items.Add("Ijebu North East")
        dropLGAs.Items.Add("Ijebu Ode")
        dropLGAs.Items.Add("Ikenne")
        dropLGAs.Items.Add("Imeko-Afon")
        dropLGAs.Items.Add("Ipokia")
        dropLGAs.Items.Add("Obafemi-Owode")
        dropLGAs.Items.Add("Ogun Waterside")
        dropLGAs.Items.Add("Odeda")
        dropLGAs.Items.Add("Odogbolu")
        dropLGAs.Items.Add("Remo North")
        dropLGAs.Items.Add("Shagamu")
    End Sub

    Public Sub ondo()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Akoko North East")
        dropLGAs.Items.Add("Akoko North West")
        dropLGAs.Items.Add("Akoko South Akure East")
        dropLGAs.Items.Add("Akoko South West")
        dropLGAs.Items.Add("Akure North")
        dropLGAs.Items.Add("Akure South")
        dropLGAs.Items.Add("Ese-Odo")
        dropLGAs.Items.Add("Idanre")
        dropLGAs.Items.Add("Ifedore")
        dropLGAs.Items.Add("Ilaje")
        dropLGAs.Items.Add("Ile-Oluji")
        dropLGAs.Items.Add("Okeigbo")
        dropLGAs.Items.Add("Irele")
        dropLGAs.Items.Add("Odigbo")
        dropLGAs.Items.Add("Okitipupa")
        dropLGAs.Items.Add("Ondo East")
        dropLGAs.Items.Add("Ondo West")
        dropLGAs.Items.Add("Ose")
        dropLGAs.Items.Add("Owo")
    End Sub

    Public Sub osun()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Aiyedade")
        dropLGAs.Items.Add("Aiyedire")
        dropLGAs.Items.Add("Atakumosa East")
        dropLGAs.Items.Add("Atakumosa West")
        dropLGAs.Items.Add("Boluwaduro")
        dropLGAs.Items.Add("Boripe")
        dropLGAs.Items.Add("Ede North")
        dropLGAs.Items.Add("Ede South")
        dropLGAs.Items.Add("Egbedore")
        dropLGAs.Items.Add("Ejigbo")
        dropLGAs.Items.Add("Ife Central")
        dropLGAs.Items.Add("Ife East")
        dropLGAs.Items.Add("Ife North")
        dropLGAs.Items.Add("Ife South")
        dropLGAs.Items.Add("Ifedayo")
        dropLGAs.Items.Add("Ifelodun")
        dropLGAs.Items.Add("Ila")
        dropLGAs.Items.Add("Ilesha East")
        dropLGAs.Items.Add("Ilesha West")
        dropLGAs.Items.Add("Irepodun")
        dropLGAs.Items.Add("Irewole")
        dropLGAs.Items.Add("Isokan")
        dropLGAs.Items.Add("Iwo")
        dropLGAs.Items.Add("Obokun")
        dropLGAs.Items.Add("Odo-Otin")
        dropLGAs.Items.Add("Ola-Oluwa")
        dropLGAs.Items.Add("Olorunda")
        dropLGAs.Items.Add("Oriade")
        dropLGAs.Items.Add("Orolu")
        dropLGAs.Items.Add("Osogbo")
    End Sub

    Public Sub oyo()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Afijio")
        dropLGAs.Items.Add("Akinyele")
        dropLGAs.Items.Add("Atiba")
        dropLGAs.Items.Add("Atigbo")
        dropLGAs.Items.Add("Egbeda")
        dropLGAs.Items.Add("IbadanCentral")
        dropLGAs.Items.Add("Ibadan North")
        dropLGAs.Items.Add("Ibadan North West")
        dropLGAs.Items.Add("Ibadan South East")
        dropLGAs.Items.Add("Ibadan South West")
        dropLGAs.Items.Add("Ibarapa Central")
        dropLGAs.Items.Add("Ibarapa East")
        dropLGAs.Items.Add("Ibarapa North")
        dropLGAs.Items.Add("Ido")
        dropLGAs.Items.Add("Irepo")
        dropLGAs.Items.Add("Iseyin")
        dropLGAs.Items.Add("Itesiwaju")
        dropLGAs.Items.Add("Iwajowa")
        dropLGAs.Items.Add("Kajola")
        dropLGAs.Items.Add("Lagelu Ogbomosho North")
        dropLGAs.Items.Add("Ogbmosho South")
        dropLGAs.Items.Add("Ogo Oluwa")
        dropLGAs.Items.Add("Olorunsogo")
        dropLGAs.Items.Add("Oluyole")
        dropLGAs.Items.Add("Ona-Ara")
        dropLGAs.Items.Add("Orelope")
        dropLGAs.Items.Add("Ori Ire")
        dropLGAs.Items.Add("Oyo East")
        dropLGAs.Items.Add("Oyo West")
        dropLGAs.Items.Add("Saki East")
        dropLGAs.Items.Add("Saki West")
        dropLGAs.Items.Add("Surulere")
    End Sub

    Public Sub plateau()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Barikin Ladi")
        dropLGAs.Items.Add("Bassa")
        dropLGAs.Items.Add("Bokkos")
        dropLGAs.Items.Add("Jos East")
        dropLGAs.Items.Add("Jos North")
        dropLGAs.Items.Add("Jos South")
        dropLGAs.Items.Add("Kanam")
        dropLGAs.Items.Add("Kanke")
        dropLGAs.Items.Add("Langtang North")
        dropLGAs.Items.Add("Langtang South")
        dropLGAs.Items.Add("Mangu")
        dropLGAs.Items.Add("Mikang")
        dropLGAs.Items.Add("Pankshin")
        dropLGAs.Items.Add("Qua'an Pan")
        dropLGAs.Items.Add("Riyom")
        dropLGAs.Items.Add("Shendam")
        dropLGAs.Items.Add("Wase")
    End Sub

    Public Sub rivers()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Abua/Odual")
        dropLGAs.Items.Add("Ahoada East")
        dropLGAs.Items.Add("Ahoada West")
        dropLGAs.Items.Add("Akuku Toru")
        dropLGAs.Items.Add("Andoni")
        dropLGAs.Items.Add("Asari-Toru")
        dropLGAs.Items.Add("Bonny")
        dropLGAs.Items.Add("Degema")
        dropLGAs.Items.Add("Emohua")
        dropLGAs.Items.Add("Eleme")
        dropLGAs.Items.Add("Etche")
        dropLGAs.Items.Add("Gokana")
        dropLGAs.Items.Add("Ikwerre")
        dropLGAs.Items.Add("Khana")
        dropLGAs.Items.Add("Obia/Akpor")
        dropLGAs.Items.Add("Ogba/Egbema/Ndoni")
        dropLGAs.Items.Add("Ogu/Bolo")
        dropLGAs.Items.Add("Okrika")
        dropLGAs.Items.Add("Omumma")
        dropLGAs.Items.Add("Opobo/Nkoro")
        dropLGAs.Items.Add("Oyigbo")
        dropLGAs.Items.Add("Port-Harcourt")
        dropLGAs.Items.Add("Tai")
    End Sub

    Public Sub sokoto()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Binji")
        dropLGAs.Items.Add("Bodinga")
        dropLGAs.Items.Add("Dange-Shnsi")
        dropLGAs.Items.Add("Gada")
        dropLGAs.Items.Add("Goronyo")
        dropLGAs.Items.Add("Gudu")
        dropLGAs.Items.Add("Gawabawa")
        dropLGAs.Items.Add("Illela")
        dropLGAs.Items.Add("IsArray")
        dropLGAs.Items.Add("Kware")
        dropLGAs.Items.Add("Kebbe")
        dropLGAs.Items.Add("Rabah")
        dropLGAs.Items.Add("Sabon Birni")
        dropLGAs.Items.Add("Shagari")
        dropLGAs.Items.Add("Silame")
        dropLGAs.Items.Add("Sokoto North")
        dropLGAs.Items.Add("Sokoto South")
        dropLGAs.Items.Add("Tambuwal")
        dropLGAs.Items.Add("Tqngaza")
        dropLGAs.Items.Add("Tureta")
        dropLGAs.Items.Add("Wamako")
        dropLGAs.Items.Add("Wurno")
        dropLGAs.Items.Add("Yabo")
    End Sub

    Public Sub taraba()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Ardo-Kola")
        dropLGAs.Items.Add("Bali")
        dropLGAs.Items.Add("Donga")
        dropLGAs.Items.Add("Gashaka")
        dropLGAs.Items.Add("Cassol")
        dropLGAs.Items.Add("Ibi")
        dropLGAs.Items.Add("Jalingo")
        dropLGAs.Items.Add("Karin-Lamido")
        dropLGAs.Items.Add("Kurmi")
        dropLGAs.Items.Add("Lau")
        dropLGAs.Items.Add("Sardauna")
        dropLGAs.Items.Add("Takum")
        dropLGAs.Items.Add("Ussa")
        dropLGAs.Items.Add("Wukari")
        dropLGAs.Items.Add("Yorro")
        dropLGAs.Items.Add("Zing")
    End Sub

    Public Sub yobe()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Bade")
        dropLGAs.Items.Add("Bursari")
        dropLGAs.Items.Add("Damaturu")
        dropLGAs.Items.Add("Fika")
        dropLGAs.Items.Add("Fune")
        dropLGAs.Items.Add("Geidam")
        dropLGAs.Items.Add("Gujba")
        dropLGAs.Items.Add("Gulani")
        dropLGAs.Items.Add("Jakusko")
        dropLGAs.Items.Add("Karasuwa")
        dropLGAs.Items.Add("Karawa")
        dropLGAs.Items.Add("Machina")
        dropLGAs.Items.Add("Nangere")
        dropLGAs.Items.Add("Nguru Potiskum")
        dropLGAs.Items.Add("Tarmua")
        dropLGAs.Items.Add("Yunusari")
        dropLGAs.Items.Add("Yusufari")
    End Sub

    Public Sub zamfarawa()
        dropLGAs.Items.Clear()
        dropLGAs.Items.Add("Anka")
        dropLGAs.Items.Add("Bakura")
        dropLGAs.Items.Add("Birnin Magaji")
        dropLGAs.Items.Add("Bukkuyum")
        dropLGAs.Items.Add("Bungudu")
        dropLGAs.Items.Add("Gummi")
        dropLGAs.Items.Add("Gusau")
        dropLGAs.Items.Add("Kaura")
        dropLGAs.Items.Add("Namoda")
        dropLGAs.Items.Add("Maradun")
        dropLGAs.Items.Add("Maru")
        dropLGAs.Items.Add("Shinkafi")
        dropLGAs.Items.Add("Talata Mafara")
        dropLGAs.Items.Add("Tsafe")
        dropLGAs.Items.Add("Zurmi")
    End Sub
End Class