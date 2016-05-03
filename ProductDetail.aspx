<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductDetail.aspx.cs" Inherits="ProductDetail" MasterPageFile="~/MasterPage.master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <table class="CategoriesTable" cellspacing="3">
        <tr>
            <td class="CategoriesTableTitle">Kategorie</td>
        </tr>
        <tr>
            <td>
                <asp:Menu ID="menuCategoriesTree" runat="server"></asp:Menu>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <%--    <asp:FormView ID="FormView1" runat="server" OnItemCommand="FormView1_ItemCommand" DataKeyNames="id,title,price">
        <ItemTemplate>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="3" align="center"><b><%# DataBinder.Eval(Container.DataItem, "title") %></b></td>
                </tr>
                <tr>
                    <td rowspan="3">
                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "large_image_url") %>' />
                    </td>
                    <td>kód:</td>
                    <td><%# DataBinder.Eval(Container.DataItem, "id") %></td>
                </tr>
                <tr>
                    <td>cena:</td>
                    <td><%# DataBinder.Eval(Container.DataItem, "price") %>&#160;Kè</td>
                </tr>
                <tr>
                    <td>výrobce:</td>
                    <td><%# DataBinder.Eval(Container.DataItem, "producer") %></td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align:justify"><%# DataBinder.Eval(Container.DataItem, "description") %></td>
                </tr>
                <tr>
                    <td colspan="3" align="center" style="background-color: #eee8aa; border: solid 1px black">
                        Poèet:&nbsp;<asp:TextBox ID="txtQuantity" runat="server" Columns="1">1</asp:TextBox>&nbsp;&nbsp;
                        <asp:RequiredFieldValidator ID="RangeValidator1" runat="server" ErrorMessage="Vyplòte množství" EnableClientScript="false" ControlToValidate="txtQuantity"></asp:RequiredFieldValidator>
                        <asp:RangeValidator runat="server" ControlToValidate="txtQuantity" ErrorMessage="Pouze celé èíslo od 1 do 100" Type="Integer" MinimumValue="1" MaximumValue="100" EnableClientScript="false"></asp:RangeValidator>
                        &nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" runat="server">Pøidat do košíku</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:FormView>--%>

    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="3" align="center"><b>
                <asp:Literal ID="litTitle" runat="server" /></b></td>
        </tr>
        <tr>
            <td style="width: 400px; height: 400px; text-align: center; vertical-align: central">
                <asp:Image AlternateText="Náhled není k dispozici." ID="imgProduct" runat="server" Height="400px" Width="400px" />
            </td>
            <td style="vertical-align:top">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width:80px">cena:</td>
                        <td>
                            <asp:Literal ID="litPrice" runat="server" />&#160;Kè
                        </td>
                    </tr>
                    <tr>
                        <td>výrobce:</td>
                        <td>
                            <asp:Literal ID="litProducer" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>kód:</td>
                        <td>
                            <asp:Literal ID="litId" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: justify">
                <asp:Literal ID="litDescription" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" style="background-color: #eee8aa; border: solid 1px black">Poèet:&nbsp;<asp:TextBox ID="txtQuantity" runat="server" Columns="1">1</asp:TextBox>&nbsp;&nbsp;
                        <asp:RequiredFieldValidator ID="RangeValidator1" runat="server" ErrorMessage="Vyplòte množství" EnableClientScript="false" ControlToValidate="txtQuantity" />
                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Pouze celé èíslo od 1 do 100" Type="Integer" MinimumValue="1" MaximumValue="100" EnableClientScript="false" />
                &nbsp;&nbsp;<asp:LinkButton ID="btnAddToCart" OnClick="btnAddToCart_Click" runat="server">Pøidat do košíku</asp:LinkButton>
            </td>
        </tr>
    </table>
</asp:Content>
