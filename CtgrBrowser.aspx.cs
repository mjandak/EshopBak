using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CtgrBrowser : System.Web.UI.Page
{
	private string ClickedCategoryId;

	private List<int> Ascendants = new List<int>();

	protected void Page_Load(object sender, EventArgs e)
	{
		Title = "Category Browser";
		//ConnectionStringSettings ConnStr = ConfigurationManager.ConnectionStrings["DatabaseConnection"];
		//connectionString = ConnStr.ConnectionString;

		//Postback nastane při přidání do košíku a přihlášení. Při těchto akcích není potřeba z databáze znovu obnovovat strom kategorií a seznam produktů.
		//Použije se ViewState.
		if (!IsPostBack) 
		{
			string over = "id ASC";
			if (Request.QueryString["priceord"] == "asc")
			{
				over = "price ASC";
			}
			else if (Request.QueryString["priceord"] == "desc")
			{
				over = "price DESC";
			}

			hypPriceAsc.NavigateUrl = GetQueryString("priceord", "asc");
			hypPriceDesc.NavigateUrl = GetQueryString("priceord", "desc");

			//existuje parametr catid?
			if (Request.QueryString["catid"] == null)
			{
				string PageStart;
				string NextPageStart;
				string PreviousPageStart;

				//exstuje parametr ps?
				if (Request.QueryString["ps"] == null)
				{
					PageStart = "1";
				}
				else
				{
					PageStart = Request.QueryString["ps"];
				}

				NextPageStart = Convert.ToString((int.Parse(PageStart) + 9));
				PreviousPageStart = Convert.ToString((int.Parse(PageStart) - 9));

				SqlConnection Connection = new System.Data.SqlClient.SqlConnection();
				Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;

				//vybere kategorie 1. urovne, tj. ty co maji parent_id = 0
				SqlCommand Query = new System.Data.SqlClient.SqlCommand();
				Query.Parameters.Add("@PageStart", SqlDbType.Int).Value = PageStart;
				Query.Parameters.Add("@NextPageStart", SqlDbType.Int).Value = NextPageStart;
				Query.CommandText = "SELECT id, title FROM Categories WHERE parent_id = 0";
				Query.Connection = Connection;

				Connection.Open();

				SqlDataReader FirstLevelCategories = Query.ExecuteReader();

				//pro kazdou kategorii vytvori odpovidajici polozku v menu
				while (FirstLevelCategories.Read())
				{
					string CategoryTitle = FirstLevelCategories.GetString(FirstLevelCategories.GetOrdinal("title"));
					string CategoryId = FirstLevelCategories.GetInt32(FirstLevelCategories.GetOrdinal("id")).ToString(); //GetInt16(0) doesn't work here, sql server probably stores integers as 32-bit
					MenuItem MenuItem = new MenuItem();
					MenuItem.Text = CategoryTitle;
					MenuItem.NavigateUrl = "CtgrBrowser.aspx?catid=" + CategoryId;
					menuCategoriesTree.Items.Add(MenuItem);
				}

				FirstLevelCategories.Close();

				//vybere stranku specialnich produktu
				Query.CommandText = String.Format(@"
				SELECT id, title, price, small_image_url
				FROM (
					SELECT row_number() OVER (ORDER BY {0}) AS rownumber, id, title, price, small_image_url
					FROM Goods
					WHERE special = 1
				) AS a
				WHERE rownumber >= @PageStart AND rownumber < @NextPageStart", over);

				SqlDataReader SpecialProducts = Query.ExecuteReader();

				dlstCategoryContents.DataSource = SpecialProducts;
				dlstCategoryContents.DataBind();

				SpecialProducts.Close();

				//vrati pocet specialnich produktu
				Query.CommandText = "SELECT COUNT(*) FROM Goods WHERE special = 1";

				double NumberOfSpecialProducts = Convert.ToDouble(Query.ExecuteScalar()); //deleni dvou int zahazuje zbytek a Math.Ceiling prijima jen Double nabo Decimal
				double NumberOfPages = Math.Ceiling(NumberOfSpecialProducts / 9);

				Connection.Close();

				//hypNext.NavigateUrl = "CtgrBrowser.aspx?ps=" + NextPageStart;
				hypNext.NavigateUrl = GetQueryString("ps", NextPageStart);
				//hypPrevious.NavigateUrl = "CtgrBrowser.aspx?ps=" + PreviousPageStart;
				hypPrevious.NavigateUrl = GetQueryString("ps", PreviousPageStart);
				litNumberOfPages.Text = Convert.ToString(NumberOfPages);
				litPageNumber.Text = Convert.ToString((Convert.ToInt32(NextPageStart) / 9));

				if (Convert.ToInt32(NextPageStart) > NumberOfSpecialProducts)
				{
					hypNext.Enabled = false;
				}

				if (PageStart == "1")
				{
					hypPrevious.Enabled = false;
				}
				else
				{
					hypPrevious.Enabled = true;
				}
			}
			else
			{
				string PageStart;
				string NextPageStart;
				string PreviousPageStart;
				ClickedCategoryId = Request.QueryString["catid"]; //id vybrane kategorie
				if (Request.QueryString["ps"] == null)
				{
					PageStart = "1";
				}
				else
				{
					PageStart = Request.QueryString["ps"];
				}
				NextPageStart = Convert.ToString((int.Parse(PageStart) + 9));
				PreviousPageStart = Convert.ToString((int.Parse(PageStart) - 9));

				SqlConnection Connection = new SqlConnection();
				Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;

				//vybere id predku vybrane kategorie, vcetne id vybrane kategorie
				SqlCommand Querry = new SqlCommand();
				Querry.Parameters.Add("@ClickedCategoryId", SqlDbType.Int).Value = ClickedCategoryId;
				Querry.Parameters.Add("@PageStart", SqlDbType.Int).Value = PageStart;
				Querry.Parameters.Add("@NextPageStart", SqlDbType.Int).Value = NextPageStart;
				Querry.CommandText = "WITH Ascendants (id, parent_id) AS (SELECT id, parent_id FROM Categories WHERE id = @ClickedCategoryId UNION ALL SELECT a.id, a.parent_id FROM Categories AS a INNER JOIN Ascendants AS b ON a.id = b.parent_id) SELECT id FROM Ascendants";
				Querry.Connection = Connection;

				Connection.Open();

				SqlDataReader AscendantsReader = Querry.ExecuteReader();

				//naplni seznam id predku vybrane kategorie, vcetne id vybrane kategorie
				while (AscendantsReader.Read())
				{
					Ascendants.Add(AscendantsReader.GetInt32(0));
				}

				AscendantsReader.Close();

				//vybere kategorie (id, title) prvni urovne, ty jsou videt vzdycky vsechny
				Querry.CommandText = "SELECT id, title FROM Categories WHERE parent_id = 0";

				SqlDataReader FirstLevelCategories = Querry.ExecuteReader();

				//pro kazdou kategorii prvni urovne vytvori odpovidajici polozku v menu
				while (FirstLevelCategories.Read())
				{
					string CategoryTitle = FirstLevelCategories.GetString(1);
					string CategoryId = FirstLevelCategories.GetInt32(0).ToString(); //GetInt16(0) doesn't work here, sql server probably stores integers as 32-bit
					MenuItem MenuItem = new MenuItem();
					MenuItem.Text = CategoryTitle;
					MenuItem.NavigateUrl = "CtgrBrowser.aspx?catid=" + CategoryId;
					menuCategoriesTree.Items.Add(MenuItem);
					if (CategoryId == ClickedCategoryId)
					{
						MenuItem.Selected = true;
					}
					if (Ascendants.Contains(Convert.ToInt32(CategoryId)))
					{
						CreateCategories(CategoryId, MenuItem);
					}
				}

				FirstLevelCategories.Close();

				Querry.CommandText = String.Format(@"
				WITH Descendants (id)
				AS (
					SELECT id FROM Categories WHERE id = @ClickedCategoryId UNION ALL SELECT a.id FROM Categories AS a INNER JOIN Descendants AS b ON a.parent_id = b.id
				)
				SELECT id, title, price, small_image_url
				FROM (
					SELECT row_number() OVER (ORDER BY {0}) AS rownumber, id, title, price, small_image_url
					FROM (
						SELECT DISTINCT product_id 
						FROM
						(SELECT id FROM Descendants) AS a
						INNER JOIN GoodsCategories AS b ON a.id = b.category_id
					) AS c
					INNER JOIN Goods AS d ON c.product_id = d.id
				) AS e
				WHERE rownumber >= @PageStart AND rownumber < @NextPageStart", over);

				SqlDataReader Products = Querry.ExecuteReader();

				if (Products.HasRows) //jsou nejake produkty
				{
					dlstCategoryContents.DataSource = Products;
					dlstCategoryContents.DataBind();

					Products.Close();

					//vrati pocet vsech produktu pod kategorii
					Querry.CommandText = "WITH Descendants (id) AS (SELECT id FROM Categories WHERE id = @ClickedCategoryId UNION ALL SELECT a.id FROM Categories AS a INNER JOIN Descendants AS b ON a.parent_id = b.id) SELECT COUNT (DISTINCT product_id) FROM (SELECT id FROM Descendants) AS a INNER JOIN GoodsCategories AS b ON a.id = b.category_id";

					double NumberOfProducts = Convert.ToDouble(Querry.ExecuteScalar()); //deleni dvou int zahazuje zbytek a Math.Ceiling prijima jen Double nabo Decimal
					double NumberOfPages = Math.Ceiling(NumberOfProducts / 9);

					Connection.Close();

					//hypNext.NavigateUrl = "CtgrBrowser.aspx?catid=" + ClickedCategoryId + "&ps=" + NextPageStart;
					hypNext.NavigateUrl = GetQueryString("ps", NextPageStart);
					//hypPrevious.NavigateUrl = "CtgrBrowser.aspx?catid=" + ClickedCategoryId + "&ps=" + PreviousPageStart;
					hypPrevious.NavigateUrl = GetQueryString("ps", PreviousPageStart);
					litNumberOfPages.Text = Convert.ToString(NumberOfPages);
					litPageNumber.Text = Convert.ToString((Convert.ToInt32(NextPageStart) / 9));

					if (Convert.ToInt32(NextPageStart) > NumberOfProducts)
					{
						hypNext.Enabled = false;
					}

					if (PageStart == "1")
					{
						hypPrevious.Enabled = false;
					}
					else
					{
						hypPrevious.Enabled = true;
					}
				}
				else
				{
					hypNext.Enabled = false;
					hypPrevious.Enabled = false;
				}
			}
		}
	}

	protected void CreateCategories(string ParentCategoryId, MenuItem ParentMenuItem)
	{
		//tady se musi vytvorit nove pripojeni, to prvni nejde priradit k novemu prikazu (pokud se nezavre s nim asociovany datareader)
		//While the SqlDataReader is being used, the associated SqlConnection is busy serving the SqlDataReader, and no other operations can be performed on the SqlConnection other than closing it.
		SqlConnection ChildsConnection = new System.Data.SqlClient.SqlConnection();
		ChildsConnection.ConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;

		SqlCommand ChildCategoriesQuerry = new SqlCommand();
		ChildCategoriesQuerry.Parameters.Add("@ParentCategoryId", SqlDbType.Int).Value = ParentCategoryId;
		ChildCategoriesQuerry.CommandText = "SELECT id, title FROM Categories WHERE parent_id = @ParentCategoryId";
		ChildCategoriesQuerry.Connection = ChildsConnection;

		ChildsConnection.Open();

		SqlDataReader ChildCategories = ChildCategoriesQuerry.ExecuteReader();

		while (ChildCategories.Read())
		{
			string CategoryTitle = ChildCategories.GetString(1);
			string CategoryId = ChildCategories.GetInt32(0).ToString(); //GetInt16(0) doesn't work here, sql server probably stores integers as 32-bit
			MenuItem MenuItem = new MenuItem();
			MenuItem.Text = CategoryTitle;
			MenuItem.NavigateUrl = "CtgrBrowser.aspx?catid=" + CategoryId;
			ParentMenuItem.ChildItems.Add(MenuItem);
			if (CategoryId == ClickedCategoryId)
			{
				MenuItem.Selected = true;
			}
			if (Ascendants.Contains(Convert.ToInt32(CategoryId)))
			{
				CreateCategories(CategoryId, MenuItem);
			}
		}

		ChildsConnection.Close();
	}

	protected void dlstCategoryContents_ItemCommand(object source, DataListCommandEventArgs e) //pridej product do kosiku
	{
		if (Profile.ShoppingCart == null)
		{
			Profile.ShoppingCart = new ShoppingCart();
		}

		string id = dlstCategoryContents.DataKeys[e.Item.ItemIndex].ToString();
		string title = ((Literal)e.Item.FindControl("litTitle")).Text;
		decimal price = decimal.Parse(((Literal)e.Item.FindControl("litPrice")).Text);

		Profile.ShoppingCart.AddItem(id, title, price, 1);
	}

	protected string GetQueryString(string key, string value)
	{
		StringBuilder queryString = new StringBuilder();
		//queryString.Append(Request.Url.Host);
		queryString.Append(Request.Url.AbsolutePath);
		queryString.Append('?');
		foreach (string k in Request.QueryString.Keys)
		{
			if (k == key) continue;
			queryString.Append(k);
			queryString.Append('=');
			queryString.Append(Request.QueryString[k]);
			queryString.Append('&');
		}
		queryString.Append(key);
		queryString.Append('=');
		queryString.Append(value);
		return queryString.ToString();
	}

	protected void Page_PreRender(object sender, EventArgs e)
	{
		if (Session.IsNewSession)
		{
			HttpCookie c = FormsAuthentication.GetAuthCookie("p", false);
			Response.Cookies["_xa"].Expires = c.Expires;
		}
	}
}
