using MainCode.Models;
using MainCode.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Repository.AdminMenuOptions
{
    public class ReviewOptions
    {
        public string FindReviewbyRating(ReviewsRepository repository)
        {
            StringBuilder stringBuilder = new StringBuilder();
            List<Review> allOfTheReviews = repository.ReadGetAllRows();
            Console.WriteLine("Please enter the review rating (1 to 5): ");
            int rating = 0;
            bool result = int.TryParse(Console.ReadLine(), out rating);
            CultureInfo ci = new CultureInfo("en-za");
            if (result)
            {
                var reviews = repository.ReadRowByRating(rating);
                if (reviews != null)
                {
                    reviews.ForEach(b => stringBuilder.AppendLine($"ID: {b.ReviewID}, Rating: {b.Rating}, Title: {b.Title}, Comment: {b.Comment}, Review Date: {b.ReviewDate.ToString("dd MMMM yyyy HH:mm")}"));
                }
                else
                {
                    stringBuilder.AppendLine("no review exists for that rating, Please try again");
                }
            }
            else
            {
                stringBuilder.AppendLine("Please make sure that you insert a number for the rating");
            }
            return stringBuilder.ToString();
        }

        public string FindReviewbyDate(ReviewsRepository repository)
        {
            StringBuilder stringBuilder = new StringBuilder();
            List<Review> allOfTheReviews = repository.ReadGetAllRows();
            Console.WriteLine("Please type in the review date (dd/MM/yyy): ");
            string response = Console.ReadLine();
            if (response != "")
            {
                var reviews = repository.ReadRowByDate(response);
                if (reviews != null)
                {
                    reviews.ForEach(b => stringBuilder.AppendLine($"ID: {b.ReviewID}, Rating: {b.Rating}, Title: {b.Title}, Comment: {b.Comment}, Review Date: {b.ReviewDate.ToString("dd MMMM yyyy HH:mm")}"));
                }
                else
                {
                    stringBuilder.AppendLine("no review exists for that date, Please try again");
                }
            }
            else
            {
                stringBuilder.AppendLine("Please enter a review date");
            }
            return stringBuilder.ToString();
        }

        public string FindReviewbetweenDates(ReviewsRepository repository)
        {
            StringBuilder stringBuilder = new StringBuilder();
            List<Review> allOfTheReviews = repository.ReadGetAllRows();
            Console.WriteLine("Please type in the starting review date (dd/MM/yyy): ");
            string response = Console.ReadLine();

            Console.WriteLine("Please type in the ending review date (dd/MM/yyy): ");
            string responseTwo = Console.ReadLine();
            if (response != "" || responseTwo != "")
            {
                var reviews = repository.ReadRowByDate(response, responseTwo);
                if (reviews != null)
                {
                    reviews.ForEach(b => stringBuilder.AppendLine($"ID: {b.ReviewID}, Rating: {b.Rating}, Title: {b.Title}, Comment: {b.Comment}, Review Date: {b.ReviewDate.ToString("dd MMMM yyyy HH:mm")}"));
                }
                else
                {
                    stringBuilder.AppendLine("no review not exists for those dates, Please try again");
                }
            }
            else
            {
                stringBuilder.AppendLine("Please enter both review dates");
            }
            return stringBuilder.ToString();
        }
    }
}
