namespace VibeCRM.Shared.Models
{
    /// <summary>
    /// Represents a paged list of items for efficient pagination in query results.
    /// </summary>
    /// <typeparam name="T">The type of items in the paged list.</typeparam>
    public class PagedList<T>
    {
        /// <summary>
        /// Gets or sets the items in the current page.
        /// </summary>
        public IEnumerable<T> Items { get; set; } = new List<T>();

        /// <summary>
        /// Gets or sets the current page number (1-based).
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the size of each page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the total count of items across all pages.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Gets the total number of pages based on the total count and page size.
        /// </summary>
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        /// <summary>
        /// Gets a value indicating whether there is a previous page.
        /// </summary>
        public bool HasPreviousPage => PageNumber > 1;

        /// <summary>
        /// Gets a value indicating whether there is a next page.
        /// </summary>
        public bool HasNextPage => PageNumber < TotalPages;

        /// <summary>
        /// Creates a new instance of the <see cref="PagedList{T}"/> class.
        /// </summary>
        public PagedList()
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="PagedList{T}"/> class with the specified parameters.
        /// </summary>
        /// <param name="items">The items in the current page.</param>
        /// <param name="totalCount">The total count of items across all pages.</param>
        /// <param name="pageNumber">The current page number (1-based).</param>
        /// <param name="pageSize">The size of each page.</param>
        public PagedList(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}