using System.Collections.Generic;
using System.Threading.Tasks;
using VSO.Cortana.Service.Models;
using VSO.Cortana.Service.Queries;

namespace VSO.Cortana.Service
{
    public interface IVSOService
    {
        Task<WorkItem> GetWorkItemById(int id);
        Task<IEnumerable<WorkItem>> GetWorkItemsById(params int[] ids);
        Task<IEnumerable<WorkItem>> GetWorkItemsByQuery(WorkItemQuery query);
        Task<IEnumerable<WorkItem>> GetWorkItemsByTitle(string title);
    }
}