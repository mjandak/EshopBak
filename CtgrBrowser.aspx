<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CtgrBrowser.aspx.cs" Inherits="CtgrBrowser"
	MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
	<table class="CategoriesTable" cellspacing="3">
		<tr>
			<td class="CategoriesTableTitle">
				Kategorie
			</td>
		</tr>
		<tr>
			<td>
				<asp:Menu ID="menuCategoriesTree" runat="server">
				</asp:Menu>
			</td>
		</tr>
	</table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
	<%--<asp:Label ID="lblCategoryTitle" runat="server"></asp:Label>--%>
	<div style="text-align:left; width:519px">
		Seřadit dle ceny:
		<asp:HyperLink ID="hypPriceAsc" runat="server">&#9650;</asp:HyperLink>&#160;<asp:HyperLink
			ID="hypPriceDesc" runat="server">&#9660;</asp:HyperLink>
	</div>
	<%--<asp:DropDownList runat="server" ID="cbxPriceOrder" AutoPostBack="true" EnableViewState="true">
		<asp:ListItem Value="" Text="" />
		<asp:ListItem Value="ASC" Text="Vzestupně" />
		<asp:ListItem Value="DESC" Text="Sestupně" />
	</asp:DropDownList>--%>
	<asp:DataList ID="dlstCategoryContents" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
		OnItemCommand="dlstCategoryContents_ItemCommand" DataKeyField="id" CellPadding="3"
		ItemStyle-Width="166px">
		<ItemTemplate>
			<%--<asp:Literal ID="litId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>'></asp:Literal><br />--%>
			<div style="height: 32px">
				<b>
					<asp:Literal ID="litTitle" runat="server" Text='<%# ((System.Data.Common.DbDataRecord)Container.DataItem).GetString(1) %>' />
				</b>
			</div>
			<br />
			<div style="height: 120px;">
				<asp:Image AlternateText="Náhled není k dispozoci." runat="server" ID="imgProductImage"
					ImageUrl='<%# DataBinder.Eval(Container.DataItem, "small_image_url") %>' />
			</div>
			<br />
			<asp:Literal ID="litPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "price", "{0:n2}") %>' />
			Kč<br />
			<div style="border: solid 1px black; background-color: #eee8aa; padding: 3px">
				<asp:LinkButton ID="lbtnAddToCart" runat="server" CommandName="select">Přidat do košíku</asp:LinkButton><br />
				<asp:HyperLink ID="lbtnDetails" runat="server" NavigateUrl='<%# "~/ProductDetail.aspx?id=" + DataBinder.Eval(Container.DataItem, "id") %>'>Detaily</asp:HyperLink><br />
			</div>
		</ItemTemplate>
		<ItemStyle BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" />
	</asp:DataList>
	<table>
		<!--strankovani-->
		<tr>
			<td style="width: 100px; text-align: right">
				<asp:HyperLink ID="hypPrevious" runat="server"><< předchozí</asp:HyperLink>
			</td>
			<td style="width: 100px; text-align: center">
				<asp:Literal ID="litPageNumber" runat="server"></asp:Literal>
				z
				<asp:Literal ID="litNumberOfPages" runat="server"></asp:Literal>
			</td>
			<td style="width: 100px; text-align: left">
				<asp:HyperLink ID="hypNext" runat="server">další >></asp:HyperLink>
			</td>
		</tr>
	</table>
</asp:Content>
