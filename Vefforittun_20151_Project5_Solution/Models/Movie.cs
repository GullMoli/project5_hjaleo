using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vefforittun_20151_Project5_Solution.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string DescriptionShort { get; set; }
        public string DescriptionLong { get; set; }
        public List<string> Categories { get; set; }
        public double RatingOverall { get; set; }
        
        public int Length { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ThumbnailUrlSmall { get; set; }
        public string ThumbnailUrlLarge { get; set; }
        public string VideoLInk { get; set; }

        public List<string> Directors { get; set; }
        public List<string> Writers { get; set; }
        public List<string> Actors { get; set; }
        
        public List<Review> Reviews { get; set; }
        public List<Rating> Ratings { get; set; }

        public Rating CurrentUserRating { get; set; }
    }
}