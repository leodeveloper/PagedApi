// DO NOT MODIFY THIS FILE
using System.Collections.Generic;

namespace PagedApi
{
	/// <summary>
	/// Represents a single page of items for a request
	/// </summary>
	public class Page
	{
		/// <summary>
		/// The Id of the request this page is for
		/// </summary>
		public int RequestId { get; set; }

		/// <summary>
		/// The type of items this request is for
		/// </summary>
		public ItemTypeId ItemTypeId { get; set; }

		/// <summary>
		/// Signifies if there are more pages to follow for this request
		/// </summary>
		public bool HasNextPage { get; set; }

		/// <summary>
		/// The items returned for this page
		/// </summary>
		public IEnumerable<object> Items { get; set; }
	}
}