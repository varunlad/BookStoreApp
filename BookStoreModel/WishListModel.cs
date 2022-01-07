using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UserModel
{
    public class WishListModel
    {
        [Key]
        public int WishListId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public BookModel BookModelRef { get; set; }
    }
}
