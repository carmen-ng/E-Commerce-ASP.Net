<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ViewCart.aspx.vb" Inherits="ViewCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <section>
        <div class="breadcrumbs" style="margin-left: 100px">
            <ol class="breadcrumb">
                <li><a href="Default.aspx">Home</a></li>
                <li class="active">Shopping Cart</li>
            </ol>
        </div>

        <div class="table-responsive cart_info">
            <asp:SqlDataSource ID="SqlDSCart2" runat="server" 
                DataSourceMode="DataSet"
                ConnectionString="<%$ ConnectionStrings:StoreDatabaseConnectionString %>">       
            </asp:SqlDataSource>
            <asp:ListView ID="lvCart" runat="server" DataSourceID="SqlDSCart2"
                    OnItemCommand="lvCart_OnItemCommand" CellPadding="3" DataKeyField="CartID,ProductID"
                        CellSpacing="0" RepeatColumns="1" DataKeyNames="CartID">
                    <LayoutTemplate>              
                        <div style="float: right; margin: 0 30px 5px 0;">
                            <asp:Label ID="lblPage" runat="server" Text="" Font-Size="14px"></asp:Label>
                        </div><br /><br />
				        <table class="table table-condensed" style="margin-left: 10%; width:80%;">
					        <thead>
						        <tr class="cart_menu">
							        <td class="image">Item</td>
							        <td class="description"></td>
							        <td class="price">Price</td>
							        <td class="quantity">Quantity</td>
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
								<a href="ProductDetail.aspx?ProductID=<%#Container.DataItem("ProductID")%>"><img src="images/<%#Trim(Eval("ProductID"))%>.png" width="120" alt=""></a>
							</td>
							<td class="cart_description">
								<h4><a href="ProductDetail.aspx?ProductID=<%#Container.DataItem("ProductID")%>"><%# Eval("ProductName")%></a></h4>
								<p><%# Eval("ProductID")%></p>
							</td>
							<td class="cart_price">
								<p>$<%# Eval("ProductPrice")%> x <%# Eval("Quantity")%></p>
							</td>
							<td class="cart_quantity">
								<div class="cart_quantity_button">
                                    <asp:TextBox ID="tbQuantity" Text='<%# Eval("Quantity")%>' Width="50px" Height="40px" CssClass="cart_quantity_input" runat="server"></asp:TextBox>
                                    <asp:LinkButton runat="server" ID="lbUpdate" Text='Update'
                                        CommandName="cmdUpdate" CommandArgument='<%# Eval("ProductID")%>' />
								</div>
							</td>
							<td class="cart_total">
								<p class="cart_total_price">$<%# Eval("ProductPrice") * Eval("Quantity")%></p>
							</td>
							<td class="cart_delete">
								<asp:LinkButton runat="server" ID="lbDelete" Text='Remove from Cart'
                                        CommandName="cmdDelete" CommandArgument='<%# Eval("ProductID")%>'><i class="fa fa-times"></i></asp:LinkButton>
							</td>
						</tr>
                    </ItemTemplate>
                </asp:ListView>

                <div style="padding: 8px;width: 100%;text-align: center;">
                    <div style="display: inline-block; margin-top: 5px">
                        <div style="display: inline-block"><asp:Button runat="server" Text="&laquo;" id="show_prev" CssClass="show_prevx"></asp:Button></div>
                            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvCart" PageSize="5">
                                    <Fields>
                                        <asp:NumericPagerField NextPageText='&raquo;' PreviousPageText='&laquo;' ButtonCount="5" ButtonType="Button"
                                        NextPreviousButtonCssClass="next_prevx" NumericButtonCssClass="numericx" CurrentPageLabelCssClass="currentx"
                                        RenderNonBreakingSpacesBetweenControls="False" />
                                    </Fields>
                            </asp:DataPager>
                        <div style="display: inline-block"><asp:Button runat="server" Text="&raquo;" id="show_next" CssClass="show_nextx"></asp:Button></div>
                    </div>
                </div> 

                <div style="text-align: right; padding-right: 50px">
                    <asp:Label ID="lblCartTotal" runat="server" Text=""></asp:Label><br />
                    <asp:LinkButton runat="server" ID="lbEmpty" Text='Empty Cart' CommandName="cmdEmpty" OnClick="lbEmpty_Click"></asp:LinkButton><br />
                    <asp:Button ID="btnCheckout" runat="server" Text="Checkout" style="margin-left: 85%;"/>
                </div>

                <!-- gridview -->
                <asp:SqlDataSource ID="SqlDSCart" runat="server" 
                    DataSourceMode="DataSet" 
                    ConnectionString="<%$ ConnectionStrings:StoreDatabaseConnectionString %>"                     
                    SelectCommand="SELECT [CartID], [ProductID], [ProductName], [Quantity], [ProductPrice] FROM [CartLine]"
                    DeleteCommand="DELETE FROM [CartLine] WHERE ([CartID] = @CartID AND [ProductID] = @ProductID)"
                    UpdateCommand="UPDATE [CartLine] SET [Quantity] = @Quantity WHERE ([CartID] = @CartID AND [ProductID] = @ProductID)">
                </asp:SqlDataSource>
                <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="False" 
                    DataSourceID="SqlDSCart" AllowPaging="True" PageSize="3" DataKeyNames="CartID,ProductID"
                    AutoGenerateDeleteButton="True" AutoGenerateEditButton="True" 
                    EmptyDataText="There are no product in your cart.">
                    <Columns>
                        <asp:BoundField DataField="CartID" HeaderText="CartID" InsertVisible="False" ReadOnly="true"
                            SortExpression="CartID" />
                        <asp:BoundField DataField="ProductID" HeaderText="ProductID" InsertVisible="False" ReadOnly="true"
                            SortExpression="ProductID" />
                        <asp:BoundField DataField="ProductName" HeaderText="Product Name" InsertVisible="False" ReadOnly="true"
                            SortExpression="ProductName" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" 
                            SortExpression="Quantity" />
                        <asp:BoundField DataField="ProductPrice" HeaderText="Price" SortExpression="ProductPrice" InsertVisible="False" ReadOnly="true" />
                    </Columns>
                </asp:GridView>

			</div>
	</section> <!--/#cart_items-->


</asp:Content>

