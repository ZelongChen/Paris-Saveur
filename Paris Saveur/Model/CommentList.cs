using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paris_Saveur.Model
{
    class CommentList
    {
        private ObservableCollection<LatestRating> comments = new ObservableCollection<LatestRating>();
        public ObservableCollection<LatestRating> Comments
        {
            get { return comments; }
            set
            {
                comments = value;
            }
        } 
    }
}
