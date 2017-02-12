using System.Collections.Generic;
using System.Web.Services;

namespace OnTheRoad.WebServices
{
    /// <summary>
    /// Summary description for Tags
    /// </summary>
    [WebService(Namespace = WebServiceNamespace)]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Tags : System.Web.Services.WebService
    {
        private const string WebServiceNamespace = "http://ontheroad.com/webservices/";

        [WebMethod]
        public List<string> GetTagsByPrefix(string prefixText, int count)
        {
            return new List<string>() { "Alaska", "Alabama", "California", "North Dakota" };
        }
    }
}
