Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.DataSet
Imports System.Data.DataTable
Imports System.Web.SessionState.HttpSessionState
Imports System.Web.HttpContext
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Drawing
Imports System.Math
'Imports Dundas.Charting.WebControl
Imports System.Net.Mail


Public Class mfa1


    'Public pth As String = Web.HttpContext.Current.Server.MapPath(dbpath)
    Public con As New SqlConnection

    Public str As String

    Public app As String = Web.HttpContext.Current.Server.MapPath("/")
    Public pwd As String = ""
    'SSSSSSSSSSSSSSSSSS
    Public mypc As String = Current.Request.Browser.Platform.ToString 'this detects phone as unknown and windows as winnt
    Public cuIP As String = (HttpContext.Current.Request.UserHostAddress.ToString).Substring(0, 7)



    Public lmsg As String
    Public USerName As String '= CType(Current.Session("Username"), String)
    Public BranchCode As String '= CType(Current.Session.Item("Branchcode"), String)
    Public region As String
    Public AndCondition As String
    Public AndBranchCode As String
    Public WhereBranchCode As String

    Public dCode As String
    Public AnddCode As String
    Public WheredCode As String
    Public xCode As String
    Public smsCode As String

    Public DT As DataTable
    Public DT1 As DataTable
    Public DH As String
    Public DA As String

    Dim oDs As DataSet
    Public Sub Checksession()

        DH = "'"
        DA = ""
        'str = "Data Source=" & "162.222.225.88; " &
        str = "Data Source=" & "162.222.225.88; " &
                       "Initial Catalog=fsmshalogen;" &
                               "User ID=fsmshalogen;" &
                               "Password=fsmshalogen_9fsmshalogen_9;"
        'str = "Data Source=" & ".\SQLEXPRESS; " &
        '                               "Initial Catalog=fsmsPBI;" &
        '                                       "User ID=ebere;" &
        '                                       "Password=ebere;"


        If Not Current.Session.Item("Username") Is Nothing Then
            USerName = CType(Current.Session("Username"), String)
            xCode = CType(Current.Session("xCode"), String)
            smsCode = CType(Current.Session("xCode"), String)
            BranchCode = CType(Current.Session("BranchCode"), String)
            AndBranchCode = " and BranchCode = '" + CType(Current.Session("BranchCode"), String) + "'"
            WhereBranchCode = " where BranchCode = '" + CType(Current.Session("BranchCode"), String) + "'"

            dCode = CType(Current.Session("dCode"), String)
            AnddCode = " and dCode = '" + CType(Current.Session("dCode"), String) + "'"
            WheredCode = " where dCode = '" + CType(Current.Session("dCode"), String) + "'"

            region = ReturnConditionNA("cmstblcoy", "region", WhereBranchCode)
        Else
            Current.Response.Redirect("default.aspx")
        End If


    End Sub

    Public Sub PreChecksession()

        DH = "'"
        DA = ""
        ' str = "Data Source=" & "162.222.225.88; " &
        str = "Data Source=" & "162.222.225.88; " &
                        "Initial Catalog=fsmshalogen;" &
                               "User ID=fsmshalogen;" &
                               "Password=fsmshalogen_9fsmshalogen_9;"
        'str = "Data Source=" & ".\SQLEXPRESS; " &
        '                               "Initial Catalog=fsmsPBI;" &
        '                                       "User ID=ebere;" &
        '                                       "Password=ebere;"





    End Sub
    Public Function BranchCodeRegion(ByVal BranchCode As String) As String
        BranchCodeRegion = ""
        Dim s As String

        s = ReturnConditionNA("cmstblcoy", "region", " where Branchcode='" & BranchCode + "'")
        Return s

    End Function
    Public Function UserPhoto(ByVal s As ImageButton, ByVal Username As String) As ImageButton
        Try
            If IsJohn(Username) = True Then
                s.ImageUrl = "photo/pp" & "/" & "ppp.png"
                s.DataBind()
                Exit Function
            End If

            s.ImageUrl = ReturnConditionNA("cmstbllog", "Fullname", " where username='" & Username + "'")

            If ReturnConditionNA("cmstbllog", "Photo", " where Username='" + Username + "'").ToString.Length > 3 Then

                s.ImageUrl = "photo/pp" + "/" + ReturnFromQuery("select photo from cmstbllog where Username='" + Username + "'").ToString
                s.DataBind()
            Else

                s.ImageUrl = "photo/pp" & "/" & "ppp.png"
                s.DataBind()

            End If

            Return s

        Catch ex As Exception
            s.ImageUrl = "~/d/pp" & "/" & "ppp.png"
            s.DataBind()
        End Try
    End Function


    Public Function BranchLogo(ByVal s As ImageButton, ByVal BranchCode As String) As ImageButton
        Try


            s.ImageUrl = ReturnConditionNA("cmstblCoy", "Photo", " where BranchCode='" & BranchCode + "'")

            If ReturnConditionNA("cmstblCoy", "Photo", " where branchCode='" + BranchCode + "'").ToString.Length > 3 Then

                s.ImageUrl = "photo/logo" + "/" + ReturnFromQuery("select photo from cmstblCoy where BranchCode='" + BranchCode + "'").ToString
                s.DataBind()
            Else

                s.ImageUrl = "photo/logo" & "/" & "logo.png"
                s.DataBind()

            End If

            Return s

        Catch ex As Exception
            s.ImageUrl = "photo/logo" & "/" & "logo.png"
            s.DataBind()
        End Try
    End Function



    Public Function GetRandom(ByVal Min As Integer, ByVal Max As Integer) As Integer
        Dim Generator As System.Random = New System.Random()
        Return Generator.Next(Min, Max)
    End Function
    Public Function IsJohn(ByVal username As String) As Boolean
        IsJohn = False
        If ReturnCheckCondition("cmstbljohn", "username", " Where username='" & username & "'") = True Then
            IsJohn = True

            Return IsJohn

        End If
    End Function
    Public Sub ReplaceText(ByVal root As Control)
        For Each ctrl As Control In root.Controls
            ReplaceText(ctrl)
            If TypeOf ctrl Is TextBox Then
                ''CType(ctrl, TextBox).BorderColor = Drawing.Color.Black
                ''CType(ctrl, TextBox).BorderStyle = BorderStyle.Solid
                ''CType(ctrl, TextBox).BorderWidth = 1
                'check if ' is there
                If (CType(ctrl, TextBox).Text).Contains("'") = True Then
                    'get the original text
                    Dim OriginalText As String
                    OriginalText = CType(ctrl, TextBox).Text.Trim
                    Dim newText As String
                    newText = Replace(OriginalText, "'", "@")
                    'place back the new text in the textbox
                    CType(ctrl, TextBox).Text = newText
                End If
            End If

        Next ctrl
    End Sub
    Public Function ReturnDelete(ByVal xtbl As String, ByVal AndCondition As String) As Integer
        Try

            Dim t As String

            t = "Delete  " + DA + " FROM " + xtbl + AndCondition
            Dim addedD As Integer
            Me.forceclosedatabase()
            con.ConnectionString = str
            Dim dbcommD
                dbcommD = New SqlCommand(t, con)
                con.Open()
                addedD = dbcommD.ExecuteNonQuery()
                con.Close()

                Return addedD
            ReturnDelete = addedD
        Catch ex As Exception
            con.Close()
        End Try
    End Function
    Public Sub MustBe(ByVal GMgr As String, ByVal GAccount As String, ByVal RMgr As String, ByVal RAccount As String, ByVal Bmgr As String, ByVal BAccount As String)
        If ReturnCheckCondition("cmstbljohn", "Username", " where Username='" + CType(Current.Session("username"), String) + "'") = False And ReturnCheckCondition("cmstbllog", "Username", " where Username='" + CType(Current.Session("username"), String) + "' and CEO='Yes'" & AnddCode) = False And ReturnCheckCondition("cmstbllog", "Username", " where Username='" + CType(Current.Session("username"), String) + "' and " & Bmgr & " ='Yes'" & AndBranchCode) = False And ReturnCheckCondition("cmstbllog", "Username", " where Username='" + CType(Current.Session("username"), String) + "' and " & BAccount & " ='Yes'" & AndBranchCode) = False And ReturnCheckCondition("cmstbllog", "Username", " where Username='" + CType(Current.Session("username"), String) + "' and " & RMgr & " ='Yes'" & AnddCode) = False And ReturnCheckCondition("cmstbllog", "Username", " where Username='" + CType(Current.Session("username"), String) + "' and " & RAccount & " ='Yes'" & AnddCode) = False And ReturnCheckCondition("cmstbllog", "Username", " where Username='" + CType(Current.Session("username"), String) + "' and " & GMgr & " ='Yes'" & AnddCode) = False And ReturnCheckCondition("cmstbllog", "Username", " where Username='" + CType(Current.Session("username"), String) + "' and " & GAccount & " ='Yes'" & AnddCode) = False Then

            Current.Response.Redirect("Accessdenied.aspx")
        End If
    End Sub
    Public Function MustbeBOOL(ByVal GMgr As String, ByVal GAccount As String, ByVal RMgr As String, ByVal RAccount As String, ByVal Bmgr As String, ByVal BAccount As String) As Boolean

        MustbeBOOL = False
        If ReturnCheckCondition("cmstbljohn", "Username", " where Username='" + CType(Current.Session("username"), String) + "'") = False And ReturnCheckCondition("cmstbllog", "Username", " where Username='" + CType(Current.Session("username"), String) + "' and CEO='Yes'" & AnddCode) = False And ReturnCheckCondition("cmstbllog", "Username", " where Username='" + CType(Current.Session("username"), String) + "' and " & Bmgr & " ='Yes'" & AndBranchCode) = False And ReturnCheckCondition("cmstbllog", "Username", " where Username='" + CType(Current.Session("username"), String) + "' and " & BAccount & " ='Yes'" & AndBranchCode) = False And ReturnCheckCondition("cmstbllog", "Username", " where Username='" + CType(Current.Session("username"), String) + "' and " & RMgr & " ='Yes'" & AnddCode) = False And ReturnCheckCondition("cmstbllog", "Username", " where Username='" + CType(Current.Session("username"), String) + "' and " & RAccount & " ='Yes'" & AnddCode) = False And ReturnCheckCondition("cmstbllog", "Username", " where Username='" + CType(Current.Session("username"), String) + "' and " & GMgr & " ='Yes'" & AnddCode) = False And ReturnCheckCondition("cmstbllog", "Username", " where Username='" + CType(Current.Session("username"), String) + "' and " & GAccount & " ='Yes'" & AnddCode) = False Then

            MustbeBOOL = False
        Else
            MustbeBOOL = True
        End If
        Return MustbeBOOL
    End Function
    Public Function IsRoleBOOL(ByVal GMgr As String, ByVal GAccount As String, ByVal RMgr As String, ByVal RAccount As String, ByVal Bmgr As String, ByVal BAccount As String) As Boolean

        IsRoleBOOL = False
        If ReturnCheckCondition("cmstbljohn", "Username", " where Username='" + CType(Current.Session("username"), String) + "'") = True Or ReturnCheckCondition("cmstbllog", "Username", " where Username='" + CType(Current.Session("username"), String) + "' and CEO='Yes'" & AnddCode) = True Or ReturnCheckCondition("cmstbllog", "Username", " where Username='" + CType(Current.Session("username"), String) + "' and " & Bmgr & " ='Yes'" & AndBranchCode) = True Or ReturnCheckCondition("cmstbllog", "Username", " where Username='" + CType(Current.Session("username"), String) + "' and " & BAccount & " ='Yes'" & AndBranchCode) = True Or ReturnCheckCondition("cmstbllog", "Username", " where Username='" + CType(Current.Session("username"), String) + "' and " & RMgr & " ='Yes'" & AnddCode) = True Or ReturnCheckCondition("cmstbllog", "Username", " where Username='" + CType(Current.Session("username"), String) + "' and " & RAccount & " ='Yes'" & AnddCode) = True Or ReturnCheckCondition("cmstbllog", "Username", " where Username='" + CType(Current.Session("username"), String) + "' and " & GMgr & " ='Yes'" & AnddCode) = True Or ReturnCheckCondition("cmstbllog", "Username", " where Username='" + CType(Current.Session("username"), String) + "' and " & GAccount & " ='Yes'" & AnddCode) = True Then

            IsRoleBOOL = True
        Else
            IsRoleBOOL = False
        End If
        Return IsRoleBOOL
    End Function
    Public Function UserRegion(ByVal Username As String) As String
        UserRegion = ""
        Dim s As String

        s = ReturnConditionNA("cmstblcoy", "region", " where Branchcode='" & UserBranchCode(Username) + "'")
        Return s

    End Function


    Public Function UserBranchCode(ByVal Username As String) As String
        UserBranchCode = ""
        Dim s As String

        s = ReturnConditionNA("cmstbllog", "BranchCode", " where Username='" & Username + "'")
        Return s

    End Function
    Public Sub MustBeJohnorAdmin()
        If ReturnCheckCondition("cmstbljohn", "Username", " where Username='" + CType(Current.Session("username"), String) + "'") = False And ReturnCheckCondition("cmstbllog", "Username", " where Username='" + CType(Current.Session("username"), String) + "' and ICT='Yes'" & AndBranchCode) = False Then

            Current.Response.Redirect("Accessdenied.aspx")
        End If
    End Sub
    Public Function ReportDataTable2FROMQUERY(ByVal xtbl As String, ByVal Query As String) As DataTable

        Try

            Me.forceclosedatabase()
            Dim sqlBrand As String = Query

            Dim dsBrand As New DataSet

            con.ConnectionString = str
            Dim daBrand As SqlClient.SqlDataAdapter
                con.Open()
                daBrand = New SqlClient.SqlDataAdapter(sqlBrand, con)
                daBrand.Fill(dsBrand, xtbl)
                con.Close()


            Dim DTBrand As DataTable = dsBrand.Tables(xtbl)
            '  Return ReturnNo(xtbl, xfield, xtext, ytext)
            Dim res As DataTable

            If DTBrand.Rows.Count > 0 Then
                res = DTBrand
                Return res
                ReportDataTable2FROMQUERY = res

                'Else
                '    Return ReturnCheck = False

            End If

        Catch ex As Exception
            'MsgBox("NA YOU " + ex.Message.ToString)
            con.Close()

        End Try
    End Function

    Public Function ReportDataTable2DISTINCT(ByVal xtbl As String, ByVal xfield As String, ByVal AndCondition As String) As DataTable

        Try

            Me.forceclosedatabase()
            Dim sqlBrand As String = "SELECT DISTINCT " + xfield + " FROM " + xtbl + AndCondition

            Dim dsBrand As New DataSet

            con.ConnectionString = str
            Dim daBrand As SqlClient.SqlDataAdapter
                con.Open()
                daBrand = New SqlClient.SqlDataAdapter(sqlBrand, con)
                daBrand.Fill(dsBrand, xtbl)
                con.Close()


            Dim DTBrand As DataTable = dsBrand.Tables(xtbl)
            '  Return ReturnNo(xtbl, xfield, xtext, ytext)
            Dim res As DataTable

            If DTBrand.Rows.Count > 0 Then
                res = DTBrand
                Return res
                ReportDataTable2DISTINCT = res

                'Else
                '    Return ReturnCheck = False

            End If

        Catch ex As Exception
            con.Close()

        End Try
    End Function
    Public Function ReportSelectedDataTable(ByVal xtbl As String, ByVal xfield As String, ByVal AndCondition As String) As DataTable

        Try

            Me.forceclosedatabase()
            Dim sqlBrand As String = "SELECT " + xfield + " FROM " + xtbl + AndCondition

            Dim dsBrand As New DataSet

            con.ConnectionString = str
            Dim daBrand As SqlClient.SqlDataAdapter
                con.Open()
                daBrand = New SqlClient.SqlDataAdapter(sqlBrand, con)
                daBrand.Fill(dsBrand, xtbl)
                con.Close()



            Dim DTBrand As DataTable = dsBrand.Tables(xtbl)
            '  Return ReturnNo(xtbl, xfield, xtext, ytext)
            Dim res As DataTable

            If DTBrand.Rows.Count > 0 Then
                res = DTBrand
                Return res
                ReportSelectedDataTable = res

                'Else
                '    Return ReturnCheck = False

            End If

        Catch ex As Exception
            con.Close()

        End Try
    End Function

    Public Function ReportSelectedDataTableTOP10(ByVal xtbl As String, ByVal xfield As String, ByVal AndCondition As String) As DataTable

        Try

            Me.forceclosedatabase()
            Dim sqlBrand As String = "SELECT TOP 5 " + xfield + " FROM " + xtbl + AndCondition

            Dim dsBrand As New DataSet

            con.ConnectionString = str
            Dim daBrand As SqlClient.SqlDataAdapter
                con.Open()
                daBrand = New SqlClient.SqlDataAdapter(sqlBrand, con)
                daBrand.Fill(dsBrand, xtbl)
                con.Close()



            Dim DTBrand As DataTable = dsBrand.Tables(xtbl)
            '  Return ReturnNo(xtbl, xfield, xtext, ytext)
            Dim res As DataTable

            If DTBrand.Rows.Count > 0 Then
                res = DTBrand
                Return res
                ReportSelectedDataTableTOP10 = res

                'Else
                '    Return ReturnCheck = False

            End If

        Catch ex As Exception
            con.Close()

        End Try
    End Function
    Public Function ReportSelectedDataTableNTOP(ByVal xtbl As String, ByVal xfield As String, ByVal AndCondition As String, ByVal nTop As String) As DataTable

        Try

            Me.forceclosedatabase()
            Dim sqlBrand As String = "SELECT TOP " + nTop + " " + xfield + " FROM " + xtbl + AndCondition

            Dim dsBrand As New DataSet

            con.ConnectionString = str
            Dim daBrand As SqlClient.SqlDataAdapter
            con.Open()
            daBrand = New SqlClient.SqlDataAdapter(sqlBrand, con)
            daBrand.Fill(dsBrand, xtbl)
            con.Close()
            'SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS


            Dim DTBrand As DataTable = dsBrand.Tables(xtbl)
            '  Return ReturnNo(xtbl, xfield, xtext, ytext)
            Dim res As DataTable

            If DTBrand.Rows.Count > 0 Then
                res = DTBrand
                Return res
                ReportSelectedDataTableNTOP = res

                'Else
                '    Return ReturnCheck = False

            End If

        Catch ex As Exception
            con.Close()
        End Try
    End Function
    Public Function ReturnSumField(ByVal xtbl As String, ByVal xfield As String, ByVal AndCondition As String) As String


        Try


            Me.forceclosedatabase()
            Dim sqlBrand As String = "SELECT " + xfield + " FROM " + xtbl + AndCondition

            Dim dsBrand As New DataSet

            con.ConnectionString = str
            Dim daBrand As SqlClient.SqlDataAdapter
            con.Open()
            daBrand = New SqlClient.SqlDataAdapter(sqlBrand, con)
            daBrand.Fill(dsBrand, xtbl)
            con.Close()


            Dim DTBrand As DataTable = dsBrand.Tables(xtbl)
            '  Return ReturnNo(xtbl, xfield, xtext, ytext)
            Dim res As String

            If DTBrand.Rows.Count > 0 Then
                res = DTBrand.Rows(0)(xfield)
                Dim o As String
                o = "Sum (" + xfield + ")"
                res = DTBrand.Compute(o, "").ToString
                Return res
                ReturnSumField = res

            End If

        Catch ex As Exception
            ' ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & ex.Message.ToString & "');", True)
            ReturnSumField = 0
            con.Close()
        End Try
    End Function

    Public Sub ListAnyThingQUERY(ByVal Lctrl As ListControl, ByVal tbl As String, ByVal xField As String, ByVal query As String)
        Try


            Dim curr As String
            ' Dim xField As String = "WellName"
            'AndCondition = " where BranchCode='" + BranchCode + "'"

            DT = ReportDataTable2FROMQUERY(tbl, query)
            Lctrl.Items.Clear()
            If DT.Rows.Count > 0 Then
                Dim cnt As Single = DT.Rows.Count
                'MsgBox(cnt.ToString + " Wells Found")
                Dim j As Integer
                For j = 0 To DT.Rows.Count - 1
                    curr = DT.Rows(j)(xField).ToString
                    Lctrl.Items.Add(curr)

                Next
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub ListAnyThing(ByVal Lctrl As ListControl, ByVal tbl As String, ByVal xField As String, ByVal AndCondition As String)
        Try


            Dim curr As String
            ' Dim xField As String = "WellName"
            'AndCondition = " where BranchCode='" + BranchCode + "'"

            DT = ReportDataTable2DISTINCT(tbl, xField, AndCondition)
            Lctrl.Items.Clear()
            If DT.Rows.Count > 0 Then
                Dim cnt As Single = DT.Rows.Count
                'MsgBox(cnt.ToString + " Wells Found")
                Dim j As Integer
                For j = 0 To DT.Rows.Count - 1
                    curr = DT.Rows(j)(xField).ToString
                    Lctrl.Items.Add(curr)

                Next
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub ListRMgr(ByVal Lctrl As ListControl)
        Try
            Lctrl.Items.Clear()

            Dim brcDT As DataTable
            DT = ReportDataTable2DISTINCT("cmstblcoy", "BranchCode", " where region='" & region & "'")
            If DT.Rows.Count > 0 Then
                Dim cnt As Single = DT.Rows.Count
                Dim RMgr As String
                Dim Brc As String 'each branchcode
                Dim j As Integer
                For j = 0 To DT.Rows.Count - 1
                    Brc = DT.Rows(j)("BranchCode").ToString
                    'check if brc has any Rmgr
                    If ReturnCheckCondition("cmstbllog", "RMgr", " where branchcode='" & Brc & "'") = True Then
                        brcDT = ReportDataTable2DISTINCT("cmstbllog", "FullName", " where RMgr='Yes' and branchCode='" & Brc & "'")
                        'get RMgrs
                        If brcDT.Rows.Count > 0 Then
                            Dim jj As Integer
                            For jj = 0 To brcDT.Rows.Count - 1
                                RMgr = brcDT.Rows(jj)("FullName").ToString
                                Lctrl.Items.Add(RMgr)
                            Next
                        End If

                    End If



                Next
            End If

        Catch ex As Exception

        End Try
    End Sub



    Public Sub ListAnyThinginLISTBOX(ByVal Lctrl As ListBox, ByVal tbl As String, ByVal xField As String, ByVal AndCondition As String)
        Try


            Dim curr As String
            ' Dim xField As String = "WellName"
            'AndCondition = " where BranchCode='" + BranchCode + "'"

            DT = ReportDataTable2DISTINCT(tbl, xField, AndCondition)
            Lctrl.Items.Clear()
            If DT.Rows.Count > 0 Then
                Dim cnt As Single = DT.Rows.Count
                'MsgBox(cnt.ToString + " Wells Found")
                Dim j As Integer
                For j = 0 To DT.Rows.Count - 1
                    curr = DT.Rows(j)(xField).ToString
                    Lctrl.Items.Add(curr)

                Next
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function UpdateData(ByVal xtbl As String, ByVal xText As String, ByVal AndCondition As String) As Integer
        Try
            Dim addedD9 As Integer
            Dim s As String
            s = "Update " + xtbl + " Set " + xText + AndCondition
            con.ConnectionString = str
            Dim dbcommD9
                dbcommD9 = New SqlCommand(s, con)
                con.Open()
                addedD9 = dbcommD9.ExecuteNonQuery()
                con.Close()

                Return addedD9
            UpdateData = addedD9
        Catch ex As Exception
            con.Close()

        End Try
    End Function
    Public Function ReturnCheckCondition(ByVal xtbl As String, ByVal xfield As String, ByVal AndCondition As String) As Boolean

        ReturnCheckCondition = False
        Try

            Me.forceclosedatabase()
            Dim sqlBrand As String = "SELECT " + xfield + " FROM " + xtbl + AndCondition
            Dim dsBrand As New DataSet

            con.ConnectionString = str
            Dim daBrand As SqlClient.SqlDataAdapter
                con.Open()
                daBrand = New SqlClient.SqlDataAdapter(sqlBrand, con)
                daBrand.Fill(dsBrand, xtbl)
                con.Close()



                Dim DTBrand As DataTable = dsBrand.Tables(xtbl)
            '  Return ReturnNo(xtbl, xfield, xtext, ytext)
            Dim res As Boolean

            If DTBrand.Rows.Count > 0 Then
                res = True
                Return res
                ReturnCheckCondition = res

                'Else
                '    Return ReturnCheck = False

            End If

        Catch ex As Exception
            Me.forceclosedatabase()
        End Try
    End Function

    Public Function ReturnCheckConditionQUERY(ByVal xtbl As String, ByVal Query As String) As Boolean

        ReturnCheckConditionQUERY = False
        Try

            Me.forceclosedatabase()
            Dim sqlBrand As String = Query
            Dim dsBrand As New DataSet

            con.ConnectionString = str
            Dim daBrand As SqlClient.SqlDataAdapter
                con.Open()
                daBrand = New SqlClient.SqlDataAdapter(sqlBrand, con)
                daBrand.Fill(dsBrand, xtbl)
                con.Close()



            Dim DTBrand As DataTable = dsBrand.Tables(xtbl)
            '  Return ReturnNo(xtbl, xfield, xtext, ytext)
            Dim res As Boolean

            If DTBrand.Rows.Count > 0 Then
                res = True
                Return res
                ReturnCheckConditionQUERY = res

                'Else
                '    Return ReturnCheck = False

            End If

        Catch ex As Exception
            Me.forceclosedatabase()
        End Try
    End Function

    Public Sub forceclosedatabase()
        If con.State = ConnectionState.Open Then
            con.Close()
        End If


    End Sub

    Public Sub ClearText(ByVal root As Control)
        For Each ctrl As Control In root.Controls
            ClearText(ctrl)
            If TypeOf ctrl Is TextBox Then
                CType(ctrl, TextBox).Text = ""
            End If

        Next ctrl
    End Sub


    Public Function InsertData(ByVal xtbl As String, ByVal xField As String, ByVal xText As String) As Integer
        Try


            Dim s As String
            Dim addedD9 As Integer
            Dim dbCommD
            s = "INSERT INTO " + xtbl + " (" + xField + ") Values (" + xText + ")"
            con.ConnectionString = str
            dbCommD = New SqlCommand(s, con)
                con.Open()
                addedD9 = dbCommD.ExecuteNonQuery()
                con.Close()


            Return addedD9
            InsertData = addedD9
        Catch ex As Exception
            con.Close()

        End Try
    End Function

    Public Function ReturnCondition(ByVal xtbl As String, ByVal xfield As String, ByVal AndCondition As String) As String
        Try


            Me.forceclosedatabase()
            Dim sqlBrand As String = "SELECT " + xfield + " FROM " + xtbl + AndCondition

            Dim dsBrand As New DataSet

            con.ConnectionString = str
            Dim daBrand As SqlClient.SqlDataAdapter
                con.Open()
                daBrand = New SqlClient.SqlDataAdapter(sqlBrand, con)
                daBrand.Fill(dsBrand, xtbl)
                con.Close()



            Dim DTBrand As DataTable = dsBrand.Tables(xtbl)
            '  Return ReturnNo(xtbl, xfield, xtext, ytext)
            Dim res As String

            If DTBrand.Rows.Count > 0 Then
                res = DTBrand.Rows(0)(xfield)
                Return res
                ReturnCondition = res

            End If

        Catch ex As Exception
            ReturnCondition = 0
            con.Close()

        End Try
    End Function

    Public Function ReturnConditionNA(ByVal xtbl As String, ByVal xfield As String, ByVal AndCondition As String) As String
        Try


            Me.forceclosedatabase()
            Dim sqlBrand As String = "SELECT " + xfield + " FROM " + xtbl + AndCondition

            Dim dsBrand As New DataSet

            con.ConnectionString = str
            Dim daBrand As SqlClient.SqlDataAdapter
                con.Open()
                daBrand = New SqlClient.SqlDataAdapter(sqlBrand, con)
                daBrand.Fill(dsBrand, xtbl)
                con.Close()


            Dim DTBrand As DataTable = dsBrand.Tables(xtbl)
            '  Return ReturnNo(xtbl, xfield, xtext, ytext)
            Dim res As String

            If DTBrand.Rows.Count > 0 Then
                res = DTBrand.Rows(0)(xfield)
                Return res
                ReturnConditionNA = res

            End If

        Catch ex As Exception
            ReturnConditionNA = ""
            con.Close()

        End Try
    End Function
    Public Function RunQuery(ByVal sql As String) As Integer
        Try
            Dim addedD9 As Integer
            'Dim s As String
            's = "Update " + xtbl + " Set " + xText + AndCondition
            con.ConnectionString = str
            Dim dbcommD9
                dbcommD9 = New SqlCommand(sql, con)
                con.Open()
                addedD9 = dbcommD9.ExecuteNonQuery()
                con.Close()


            Return addedD9
            RunQuery = addedD9

        Catch ex As Exception
            con.Close()

            lmsg = "Wrong Query String"
            'Web.HttpContext.Current.ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & lmsg & "');", True)

        End Try
    End Function

    Public Function ReturnFromQuery(ByVal xQuery As String) As String
        Try



            ReturnFromQuery = ""
            con.ConnectionString = str
            con.Open()
                Dim oCmd As SqlClient.SqlCommand
                oCmd = New SqlClient.SqlCommand
                With oCmd
                    .CommandText = xQuery
                    .CommandType = CommandType.Text
                    .Connection = con
                    ReturnFromQuery = .ExecuteScalar
                End With
                con.Close()





        Catch ex As Exception
            ReturnFromQuery = "0"
            con.Close()

        End Try
    End Function
    Public Sub ActivityANDEmailLog(ByVal Expr As String)
        'if email set=yes Send Activity toemail
        If ReturnCheckCondition("cmstblCoy", "EnableActivity", " Where EnableActivity='Yes' and Branchcode='" + BranchCode + "'") = True Then

            'if Activity set=yes Start recoding the activities
            Dim username, Dept, branchCode As String
            username = CType(Current.Session("UserName"), String)
            branchCode = CType(Current.Session("branchCode"), String)
            If ReturnCheckCondition("cmstblJohn", "Username", " where username='" & username & "'") = False Then

                Dept = ReturnCondition("cmstbllog", "Dept", " Where Username='" & username & "'" & AndBranchCode)
                Dim xFields As String = "UserName,Dept,Activities,Device,IPAddress,BranchCode"
                Dim xTexts As String = "'" & username & "'," &
                 "'" & Dept & "'," &
                  "'" & Expr & "'," &
                   "'" & mypc & "'," &
                    "'" & cuIP & "'," &
                   "'" & branchCode & "'"
                'DH & Now.Date & DH + "," & _
                ' DH & Now & DH
                InsertData("cmstblActivity", xFields, xTexts)
            End If
        End If
        'if Activity set=yes Start recoding the activities

    End Sub

    Public Function Userfullname(ByVal Username As String) As String
        Userfullname = ""
        Dim s As String
        If IsJohn(Username) = True Then
            s = "Engr. Programmer User"
        Else
            s = ReturnConditionNA("cmstbllog", "Fullname", " where username='" & Username + "'" & AndBranchCode)

        End If
        Return s

    End Function

    Public Function UsernameEMAIL(ByVal Username As String) As String
        UsernameEMAIL = ""
        Dim s As String

        s = ReturnConditionNA("cmstbllog", "Email", " where username='" & Username + "'" & AndBranchCode)
        Return s

    End Function
    Public Function AllowEmailSending() As Boolean
        AllowEmailSending = False
        If ReturnCheckCondition("cmstblCoy", "EnableEmail", " Where EnableEmail='Yes'" & AndBranchCode) = True Then
            AllowEmailSending = True

            Return AllowEmailSending

        End If
    End Function
    Public Function sendmail(ByVal MailHost As String, ByVal MailCredential As String, ByVal MailPassword As String, ByVal MailFrom As String, ByVal MailTo As String, ByVal MailSubject As String, ByVal MailBody As String) As String
        'Try

        Dim MySmtpServer As New SmtpClient

            Dim MyMail As New MailMessage()
            MySmtpServer.Credentials = New Net.NetworkCredential(MailCredential, MailPassword) 'cle@yahoo.com,1234
            MySmtpServer.Host = MailHost 'mail.yahoo.com
            MyMail = New MailMessage()
            MyMail.From = New MailAddress(MailFrom) 'cele@yahoo.com
            MyMail.To.Add(MailTo) 'recipient emails
            ' MyMail.Bcc.Add(emails)
            MyMail.Subject = MailSubject
            MyMail.IsBodyHtml = False

            MyMail.Body = MailBody
            MySmtpServer.Send(MyMail)


        'Catch ex As Exception
        '    ActivityANDEmailLog("Error occured sending email to user from send")
        'End Try
    End Function

    Public Sub SendEmailto(ByVal EmailTo As String, ByVal Msubject As String, ByVal Mailbody As String)
        'Try
        Dim Host As String = ReturnConditionNA("cmstblCoy", "MailHost", " Where EnableEmail='Yes'" & AndBranchCode)
            Dim Mcredential As String = ReturnConditionNA("cmstblCoy", "MailCredential", " Where EnableEmail='Yes'" & AndBranchCode)
            Dim Passw As String = ReturnConditionNA("cmstblCoy", "MailPassword", " Where EnableEmail='Yes'" & AndBranchCode)
            Dim Mailfrom As String = ReturnConditionNA("cmstblCoy", "MailFrom", " Where EnableEmail='Yes'" & AndBranchCode)


            'before sending mail, make sure there is internet connection
            If AllowEmailSending() = True Then
                ' USerName = UsernameEMAIL(USerName)
                sendmail(Host, Mcredential, Passw, Mailfrom, EmailTo, Msubject, Mailbody)
            End If
        'Catch ex As Exception
        '    'ActivityANDEmailLog("Error occured sending email to user")
        'End Try
    End Sub


    Public Function EmailList(ByVal roleName As String, ByVal andOthercondition As String) As String
        Dim email As String

        email = ""
        If ReturnCheckCondition("cmstbllog", "Username", " where " & roleName & " ='Yes' and dCode='" & dCode & "'") = True Then
            Dim xFields As String = "[Email]"

            AndCondition = " where " & roleName & " ='Yes'and dCode='" & dCode & "'" & andOthercondition
            DT = ReportSelectedDataTable("cmstbllog", xFields, AndCondition)

            email = ""
            For i = 0 To DT.Rows.Count - 1
                If DT.Rows(i).Item("Email").ToString.Length > 0 Then
                    email = email + DT.Rows(i).Item("Email").ToString & ","
                End If
            Next
            'check if you have any email, then remove the last comma
            If email.Trim.Length > 0 Then
                email = email.Substring(0, email.Length - 1)
            Else
                email = ""
            End If
        Else
            email = ""
        End If
        Return email

    End Function


    Public Function EmailListRMgr() As String
        Try
            EmailListRMgr = ""

            Dim brcDT As DataTable
            DT = ReportDataTable2DISTINCT("cmstblcoy", "BranchCode", " where region='" & region & "'")
            If DT.Rows.Count > 0 Then
                Dim cnt As Single = DT.Rows.Count
                Dim RMgr As String
                Dim Brc As String 'each branchcode
                Dim j As Integer
                For j = 0 To DT.Rows.Count - 1
                    Brc = DT.Rows(j)("BranchCode").ToString
                    'check if brc has any Rmgr
                    If ReturnCheckCondition("cmstbllog", "RMgr", " where branchcode='" & Brc & "'") = True Then
                        brcDT = ReportDataTable2DISTINCT("cmstbllog", "Email", " where RMgr='Yes' and branchCode='" & Brc & "'")
                        'get RMgrs Emails
                        If brcDT.Rows.Count > 0 Then
                            Dim jj As Integer
                            For jj = 0 To brcDT.Rows.Count - 1
                                EmailListRMgr = EmailListRMgr + brcDT.Rows(jj)("Email").ToString & ","

                            Next
                        End If

                    End If



                Next

                'check if you have any email, then remove the last comma
                If EmailListRMgr.Trim.Length > 0 Then
                    EmailListRMgr = EmailListRMgr.Substring(0, EmailListRMgr.Length - 1)
                Else
                    EmailListRMgr = ""
                End If
                Return EmailListRMgr

            End If

        Catch ex As Exception

        End Try
    End Function


    'MFA CHECKS



    Public Function userlastlogindate(ByVal Username As String) As Date
        Try


            Dim res As Date
            res = Now
            If ReturnCheckCondition("tblfma", "nTime", " where Username='" + Username + "' and dCode='" + CType(Current.Session("dCode"), String) + "' and status='Completed' order by nTime Desc") = True Then
                res = CDate(ReturnConditionNA("tblfma", "nTime", " where Username='" + Username + "' and dCode='" + CType(Current.Session("dCode"), String) + "' and status='Completed' order by nTime Desc"))
            Else
                res = Now
            End If
            Return res
            userlastlogindate = res

        Catch ex As Exception
            userlastlogindate = Now
        End Try
    End Function

    Public Function userlastDevice(ByVal Username As String) As String
        Try


            Dim res As String
            res = "No"
            If ReturnCheckCondition("tblfma", "Device", " where Username='" + Username + "' and dCode='" + CType(Current.Session("dCode"), String) + "' and status='Completed' order by nTime Desc") = True Then
                res = ReturnConditionNA("tblfma", "Device", " where Username='" + Username + "' and dCode='" + CType(Current.Session("dCode"), String) + "' and status='Completed' order by nTime Desc")
            Else
                res = "No"
            End If
            Return res
            userlastDevice = res

        Catch ex As Exception
            userlastDevice = "No"
        End Try
    End Function

    Public Function userlastIP(ByVal Username As String) As String
        Try


            Dim res As String
            res = "No"
            If ReturnCheckCondition("tblfma", "LocationIP", " where Username='" + Username + "' and dCode='" + CType(Current.Session("dCode"), String) + "' and status='Completed' order by nTime Desc") = True Then
                res = ReturnConditionNA("tblfma", "LocationIP", " where Username='" + Username + "' and dCode='" + CType(Current.Session("dCode"), String) + "' and status='Completed' order by nTime Desc")
            Else
                res = "No"
            End If
            Return res
            userlastIP = res

        Catch ex As Exception
            userlastIP = "No"
        End Try
    End Function

    Public Function userlastBrowswer(ByVal Username As String) As String
        Try


            Dim res As String
            res = "No"
            If ReturnCheckCondition("tblfma", "Browser", " where Username='" + Username + "' and dCode='" + CType(Current.Session("dCode"), String) + "' and status='Completed' order by nTime Desc") = True Then
                res = ReturnConditionNA("tblfma", "Browser", " where Username='" + Username + "' and dCode='" + CType(Current.Session("dCode"), String) + "' and status='Completed' order by nTime Desc")
            Else
                res = "No"
            End If
            Return res
            userlastBrowswer = res

        Catch ex As Exception
            userlastBrowswer = "No"
        End Try
    End Function
    Public Function IsMFARequired(ByVal Username As String, ByVal BranchCode As String) As Boolean



        'Check if EnableMFA-yes for this branch

        If ReturnCheckCondition("cmstblCoy", "EnableMFA", " Where EnableMFA='Yes' and BranchCode='" & BranchCode & "'") = True Then
            'go ahead and check if he requires MFA based on other factors

            IsMFARequired = True
            'check last login

            'No of hours set
            Dim mfahours As Integer
            mfahours = ReturnFromQuery("select mfaLastloginTime from cmstblcoy where BranchCode='" & BranchCode & "'")

            'No of minutes to wait set
            Dim mfaminutes As Integer
            mfaminutes = ReturnFromQuery("select mfaResponseTime from cmstblcoy where BranchCode='" & BranchCode & "'")

            'when was the last login date
            'get the current date
            Dim cuDate As Date = Now.Date

            'get current device
            Dim cuDevice As String = mypc
            'get current browser
            Dim cuBrowser = HttpContext.Current.Request.Browser.Browser
            'get current IP


            If DateDiff(DateInterval.Hour, userlastlogindate(Username), cuDate) < mfahours And userlastBrowswer(Username) = cuBrowser And userlastDevice(Username) = cuDevice And userlastIP(Username) = cuIP Then

                IsMFARequired = False
            Else
                IsMFARequired = True
            End If

            Return IsMFARequired


        Else
            IsMFARequired = False
            Return IsMFARequired
        End If
    End Function


    Public Function userlastInCompletedCode(ByVal Username As String) As String
        Try


            Dim res As String
            res = "No"
            If ReturnCheckCondition("tblfma", "EmailCode", " where Username='" + Username + "' and dCode='" + CType(Current.Session("dCode"), String) + "' and status='Pending' order by nTime Desc") = True Then
                res = ReturnConditionNA("tblfma", "EmailCode", " where Username='" + Username + "' and dCode='" + CType(Current.Session("dCode"), String) + "' and status='Pending' order by nTime Desc")
            Else
                res = "No"
            End If
            Return res
            userlastInCompletedCode = res

        Catch ex As Exception
            userlastInCompletedCode = "No"
        End Try
    End Function

    Public Function userlastInCompletedMinutethecodewassent(ByVal Username As String) As String
        Try


            Dim res As Date
            res = Now
            If ReturnCheckCondition("tblfma", "nTime", " where Username='" + Username + "' and dCode='" + CType(Current.Session("dCode"), String) + "' and status='Pending' order by nTime Desc") = True Then
                res = ReturnConditionNA("tblfma", "nTime", " where Username='" + Username + "' and dCode='" + CType(Current.Session("dCode"), String) + "' and status='Pending' order by nTime Desc")
            Else
                res = Now
            End If
            Return res
            userlastInCompletedMinutethecodewassent = res

        Catch ex As Exception
            userlastInCompletedMinutethecodewassent = Now
        End Try
    End Function


    'the ID ofthe row where the last INCOMPLETED Code was sent
    Public Function userlastInCompletedCode_ID(ByVal Username As String) As Integer
        Try


            Dim res As Integer
            res = 0
            If ReturnCheckCondition("tblfma", "ID", " where Username='" + Username + "' and dCode='" + CType(Current.Session("dCode"), String) + "' and status='Pending' order by nTime Desc") = True Then
                res = ReturnConditionNA("tblfma", "ID", " where Username='" + Username + "' and dCode='" + CType(Current.Session("dCode"), String) + "' and status='Pending' order by nTime Desc")
            Else
                res = 0
            End If
            Return res
            userlastInCompletedCode_ID = res

        Catch ex As Exception
            userlastInCompletedCode_ID = 0
        End Try
    End Function

    Public Function SendSMS(ByVal toPhoneNumber As String, ByVal SmsBody As String) As String
        Try


            Dim webrequest As Net.WebRequest
            Dim webresponse As Net.WebResponse
            Dim webresponsestring As String = ""
            'Dim message As String = body
            'Dim subject As String = Strings.Left(cnamez.Trim, 11)
            '  Dim url As String = "https://secure.xwireless.net/api/v2/SendSMS?ApiKey={ApiKey}&ClientId={ClientId}&SenderId={SenderId}&Message={Message}&MobileNumber={MobileNumber}&Is_Unicode={Is_Unicode}&Is_Flash={Is_Flash}&serviceId={serviceId}&CoRelator={CoRelator}&LinkId={LinkId}
            'Dim url As String = "http://smsc.xwireless.net/API/WebSMS/Http/v2.0/?method =compose&username=Anyaegbunam20&password=bridgetM_9&sender=" & Subject & "&to=" & toPhoneNumber & "&message=" & SmsBody & "& international=1& format=json"

            'Dim url As String = "https://secure.xwireless.net/api/v2/SendSMS?ApiKey=bridgetM_9&ClientId=Anyaegbunam20&SenderId=" & Subject & "&Message=" & SmsBody & " &MobileNumber=" & toPhoneNumber & ""
            'NOTE: Phone numbers must be like this 2348184253622

            Dim url As String = "https://secure.xwireless.net/api/v2/SendSMS?SenderId=HALOGEN&Is_Unicode=false&Is_Flash=false&Message=" & SmsBody & "&MobileNumbers=" & toPhoneNumber & "&ApiKey=fkeWD8O+6xnVkek7el41afcnm05PylIEL8kGj8Xmir8=&ClientId=7d319d2d-7002-4c6f-b40f-905c6c4c89f8"
            'Dim url As String = "https://secure.xwireless.net/api/v2/SendSMS?SenderId=" & sendIdentity & "&Is_Unicode=false&Is_Flash=false&Message=" & SmsBody & "&MobileNumbers=" & toPhoneNumber & "&ApiKey=fkeWD8O+6xnVkek7el41afcnm05PylIEL8kGj8Xmir8=&ClientId=7d319d2d-7002-4c6f-b40f-905c6c4c89f8"

            webrequest = Net.HttpWebRequest.Create(url)
            webrequest.Timeout = 25000
            webresponse = webrequest.GetResponse
            Dim reader As IO.StreamReader = New IO.StreamReader(webresponse.GetResponseStream)
            webresponsestring = reader.ReadToEnd
            webresponse.Close()
        Catch ex As Exception

        End Try
    End Function
End Class

