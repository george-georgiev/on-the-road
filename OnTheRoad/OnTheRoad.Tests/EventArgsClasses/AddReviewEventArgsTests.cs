using System;
using NUnit.Framework;
using OnTheRoad.Domain.Enumerations;
using OnTheRoad.Mvp.EventArgsClasses;

namespace OnTheRoad.Tests.EventArgsClasses
{
    [TestFixture]
    public class AddReviewEventArgsTests
    {
        [Test]
        public void WhenAddReviewEventArgsIsInitializes_InstanceShouldBeReturned()
        {
            var actualInstance = new AddReviewEventArgs();

            Assert.That(actualInstance, Is.InstanceOf<AddReviewEventArgs>());
        }

        [Test]
        public void VerifyThatFromUserCanBeGettedSetted()
        {
            var addReviewEventArgs = new AddReviewEventArgs();
            var user = "user";

            addReviewEventArgs.FromUser = user;

            Assert.That(addReviewEventArgs.FromUser.Equals(user));
        }

        [Test]
        public void VerifyThatFromUserReturnsInstanceOfString()
        {
            var addReviewEventArgs = new AddReviewEventArgs();
            var user = "user";

            addReviewEventArgs.FromUser = user;

            Assert.That(addReviewEventArgs.FromUser, Is.InstanceOf<string>());
        }

        [Test]
        public void VerifyThatToUserCanBeGettedSetted()
        {
            var addReviewEventArgs = new AddReviewEventArgs();
            var user = "user";

            addReviewEventArgs.ToUser = user;

            Assert.That(addReviewEventArgs.ToUser.Equals(user));
        }

        [Test]
        public void VerifyThatToUserReturnsInstanceOfString()
        {
            var addReviewEventArgs = new AddReviewEventArgs();
            var user = "user";

            addReviewEventArgs.ToUser = user;

            Assert.That(addReviewEventArgs.ToUser, Is.InstanceOf<string>());
        }

        [Test]
        public void VerifyThatRatingCanBeGettedSetted()
        {
            var addReviewEventArgs = new AddReviewEventArgs();
            var rating = RatingEnum.Neutral;

            addReviewEventArgs.Rating = rating;

            Assert.That(addReviewEventArgs.Rating.Equals(rating));
        }

        [Test]
        public void VerifyThatRatingReturnsInstanceOfRatingEnum()
        {
            var addReviewEventArgs = new AddReviewEventArgs();
            var rating = RatingEnum.Neutral;

            addReviewEventArgs.Rating = rating;

            Assert.That(addReviewEventArgs.Rating, Is.InstanceOf<RatingEnum>());
        }

        [Test]
        public void VerifyThatContentCanBeGettedSetted()
        {
            var addReviewEventArgs = new AddReviewEventArgs();
            var content = "content";

            addReviewEventArgs.Content = content;

            Assert.That(addReviewEventArgs.Content.Equals(content));
        }

        [Test]
        public void VerifyThatContentReturnsInstanceOfString()
        {
            var addReviewEventArgs = new AddReviewEventArgs();
            var content = "content";

            addReviewEventArgs.Content = content;

            Assert.That(addReviewEventArgs.Content, Is.InstanceOf<string>());
        }

        [Test]
        public void VerifyThatPostingDateCanBeGettedSetted()
        {
            var addReviewEventArgs = new AddReviewEventArgs();
            var postingDate = DateTime.Now;

            addReviewEventArgs.PostingDate = postingDate;

            Assert.That(addReviewEventArgs.PostingDate.Equals(postingDate));
        }

        [Test]
        public void VerifyThatPostingDateReturnsInstanceOfDateTime()
        {
            var addReviewEventArgs = new AddReviewEventArgs();
            var postingDate = DateTime.Now;

            addReviewEventArgs.PostingDate = postingDate;

            Assert.That(addReviewEventArgs.PostingDate, Is.InstanceOf<DateTime>());
        }
    }
}