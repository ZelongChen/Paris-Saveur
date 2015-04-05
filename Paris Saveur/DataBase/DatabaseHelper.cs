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
                        dbConn.CreateTable<RestaurantDB>();
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
        public RestaurantDB ReadRestaurant(int contactid)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var restaurant = dbConn.Query<RestaurantDB>("select * from RestaurantDB where Id =" + contactid).FirstOrDefault();
                return restaurant;
            }
        }
        // Retrieve the all contact list from the database. 
        public ObservableCollection<RestaurantDB> ReadAllRestaurant()
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                List<RestaurantDB> myCollection = dbConn.Table<RestaurantDB>().ToList<RestaurantDB>().OrderByDescending(x => x.ViewTime).ToList<RestaurantDB>();
                ObservableCollection<RestaurantDB> RestaurantList = new ObservableCollection<RestaurantDB>(myCollection);
                return RestaurantList;
            }
        }

        // Retrieve the all contact list from the database. 
        public ObservableCollection<RestaurantDB> ReadFavoriteRestaurant()
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                List<RestaurantDB> myCollection = dbConn.Table<RestaurantDB>().ToList<RestaurantDB>().FindAll(x => x.Bookmarked.Equals(true)).OrderByDescending(x => x.ViewTime).ToList<RestaurantDB>();
                myCollection.Reverse();
                ObservableCollection<RestaurantDB> RestaurantList = new ObservableCollection<RestaurantDB>(myCollection);
                return RestaurantList;
            }
        }

        //Update existing conatct 
        public void UpdateRestaurant(RestaurantDB restaurant)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var existingRestaurant = dbConn.Query<RestaurantDB>("select * from RestaurantDB where Id =" + restaurant.Id).FirstOrDefault();
                if (existingRestaurant != null)
                {
                    existingRestaurant.ViewTime = restaurant.ViewTime;
                    existingRestaurant.Bookmarked = restaurant.Bookmarked;
                    dbConn.RunInTransaction(() =>
                    {
                        dbConn.Update(existingRestaurant);
                    });
                }
            }
        }
        // Insert the new contact in the Contacts table. 
        public void Insert(RestaurantDB restaurant)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var existingRestaurant = dbConn.Query<RestaurantDB>("select * from RestaurantDB where Id =" + restaurant.Id).FirstOrDefault();
                if (existingRestaurant == null)
                {
                    dbConn.RunInTransaction(() =>
                    {
                        dbConn.Insert(restaurant);
                    });
                }
                else
                {
                    UpdateRestaurant(restaurant);
                }
            }
        }

        //Delete specific contact 
        public void DeleteRestaurant(int Id)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var existingRestaurant = dbConn.Query<RestaurantDB>("select * from RestaurantDB where Id =" + Id).FirstOrDefault();
                if (existingRestaurant != null)
                {
                    dbConn.RunInTransaction(() =>
                    {
                        dbConn.Delete(existingRestaurant);
                    });
                }
            }
        }

        public void DeleteAllRestaurants()
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                dbConn.DropTable<RestaurantDB>();
                dbConn.CreateTable<RestaurantDB>();
                dbConn.Dispose();
                dbConn.Close();
            }
        }

        public void DeleteAllBookmarks()
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                List<RestaurantDB> FavoriteRestaurants = dbConn.Query<RestaurantDB>("select * from RestaurantDB where Bookmarked = 1" ).ToList<RestaurantDB>();
                if (FavoriteRestaurants.Count != 0)
                {
                    foreach (RestaurantDB restaurantDB in FavoriteRestaurants)
                    {
                        restaurantDB.Bookmarked = false;
                        UpdateRestaurant(restaurantDB);
                    }
                }
            }
        } 
    }
}
