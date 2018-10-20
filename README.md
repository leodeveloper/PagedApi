# PagedApi Test

## Scenario

You are working on a project which must integrate heavily with a 3rd party API.
The 3rd party have provided you with a client library to access this API, contained within the `PagedApi` project.

The API currently exposes the ability to retreive `Foo` and `Bar` items, which are represented by the `ItemTypeId.Foo` and `ItemTypeId.Bar` enum respectively.

Fetching items works using a statefull 'request' model. The caller must first establish a request for a particular type of item by calling `int 
BeginPagesRequest(ItemTypeId itemTypeId)`, which returns a unique RequestId for the given `ItemTypeId`.

Items are then fetched one page at a time, by passing the RequestId to `Page GetNextPage(int requestId)`. The size of each page is determined by the API.
The returned `Page` object will contain the collection of items for this page, and a boolean indicating if there are more pages to follow.

Once all desired items have been retrieved, the caller should call `void EndPagesRequest(int requestId)`, 
signalling to the API that it may now clean up any resources associated with the request.

## Task

The `IPagedApiCollection` interface has been proposed by your team as a way of simplifing access to `IPagedApi` throughout the project.
Designed to wrap `IPagedApi`, it contains only a single method: `GetItems<>()`.

Complete the implementation of `MyPagedApiCollection`.

## Considerations

Consider the following points when designing your implementation.
You should attempt to address at least 2 of them.

* The API call `IPagedApi.GetNextPage()` may take a long time to complete. If the consumer only enumerates the first few items of the `IPagedApiCollection.GetItems()` response, 
ensure `IPagedApi.GetNextPage()` is called as few times as possible.
* If the consumer never enumerates the response from `IPagedApiCollection.GetItems()`, ensure `IPagedApi.BeginPagesRequest()` is not unnecessarily called.
* Once the consumer is finished with the request, ensure `IPagedApi.EndPagesRequest()` is called.
* You discover a quirk concerning `IPagedApi.GetNextPage()`, where sometimes the returned `Page.Items` collection can be empty. Ensure this is gracefully handled within your implementation.
* What happens when the response from `IPagedApiCollection.GetItems()` is enumeratad by the consumer multiple times?
* What happens when the response from `IPagedApiCollection.GetItems()` is enumerated after it has been disposed?