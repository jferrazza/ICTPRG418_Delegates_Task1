using System;
using Xunit;
using System.Collections.Generic;
using FileParser;

namespace FileParserTests
{
    public class FileParserTests
    {

        private FileHandler _fh;
        private DataParser _dp;
        private string _csvPath = "../data.csv";  // change to appropriate path
        private string _tsvPath = "../data.tsv";  // change to appropriate path

        private string _writeFile ="../dataWrite.txt";  // change to appropriate path
       
        private List<List<string>> _data;

        public FileParserTests() {
            _fh = new FileHandler();
            _dp = new DataParser();
            
            _data = new List<List<string>>();
            
            _data.Add(new List<string>() {"1", "John", "Smith"});
            _data.Add(new List<string>() {"2", "Jane", "Jones"});
            _data.Add(new List<string>() {"3", "Jill", "Rhodes"});
            _data.Add(new List<string>() {"4", "Bill", "Holmes"});
            _data.Add(new List<string>() {"5", "Peter", "Watson"});
            _data.Add(new List<string>() {"\"6\"", "\"Ophelia\"", "\"Turing\""});
            _data.Add(new List<string>() {"7", "Catherine", "Clark"});
            _data.Add(new List<string>() {" 8", "    Wilfred     ", "Sutherland "});
            _data.Add(new List<string>() {"9", "Rickgard", "Arthurs"});
        }

        [Fact]
        public void ReadFileTest() {
            var list = _fh.ReadFile(_csvPath);

            Assert.Equal(501, list.Count);
            Assert.Equal("\"#419\",\"#Augy\",\"#Dedrick\",\"#636399072000000000\"", list[419]);
            Assert.Equal("\"#231\",\"#Silvester\",\"#O'Crowley\",\"#636340320000000000\"", list[231]);

            list = _fh.ReadFile(_tsvPath);

            Assert.Equal(1001, list.Count);
            Assert.Equal("YLU-423	Kia	Sephia	2001", list[735]);
            Assert.Equal("YTU-308	Lotus	Elise	2005", list[918]);
        }
        [Fact]
        public void ParseDataTest() {
            var list = _fh.ReadFile(_csvPath);
            var data = _fh.ParseData(list, ',');
            
            Assert.Equal(501, data.Count);
            Assert.Equal("\"#Dedrick\"", data[419][2]);
            Assert.Equal("\"#Silvester\"", data[231][1]);

            list = _fh.ReadFile(_tsvPath);
            data = _fh.ParseData(list, '\t');

            Assert.Equal(1001, data.Count);
            Assert.Equal("YLU-423", data[735][0]);
            Assert.Equal("Elise", data[918][2]);
        }
        
        [Fact]
        public void ParseCsvTest() {
            var list = _fh.ReadFile(_csvPath);
            var data = _fh.ParseCsv(list);
            
            Assert.Equal(501, data.Count);
            Assert.Equal("\"#Dedrick\"", data[419][2]);
            Assert.Equal("\"#Silvester\"", data[231][1]);
        }

        [Fact]
        public void WriteFileTest() {
            _fh.WriteFile(_writeFile, '*', _data);

            var data = _fh.ReadFile(_writeFile);
            
            Assert.Equal("7*Catherine*Clark", data[6]);

        }

        [Fact]
        public void StripWhiteSpaceTest() {
            var list = _dp.StripWhiteSpace(_data);

            Assert.Equal("8", list[7][0]); 
            Assert.Equal("Wilfred", list[7][1]);
            Assert.Equal("Sutherland", list[7][2]);

            
        }
        
        [Fact]
        public void StripQuotesTest() {
            var list = _dp.StripQuotes(_data);

            Assert.Equal("6", list[5][0]); 
            Assert.Equal("Ophelia", list[5][1]);
            Assert.Equal("Turing", list[5][2]);   
        }
    }
}
/* Environment.GetEnvironmentVariable("HOME") + */
