using System;
using System.Web.Caching;

namespace DEPT_Api.Helper
{
    public class CacheData
    {
        private static Cache _cache = null;
        private static Cache Cache
        {
            get
            {
                if (_cache == null)
                    _cache = (System.Web.HttpContext.Current == null) ? System.Web.HttpRuntime.Cache : System.Web.HttpContext.Current.Cache;
                return _cache;
            }
            set
            {
                _cache = value;
            }
        }

        public static object Get(string key)
        {
            return Cache.Get(key);
        }

        public static void Add(string key, object value)
        {
            Cache.Insert(key, value, null, DateTime.MaxValue, TimeSpan.FromMinutes(20), CacheItemPriority.NotRemovable, null);
        }

        public static void Remove(string key)
        {
            Cache.Remove(key);
        }
    }
}