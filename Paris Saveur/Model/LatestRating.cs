﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paris_Saveur.Model
{
    class LatestRating
    {
        public int pk { get; set; }
        public User user { get; set; }
        public int score { get; set; }
        public int consumption { get; set; }
        public string comment { get; set; }
        public string rate_date { get; set; }
        public bool is_suspicious { get; set; }
        public int num_agreed { get; set; }
        public int num_disagreed { get; set; }
        public int popularity { get; set; }
        public string reply { get; set; }

        public void convertDateToChinese()
        {
            rate_date = rate_date.Substring(0, 4) + "年" + rate_date.Substring(5, 2) + "月" + rate_date.Substring(8, 2) + "日";
        }
    }
}
