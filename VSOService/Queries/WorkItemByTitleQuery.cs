using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSO.Cortana.Service.Queries
{
    public class WorkItemByTitleQuery : WorkItemQuery
    {
        public string Title { get; private set; }

        /// <summary>
        /// Note the double brackets. This is needed when using string.Format to properly escape the text
        /// </summary>
        private static readonly string QueryFormat = 
@"{{ ""query"":
""Select [System.Id], [System.Title], [System.State], [System.AreaPath], [System.TeamProject], [System.WorkItemType], [System.Reason], [System.CreatedBy]
FROM WorkItems WHERE [System.Title] CONTAINS '{0}'
ORDER BY [System.CreatedDate] DESC""
}}
";

        public WorkItemByTitleQuery(string title)
        {
            this.Title = title;
        }

        public override string GetQuery()
        {
            return string.Format(QueryFormat, this.Title);
        }


    }
}
