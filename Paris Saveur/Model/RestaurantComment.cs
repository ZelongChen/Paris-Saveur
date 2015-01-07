using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paris_Saveur.Model
{
    class RestaurantComment
    {
        public Restaurant restaurant { get; set; }
        public List<LatestRating> rating_list { get; set; }
    }
}
