using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using NUnit.Framework;
using Moq;
using OnTheRoad.Data.Contracts;
using OnTheRoad.Data.Models;
using OnTheRoad.Data.Repositories;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Data.Tests.Repositories
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private Mock<IOnTheRoadDbContext> contextMock;
        private Mock<DbSet<User>> dbSetMock;

        [SetUp]
        public void SetUpMocks()
        {
            this.contextMock = new Mock<IOnTheRoadDbContext>();
            this.dbSetMock = new Mock<DbSet<User>>();
            contextMock.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);
        }

        [Test]
        public void UserRepository_WhenInitializedWithNullForContext_ShouldThrowNewArgumentExeption()
        {
            Assert.Throws<ArgumentNullException>(() => new UserRepository(null));
        }

        [Test]
        public void UserRepository_WhenInitializedWithNullForContext_ShouldThrowProperExeptionMessage()
        {
            var exc = Assert.Throws<ArgumentNullException>(() => new UserRepository(null));
            StringAssert.Contains("context cannot be null!", exc.Message);
        }

        [Test]
        public void GetById_WhenIsCalled_ShouldCallDbSetFindExactlyOnce()
        {
            var userId = "id";
            var userRepository = new UserRepository(this.contextMock.Object);
            var actual = userRepository.GetById(userId);

            this.dbSetMock.Verify(x => x.Find(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetById_WhenIsCalled_ShouldCallDbSetFindExactlyOnceWithTheRightParams()
        {
            var userId = "id";
            var userRepository = new UserRepository(this.contextMock.Object);
            var actual = userRepository.GetById(userId);

            this.dbSetMock.Verify(x => x.Find(userId), Times.Once);
        }

        [Test]
        public void GetById_WhenIsCalledWithoutExistingUser__ShouldReturnNull()
        {
            var userRepository = new UserRepository(this.contextMock.Object);
            var actual = userRepository.GetById("id");

            Assert.That(actual, Is.Null);
        }

        [Test]
        public void GetById_WhenIsCalled_ShouldReturnInstanceOfIUser()
        {
            var userMock = new Mock<User>();
            this.dbSetMock.Setup(x => x.Find(It.IsAny<string>())).Returns(userMock.Object);

            var userRepository = new UserRepository(this.contextMock.Object);
            var actual = userRepository.GetById("id");

            Assert.That(actual, Is.InstanceOf<IUser>());
        }

        [TestCase("Sub-Zero", "Sub", "Zero", "Freeze", "sub@zero", "Varna", 5, "12345", 3, 1, 4)]
        [TestCase("Scorpion", "Scorpion", "Scorpion", "Fire", "scorpion@abv", "Plovdiv", 1, "12341", 4, 5, 0)]
        [Test]
        public void GetById_WhenIsCalled_ShouldReturnDomainEntityWithCorrectProperties(
            string username,
            string fName,
            string lName,
            string info,
            string email,
            string cityName,
            int cityId,
            string phoneNumber,
            int favUserCount,
            int givenReviewsCount,
            int receivedReviewsCount)
        {
            var contextMock = new Mock<OnTheRoadIdentityDbContext>();
            var dbSetMock = new Mock<DbSet<User>>();
            var userMock = new Mock<User>();
            var cityMock = new Mock<City>();
            var cityStub = new City() { Name = cityName, Id = cityId };
            var userStub = new User()
            {
                UserName = username,
                FirstName = fName,
                LastName = lName,
                Email = email,
                Info = info,
                City = cityStub,
                CityId = cityId,
                PhoneNumber = phoneNumber
            };

            this.AddFavouriteUsers(userStub, favUserCount);
            this.AddGivenReviews(userStub, givenReviewsCount);
            this.AddReceivedRevies(userStub, receivedReviewsCount);
            this.dbSetMock.Setup(x => x.Find(It.IsAny<string>())).Returns(userStub);

            var userRepository = new UserRepository(this.contextMock.Object);
            var actual = userRepository.GetById("notImportant");

            Assert.That(actual.Username.Equals(username));
            Assert.That(actual.FirstName.Equals(fName));
            Assert.That(actual.LastName.Equals(lName));
            Assert.That(actual.Email.Equals(email));
            Assert.That(actual.Info.Equals(info));
            Assert.That(actual.PhoneNumber.Equals(phoneNumber));
            Assert.That(actual.City.Name.Equals(cityName));
            Assert.That(actual.City.Id.Equals(cityId));
            Assert.That(actual.FavouriteUsers.Count.Equals(userStub.FavouriteUsers.Count));
            Assert.That(actual.GivenReviews.Count.Equals(userStub.GivenReviews.Count));
            Assert.That(actual.ReceivedReviews.Count.Equals(userStub.ReceivedReviews.Count));
        }

        [Test]
        public void GetByUserName_WhenIsCalledWithNonExistingUser_ShouldReturnNull()
        {
            var userStub = new User();
            var fakeData = new List<User>() { userStub }.AsQueryable();
            this.SetDbSetUserAsQueryable(fakeData);

            var userRepository = new UserRepository(this.contextMock.Object);
            var actual = userRepository.GetByUserName("username");

            Assert.That(actual, Is.Null);
        }

        [Test]
        public void GetByUserName_WhenIsCalled_ShouldReturnInstanceOfIUser()
        {
            var username = "Sub-Zero";
            var userStub = new User() { UserName = username };
            var fakeData = new List<User>() { userStub }.AsQueryable();
            this.SetDbSetUserAsQueryable(fakeData);

            var userRepository = new UserRepository(this.contextMock.Object);
            var actual = userRepository.GetByUserName(username);

            Assert.That(actual, Is.InstanceOf<IUser>());
        }

        [TestCase("Sub-Zero", "Sub", "Zero", "Freeze", "sub@zero", "Varna", 5, "12345", 3, 1, 4)]
        [TestCase("Scorpion", "Scorpion", "Scorpion", "Fire", "scorpion@abv", "Plovdiv", 1, "12341", 4, 5, 0)]
        [Test]
        public void GetByUserName_WhenIsCalled_ShouldReturnDomainEntityWithCorrectProperties(
           string username,
           string fName,
           string lName,
           string info,
           string email,
           string cityName,
           int cityId,
           string phoneNumber,
           int favUserCount,
           int givenReviewsCount,
           int receivedReviewsCount)
        {
            var contextMock = new Mock<OnTheRoadIdentityDbContext>();
            var dbSetMock = new Mock<DbSet<User>>();
            var userMock = new Mock<User>();
            var cityMock = new Mock<City>();
            var cityStub = new City() { Name = cityName, Id = cityId };
            var userStub = new User()
            {
                UserName = username,
                FirstName = fName,
                LastName = lName,
                Email = email,
                Info = info,
                City = cityStub,
                CityId = cityId,
                PhoneNumber = phoneNumber
            };

            this.AddFavouriteUsers(userStub, favUserCount);
            this.AddGivenReviews(userStub, givenReviewsCount);
            this.AddReceivedRevies(userStub, receivedReviewsCount);

            var fakeData = new List<User>() { userStub }.AsQueryable();
            this.SetDbSetUserAsQueryable(fakeData);

            var userRepository = new UserRepository(this.contextMock.Object);
            var actual = userRepository.GetByUserName(username);

            Assert.That(actual.Username.Equals(username));
            Assert.That(actual.FirstName.Equals(fName));
            Assert.That(actual.LastName.Equals(lName));
            Assert.That(actual.Email.Equals(email));
            Assert.That(actual.Info.Equals(info));
            Assert.That(actual.PhoneNumber.Equals(phoneNumber));
            Assert.That(actual.City.Name.Equals(cityName));
            Assert.That(actual.City.Id.Equals(cityId));
            Assert.That(actual.FavouriteUsers.Count.Equals(userStub.FavouriteUsers.Count));
            Assert.That(actual.GivenReviews.Count.Equals(userStub.GivenReviews.Count));
            Assert.That(actual.ReceivedReviews.Count.Equals(userStub.ReceivedReviews.Count));
        }

        [Test]
        public void GetAllIs_WhenCalledAndThereAreNoUsersInTheDbSet_ShouldReturnZeroUsers()
        {
            var expected = 0;
            var fakeData = new List<User>().AsQueryable();
            this.SetDbSetUserAsQueryable(fakeData);
            var userRepository = new UserRepository(this.contextMock.Object);
            var actual = userRepository.GetAll();

            Assert.That(actual.Count() == expected);
        }

        [Test]
        public void GetAllIs_WhenCalledAndThereAreThreeUsersInTheDbSet_ShouldReturnExactAmountOfUsers()
        {
            var expected = 3;
            var fakeData = new List<User>() { new User(), new User(), new User() }.AsQueryable();
            this.SetDbSetUserAsQueryable(fakeData);
            this.SetDbSetUserAsQueryable(fakeData);
            var userRepository = new UserRepository(this.contextMock.Object);
            var actual = userRepository.GetAll();

            Assert.That(actual.Count() == expected);
        }

        [TestCase("Sub-Zero", "Sub", "Zero", "Freeze", "sub@zero", "Varna", 5, "12345", 3, 1, 4)]
        [TestCase("Scorpion", "Scorpion", "Scorpion", "Fire", "scorpion@abv", "Plovdiv", 1, "12341", 4, 5, 0)]
        [Test]
        public void GetAllIs_WhenCalled_ShouldReturnDomainEntityWithCorrectProperties(
        string username,
        string fName,
        string lName,
        string info,
        string email,
        string cityName,
        int cityId,
        string phoneNumber,
        int favUserCount,
        int givenReviewsCount,
        int receivedReviewsCount)
        {
            var contextMock = new Mock<OnTheRoadIdentityDbContext>();
            var dbSetMock = new Mock<DbSet<User>>();
            var userMock = new Mock<User>();
            var cityMock = new Mock<City>();
            var cityStub = new City() { Name = cityName, Id = cityId };
            var userStub = new User()
            {
                UserName = username,
                FirstName = fName,
                LastName = lName,
                Email = email,
                Info = info,
                City = cityStub,
                CityId = cityId,
                PhoneNumber = phoneNumber
            };

            this.AddFavouriteUsers(userStub, favUserCount);
            this.AddGivenReviews(userStub, givenReviewsCount);
            this.AddReceivedRevies(userStub, receivedReviewsCount);

            var fakeData = new List<User>() { userStub }.AsQueryable();
            this.SetDbSetUserAsQueryable(fakeData);

            var userRepository = new UserRepository(this.contextMock.Object);
            var actual = userRepository.GetAll().First();

            Assert.That(actual.Username.Equals(username));
            Assert.That(actual.FirstName.Equals(fName));
            Assert.That(actual.LastName.Equals(lName));
            Assert.That(actual.Email.Equals(email));
            Assert.That(actual.Info.Equals(info));
            Assert.That(actual.PhoneNumber.Equals(phoneNumber));
            Assert.That(actual.City.Name.Equals(cityName));
            Assert.That(actual.City.Id.Equals(cityId));
            Assert.That(actual.FavouriteUsers.Count.Equals(userStub.FavouriteUsers.Count));
            Assert.That(actual.GivenReviews.Count.Equals(userStub.GivenReviews.Count));
            Assert.That(actual.ReceivedReviews.Count.Equals(userStub.ReceivedReviews.Count));
        }

        [Test]
        public void Update_WhenIsCalledWithNull_ShouldThrowArgumentNullException()
        {
            var userRepository = new UserRepository(this.contextMock.Object);

            Assert.Throws<ArgumentNullException>(() => userRepository.Update(null));
        }

        [Test]
        public void Update_WhenIsCalledWithNull_ShouldThrowArgumentNullExceptionWIthProperMessage()
        {
            var userRepository = new UserRepository(this.contextMock.Object);

            var exc = Assert.Throws<ArgumentNullException>(() => userRepository.Update(null));
            StringAssert.Contains("model can not be null!", exc.Message);
        }

        [Test]
        public void Context_WhenUpdateIsCalled_ShouldCallSetEntryStateExactlyOnce()
        {
            var iuserMock = new Mock<IUser>();
            var observableCollection = new ObservableCollection<User>();
            this.dbSetMock.Setup(x => x.Local).Returns(observableCollection);
            this.contextMock.Setup(x => x.SetEntryState(It.IsAny<User>(), It.IsAny<EntityState>())).Verifiable();

            var userRepository = new UserRepository(this.contextMock.Object);
            userRepository.Update(iuserMock.Object);

            contextMock.Verify(x => x.SetEntryState(It.IsAny<User>(), It.IsAny<EntityState>()), Times.Once);
        }

        [Test]
        public void InjectedIUserObject_WhenItsFavouriteUsersAreNullAndUpdateIsCalled_ShouldCallFavouriteUsersExactlyOnce()
        {
            var iuserMock = new Mock<IUser>();
            var observableCollection = new ObservableCollection<User>();
            this.dbSetMock.Setup(x => x.Local).Returns(observableCollection);
            this.contextMock.Setup(x => x.SetEntryState(It.IsAny<User>(), It.IsAny<EntityState>())).Verifiable();

            var userRepository = new UserRepository(this.contextMock.Object);
            userRepository.Update(iuserMock.Object);

            iuserMock.Verify(x => x.FavouriteUsers, Times.Once);
        }

        [Test]
        public void InjectedIUserObject_WhenItsFavouriteUsersAreMoreThanZeroAndUpdateIsCalled_ShouldCallFavouriteUsersTwice()
        {
            var userId = "id";
            var iuserMock = new Mock<IUser>();
            iuserMock.Setup(x => x.Id).Returns(userId);
            iuserMock.Setup(x => x.FavouriteUsers).Returns(new List<IUser>() { iuserMock.Object });

            var userStub = new User() { Id = userId };
            var fakeData = new List<User>() { userStub }.AsQueryable();
            this.SetDbSetUserAsQueryable(fakeData);

            var observableCollection = new ObservableCollection<User>(new List<User>() { userStub });
            this.dbSetMock.Setup(x => x.Local).Returns(observableCollection);

            this.contextMock.Setup(x => x.Users).Returns(this.dbSetMock.Object);
            this.contextMock.Setup(x => x.SetEntryState(It.IsAny<User>(), It.IsAny<EntityState>())).Verifiable();

            var userRepository = new UserRepository(this.contextMock.Object);
            userRepository.Update(iuserMock.Object);

            iuserMock.Verify(x => x.FavouriteUsers, Times.Exactly(2));
        }

        [Test]
        public void Context_WhenUpdateIsCalledWithDomainModelFavouriteUsersEqualsToOne_ShouldCallUsersExactlyOnce()
        {
            var userId = "id";
            var iuserMock = new Mock<IUser>();
            iuserMock.Setup(x => x.Id).Returns(userId);
            iuserMock.Setup(x => x.FavouriteUsers).Returns(new List<IUser>() { iuserMock.Object });

            var userStub = new User() { Id = userId };
            var fakeData = new List<User>() { userStub }.AsQueryable();
            this.SetDbSetUserAsQueryable(fakeData);

            var observableCollection = new ObservableCollection<User>();
            this.dbSetMock.Setup(x => x.Local).Returns(observableCollection);

            this.contextMock.Setup(x => x.Users).Returns(this.dbSetMock.Object);
            this.contextMock.Setup(x => x.SetEntryState(It.IsAny<User>(), It.IsAny<EntityState>())).Verifiable();

            var userRepository = new UserRepository(this.contextMock.Object);
            userRepository.Update(iuserMock.Object);

            this.contextMock.Verify(x => x.Users, Times.Once);
        }

        [Test]
        public void InjectedIUserObject_WhenItsGiverReviewsNullAndUpdateIsCalled_ShouldCallGivenReviewsExactlyOnce()
        {
            var iuserMock = new Mock<IUser>();
            var observableCollection = new ObservableCollection<User>();
            this.dbSetMock.Setup(x => x.Local).Returns(observableCollection);
            this.contextMock.Setup(x => x.SetEntryState(It.IsAny<User>(), It.IsAny<EntityState>())).Verifiable();

            var userRepository = new UserRepository(this.contextMock.Object);
            userRepository.Update(iuserMock.Object);

            iuserMock.Verify(x => x.FavouriteUsers, Times.Once);
        }

        [Test]
        public void InjectedIUserObject_WhenItsGiverReviewsAreMoreThanZeroAndUpdateIsCalled_ShouldCallGivenReviewsExactlyOnce()
        {
            var reviewId = 4;
            var iuserMock = new Mock<IUser>();
            var iReviewMock = new Mock<IReview>();
            iReviewMock.Setup(x => x.Id).Returns(reviewId);
            iuserMock.Setup(x => x.GivenReviews).Returns(new List<IReview>() { });

            var dbSetReviewMock = new Mock<DbSet<Review>>();
            contextMock.Setup(x => x.Set<Review>()).Returns(dbSetReviewMock.Object);
            this.contextMock.Setup(x => x.Reviews).Returns(dbSetReviewMock.Object);

            var observableCollection = new ObservableCollection<User>();
            this.dbSetMock.Setup(x => x.Local).Returns(observableCollection);
            this.contextMock.Setup(x => x.SetEntryState(It.IsAny<User>(), It.IsAny<EntityState>())).Verifiable();

            var userRepository = new UserRepository(this.contextMock.Object);
            userRepository.Update(iuserMock.Object);

            iuserMock.Verify(x => x.GivenReviews, Times.Exactly(2));
        }

        [Test]
        public void Context_WhenUpdateIsCalledWithDomainModelGivenReviewsEqualsToOne_ShouldCallReviewsExactlyOnce()
        {
            var dbSetReviewMock = new Mock<DbSet<Review>>();
            contextMock.Setup(x => x.Set<Review>()).Returns(dbSetReviewMock.Object);

            var reviewId = 4;
            var iuserMock = new Mock<IUser>();
            var iReviewMock = new Mock<IReview>();
            iReviewMock.Setup(x => x.Id).Returns(reviewId);
            iuserMock.Setup(x => x.GivenReviews).Returns(new List<IReview>() { iReviewMock.Object });

            var reviewStub = new Review() { Id = reviewId };
            var fakeData = new List<Review>() { reviewStub }.AsQueryable();
            dbSetReviewMock.As<IQueryable<Review>>().Setup(m => m.Provider).Returns(fakeData.Provider);
            dbSetReviewMock.As<IQueryable<Review>>().Setup(m => m.Expression).Returns(fakeData.Expression);
            dbSetReviewMock.As<IQueryable<Review>>().Setup(m => m.ElementType).Returns(fakeData.ElementType);
            dbSetReviewMock.As<IQueryable<Review>>().Setup(m => m.GetEnumerator()).Returns(fakeData.GetEnumerator());

            var observableCollection = new ObservableCollection<User>();
            this.dbSetMock.Setup(x => x.Local).Returns(observableCollection);

            this.contextMock.Setup(x => x.Reviews).Returns(dbSetReviewMock.Object);
            this.contextMock.Setup(x => x.SetEntryState(It.IsAny<User>(), It.IsAny<EntityState>())).Verifiable();

            var userRepository = new UserRepository(this.contextMock.Object);
            userRepository.Update(iuserMock.Object);

            this.contextMock.Verify(x => x.Reviews, Times.Once);
        }

        [Test]
        public void InjectedIUserObject_WhenItsReceivedReviewsNullAndUpdateIsCalled_ShouldCallReceivedReviewsExactlyOnce()
        {
            var iuserMock = new Mock<IUser>();
            var observableCollection = new ObservableCollection<User>();
            this.dbSetMock.Setup(x => x.Local).Returns(observableCollection);
            this.contextMock.Setup(x => x.SetEntryState(It.IsAny<User>(), It.IsAny<EntityState>())).Verifiable();

            var userRepository = new UserRepository(this.contextMock.Object);
            userRepository.Update(iuserMock.Object);

            iuserMock.Verify(x => x.FavouriteUsers, Times.Once);
        }

        [Test]
        public void InjectedIUserObject_WhenItsReceivedReviewsAreMoreThanZeroAndUpdateIsCalled_ShouldCallReceivedReviewsExactlyOnce()
        {
            var reviewId = 4;
            var iuserMock = new Mock<IUser>();
            var iReviewMock = new Mock<IReview>();
            iReviewMock.Setup(x => x.Id).Returns(reviewId);
            iuserMock.Setup(x => x.ReceivedReviews).Returns(new List<IReview>() { });

            var dbSetReviewMock = new Mock<DbSet<Review>>();
            contextMock.Setup(x => x.Set<Review>()).Returns(dbSetReviewMock.Object);
            this.contextMock.Setup(x => x.Reviews).Returns(dbSetReviewMock.Object);

            var observableCollection = new ObservableCollection<User>();
            this.dbSetMock.Setup(x => x.Local).Returns(observableCollection);
            this.contextMock.Setup(x => x.SetEntryState(It.IsAny<User>(), It.IsAny<EntityState>())).Verifiable();

            var userRepository = new UserRepository(this.contextMock.Object);
            userRepository.Update(iuserMock.Object);

            iuserMock.Verify(x => x.ReceivedReviews, Times.Exactly(2));
        }

        [Test]
        public void Context_WhenUpdateIsCalledWithDomainModelReceivedReviewsEqualsToOne_ShouldCallReviewsExactlyOnce()
        {
            var dbSetReviewMock = new Mock<DbSet<Review>>();
            contextMock.Setup(x => x.Set<Review>()).Returns(dbSetReviewMock.Object);

            var reviewId = 4;
            var iuserMock = new Mock<IUser>();
            var iReviewMock = new Mock<IReview>();
            iReviewMock.Setup(x => x.Id).Returns(reviewId);
            iuserMock.Setup(x => x.ReceivedReviews).Returns(new List<IReview>() { iReviewMock.Object });

            var reviewStub = new Review() { Id = reviewId };
            var fakeData = new List<Review>() { reviewStub }.AsQueryable();
            dbSetReviewMock.As<IQueryable<Review>>().Setup(m => m.Provider).Returns(fakeData.Provider);
            dbSetReviewMock.As<IQueryable<Review>>().Setup(m => m.Expression).Returns(fakeData.Expression);
            dbSetReviewMock.As<IQueryable<Review>>().Setup(m => m.ElementType).Returns(fakeData.ElementType);
            dbSetReviewMock.As<IQueryable<Review>>().Setup(m => m.GetEnumerator()).Returns(fakeData.GetEnumerator());

            var observableCollection = new ObservableCollection<User>();
            this.dbSetMock.Setup(x => x.Local).Returns(observableCollection);

            this.contextMock.Setup(x => x.Reviews).Returns(dbSetReviewMock.Object);
            this.contextMock.Setup(x => x.SetEntryState(It.IsAny<User>(), It.IsAny<EntityState>())).Verifiable();

            var userRepository = new UserRepository(this.contextMock.Object);
            userRepository.Update(iuserMock.Object);

            this.contextMock.Verify(x => x.Reviews, Times.Once);
        }

        private void AddReceivedRevies(User userStub, int receivedReviewsCount)
        {
            for (int i = 0; i < receivedReviewsCount; i++)
            {
                var review = new Mock<Review>();
                userStub.ReceivedReviews.Add(review.Object);
            }
        }

        private void AddGivenReviews(User userStub, int givenReviewsCount)
        {
            for (int i = 0; i < givenReviewsCount; i++)
            {
                var review = new Mock<Review>();
                userStub.GivenReviews.Add(review.Object);
            }
        }

        private void AddFavouriteUsers(User userStub, int favUserCount)
        {
            for (int i = 0; i < favUserCount; i++)
            {
                var user = new Mock<User>();
                userStub.FavouriteUsers.Add(user.Object);
            }
        }

        private void SetDbSetUserAsQueryable(IQueryable<User> fakeData)
        {
            this.dbSetMock.As<IQueryable<User>>().Setup(m => m.Provider).Returns(fakeData.Provider);
            this.dbSetMock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(fakeData.Expression);
            this.dbSetMock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(fakeData.ElementType);
            this.dbSetMock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(fakeData.GetEnumerator());
        }
    }
}
