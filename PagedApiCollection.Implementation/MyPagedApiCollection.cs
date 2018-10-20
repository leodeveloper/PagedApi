using System;
using System.Collections.Generic;
using System.Linq;
using PagedApi;

namespace PagedApiCollection.Implementation
{


    public class MyPagedApiCollection : IPagedApiCollection
    {
        private readonly IPagedApi _pagedApi;

        public MyPagedApiCollection(IPagedApi pagedApi)
        {
            _pagedApi = pagedApi;
        }

        public IEnumerableDisposable<TItem> GetItems<TItem>()
        {
            IEnumerable<TItem> items = getPageItems<TItem>();
            IEnumerableDisposable<TItem> pageItems = new EnumerableDisposable<TItem>(items);
            return pageItems;
        }

        #region Private Methods

        /// <summary>
        /// * If the consumer never enumerates the response from `IPagedApiCollection.GetItems()`, ensure `IPagedApi.BeginPagesRequest()` is not unnecessarily called.
        /// * Once the consumer is finished with the request, ensure `IPagedApi.EndPagesRequest()` is called.
        /// * You discover a quirk concerning `IPagedApi.GetNextPage()`, where sometimes the returned `Page.Items` collection can be empty. Ensure this is gracefully handled within your implementation.
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        private IEnumerable<TItem> getPageItems<TItem>()
        {
            IEnumerable<TItem> items = Enumerable.Empty<TItem>();
            ItemTypeId value = (ItemTypeId)Enum.Parse(typeof(ItemTypeId), typeof(TItem).Name);
            _pagedApi.BeginPagesRequest(value);
            Page page = _pagedApi.GetNextPage(1);
            _pagedApi.EndPagesRequest(page.RequestId);

            if (page?.Items.Count() > 0)
            {
                items = page.Items.Cast<TItem>();
            }

            return items;
        }

        #endregion
    }
}
