using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VSO.Cortana.Service.Models;
using VSO.Cortana.Service.Queries;
using VSO.Cortana.Service.Util;

namespace VSO.Cortana.Service
{
    public class VSOService
    {
        public string VSOAccount { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string  BaseUrl { get; private set; }
        public string Project { get; private set; }

        private static readonly string ApiVersion = "?api-version=1.0";


        public VSOService(string vsoAccount, string userName, string password, string project = "")
        {
            this.VSOAccount = vsoAccount;
            this.UserName = userName;
            this.Password = password;
            this.BaseUrl = string.Format("https://{0}.visualstudio.com/DefaultCollection/_apis/", this.VSOAccount);
            this.Project = project;
        }

        public async Task<WorkItem> GetWorkItemById(int id)
        {
            using (var client = GetClient())
            {
                string qs = string.Format("?ids={0}", id);
                try {
                    var responseBody = await GetAsync(client, BaseUrl + VSOPaths.WorkItems + qs);
                    var items = JsonConvert.DeserializeObject<ApiCollection<WorkItem>>(responseBody);
                    return items.Value.ToArray()[0];
                }
                catch(Exception ex)
                {
                    //todo: something
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
                return new WorkItem();
            }
        }

        public async Task<IEnumerable<WorkItem>> GetWorkItemsById(params int[] ids)
        {
            using (var client = GetClient())
            {
                string qs = string.Format("?ids={0}", ids.ToCommaString());
                try
                {
                    var responseBody = await GetAsync(client, BaseUrl + VSOPaths.WorkItems + qs);
                    var items = JsonConvert.DeserializeObject<ApiCollection<WorkItem>>(responseBody);
                    return items.Value;
                }
                catch (Exception ex)
                {
                    //todo: something
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            return new List<WorkItem>();
        }

        private HttpClient GetClient()
        {
            string auth = Convert.ToBase64String(System.Text.Encoding.GetEncoding("ASCII")
                .GetBytes(string.Format("{0}:{1}", this.UserName, this.Password)));
            var client = new HttpClient();
            
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
            return client;
        }

        public async Task<IEnumerable<WorkItem>> GetWorkItemsByTitle(string title)
        {
            var queryResponse =  await RunWorkItemQuery(new WorkItemByTitleQuery(title));
            var ids = queryResponse.workItems.Select(x => x.id).ToArray();
            return await GetWorkItemsById(ids);
        }

        public async Task<QueryResponse> RunWorkItemQuery(WorkItemQuery query)
        {
            using (var client = GetClient())
            {
                var responseBody = await PostAsync(client, BaseUrl + query.QueryPath + ApiVersion, query.GetQuery());
                var items = JsonConvert.DeserializeObject<QueryResponse>(responseBody);
                return items;
            }
        }

        public async Task<string> PostAsync(HttpClient client, string url, string content)
        {
            var responseBody = string.Empty;
            try
            {
                using (var response = client.PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json")).Result)
                {
                    response.EnsureSuccessStatusCode();
                    responseBody = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                //todo: something
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return responseBody;
        }

        public async Task<string> GetAsync(HttpClient client, string url)
        {
            var responseBody = string.Empty;
            try
            {
                using(var response = client.GetAsync(url).Result)
                {
                    response.EnsureSuccessStatusCode();
                    responseBody = await response.Content.ReadAsStringAsync();
                }
            }catch(Exception ex)
            {
                //todo: something
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return responseBody;
        }
    }



    public static class VSOPaths
    {
        public static readonly string WorkItems = "wit/workitems";
        public static readonly string WIQL = "wit/wiql";
    }
}
