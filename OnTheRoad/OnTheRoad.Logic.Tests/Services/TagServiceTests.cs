using Moq;
using NUnit.Framework;
using OnTheRoad.Domain.Models;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Logic.Factories;
using OnTheRoad.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnTheRoad.Logic.Tests.Services
{
    [TestFixture]
    public class TagServiceTests
    {
        [Test]
        public void Constructor_WhenTagDataUtilIsNull_ShouldThrow()
        {
            var tagFactoryMock = new Mock<ITagFactory>();

            Assert.Throws<ArgumentNullException>(() => new TagService(null, tagFactoryMock.Object));
        }

        [Test]
        public void Constructor_WhenTagFactoryIsNull_ShouldThrow()
        {
            var tagDataUtilMock = new Mock<ITagDataUtil>();

            Assert.Throws<ArgumentNullException>(() => new TagService(tagDataUtilMock.Object, null));
        }

        [Test]
        public void Constructor_WhenCorrectParametersPassed_ShouldReturnInstance()
        {
            var tagDataUtilMock = new Mock<ITagDataUtil>();
            var tagFactoryMock = new Mock<ITagFactory>();

            var tagService = new TagService(tagDataUtilMock.Object, tagFactoryMock.Object);

            Assert.IsInstanceOf<TagService>(tagService);
            Assert.IsNotNull(tagService);
        }

        [Test]
        public void GetOrCreateTags_WhenTagNamesIsNull_ShouldThrow()
        {
            var tagDataUtilMock = new Mock<ITagDataUtil>();
            var tagFactoryMock = new Mock<ITagFactory>();

            var tagService = new TagService(tagDataUtilMock.Object, tagFactoryMock.Object);

            Assert.Throws<ArgumentNullException>(() => tagService.GetOrCreateTags(null));
        }

        [TestCase("TagName1", "TagName2")]
        [TestCase("OtherTagName1", "OtherTagName2")]
        public void GetOrCreateTags_WhenTagsWithTagNamesExist_ShouldCallGetTagByNameForEachTagName(string tagName1, string tagName2)
        {
            var tagDataUtilMock = new Mock<ITagDataUtil>();
            var tagFactoryMock = new Mock<ITagFactory>();
            var tagMock = new Mock<ITag>();
            tagDataUtilMock.Setup(x => x.GetTagByName(It.IsAny<string>())).Returns(tagMock.Object);
            var tagService = new TagService(tagDataUtilMock.Object, tagFactoryMock.Object);
            var tagNames = new List<string>() { tagName1, tagName2 };

            tagService.GetOrCreateTags(tagNames);
            
            tagDataUtilMock.Verify(x => x.GetTagByName(It.Is<string>(n => n == tagName1)), Times.Once);
            tagDataUtilMock.Verify(x => x.GetTagByName(It.Is<string>(n => n == tagName2)), Times.Once);
        }

        [TestCase("TagName1", "TagName2")]
        [TestCase("OtherTagName1", "OtherTagName2", "OtherTagName3")]
        public void GetOrCreateTags_WhenTagsWithTagNamesExist_ShouldCallGetTagByNameExactTimes(params string[] tagNames)
        {
            var tagDataUtilMock = new Mock<ITagDataUtil>();
            var tagFactoryMock = new Mock<ITagFactory>();
            var tagMock = new Mock<ITag>();
            tagDataUtilMock.Setup(x => x.GetTagByName(It.IsAny<string>())).Returns(tagMock.Object);
            var tagService = new TagService(tagDataUtilMock.Object, tagFactoryMock.Object);

            tagService.GetOrCreateTags(tagNames);
            
            tagDataUtilMock.Verify(x => x.GetTagByName(It.IsAny<string>()), Times.Exactly(tagNames.Length));
        }

        [TestCase("TagName1", "TagName2")]
        [TestCase("OtherTagName1", "OtherTagName2")]
        public void GetOrCreateTags_WhenTagsWithTagNamesDoesNotExist_ShouldCallGetTagByNameForEachTagNameTwice(string tagName1, string tagName2)
        {
            var tagDataUtilMock = new Mock<ITagDataUtil>();
            var tagFactoryMock = new Mock<ITagFactory>();

            var tagMock1 = new Mock<ITag>();
            tagMock1.SetupGet(x => x.Name).Returns(tagName1);
            tagFactoryMock.Setup(x => x.CreateTag(It.Is<string>(n => n == tagName1))).Returns(tagMock1.Object);

            var tagMock2 = new Mock<ITag>();
            tagMock2.SetupGet(x => x.Name).Returns(tagName2);
            tagFactoryMock.Setup(x => x.CreateTag(It.Is<string>(n => n == tagName2))).Returns(tagMock2.Object);

            var tagService = new TagService(tagDataUtilMock.Object, tagFactoryMock.Object);
            var tagNames = new List<string>() { tagName1, tagName2 };


            tagService.GetOrCreateTags(tagNames);


            tagDataUtilMock.Verify(x => x.GetTagByName(It.Is<string>(n => n == tagName1)), Times.Exactly(2));
            tagDataUtilMock.Verify(x => x.GetTagByName(It.Is<string>(n => n == tagName2)), Times.Exactly(2));
        }

        [TestCase("TagName1", "TagName2")]
        [TestCase("OtherTagName1", "OtherTagName2", "OtherTagName3")]
        public void GetOrCreateTags_WhenTagsWithTagNamesDoesNotExist_ShouldCallGetTagByNameExactTimes(params string[] tagNames)
        {
            var tagDataUtilMock = new Mock<ITagDataUtil>();
            var tagFactoryMock = new Mock<ITagFactory>();
            var tagMock = new Mock<ITag>();
            tagFactoryMock.Setup(x => x.CreateTag(It.IsAny<string>())).Returns(tagMock.Object);
            var tagService = new TagService(tagDataUtilMock.Object, tagFactoryMock.Object);

            tagService.GetOrCreateTags(tagNames);

            var timesToCall = tagNames.Length * 2;
            tagDataUtilMock.Verify(x => x.GetTagByName(It.IsAny<string>()), Times.Exactly(timesToCall));
        }

        [TestCase("TagName1", "TagName2")]
        [TestCase("OtherTagName1", "OtherTagName2")]
        public void GetOrCreateTags_WhenTagsWithTagNamesDoesNotExist_ShouldCallCreateTagForEachTagName(string tagName1, string tagName2)
        {
            var tagDataUtilMock = new Mock<ITagDataUtil>();
            var tagFactoryMock = new Mock<ITagFactory>();
            var tagMock = new Mock<ITag>();
            tagFactoryMock.Setup(x => x.CreateTag(It.IsAny<string>())).Returns(tagMock.Object);

            var tagService = new TagService(tagDataUtilMock.Object, tagFactoryMock.Object);
            var tagNames = new List<string>() { tagName1, tagName2 };


            tagService.GetOrCreateTags(tagNames);


            tagFactoryMock.Verify(x => x.CreateTag(It.Is<string>(n => n == tagName1)), Times.Once);
            tagFactoryMock.Verify(x => x.CreateTag(It.Is<string>(n => n == tagName2)), Times.Once);
        }

        [TestCase("TagName1", "TagName2")]
        [TestCase("OtherTagName1", "OtherTagName2")]
        public void GetOrCreateTags_WhenTagsWithTagNamesDoesNotExist_ShouldCallAddTagForEachTagCreated(string tagName1, string tagName2)
        {
            var tagDataUtilMock = new Mock<ITagDataUtil>();
            var tagFactoryMock = new Mock<ITagFactory>();

            var tagMock1 = new Mock<ITag>();
            tagFactoryMock.Setup(x => x.CreateTag(It.Is<string>(n => n == tagName1))).Returns(tagMock1.Object);

            var tagMock2 = new Mock<ITag>();
            tagFactoryMock.Setup(x => x.CreateTag(It.Is<string>(n => n == tagName2))).Returns(tagMock2.Object);

            var tagService = new TagService(tagDataUtilMock.Object, tagFactoryMock.Object);
            var tagNames = new List<string>() { tagName1, tagName2 };


            tagService.GetOrCreateTags(tagNames);


            tagDataUtilMock.Verify(x => x.AddTag(It.Is<ITag>(t => t.Equals(tagMock1.Object))), Times.Once);
            tagDataUtilMock.Verify(x => x.AddTag(It.Is<ITag>(t => t.Equals(tagMock2.Object))), Times.Once);
        }

        [TestCase("TagName1", "TagName2")]
        [TestCase("OtherTagName1", "OtherTagName2", "OtherTagName3")]
        public void GetOrCreateTags_WhenTagsWithTagNamesDoesNotExist_ShouldCallAddTagForExactTimes(params string[] tagNames)
        {
            var tagDataUtilMock = new Mock<ITagDataUtil>();
            var tagFactoryMock = new Mock<ITagFactory>();
            var tagMock = new Mock<ITag>();
            tagFactoryMock.Setup(x => x.CreateTag(It.IsAny<string>())).Returns(tagMock.Object);
            var tagService = new TagService(tagDataUtilMock.Object, tagFactoryMock.Object);

            tagService.GetOrCreateTags(tagNames);

            tagDataUtilMock.Verify(x => x.AddTag(It.IsAny<ITag>()), Times.Exactly(tagNames.Length));
        }

        [TestCase("TagName1", "TagName2")]
        [TestCase("OtherTagName1", "OtherTagName2")]
        public void GetOrCreateTags_WhenTagNamesArePassed_ShouldReturnCorrectTagsCollection(string tagName1, string tagName2)
        {
            var tagDataUtilMock = new Mock<ITagDataUtil>();
            var tagFactoryMock = new Mock<ITagFactory>();

            var tagMock1 = new Mock<ITag>();
            tagDataUtilMock.Setup(x => x.GetTagByName(It.Is<string>(n => n == tagName1))).Returns(tagMock1.Object);

            var tagMock2 = new Mock<ITag>();
            tagDataUtilMock.Setup(x => x.GetTagByName(It.Is<string>(n => n == tagName2))).Returns(tagMock2.Object);

            var tagService = new TagService(tagDataUtilMock.Object, tagFactoryMock.Object);
            var tagNames = new List<string>() { tagName1, tagName2 };


            var tags = tagService.GetOrCreateTags(tagNames).ToList();


            Assert.AreSame(tags[0], tagMock1.Object);
            Assert.AreSame(tags[1], tagMock2.Object);
        }

        [TestCase("TagName1", "TagName2")]
        [TestCase("OtherTagName1", "OtherTagName2", "OtherTagName3")]
        public void GetOrCreateTags_WhenTagNamesArePassed_ShouldReturnCorrectTagsCount(params string[] tagNames)
        {
            var tagDataUtilMock = new Mock<ITagDataUtil>();
            var tagFactoryMock = new Mock<ITagFactory>();
            var tagMock = new Mock<ITag>();
            tagDataUtilMock.Setup(x => x.GetTagByName(It.IsAny<string>())).Returns(tagMock.Object);
            var tagService = new TagService(tagDataUtilMock.Object, tagFactoryMock.Object);

            var tags = tagService.GetOrCreateTags(tagNames).ToList();

            Assert.AreEqual(tags.Count, tagNames.Length);
        }

        [Test]
        public void GetTagsByNamePrefix_WhenNamePrefixIsNull_ShouldThrow()
        {
            var tagDataUtilMock = new Mock<ITagDataUtil>();
            var tagFactoryMock = new Mock<ITagFactory>();
            var tagService = new TagService(tagDataUtilMock.Object, tagFactoryMock.Object);
            var take = 5;

            Assert.Throws<ArgumentNullException>(() => tagService.GetTagsByNamePrefix(null, take));
        }

        [Test]
        public void GetTagsByNamePrefix_WhenCalled_ShouldCallGetTagsByNamePrefixWithCorrectParameters()
        {
            var tagDataUtilMock = new Mock<ITagDataUtil>();
            var tagFactoryMock = new Mock<ITagFactory>();
            var tagService = new TagService(tagDataUtilMock.Object, tagFactoryMock.Object);
            var take = 5;
            var prefix = "prefix";

            tagService.GetTagsByNamePrefix(prefix, take);

            tagDataUtilMock.Verify(x => x.GetTagsByNamePrefix(It.Is<string>(s => s == prefix), It.Is<int>(t => t == take)), Times.Once);
        }

        [Test]
        public void GetTagsByNamePrefix_WhenCalled_ShouldCallGetTagsByNamePrefixOnce()
        {
            var tagDataUtilMock = new Mock<ITagDataUtil>();
            var tagFactoryMock = new Mock<ITagFactory>();
            var tagService = new TagService(tagDataUtilMock.Object, tagFactoryMock.Object);
            var take = 5;
            var prefix = "prefix";

            tagService.GetTagsByNamePrefix(prefix, take);

            tagDataUtilMock.Verify(x => x.GetTagsByNamePrefix(It.IsAny<string>(), It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void GetTagsByNamePrefix_WhenCalled_ShouldReturnCorrectTags()
        {
            var tagDataUtilMock = new Mock<ITagDataUtil>();
            var take = 5;
            var prefix = "prefix";
            var tagMock = new Mock<ITag>();
            var tags = new List<ITag>() { tagMock.Object };
            tagDataUtilMock.Setup(x => x.GetTagsByNamePrefix(It.Is<string>(s => s == prefix), It.Is<int>(t => t == take))).Returns(tags);
            var tagFactoryMock = new Mock<ITagFactory>();
            var tagService = new TagService(tagDataUtilMock.Object, tagFactoryMock.Object);

            var tagsResult = tagService.GetTagsByNamePrefix(prefix, take).ToList();

            Assert.AreEqual(tags, tagsResult);
            Assert.AreSame(tagMock.Object, tagsResult[0]);
        }
    }
}
