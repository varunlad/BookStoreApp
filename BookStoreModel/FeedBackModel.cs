using System;
using System.Collections.Generic;
using System.Text;

namespace UserModel
{
    public class FeedBackModel
    {
        public int FeedBackId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public string Comments { get; set; }
        public int Ratings { get; set; }
        public SignupModel UserReference { get; set; }
    }
}
