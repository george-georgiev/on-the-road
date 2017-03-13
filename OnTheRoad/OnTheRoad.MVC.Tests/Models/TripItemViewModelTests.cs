using NUnit.Framework;
using OnTheRoad.MVC.Models;
using System;

namespace OnTheRoad.MVC.Tests.Models
{
    [TestFixture]
    public class TripItemViewModelTests
    {
        [Test]
        public void Constructor_ShouldReturnTripItemViewModelInstance()
        {
            // Arrange & Act & Assert
            Assert.IsInstanceOf<TripViewModel>(new TripViewModel());
        }

        [Test]
        public void Id_ShouldGetSetCorrectValue()
        {
            // Arrange
            var tripItemViewModel = new TripViewModel();
            var id = 5;

            // Act
            tripItemViewModel.Id = id;

            // Assert
            Assert.AreEqual(id, tripItemViewModel.Id);
        }

        [Test]
        public void Name_ShouldGetSetCorrectValue()
        {
            // Arrange
            var tripItemViewModel = new TripViewModel();
            var name = "Some Name";

            // Act
            tripItemViewModel.Name = name;

            // Assert
            Assert.AreEqual(name, tripItemViewModel.Name);
        }

        [Test]
        public void CoverImage_ShouldGetSetCorrectValue()
        {
            // Arrange
            var tripItemViewModel = new TripViewModel();
            var coverImage = new byte[0];

            // Act
            tripItemViewModel.CoverImage = coverImage;

            // Assert
            Assert.AreEqual(coverImage, tripItemViewModel.CoverImage);
        }

        [Test]
        public void Location_ShouldGetSetCorrectValue()
        {
            // Arrange
            var tripItemViewModel = new TripViewModel();
            var location = "Some Location";

            // Act
            tripItemViewModel.Location = location;

            // Assert
            Assert.AreEqual(location, tripItemViewModel.Location);
        }

        [Test]
        public void StartDate_ShouldGetSetCorrectValue()
        {
            // Arrange
            var tripItemViewModel = new TripViewModel();
            var startDate = DateTime.Now;

            // Act
            tripItemViewModel.StartDate = startDate;

            // Assert
            Assert.AreEqual(startDate, tripItemViewModel.StartDate);
        }

        [Test]
        public void OrganiserUserName_ShouldGetSetCorrectValue()
        {
            // Arrange
            var tripItemViewModel = new TripViewModel();
            var userName = "Some UserName";

            // Act
            tripItemViewModel.OrganiserUsername = userName;

            // Assert
            Assert.AreEqual(userName, tripItemViewModel.OrganiserUsername);
        }

        [Test]
        public void OrganizerFirstName_ShouldGetSetCorrectValue()
        {
            // Arrange
            var tripItemViewModel = new TripViewModel();
            var fisrstName = "Some First Name";

            // Act
            tripItemViewModel.OrganiserFirstName = fisrstName;

            // Assert
            Assert.AreEqual(fisrstName, tripItemViewModel.OrganiserFirstName);
        }

        [Test]
        public void OrganizerLastName_ShouldGetSetCorrectValue()
        {
            // Arrange
            var tripItemViewModel = new TripViewModel();
            var lastName = "Some Last Name";

            // Act
            tripItemViewModel.OrganiserLastName = lastName;

            // Assert
            Assert.AreEqual(lastName, tripItemViewModel.OrganiserLastName);
        }
    }
}
