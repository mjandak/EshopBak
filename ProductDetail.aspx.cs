using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Data.SqlClient;

public partial class ProductDetail : System.Web.UI.Page
{
    private List<int> ProductCategories = new List<int>();

    private List<int> Ascendants = new List<int>();

    protected void Page_Load(object sender, EventArgs e)
    {
      try
      {
        if (!IsPostBack)
        {
          string ProductId;
          ProductId = Request.QueryString["id"];

          SqlConnection Connection = new System.Data.SqlClient.SqlConnection();
          Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;

          //tri dotazy:  
          //prvni vybere detaily produktu
          //druhy vybere kategorie (id), ve kterych se nachazi produkt
          //treti vybere kategorie prvni urovne (id, title)
          SqlCommand Querry = new System.Data.SqlClient.SqlCommand();
          Querry.Parameters.Add("@ProductId", SqlDbType.Int).Value = ProductId;
          Querry.CommandText = "SELECT * FROM Goods WHERE id = @ProductId SELECT category_id FROM GoodsCategories WHERE product_id = @ProductId SELECT id, title FROM Categories WHERE parent_id = 0";
          Querry.Connection = Connection;

          Connection.Open();

          SqlDataReader Reader = Querry.ExecuteReader();

          //FormView1.DataSource = Reader;
          //FormView1.DataBind();

          Reader.Read();
          imgProduct.ImageUrl = Reader["large_image_url"].ToString();
          litTitle.Text = Reader["title"].ToString();
          litPrice.Text = ((decimal)Reader["price"]).ToString("n2");
          litId.Text = Reader["id"].ToString();
          litProducer.Text = Reader["producer"].ToString();
          litDescription.Text = Reader["description"].ToString();

          Reader.NextResult();

          while (Reader.Read())
          {
            ProductCategories.Add(Reader.GetInt32(0));
          }

          //vytvoreni a naplneni spolecneho seznamu predku kategorii, ve kterych se produkt nachazi (vcetne)
          foreach (int id in ProductCategories)
          {
            SqlConnection AscendantsConnection = new System.Data.SqlClient.SqlConnection();
            AscendantsConnection.ConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;

            //vybere predky (id) kategorie, vcetne id teto kategorie
            SqlCommand AscendantsQuerry = new System.Data.SqlClient.SqlCommand();
            AscendantsQuerry.CommandText = "WITH Ascendants (id, parent_id) AS (SELECT id, parent_id FROM Categories WHERE id = " + id + " UNION ALL SELECT a.id, a.parent_id FROM Categories AS a INNER JOIN Ascendants AS b ON a.id = b.parent_id) SELECT id FROM Ascendants";
            AscendantsQuerry.Connection = AscendantsConnection;

            AscendantsConnection.Open();

            SqlDataReader AscendantsReader = AscendantsQuerry.ExecuteReader();

            while (AscendantsReader.Read())
            {
              Ascendants.Add(AscendantsReader.GetInt32(0));
            }

            AscendantsReader.Close();

            AscendantsConnection.Close();
          }

          Reader.NextResult(); //treti dotaz

          while (Reader.Read())
          {
            string CategoryTitle = Reader.GetString(1);
            string CategoryId = Reader.GetInt32(0).ToString(); //GetInt16(0) doesn't work here, sql server probably stores integers as 32-bit
            MenuItem MenuItem = new MenuItem();
            MenuItem.Text = CategoryTitle;
            MenuItem.NavigateUrl = "CtgrBrowser.aspx?catid=" + CategoryId;
            menuCategoriesTree.Items.Add(MenuItem);
            if (Ascendants.Contains(Convert.ToInt32(CategoryId)))
            {
              if (ProductCategories.Contains(Convert.ToInt32(CategoryId)))
              {
                MenuItem.Text = "<b>" + CategoryTitle + "</b>"; //Nepredpoklada se, ze by produkt byl umisten v nejake kategorii a zaroven v jejích predcich (nebo potomcich)
              }
              else
              {
                CreateCategories(CategoryId, MenuItem);
              }
            }
          }

          Reader.Close();

          Connection.Close();
        }
      }
      catch (Exception)
      {
        //Response.Redirect("~/Error.aspx");
          throw;
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
      if (Ascendants.Contains(Convert.ToInt32(CategoryId)))
      {
        if (ProductCategories.Contains(Convert.ToInt32(CategoryId)))
        {
          MenuItem.Text = "<b>" + CategoryTitle + "</b>";
        }
        else
        {
          CreateCategories(CategoryId, MenuItem);
        }
      }
    }

    ChildsConnection.Close();
  }

  //protected void FormView1_ItemCommand(object sender, FormViewCommandEventArgs e)
  //{
  //  if (IsValid)
  //  {
  //    string product_id = FormView1.DataKey.Values["id"].ToString();
  //    string product_title = FormView1.DataKey.Values["title"].ToString();
  //    decimal product_price = Decimal.Parse(FormView1.DataKey.Values["price"].ToString());
  //    int product_quantity = Int32.Parse(((TextBox)FormView1.FindControl("txtQuantity")).Text);

  //    Profile.ShoppingCart.AddItem(product_id, product_title, product_price, product_quantity);
  //  }
  //}

  protected void btnAddToCart_Click(object sender, EventArgs e)
  {
      if (IsValid)
      {
          string product_id = litId.Text;
          string product_title = litTitle.Text;
          decimal product_price = Decimal.Parse(litPrice.Text);
          int product_quantity = Int32.Parse(txtQuantity.Text);

          Profile.ShoppingCart.AddItem(product_id, product_title, product_price, product_quantity);
      }
  }
}
