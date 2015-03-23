using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vefforittun_20151_Project5_Solution.Models
{
    public class MovieAppRepository
    {

        private static MovieAppRepository instance;

        public static MovieAppRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new MovieAppRepository();
                return instance;
            }
        }

        private List<Movie> movies = null;
        private List<Review> reviews = null;
        private List<Rating> ratings = null;

        private MovieAppRepository()
        {
            InitializeContent();
        }

        public IEnumerable<Movie> GetAllMovies(string username)
        {
            IEnumerable<Movie> result = from movie in movies
                                        orderby movie.ReleaseDate descending
                                        select movie;

            foreach(Movie item in result)
            {
                item.Reviews = (from review in reviews
                                where review.MovieId == item.Id
                                orderby review.CreatedDate ascending
                                select review).ToList();

                item.Ratings = (from rating in ratings
                                where rating.MovieId == item.Id
                                orderby rating.CreatedDate ascending
                                select rating).ToList();

                item.CurrentUserRating = (from rating in ratings
                                          where rating.MovieId == item.Id && rating.Username == username
                                          select rating).SingleOrDefault();
                double count = item.Ratings.Count();
                item.RatingOverall = Math.Round(count > 0 ? item.Ratings.Sum(x => x.rating) / count : 0, 1);
            }

            return result;
        }

        public Movie GetMovieById(string username, int movieId)
        {

            Movie result = (from movie in movies
                          where movie.Id == movieId
                          select movie).SingleOrDefault();
            if (result != null)
            {
                result.Reviews = (from review in reviews
                                  where review.MovieId == result.Id
                                orderby review.CreatedDate ascending
                                select review).ToList();

                result.Ratings = (from rating in ratings
                                  where rating.MovieId == result.Id
                                orderby rating.CreatedDate ascending
                                select rating).ToList();

                result.CurrentUserRating = (from rating in ratings
                                            where rating.MovieId == result.Id && rating.Username == username
                                          select rating).SingleOrDefault();

                double count = result.Ratings.Count();
                result.RatingOverall = Math.Round(count > 0 ? result.Ratings.Sum(x => x.rating) / count : 0, 1);
            }
            return result;
        }

        public void AddReview(Review review)
        {
            int newID = 1;
            if (reviews.Count() > 1)
            {
                newID = reviews.Max(x => x.Id) + 1;
            }
            review.Id = newID;
            review.CreatedDate = DateTime.Now;
            reviews.Add(review);
        }

        public void AddRating(Rating rating)
        {
            int newID = 1;
            if (ratings.Count() > 1)
            {
                newID = reviews.Max(x => x.Id) + 1;
            }
            rating.Id = newID;
            rating.CreatedDate = DateTime.Now;
            ratings.Add(rating);
        }

        public void UpdateRating(Rating rating)
        {
            Rating old = ratings.Where(x => x.Id == rating.Id).SingleOrDefault();
            if(old != null)
            {
                ratings.Remove(old);
                ratings.Add(rating);
            }
        }

        public IEnumerable<Rating> GetRatingsByID(int movieId)
        {
            IEnumerable<Rating> result = from rating in ratings
                                         where rating.MovieId == movieId
                                         orderby rating.CreatedDate ascending
                                         select rating;
            return result;
        }

        public Rating CheckRating(int movieId, string username)
        {
            Rating result = (from rating in ratings
                          where rating.MovieId == movieId && rating.Username == username
                          select rating).FirstOrDefault();
            return result;
        }

        private void InitializeContent()
        {
            //Initialize some movies
            this.movies = new List<Movie>();
            Movie movie1 = new Movie
            {
                Id = 1,
                Title = "The Hobbit: The Battle of the Five Armies",
                DescriptionShort = "Bilbo and Company are forced to engage in a war against an array of combatants and keep the Lonely Mountain from falling into the hands of a rising darkness.",
                DescriptionLong = "Bilbo and Company are forced to engage in a war against an array of combatants and keep the Lonely Mountain from falling into the hands of a rising darkness.",
                Categories = new List<string> { " Adventure", "Fantasy " },
                RatingOverall = 0,
                Length = 144,
                ReleaseDate = new DateTime(2014, 12, 17),
                ThumbnailUrlSmall = "~/Content/Images/movie" + 1 + ".jpg",
                ThumbnailUrlLarge = "~/Content/Images/movie" + 1 + ".jpg",
                VideoLInk = "ZSzeFFsKEt4",
                Directors = new List<string> { "Peter Jackson" },
                Writers = new List<string> { "Fran Walsh", "Philippa Boyens", "Peter Jackson", "Guillermo del Toro", "J.R.R. Tolkien" },
                Actors = new List<string> { "Ian McKellen", "Martin Freeman", "Richard Armitage" },
                Reviews = new List<Review>(),
                Ratings = new List<Rating>(),
                CurrentUserRating = null
            };
            this.movies.Add(movie1);

            Movie movie2 = new Movie
            {
                Id = 2,
                Title = "Star Wars: Episode VII - The Force Awakens",
                DescriptionShort = "A continuation of the saga created by George Lucas set thirty years after Star Wars: Episode VI - Return of the Jedi (1983).",
                DescriptionLong = "A continuation of the saga created by George Lucas set thirty years after Star Wars: Episode VI - Return of the Jedi (1983).",
                Categories = new List<string> { "Action", "Adventure", "Fantasy" },
                RatingOverall = 0,
                Length = 130,
                ReleaseDate = new DateTime(2015, 12, 18),
                ThumbnailUrlSmall = "~/Content/Images/movie" + 2 + ".jpg",
                ThumbnailUrlLarge = "~/Content/Images/movie" + 2 + ".jpg",
                VideoLInk = "OMOVFvcNfvE",
                Directors = new List<string> { "J.J. Abrams" },
                Writers = new List<string> { "J.J. Abrams", "Lawrence Kasdan", "George Lucas" },
                Actors = new List<string> { "Harrison Ford", "Mark Hamill", "Carrie Fisher" },
                Reviews = new List<Review>(),
                Ratings = new List<Rating>(),
                CurrentUserRating = null
            };
            this.movies.Add(movie2);

            Movie movie3 = new Movie
            {
                Id = 3,
                Title = "American Sniper",
                DescriptionShort = "Navy SEAL sniper Chris Kyle's pinpoint accuracy saves countless lives on the battlefield and turns him into a legend. Back home to his wife and kids after four tours of duty, however, Chris finds that it is the war he can't leave behind.",
                DescriptionLong = "Navy SEAL sniper Chris Kyle's pinpoint accuracy saves countless lives on the battlefield and turns him into a legend. Back home to his wife and kids after four tours of duty, however, Chris finds that it is the war he can't leave behind.",
                Categories = new List<string> { "Action", "Biography" },
                RatingOverall = 0,
                Length = 132,
                ReleaseDate = new DateTime(2015, 1, 16),
                ThumbnailUrlSmall = "~/Content/Images/movie" + 3 + ".jpg",
                ThumbnailUrlLarge = "~/Content/Images/movie" + 3 + ".jpg",
                VideoLInk = "99k3u9ay1gs",
                Directors = new List<string> { "Clint Eastwood" },
                Writers = new List<string> { "Jason Hall", "Chris Kyle", "Scott McEwen", "James Defelice" },
                Actors = new List<string> { "Bradley Cooper", "Sienna Miller", "Kyle Gallner" },
                Reviews = new List<Review>(),
                Ratings = new List<Rating>(),
                CurrentUserRating = null
            };
            this.movies.Add(movie3);

            Movie movie4 = new Movie
            {
                Id = 4,
                Title = "Cinderella",
                DescriptionShort = "A live-action retelling of the classic fairy tale about a servant step-daughter who wins the heart of a prince.",
                DescriptionLong = "A live-action retelling of the classic fairy tale about a servant step-daughter who wins the heart of a prince.",
                Categories = new List<string> { "Adventure", "Drama", "Family" },
                RatingOverall = 0,
                Length = 125,
                ReleaseDate = new DateTime(2015, 3, 13),
                ThumbnailUrlSmall = "~/Content/Images/movie" + 4 + ".jpg",
                ThumbnailUrlLarge = "~/Content/Images/movie" + 4 + ".jpg",
                VideoLInk = "McQ_cCBaiac",
                Directors = new List<string> { "Kenneth Branagh" },
                Writers = new List<string> { "Chris Weitz" },
                Actors = new List<string> { "Lily James", "Hayley Atwell", "Helena Bonham Carter" },
                Reviews = new List<Review>(),
                Ratings = new List<Rating>(),
                CurrentUserRating = null
            };
            this.movies.Add(movie4);

            Movie movie5 = new Movie
            {
                Id = 5,
                Title = "The Shawshank Redemption",
                DescriptionShort = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                DescriptionLong = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                Categories = new List<string> { "Crime", "Drama" },
                RatingOverall = 0,
                Length = 142,
                ReleaseDate = new DateTime(1994, 10, 14),
                ThumbnailUrlSmall = "~/Content/Images/movie" + 5 + ".jpg",
                ThumbnailUrlLarge = "~/Content/Images/movie" + 5 + ".jpg",
                VideoLInk = "6hB3S9bIaco",
                Directors = new List<string> { "Frank Darabont" },
                Writers = new List<string> { "Stephen King", "Frank Darabont" },
                Actors = new List<string> { "Tim Robbins", "Morgan Freeman", "Bob Gunton" },
                Reviews = new List<Review>(),
                Ratings = new List<Rating>(),
                CurrentUserRating = null
            };
            this.movies.Add(movie5);

            //Initialize some reviews
            this.reviews = new List<Review>();
            Review review1 = new Review
            {
                Id = 1,
                MovieId = 1,
                Username = "Haukur",
                Text = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat. Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat.",
                CreatedDate = new DateTime(2014, 12, 20)
            };
            this.reviews.Add(review1);
            review1 = new Review
            {
                Id = 2,
                MovieId = 2,
                Username = "Haukur",
                Text = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat. Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat.",
                CreatedDate = new DateTime(2014, 12, 20)
            };
            this.reviews.Add(review1);

            Review review2 = new Review
            {
                Id = 3,
                MovieId = 1,
                Username = "Victor",
                Text = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat. Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat.",
                CreatedDate = new DateTime(2014, 12, 20)
            };
            this.reviews.Add(review2);
            review2 = new Review
            {
                Id = 4,
                MovieId = 2,
                Username = "Victor",
                Text = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat. Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat.",
                CreatedDate = new DateTime(2014, 12, 20)
            };
            this.reviews.Add(review2);

            Review review3 = new Review
            {
                Id = 5,
                MovieId = 1,
                Username = "Leifur",
                Text = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat. Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat.",
                CreatedDate = new DateTime(2014, 12, 20)
            };
            this.reviews.Add(review3);
            review3 = new Review
            {
                Id = 6,
                MovieId = 2,
                Username = "Leifur",
                Text = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat. Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat.",
                CreatedDate = new DateTime(2014, 12, 20)
            };
            this.reviews.Add(review3);

            //Initialize some ratings
            this.ratings = new List<Rating>();
            Rating rating1 = new Rating
            {
                Id = 1,
                MovieId = 1,
                Username = "Haukur",
                rating = 5,
                CreatedDate = new DateTime(2014, 12, 20)

            };
            this.ratings.Add(rating1);
            rating1 = new Rating
            {
                Id = 2,
                MovieId = 2,
                Username = "Haukur",
                rating = 8,
                CreatedDate = new DateTime(2014, 12, 20)

            };
            this.ratings.Add(rating1);

            Rating rating2 = new Rating
            {
                Id = 3,
                MovieId = 1,
                Username = "Victor",
                rating = 8,
                CreatedDate = new DateTime(2014, 12, 21)

            };
            this.ratings.Add(rating2);
            rating2 = new Rating
            {
                Id = 4,
                MovieId = 2,
                Username = "Victor",
                rating = 9,
                CreatedDate = new DateTime(2014, 12, 21)

            };
            this.ratings.Add(rating2);

            Rating rating3 = new Rating
            {
                Id = 5,
                MovieId = 1,
                Username = "Leifur",
                rating = 7,
                CreatedDate = new DateTime(2014, 12, 22)

            };
            this.ratings.Add(rating3);
            rating3 = new Rating
            {
                Id = 6,
                MovieId = 2,
                Username = "Leifur",
                rating = 9,
                CreatedDate = new DateTime(2014, 12, 22)

            };
            this.ratings.Add(rating3);
        }
    }
}