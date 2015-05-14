namespace VSO.Cortana.Service.Queries
{
    public abstract class WorkItemQuery
    {
        public string QueryPath {
            get
            {
                return VSOPaths.WIQL;
            }
        }

        public abstract string GetQuery();
        
    }
}