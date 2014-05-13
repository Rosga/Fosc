using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Web.Security;
using FavSites.Code;
using FavouriteSitesDAL;


namespace FavSites
{
    public partial class InsertSite : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddSite_Click(object sender, EventArgs e)
        {
            //створити новий об'єкт класу рівня доступу до даних, вказавши ім'я користувача
            var fs = new FavSitesDAL(Membership.GetUser().UserName);

            //відкрити з'єднання з БД, задавши БД
            fs.OpenConnection(WebConfigurationManager.ConnectionStrings["FavouriteSites"].ConnectionString);

            //додати запис до БД на основі заданих параметрів, отриманих шляхом вводу в текстові
            //елементи керування ASP.NET
            fs.InsertSite(txtSiteName.Text, txtSiteLink.Text,
                Thumbnail.CreateThumbnailImage(txtSiteLink.Text, 256, 192));

            //закрити з'єднання
            fs.CloseConnection();

            //обнулити значення текстових полів сторінки
            txtSiteLink.Text = null;
            txtSiteName.Text = null;
        }

    }
}