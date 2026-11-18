using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _25DH230146_MyStore.Models.ViewModel
{
    public class CategoryStatItem
    {
        public string CategoryName { get; set; }
        public int ProductCount { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal AvgPrice { get; set; }
    }

    public class StatisticsVM
    {
        public int TotalProducts { get; set; }
        public int TotalCategories { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<CategoryStatItem> CategoryStats { get; set; }
    }
}