using System.Collections.Generic;

namespace FileParser
{
    public class DataParser
    {


        /// <summary>
        /// Strips any whitespace before and after a data value.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<List<string>> StripWhiteSpace(List<List<string>> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].Count == 0)
                {
                    data.RemoveAt(i);
                }
                else for (int ii = 0; ii < data[i].Count; ii++)
                    {
                        data[i][ii] = data[i][ii].Trim();
                    }

            }


            return data; //-- return result here
        }

        /// <summary>
        /// Strips quotes from beginning and end of each data value
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<List<string>> StripQuotes(List<List<string>> data)
        {

            //FOR loop because FOREACH loop will crash due to editing
            for (int i = 0; i < data.Count; i++)
            {
                for (int ii = 0; ii < data[i].Count; ii++)
                {
                    //lazy code partly because STRING is a simple type
                    if (data[i][ii].StartsWith("\"") && data[i][ii].EndsWith("\""))
                        data[i][ii] = data[i][ii].Substring(1, data[i][ii].Length - 2);
                }

            }

            return data; //-- return result here
        }

    }
}