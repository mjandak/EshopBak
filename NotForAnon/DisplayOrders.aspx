<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DisplayOrders.aspx.cs" Inherits="DisplayOrders" MasterPageFile="~/MasterPage.master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
        <%--<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="GenericTable OrdersTable">
        <Columns>
         <asp:HyperLinkField DataTextField="OrderId" DataNavigateUrlFields="OrderId" DataNavigateUrlFormatString="OrderDetails.aspx?OrderId={0}" HeaderText="��slo objedn�vky" />
         <asp:BoundField DataField="CreateDate" HeaderText="Vytvo�eno" />
         <asp:BoundField DataField="Total" HeaderText="Celkov� cena" />
        </Columns>
        </asp:GridView>--%>
        
        <asp:Repeater runat="server" ID="repOrders">
        <HeaderTemplate>
        <table class="GenericTable OrdersTable">
        <tr>
         <td><b>��slo objedn�vky</b></td>
         <td><b>Vytvo�eno</b></td>
         <td><b>Celkov� cena</b></td>
         <td><b>Stav</b></td>
        </tr>
        </HeaderTemplate>
        <ItemTemplate>
        <tr>
         <td><a href="OrderDetails.aspx?OrderId=<%# DataBinder.Eval(Container.DataItem, "id") %>"><%# DataBinder.Eval(Container.DataItem, "id") %></a></td>
         <td><%# DataBinder.Eval(Container.DataItem, "CreateDate")%></td>
         <td><%# DataBinder.Eval(Container.DataItem, "Total")%></td>
         <td><%# DataBinder.Eval(Container.DataItem, "State")%></td>
        </tr>
        </ItemTemplate>
        <FooterTemplate>
        </table>
        </FooterTemplate>
        </asp:Repeater>
</asp:Content>
