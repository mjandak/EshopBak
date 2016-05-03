<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Profile.aspx.cs" Inherits="NotForAnon_Profile" MasterPageFile="~/MasterPage.master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<table class="GenericTable">
    <tr><td>Jméno:</td><td><asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox></td></tr>
    <tr><td>Pøijmení:</td><td><asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></td></tr>
    <tr><td>Ulice:</td><td><asp:TextBox ID="txtStreet" runat="server"></asp:TextBox></td></tr>
    <tr><td>Mìsto:</td><td><asp:TextBox ID="txtCity" runat="server"></asp:TextBox></td></tr>
    <tr><td>PSÈ:</td><td><asp:TextBox ID="txtZipCode" runat="server"></asp:TextBox></td></tr>
    <tr><td>E-mail:</td><td><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td></tr>
</table>
<%--<asp:FormView ID="FormView1" runat="server" OnItemUpdating="FormView1_ItemUpdating" edi>
 <EditItemTemplate>
  <table>
  <tr><td>Jméno:</td><td><asp:TextBox ID="txtFirstName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FirstName") %>'></asp:TextBox></td></tr>
  <tr><td>Pøijmení:</td><td><asp:TextBox ID="txtLastName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastName") %>'></asp:TextBox></td></tr>
  <tr><td>Ulice:</td><td><asp:TextBox ID="txtStreet" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Street") %>'></asp:TextBox></td></tr>
  <tr><td>Mìsto:</td><td><asp:TextBox ID="txtCity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "City") %>'></asp:TextBox></td></tr>
  <tr><td>PSÈ:</td><td><asp:TextBox ID="txtZipCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ZipCode") %>'></asp:TextBox></td></tr>
  </table>
  <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Update">Uložit zmìny</asp:LinkButton>
 </EditItemTemplate>
</asp:FormView>--%>
    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Uložit zmìny</asp:LinkButton>
</asp:Content>