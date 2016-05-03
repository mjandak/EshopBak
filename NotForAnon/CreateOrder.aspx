<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateOrder.aspx.cs" Inherits="CreateOrder" MasterPageFile="~/MasterPage.master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <%--<asp:GridView ID="grdwOrders" runat="server" AutoGenerateColumns="false">
      <Columns>
      <asp:HyperLinkField DataTextField="orderId" DataNavigateUrlFields="orderId" DataNavigateUrlFormatString="OrderDetails.aspx?OrderId={0}" HeaderText="��slo objedn�vky" />
      <asp:BoundField DataField="OrderCreateDate" HeaderText="Vytvo�eno" />
      <asp:BoundField DataField="Total" HeaderText="Celkov� cena" />
      </Columns>
      </asp:GridView>--%>

    <span runat="server" id="OrderCreatedMsg">
        Va�e
        <asp:HyperLink ID="hypNewOrderDetails" runat="server">
            objedn�vka
        </asp:HyperLink>
        byla �sp�n� vytvo�ena a zaznamen�na v datab�zi. Na Va�i emailovou adresu byla odesl�na rekapitulace objedn�vky.
    </span>
    <span runat="server" id="EmptyCartMsg">V� ko��k je pr�zdn�, nen� co objedn�vat.</span>
</asp:Content>
