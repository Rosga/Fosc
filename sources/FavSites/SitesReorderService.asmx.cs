using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Script.Services;
using System.Web.Services;

namespace FavSites
{
    /// <summary>
    /// Summary description for SitesReorderService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 

    //Веб-служба
    //Призначена для збереження новозміненого положення закладок
    [ScriptService]
    public class SitesReorderService : System.Web.Services.WebService
    {
        
        //Веб-метод
        //Встановлює новий порядок закладок
        //параметр order - новий порядок закладок, збережений у строковому вигляді
        [WebMethod]
        public void SetNewOrderOfSites(string order)
        {
            //перетворити строку у масив стрічок використовуючи подільник строки
            //порядковий номер значення у масиві дорівнює значення позиції закладки
            var newSitesOrder = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(order);

            //створити нове підключення до БД
            var con = new SqlConnection(WebConfigurationManager.ConnectionStrings["FavouriteSites"].ConnectionString);

            //відкрити підключення
            con.Open();
            //цикл
            //прийтись по всьому масиву стрічок і встановити нове значення позиції для кожної закладки
            for (int i = 0; i < newSitesOrder.Length; i++)
            {
                SetNewPos(i, con, int.Parse(newSitesOrder[i]));
            }
            con.Close();
        }

        //закритий метод
        //Встановлює нове значення позиції закладки, використовуючи такі параметри:
            //newPos - нове значення позиції
            //siteId - ID закладки
        private void SetNewPos(int newPos, SqlConnection sqlCon, int siteId)
        {
            //створити SQL-запит
            var sql = string.Format("UPDATE Sites SET Position = @NewPos WHERE SiteId = @SiteId");
                                    //обновити таблицю Sites
                                    //Установити нове значення Position 
                                    //для запису з заданим значенням SiteId


            var paramNewPos = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@NewPos",
                Value = newPos
            };
            var paramSiteId = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@SiteId",
                Value = siteId
            };

            using (var cmd = new SqlCommand(sql, sqlCon))
            {
                cmd.Parameters.Add(paramNewPos);
                cmd.Parameters.Add(paramSiteId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
