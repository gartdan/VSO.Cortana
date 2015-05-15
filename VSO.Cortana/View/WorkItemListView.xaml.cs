using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VSO.Cortana.Common;
using VSO.Cortana.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace VSO.Cortana.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WorkItemListView : Page
    {

        private NavigationHelper navigationHelper;

        private WorkItemListViewModel defaultViewModel;

        public WorkItemListViewModel DefaultViewModel
        {
            get { return defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public WorkItemListView()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        /// <summary>
        /// If we've lost focus and returned to this app, reload the store, as the on-disk content might have
        /// been changed by the background task.
        /// </summary>
        /// <param name="sender">Ignored</param>
        /// <param name="args">Ignored</param>
        private async void CoreWindow_VisibilityChanged(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.VisibilityChangedEventArgs args)
        {
            if (args.Visible == true)
            {
                await DefaultViewModel.LoadWorkItems();
            }
        }

        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            defaultViewModel = (WorkItemListViewModel)this.DataContext;

            DefaultViewModel.SelectedWorkItem = null;

            Window.Current.CoreWindow.VisibilityChanged += CoreWindow_VisibilityChanged;
        }

        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }


        /// <summary>
        /// Since there's an imperfect click implementation on listboxes, this provides a quick
        /// way to handle the SelectionChanged event, in order to open the selected trip's 
        /// details.
        /// </summary>
        /// <param name="sender">Ignored</param>
        /// <param name="e">Ignored</param>
        private void workItemsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.DefaultViewModel.SelectionChanged();
        }

        /// <summary>
        /// Handle footer link clicks.
        /// </summary>
        /// <param name="sender">The target uri</param>
        /// <param name="e">Ignored</param>
        async void Footer_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri(((HyperlinkButton)sender).Tag.ToString()));
        }
    }
}
