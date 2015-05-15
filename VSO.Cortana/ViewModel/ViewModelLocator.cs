using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSO.Cortana.ViewModel
{
    public class ViewModelLocator
    {
        private Dictionary<string, ViewModelBase> modelSet = new Dictionary<string, ViewModelBase>();

        public ViewModelLocator()
        {
            modelSet.Add("WorkItemListViewModel", new WorkItemListViewModel());
            modelSet.Add("SetVSOCredentialsViewModel", new SetVSOCredentialsViewModel());
            modelSet.Add("WorkItemDetailsViewModel", new WorkItemDetailsViewModel());
        }

        public WorkItemListViewModel WorkItemListViewModel
        {
            get
            {
                return (WorkItemListViewModel)modelSet["WorkItemListViewModel"];
            }
        }

        public SetVSOCredentialsViewModel SetVSOCredentialsViewModel
        {
            get
            {
                return (SetVSOCredentialsViewModel)modelSet["SetVSOCredentialsViewModel"];
            }
        }

        public WorkItemDetailsViewModel WorkItemDetailsViewModel
        {
            get
            {
                return (WorkItemDetailsViewModel)modelSet["WorkItemDetailsViewModel"];
            }
        }


    }
}
