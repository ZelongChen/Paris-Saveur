using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paris_Saveur.DataBase
{
    class DatabaseHelper
    {

        SQLiteConnection dbConn;

        //Create Tabble 
        public async Task<bool> onCreate(string DB_PATH)
        {
            try
            {
                if (!CheckFileExists(DB_PATH).Result)
                {
                    using (dbConn = new SQLiteConnection(DB_PATH))
                    {
                        dbConn.CreateTable<Restaurant>();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private async Task<bool> CheckFileExists(string fileName)
        {
            try
            {
                var store = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Retrieve the specific contact from the database. 
        public Restaurant ReadRestaurant(int contactid)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var restaurant = dbConn.Query<Restaurant>("select * from Restaurant where Id =" + contactid).FirstOrDefault();
                return restaurant;
            }
        }
        // Retrieve the all contact list from the database. 
        public ObservableCollection<Restaurant> ReadAllRestaurant()
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                List<Restaurant> myCollection = dbConn.Table<Restaurant>().ToList<Restaurant>();
                ObservableCollection<Restaurant> RestaurantList = new ObservableCollection<Restaurant>(myCollection);
                return RestaurantList;
            }
        }

        //Update existing conatct 
        public void UpdateRestaurant(Restaurant restaurant)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var existingRestaurant = dbConn.Query<Restaurant>("select * from Contacts where Id =" + restaurant.pk).FirstOrDefault();
                if (existingRestaurant != null)
                {
                    existingRestaurant.ViewTime = existingRestaurant.ViewTime;
                    existingRestaurant.Bookmarked = existingRestaurant.Bookmarked;
                    dbConn.RunInTransaction(() =>
                    {
                        dbConn.Update(existingRestaurant);
                    });
                }
            }
        }
        // Insert the new contact in the Contacts table. 
        public void Insert(Restaurant newRestaurant)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.Insert(newRestaurant);
                });
            }
        }

        //Delete specific contact 
        public void DeleteRestaurant(int Id)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var existingRestaurant = dbConn.Query<Restaurant>("select * from Restaurant where Id =" + Id).FirstOrDefault();
                if (existingRestaurant != null)
                {
                    dbConn.RunInTransaction(() =>
                    {
                        dbConn.Delete(existingRestaurant);
                    });
                }
            }
        }
        //Delete all contactlist or delete Contacts table 
        public void DeleteAlRestaurant()
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                //dbConn.RunInTransaction(() => 
                //   { 
                dbConn.DropTable<Restaurant>();
                dbConn.CreateTable<Restaurant>();
                dbConn.Dispose();
                dbConn.Close();
                //}); 
            }
        } 
    }
}
