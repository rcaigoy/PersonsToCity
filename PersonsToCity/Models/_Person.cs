using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using PersonsToCity.DBContext;

namespace PersonsToCity.Models
{
    public class _Person
    {
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public int CityId { get; set; }

        public string CityName { get; set; }

        public _Person(Person Values)
        {
            this.PersonId = Values.PersonId;
            this.PersonName = Values.PersonName;
            this.CityId = Values.CityId;

            if (Values.City != null)
            {
                this.CityName = Values.City.CityName;
            }
        }
        public _Person() { }

        public static Person CreatePerson(string PersonName, int CityId)
        {
            using (var db = new PersonsToCityContext())
            {
                var ToAdd = new Person();
                ToAdd.PersonName = PersonName;
                ToAdd.CityId = CityId;
                var AddedPerson = db.Persons.Add(ToAdd);
                AddedPerson.City = db.Cities.FirstOrDefault(x => x.CityId == CityId);
                db.SaveChanges();

                return AddedPerson;
            }
        }

        public static Person GetPerson(int PersonId)
        {
            using (var db = new PersonsToCityContext())
            {
                var PersonFromDb = db.Persons.FirstOrDefault(x => x.PersonId == PersonId);

                if (PersonFromDb != null)
                    PersonFromDb.City = db.Cities.FirstOrDefault(x => x.CityId == PersonFromDb.CityId);

                return PersonFromDb;
            }
        }

        public static List<_Person> ListPersons()
        {
            using (var db = new PersonsToCityContext())
            {
                List<_Person> ToReturn = new List<_Person>();

                foreach (var person in db.Persons.Where(x => x.PersonName.Contains("e")))
                {
                    ToReturn.Add(new _Person(person));
                }

                return ToReturn;
            }
        }


        public static Person UpdatePerson(int PersonId, string PersonName, int CityId)
        {
            using (var db = new PersonsToCityContext())
            {
                var ToUpdate = db.Persons.FirstOrDefault(x => x.PersonId == PersonId);
                ToUpdate.PersonName = PersonName;
                ToUpdate.CityId = CityId;
                db.SaveChanges();

                var UpdatedPerson = ToUpdate;

                UpdatedPerson.City = db.Cities.FirstOrDefault(x => x.CityId == CityId);

                return UpdatedPerson;
                
            }
        }


        public static void DeletePerson(int PersonId)
        {
            using (var db = new PersonsToCityContext())
            {
                var ToDelete = db.Persons.FirstOrDefault(x => x.PersonId == PersonId);
                db.Persons.Remove(ToDelete);
                db.SaveChanges();
            }
        }

    }
}