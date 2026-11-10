using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace _25DH230146_MyStore.Models.ViewModel
{
    public class HomeProductVM
    {
        // Tiêu chí tìm kiếm theo tên, mô tả hoặc danh mục
        public string SearchTerm { get; set; }

        // Các thuộc tính hỗ trợ phân trang cho sản phẩm mới
        public int PageNumber { get; set; } // Trang hiện tại
        public int PageSize { get; set; } = 6; // Số sản phẩm mỗi trang

        // Danh sách sản phẩm nổi bật (Top 10 bán chạy)
        public List<Product> FeaturedProducts { get; set; }

        // Danh sách sản phẩm mới đã phân trang
        public IPagedList<Product> NewProducts { get; set; }
    }
}