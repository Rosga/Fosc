using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace FavouriteSitesDAL
{
    public class NewSite
    { 
        public string SiteName { get; set; }
        public string SiteLink { get; set; }
        public int SiteID { get; set; }
        public string ImgSiteUrl { get; set; }
        public int Position { get; set; }
    }

    //Клас FavSitesDAL
    //Визначає рівень доступу до даних у базі даних Favourite sites
    public class FavSitesDAL
    {
        //закрите поле. Зберігає в собі підключення до БД
        private SqlConnection _sqlCon;

        //закрите поле. Зберігає ім'я поточного авторизованого користувача
        private string currentUser;

        //Стандартний конструктор
        public FavSitesDAL()
        {
            
        }

        //Спеціальний конструктор. 
        //Присвоює полю поточного користувача отримане за параметром строкове значення
        public FavSitesDAL(string currentUser)
        {
            this.currentUser = currentUser;
        }

        //Відкритий метод.
        //Призначений для відкриття підключення до бази даних, переданій в якості параметру.
        public void OpenConnection(string connectionString)
        {
            //створити нове підключення до БД
            _sqlCon = new SqlConnection {ConnectionString = connectionString};

            //відкрити підключення до бД
            _sqlCon.Open();
        }

        //Відкритий метод
        //Призначений для закриття підключення до БД
        public void CloseConnection()
        {
            //Закрити підключення
            _sqlCon.Close();
        }

        //Закритий метод
        //Повертає кількість доданих сайтів поточного користувача
        private int GetCountOfSitesOfCurrentUser()
        {
            //створити SQL-запит
            var sql = string.Format("SELECT COUNT (*) FROM Sites WHERE userName = @userName");
                                    //вибрати всі записи з таблиці Sites, для задного параметра username
            //створити T-SQL-команду
            var cmd = new SqlCommand(sql, _sqlCon);

            //задати значення параметра
            cmd.Parameters.AddWithValue("@userName", currentUser);

            //повернути пораховане значення
            return (int) cmd.ExecuteScalar();
        }

        //Відкритий метод
        //Додає новий запис до БД на основі параметрів.
            //siteName - назва сайту
            //siteLink - посилання на сайт
            //siteImage - зображення-мініатюра сайту
        public void InsertSite(string siteName, string siteLink, byte[] siteImage)
        {
            //Створити SQL-запит
            var sql = string.Format("Insert into Sites" +
                "(userName, SiteName, SiteLink,SiteImage,Position) Values" +
                "(@userName,@SiteName,@SiteLink,@SiteImage, @Position)");
            //вставити в таблицю Sites у поля userName, SiteName, SiteLink,SiteImage,Position
                        //значення параметрів @userName,@SiteName,@SiteLink,@SiteImage, @Position

            //створити SQL-команду
            var cmd = new SqlCommand(sql, _sqlCon);

            //дізнатись кількість уже доданим поточним користувачем закладок
            //та збільшити це значення на одиницю
            var position = GetCountOfSitesOfCurrentUser() + 1;

            //присвоєти значення параметрів
            cmd.Parameters.AddWithValue("@userName", currentUser);  //ім'я користувача
            cmd.Parameters.AddWithValue("@SiteName", siteName);     //назва сайту
            cmd.Parameters.AddWithValue("@SiteLink", siteLink);     //посилання на сайт
            cmd.Parameters.AddWithValue("@Position", position);     //номер позиції закладки у списку
            var param = new SqlParameter
            {
                DbType = DbType.Binary,
                ParameterName = "@SiteImage",
                Value = siteImage
            }; 
            cmd.Parameters.Add(param); //зображення-мініатюра сайту

            //виконати команду
            cmd.ExecuteNonQuery();
        }

        //Відкрита функція
        //видаляє закладку із БД за переданним значення ID закладки
        public void DeleteSite(int id)
        {
            //Створити SQL-запит
            var sqlPos = string.Format("SELECT Position FROM Sites WHERE SiteId = {0}", id);
                                        //вибрати значення позиції з табл. Sites, де SiteID дорівнює заданому параметру

            //створити новий параметр, встановивши
            var paramId = new SqlParameter
            {
                DbType = DbType.Int32,      //тип
                ParameterName = "@SiteId",  //ім'я
                Value = id                  //значення
            };

            //установити за замовчуванням позицію 0
            var position = 0;
            //створити нову Т-SQL-команду
            using (var cmdGetPos = new SqlCommand(sqlPos, _sqlCon))
            {
                //отримати значення позиції сайту за його ID
                var reader = cmdGetPos.ExecuteReader();
                while (reader.Read())
                {
                    position = (int)reader["Position"];
                }
                reader.Close();
            }

            //створити новий параметр, встановивши
            var paramPos = new SqlParameter
            {
                DbType = DbType.Int32,          //тип
                ParameterName = "@Position",    //ім'я
                Value = position                //значення
            };

            //створити новий параметр, встановивши
            var paramUserName = new SqlParameter
            {
                DbType = DbType.String,         //тип
                ParameterName = "@userName",    //ім'я
                Value = currentUser             //значення
            };

            //Створити SQL-запит
            var sqlDecPos = string.Format("UPDATE Sites SET Position = Position-1 WHERE userName = @userName AND Position > @Position");
                                        //обновити таблицю Sites
                                        //декрементувати значення поля Position 
                                        //для записів з заданими значеннями користувача, 
                                        //що більші за задане значення параметру позиції

            //створити нову Т-SQL-команду
            using (var cmd = new SqlCommand(sqlDecPos, _sqlCon))
            {
                //додати параметри до команди
                cmd.Parameters.Add(paramPos);
                cmd.Parameters.Add(paramUserName);

                //виконати команду
                cmd.ExecuteNonQuery();
            }

            //Створити SQL-запит
            var sql = string.Format("Delete from Sites where SiteId = @SiteId");
                                    //видалити за таблиці Sites запис із заданим значенням SiteID

            //створити нову Т-SQL-команду
            using (var cmd = new SqlCommand(sql, _sqlCon))
            {
                //додати параметри до команди
                cmd.Parameters.Add(paramId);

                //виконати команду
                cmd.ExecuteNonQuery();
            }
        }

        //Відкритий метод
        //Повертає список типу NewSite усіх записів поточного користувача з БД
        public List<NewSite> GetFaveSitesOfCurrentUserAsList()
        {
            var data = new List<NewSite>();

            //створити SQL-запит
            const string sql = "SELECT * FROM Sites WHERE userName = @userName ORDER BY Position ASC";
                                //вибрати всі записи з таблиці Sites
                                //із заданим параметром userName,
                                //впорядковуючи записи в порядку зростання за полем Position

            //створити новий параметр, задавши
            var paramUserName = new SqlParameter
            {
                DbType = DbType.String,         //тип
                ParameterName = "@userName",    //ім'я
                Value = currentUser             //значення
            };

            //створити нову T-SQL-команду та додати до неї параметр з іменем користувача
            var cmd = new SqlCommand(sql, _sqlCon);
            cmd.Parameters.Add(paramUserName);

            //Виконати зчитування 
            var dataReader = cmd.ExecuteReader();
                //цикл
                //Виконується поки є записи в таблиці
                //Якщо умова вірна - записати у список значення запису таблиці
                while (dataReader.Read())
                {
                    //Конвертувати бітове значення зображення в строкове посилання на це зображення
                    var imgByteString = (byte [])dataReader["SiteImage"];
                    var imgStringUrl = String.Format("data:image/Bmp;base64,{0}", Convert.ToBase64String(imgByteString)); 
                    
                    //додати запис до списку
                        data.Add(new NewSite
                        {
                            SiteName = dataReader["SiteName"].ToString(),
                            SiteLink = dataReader["SiteLink"].ToString(),
                            SiteID = (int)dataReader["SiteId"],
                            ImgSiteUrl = imgStringUrl,
                            Position = (int)dataReader["Position"]
                        });
                }
            //закінчити зчитування
            dataReader.Close();

            //повернути список закладок
            return data;
        }
    }
}
