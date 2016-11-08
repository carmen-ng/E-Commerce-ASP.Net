
Imports System.Data.SqlClient
Imports System.Data

Partial Class MasterPage
    Inherits System.Web.UI.MasterPage
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If tbSearch.Text <> "" Then
            Dim strSQL As String

            Dim strConn As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\StoreDatabase.mdf;Integrated Security=True;Connect Timeout=30"
            Dim connProduct As SqlConnection
            Dim cmdProduct As SqlCommand
            Dim drProduct As SqlDataReader

            'Dim tbText() As String = Split(tbSearch.Text)

            'For i As Integer = 0 To tbText.Length
            strSQL = "Select * from Product Where ProductID ='" + tbSearch.Text + "' Or ProductName ='" + tbSearch.Text + "' Or ProductDescription = '" + tbSearch.Text + "'"
            connProduct = New SqlConnection(strConn)
            cmdProduct = New SqlCommand(strSQL, connProduct)
            connProduct.Open()
            drProduct = cmdProduct.ExecuteReader(CommandBehavior.CloseConnection)

            If drProduct.Read() Then
                Response.Redirect("ProductDetail.aspx?ProductID=" & drProduct.Item("ProductID"))
            Else
                drProduct.Close()
                connProduct.Close()
                connProduct.Open()

                strSQL = "Select * from Product Where ProductID LIKE '%" + tbSearch.Text + "%' Or ProductName LIKE '%" + tbSearch.Text + "%' Or ProductDescription LIKE '%" + tbSearch.Text + "%'"
                cmdProduct = New SqlCommand(strSQL, connProduct)
                drProduct = cmdProduct.ExecuteReader(CommandBehavior.CloseConnection)

                If drProduct.Read() Then
                    connProduct.Close()
                    Response.Redirect("ProductList.aspx" + "?Search=" + tbSearch.Text)
                Else
                    connProduct.Close()
                    Response.Redirect("ProductList.aspx")
                End If
            End If
        End If
    End Sub
End Class

