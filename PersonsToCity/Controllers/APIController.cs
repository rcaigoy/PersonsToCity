using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

//APIController added
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Web.Mvc;

//project added
using PersonsToCity.Models;

namespace PersonsToCity.Controllers
{
    public class APIController : ApiController
    {

        public HttpResponseMessage CreateCity(string CityName)
        {
            var CityFromModel = _City.CreateCity(CityName);
            var ToReturn = new _City(CityFromModel);
            return Json(ToReturn, JsonRequestBehavior.AllowGet);
        }

        private HttpResponseMessage Json(object o, JsonRequestBehavior j = JsonRequestBehavior.AllowGet)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK, o, Configuration.Formatters.JsonFormatter);
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                NoCache = true
            };
            return response;
        }
    }
}
