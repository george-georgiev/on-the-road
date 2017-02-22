using Moq;
using NUnit.Framework;
using OnTheRoad.Data.Contracts;
using OnTheRoad.Data.Models;
using OnTheRoad.Data.Repositories;
using OnTheRoad.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheRoad.Data.Tests.Repositories
{
    [TestFixture]
    public class TripRepositoryTests
    {
        private Mock<IOnTheRoadDbContext> contextMock;
        private Mock<DbSet<Trip>> tripDbSetMock;

        [SetUp]
        public void SetUpMocks()
        {
            this.contextMock = new Mock<IOnTheRoadDbContext>();
            this.tripDbSetMock = new Mock<DbSet<Trip>>();
            contextMock.Setup(x => x.Set<Trip>()).Returns(tripDbSetMock.Object);
        }

        [Test]
        public void Constructor_WhenDbContextIsNull_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new TripRepository(null));
        }

        [Test]
        public void GetTripsByCategoryName_WhenCategoryNameIsNull_ShouldThrow()
        {
            var tripRepository = new TripRepository(this.contextMock.Object);
            string categoryName = null;
            var skip = 5;
            var take = 5;

            Assert.Throws<ArgumentNullException>(() => tripRepository.GetTripsByCategoryName(categoryName, skip, take));
        }

        [TestCase("SomeName", "SomeSearchName")]
        [TestCase("OtherName", "OtherSearchName")]
        public void GetTripsByCategoryName_WhenTripsWithCategoryNameNotExist_ShouldReturnEmptyCollection(string categoryName, string categorySearchName)
        {
            var trip = new Trip();
            trip.Categories = new List<Category>() { new Category() { Name = categoryName } };
            var trips = new List<Trip>() { trip }.AsQueryable();
            this.SetupTripDbSet(trips);
            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);

            var tripRepository = new TripRepository(this.contextMock.Object);
            var skip = 0;
            var take = 1;


            var result = tripRepository.GetTripsByCategoryName(categorySearchName, skip, take);


            CollectionAssert.IsEmpty(result);
        }

        [TestCase("SomeName", "SomeName")]
        [TestCase("OtherName", "OtherName")]
        public void GetTripsByCategoryName_WhenTripsWithCategoryNameExists_ShouldReturnCorrectResult(string categoryName, string categorySearchName)
        {
            var trip = new Trip() { Name = "TripName" };
            trip.Categories = new List<Category>() { new Category() { Name = categoryName } };
            var trips = new List<Trip>() { trip }.AsQueryable();
            this.SetupTripDbSet(trips);
            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);

            var tripRepository = new TripRepository(this.contextMock.Object);
            var skip = 0;
            var take = 1;


            var result = tripRepository.GetTripsByCategoryName(categorySearchName, skip, take).ToList();


            Assert.AreEqual(trip.Name, result[0].Name);
        }

        [TestCase(0, "SomeTrip1", "SomeTrip2", "SomeTrip3")]
        [TestCase(1, "OtherTrip1", "OtherTrip2", "OtherTrip3")]
        [TestCase(2, "ThirdTrip1", "ThirdTrip2", "ThirdTrip3")]
        public void GetTripsByCategoryName_WhenTripsWithCategoryExists_ShouldReturnCorrectSkippedTrips(int skip, params string[] names)
        {
            const string categoryName = "SomeName";
            var categories = new List<Category>()
            {
                new Category() { Name = categoryName }
            };

            var trips = names.Select(name => new Trip() { Name = name, Categories = categories }).AsQueryable();

            this.SetupTripDbSet(trips);

            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);

            var tripRepository = new TripRepository(this.contextMock.Object);
            var take = 1;


            var result = tripRepository.GetTripsByCategoryName(categoryName, skip, take).ToList();


            Assert.AreEqual(names[skip], result[0].Name);
        }

        [TestCase(1, "SomeTrip1", "SomeTrip2", "SomeTrip3")]
        [TestCase(2, "OtherTrip1", "OtherTrip2", "OtherTrip3")]
        [TestCase(3, "ThirdTrip1", "ThirdTrip2", "ThirdTrip3")]
        public void GetTripsByCategoryName_WhenTripsWithCategoryExists_ShouldTakeCoorectNumberOfTrips(int take, params string[] names)
        {
            string categoryName = "SomeName";
            var categories = new List<Category>()
            {
                new Category() { Name = categoryName }
            };

            var trips = names.Select(name => new Trip() { Name = name, Categories = categories }).AsQueryable();

            this.SetupTripDbSet(trips);

            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);

            var tripRepository = new TripRepository(this.contextMock.Object);
            var skip = 0;


            var result = tripRepository.GetTripsByCategoryName(categoryName, skip, take);


            Assert.GreaterOrEqual(take, result.Count());
        }

        [TestCase(new int[] { 0, 1, 2 }, "SomeTrip1", "SomeTrip2", "SomeTrip3")]
        [TestCase(new int[] { 0, 1, 2 }, "OtherTrip1", "OtherTrip2", "OtherTrip3")]
        public void GetTripsByCategoryName_WhenTripsWithCategoryExists_ShouldReturnTripsOrderedByDateCreated(int[] hours, params string[] names)
        {
            string categoryName = "SomeName";
            var categories = new List<Category>()
            {
                new Category() { Name = categoryName }
            };

            var now = DateTime.Now;
            var trips = names.Select(name => new Trip() { Name = name, Categories = categories }).ToList();
            for (int i = 0; i < hours.Length; i++)
            {
                var createDate = now.AddHours(hours[i]);
                trips[i].CreateDate = createDate;
            }

            this.SetupTripDbSet(trips.AsQueryable());

            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);

            var tripRepository = new TripRepository(this.contextMock.Object);
            var skip = 0;
            var take = trips.Count();

            var result = tripRepository.GetTripsByCategoryName(categoryName, skip, take).ToList();


            Assert.AreEqual(result[0].CreateDate, now.AddHours(hours[2]));
            Assert.AreEqual(result[1].CreateDate, now.AddHours(hours[1]));
            Assert.AreEqual(result[2].CreateDate, now.AddHours(hours[0]));
        }

        [Test]
        public void GetTripsByCategoryName_WhenTripsWithCategoryExists_ShouldReturnCorrectlyMappedTrips()
        {
            var tripName = "TripName";
            var location = "Location";
            var description = "Description";
            var startDate = DateTime.Now;
            var endDate = DateTime.Now.AddDays(1);
            var createDate = DateTime.Now;
            var categoryName = "CategoryName";
            var coverImage = new byte[0];

            var user = new User()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "Email",
                Id = "Id",
                UserName = "UserName"
            };

            var categories = new List<Category>()
            {
                new Category() { Name = categoryName }
            };

            var trips = new List<Trip>()
            {
                new Trip()
                {
                    Name = tripName,
                    Location = location,
                    Description = description,
                    StartDate = startDate,
                    EndDate = endDate,
                    CreateDate = createDate,
                    Categories = categories,
                    CoverImage = coverImage,
                    Organiser = user
                }
            }
            .AsQueryable();

            this.SetupTripDbSet(trips);

            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);

            var tripRepository = new TripRepository(this.contextMock.Object);
            var skip = 0;
            var take = 1;

            var result = tripRepository.GetTripsByCategoryName(categoryName, skip, take).ToList();


            var trip = result[0];
            Assert.AreEqual(tripName, trip.Name);
            Assert.AreEqual(location, trip.Location);
            Assert.AreEqual(description, trip.Description);
            Assert.AreEqual(startDate, trip.StartDate);
            Assert.AreEqual(endDate, trip.EndDate);
            Assert.AreEqual(createDate, trip.CreateDate);
            Assert.AreEqual(coverImage, trip.CoverImage);
            Assert.AreEqual(categoryName, trip.Categories.ToList()[0].Name);

            Assert.AreEqual(user.UserName, trip.Organiser.Username);
            Assert.AreEqual(user.Id, trip.Organiser.Id);
            Assert.AreEqual(user.FirstName, trip.Organiser.FirstName);
            Assert.AreEqual(user.LastName, trip.Organiser.LastName);
            Assert.AreEqual(user.Email, trip.Organiser.Email);
        }

        [Test]
        public void GetTripsCountByCategoryName_WhenCategoryNameIsNull_ShouldThrow()
        {
            var tripRepository = new TripRepository(this.contextMock.Object);
            string categoryName = null;

            Assert.Throws<ArgumentNullException>(() => tripRepository.GetTripsCountByCategoryName(categoryName));
        }

        [Test]
        public void GetTripsCountByCategoryName_WhenTripsWithCategoryNameNotExist_ShouldReturnZero()
        {
            var trip = new Trip();
            trip.Categories = new List<Category>() { new Category() { Name = "SomeName" } };
            var trips = new List<Trip>() { trip }.AsQueryable();
            this.SetupTripDbSet(trips);
            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);
            var tripRepository = new TripRepository(this.contextMock.Object);

            var count = tripRepository.GetTripsCountByCategoryName("OtherName");

            Assert.AreEqual(0, count);
        }

        [TestCase(2)]
        [TestCase(3)]
        public void GetTripsCountByCategoryName_WhenTripsWithCategoryNameExist_ShouldReturnCorrectCount(int count)
        {
            const string categoryName = "SomeName";
            var categories = new List<Category>() { new Category() { Name = categoryName } };

            var trips = new List<Trip>();
            for (int i = 0; i < count; i++)
            {
                trips.Add(new Trip() { Categories = categories });
            }

            this.SetupTripDbSet(trips.AsQueryable());
            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);
            var tripRepository = new TripRepository(this.contextMock.Object);


            var result = tripRepository.GetTripsCountByCategoryName(categoryName);


            Assert.AreEqual(count, result);
        }

        [TestCase("SomeName", "SomeSearchName")]
        [TestCase("OtherName", "OtherSearchName")]
        public void GetTripsByCategoryNameOrderedByDate_WhenTripsWithCategoryNameNotExist_ShouldReturnEmptyCollection(string categoryName, string categorySearchName)
        {
            var trip = new Trip();
            trip.Categories = new List<Category>() { new Category() { Name = categoryName } };
            var trips = new List<Trip>() { trip }.AsQueryable();
            this.SetupTripDbSet(trips);
            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);

            var tripRepository = new TripRepository(this.contextMock.Object);
            var count = 1;
            var isAscending = false;


            var result = tripRepository.GetTripsByCategoryNameOrderedByDate(categorySearchName, count, isAscending);


            CollectionAssert.IsEmpty(result);
        }

        [TestCase("SomeName", "SomeName")]
        [TestCase("OtherName", "OtherName")]
        public void GetTripsByCategoryNameOrderedByDate_WhenTripsWithCategoryNameExist_ShouldReturnCorrectResult(string categoryName, string categorySearchName)
        {
            var trip = new Trip() { Name = "TripName" };
            trip.Categories = new List<Category>() { new Category() { Name = categoryName } };
            var trips = new List<Trip>() { trip }.AsQueryable();
            this.SetupTripDbSet(trips);
            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);

            var tripRepository = new TripRepository(this.contextMock.Object);
            var count = 1;
            var isAscending = false;


            var result = tripRepository.GetTripsByCategoryNameOrderedByDate(categorySearchName, count, isAscending).ToList();


            Assert.AreEqual(trip.Name, result[0].Name);
        }

        [TestCase(1, "SomeTrip1", "SomeTrip2", "SomeTrip3")]
        [TestCase(2, "OtherTrip1", "OtherTrip2", "OtherTrip3")]
        [TestCase(3, "ThirdTrip1", "ThirdTrip2", "ThirdTrip3")]
        public void GetTripsByCategoryNameOrderedByDate_WhenTripsWithCategoryExists_ShouldTakeCoorectNumberOfTrips(int count, params string[] names)
        {
            string categoryName = "SomeName";
            var categories = new List<Category>()
            {
                new Category() { Name = categoryName }
            };

            var trips = names.Select(name => new Trip() { Name = name, Categories = categories }).AsQueryable();

            this.SetupTripDbSet(trips);

            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);

            var tripRepository = new TripRepository(this.contextMock.Object);
            var isAscending = false;


            var result = tripRepository.GetTripsByCategoryNameOrderedByDate(categoryName, count, isAscending);


            Assert.GreaterOrEqual(count, result.Count());
        }

        [TestCase(new int[] { 0, 1, 2 }, "SomeTrip1", "SomeTrip2", "SomeTrip3")]
        [TestCase(new int[] { 0, 1, 2 }, "OtherTrip1", "OtherTrip2", "OtherTrip3")]
        public void GetTripsByCategoryNameOrderedByDate_WhenAscendingIsTrue_ShouldReturnTripsOrderedByDateCreatedAscending(int[] hours, params string[] names)
        {
            string categoryName = "SomeName";
            var categories = new List<Category>()
            {
                new Category() { Name = categoryName }
            };

            var now = DateTime.Now;
            var trips = names.Select(name => new Trip() { Name = name, Categories = categories }).ToList();
            for (int i = 0; i < hours.Length; i++)
            {
                var createDate = now.AddHours(hours[i]);
                trips[i].CreateDate = createDate;
            }

            this.SetupTripDbSet(trips.AsQueryable());

            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);

            var tripRepository = new TripRepository(this.contextMock.Object);
            var count = trips.Count();
            var isAscending = true;


            var result = tripRepository.GetTripsByCategoryNameOrderedByDate(categoryName, count, isAscending).ToList();


            Assert.AreEqual(result[0].CreateDate, now.AddHours(hours[0]));
            Assert.AreEqual(result[1].CreateDate, now.AddHours(hours[1]));
            Assert.AreEqual(result[2].CreateDate, now.AddHours(hours[2]));
        }

        [TestCase(new int[] { 0, 1, 2 }, "SomeTrip1", "SomeTrip2", "SomeTrip3")]
        [TestCase(new int[] { 0, 1, 2 }, "OtherTrip1", "OtherTrip2", "OtherTrip3")]
        public void GetTripsByCategoryNameOrderedByDate_WhenAscendingIsFalse_ShouldReturnTripsOrderedByDateCreatedDescending(int[] hours, params string[] names)
        {
            string categoryName = "SomeName";
            var categories = new List<Category>()
            {
                new Category() { Name = categoryName }
            };

            var now = DateTime.Now;
            var trips = names.Select(name => new Trip() { Name = name, Categories = categories }).ToList();
            for (int i = 0; i < hours.Length; i++)
            {
                var createDate = now.AddHours(hours[i]);
                trips[i].CreateDate = createDate;
            }

            this.SetupTripDbSet(trips.AsQueryable());

            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);

            var tripRepository = new TripRepository(this.contextMock.Object);
            var count = trips.Count();
            var isAscending = false;


            var result = tripRepository.GetTripsByCategoryNameOrderedByDate(categoryName, count, isAscending).ToList();


            Assert.AreEqual(result[0].CreateDate, now.AddHours(hours[2]));
            Assert.AreEqual(result[1].CreateDate, now.AddHours(hours[1]));
            Assert.AreEqual(result[2].CreateDate, now.AddHours(hours[0]));
        }

        [Test]
        public void GetTripsByCategoryNameOrderedByDate_WhenTripsWithCategoryNameExists_ShouldReturnCorrectlyMappedTrips()
        {
            var tripName = "TripName";
            var location = "Location";
            var description = "Description";
            var startDate = DateTime.Now;
            var endDate = DateTime.Now.AddDays(1);
            var createDate = DateTime.Now;
            var categoryName = "CategoryName";
            var coverImage = new byte[0];

            var user = new User()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "Email",
                Id = "Id",
                UserName = "UserName"
            };

            var categories = new List<Category>()
            {
                new Category() { Name = categoryName }
            };

            var trips = new List<Trip>()
            {
                new Trip()
                {
                    Name = tripName,
                    Location = location,
                    Description = description,
                    StartDate = startDate,
                    EndDate = endDate,
                    CreateDate = createDate,
                    Categories = categories,
                    CoverImage = coverImage,
                    Organiser = user
                }
            }
            .AsQueryable();

            this.SetupTripDbSet(trips);

            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);

            var tripRepository = new TripRepository(this.contextMock.Object);
            var count = 1;
            var isAscending = false;

            var result = tripRepository.GetTripsByCategoryNameOrderedByDate(categoryName, count, isAscending).ToList();


            var trip = result[0];
            Assert.AreEqual(tripName, trip.Name);
            Assert.AreEqual(location, trip.Location);
            Assert.AreEqual(description, trip.Description);
            Assert.AreEqual(startDate, trip.StartDate);
            Assert.AreEqual(endDate, trip.EndDate);
            Assert.AreEqual(createDate, trip.CreateDate);
            Assert.AreEqual(coverImage, trip.CoverImage);
            Assert.AreEqual(categoryName, trip.Categories.ToList()[0].Name);

            Assert.AreEqual(user.UserName, trip.Organiser.Username);
            Assert.AreEqual(user.Id, trip.Organiser.Id);
            Assert.AreEqual(user.FirstName, trip.Organiser.FirstName);
            Assert.AreEqual(user.LastName, trip.Organiser.LastName);
            Assert.AreEqual(user.Email, trip.Organiser.Email);
        }

        [TestCase("SomeName", "SomeSearchName")]
        [TestCase("OtherName", "OtherSearchName")]
        public void GetTripsBySearchPattern_WhenMatchNotFound_ShouldReturnEmptyCollection(string tagName, string pattern)
        {
            var trip = new Trip();
            trip.Tags = new List<Tag>() { new Tag() { Name = tagName } };
            var trips = new List<Trip>() { trip }.AsQueryable();
            this.SetupTripDbSet(trips);
            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);

            var tripRepository = new TripRepository(this.contextMock.Object);
            var skip = 0;
            var take = 1;


            var result = tripRepository.GetTripsBySearchPattern(pattern, skip, take);


            CollectionAssert.IsEmpty(result);
        }

        [TestCase("SomeName", "SomeName")]
        [TestCase("OtherName", "OtherName")]
        public void GetTripsBySearchPattern_WhenFullMatchFound_ShouldReturnCorrectResult(string tagName, string pattern)
        {
            var trip = new Trip() { Name = "TripName" };
            trip.Tags = new List<Tag>() { new Tag() { Name = tagName } };
            var trips = new List<Trip>() { trip }.AsQueryable();
            this.SetupTripDbSet(trips);
            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);

            var tripRepository = new TripRepository(this.contextMock.Object);
            var skip = 0;
            var take = 1;


            var result = tripRepository.GetTripsBySearchPattern(pattern, skip, take).ToList();


            Assert.AreEqual(trip.Name, result[0].Name);
        }

        [TestCase("SomeName", "Some")]
        [TestCase("OtherName", "Other")]
        public void GetTripsBySearchPattern_WhenPartialMatchFound_ShouldReturnCorrectResult(string tagName, string pattern)
        {
            var trip = new Trip() { Name = "TripName" };
            trip.Tags = new List<Tag>() { new Tag() { Name = tagName } };
            var trips = new List<Trip>() { trip }.AsQueryable();
            this.SetupTripDbSet(trips);
            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);

            var tripRepository = new TripRepository(this.contextMock.Object);
            var skip = 0;
            var take = 1;


            var result = tripRepository.GetTripsBySearchPattern(pattern, skip, take).ToList();


            Assert.AreEqual(trip.Name, result[0].Name);
        }

        [TestCase("SOMENAME", "somename")]
        [TestCase("OTHERNAME", "othername")]
        public void GetTripsBySearchPattern_ShouldBeCaseInsensitive(string tagName, string pattern)
        {
            var trip = new Trip() { Name = "TripName" };
            trip.Tags = new List<Tag>() { new Tag() { Name = tagName } };
            var trips = new List<Trip>() { trip }.AsQueryable();
            this.SetupTripDbSet(trips);
            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);

            var tripRepository = new TripRepository(this.contextMock.Object);
            var skip = 0;
            var take = 1;


            var result = tripRepository.GetTripsBySearchPattern(pattern, skip, take).ToList();


            Assert.AreEqual(trip.Name, result[0].Name);
        }

        [TestCase(0, "SomeTrip1", "SomeTrip2", "SomeTrip3")]
        [TestCase(1, "OtherTrip1", "OtherTrip2", "OtherTrip3")]
        [TestCase(2, "ThirdTrip1", "ThirdTrip2", "ThirdTrip3")]
        public void GetTripsBySearchPattern_WhenTripsMatchFound_ShouldReturnCorrectSkippedTrips(int skip, params string[] names)
        {
            const string tagName = "SomeName";
            var tags = new List<Tag>()
            {
                new Tag() { Name = tagName }
            };

            var trips = names.Select(name => new Trip() { Name = name, Tags = tags }).AsQueryable();

            this.SetupTripDbSet(trips);

            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);

            var tripRepository = new TripRepository(this.contextMock.Object);
            var take = 1;


            var result = tripRepository.GetTripsBySearchPattern(tagName, skip, take).ToList();


            Assert.AreEqual(names[skip], result[0].Name);
        }

        [TestCase(1, "SomeTrip1", "SomeTrip2", "SomeTrip3")]
        [TestCase(2, "OtherTrip1", "OtherTrip2", "OtherTrip3")]
        [TestCase(3, "ThirdTrip1", "ThirdTrip2", "ThirdTrip3")]
        public void GetTripsBySearchPattern_WhenTripsMatchFound_ShouldTakeCoorectNumberOfTrips(int take, params string[] names)
        {
            string tagName = "SomeName";
            var tags = new List<Tag>()
            {
                new Tag() { Name = tagName }
            };

            var trips = names.Select(name => new Trip() { Name = name, Tags = tags }).AsQueryable();

            this.SetupTripDbSet(trips);

            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);

            var tripRepository = new TripRepository(this.contextMock.Object);
            var skip = 0;


            var result = tripRepository.GetTripsBySearchPattern(tagName, skip, take);


            Assert.GreaterOrEqual(take, result.Count());
        }

        [TestCase(new int[] { 0, 1, 2 }, "SomeTrip1", "SomeTrip2", "SomeTrip3")]
        [TestCase(new int[] { 0, 1, 2 }, "OtherTrip1", "OtherTrip2", "OtherTrip3")]
        public void GetTripsBySearchPattern_WhenTripsMatchFound_ShouldReturnTripsOrderedByDateCreated(int[] hours, params string[] names)
        {
            string tagName = "SomeName";
            var tags = new List<Tag>()
            {
                new Tag() { Name = tagName }
            };

            var now = DateTime.Now;
            var trips = names.Select(name => new Trip() { Name = name, Tags = tags }).ToList();
            for (int i = 0; i < hours.Length; i++)
            {
                var createDate = now.AddHours(hours[i]);
                trips[i].CreateDate = createDate;
            }

            this.SetupTripDbSet(trips.AsQueryable());

            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);

            var tripRepository = new TripRepository(this.contextMock.Object);
            var skip = 0;
            var take = trips.Count();

            var result = tripRepository.GetTripsBySearchPattern(tagName, skip, take).ToList();


            Assert.AreEqual(result[0].CreateDate, now.AddHours(hours[2]));
            Assert.AreEqual(result[1].CreateDate, now.AddHours(hours[1]));
            Assert.AreEqual(result[2].CreateDate, now.AddHours(hours[0]));
        }

        [Test]
        public void GetTripsBySearchPattern_WhenTripsMatchFound_ShouldReturnCorrectlyMappedTrips()
        {
            var tripName = "TripName";
            var location = "Location";
            var description = "Description";
            var startDate = DateTime.Now;
            var endDate = DateTime.Now.AddDays(1);
            var createDate = DateTime.Now;
            var categoryName = "CategoryName";
            var tagName = "TagName";
            var coverImage = new byte[0];

            var user = new User()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "Email",
                Id = "Id",
                UserName = "UserName"
            };

            var categories = new List<Category>()
            {
                new Category() { Name = categoryName }
            };

            var tags = new List<Tag>()
            {
                new Tag() { Name = tagName }
            };

            var trips = new List<Trip>()
            {
                new Trip()
                {
                    Tags = tags,
                    Name = tripName,
                    Location = location,
                    Description = description,
                    StartDate = startDate,
                    EndDate = endDate,
                    CreateDate = createDate,
                    Categories = categories,
                    CoverImage = coverImage,
                    Organiser = user
                }
            }
            .AsQueryable();

            this.SetupTripDbSet(trips);

            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);

            var tripRepository = new TripRepository(this.contextMock.Object);
            var skip = 0;
            var take = 1;

            var result = tripRepository.GetTripsBySearchPattern(tagName, skip, take).ToList();


            var trip = result[0];
            Assert.AreEqual(tripName, trip.Name);
            Assert.AreEqual(location, trip.Location);
            Assert.AreEqual(description, trip.Description);
            Assert.AreEqual(startDate, trip.StartDate);
            Assert.AreEqual(endDate, trip.EndDate);
            Assert.AreEqual(createDate, trip.CreateDate);
            Assert.AreEqual(coverImage, trip.CoverImage);
            Assert.AreEqual(categoryName, trip.Categories.ToList()[0].Name);

            Assert.AreEqual(user.UserName, trip.Organiser.Username);
            Assert.AreEqual(user.Id, trip.Organiser.Id);
            Assert.AreEqual(user.FirstName, trip.Organiser.FirstName);
            Assert.AreEqual(user.LastName, trip.Organiser.LastName);
            Assert.AreEqual(user.Email, trip.Organiser.Email);
        }

        [Test]
        public void GetTripsCountBySearchPattern_WhenMatchNotFound_ShouldReturnZero()
        {
            var trip = new Trip();
            trip.Tags = new List<Tag>() { new Tag() { Name = "SomeName" } };
            var trips = new List<Trip>() { trip }.AsQueryable();
            this.SetupTripDbSet(trips);
            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);
            var tripRepository = new TripRepository(this.contextMock.Object);

            var count = tripRepository.GetTripsCountBySearchPattern("OtherName");

            Assert.AreEqual(0, count);
        }

        [TestCase(2)]
        [TestCase(3)]
        public void GetTripsCountBySearchPattern_WhenFullMatchFound_ShouldReturnCorrectCount(int count)
        {
            const string tagName = "SomeName";
            var tags = new List<Tag>() { new Tag() { Name = tagName } };

            var trips = new List<Trip>();
            for (int i = 0; i < count; i++)
            {
                trips.Add(new Trip() { Tags = tags });
            }

            this.SetupTripDbSet(trips.AsQueryable());
            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);
            var tripRepository = new TripRepository(this.contextMock.Object);


            var result = tripRepository.GetTripsCountBySearchPattern(tagName);


            Assert.AreEqual(count, result);
        }

        [TestCase(2)]
        [TestCase(3)]
        public void GetTripsCountBySearchPattern_WhenPartialMatchFound_ShouldReturnCorrectCount(int count)
        {
            var tags = new List<Tag>() { new Tag() { Name = "SomeName" } };
            var trips = new List<Trip>();
            for (int i = 0; i < count; i++)
            {
                trips.Add(new Trip() { Tags = tags });
            }

            this.SetupTripDbSet(trips.AsQueryable());
            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);
            var tripRepository = new TripRepository(this.contextMock.Object);


            var result = tripRepository.GetTripsCountBySearchPattern("Some");


            Assert.AreEqual(count, result);
        }

        [TestCase(2)]
        [TestCase(3)]
        public void GetTripsCountBySearchPattern_ShouldBeCaseInsensitive(int count)
        {
            var tags = new List<Tag>() { new Tag() { Name = "SOMENAME" } };
            var trips = new List<Trip>();
            for (int i = 0; i < count; i++)
            {
                trips.Add(new Trip() { Tags = tags });
            }

            this.SetupTripDbSet(trips.AsQueryable());
            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);
            var tripRepository = new TripRepository(this.contextMock.Object);


            var result = tripRepository.GetTripsCountBySearchPattern("somename");


            Assert.AreEqual(count, result);
        }

        [TestCase("TripName1", "TripName2", "TripName3")]
        [TestCase("OtherTripName1", "OtherTripName2", "OtherTripName3")]
        public void GetTrips_ShouldReturnCollectionWithAllTrips(params string[] names)
        {
            var trips = names.Select(n => new Trip() { Name = n });

            this.SetupTripDbSet(trips.AsQueryable());
            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);

            var tripRepository = new TripRepository(this.contextMock.Object);
            var skip = 0;
            var take = names.Length;


            var result = tripRepository.GetTrips(skip, take).ToList();


            Assert.AreEqual(names.Length, result.Count);
            Assert.AreEqual(names[0], result[0].Name);
            Assert.AreEqual(names[1], result[1].Name);
            Assert.AreEqual(names[2], result[2].Name);
        }

        [TestCase(0, "SomeTrip1", "SomeTrip2", "SomeTrip3")]
        [TestCase(1, "OtherTrip1", "OtherTrip2", "OtherTrip3")]
        [TestCase(2, "ThirdTrip1", "ThirdTrip2", "ThirdTrip3")]
        public void GetTrips_ShouldReturnCorrectSkippedTrips(int skip, params string[] names)
        {
            var trips = names.Select(name => new Trip() { Name = name }).AsQueryable();
            this.SetupTripDbSet(trips);
            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);

            var tripRepository = new TripRepository(this.contextMock.Object);
            var take = 1;


            var result = tripRepository.GetTrips(skip, take).ToList();


            Assert.AreEqual(names[skip], result[0].Name);
        }

        [TestCase(1, "SomeTrip1", "SomeTrip2", "SomeTrip3")]
        [TestCase(2, "OtherTrip1", "OtherTrip2", "OtherTrip3")]
        [TestCase(3, "ThirdTrip1", "ThirdTrip2", "ThirdTrip3")]
        public void GetTrips_ShouldTakeCoorectNumberOfTrips(int take, params string[] names)
        {
            var trips = names.Select(name => new Trip() { Name = name }).AsQueryable();
            this.SetupTripDbSet(trips);
            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);

            var tripRepository = new TripRepository(this.contextMock.Object);
            var skip = 0;


            var result = tripRepository.GetTrips(skip, take);


            Assert.GreaterOrEqual(take, result.Count());
        }

        [TestCase(new int[] { 0, 1, 2 }, "SomeTrip1", "SomeTrip2", "SomeTrip3")]
        [TestCase(new int[] { 0, 1, 2 }, "OtherTrip1", "OtherTrip2", "OtherTrip3")]
        public void GetTrips_ShouldReturnTripsOrderedByDateCreated(int[] hours, params string[] names)
        {
            var now = DateTime.Now;
            var trips = names.Select(name => new Trip() { Name = name }).ToList();
            for (int i = 0; i < hours.Length; i++)
            {
                var createDate = now.AddHours(hours[i]);
                trips[i].CreateDate = createDate;
            }

            this.SetupTripDbSet(trips.AsQueryable());
            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);

            var tripRepository = new TripRepository(this.contextMock.Object);
            var skip = 0;
            var take = trips.Count();


            var result = tripRepository.GetTrips(skip, take).ToList();


            Assert.AreEqual(result[0].CreateDate, now.AddHours(hours[2]));
            Assert.AreEqual(result[1].CreateDate, now.AddHours(hours[1]));
            Assert.AreEqual(result[2].CreateDate, now.AddHours(hours[0]));
        }

        [Test]
        public void GetTrips_ShouldReturnCorrectlyMappedTrips()
        {
            var tripName = "TripName";
            var location = "Location";
            var description = "Description";
            var startDate = DateTime.Now;
            var endDate = DateTime.Now.AddDays(1);
            var createDate = DateTime.Now;
            var categoryName = "CategoryName";
            var coverImage = new byte[0];

            var user = new User()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "Email",
                Id = "Id",
                UserName = "UserName"
            };

            var categories = new List<Category>()
            {
                new Category() { Name = categoryName }
            };

            var trips = new List<Trip>()
            {
                new Trip()
                {
                    Name = tripName,
                    Location = location,
                    Description = description,
                    StartDate = startDate,
                    EndDate = endDate,
                    CreateDate = createDate,
                    Categories = categories,
                    CoverImage = coverImage,
                    Organiser = user
                }
            }
            .AsQueryable();

            this.SetupTripDbSet(trips);

            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);

            var tripRepository = new TripRepository(this.contextMock.Object);
            var skip = 0;
            var take = 1;


            var result = tripRepository.GetTrips(skip, take).ToList();


            var trip = result[0];
            Assert.AreEqual(tripName, trip.Name);
            Assert.AreEqual(location, trip.Location);
            Assert.AreEqual(description, trip.Description);
            Assert.AreEqual(startDate, trip.StartDate);
            Assert.AreEqual(endDate, trip.EndDate);
            Assert.AreEqual(createDate, trip.CreateDate);
            Assert.AreEqual(coverImage, trip.CoverImage);
            Assert.AreEqual(categoryName, trip.Categories.ToList()[0].Name);

            Assert.AreEqual(user.UserName, trip.Organiser.Username);
            Assert.AreEqual(user.Id, trip.Organiser.Id);
            Assert.AreEqual(user.FirstName, trip.Organiser.FirstName);
            Assert.AreEqual(user.LastName, trip.Organiser.LastName);
            Assert.AreEqual(user.Email, trip.Organiser.Email);
        }

        [Test]
        public void GetTripsCount_WhenNoTripsExist_ShouldReturnZero()
        {
            var trips = new List<Trip>().AsQueryable();
            this.SetupTripDbSet(trips);
            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);
            var tripRepository = new TripRepository(this.contextMock.Object);

            var count = tripRepository.GetTripsCount();

            Assert.AreEqual(0, count);
        }

        [TestCase(2)]
        [TestCase(3)]
        public void GetTripsCount_WhenTripsExist_ShouldReturnCorrectCount(int count)
        {
            var trips = new List<Trip>();
            for (int i = 0; i < count; i++)
            {
                trips.Add(new Trip());
            }

            this.SetupTripDbSet(trips.AsQueryable());
            this.contextMock.SetupGet(x => x.Trips).Returns(this.tripDbSetMock.Object);
            var tripRepository = new TripRepository(this.contextMock.Object);


            var result = tripRepository.GetTripsCount();


            Assert.AreEqual(count, result);
        }

        [Test]
        public void Add_ShouldSetCorrectEntryState()
        {
            var trip = new Trip() { Categories = new List<Category>(), Tags = new List<Tag>() };
            var trips = new ObservableCollection<Trip>() { trip };
            this.tripDbSetMock.Setup(x => x.Local).Returns(trips);
            this.contextMock.Setup(x => x.SetEntryState(It.IsAny<Trip>(), It.IsAny<EntityState>()));

            var model = new Mock<ITrip>();
            var organiser = new Mock<IUser>();
            const string username = "username";
            organiser.Setup(x => x.Username).Returns(username);
            model.Setup(x => x.Organiser).Returns(organiser.Object);
            model.Setup(x => x.Categories).Returns(new List<ICategory>());
            model.Setup(x => x.Tags).Returns(new List<ITag>());

            var userDbSetMock = new Mock<DbSet<User>>();
            var users = new List<User>() { new User() { UserName = username } }.AsQueryable();
            userDbSetMock.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            userDbSetMock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            userDbSetMock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            userDbSetMock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());
            this.contextMock.SetupGet(x => x.Users).Returns(userDbSetMock.Object);

            var tripRepository = new TripRepository(this.contextMock.Object);


            tripRepository.Add(model.Object);


            this.contextMock.Verify(x => x.SetEntryState(It.Is<Trip>(t => t.Equals(trip)), It.Is<EntityState>(n => n == EntityState.Added)), Times.Once);
        }

        [Test]
        public void Add_WhenOrganiserIsNotFound_ShouldThrow()
        {
            var trip = new Trip() { Categories = new List<Category>(), Tags = new List<Tag>() };
            var trips = new ObservableCollection<Trip>() { trip };
            this.tripDbSetMock.Setup(x => x.Local).Returns(trips);
            this.contextMock.Setup(x => x.SetEntryState(It.IsAny<Trip>(), It.IsAny<EntityState>()));

            var model = new Mock<ITrip>();
            var organiser = new Mock<IUser>();
            const string username = "username";
            organiser.Setup(x => x.Username).Returns(username);
            model.Setup(x => x.Organiser).Returns(organiser.Object);
            model.Setup(x => x.Categories).Returns(new List<ICategory>());
            model.Setup(x => x.Tags).Returns(new List<ITag>());

            var userDbSetMock = new Mock<DbSet<User>>();
            var users = new List<User>() { new User() { UserName = "otherUsername" } }.AsQueryable();
            userDbSetMock.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            userDbSetMock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            userDbSetMock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            userDbSetMock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());
            this.contextMock.SetupGet(x => x.Users).Returns(userDbSetMock.Object);

            var tripRepository = new TripRepository(this.contextMock.Object);


            Assert.Throws<ArgumentException>(() => tripRepository.Add(model.Object));
        }

        private void SetupTripDbSet(IQueryable<Trip> trips)
        {
            this.tripDbSetMock.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(trips.Provider);
            this.tripDbSetMock.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(trips.Expression);
            this.tripDbSetMock.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(trips.ElementType);
            this.tripDbSetMock.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(trips.GetEnumerator());
            this.tripDbSetMock.Setup(m => m.Include(It.IsAny<string>())).Returns(this.tripDbSetMock.Object);
        }
    }
}
