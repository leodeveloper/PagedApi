using System;
using System.Collections.Generic;
using System.Linq;
using PagedApi;

namespace PagedApiCollection.Implementation.Tests
{
	public class FakePagedApi : IPagedApi
	{
		private readonly int _pageSize;
		private readonly IReadOnlyDictionary<ItemTypeId, List<object>> _itemsById;
		private readonly IDictionary<int, Request> _requestsById;

		private int _lastRequestId;

		internal List<object> this[ItemTypeId itemTypeId] => _itemsById[itemTypeId];
		internal IEnumerable<Request> InProgressRequests => _requestsById.Values;

		public FakePagedApi(int pageSize)
		{
			_pageSize = pageSize;
			_itemsById = Enum.GetValues(typeof(ItemTypeId)).Cast<ItemTypeId>().ToDictionary(i => i, i => new List<object>());
			_requestsById = new Dictionary<int, Request>();
		}

		public int BeginPagesRequest(ItemTypeId itemTypeId)
		{
			_lastRequestId++;
			_requestsById.Add(_lastRequestId, new Request(_lastRequestId, itemTypeId));

			return _lastRequestId;
		}

		public Page GetNextPage(int requestId)
		{
			var request = _requestsById[requestId];
			var allItems = _itemsById[request.ItemTypeId];

			return request.GetNextPageFrom(allItems, _pageSize);
		}

		public void EndPagesRequest(int requestId)
		{
			if (!_requestsById.Remove(requestId))
				throw new ArgumentException($"Unknown {nameof(requestId)}: {requestId}");
		}

		internal class Request
		{
			private int _currentIndex;

			public int RequestId { get; }
			public ItemTypeId ItemTypeId { get; }
			public int PagesFetched { get; private set; }

			public Request(int requestId, ItemTypeId itemTypeId)
			{
				RequestId = requestId;
				ItemTypeId = itemTypeId;
			}

			protected internal Page GetNextPageFrom(List<object> allItems, int pageSize)
			{
				var remainingItemCount = allItems.Count - _currentIndex;
				var currPageSize = Math.Min(remainingItemCount, pageSize);
				var hasNextPage = remainingItemCount > pageSize;

				var items = currPageSize == 0
					? Enumerable.Empty<object>()
					: allItems.GetRange(_currentIndex, currPageSize);

				PagesFetched++;
				_currentIndex += currPageSize;

				return new Page
				{
					RequestId = RequestId,
					ItemTypeId = ItemTypeId,
					HasNextPage = hasNextPage,
					Items = items
				};
			}
		}
	}
}