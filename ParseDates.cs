using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Globalization;
using System.IO;

namespace DataTools
{
    class ParseDates
    {
        /*
         * Name:
         *      ParseDates
         *      
         * Description: 
         *      Open a text datafile and parse through the date column returning a list of valid dates
         *      Send the output to the screen with a count of the number of good dates found
         *      
         * Input args:
         *      arg0    file name
         *      
         * Author:
         *      Kevin Millar
         *      
         * Revisions:
         *      v0   30Aug2016
         *
         * REFACTOR:
         *      input args
         *      output results to file
         *      log invalid records to error file
        */

        //REFACTOR as input args
        const string kFname = "MarketingDataFile.txt"; //hard coded file name in case not input args are supplied
        const string kDatePattern = "MMddyyyy"; //expected format of date being inspecting
        const int kMinYear = 2000; //set the minimum acceptable year
        const int kDateFieldIndex = 1; //zero based index for location of date field
        static string[] delim = new string[] {"\t"}; //record delimiter
        static DateTime maxDate = DateTime.Today; // dates greater then or equal to today are ignored


        static bool isValidDate(string value, int minyear, out DateTime dt_out)
        {
            DateTime dt = DateTime.Today;
            bool result = true;

            try 
	        {
                if (DateTime.TryParseExact(value, kDatePattern, System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                {
                    if (dt.Year < minyear)
                    {
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }
	        }
	        catch (Exception)
	        {
		        result = false;
	        }
            finally
            {
                dt_out = dt;
            }
            return result;
        }
        static List<string> ParseLines(string fname, DateTime maxdate)
        {
            DateTime validDate;
            string ckDate;
            string[] rec;
            List<string> result = new List<string>();

                var lines = File.ReadLines(fname);

                foreach (var line in lines)
                {
                    if(line.Length == 0)
                    {
                        continue; //skip empty lines
                    }
                    try
                    {
                        rec = line.Split(delim, StringSplitOptions.None); //attempt to split the line into an array of values, pulling out the date field

                        if (rec.Length >= kDateFieldIndex) //handles missing field
                        {
                            ckDate = rec[kDateFieldIndex];
                            if (isValidDate(ckDate, kMinYear, out validDate))
                            {
                                // valid date format
                                // if the date is before max date, then then keep
                                if (validDate < maxdate)
                                {
                                    result.Add(ckDate);
                                }
                            }
                            else
                            {
                                //NoOp - invalid date skipped
                            }
                        }
                    }
                    catch (Exception)
                    {
                        //REFACTOR - log to error file
                        //NoOp invalid format or missing data is skipped
                    }
                }
            return result;
        }
        static void OutputList(List<string> list)
        {
            //REFACTOR to save output to a file
            foreach(var line in list)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine(list.Count());
        }

        static void Main(string[] args)
        {
            string fname = kFname;
            List<string> dateList = new List<string>();

            //Get input args[0]: path to input file
            if (args.Length > 0 && args[0] != String.Empty)
            {
                fname = args[0];
            }

            dateList = ParseLines(kFname, maxDate);
            OutputList(dateList);

            Console.WriteLine("Press RETURN to exit."); //allow user to see output
            Console.ReadLine(); //pause
        }
    }
}
