using System;
using System.Collections.Generic;
using System.Text;

namespace UserModel
{
    public class OrderModel
    {
        public int OrderId { get; set;}
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public DateTime Date { get; set; }
        public int Price { get; set; }
        public int BookQuantity { get; set;}
        public BookModel BookReference { get; set; }
    }
}
