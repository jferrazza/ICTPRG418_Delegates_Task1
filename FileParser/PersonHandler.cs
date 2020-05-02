using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ObjectLibrary;


namespace FileParser {
    
    //public class Person { }  // temp class delete this when Person is referenced from dll
    
    public class PersonHandler {
        public List<Person> People;

        /// <summary>
        /// Converts List of list of strings into Person objects for People attribute.
        /// </summary>
        /// <param name="people"></param>
        public PersonHandler(List<List<string>> people) {
            People = new List<Person>();
            List<List<string>> people2 = new List<List<string>>();

            //Delegate to process the strings:
            DataParser dataParser = new DataParser();
            Func<List<List<string>>, List<List<string>>> func = dataParser.StripQuotes;
            func += dataParser.StripWhiteSpace;
            people2 = func(people);
            //Cut off headers:
            people2.RemoveAt(0);
            //Coppied from Program, too complex to delegate
            foreach (var row in people2)
            {
                for (var index = 0; index < row.Count; index++)
                {
                    if (row[index][0] == '#')
                        row[index] = row[index].Remove(0, 1);

                }
            }

            foreach (var item in people2)
            {
                
                
                People.Add(new Person(int.Parse(item[0]), item[1], item[2], new DateTime(long.Parse(item[3]))));
            }


        }

        /// <summary>
        /// Gets oldest people
        /// </summary>
        /// <returns></returns>
        public List<Person> GetOldest() {

            //TWO DELEGATES,
            //the one on the right collects the dates from the people,
            // the one on the left picks the persons with the highest date from the former results.
            // The latter results are turned back into a list.

            return People.Where(d => d.Dob == People.Select(dd => dd.Dob).Min()).ToList(); //-- return result here

        }

        /// <summary>
        /// Gets string (from ToString) of Person from given Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetString(int id) {

            return People[id].ToString();  //-- return result here
        }

        public List<Person> GetOrderBySurname() {
            //Instantiation overload to clone the existing list just in case.
            var l = new List<Person>(People);

            //Delegate to return an ordered enumerable and return it back as a list.
            return l.OrderBy(d => d.Surname).ToList();  //-- return result here
        }

        /// <summary>
        /// Returns number of people with surname starting with a given string.  Allows case-sensitive and case-insensitive searches
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="caseSensitive"></param>
        /// <returns></returns>
        public int GetNumSurnameBegins(string searchTerm, bool caseSensitive) {
            //Delegate and conditional function to count items that match the function:
            return People.Count(d => d.Surname.StartsWith(searchTerm,caseSensitive ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase));  //-- return result here
        }
        
        /// <summary>
        /// Returns a string with date and number of people with that date of birth.  Two values seperated by a tab.  Results ordered by date.
        /// </summary>
        /// <returns></returns>
        public List<string> GetAmountBornOnEachDate() {
            List<string> result = new List<string>();

            //Delegate to order the list of people by the dates, get the dates from the list of people, removes duplicates, and returns them back into a List for familiarity:
            var uniqueDates = People.OrderBy(o => o.Dob).Select(i => i.Dob).Distinct().ToList();
            foreach (var item in uniqueDates)
            {
                //Delegate to count the amount of people with the date:
                result.Add($"{item.ToString("dd/MM/yyyy")} {People.Where(d => d.Dob == item).Count()}");
            }


            return result;  //-- return result here
        }
    }
}