using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;

namespace OnTheRoad
{
    /// <summary>
    /// Custom SiteMap Provider to assist in mixing Site Maps with Routing in ASP.Net Web Form 
    /// Reads a custom attribute of the site map, called urlRoute, to assist with proper connecting of dots
    /// </summary>
    class RouteBaseSitemapProvider : XmlSiteMapProvider
    {
        /// <summary>
        /// Override the CurrentNode to also find pages that were routed.
        /// </summary>
        public override SiteMapNode CurrentNode
        {
            get
            {
                var node = base.CurrentNode;

                // replacement data will depend on the current page's route information, so we need a copy of the current page
                var page = HttpContext.Current.CurrentHandler as System.Web.UI.Page;

                // if it is not a page, or doesn't have route data, we have nothing to do
                if (page != null && page.RouteData != null)
                {

                    // if the current page wasn't found in the sitemap, maybe it was becase of routing... let's find out
                    if (node == null)
                    {


                        // See if this page has a route
                        var handler = page.RouteData.RouteHandler as PageRouteHandler;

                        // if it was a page Route handler, we are in business
                        if (handler != null)
                        {
                            // try and find the virutal path of the .aspx actually handling the request instead.
                            node = FindSiteMapNode(handler.VirtualPath);
                        }


                    }

                    // if either we had, or found, out place in the sitemap, trace up the heirachy doing replacements
                    if (node != null)
                    {

                        // build a list of RegEx to aid in converstion, using RegEx so I can ignore case.  
                        Dictionary<Regex, string> replacements = new Dictionary<Regex, string>();
                        foreach (var key in page.RouteData.Values.Keys)
                        {
                            // if we wanted to get fancy, we could do so here by changeing the RegEx and allowing options...  {routedata:option}
                            // we'd need to alter the replacements Dictionary to capture that information, and then write code later to handle those options

                            replacements.Add(new Regex(string.Format("\\{{{0}\\}}", key), RegexOptions.IgnoreCase), page.RouteData.Values[key].ToString());
                        }

                        // when you are passed the node it is read-only, and settting node.ReadOnly = False will not fix that... we need to clone it.
                        // pass it into node, vice activeNode, because we are going to return node, 
                        node = node.Clone(true);


                        // walk up the heirarchy
                        for (var activeNode = node; activeNode != null; activeNode = activeNode.ParentNode)
                        {
                            // get the custom route information...
                            var urlRoute = activeNode["urlRoute"];

                            // ... and if we have it, transform the node
                            if (urlRoute != null)
                            {

                                // ensure we can play with the node
                                node.ReadOnly = false;


                                // change the URL to one that can have replacement value plugged in                                
                                activeNode.Url = urlRoute;

                                // go through each replacement and make it so.
                                foreach (var replacement in replacements)
                                {
                                    // if this was extended with options, here is where you would implement those options
                                    activeNode.Url = replacement.Key.Replace(activeNode.Url, replacement.Value);
                                    activeNode.Title = replacement.Key.Replace(activeNode.Title, replacement.Value);
                                }

                            }
                        }

                    }

                }

                // all done, send the node out for usage.
                return node;
            }
        }
    }
}