using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using OnTheRoad.Data.Models;

namespace OnTheRoad.Admin
{
    public class ManageUserReviews
    {
        public static IEnumerable<Review> GetUserReviews(string userId)
        {
            var reviews = new List<Review>();
            ConnectionStringSettings dBConnectionString = ConfigurationManager.ConnectionStrings["OnTheRoadDB"];
            SqlConnection dbCon = new SqlConnection(dBConnectionString.ConnectionString);
            dbCon.Open();

            SqlCommand cmd = new SqlCommand("SELECT r.Id, u.UserName AS [To User], uu.UserName AS [From User], r.ReviewContent, ra.Value AS [Rating], r.PostingDate, r.IsDeleted " +
                                            "FROM Reviews r " +
                                            "INNER JOIN Users u " +
                                            "ON r.ToUserId = u.Id " +
                                            "INNER JOIN Users uu " +
                                            "ON r.FromUserId = uu.Id " +
                                            "INNER JOIN Ratings ra " +
                                            "ON ra.Id = r.RatingId " +
                                            "WHERE u.Id = @userId", dbCon);
            cmd.Parameters.AddWithValue("@userId", userId);

            SqlDataReader reader = cmd.ExecuteReader();
            using (reader)
            {
                while (reader.Read())
                {
                    var review = new Review();
                    review.Id = Convert.ToInt32(reader["Id"]);
                    review.ReviewContent = reader["ReviewContent"].ToString();
                    review.Rating = new Rating();
                    review.Rating.Value = reader["Rating"].ToString();
                    review.FromUser = new User();
                    review.FromUser.UserName = reader["From User"].ToString();
                    review.ToUser = new User();
                    review.ToUser.UserName = reader["To User"].ToString();
                    review.IsDeleted = (bool)reader["IsDeleted"];
                    review.PostingDate = (DateTime)reader["PostingDate"];
                    reviews.Add(review);
                }
            }

            dbCon.Close();

            return reviews;
        }

        public static void UpdateUserReviews(string IsDeleted, string ReviewId)
        {
            ConnectionStringSettings dBConnectionString = ConfigurationManager.ConnectionStrings["OnTheRoadDB"];
            SqlConnection dbCon = new SqlConnection(dBConnectionString.ConnectionString);
            dbCon.Open();

            SqlCommand cmd = new SqlCommand("UPDATE Reviews " +
                                            "SET IsDeleted = @isDeleted " +
                                            "WHERE Id = @reviewId", dbCon);
            cmd.Parameters.AddWithValue("@isDeleted", IsDeleted);
            cmd.Parameters.AddWithValue("@reviewId", Convert.ToInt32(ReviewId));
            cmd.ExecuteNonQuery();

            dbCon.Close();
        }
    }
}