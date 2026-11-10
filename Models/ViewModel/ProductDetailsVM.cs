using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace _25DH230146_MyStore.Models.ViewModel
{
    public class ProductDetailsVM
    {
        public Product product { get; set; } // Thông tin sản phẩm hiện tại
        public int quantity { get; set; } = 1; // Số lượng mặc định là 1
        public decimal estimatedValue =>
            quantity * (product != null ? product.ProductPrice : 0); // Giá trị tạm tính

        // Các thuộc tính hỗ trợ phân trang cho sản phẩm bán chạy
        public int PageNumber { get; set; } // Trang hiện tại
        public int PageSize { get; set; } = 3; // Số sản phẩm mỗi trang

        // Danh sách 8 sản phẩm cùng danh mục
        public List<Product> RelatedProducts { get; set; }

        // Danh sách sản phẩm bán chạy cùng danh mục (đã phân trang)
        public IPagedList<Product> TopProducts { get; set; }
    }
}