Imports System.Data
Imports System.Data.SqlClient
Partial Class ProductDetail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Request.QueryString("ProductID") <> "" Then
            Dim strConn As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\StoreDatabase.mdf;Integrated Security=True;Connect Timeout=30"
            Dim connProduct As SqlConnection
            Dim cmdProduct As SqlCommand
            Dim drProduct As SqlDataReader
            Dim strSQL As String = "Select * from Product Where ProductID = '" & Request.QueryString("ProductID") & "'"
            'Response.Write(strSQL)
            connProduct = New SqlConnection(strConn)
            cmdProduct = New SqlCommand(strSQL, connProduct)
            connProduct.Open()
            drProduct = cmdProduct.ExecuteReader(CommandBehavior.CloseConnection)
            'drProduct.Read()
            If drProduct.Read() Then
                lblProductName.Text = drProduct.Item("ProductName")
                lblProductDesc.Text = drProduct.Item("ProductDescription")
                lblProductPrice.Text = drProduct.Item("ProductPrice")
                lblProductID.Text = drProduct.Item("ProductID")
                imgProduct.ImageUrl = "images/products/" + Trim(drProduct.Item("ProductID")) + ".png"
            End If

            Dim category As String = ""

            Select Case drProduct.Item("CategoryID")
                Case 5, 6, 7
                    category = "Phones"
                Case 8, 9, 10
                    category = "Tablets"
                Case 11, 12, 13
                    category = "Laptops"
                Case Else
            End Select


            Dim breadcrumb As String = ""
            Select Case drProduct.Item("CategoryID")
                Case 5, 8, 11
                    breadcrumb = "Apple"
                Case 6, 9
                    breadcrumb = "Samsung"
                Case 7, 10
                    breadcrumb = "Google"
                Case 12
                    breadcrumb = "Lenovo"
                Case 13
                    breadcrumb = "Asus"
                Case Else
            End Select

            lblCategoryName.Text = category
            lblSubCategoryName.Text = breadcrumb
            lblProduct.Text = drProduct.Item("ProductName")
        End If
    End Sub

    Protected Sub btnCart_Click(sender As Object, e As EventArgs) Handles btnCart.Click

        Dim drCartLine As SqlDataReader
        Dim strSQLStatement As String
        Dim cmdSQL As SqlCommand
        Dim strConnectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("StoreDatabaseConnectionString").ConnectionString
        Dim conn As New SqlConnection(strConnectionString)

        conn.Open()
        ' *** get product price
        strSQLStatement = "SELECT * FROM Product WHERE ProductID = '" & lblProductID.Text & "'"
        cmdSQL = New SqlCommand(strSQLStatement, conn)
        drCartLine = cmdSQL.ExecuteReader()
        Dim sngPrice As Single
        If drCartLine.Read() Then
            sngPrice = drCartLine.Item("ProductPrice")
        End If
        '*** get CartID
        Dim strCartID As String
        If HttpContext.Current.Request.Cookies("CartID") Is Nothing Then
            strCartID = GetRandomCartID(10)
            Dim CookieTo As New HttpCookie("CartID", strCartID)
            HttpContext.Current.Response.AppendCookie(CookieTo)
        Else
            Dim CookieBack As HttpCookie
            CookieBack = HttpContext.Current.Request.Cookies("CartID")
            strCartID = CookieBack.Value
        End If
        conn.Close()

        If IsNumeric(tbQuantity.Text) Then

            ' figure out if this product already exits in the cart
            ' hint: before you issue the insert command, you check the cart
            strSQLStatement = "SELECT COUNT(*) FROM CartLine WHERE ProductID = '" & lblProductID.Text & "' AND CartID = '" & strCartID & "'"
            cmdSQL = New SqlCommand(strSQLStatement, conn)
            conn.Open()
            Dim numItems As Integer = cmdSQL.ExecuteScalar()
            conn.Close()

            If numItems > 0 Then
                strSQLStatement = "SELECT Quantity FROM CartLine WHERE ProductID = '" & lblProductID.Text & "' AND CartID = '" & strCartID & "'"
                cmdSQL = New SqlCommand(strSQLStatement, conn)
                conn.Open()
                Dim quantity As Integer = cmdSQL.ExecuteScalar()
                conn.Close()
                Dim newQuantity As Integer = CInt(tbQuantity.Text) + quantity

                strSQLStatement = "Update CartLine set Quantity = '" & newQuantity & "' WHERE ProductID = '" & lblProductID.Text & "' AND CartID = '" & strCartID & "'"
                cmdSQL = New SqlCommand(strSQLStatement, conn)
                conn.Open()
                drCartLine = cmdSQL.ExecuteReader(CommandBehavior.CloseConnection)
                Response.Redirect("ViewCart.aspx")
                conn.Close()
            Else
                strSQLStatement = "INSERT INTO CartLine (CartID, ProductID, ProductName, Quantity, ProductPrice) values('" & strCartID & "', '" & lblProductID.Text & "', '" & lblProductName.Text & "', " & CInt(tbQuantity.Text) & ", " & sngPrice & ")"
                cmdSQL = New SqlCommand(strSQLStatement, conn)
                conn.Open()
                drCartLine = cmdSQL.ExecuteReader(CommandBehavior.CloseConnection)
                Response.Redirect("ViewCart.aspx")
            End If
        End If
    End Sub

    Public Function GetRandomCartID(ByVal length As Integer) As String
        Dim cartChars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim rand As New Random
        Dim sb As New StringBuilder
        For i As Integer = 1 To 10
            Dim idx As Integer = rand.Next(0, 35)
            sb.Append(cartChars.Substring(idx, 1))
        Next
        Return sb.ToString()
    End Function
End Class