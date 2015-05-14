using Newtonsoft.Json;
using System;

namespace VSO.Cortana.Service.Models
{
    public class WorkItem
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "rev")]
        public int Rev { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "fields")]
        public Fields Fields { get; set; }

    }


    public class Value
    {
        public int id { get; set; }
        public int rev { get; set; }
        public Fields fields { get; set; }
        public string url { get; set; }
    }

    public class Fields
    {

        [JsonProperty(PropertyName = "System.AreaPath")]
        public string SystemAreaPath { get; set; }
        [JsonProperty(PropertyName = "System.TeamProject")]
        public string SystemTeamProject { get; set; }
        [JsonProperty(PropertyName = "System.IterationPath")]
        public string SystemIterationPath { get; set; }
        [JsonProperty(PropertyName = "System.WorkItemType")]
        public string SystemWorkItemType { get; set; }
        [JsonProperty(PropertyName = "System.State")]
        public string SystemState { get; set; }
        [JsonProperty(PropertyName = "System.Reason")]
        public string SystemReason { get; set; }
        [JsonProperty(PropertyName = "System.CreatedDate")]
        public DateTime SystemCreatedDate { get; set; }
        [JsonProperty(PropertyName = "System.CreatedBy")]
        public string SystemCreatedBy { get; set; }
        [JsonProperty(PropertyName = "System.Title")]
        public string SystemTitle { get; set; }

    }

}
