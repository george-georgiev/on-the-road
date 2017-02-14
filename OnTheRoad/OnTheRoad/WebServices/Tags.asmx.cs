using OnTheRoad.App_Start;
using OnTheRoad.Logic.Contracts;
using System.Collections.Generic;
using System.Web.Services;
using Ninject;
using System.Linq;

namespace OnTheRoad.WebServices
{
    /// <summary>
    /// Summary description for Tags
    /// </summary>
    [WebService(Namespace = WebServiceNamespace)]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class Tags : WebService
    {
        private const string WebServiceNamespace = "http://ontheroad.com/webservices/";
        private readonly ITagService tagService;

        public Tags()
        {
            var tagService = NinjectKernelInstanceProvider.Instance.Get<ITagService>();
            this.tagService = tagService;
        }

        [WebMethod]
        public List<string> GetTagsByPrefix(string prefixText, int count)
        {
            var tags = this.tagService
                .GetTagsByNamePrefix(prefixText, count)
                .Select(t => t.Name)
                .ToList();

            return tags;
        }
    }
}
