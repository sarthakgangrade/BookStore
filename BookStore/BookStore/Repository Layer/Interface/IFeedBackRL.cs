using CommonLayer.Models.FeedBack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IFeedBackRL
    {
        public string AddFeedback(FeedBackModel feedback);
        public List<GetFeedBackModel> FeedBackDetails(int BookId);
    }
}
