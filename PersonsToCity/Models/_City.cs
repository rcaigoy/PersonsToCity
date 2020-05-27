using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using PersonsToCity.DBContext;

namespace PersonsToCity.Models
{
    public class _City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public List<_Person> Persons { get; set; }

        public _City() { }
        public _City(City Values)
        {
            this.CityId = Values.CityId;
            this.CityName = Values.CityName;

            if (Values.Persons != null)
            {
                this.Persons = new List<_Person>();
                foreach (var person in Values.Persons)
                {
                    this.Persons.Add(new _Person(person));
                }
            }
        }

        public static City CreateCity(string CityName)
        {
            using (var db = new PersonsToCityContext()) 
            {
                var ToAdd = new City();
                ToAdd.CityName = CityName;
                var AddedCity = db.Cities.Add(ToAdd);
                db.SaveChanges();
                
                if (AddedCity.Persons == null)
                {
                    AddedCity.Persons = new List<Person>();
                    foreach (var person in db.Persons.Where(x => x.CityId == AddedCity.CityId))
                    {
                        AddedCity.Persons.Add(person);
                    }
                }

                return AddedCity;
                
            }
        }


        public static City GetCity(int CityId)
        {
            using (var db = new PersonsToCityContext())
            {
                var CityFromDb = db.Cities.FirstOrDefault(x => x.CityId == CityId);
                if (CityFromDb.Persons == null)
                {
                    CityFromDb.Persons = new List<Person>();
                    foreach (var person in db.Persons.Where(x => x.CityId == CityFromDb.CityId))
                    {
                        CityFromDb.Persons.Add(person);
                    }
                }
                return CityFromDb;
            }
        }


        public static List<_City> ListCities()
        {
            using (var db = new PersonsToCityContext())
            {
                var ToReturn = new List<_City>();
                foreach (var city in db.Cities)
                {
                    ToReturn.Add(new _City(city));
                }
                return ToReturn;
            }
        }


        public static City UpdateCity(int CityId, string CityName)
        {
            using (var db = new PersonsToCityContext())
            {
                var ToUpdate = db.Cities.FirstOrDefault(x => x.CityId == CityId);
                ToUpdate.CityName = CityName;
                db.SaveChanges();

                var UpdatedCity = ToUpdate;
                if (UpdatedCity == null)
                {
                    UpdatedCity.Persons = new List<Person>();
                    foreach (var person in db.Persons.Where(x => x.CityId == UpdatedCity.CityId))
                    {
                        UpdatedCity.Persons.Add(person);
                    }
                }

                return UpdatedCity;
            }
        }


        public static void DeleteCity(int CityId)
        {
            using (var db = new PersonsToCityContext())
            {
                var ToDelete = db.Cities.FirstOrDefault(x => x.CityId == CityId);
                db.Cities.Remove(ToDelete);
                db.SaveChanges();
            }
        }

    }
}