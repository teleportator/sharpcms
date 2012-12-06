using SharpCMS.BusinessLogic.Views;

namespace SharpCMS.BusinessLogic.Cache
{
    public interface ISiteMapCache
    {
        void Clear();
    	SiteMap GetSiteMap();
    }
}