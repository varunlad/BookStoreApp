using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UserModel
{
    public class BookModel
    {
        [Key]
        [Required]
        public int bookId { get; set; }
        [Required]
        public string bookName { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int discountPrice { get; set; }
        public string Detail { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public string Review { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public int Quantity { get; set; }

    }
}
