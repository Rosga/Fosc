using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Web.Security;
using FavouriteSitesDAL;


namespace FavSites
{
    public partial class Sites : System.Web.UI.Page
    {
        MembershipUser _membershipUser;

        //Загрузка сторінки
        protected void Page_Load(object sender, EventArgs e)
        {
            //отримати з БД інформацію про поточного користувача
            _membershipUser = Membership.GetUser();

            //якщо користувач не авторизований
            if (_membershipUser == null)
            {
                //перекинути його на сторінку авторизації
                Response.Redirect("~/Account/Login.aspx");
            }
            //інакше(отже користувач авторизований)
            else
            {
                var fs = new FavSitesDAL(_membershipUser.UserName);

                fs.OpenConnection(WebConfigurationManager.ConnectionStrings["FavouriteSites"].ConnectionString);

                //встановити як джерело даних елемента керування ASP.NET Web Forms
                //список закладок користувача 
                Repeater1.DataSource = fs.GetFaveSitesOfCurrentUserAsList();
                //Установити зв'язок з даними
                Repeater1.DataBind();

                //закрити з'єднання
                fs.CloseConnection();
            }        
        }

        protected void imgbtnDeleteSite_Command(object sender, CommandEventArgs e)
        {
            //створити новий об'єкт класу рівня доступу до даних, вказавши ім'я користувача 
            var fs = new FavSitesDAL(Membership.GetUser().UserName);
            //відкрити з'єднання з БД, задавши БД
            fs.OpenConnection(WebConfigurationManager.ConnectionStrings["FavouriteSites"].ConnectionString);

            //Видалили закладку із зазначеним ID
            fs.DeleteSite(int.Parse(e.CommandArgument.ToString()));
            //закрити з'єднання
            fs.CloseConnection();
            //обновити сторінку для відображення актуальних значень
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}