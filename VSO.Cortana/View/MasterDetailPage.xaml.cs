using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using VSO.Cortana.Service;
using VSO.Cortana.Service.Models;
using VSO.Cortana.Service.Queries;
using VSO.Cortana.View;
using VSO.Cortana.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace VSO.Cortana.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MasterDetailPage : Page
    {
        private WorkItemViewModel _lastSelectedItem;
        private IVSOService _vsoService;

        public MasterDetailPage() : this(App.VSOService) { }

        public MasterDetailPage(IVSOService vsoService)
        {
            this._vsoService = vsoService;
            this.InitializeComponent();
        }

        private static DependencyProperty s_titleProperty
            = DependencyProperty.Register("Title", typeof(string), typeof(MasterDetailPage), new PropertyMetadata(null));

        public static DependencyProperty ItemProperty
        {
            get { return s_titleProperty; }
        }

        public string Title
        {
            get { return (string)GetValue(s_titleProperty); }
            set { SetValue(s_titleProperty, value); }
        }



        public IEnumerable<WorkItem> GetWorkItems(NavigationEventArgs e)
        {
            var query = new WorkItemsAssignedToMeQuery();
            IEnumerable<WorkItem> workItems = workItems = this._vsoService.GetWorkItemsByQuery(query).Result;
            if (e.Parameter is VSOVoiceCommand)
            {
                var cmd = e.Parameter as VSOVoiceCommand;
                if(!string.IsNullOrEmpty(cmd.WorkItemState))
                {
                    workItems = workItems.Where(x => x.Fields.SystemState == cmd.WorkItemState);
                }else if(!string.IsNullOrEmpty(cmd.WorkItemType))
                {
                    workItems = workItems.Where(x => x.Fields.SystemWorkItemType == cmd.WorkItemType);
                }
            }
            return workItems;
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
            var items = MasterListView.ItemsSource as List<WorkItemViewModel>;

            if (items == null)
            {
                items = LoadWorkItems(e);
            }

            if (e.Parameter != null)
            {
                // Parameter is item ID]
                if (e.Parameter is int)
                {
                    var id = (int)e.Parameter;
                    _lastSelectedItem =
                        items.Where((item) => item.Id == id).FirstOrDefault();
                }
                if(e.Parameter is VSOVoiceCommand)
                {
                    var cmd = (VSOVoiceCommand)e.Parameter;
                    if(!string.IsNullOrEmpty(cmd.WorkItemState))
                    {
                        this.Title = string.Format("My {0} Items", cmd.WorkItemState);
                    }else if(!string.IsNullOrEmpty(cmd.WorkItemType))
                    {
                        this.Title = string.Format("My {0}s", cmd.WorkItemType);
                    }
                    LoadWorkItems(e);
                }
                else
                {
                    this.Title = "My Items";
                }
            }
            else
            {
                this.Title = "My Items";
            }

            UpdateForVisualState(AdaptiveStates.CurrentState);

            // Don't play a content transition for first item load.
            // Sometimes, this content will be animated as part of the page transition.
            DisableContentTransitions();
        }

        private List<WorkItemViewModel> LoadWorkItems(NavigationEventArgs e)
        {
            List<WorkItemViewModel> items = new List<WorkItemViewModel>();
            foreach (var item in this.GetWorkItems(e))
            {
                items.Add(WorkItemViewModel.FromItem(item));
            }
            MasterListView.ItemsSource = null;
            MasterListView.ItemsSource = items;
            return items;
        }

        private void AdaptiveStates_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            UpdateForVisualState(e.NewState, e.OldState);
        }

        private void UpdateForVisualState(VisualState newState, VisualState oldState = null)
        {
            var isNarrow = newState == NarrowState;

            if (isNarrow && oldState == DefaultState && _lastSelectedItem != null)
            {
                // Resize down to the detail item. Don't play a transition.
                Frame.Navigate(typeof(DetailPage), _lastSelectedItem.Id, new SuppressNavigationTransitionInfo());
            }

            EntranceNavigationTransitionInfo.SetIsTargetElement(MasterListView, isNarrow);
            if (DetailContentPresenter != null)
            {
                EntranceNavigationTransitionInfo.SetIsTargetElement(DetailContentPresenter, !isNarrow);
            }
        }

        private void MasterListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = (WorkItemViewModel)e.ClickedItem;
            _lastSelectedItem = clickedItem;

            if (AdaptiveStates.CurrentState == NarrowState)
            {
                // Use "drill in" transition for navigating from master list to detail view
                Frame.Navigate(typeof(DetailPage), clickedItem.Id, new DrillInNavigationTransitionInfo());
            }
            else
            {
                // Play a refresh animation when the user switches detail items.
                EnableContentTransitions();
            }
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            // Assure we are displaying the correct item. This is necessary in certain adaptive cases.
            MasterListView.SelectedItem = _lastSelectedItem;
        }

        private void EnableContentTransitions()
        {
            DetailContentPresenter.ContentTransitions.Clear();
            DetailContentPresenter.ContentTransitions.Add(new EntranceThemeTransition());
        }

        private void DisableContentTransitions()
        {
            if (DetailContentPresenter != null)
            {
                DetailContentPresenter.ContentTransitions.Clear();
            }
        }
    }
}
