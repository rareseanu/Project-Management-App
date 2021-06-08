using Microsoft.AspNetCore.Mvc.Filters;
using ProjectManagementApp.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Filters
{
    public class PaginationResourceFilter : ActionFilterAttribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var query = context.HttpContext.Request.Query;
            var pagination = new BasePaginationRequest();

            if (query["size"].ToString() != null &&
                int.TryParse(query["size"].ToString(), out var size))
                pagination.Size = size;

            if (query["page"].ToString() != null &&
                int.TryParse(query["page"].ToString(), out var page))
                pagination.Page = page;

            if (!string.IsNullOrWhiteSpace(query["orderBy"].ToString()))
                pagination.OrderBy = query["orderBy"].ToString();

            if (bool.TryParse(query["desc"].ToString(), out var desc))
                pagination.Desc = desc;

            if (pagination.Size != 0 ||
                pagination.Page != 0 ||
                string.IsNullOrWhiteSpace(pagination.OrderBy) ||
                pagination.Desc)
                context.HttpContext.Items.Add("pagination", pagination);
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }
    }
}
