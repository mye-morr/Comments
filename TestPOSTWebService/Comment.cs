using System;
using System.Web;

namespace TestPOSTWebService
{
    public class Comment
    {
        public String datComment { get; set; }
        public String vcCommentBy { get; set; }
        public String vcComment { get; set; }
        public String idClaim { get; set; }
        public String vcCategory { get; set; }
        public String decExpected { get; set; }
        public String decVariance { get; set; }
        public String vcClient { get; set; }
        public String vcInsurance { get; set; }
        public String vcContactName { get; set; }
        public String vcContactPhone { get; set; }
        public String vcContactEmail { get; set; }
        public String vcCallRefNo { get; set; }
        public String datFollowUp { get; set; }
        public String vcNotes { get; set; }
    }
}
