﻿using System.Web;
using System.Web.Mvc;

namespace WEB_API_SAMPLE
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}