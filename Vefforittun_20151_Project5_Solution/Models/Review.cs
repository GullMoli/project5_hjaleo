using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vefforittun_20151_Project5_Solution.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}