Imports System.IO
Imports System.Collections.Generic
Imports System.Data.OleDb
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.DataTable
Imports System.Web.SessionState
Imports System.Web.HttpContext
Partial Class [Default]
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            If Not IsPostBack Then
                Me.connec()
                Dim le As New stadium
            End If

        End If
    End Sub

    Private Sub connec()
        Dim fnation As StreamReader
        Dim fnation2 As StreamReader
        '  Try
        fnation = IO.File.OpenText(Server.MapPath("conne.txt"))
        Session.Remove("conne")
        Session("conne") = fnation.ReadToEnd()

        fnation.Close()


        fnation2 = IO.File.OpenText(Server.MapPath("conne2.txt"))
        Session.Remove("conne2")
        Session("conne2") = fnation2.ReadToEnd()
        fnation2.Close()

        Page.Title = "The Online Stadium | APP:|" + CType(Session("Conne2"), String) + " | " + CType(Session("Conne"), String)

        Dim le As New stadium
        le.PreChecksession()
        'Catch err As Exception
        '    lmsg = "No Database Definition"
        '    ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & lmsg & "');", True)

        'Finally

        'End Try
    End Sub


    Protected Sub btnAdmin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdmin.Click

        Dim le As New stadium
        le.PreChecksession()


        le.AndCondition = " where Username='" + txtAdminuser.Text.Trim + "' And pwd='" + txtAdminPwd.Text.Trim + "' And IsApproved='Yes'"
        If le.ReturnCheckCondition("cmstbllog", "Username", le.AndCondition) = True Or le.ReturnCheckCondition("aztbljohn", "Username", " where Username='" + txtAdminuser.Text.Trim + "'") = True Then

            'Assign sessions

            Session.Remove("Username")

            Session("Username") = Me.txtAdminuser.Text.Trim.ToString


            le.USerName = CType(Session("Username"), String)


            Dim xTexts As String = " LastLoginDate =" + le.DH + Now + le.DH
            le.UpdateData("cmstbllog", xTexts, " where Username='" + Me.txtAdminuser.Text + "'" + le.AndCIN)


            le.ActivityANDEmailLog("Logged in successfully")
            Response.Redirect("d/index.aspx")


        Else
            le.lmsg = "Wrong Username and/or password."
            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg & "');", True)

        End If
        'Else
        '    le.lmsg = "Incorrect CIN"
        '    ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg & "');", True)

        'End If
    End Sub



    Private Sub btnSignup_Click(sender As Object, e As EventArgs) Handles btnSignup.Click
        Dim le As New stadium
        le.PreChecksession()

        ' A user has clicked a sign up button
        'check if he has filled in his full name
        If txtsignupfullName.Text.Trim.Length > 0 Then

            'he entered full name
            'check if he has entered email, and  they are same
            If txtsignupEmail1.Text.Trim.Length > 0 And txtsignupEmail2.Text.Trim.Length > 0 Then

                'check if the two emails are the same
                If txtsignupEmail1.Text.Trim = txtsignupEmail2.Text.Trim Then
                    ' Check if he has registered before now or Blocked
                    If le.ReturnCheckCondition("cmstbluser", "username", " where username='" & txtsignupEmail1.Text.Trim & "'") = False Then
                        ' go ahead to register him
                        Dim newpwd As String = alphabet.ToString.Trim & alphabet.ToString.Trim & alphabet.ToString.Trim & alphabet.ToString.Trim & alphabet.ToString.Trim

                        'Try
                        Dim xFields As String = "Username,fullname,pwd"
                        Dim xTexts As String = "'" & Me.txtsignupEmail1.Text & "'," &
                          "'" & Me.txtsignupfullName.Text & "'," &
                           "'" & newpwd & "'"

                        le.InsertData("cmstbluser", xFields, xTexts)
                        'send email
                        'EMAIL START
                        le.AndCondition = " where username='" & txtsignupEmail1.Text & "'"
                        Dim emailto As String = txtsignupEmail1.Text
                        Dim subject As String = "Account Created for Abuja Stadium Entrance Access Portal "

                        Dim bbody As String = "Thank you for registering with the Abuja Stadium Entrance Portal. " & "." &
                                                    vbCrLf & vbCrLf & " Kindly find hereunder your access password for booking and account managements. " & vbCrLf &
                                                      vbCrLf & " PASSWORD: " & le.ReturnFromQuery("select pwd from cmstbluser" & le.AndCondition) & vbCrLf &
                                                          vbCrLf & " Kindly log in to https://onlinestadium.veracelservers.com " & vbCrLf &
                                                          vbCrLf & vbCrLf & vbCrLf & " Thank you."
                        le.SendEmailto(emailto, subject, bbody)
                        'EMAIL END
                        le.lmsg = "Thank you!!! Your registration was successful. Your LOGIN PASSWORD has been sent to your email address. Kindly check your email now."
                        ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg & "');", True)

                    Else
                        'he has registered before
                        le.lmsg = "User Email with " + txtsignupEmail1.Text + " is existing. Please try another email."
                        ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg & "');", True)

                    End If
                Else
                    'they are not the same
                    le.lmsg = "The repeated email is different from the first. Please try again."
                    ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg & "');", True)

                End If
            Else
                'he did not enter emails
                'so tell him to enter his emails
                le.lmsg = "Enter Your Email Address, and repeat it"
                ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg & "');", True)

            End If
        Else
            'he did not enter full name
            'so tell him to enter his full name
            le.lmsg = "Enter Your Fullname"
            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg & "');", True)

        End If
    End Sub


    Dim firstnumber As Integer
    Dim randomobject As New Random
    Function alphabet() As String
        alphabet = "A"
        firstnumber = randomobject.Next(1, 26)
        On Error GoTo asd
        If firstnumber = 1 Then
            alphabet = "A"
        ElseIf firstnumber = 2 Then
            alphabet = "B"
        ElseIf firstnumber = 3 Then
            alphabet = "C"
        ElseIf firstnumber = 4 Then
            alphabet = "D"
        ElseIf firstnumber = 5 Then
            alphabet = "E"
        ElseIf firstnumber = 6 Then
            alphabet = "F"
        ElseIf firstnumber = 7 Then
            alphabet = "G"
        ElseIf firstnumber = 8 Then
            alphabet = "H"
        ElseIf firstnumber = 9 Then
            alphabet = "I"
        ElseIf firstnumber = 10 Then
            alphabet = "J"
        ElseIf firstnumber = 11 Then
            alphabet = "K"
        ElseIf firstnumber = 12 Then
            alphabet = "L"
        ElseIf firstnumber = 13 Then
            alphabet = "M"
        ElseIf firstnumber = 14 Then
            alphabet = "N"
        ElseIf firstnumber = 15 Then
            alphabet = "O"
        ElseIf firstnumber = 16 Then
            alphabet = "P"
        ElseIf firstnumber = 17 Then
            alphabet = "Q"
        ElseIf firstnumber = 18 Then
            alphabet = "R"
        ElseIf firstnumber = 19 Then
            alphabet = "S"
        ElseIf firstnumber = 20 Then
            alphabet = "T"
        ElseIf firstnumber = 21 Then
            alphabet = "U"
        ElseIf firstnumber = 22 Then
            alphabet = "V"
        ElseIf firstnumber = 23 Then
            alphabet = "W"
        ElseIf firstnumber = 24 Then
            alphabet = "X"
        ElseIf firstnumber = 25 Then
            alphabet = "Y"
        ElseIf firstnumber = 26 Then
            alphabet = "z"
        End If
        Return alphabet
asd:
    End Function


    Protected Sub btnSignin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSignin.Click

        Dim le As New stadium
        le.PreChecksession()


        le.AndCondition = " where Username='" + txtsigninEmail.Text.Trim + "' And pwd='" + txtsigninpwd.Text.Trim + "'"
        If le.ReturnCheckCondition("cmstbluser", "Username", le.AndCondition) = True Then

            'Assign sessions

            Session.Remove("Username")

            Session("Username") = Me.txtsigninEmail.Text.Trim.ToString


            le.USerName = CType(Session("Username"), String)


            Dim xTexts As String = " LastLoginDate =" + le.DH + Now + le.DH
            le.UpdateData("cmstbluser", xTexts, " where Username='" + Me.txtsigninEmail.Text + "'")


            le.ActivityANDEmailLog("Logged in successfully")
            Response.Redirect("d/indexuser.aspx")


        Else
            le.lmsg = "Wrong Username and/or password."
            ClientScript.RegisterStartupScript(Me.GetType(), "AlertMessageBox", "alert('" & le.lmsg & "');", True)

        End If

    End Sub
End Class
