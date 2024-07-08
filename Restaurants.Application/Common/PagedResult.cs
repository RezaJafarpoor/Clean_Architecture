using Restaurants.Application.DTOs;

namespace Restaurants.Application.Common;

public class PagedResult<T>
{
    public PagedResult(IEnumerable<T?> items, int totalItemCount, int pageSize, int pageNumber)
    {
        Items = items;
        TotalItemCount = totalItemCount;
        TotalPages =(int) Math.Ceiling(TotalItemCount / (double) pageSize);
        ItemFrom = pageSize * (pageNumber - 1) + 1;
        ItemTo = ItemFrom + pageSize -1;
    }
    public IEnumerable<T> Items { get; set; }
    public int TotalPages { get; set; }
    public int  TotalItemCount { get; set; }
    public int ItemFrom { get; set; }
    public int  ItemTo { get; set; }
    
}