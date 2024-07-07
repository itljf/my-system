using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace L;

public class PagedList<T> : List<T>, IPagedList<T>, IEnumerable<T>, IEnumerable, IPagedList
{
    public PagedList(IEnumerable<T> allItems, int pageIndex, int pageSize)
    {
      this.PageSize = pageSize;
      if (!(allItems is IList<T> objList))
        objList = (IList<T>) allItems.ToList<T>();
      IList<T> source = objList;
      this.TotalItemCount = source.Count<T>();
      this.CurrentPageIndex = pageIndex;
      this.AddRange(source.Skip<T>(this.StartItemIndex - 1).Take<T>(pageSize));
    }

    /// <summary>
    ///  Initializes a new instance of the PagedList class using current page items, current page index, page size and the total item count.
    /// </summary>
    /// <param name="currentPageItems">Object that contains data to be displayed on page, should implements IEnumerable&lt;T&gt; interface;</param>
    /// <param name="pageIndex">Current page index</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="totalItemCount">The total number of records that are available for paging</param>
    public PagedList(
      IEnumerable<T> currentPageItems,
      int pageIndex,
      int pageSize,
      int totalItemCount)
    {
      this.AddRange(currentPageItems);
      this.TotalItemCount = totalItemCount;
      this.CurrentPageIndex = pageIndex;
      this.PageSize = pageSize;
    }

    /// <summary>
    ///   Initializes a new instance of the PagedList class using the data to be paged, current page index and page size.
    /// </summary>
    /// <param name="allItems">All items to be paged</param>
    /// <param name="pageIndex">Current page index</param>
    /// <param name="pageSize">Page size</param>
    public PagedList(IQueryable<T> allItems, int pageIndex, int pageSize)
    {
      int count = (pageIndex - 1) * pageSize;
      this.AddRange((IEnumerable<T>) allItems.Skip<T>(count).Take<T>(pageSize));
      this.TotalItemCount = allItems.Count<T>();
      this.CurrentPageIndex = pageIndex;
      this.PageSize = pageSize;
    }

    /// <summary>
    ///   Initializes a new instance of the PagedList class using current page items, current page index, page size and the total item count.
    /// </summary>
    /// <param name="currentPageItems">Object that contains data to be displayed on page, should implements IQueryable&lt;T&gt; interface</param>
    /// <param name="pageIndex">Current page index</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="totalItemCount">The total number of records that are available for paging</param>
    public PagedList(
      IQueryable<T> currentPageItems,
      int pageIndex,
      int pageSize,
      int totalItemCount)
    {
      this.AddRange((IEnumerable<T>) currentPageItems);
      this.TotalItemCount = totalItemCount;
      this.CurrentPageIndex = pageIndex;
      this.PageSize = pageSize;
    }

    /// <summary>Gets or sets the current page index.</summary>
    public int CurrentPageIndex { get; set; }

    /// <summary>Gets or sets the number of data items that are displayed for each page of data.</summary>
    public int PageSize { get; set; }

    /// <summary>Gets or sets the total number of records that are available for paging.</summary>
    public int TotalItemCount { get; set; }

    /// <summary>Gets or sets the total number of data items that are available for paging.</summary>
    public int TotalPageCount => (int) Math.Ceiling((double) this.TotalItemCount / (double) this.PageSize);

    /// <summary>Gets the index of the first data item that is displayed on a page of data.</summary>
    public int StartItemIndex => (this.CurrentPageIndex - 1) * this.PageSize + 1;

    /// <summary>Gets the index of the last data item that is displayed on a page of data.</summary>
    public int EndItemIndex => this.TotalItemCount <= this.CurrentPageIndex * this.PageSize ? this.TotalItemCount : this.CurrentPageIndex * this.PageSize;
}
public interface IPagedList<T> : IEnumerable<T>, IEnumerable, IPagedList
{
}
public interface IPagedList : IEnumerable
{
    /// <summary>Gets or sets the current page index.</summary>
    int CurrentPageIndex { get; set; }

    /// <summary>Gets or sets the number of data items that are displayed for each page of data.</summary>
    int PageSize { get; set; }

    /// <summary>Gets or sets the total number of data items that are available for paging.</summary>
    int TotalItemCount { get; set; }
}

public static class PagedExtensions
{
    public static PagedList<T> ToPagedList<T>(
        this IOrderedQueryable<T> allItems,
        int pageIndex,
        int pageSize)
    {
        if (pageIndex < 1)
            pageIndex = 1;
        int count = (pageIndex - 1) * pageSize;
        int totalItemCount = Queryable.Count<T>(allItems);
        while (totalItemCount <= count && pageIndex > 1)
            count = (--pageIndex - 1) * pageSize;

        return new PagedList<T>(Queryable.Take<T>(Queryable.Skip<T>(allItems, count), pageSize), pageIndex, pageSize, totalItemCount);
    }

    /// <summary>
    ///   Gets a strongly typed PagedList object using all items, current page index and page size.
    /// </summary>
    /// <param name="allItems">A sequence of data items that implemented IEnumerable interface.</param>
    /// <param name="pageIndex">Current page index.</param>
    /// <param name="pageSize">The number of data items that are displayed for each page of data.</param>
    public static PagedList<T> ToPagedList<T>(
        this IEnumerable<T> allItems,
        int pageIndex,
        int pageSize)
    {
        return ToPagedList<T>(allItems.AsQueryable<T>(), pageIndex, pageSize);
    }
}

[Serializable]
public class MvcPagerDto
{
    /// <summary>
    /// 当前页(默认为第一页)
    /// </summary>
    public virtual int? CurrentPage { get; set; }
    /// <summary>
    /// 每页的数据量(默认为16)
    /// </summary>
    public virtual int? PageSize { get; set; }
    /// <summary>
    /// 排序类型
    /// null或0，按照默认规则排序，5：按照时间倒叙
    /// </summary>
    public virtual int? SortType { get; set; }
    /// <summary>
    /// 排序字段
    /// <para>(默认Id,guid请在构造函数中重写)</para>
    /// </summary>
    public virtual string SortName { get; set; }
    /// <summary>
    /// 是否倒序
    /// <para>0正序</para>
    /// <para>1倒序(默认)</para>
    /// </summary>
    public virtual int? SortDesc { get; set; }
}