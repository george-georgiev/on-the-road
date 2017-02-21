using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using OnTheRoad.Data.Contracts;
using OnTheRoad.Data.Readers;
using System.IO;
using OnTheRoad.Data.Factories.Contracts;

namespace OnTheRoad.Data.Tests.Readers
{
    [TestFixture]
    public class TextDataReaderTests
    {
        [Test]
        public void WhenFileReaderFactoryPropertyIsCalled_ShouldReturnAnInstanceOfIFileReaderFactory()
        {
            var textDataReader = new TextDataReader();
            var actual = textDataReader.FileReaderFactory;

            Assert.That(actual, Is.InstanceOf<IFileReaderFactory>());
        }

        [Test]
        public void WhenFileReaderFactoryPropertySetWithNull_ShouldThrowArgumentNullException()
        {
            var textDataReader = new TextDataReader();

            Assert.Throws<ArgumentNullException>(() => textDataReader.FileReaderFactory = null);
        }

        [Test]
        public void WhenFileReaderFactoryPropertySetWithNull_ShouldThrowArgumentNullExceptionWithProperMessage()
        {
            var textDataReader = new TextDataReader();

            var exc = Assert.Throws<ArgumentNullException>(() => textDataReader.FileReaderFactory = null);
            StringAssert.Contains("fileReaderFactory cannot be null!", exc.ParamName);
        }

        [Test]
        public void WhenFileReaderFactoryPropertySetWithProperInstance_ShouldSetInstanceOfIFileReaderFactory()
        {
            var factoryMock = new Mock<IFileReaderFactory>();

            var textDataReader = new TextDataReader();
            textDataReader.FileReaderFactory = factoryMock.Object;
            var actual = textDataReader.FileReaderFactory;
            Assert.That(actual, Is.InstanceOf<IFileReaderFactory>());
        }

        [Test]
        public void WhenResourcePathResolverPropertyIsCalled_ShouldReturnAnInstanceOfIResourcePathResolver()
        {
            var textDataReader = new TextDataReader();
            var actual = textDataReader.ResourcePathResolver;

            Assert.That(actual, Is.InstanceOf<IResourcePathResolver>());
        }

        [Test]
        public void WhenResourcePathResolverPropertySetWithNull_ShouldThrowArgumentNullException()
        {
            var textDataReader = new TextDataReader();

            Assert.Throws<ArgumentNullException>(() => textDataReader.ResourcePathResolver = null);
        }

        [Test]
        public void WhenResourcePathResolverPropertySetWithNull_ShouldThrowArgumentNullExceptionWithProperMessage()
        {
            var textDataReader = new TextDataReader();

            var exc = Assert.Throws<ArgumentNullException>(() => textDataReader.ResourcePathResolver = null);
            StringAssert.Contains("resourcePathResolver cannot be null!", exc.ParamName);
        }

        [Test]
        public void WhenResourcePathResolverSetWithProperInstance_ShouldSetInstanceOfIResourcePathResolver()
        {
            var resolverMock = new Mock<IResourcePathResolver>();

            var textDataReader = new TextDataReader();
            textDataReader.ResourcePathResolver = resolverMock.Object;
            var actual = textDataReader.ResourcePathResolver;

            Assert.That(actual, Is.InstanceOf<IResourcePathResolver>());
        }

        [Test]
        public void ResourcePathResolver_WhenReadCategoriesIsCalled_ShouldCallResolveCategoriesFilePathExactlyOnce()
        {
            string fileName = "notImportant";
            var resolverMock = new Mock<IResourcePathResolver>();
            resolverMock.Setup(x => x.ResolveCategoriesFilePath()).Returns(fileName);
            var factoryMock = new Mock<IFileReaderFactory>();
            factoryMock.Setup(x => x.GetStreamReader(It.IsAny<string>())).Returns(new StringReader(fileName));

            var textDataReader = new TextDataReader();
            textDataReader.ResourcePathResolver = resolverMock.Object;
            textDataReader.FileReaderFactory = factoryMock.Object;

            var result = textDataReader.ReadCategories();

            resolverMock.Verify(x => x.ResolveCategoriesFilePath(), Times.Once);
        }

        [Test]
        public void FileReaderFactory_WhenReadCategoriesIsCalled_ShouldCallGetStreamReaderExactlyOnce()
        {
            string fileName = "notImportant";
            var resolverMock = new Mock<IResourcePathResolver>();
            resolverMock.Setup(x => x.ResolveCategoriesFilePath()).Returns(fileName);
            var factoryMock = new Mock<IFileReaderFactory>();
            factoryMock.Setup(x => x.GetStreamReader(It.IsAny<string>())).Returns(new StringReader(fileName));

            var textDataReader = new TextDataReader();
            textDataReader.ResourcePathResolver = resolverMock.Object;
            textDataReader.FileReaderFactory = factoryMock.Object;

            var result = textDataReader.ReadCategories();

            factoryMock.Verify(x => x.GetStreamReader(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void FileReaderFactory_WhenReadCategoriesIsCalled_ShouldCallGetStreamReaderWithFileNameReturnFromResourcePathResolver()
        {
            string fileName = "notImportant";
            var resolverMock = new Mock<IResourcePathResolver>();
            resolverMock.Setup(x => x.ResolveCategoriesFilePath()).Returns(fileName);
            var factoryMock = new Mock<IFileReaderFactory>();
            factoryMock.Setup(x => x.GetStreamReader(It.IsAny<string>())).Returns(new StringReader(fileName));

            var textDataReader = new TextDataReader();
            textDataReader.ResourcePathResolver = resolverMock.Object;
            textDataReader.FileReaderFactory = factoryMock.Object;

            var result = textDataReader.ReadCategories();

            factoryMock.Verify(x => x.GetStreamReader(fileName), Times.Once);
        }

        [Test]
        public void FileReaderFactory_WhenReadCategoriesIsCalled_ShouldReturnIEnumerableFromString()
        {
            string fileName = "notImportant";
            var resolverMock = new Mock<IResourcePathResolver>();
            resolverMock.Setup(x => x.ResolveCategoriesFilePath()).Returns(fileName);
            var factoryMock = new Mock<IFileReaderFactory>();
            factoryMock.Setup(x => x.GetStreamReader(It.IsAny<string>())).Returns(new StringReader(fileName));

            var textDataReader = new TextDataReader();
            textDataReader.ResourcePathResolver = resolverMock.Object;
            textDataReader.FileReaderFactory = factoryMock.Object;

            var actual = textDataReader.ReadCategories();

            Assert.That(actual, Is.InstanceOf<IEnumerable<string>>());
        }

        [Test]
        public void ResourcePathResolver_WhenReadCitiesIsCalled_ShouldCallResolveCitiesFilePathExactlyOnce()
        {
            string fileName = "notImportant";
            var resolverMock = new Mock<IResourcePathResolver>();
            resolverMock.Setup(x => x.ResolveCitiesFilePath()).Returns(fileName);
            var factoryMock = new Mock<IFileReaderFactory>();
            factoryMock.Setup(x => x.GetStreamReader(It.IsAny<string>())).Returns(new StringReader(fileName));

            var textDataReader = new TextDataReader();
            textDataReader.ResourcePathResolver = resolverMock.Object;
            textDataReader.FileReaderFactory = factoryMock.Object;

            var result = textDataReader.ReadCities();

            resolverMock.Verify(x => x.ResolveCitiesFilePath(), Times.Once);
        }

        [Test]
        public void FileReaderFactory_WhenReadCitiesIsCalled_ShouldCallGetStreamReaderExactlyOnce()
        {
            string fileName = "notImportant";
            var resolverMock = new Mock<IResourcePathResolver>();
            resolverMock.Setup(x => x.ResolveCitiesFilePath()).Returns(fileName);
            var factoryMock = new Mock<IFileReaderFactory>();
            factoryMock.Setup(x => x.GetStreamReader(It.IsAny<string>())).Returns(new StringReader(fileName));

            var textDataReader = new TextDataReader();
            textDataReader.ResourcePathResolver = resolverMock.Object;
            textDataReader.FileReaderFactory = factoryMock.Object;

            var result = textDataReader.ReadCities();

            factoryMock.Verify(x => x.GetStreamReader(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void FileReaderFactory_WhenReadCitiesIsCalled_ShouldCallGetStreamReaderWithFileNameReturnFromResourcePathResolver()
        {
            string fileName = "notImportant";
            var resolverMock = new Mock<IResourcePathResolver>();
            resolverMock.Setup(x => x.ResolveCitiesFilePath()).Returns(fileName);
            var factoryMock = new Mock<IFileReaderFactory>();
            factoryMock.Setup(x => x.GetStreamReader(It.IsAny<string>())).Returns(new StringReader(fileName));

            var textDataReader = new TextDataReader();
            textDataReader.ResourcePathResolver = resolverMock.Object;
            textDataReader.FileReaderFactory = factoryMock.Object;

            var result = textDataReader.ReadCities();

            factoryMock.Verify(x => x.GetStreamReader(fileName), Times.Once);
        }

        [Test]
        public void FileReaderFactory_WhenReadCitesIsCalled_ShouldReturnIEnumerableFromString()
        {
            string fileName = "notImportant";
            var resolverMock = new Mock<IResourcePathResolver>();
            resolverMock.Setup(x => x.ResolveCitiesFilePath()).Returns(fileName);
            var factoryMock = new Mock<IFileReaderFactory>();
            factoryMock.Setup(x => x.GetStreamReader(It.IsAny<string>())).Returns(new StringReader(fileName));

            var textDataReader = new TextDataReader();
            textDataReader.ResourcePathResolver = resolverMock.Object;
            textDataReader.FileReaderFactory = factoryMock.Object;

            var actual = textDataReader.ReadCities();

            Assert.That(actual, Is.InstanceOf<IEnumerable<string>>());
        }

        [Test]
        public void ResourcePathResolver_WhenReadRatingIsCalled_ShouldCallResolveRatingsFilePathExactlyOnce()
        {
            string fileName = "notImportant";
            var resolverMock = new Mock<IResourcePathResolver>();
            resolverMock.Setup(x => x.ResolveRatingsFilePath()).Returns(fileName);
            var factoryMock = new Mock<IFileReaderFactory>();
            factoryMock.Setup(x => x.GetStreamReader(It.IsAny<string>())).Returns(new StringReader(fileName));

            var textDataReader = new TextDataReader();
            textDataReader.ResourcePathResolver = resolverMock.Object;
            textDataReader.FileReaderFactory = factoryMock.Object;

            var result = textDataReader.ReadRatings();

            resolverMock.Verify(x => x.ResolveRatingsFilePath(), Times.Once);
        }

        [Test]
        public void FileReaderFactory_WhenReadRatingIsCalled_ShouldCallGetStreamReaderExactlyOnce()
        {
            string fileName = "notImportant";
            var resolverMock = new Mock<IResourcePathResolver>();
            resolverMock.Setup(x => x.ResolveRatingsFilePath()).Returns(fileName);
            var factoryMock = new Mock<IFileReaderFactory>();
            factoryMock.Setup(x => x.GetStreamReader(It.IsAny<string>())).Returns(new StringReader(fileName));

            var textDataReader = new TextDataReader();
            textDataReader.ResourcePathResolver = resolverMock.Object;
            textDataReader.FileReaderFactory = factoryMock.Object;

            var result = textDataReader.ReadRatings();

            factoryMock.Verify(x => x.GetStreamReader(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void FileReaderFactory_WhenReadRatingIsCalled_ShouldCallGetStreamReaderWithFileNameReturnFromResourcePathResolver()
        {
            string fileName = "notImportant";
            var resolverMock = new Mock<IResourcePathResolver>();
            resolverMock.Setup(x => x.ResolveRatingsFilePath()).Returns(fileName);
            var factoryMock = new Mock<IFileReaderFactory>();
            factoryMock.Setup(x => x.GetStreamReader(It.IsAny<string>())).Returns(new StringReader(fileName));

            var textDataReader = new TextDataReader();
            textDataReader.ResourcePathResolver = resolverMock.Object;
            textDataReader.FileReaderFactory = factoryMock.Object;

            var result = textDataReader.ReadRatings();

            factoryMock.Verify(x => x.GetStreamReader(fileName), Times.Once);
        }

        [Test]
        public void FileReaderFactory_WhenReadRatingIsCalled_ShouldReturnIEnumerableFromString()
        {
            string fileName = "notImportant";
            var resolverMock = new Mock<IResourcePathResolver>();
            resolverMock.Setup(x => x.ResolveRatingsFilePath()).Returns(fileName);
            var factoryMock = new Mock<IFileReaderFactory>();
            factoryMock.Setup(x => x.GetStreamReader(It.IsAny<string>())).Returns(new StringReader(fileName));

            var textDataReader = new TextDataReader();
            textDataReader.ResourcePathResolver = resolverMock.Object;
            textDataReader.FileReaderFactory = factoryMock.Object;

            var actual = textDataReader.ReadRatings();

            Assert.That(actual, Is.InstanceOf<IEnumerable<string>>());
        }
    }
}
