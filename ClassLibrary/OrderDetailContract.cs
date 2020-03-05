﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class OrderDetailContract
    {
        public int odetailid { get; set; }
        public int orderid { get; set; }
        public int userid { get; set; }
        public int pid { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
        public DateTime date { get; set; }
        public string pimage { get; set; }
        public string pname { get; set; }
        public string brand { get; set; }
        public string comment { get; set; }
    }
}