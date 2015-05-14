using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSO.Cortana.Service.Queries
{
    public class WorkItemsCreatedThisWeekQuery : WorkItemQuery
    {
        private static readonly string QueryFormat =
@"{{ ""query"":
""Select [System.Id], [System.Title], [System.State], [System.AreaPath], [System.TeamProject], [System.WorkItemType], [System.Reason], [System.CreatedBy]
FROM WorkItems WHERE [System.CreatedDate] = @today-7
ORDER BY [System.CreatedDate] DESC""
}}
";

        public override string GetQuery()
        {
            return QueryFormat;
        }
    }
}
