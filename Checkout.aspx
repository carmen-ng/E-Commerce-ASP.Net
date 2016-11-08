<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Checkout.aspx.vb" Inherits="Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style>
        label{
            width: 200px;
        }

        .title{
            font-size: x-large;
            margin-bottom: 50px;
            width: 300px;
        }
    </style>
    <section>
        <div class="breadcrumbs" style="margin-left: 100px">
            <ol class="breadcrumb">
                <li><a href="Default.aspx">Home</a></li>
                <li class="active">Checkout</li>
            </ol>
        </div>
        <br /><br />


    <div id="form" style="margin-left:100px;">
            <label class="title">Name</label><br />
            <label>First Name: </label><asp:TextBox ID="tbFName" Width="300px" runat="server"></asp:TextBox><br />
            <label>Last Name: </label><asp:TextBox ID="tbLName" Width="300px" runat="server"></asp:TextBox><br /><br />

            <label class="title">Shipping Address</label><br />
            <label>Street Address: </label><asp:TextBox ID="tbStreetAddress" Width="300px" runat="server"></asp:TextBox><br />
            <label>City: </label><asp:TextBox ID="tbCity" Width="300px" runat="server"></asp:TextBox><br />
            <label>State: </label>
            <asp:DropDownList ID="ddState" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddState_SelectedIndexChanged">
                <asp:ListItem Text="AL" Value="AL"></asp:ListItem><asp:ListItem Text="AK" Value="AK"></asp:ListItem><asp:ListItem Text="AZ" Value="AZ"></asp:ListItem>
                <asp:ListItem Text="AR" Value="AR"></asp:ListItem><asp:ListItem Text="CA" Value="CA"></asp:ListItem><asp:ListItem Text="CO" Value="CO"></asp:ListItem>
                <asp:ListItem Text="CT" Value="CT"></asp:ListItem><asp:ListItem Text="DE" Value="DE"></asp:ListItem><asp:ListItem Text="FL" Value="FL"></asp:ListItem>
                <asp:ListItem Text="GA" Value="GA"></asp:ListItem><asp:ListItem Text="HI" Value="HI"></asp:ListItem><asp:ListItem Text="ID" Value="ID"></asp:ListItem>
                <asp:ListItem Text="IL" Value="IL"></asp:ListItem><asp:ListItem Text="IN" Value="IN"></asp:ListItem><asp:ListItem Text="IA" Value="IA"></asp:ListItem>
                <asp:ListItem Text="KS" Value="KS"></asp:ListItem><asp:ListItem Text="KY" Value="KY"></asp:ListItem><asp:ListItem Text="LA" Value="LA"></asp:ListItem>
                <asp:ListItem Text="ME" Value="ME"></asp:ListItem><asp:ListItem Text="MD" Value="MD"></asp:ListItem><asp:ListItem Text="MA" Value="MA"></asp:ListItem>
                <asp:ListItem Text="MI" Value="MI"></asp:ListItem><asp:ListItem Text="MN" Value="MN"></asp:ListItem><asp:ListItem Text="MS" Value="MS"></asp:ListItem>
                <asp:ListItem Text="MO" Value="MO"></asp:ListItem><asp:ListItem Text="MT" Value="MT"></asp:ListItem><asp:ListItem Text="NE" Value="NE"></asp:ListItem>
                <asp:ListItem Text="NV" Value="NV"></asp:ListItem><asp:ListItem Text="NH" Value="NH"></asp:ListItem><asp:ListItem Text="NJ" Value="NJ"></asp:ListItem>
                <asp:ListItem Text="NM" Value="NM"></asp:ListItem><asp:ListItem Text="NY" Value="NY"></asp:ListItem><asp:ListItem Text="NC" Value="NC"></asp:ListItem>
                <asp:ListItem Text="ND" Value="ND"></asp:ListItem><asp:ListItem Text="OH" Value="OH"></asp:ListItem><asp:ListItem Text="OK" Value="OK"></asp:ListItem>
                <asp:ListItem Text="OR" Value="OR"></asp:ListItem><asp:ListItem Text="PA" Value="PA"></asp:ListItem><asp:ListItem Text="RI" Value="RI"></asp:ListItem>
                <asp:ListItem Text="SC" Value="SC"></asp:ListItem><asp:ListItem Text="SD" Value="SD"></asp:ListItem><asp:ListItem Text="TN" Value="TN"></asp:ListItem>
                <asp:ListItem Text="TX" Value="TX"></asp:ListItem><asp:ListItem Text="UT" Value="UT"></asp:ListItem><asp:ListItem Text="VT" Value="VT"></asp:ListItem>
                <asp:ListItem Text="VA" Value="VA"></asp:ListItem><asp:ListItem Text="WA" Value="WA"></asp:ListItem><asp:ListItem Text="WV" Value="WV"></asp:ListItem>
                <asp:ListItem Text="WI" Value="WI"></asp:ListItem><asp:ListItem Text="WY" Value="WY"></asp:ListItem>
            </asp:DropDownList><br /><br />
            <label>ZIP: </label><asp:TextBox ID="tbZip" Width="300px" runat="server"></asp:TextBox><br/><br />

            <label class="title">Phone Number</label><br />
            <label>E-mail Address: </label><asp:TextBox ID="tbEmail" Width="300px" runat="server"></asp:TextBox><br />
            <label>Phone Number: </label><asp:TextBox ID="tbPhone" Width="300px" runat="server"></asp:TextBox><br /><br />

            <label class="title">Payment Method</label><br />
            <label>Credit Card Number: </label><asp:TextBox ID="tbCardNum" Width="300px" runat="server"></asp:TextBox><br />
            <label>Credit Card Type:</label>
            <asp:DropDownList ID="ddCardType" Width="300px"  runat="server">
                 <asp:ListItem Text="VISA" Value="visa"></asp:ListItem>
                 <asp:ListItem Text="MasterCard" Value="master"></asp:ListItem>
                 <asp:ListItem Text="Discover" Value="discover"></asp:ListItem>
                 <asp:ListItem Text="American Express" Value="amex"></asp:ListItem>
            </asp:DropDownList><br /><br />
            <label>Expiration Date: </label>
            <asp:DropDownList ID="ddExpMonth" runat="server">
                 <asp:ListItem Text="01" Value="1"></asp:ListItem>
                 <asp:ListItem Text="02" Value="2"></asp:ListItem>
                 <asp:ListItem Text="03" Value="3"></asp:ListItem>
                 <asp:ListItem Text="04" Value="4"></asp:ListItem>
                 <asp:ListItem Text="05" Value="5"></asp:ListItem>
                 <asp:ListItem Text="06" Value="6"></asp:ListItem>
                 <asp:ListItem Text="07" Value="7"></asp:ListItem>
                 <asp:ListItem Text="08" Value="8"></asp:ListItem>
                 <asp:ListItem Text="09" Value="9"></asp:ListItem>
                 <asp:ListItem Text="10" Value="10"></asp:ListItem>
                 <asp:ListItem Text="11" Value="11"></asp:ListItem>
                 <asp:ListItem Text="12" Value="12"></asp:ListItem>
            </asp:DropDownList>/<asp:DropDownList ID="ddExpYear" runat="server">
                 <asp:ListItem Text="2016" Value="2016"></asp:ListItem>
                 <asp:ListItem Text="2017" Value="2017"></asp:ListItem>
                 <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                 <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                 <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                 <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                 <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                 <asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                 <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
                 <asp:ListItem Text="2025" Value="2025"></asp:ListItem>
                 <asp:ListItem Text="2026" Value="2026"></asp:ListItem>
                 <asp:ListItem Text="2027" Value="2027"></asp:ListItem>
            </asp:DropDownList><br /><br /><br />


        <label class="title">Order Summary</label><br />

        <asp:SqlDataSource ID="SqlDSCart2" runat="server" 
                    DataSourceMode="DataSet"
                    ConnectionString="<%$ ConnectionStrings:StoreDatabaseConnectionString %>">       
                </asp:SqlDataSource>
                <asp:ListView ID="lvCart" runat="server" DataSourceID="SqlDSCart2" CellPadding="3" DataKeyField="CartID,ProductID"
                            CellSpacing="0" RepeatColumns="1" DataKeyNames="CartID">
                        <LayoutTemplate>           
				            <table class="table table-condensed" style="margin-right:100px;">
					            <thead>
						            <tr class="cart_menu">
							            <td class="image">Items</td>
							            <td class="description"></td>
							            <td class="price">Price x Quantity</td>
							            <td class="total">Total</td>
							            <td></td>
						            </tr>
					            </thead>
                                <asp:PlaceHolder runat="server" ID="groupPlaceHolder" />
                            </table>                     
                        </LayoutTemplate>
                        <GroupTemplate>
                            <asp:PlaceHolder runat="server" id="itemPlaceholder"></asp:PlaceHolder>
                        </GroupTemplate>
                    <ItemTemplate>
 						    <tr>
							    <td class="cart_product">
								    <a href="ProductDetail.aspx?ProductID=<%#Container.DataItem("ProductID")%>"><img src="images/<%#Trim(Eval("ProductID"))%>.png" width="80" alt=""></a>
							    </td>
							    <td class="cart_description">
								    <h4><a href="ProductDetail.aspx?ProductID=<%#Container.DataItem("ProductID")%>"><%# Eval("ProductName")%></a></h4>
								    <p><%# Eval("ProductID")%></p>
							    </td>
							    <td class="cart_price">
								    <p>$<%# Eval("ProductPrice")%>x <%# Eval("Quantity")%></p>
							    </td>
							    <td class="cart_total">
								    <p class="cart_total_price">$<%# Eval("ProductPrice") * Eval("Quantity")%></p>
							    </td>
						    </tr>
                        </ItemTemplate>
                    </asp:ListView><br />


        <label>Items: </label>$<asp:Label ID="lblCartTotal" runat="server" Text=""></asp:Label><br />
        <label>Shipping: </label>free<br />
        <label>Order Subtotal: </label>$<asp:Label ID="lblCartTotal2" runat="server" Text=""></asp:Label><br />
        <label>Estimated Tax: </label>$<asp:Label ID="lblTax" runat="server" Text="0"></asp:Label><br /><br />
        <label>Order Total: </label>$<asp:Label ID="lblOrderTotal" runat="server" Text=""></asp:Label><br />
        <br /><br /><br />

        <asp:Button ID="btnPlaceOrder" runat="server" Text="Place Order"/>
    </div>
        
    </section>

</asp:Content>

