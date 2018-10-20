// DO NOT MODIFY THIS FILE
namespace PagedApiCollection
{
	public interface IPagedApiCollection
	{
		IEnumerableDisposable<TItem> GetItems<TItem>();
	}
}