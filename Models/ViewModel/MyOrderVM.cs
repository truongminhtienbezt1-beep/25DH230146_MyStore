using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _25DH230146_MyStore.Models;

namespace _25DH230146_MyStore.Models.ViewModel
{
    public class MyOrderVM
    {
        public string SearchTerm { get; set; }
        public string CurrentTab { get; set; }
        public string ExpandedStatus { get; set; }
        public List<Order> Orders { get; set; }
    }
}