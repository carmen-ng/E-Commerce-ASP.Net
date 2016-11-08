<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ProductList.aspx.vb" Inherits="ProductList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        input[type="text"]{
            width: 50px;
            height: 40px;
            margin-bottom: 0px;
        }

        #quantity{
            font-weight: unset;
            padding-right: 15px;
        }
    </style>
    <section>
        <div class="breadcrumbs" style="margin-left: 100px">
            <ol class="breadcrumb">
                <li><a href="Default.aspx">Home</a></li>
                <li class="active"><a href="ProductList.aspx">Shop</a></li>
            </ol>
        </div>

        <div class="action-bar">
            <div class="container">
                <div class="row">
                    <div class="panel-group category-products" id="accordian">
                        <!--category-productsr-->
                        <asp:SqlDataSource ID="sqlDSCategory"
                            ConnectionString="<%$ ConnectionStrings:StoreDatabaseConnectionString %>"
                            SelectCommand=""
                            runat="server"></asp:SqlDataSource>
                        <asp:Repeater ID="rpCategory" runat="server" DataSourceID="sqlDSCategory" Visible="True">
                            <ItemTemplate>
                                <div class="col-sm-4">
                                    <div class="panel-heading">
                                        <h4 class="panel-title"><a href="SubCategory.aspx?CategoryID=<%#Container.DataItem("Id")%>&CategoryName=<%#Container.DataItem("CategoryName")%>"><%#Container.DataItem("CategoryName")%></a></h4>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <!--/category-productsr-->

                </div>
            </div>
        </div>

        <section class="product-list">
            <div class="container">
                <asp:Panel ID="pnlProductList" runat="server" Visible="true">
                    <div class="row">
                        <asp:SqlDataSource ID="sqlDSProductList"
                            ConnectionString="<%$ ConnectionStrings:StoreDatabaseConnectionString %>"
                            SelectCommand=""
                            runat="server"></asp:SqlDataSource>
                        <asp:Repeater ID="rpProductList" runat="server" DataSourceID="sqlDSProductList" Visible="True">
                            <ItemTemplate>
                                <div class="col-sm-4">
                                    <div class="product-image-wrapper">
                                        <div class="single-products">
                                            <div class="productinfo text-center">
                                                <a href="ProductDetail.aspx?ProductID=<%#Container.DataItem("ProductID")%>">
                                                    <img src="images/<%#Trim(Container.DataItem("ProductID"))%>.png" alt="" />
                                                </a>
                                                <h2>$<%#Container.DataItem("ProductPrice")%></h2>
                                                <p>
                                                    <a href="ProductDetail.aspx?ProductID=<%#Container.DataItem("ProductID")%>">
                                                        <%#Container.DataItem("ProductName")%>
                                                    </a>
                                                </p>
                                                <label id="quantity">Quantity:</label><asp:TextBox ID="tbQuantity" runat="server" Text="1"></asp:TextBox>
                                                <asp:Button ID="addCart" class="btn btn-default add-to-cart" runat="server" Text="Add to Cart" />
                                            </div>
                                        </div>
                                        <div class="choose">
                                            <ul class="nav nav-pills nav-justified">
                                                <li><a href=""><i class="fa fa-plus-square"></i>Add to wishlist</a></li>
                                                <li><a href=""><i class="fa fa-plus-square"></i>Add to compare</a></li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <!-- /.row -->
                </asp:Panel>
            </div>
            <!-- /.container -->
        </section>

    </section>
</asp:Content>

