<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Receipt.aspx.vb" Inherits="Receipt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style>
        #period{
            color: orangered;
            font-weight: bolder;
            font-size: 40px;
        }
        .lblCompany{
            font-weight: 600;
            font-size: xx-large;
        }
        .labels{
            font-weight: 600;
        }
        #title{
            font-size: x-large;
            font-weight: 600;
        }
        #content{
            margin-left:100px;
        }
        #companyTitle{
            text-align: center;
        }

    </style>
    <section>
        <div class="breadcrumbs" style="margin-left: 100px">
            <ol class="breadcrumb">
                <li><a href="Default.aspx">Home</a></li>
                <li>Checkout</li>
                <li class="active">Receipt</li>
            </ol>
        </div>
        <br />
        <div id="content">
            <div id="companyTitle"><label class="lblCompany">Chair</label><label id="period">.</label> <label class="lblCompany">Technologies</label></div>
            <br /><br />

            <label id="title">Your Order Information</label><br /><br />

            <label class="labels">Name: </label>  <asp:Label ID="lblName" runat="server" Text=""></asp:Label><br />
            <label class="labels">Street Address: </label>  <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label><br />
            <label class="labels">City: </label>  <asp:Label ID="lblCity" runat="server" Text=""></asp:Label><br />
            <label class="labels">State: </label>  <asp:Label ID="lblState" runat="server" Text=""></asp:Label><br />
            <label class="labels">ZIP: </label>  <asp:Label ID="lblZip" runat="server" Text=""></asp:Label><br />
            <label class="labels">Email Address: </label>  <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label><br />
            <label class="labels">Phone Number: </label>  <asp:Label ID="lblPhone" runat="server" Text=""></asp:Label><br />
            <label class="labels">Credit Card Number: </label>  <asp:Label ID="lblCardNum" runat="server" Text=""></asp:Label><br />
            <label class="labels">Credit Card Type: </label>  <asp:Label ID="lblCardType" runat="server" Text=""></asp:Label><br />
            <label class="labels">Credit Card Expiration Date: </label>  <asp:Label ID="lblCardExp" runat="server" Text=""></asp:Label><br />


            <asp:SqlDataSource ID="SqlDSCart2" runat="server" 
                    DataSourceMode="DataSet"
                    ConnectionString="<%$ ConnectionStrings:StoreDatabaseConnectionString %>">       
                </asp:SqlDataSource>
                <asp:ListView ID="lvCart" runat="server" DataSourceID="SqlDSCart2" CellPadding="3" DataKeyField="CartID,ProductID"
                            CellSpacing="0" RepeatColumns="1" DataKeyNames="CartID">
                        <LayoutTemplate>              
                            <div style="float: right; margin: 0 30px 5px 0;">
                                <asp:Label ID="lblPage" runat="server" Text="" Font-Size="14px"></asp:Label>
                            </div><br /><br />
				            <table class="table table-condensed" style="margin-left: 10%; width:80%;">
					            <thead>
						            <tr class="cart_menu">
							            <td class="image">Items</td>
							            <td class="description"></td>
							            <td class="price">Price</td>
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
								    <p>$<%# Eval("ProductPrice")%> x <%# Eval("Quantity")%></p>
							    </td>
							    <td class="cart_total">
								    <p class="cart_total_price">$<%# Eval("ProductPrice") * Eval("Quantity")%></p>
							    </td>
						    </tr>
                        </ItemTemplate>
                    </asp:ListView><br />



            <label class="labels">Order Subtotal: </label>  $<asp:Label ID="lblSubtotal" runat="server" Text=""></asp:Label><br />
            <label class="labels">Tax: </label>  $<asp:Label ID="lblTax" runat="server" Text=""></asp:Label><br />
            <label class="labels">Order Total: </label>  $<asp:Label ID="lblTotal" runat="server" Text=""></asp:Label><br /><br /><br />

            <asp:Label ID="lblEnd" runat="server" Text=""></asp:Label>
        </div>
            
    </section>
</asp:Content>

