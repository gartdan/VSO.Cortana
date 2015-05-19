using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSO.Cortana.Service.Models;

namespace VSO.Cortana.ViewModel
{
    public class WorkItemViewModel
    {
        public string Title { get; set; }
        public string TeamProject { get; set; }
        public int Id { get; set; }
        public string Creator { get; set; }
        public string Url { get; set; }
        public string WorkItemType { get; set; }
        public DateTime DateCreated { get; set; }
        public string AreaPath { get;  set; }
        public string IterationPath { get;  set; }

        public WorkItemViewModel()
        {

        }

        public static WorkItemViewModel FromItem(WorkItem item)
        {
            var vm = new WorkItemViewModel();
            vm.Id = item.Id;
            vm.Title = item.Fields.SystemTitle;
            vm.Url = item.Url;
            vm.TeamProject = item.Fields.SystemTeamProject;
            vm.WorkItemType = item.Fields.SystemWorkItemType;
            vm.DateCreated = item.Fields.SystemCreatedDate;
            vm.Creator = item.Fields.SystemCreatedBy;
            vm.AreaPath = item.Fields.SystemAreaPath;
            vm.IterationPath = item.Fields.SystemIterationPath;
            return vm;
        }
    }
}
