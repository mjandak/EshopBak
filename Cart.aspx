<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cart.aspx.cs" Inherits="Cart" MasterPageFile="~/MasterPage.master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:Repeater ID="repCart" runat="server">
        <HeaderTemplate>
            <table class="GenericTable">
                <tr>
                    <td><b>k�d</b></td>
                    <td><b>n�zev</b></td>
                    <td><b>mno�stv�</b></td>
                    <td><b>cena</b></td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:HyperLink runat="server" ID="litProductId" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>' NavigateUrl='<%# "~/productdetail.aspx?id=" + DataBinder.Eval(Container.DataItem, "id") %>'></asp:HyperLink>
                </td>
                <td><%# DataBinder.Eval(Container.DataItem, "title") %></td>
                <td class="ShoppingCartTable">
                    <asp:TextBox runat="server" ID="txtProductQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "quantity") %>' Columns="7"></asp:TextBox>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Pouze cel� ��slo od 0 do 100" EnableClientScript="false" ControlToValidate="txtProductQuantity" Type="Integer" MaximumValue="100" MinimumValue="0"></asp:RangeValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Dopl�te mno�stv�" ControlToValidate="txtProductQuantity" EnableClientScript="false"></asp:RequiredFieldValidator>
                </td>
                <td style="text-align: right">
                    <%# DataBinder.Eval(Container.DataItem, "price", "{0:n2}") %>
                </td>
                <%--<td><asp:CheckBox runat="server" ID="chckToDelete" /></td>--%>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            <%--<tr><td colspan="4">celkem</td><td><asp:Literal runat="server" ID="litSum"></asp:Literal></td></tr>--%>
            <tr>
                <td colspan="3"><b>celkov� cena</b></td>
                <td style="text-align: right">
                    <b>
                        <%= Profile.ShoppingCart.Total.ToString("n2") %>
                    </b>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Button ID="btnSaveChanges" runat="server" Text="Ulo�it zm�ny" OnClick="btnSaveChanges_Click" /></td>
                <td>
                    <asp:Button ID="btnEmptyCart" runat="server" Text="Vysypat ko��k" OnClick="btnEmptyCart_Click" /></td>
            </tr>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <asp:Literal runat="server" ID="litCartIsEmptyMessage">Ko��k je pr�zdn�.</asp:Literal>
    <%--<asp:Button ID="Button1" runat="server" Text="Odstranit vybran� p�edm�ty z ko��ku" OnClick="Button1_Click" />--%>

    <asp:LoginView ID="LoginView1" runat="server">
        <LoggedInTemplate>
            <asp:Button ID="hypCreateOrder" runat="server" PostBackUrl="~/NotForAnon/CreateOrder.aspx" Text="Vytvo�it objedn�vku" Visible="true"></asp:Button>
            <asp:Label ID="lblOrderCreatePermission" runat="server" Text="Vytv��et objedn�vky m��ete pouze pokud jste p�ihl�en." Visible="false"></asp:Label>
        </LoggedInTemplate>
        <AnonymousTemplate>
            <asp:Button ID="hypCreateOrder" runat="server" PostBackUrl="~/NotForAnon/CreateOrder.aspx" Text="Vytvo�it objedn�vku" Visible="false"></asp:Button>
            <asp:Label ID="lblOrderCreatePermission" runat="server" Text="Vytv��et objedn�vky m��ete pouze pokud jste p�ihl�en." Visible="true"></asp:Label>
        </AnonymousTemplate>
    </asp:LoginView>
</asp:Content>
