<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateOrder.aspx.cs" Inherits="CreateOrder" MasterPageFile="~/MasterPage.master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <%--<asp:GridView ID="grdwOrders" runat="server" AutoGenerateColumns="false">
      <Columns>
      <asp:HyperLinkField DataTextField="orderId" DataNavigateUrlFields="orderId" DataNavigateUrlFormatString="OrderDetails.aspx?OrderId={0}" HeaderText="Èíslo objednávky" />
      <asp:BoundField DataField="OrderCreateDate" HeaderText="Vytvoøeno" />
      <asp:BoundField DataField="Total" HeaderText="Celková cena" />
      </Columns>
      </asp:GridView>--%>

    <span runat="server" id="OrderCreatedMsg">
        Vaše
        <asp:HyperLink ID="hypNewOrderDetails" runat="server">
            objednávka
        </asp:HyperLink>
        byla úspìšnì vytvoøena a zaznamenána v databázi. Na Vaši emailovou adresu byla odeslána rekapitulace objednávky.
    </span>
    <span runat="server" id="EmptyCartMsg">Váš košík je prázdný, není co objednávat.</span>
</asp:Content>
