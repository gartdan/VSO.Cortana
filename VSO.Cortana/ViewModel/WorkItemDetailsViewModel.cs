using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.VoiceCommands;

namespace VSO.Cortana.ViewModel
{
    public class WorkItemDetailsViewModel : ViewModelBase
    {

        public async Task UpdateWorkItemPhraseList()
        {
            try
            {
                // Update the destination phrase list, so that Cortana voice commands can use destinations added by users.
                // When saving a trip, the UI navigates automatically back to this page, so the phrase list will be
                // updated automatically.
                VoiceCommandDefinition commandDefinitionsEnUs;

                if (VoiceCommandDefinitionManager.InstalledCommandDefinitions.TryGetValue("VSOCommandSet_en-us", out commandDefinitionsEnUs))
                {
                    //List<string> destinations = new List<string>();
                    //foreach (Model.Trip t in store.Trips)
                    //{
                    //    destinations.Add(t.Destination);
                    //}

                    //await commandDefinitionsEnUs.SetPhraseListAsync("destination", destinations);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Updating Phrase list for VCDs: " + ex.ToString());
            }
        }
    }
}
