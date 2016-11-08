Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Mail

Partial Class Checkout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If (Not Request.Cookies("CartID") Is Nothing) Then
            Dim strCartID As String
            Dim CookieBack As HttpCookie
            CookieBack = HttpContext.Current.Request.Cookies("CartID")
            strCartID = CookieBack.Value
            Dim strSQL As String = "Select * From Cartline where cartid = '" & strCartID & "'"
            SqlDSCart2.SelectCommand = strSQL

            Dim strConn As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\StoreDatabase.mdf;Integrated Security=True;Connect Timeout=10"
            Dim connCart As SqlConnection
            Dim cmdCart As SqlCommand
            connCart = New SqlConnection(strConn)
            Dim strSQL2 As String = "Select ROUND(SUM(ProductPrice * Quantity), 2) AS Total FROM CartLine WHERE CartID = '" & strCartID & "'"
            cmdCart = New SqlCommand(strSQL2, connCart)
            connCart.Open()
            Dim totalPrice = cmdCart.ExecuteScalar()

            lblCartTotal.Text = totalPrice.ToString()
            lblCartTotal2.Text = totalPrice.ToString()
            connCart.Close()

            Dim tax As Double = Convert.ToDouble(lblTax.Text)

            Dim subtotal As Double = Convert.ToDouble(totalPrice)
            Dim total As Double = tax + subtotal
            lblOrderTotal.Text = Math.Round(total, 2).ToString()
        End If
    End Sub

    Protected Sub ddState_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddState.SelectedIndexChanged
        If ddState.SelectedItem.Text = "CA" Then
            Dim subtotal As Double = Convert.ToDouble(lblCartTotal.Text)
            Dim tax As Double = subtotal * 0.0875
            Dim total As Double = tax + subtotal

            lblTax.Text = Math.Round(tax, 2).ToString()
            lblOrderTotal.Text = Math.Round(total, 2).ToString()
        Else
            Dim subtotal As Double = Convert.ToDouble(lblCartTotal.Text)

            lblTax.Text = 0
            lblOrderTotal.Text = Math.Round(subtotal, 2).ToString()
        End If
    End Sub

    Protected Sub btnPlaceOrder_Click(sender As Object, e As EventArgs) Handles btnPlaceOrder.Click
        If (Not Request.Cookies("CartID") Is Nothing) Then
            Dim drOrderHead As SqlDataReader
            Dim strSQLStatement As String
            Dim cmdSQL As SqlCommand
            Dim strConnectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("StoreDatabaseConnectionString").ConnectionString
            Dim conn As New SqlConnection(strConnectionString)

            Dim currentDate As DateTime = DateTime.Now
            Dim currentMonth = currentDate.Month
            Dim currentYear = currentDate.Year
            Dim msg As String = "One or more of your fields are invalid. Please try again."

            If tbFName.Text.Trim <> "" And tbLName.Text.Trim <> "" And tbStreetAddress.Text.Trim <> "" And tbCity.Text.Trim <> "" And
                tbZip.Text.Trim <> "" And IsNumeric(tbZip.Text) And tbEmail.Text.Trim <> "" And tbPhone.Text.Trim <> "" And IsNumeric(tbPhone.Text) And tbCardNum.Text.Trim.Length = 16 Then

                If CInt(ddExpYear.SelectedItem.Value) = CInt(currentYear) And CInt(ddExpMonth.SelectedItem.Text) <= CInt(currentMonth) Then
                    MsgBox(msg,, "Error")
                Else
                    Dim subtotal As String = lblCartTotal2.Text
                    Dim tax As String = lblTax.Text
                    Dim total As String = lblOrderTotal.Text

                    'GET CARTID COOKIE
                    Dim strCartID As String
                    Dim CookieBack As HttpCookie
                    CookieBack = HttpContext.Current.Request.Cookies("CartID")
                    strCartID = CookieBack.Value

                    'GET CART RESULTS
                    Dim strSQL As String = "Select ProductName, ProductPrice, Quantity From Cartline where cartID = '" & strCartID & "'"
                    cmdSQL = New SqlCommand(strSQL, conn)
                    conn.Open()
                    drOrderHead = cmdSQL.ExecuteReader()
                    Dim orderList As New StringBuilder
                    While drOrderHead.Read()
                        orderList.AppendLine(String.Format("{0}       {1}        {2}", drOrderHead(0), drOrderHead(1), drOrderHead(2)))
                    End While
                    conn.Close()

                    'SEND EMAIL
                    Try
                        Dim smtpClient As SmtpClient = New SmtpClient
                        Dim MyMail As MailMessage = New MailMessage()
                        smtpClient.Host = ConfigurationManager.AppSettings("Host")
                        smtpClient.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings("EnableSsl"))
                        Dim toAddress As MailAddress = New MailAddress(tbEmail.Text)
                        MyMail.From = New MailAddress(ConfigurationManager.AppSettings("UserName"), "Chair Tech")
                        MyMail.To.Add(toAddress)
                        MyMail.Subject = "Chair Tech Order Receipt #" + strCartID

                        Dim mailBody As New StringBuilder
                        Dim cardNum As String = tbCardNum.Text
                        mailBody.AppendLine("Thank you for shopping with us at Chair Technologies. Below is your order summary.")
                        mailBody.AppendLine("")
                        mailBody.AppendLine("Date: " + currentDate.ToString())
                        mailBody.AppendLine("")
                        mailBody.AppendLine("CUSTOMER INFORMATION")
                        mailBody.AppendLine("Name: " + tbFName.Text + " " + tbLName.Text)
                        mailBody.AppendLine("Street Address: " + tbStreetAddress.Text)
                        mailBody.AppendLine("City: " + tbCity.Text)
                        mailBody.AppendLine("State: " + ddState.SelectedItem.Text)
                        mailBody.AppendLine("ZIP: " + tbZip.Text)
                        mailBody.AppendLine("Email Address: " + tbEmail.Text)
                        mailBody.AppendLine("Phone Number: " + tbPhone.Text)
                        mailBody.AppendLine("")
                        mailBody.AppendLine("PAYMENT INFORMATION")
                        mailBody.AppendLine("Credit Card Number: ************" + cardNum.Substring(cardNum.Length - 4))
                        mailBody.AppendLine("Credit Card Type: " + ddCardType.SelectedItem.Text)
                        mailBody.AppendLine("Credit Card Expiration Date: " + ddExpMonth.SelectedItem.Text + "/" + ddExpYear.SelectedItem.Text)
                        mailBody.AppendLine("")
                        mailBody.AppendLine("ORDER SUMMARY")
                        mailBody.AppendLine("Product Name                           Price       Quantity")
                        mailBody.AppendLine(orderList.ToString())
                        mailBody.AppendLine("")
                        mailBody.AppendLine("Order Subtotal:  $" + lblCartTotal2.Text)
                        mailBody.AppendLine("Tax: $" + lblTax.Text)
                        mailBody.AppendLine("Order Total: $" + lblOrderTotal.Text)

                        MyMail.Body = mailBody.ToString()


                        Dim NetworkCred As System.Net.NetworkCredential = New System.Net.NetworkCredential
                        NetworkCred.UserName = ConfigurationManager.AppSettings("UserName")
                        NetworkCred.Password = ConfigurationManager.AppSettings("Password")
                        smtpClient.UseDefaultCredentials = True
                        smtpClient.Credentials = NetworkCred
                        smtpClient.Port = Integer.Parse(ConfigurationManager.AppSettings("Port"))
                        smtpClient.Send(MyMail)


                        'INSERT DATA INTO ORDERHEAD
                        strSQLStatement = "INSERT INTO OrderHead (OrderID, Date, FirstName, LastName, StreetAddress, City, State, ZIP, Email, PhoneNumber," _
                        & "CreditCardNumber, CreditCardType, CreditCardExpMonth, CreditCardExpYear, OrderSubtotal, OrderTax, OrderTotal)" _
                        & "values('" & strCartID & "','" & currentDate.ToString() & "','" & tbFName.Text & "','" & tbLName.Text & "','" & tbStreetAddress.Text & "','" & tbCity.Text & "','" _
                        & ddState.SelectedItem.Text & "','" & tbZip.Text & "','" & tbEmail.Text & "','" & tbPhone.Text & "','" & tbCardNum.Text & "','" _
                        & ddCardType.SelectedItem.Text & "','" & ddExpMonth.SelectedItem.Text & "','" & ddExpYear.SelectedItem.Text & "','" _
                        & subtotal & "','" & tax & "','" & total & "')"

                        cmdSQL = New SqlCommand(strSQLStatement, conn)
                        conn.Open()
                        drOrderHead = cmdSQL.ExecuteReader()
                        conn.Close()

                    Catch ex As Exception
                        MsgBox(ex.ToString())
                    Finally
                        'REDIRECT TO RECEIPT.ASPX
                        Response.Redirect("Receipt.aspx" + "?OrderConfirmation=" + strCartID)
                    End Try

                End If
            Else
                MsgBox(msg,, "Error")
            End If
        End If
    End Sub

    Public Function GetRandomOrderID(ByVal length As Integer) As String
        Dim chars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim rand As New Random
        Dim sb As New StringBuilder
        For i As Integer = 1 To 10
            Dim idx As Integer = rand.Next(0, 35)
            sb.Append(chars.Substring(idx, 1))
        Next
        Return sb.ToString()
    End Function
End Class
