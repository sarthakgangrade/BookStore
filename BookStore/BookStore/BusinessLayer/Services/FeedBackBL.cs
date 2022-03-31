using BusinessLayer.Interface;
using CommonLayer.Models.FeedBack;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class FeedBackBL : IFeedBackBL
    {
        IFeedBackRL feedbackRL;
        public FeedBackBL(IFeedBackRL feedbackRL)
        {
            this.feedbackRL = feedbackRL;
        }

        public string AddFeedback(FeedBackModel feedback)
        {
            try
            {
                return this.feedbackRL.AddFeedback(feedback);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<GetFeedBackModel> FeedBackDetails(int BookId)
        {
            try
            {
                return this.feedbackRL.FeedBackDetails(BookId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
   }
}
