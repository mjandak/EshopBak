<%@ Application Language="C#" %>
<%@ Import Namespace="System.IO" %>
<script RunAt="server">

	void Application_Start(object sender, EventArgs e)
	{
		// Code that runs on application startup

	}

	void Application_End(object sender, EventArgs e)
	{
		//  Code that runs on application shutdown

	}

	void Application_Error(object sender, EventArgs e) 
	{ 
		// Code that runs when an unhandled error occurs
		
		Exception exc = Server.GetLastError();
        using (StreamWriter sw = File.AppendText(@"D:\Websites\80327e6b9f\www\Log\Err.log"))
        {
            sw.WriteLine(DateTime.Now.ToString());
            //sw.WriteLine(exc.Message);
            //sw.WriteLine(exc.StackTrace);
            //sw.WriteLine(exc.InnerException.Message);
            sw.WriteLine(exc);
            sw.WriteLine(exc.GetBaseException());
            sw.WriteLine();
        }
		//StreamReader sr = File.OpenText(@"D:\Websites\80327e6b9f\www\Log\Err.log");
		//Response.Write(sr.ReadToEnd());
		//sr.Dispose();
		//Response.Write("Omlouváme se, došlo k chybě.");
		//Response.Flush();
		//Response.End();
        Response.Redirect("~/Error.aspx");
	}

	void Session_Start(object sender, EventArgs e)
	{
		// Code that runs when a new session is started

	}

	void Session_End(object sender, EventArgs e)
	{
		// Code that runs when a session ends. 
		// Note: The Session_End event is raised only when the sessionstate mode
		// is set to InProc in the Web.config file. If session mode is set to StateServer 
		// or SQLServer, the event is not raised.

	}

	void Profile_MigrateAnonymous(object sender, ProfileMigrateEventArgs e)
	{
		//string a;
		//a = e.AnonymousID;
		//a = e.Context.User.Identity.Name;
		ProfileCommon anonProfile = Profile.GetProfile(e.AnonymousID);
		if (anonProfile.ShoppingCart.CartItems.Count > 0) //pokud anonymni kosik neco obsahuje, tak se bude migrovat, jinak ne
		{
			Profile.ShoppingCart = anonProfile.ShoppingCart;
		}
		////Profile.FirstName = anonProfile.FirstName;
		////Profile.LastName = anonProfile.LastName;
		bool b;
		b = ProfileManager.DeleteProfile(e.AnonymousID); //vymaze anonymni profil z tabulky aspnet_Profile (zmeni UserId anonymniho uzivatele na UserId prave zalogovaneho uzivatele)
		AnonymousIdentificationModule.ClearAnonymousIdentifier(); //vymaze anonymni cookie
		Membership.DeleteUser(e.AnonymousID, true); //vy
	}
	   
</script>
