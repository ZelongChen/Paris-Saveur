using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paris_Saveur.Model
{
    class RestaurantList
    {
        private ObservableCollection<Restaurant> restaurant_list = new ObservableCollection<Restaurant>();
        public ObservableCollection<Restaurant> Restaurant_list
        {
            get { return restaurant_list; }
            set
            {
                restaurant_list = value;
            }
        }
        
    }
}
