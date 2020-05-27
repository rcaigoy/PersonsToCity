using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PersonsToCity.Models;
using PersonsToCity.DBContext;

namespace PersonsToCity.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.CityList = _City.ListCities();
            return View();
        }

        public ActionResult City(int CityId)
        {
            ViewBag.City = _City.GetCity(CityId);
            return View();
        }


        public ActionResult Person(int PersonId)
        {
            var PersonFromModel = _Person.GetPerson(PersonId);
            ViewBag.Person = new _Person(PersonFromModel);
            return View();
        }


        public ActionResult AddPerson()
        {
            ViewBag.CityList = _City.ListCities();
            return View();
        }


        public ActionResult EditPerson(int PersonId)
        {
            ViewBag.Person = _Person.GetPerson(PersonId);
            ViewBag.CityList = _City.ListCities();

            return View();
        }


        [HttpPost]
        public ActionResult CreatePerson(string PersonName, int CityId)
        {
            var PersonFromModel = _Person.CreatePerson(PersonName, CityId);
            var ToReturn = new _Person(PersonFromModel);

            return Json(ToReturn, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetPerson(int PersonId)
        {
            var PersonFromModel = _Person.GetPerson(PersonId);
            var ToReturn = new _Person(PersonFromModel);

            return Json(ToReturn, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetPersonsList()
        {
            var PersonsList = _Person.ListPersons();
            return Json(PersonsList, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        public ActionResult UpdatePerson(int PersonId, string PersonName, int CityId)
        {
            var UpdatedPersonFromModel = _Person.UpdatePerson(PersonId, PersonName, CityId);
            var ToReturn = new _Person(UpdatedPersonFromModel);
            return Json(ToReturn, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public ActionResult DeletePerson(int PersonId)
        {
            _Person.DeletePerson(PersonId);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        public ActionResult CreateCity(string CityName)
        {
            var CityFromModel = _City.CreateCity(CityName);
            var ToReturn = new _City(CityFromModel);
            return Json(ToReturn, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetCity(int CityId)
        {
            var CityFromModel = _City.GetCity(CityId);
            var ToReturn = new _City(CityFromModel);
            return Json(ToReturn, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetCitiesList()
        {
            var ToReturn = _City.ListCities();
            return Json(ToReturn, JsonRequestBehavior.AllowGet);
        }


        public ActionResult UpdateCity(int CityId, string CityName)
        {
            var UpdatedCityFromModel = _City.UpdateCity(CityId, CityName);
            var ToReturn = new _City(UpdatedCityFromModel);

            return Json(ToReturn, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DeleteCity (int CityId)
        {
            _City.DeleteCity(CityId);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }
    }
}
