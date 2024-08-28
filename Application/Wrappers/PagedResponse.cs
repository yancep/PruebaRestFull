﻿
namespace Application.Wrappers
{
    public class PagedResponse<T>: Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PagedResponse(T data, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Data = data;
            this.Message = null;
            this.Succeded = true;
            this.Errors = null;
        }
    }
}
