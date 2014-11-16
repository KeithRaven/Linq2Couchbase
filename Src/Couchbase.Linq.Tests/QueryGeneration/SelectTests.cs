﻿using Couchbase.Core;
using Couchbase.Linq.Tests.Documents;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace Couchbase.Linq.Tests.QueryGeneration
{
    [TestFixture]
    public class SelectTests : N1QLTestBase
    {
        [Test]
        public void Test_Select_With_Projection()
        {
            var mockBucket = new Mock<IBucket>();
            mockBucket.SetupGet(e => e.Name).Returns("default");

            var query =
                QueryFactory.Queryable<Contact>(mockBucket.Object)
                    .Select(e => new { age = e.Age, name = e.FirstName });

            const string expected = "SELECT e.age as age, e.fname as name FROM default as e";

            var n1QlQuery = CreateN1QlQuery(mockBucket.Object, query.Expression);

            Assert.AreEqual(expected, n1QlQuery);
        }

        [Test]
        public void Test_Select_All_Properties()
        {
            var mockBucket = new Mock<IBucket>();
            mockBucket.SetupGet(e => e.Name).Returns("default");

            var query =
                QueryFactory.Queryable<Contact>(mockBucket.Object)
                    .Select(e => e);

            const string expected = "SELECT e.* FROM default as e";

            var n1QlQuery = CreateN1QlQuery(mockBucket.Object, query.Expression);

            Assert.AreEqual(expected, n1QlQuery);
        }
    }
}
