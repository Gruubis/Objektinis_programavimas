using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GUI
{
   public class Person
    {

        protected string Name;
        protected string LastName;
        protected DateTime BirthDate;

        public Person(string name, string lastName, DateTime birthDate)
        {

            if (name == "")
                throw new Exception("name cannot be empty");
            if (lastName == "")
                throw new Exception("Lastname cannot be empty");
            if (birthDate > DateTime.Now)
                throw new Exception("Birthdate cannot be in the past");


            Name = name;
            LastName = lastName;
            BirthDate = birthDate;
        }

        public string GetFullName()         
        {
             return Name + " " + LastName;
        }
        public DateTime GetBirthDate()
        {
            return BirthDate;
        }
        public int GetAge()
        {
            return (int)((DateTime.Now - BirthDate).TotalDays / 365.242199);
        }

        public int GetDays()
        {
            int Days = ((int)(BirthDate.AddYears(GetAge()) - DateTime.Now).TotalDays);
            if (Days < 0)
            {
                return (Days + 366);
            }
            else
            {
                return (Days + 1);
            }
        }
    }
}
