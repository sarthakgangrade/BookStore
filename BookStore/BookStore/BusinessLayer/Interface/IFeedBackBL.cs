using CommonLayer.Models.FeedBack;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IFeedBackBL
    {
        public string AddFeedback(FeedBackModel feedback);
        public List<GetFeedBackModel> FeedBackDetails(int BookId);
    }
}
