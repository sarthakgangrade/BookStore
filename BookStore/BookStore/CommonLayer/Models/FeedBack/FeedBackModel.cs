using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models.FeedBack
{
    public class FeedBackModel
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
		public string Comments { get; set; }
		public int Ratings { get; set; }
        //public RegisterModel User { get; set; }
    }
}
