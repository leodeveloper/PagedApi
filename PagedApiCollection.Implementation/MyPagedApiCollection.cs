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
            using (IEnumerableDisposable<TItem> pageItems = new EnumerableDisposable<TItem>(items))
            {
                return pageItems;
            }
                
        }

        #region Private Methods

        /// <summary>
        /// * If the consumer never enumerates the response from `IPagedApiCollection.GetItems()`, ensure `IPagedApi.BeginPagesRequest()` is not unnecessarily called.
        /// * Once the consumer is finished with the request, ensure `IPagedApi.EndPagesRequest()` is called.
        /// * You discover a quirk concerning `IPagedApi.GetNextPage()`, where sometimes the returned `Page.Items` collection can be empty. Ensure this is gracefully handled within your implementation.
        /// </summary>
        /// <typeparam name="TItem">IEnumerable of item oftype TItem</typeparam>
        /// <returns></returns>
        private IEnumerable<TItem> getPageItems<TItem>()
        {
            List<TItem> items = new List<TItem>();
            ItemTypeId itemType;
            if (Enum.TryParse<ItemTypeId>(typeof(TItem).Name, out itemType))
            {
                int requestId = _pagedApi.BeginPagesRequest(itemType);
                Page page = new Page();
                do
                {
                    page = _pagedApi.GetNextPage(requestId);
                    items.AddRange(page.Items.Cast<TItem>());                   

                } while (page.HasNextPage); 
                _pagedApi.EndPagesRequest(page.RequestId);               
            }
            else
            {
                //need to implement logging here if the ItemTypeId is not valid or throw an exception
            }
            return items;
        }
        #endregion
    }
}
