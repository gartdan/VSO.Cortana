using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VSO.Cortana.Service.Models;
using VSO.Cortana.Service;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VSO.Cortana.Common;
using VSO.Cortana.Service.Queries;
using VSO.Cortana.View;

namespace VSO.Cortana.ViewModel
{
    public class WorkItemListViewModel : ViewModelBase
    {
        private ICommand addWorkItemCommand;
        private ICommand findWorkItemByTitleCommand;
        private ICommand findWorkItemsAssignedToMeCommand;
        private ICommand setVSOCredentialsCommand;
        private ObservableCollection<WorkItem> workItems;
        private WorkItem selectedWorkItem;
        private IVSOService vsoService;

        public WorkItemListViewModel(IVSOService vsoService)
        {
            this.vsoService = vsoService;
            addWorkItemCommand = new RelayCommand(new Action(this.AddWorkItem));
            setVSOCredentialsCommand = new RelayCommand(new Action(this.SetVSOCredentials));
        }

        public WorkItemListViewModel() : this(App.VSOService) { }

        public ObservableCollection<WorkItem> WorkItems
        {
            get { return this.workItems; }
            set
            {
                this.workItems = value;
                NotifyPropertyChanged("WorkItems");
            }
        }

        public WorkItem SelectedWorkItem
        {
            get { return this.selectedWorkItem; }
            set
            {
                this.selectedWorkItem = value;
                NotifyPropertyChanged("SelectedWorkItem");
            }
        }

        public ICommand AddWorkItemCommand
        {
            get
            {
                return this.addWorkItemCommand;
            }
        }

        public void AddWorkItem()
        {
            App.NavigationService.Navigate<WorkItemDetailsView>();
        }

        public void SetVSOCredentials()
        {
            App.NavigationService.Navigate<SetVSOCredentialsView>();
        }

        public async Task LoadWorkItems(WorkItemQuery query)
        {
            var workItems = await this.vsoService.GetWorkItemsByQuery(query);
            this.WorkItems = new ObservableCollection<WorkItem>(workItems);
        }

        public void SelectionChanged()
        {
            if(SelectedWorkItem != null)
            {
                App.NavigationService.Navigate<WorkItem>(SelectedWorkItem);
            }
        }


    }
}
