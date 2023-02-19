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
    Inherits Page

    Dim le As New mfa1

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Me.IsPostBack = False Then
            If Not IsPostBack Then
                Session.Clear()
                Page.Title = "MFA IMPLEMENTATION  | HALOGEN GROUP:|"



                ' Dim le As New defer
                le.PreChecksession()

            End If

        End If
    End Sub
End Class