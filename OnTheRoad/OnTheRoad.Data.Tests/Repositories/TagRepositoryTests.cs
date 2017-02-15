using NUnit.Framework;
using OnTheRoad.Data.Repositories;
using System;

namespace OnTheRoad.Data.Tests.Repositories
{
    [TestFixture]
    public class TagRepositoryTests
    {
        [Test]
        public void Constructor_WhenDbContextIsNull_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new TagRepository(null));
        }
    }
}
