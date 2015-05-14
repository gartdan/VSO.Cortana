using System.Text;

namespace VSO.Cortana.Service.Util
{
    public static class IntArrayToDelimitedString
    {
        public static string ToCommaString(this int[] ary)
        {
            var sb = new StringBuilder();
            bool isFirst = true;
            foreach(var num in ary)
            {
                if (!isFirst)
                    sb.Append(",");
                sb.Append(num.ToString());
                isFirst = false;
            }
            return sb.ToString();
        }
    }
}
