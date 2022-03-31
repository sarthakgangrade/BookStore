using BusinessLayer.Interface;
using CommonLayer.Models.FeedBack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        IFeedBackBL feedbackBL;
        public FeedBackController(IFeedBackBL feedbackBL)
        {
            this.feedbackBL = feedbackBL;
        }
        [HttpPost]
        [Route("addFeedbacks")]
        public IActionResult AddFeedback(FeedBackModel feedback)
        {
            try
            {
                string result = this.feedbackBL.AddFeedback(feedback);
                if (result.Equals("Feedback added successfully"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [HttpGet]
        [Route("getFeedbacks/{BookId}")]
        public IActionResult FeedBackDetails(int BookId)
        {
            try
            {
                var result = this.feedbackBL.FeedBackDetails(BookId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "FeedBack shown  ", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Uanble to show Details" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
    }
}
