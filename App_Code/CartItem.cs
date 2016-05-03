using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for CartItem
/// </summary>

[Serializable]
public class CartItem
{
    private string id;
    private string title;
    private decimal price;
    private int quantity;

    public string Id
    {
        get { return id; }
    }

    public string Title
    {
        get { return title; }
    }

    public decimal Price
    {
        get { return price; }
    }

    public int Quantity
    {
        get { return quantity; }
        set { quantity = value; }
    }

    public CartItem(string Id, string Title, decimal Price, int Quantity)
    {
        id = Id;
        title = Title;
        price = Price;
        quantity = Quantity;
    }

}
