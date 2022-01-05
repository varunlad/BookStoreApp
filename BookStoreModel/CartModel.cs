using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UserModel
{
    public class CartModel
    {
        [Key]
        [Required]
        public int cartId { get; set; }
        [Required]
        public int userId { get; set; }
        [Required]
        public int bookId { get; set; }
        [Required]
        public int bookQuantity { get; set; }
        public BookModel RefModel1 { get; set; }
    }
}
