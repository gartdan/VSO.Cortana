using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSO.Cortana.ViewModel
{
    public class VSOVoiceCommand
    {
        public string VoiceCommand { get; set; }
        public string CommandMode { get; set; }
        public string TextSpoken { get; set; }
        public string WorkItemType { get; set; }
        public string Title { get; set; }

        public string WorkItemState { get; set; }
    }
}
