using FileRepositoryApi.Models;
using System;
using System.Web.Http;
using System.Web.Mvc;

namespace FileRepositoryApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            ManifestRepository.Init(Server.MapPath("~/App_Data"));
        }
    }
}
