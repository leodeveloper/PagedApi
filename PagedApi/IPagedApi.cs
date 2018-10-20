// DO NOT MODIFY THIS FILE
namespace PagedApi
{
	/// <summary>
	/// A client wrapper around a 3rd party API, which returns items in pages.
	/// </summary>
	public interface IPagedApi
	{
		/// <summary>
		/// Establishes a request to retrieve items of the given type
		/// </summary>
		/// <param name="itemTypeId">The type of item to be retrieved</param>
		/// <returns>A unique RequestId to be used to retrieve pages of the items</returns>
		int BeginPagesRequest(ItemTypeId itemTypeId);

		/// <summary>
		/// Gets the next page of items, associated with the given RequestId
		/// </summary>
		/// <param name="requestId">The RequestId from which the items should be sourced</param>
		/// <returns>The next available page of items</returns>
		Page GetNextPage(int requestId);

		/// <summary>
		/// Ends the request associated with the given RequestId.
		/// Signifies to the API that it can reclaim resources associated with keeping the request open.
		/// </summary>
		/// <param name="requestId">The Id of the request to end</param>
		void EndPagesRequest(int requestId);
	}
}
