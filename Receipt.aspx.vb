Imports System.Data
Imports System.Data.SqlClient

Partial Class Receipt
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If (Not Request.Cookies("CartID") Is Nothing) Then
            Dim strCartID As String
            Dim CookieBack As HttpCookie
            CookieBack = HttpContext.Current.Request.Cookies("CartID")
            strCartID = CookieBack.Value
            Dim strSQL As String = "Select * From Cartline where cartid = '" & strCartID & "'"
            SqlDSCart2.SelectCommand = strSQL


            If Request.QueryString("OrderConfirmation") <> "" Then
                Dim strConn As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\StoreDatabase.mdf;Integrated Security=True;Connect Timeout=30"
                Dim connReceipt As SqlConnection
                Dim cmdReceipt As SqlCommand
                Dim drReceipt As SqlDataReader
                Dim stringSQL As String = "Select * from OrderHead Where OrderID='" & Request.QueryString("OrderConfirmation") & "'"
                connReceipt = New SqlConnection(strConn)
                cmdReceipt = New SqlCommand(stringSQL, connReceipt)
                connReceipt.Open()
                drReceipt = cmdReceipt.ExecuteReader(CommandBehavior.CloseConnection)
                If drReceipt.Read() Then
                    lblName.Text = drReceipt.Item("FirstName") + " " + drReceipt.Item("LastName")
                    lblAddress.Text = drReceipt.Item("StreetAddress")
                    lblCity.Text = drReceipt.Item("City")
                    lblState.Text = drReceipt.Item("State")
                    lblZip.Text = drReceipt.Item("ZIP")
                    lblEmail.Text = drReceipt.Item("Email")
                    lblPhone.Text = drReceipt.Item("PhoneNumber")
                    Dim cardNum As String = drReceipt.Item("CreditCardNumber")
                    lblCardNum.Text = "************" + cardNum.Substring(cardNum.Length - 4)
                    lblCardType.Text = drReceipt.Item("CreditCardType")
                    lblCardExp.Text = drReceipt.Item("CreditCardExpMonth") + "/" + drReceipt.Item("CreditCardExpYear").ToString()
                    lblSubtotal.Text = drReceipt.Item("OrderSubtotal")
                    lblTax.Text = drReceipt.Item("OrderTax")
                    lblTotal.Text = drReceipt.Item("OrderTotal")
                    lblEnd.Text = "*An e-mail confirmation with your order details has been sent."
                End If
                connReceipt.Close()


                Dim myCookie As HttpCookie
                myCookie = New HttpCookie("CartID")
                myCookie.Expires = DateTime.Now.AddDays(-1D)
                Response.Cookies.Add(myCookie)
            End If
        End If
    End Sub
End Class
