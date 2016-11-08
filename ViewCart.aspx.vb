Imports System.Data
Imports System.Data.SqlClient

Partial Class ViewCart
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim strCartID As String
        If HttpContext.Current.Request.Cookies("CartID") Is Nothing Then

        Else
            Dim CookieBack As HttpCookie
            CookieBack = HttpContext.Current.Request.Cookies("CartID")
            strCartID = CookieBack.Value
            Dim strSQL As String = "Select * From Cartline where cartid = '" & strCartID & "'"
            SqlDSCart.SelectCommand = strSQL
            SqlDSCart2.SelectCommand = strSQL

            Dim strConn As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\StoreDatabase.mdf;Integrated Security=True;Connect Timeout=10"
            Dim connCart As SqlConnection
            Dim cmdCart As SqlCommand
            connCart = New SqlConnection(strConn)
            Dim strSQL2 As String = "Select ROUND(SUM(ProductPrice * Quantity), 2) AS Total FROM CartLine WHERE CartID = '" & strCartID & "'"
            cmdCart = New SqlCommand(strSQL2, connCart)
            connCart.Open()
            Dim totalPrice = cmdCart.ExecuteScalar()

            lblCartTotal.Text = "Cart Total: $" + totalPrice.ToString()
            connCart.Close()
        End If

    End Sub

    Protected Sub lvCart_OnItemCommand(ByVal sender As Object, ByVal e As ListViewCommandEventArgs)
        If e.CommandName = "cmdUpdate" Then
            Dim tbQuantity As TextBox = CType(e.Item.FindControl("tbQuantity"), TextBox)
            If IsNumeric(tbQuantity.Text) Then
                ' get cartid from cookies, productid, and quantity
                Dim strCartID As String
                Dim CookieBack As HttpCookie
                CookieBack = HttpContext.Current.Request.Cookies("CartID")
                strCartID = CookieBack.Value
                Dim strProductID As String = e.CommandArgument
                Dim strSQL As String = "Update CartLine set Quantity = '" & CInt(tbQuantity.Text) & "' where ProductID = '" & strProductID & "' and CartID = '" & strCartID & "'"
                ' update
                Dim strConn As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\StoreDatabase.mdf;Integrated Security=True;Connect Timeout=30"
                Dim connCart As SqlConnection
                Dim cmdCart As SqlCommand
                Dim drCart As SqlDataReader
                connCart = New SqlConnection(strConn)
                cmdCart = New SqlCommand(strSQL, connCart)
                connCart.Open()
                drCart = cmdCart.ExecuteReader(CommandBehavior.CloseConnection)

                SqlDSCart.DataBind()
                lvCart.DataBind()
                connCart.Close()

                Dim strSQL2 As String = "Select ROUND(SUM(ProductPrice * Quantity), 2) AS Total FROM CartLine WHERE CartID = '" & strCartID & "'"
                cmdCart = New SqlCommand(strSQL2, connCart)
                connCart.Open()
                Dim totalPrice As String = cmdCart.ExecuteScalar()

                lblCartTotal.Text = "Cart Total: $" + totalPrice
                connCart.Close()
            End If

        ElseIf e.CommandName = "cmdDelete" Then
            Dim strCartID As String
            Dim CookieBack As HttpCookie
            CookieBack = HttpContext.Current.Request.Cookies("CartID")
            strCartID = CookieBack.Value
            Dim strProductID As String = e.CommandArgument
            Dim strSQL As String = "Delete From CartLine where ProductID = '" & strProductID & "' and CartID = '" & strCartID & "'"
            Dim strConn As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\StoreDatabase.mdf;Integrated Security=True;Connect Timeout=30"
            Dim connCart As SqlConnection
            Dim cmdCart As SqlCommand
            Dim drCart As SqlDataReader
            connCart = New SqlConnection(strConn)
            cmdCart = New SqlCommand(strSQL, connCart)
            connCart.Open()
            drCart = cmdCart.ExecuteReader(CommandBehavior.CloseConnection)
            sqlDSCart.DataBind()
            lvCart.DataBind()
            connCart.Close()

            Dim strSQL2 As String = "Select ROUND(SUM(ProductPrice * Quantity), 2) AS Total FROM CartLine WHERE CartID = '" & strCartID & "'"
            cmdCart = New SqlCommand(strSQL2, connCart)
            connCart.Open()
            Try
                Dim totalPrice As String = cmdCart.ExecuteScalar()

                lblCartTotal.Text = "Cart Total: $" + totalPrice
            Catch ex As Exception
                lblCartTotal.Text = "Cart Total: $"
            End Try

            connCart.Close()
        End If
    End Sub

    Sub lbEmpty_Click(sender As Object, e As EventArgs)
        If (Not Request.Cookies("CartID") Is Nothing) Then
            Dim strCartID As String
            Dim CookieBack As HttpCookie
            CookieBack = HttpContext.Current.Request.Cookies("CartID")
            strCartID = CookieBack.Value
            Dim strSQL As String = "DELETE FROM CartLine where CartID = '" & strCartID & "'"
            Dim strConn As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\StoreDatabase.mdf;Integrated Security=True;Connect Timeout=10"
            Dim connCart As SqlConnection
            Dim cmdCart As SqlCommand
            Dim drCart As SqlDataReader
            connCart = New SqlConnection(strConn)
            cmdCart = New SqlCommand(strSQL, connCart)
            connCart.Open()
            drCart = cmdCart.ExecuteReader(CommandBehavior.CloseConnection)
            SqlDSCart.DataBind()
            connCart.Close()


            Dim myCookie As HttpCookie
            myCookie = New HttpCookie("CartID")
            myCookie.Expires = DateTime.Now.AddDays(-1D)
            Response.Cookies.Add(myCookie)
        End If

        lblCartTotal.Text = "Cart Total: $"
    End Sub

    Protected Sub DataPager1_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataPager1.PreRender
        Dim total_pages As Integer
        Dim current_page As Integer
        lvCart.DataBind()
        total_pages = DataPager1.TotalRowCount / DataPager1.PageSize
        current_page = DataPager1.StartRowIndex / DataPager1.PageSize + 1
        If DataPager1.TotalRowCount Mod DataPager1.PageSize <> 0 Then
            total_pages = total_pages + 1
        End If
        If CInt(lvCart.Items.Count) <> 0 Then
            Dim lbl As Label = lvCart.FindControl("lblPage")
            lbl.Text = "Page " + CStr(current_page) + " of " + CStr(total_pages) + " (Total items: " + CStr(DataPager1.TotalRowCount) + ")"
        End If
        If CInt(lvCart.Items.Count) = 0 Then
            DataPager1.Visible = False
            show_next.Visible = False
            show_prev.Visible = False
        End If
    End Sub

    Protected Sub show_prev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles show_prev.Click
        Dim pagesize As Integer = DataPager1.PageSize
        Dim current_page As Integer = DataPager1.StartRowIndex / DataPager1.PageSize + 1
        Dim total_pages As Integer = DataPager1.TotalRowCount / DataPager1.PageSize
        Dim last As Integer = total_pages \ 3
        last = last * 3
        Do While current_page < last
            last = last - 3
        Loop
        If last < 3 Then
            last = 0
        Else
            last = last - 3
        End If
        DataPager1.SetPageProperties(last * pagesize, pagesize, True)
    End Sub

    Protected Sub show_next_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles show_next.Click
        Dim last As Integer = 3
        Dim pagesize As Integer = DataPager1.PageSize
        Dim current_page As Integer = DataPager1.StartRowIndex / DataPager1.PageSize + 1
        Dim total_pages As Integer = DataPager1.TotalRowCount / DataPager1.PageSize
        Do While current_page > last
            last = last + 3
        Loop
        If last > total_pages Then
            last = total_pages
        End If
        DataPager1.SetPageProperties(last * pagesize, pagesize, True)
    End Sub

    Protected Sub btnCheckout_Click(sender As Object, e As EventArgs) Handles btnCheckout.Click
        If HttpContext.Current.Request.Cookies("CartID") Is Nothing Then
        Else
            Response.Redirect("Checkout.aspx")
        End If

    End Sub
End Class
