Imports System.Data
Imports System.Data.SqlClient
Partial Class SubCategory
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Request.QueryString("CategoryID") <> "" Then
            Dim strSQL As String = "Select * From Category Where parent = " & CInt(Request.QueryString("CategoryID"))
            'Response.Write(strSQL)
            sqlDSSubCategory.SelectCommand = strSQL
            sqlDSSubCategory.DataBind()
            lblCategoryName.Text = Request.QueryString("CategoryName")
            lblSubCategoryName.Text = "All"

            Dim strSQL2 As String = "Select * From Product P, Category C Where C.Id = P.CategoryID And C.parent = " & CInt(Request.QueryString("CategoryID"))
            'Response.Write(strSQL)
            sqlDSProductList.SelectCommand = strSQL2
            sqlDSProductList.DataBind()
        End If
        If Request.QueryString("SubCategoryID") <> "" Then
            'pnlFeatureProducts.Visible = False
            pnlProductList.Visible = True
            Dim strSQL2 As String = "Select * From Product Where CategoryID = " & CInt(Request.QueryString("SubCategoryID"))
            'Response.Write(strSQL)
            sqlDSProductList.SelectCommand = strSQL2
            sqlDSProductList.DataBind()
            lblCategoryName.Text = Request.QueryString("CategoryName")
            lblSubCategoryName.Text = Request.QueryString("SubCategoryName")
        End If
    End Sub

End Class
