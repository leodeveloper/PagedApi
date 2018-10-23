using System.Linq;
using NUnit.Framework;
using PagedApi;

namespace PagedApiCollection.Implementation.Tests
{
	public class MyPagedApiCollectionTests
	{
		private FakePagedApi _fakePagedApi;
		private IPagedApiCollection _pagedApiCollection;

		[SetUp]
		public void Setup()
		{
			_fakePagedApi = new FakePagedApi(5);
			_pagedApiCollection = new MyPagedApiCollection(_fakePagedApi);
		}

		[Test]
		public void GetItems_ToArray_ReturnsAllItems()
		{
			// Arrange
			var expectedItems = Enumerable.Range(0, 400000).Select(i => new Foo {Id = i}).ToArray();

			_fakePagedApi[ItemTypeId.Foo].AddRange(expectedItems);

			// Act
			var actualItems = _pagedApiCollection.GetItems<Foo>().ToArray();

			// Assert
			CollectionAssert.AreEqual(expectedItems, actualItems);
		}

        [Test]
        public void GetItems_Returns_No_Items()
        {
            // Arrange
            var expectedItems = Enumerable.Range(0, 0).Select(i => new Foo { Id = i }).ToArray();

            _fakePagedApi[ItemTypeId.Foo].AddRange(expectedItems);

            // Act
            var actualItems = _pagedApiCollection.GetItems<Foo>().ToArray();

            // Assert
            CollectionAssert.AreEqual(expectedItems, actualItems);
        }
    }
}
