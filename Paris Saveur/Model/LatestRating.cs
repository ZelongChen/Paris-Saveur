using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paris_Saveur.Model
{
    class LatestRating : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int pk { get; set; }
        private User _user;
        public User user 
        {
            get { return _user; }
            set 
            {
                _user = value;
                NotifyPropertyChanged("user");
            }
        }
        public string username { get; set; }
        public int score { get; set; }
        public int consumption { get; set; }
        public string comment { get; set; }
        public string rate_date { get; set; }
        public bool is_suspicious { get; set; }
        public int num_agreed { get; set; }
        public int num_disagreed { get; set; }
        public int popularity { get; set; }

        public void convertDateToChinese()
        {
            rate_date = rate_date.Substring(0, 4) + "年" + rate_date.Substring(5, 2) + "月" + rate_date.Substring(8, 2) + "日";
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
