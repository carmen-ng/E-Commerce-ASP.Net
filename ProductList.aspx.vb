
Partial Class ProductList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim strSQL1 As String = "Select * From Category Where parent = 0"
        sqlDSCategory.SelectCommand = strSQL1
        sqlDSCategory.DataBind()

        Dim searchString As String = Request.QueryString("Search")

        If (searchString <> "") Then
            pnlProductList.Visible = True
            Dim strSQL2 As String = "Select * from Product Where ProductID LIKE '%" + searchString + "%' Or ProductName LIKE '%" + searchString + "%' Or ProductDescription LIKE '%" + searchString + "%'"
            sqlDSProductList.SelectCommand = strSQL2
            sqlDSProductList.DataBind()
        Else
            pnlProductList.Visible = True
            Dim strSQL2 As String = "Select * From Product P Order By P.CategoryID"
            sqlDSProductList.SelectCommand = strSQL2
            sqlDSProductList.DataBind()
        End If
    End Sub

End Class
