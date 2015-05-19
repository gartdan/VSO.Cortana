using VSO.Cortana.ViewModel;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace VSO.Cortana.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateWorkItem : Page
    {
        private static DependencyProperty s_itemProperty
                    = DependencyProperty.Register("Item", typeof(WorkItemViewModel), typeof(DetailPage), new PropertyMetadata(null));

        public static DependencyProperty ItemProperty
        {
            get { return s_itemProperty; }
        }

        public WorkItemViewModel Item
        {
            get { return (WorkItemViewModel)GetValue(s_itemProperty); }
            set { SetValue(s_itemProperty, value); }
        }

        public CreateWorkItem()
        {
            this.InitializeComponent();
            this.Item = new WorkItemViewModel();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Parameter is item ID
            
            if(e.Parameter != null)
            {
                //Should pass the work item type as a parameter
                //e.g. VSO, Create new Bug
                if(e.Parameter is string)
                {
                    this.Item.WorkItemType = e.Parameter as string;
                }
                if(e.Parameter is VSOVoiceCommand)
                {
                    var command = (VSOVoiceCommand)e.Parameter; 
                    this.Item.WorkItemType = command.WorkItemType;
                }
            }

            var backStack = Frame.BackStack;
            var backStackCount = backStack.Count;

            if (backStackCount > 0)
            {
                var masterPageEntry = backStack[backStackCount - 1];
                backStack.RemoveAt(backStackCount - 1);

                // Doctor the navigation parameter for the master page so it
                // will show the correct item in the side-by-side view.
                var modifiedEntry = new PageStackEntry(
                    masterPageEntry.SourcePageType,
                    Item.Id,
                    masterPageEntry.NavigationTransitionInfo
                    );
                backStack.Add(modifiedEntry);
            }

            // Register for hardware and software back request from the system
            SystemNavigationManager.GetForCurrentView().BackRequested += Page_BackRequested;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            SystemNavigationManager.GetForCurrentView().BackRequested -= Page_BackRequested;
        }

        private void OnBackRequested()
        {
            // Page above us will be our master view.
            // Make sure we are using the "drill out" animation in this transition.

            Frame.GoBack();
        }





        private void PageRoot_Loaded(object sender, RoutedEventArgs e)
        {

        }




        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            OnBackRequested();
        }

        private void Page_BackRequested(object sender, BackRequestedEventArgs e)
        {
            // Mark event as handled so we don't get bounced out of the app.
            e.Handled = true;

            OnBackRequested();


        }
    }
}
