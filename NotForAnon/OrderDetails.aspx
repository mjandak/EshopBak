<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderDetails.aspx.cs" Inherits="OrderDetails" MasterPageFile="~/MasterPage.master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<div runat="server" id="divOrderDetails">
        <table class="GenericTable OrdersTable">
        <tr>
         <td colspan="4">Objedn�vka �. <asp:Literal ID="litOrderId" runat="server"></asp:Literal></td>
        </tr>
        <tr>
         <td colspan="4">Datum odesl�n�: <asp:Literal ID="litOrderCreated" runat="server"></asp:Literal></td>
        </tr>
        <tr>
         <td><b>k�d</b></td>
         <td><b>n�zev</b></td>
         <td><b>mno�stv�</b></td>
         <td><b>cena</b></td>
        </tr>
        <asp:Repeater ID="repOrderDetails" runat="server">
        <ItemTemplate>
        <tr>
                <td><%# DataBinder.Eval(Container.DataItem, "ProductId") %></td>
                <td><%# DataBinder.Eval(Container.DataItem, "title") %></td>
                <td><%# DataBinder.Eval(Container.DataItem, "ProductQuantity") %></td>
                <td><%# DataBinder.Eval(Container.DataItem, "ProductPrice") %></td>
        </tr>
        </ItemTemplate>
        </asp:Repeater>
        <tr>
        <td colspan="3"><b>celkem</b></td><td><asp:Literal runat="server" ID="litSum"></asp:Literal></td>
        </tr>
        <%--<tr>
          <td colspan="3">celkem</td><td><%# Profile.Orders.GetOrder(Convert.ToInt32(Request.QueryString["OrderId"])).Total %></td>
        </tr>--%>
        </table>
      <asp:HyperLink runat="server" ID="hypOrderDelete">Zru�it objedn�vku</asp:HyperLink>
      </div>
      <asp:Label ID="Label1" runat="server"></asp:Label>
</asp:Content>

