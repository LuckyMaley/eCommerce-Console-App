using MainCode.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Repository
{
    public class ReviewsRepository
    {
        private static List<Review> allReviews;

        public ReviewsRepository()
        {
            allReviews = ReadGetAllRows();
        }

        public virtual List<Review> ReadGetAllRows()
        {
            allReviews = new List<Review>();
            foreach (var rev in Review.ReviewsDataSet)
            {
                allReviews.Add(new Review()
                {
                    ReviewID = rev.ReviewID,
                    ProductID = rev.ProductID,
                    CustomerID = rev.CustomerID,
                    Rating = rev.Rating,
                    Title = rev.Title,
                    Comment = rev.Comment,
                    ReviewDate = rev.ReviewDate
                });
            }
            return allReviews;
        }

        public List<Review> ReadRowByRating(int rating)
        {
            ReviewsRepository repository = new ReviewsRepository();
            List<Review> allOfTheReviews = repository.ReadGetAllRows();
            IEnumerable<Review> review = allOfTheReviews.Where(c => c.Rating == rating);
            List<Review> reviews = new List<Review>();
            if(review != null)
            {
                reviews = review.ToList();
            }
            else
            {
                reviews = null;
            }
            return reviews;
        }

        public List<Review> ReadRowByDate(string date)
        {
            ReviewsRepository repository = new ReviewsRepository();
            List<Review> allOfTheReviews = repository.ReadGetAllRows();

            string defaultDateString = "10/08/2008";
            string format = "dd/MM/yyyy";
            CultureInfo ci = new CultureInfo("en-za");

            DateTime dateTime;
            if (!DateTime.TryParseExact(date, format, ci, System.Globalization.DateTimeStyles.None, out dateTime))
            {
                dateTime = DateTime.ParseExact(defaultDateString, format, ci);
            }
            IEnumerable<Review> review = allOfTheReviews.Where(c => c.ReviewDate.Date == dateTime.Date);
            List<Review> reviews = new List<Review>();
            if (review != null)
            {
                reviews = review.ToList();
            }
            else
            {
                reviews = null;
            }
            return reviews;
        }

        public List<Review> ReadRowByDate(string beginDate, string endDate)
        {
            ReviewsRepository repository = new ReviewsRepository();
            List<Review> allOfTheReviews = repository.ReadGetAllRows();


            string defaultDateString = "10/08/2008";
            string format = "dd/MM/yyyy";
            CultureInfo ci = new CultureInfo("en-za");

            DateTime beginDateTime;
            if (!DateTime.TryParseExact(beginDate, format, ci, System.Globalization.DateTimeStyles.None, out beginDateTime))
            {
                beginDateTime = DateTime.ParseExact(defaultDateString, format, ci);
            }

            DateTime endDateTime;
            if (!DateTime.TryParseExact(endDate, format, ci, System.Globalization.DateTimeStyles.None, out endDateTime))
            {
                endDateTime = DateTime.ParseExact(defaultDateString, format, ci);
            }
            IEnumerable<Review> review = allOfTheReviews.Where(c => c.ReviewDate.Date >= beginDateTime.Date && c.ReviewDate.Date <= endDateTime.Date);
            List<Review> reviews = new List<Review>();
            if (review != null)
            {
                reviews = review.ToList();
            }
            else
            {
                reviews = null;
            }
            return reviews;
        }
    }
}
