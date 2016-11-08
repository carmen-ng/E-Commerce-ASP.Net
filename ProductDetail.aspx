<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ProductDetail.aspx.vb" Inherits="ProductDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        
        input[type="text"]{
            width: 70px;
            height: 40px;
        }

        #quantity{
            font-weight: unset;
            padding-right: 15px;
        }
    </style>

    <asp:SqlDataSource ID="sqlDSProductDetail"
        ConnectionString="<%$ ConnectionStrings:StoreDatabaseConnectionString %>"
        SelectCommand=""
        runat="server"></asp:SqlDataSource>
    <div class="breadcrumbs" style="margin-left: 100px">
        <ol class="breadcrumb">
                <li><a href="Default.aspx">Home</a></li>
                <li class="active"><a href="ProductList.aspx">Shop</a></li>
                <li class="active"><a href="#"><asp:Label ID="lblCategoryName" runat="server" Text=""></asp:Label></a></li>
                <li class="active"><a href="#"><asp:Label ID="lblSubCategoryName" runat="server" Text=""></asp:Label></a></li>
                <li class="active"><asp:Label ID="lblProduct" runat="server" Text=""></asp:Label></li>
        </ol>
    </div>

    <div class="action-bar">
        <div class="container">
            <div class="row">
                <div class="panel-group category-products" id="accordian">
                    <!--category-productsr-->
                    <asp:SqlDataSource ID="sqlDSSubCategory"
                        ConnectionString="<%$ ConnectionStrings:StoreDatabaseConnectionString %>"
                        SelectCommand=""
                        runat="server"></asp:SqlDataSource>
                    <asp:Repeater ID="rpSubCategory" runat="server" DataSourceID="sqlDSSubCategory" Visible="True">
                        <ItemTemplate>
                            <div class="col-sm-4">
                                <div class="panel-heading">
                                    <h4 class="panel-title"><a href="SubCategory.aspx?CategoryID=<% = Request.QueryString("CategoryID")%>&CategoryName=<% = Request.QueryString("CategoryName")%>&SubCategoryID=<%#Container.DataItem("Id")%>&SubCategoryName=<%#Container.DataItem("CategoryName")%>"><%#Container.DataItem("CategoryName")%></a></h4>
                                </div>

                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <!--/category-productsr-->

            </div>
        </div>
    </div>

    <section class="product-single">
        <div class="container">
            <div class="row">
                <div class="col-md-7">
                    <div class="product-main-image">
                        <asp:Image ID="imgProduct" runat="server" ImageUrl="" />
                    </div>
                </div>
                <div class="col-md-4 col-md-offset-1">
                    <div class="product-details">
                        <h2>
                            <asp:Label ID="lblProductName" runat="server" Text="Label"></asp:Label></h2>
                        <p>
                            <asp:Label ID="lblProductID" runat="server" Text="Label"></asp:Label>
                        </p>
                        <span>US$<asp:Label ID="lblProductPrice" runat="server" Text="Label"></asp:Label></span>
                        <p>
                            <asp:Label ID="lblProductDesc" runat="server" Text="Label"></asp:Label>
                        </p>
                        <div class="product-social">
                            <a href="#"><i class="fa fa-facebook"></i></a>
                            <a href="#"><i class="fa fa-twitter"></i></a>
                            <a href="#"><i class="fa fa-google-plus"></i></a>
                            <a href="#"><i class="fa fa-pinterest"></i></a>
                            <a href="#"><i class="fa fa-instagram"></i></a>
                        </div>
                        <label id="quantity">Quantity:</label><asp:TextBox ID="tbQuantity" runat="server" Text="1"></asp:TextBox>
                        <asp:Button ID="btnCart" runat="server" Text="Add to Cart" />
                        </div>
                </div>
            </div>
            <!-- /.row -->
        </div>
        <!-- /.container -->
    </section>
</asp:Content>

