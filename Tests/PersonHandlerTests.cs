using System;
using Xunit;
using ObjectLibrary;
using FileParser;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace PersonHandlerTests
{
    public class PersonHandlerTests
    {
        private FileHandler _fh;
        private DataParser _dp;

        private string csvPath = "../data.csv";  // change to appropriate path
        private List<List<string>> data;

        public PersonHandlerTests() {
            _fh = new FileHandler();
            _dp = new DataParser();

            var lines = _fh.ReadFile(csvPath);
            data = _fh.ParseCsv(lines);
        }

        [Fact]
        public void ConstructorTest()
        {
            PersonHandler ph = new PersonHandler(data);
            
            Assert.Equal(500, ph.People.Count);
            Assert.Equal("Arlinda", ph.People.ElementAt(462).FirstName);
        }

        [Fact]
        public void TestOldest() {
            PersonHandler ph = new PersonHandler(data);
            var oldest = ph.GetOldest();
            
            Assert.Equal(2, oldest.Count);
            var result = oldest.Where(person => person.Id == 205 || person.Id == 402).ToList();
            if (result.Count == 2) {
                Assert.True(true);
            } else {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestGetString() {
            PersonHandler ph = new PersonHandler(data);
            
            string expected = "Thornton Mynett 02/05/2017";
            string result = ph.People[367].ToString();            
            Assert.Equal(expected, result);
            
            expected = "Elianore Wyley 10/11/2017";
            result = ph.People[84].ToString();            
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestGetOrderBySurname() {
            PersonHandler ph = new PersonHandler(data);

            var ordered = ph.GetOrderBySurname();
            
            Assert.Equal(500, ordered.Count);
            Assert.Equal("Aarons", ordered[0].Surname);
            Assert.Equal("Zarb", ordered[ordered.Count-1].Surname);
            
        }

        [Fact]
        public void TestGetNumSurnameBegins() {
            PersonHandler ph = new PersonHandler(data);

            Assert.Equal(24, ph.GetNumSurnameBegins("A", true));
            Assert.Equal(24, ph.GetNumSurnameBegins("a", false));
            Assert.Equal(0, ph.GetNumSurnameBegins("a", true));
            
            Assert.Equal(0, ph.GetNumSurnameBegins("tA", true));
            Assert.Equal(1, ph.GetNumSurnameBegins("tA", false));            
        }
        
        [Fact]
        public void TestGetAmountBornOnEachDate() {
            PersonHandler ph = new PersonHandler(data);

            var result = ph.GetAmountBornOnEachDate();
            Assert.Equal("11/04/2017 2", result.First());
            Assert.Equal("08/05/2017 1", result[23]);
        }


    }
}
