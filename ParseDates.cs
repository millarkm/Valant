using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Globalization;
using System.IO; //ReadFile
using System.Diagnostics; //Debugger

namespace DataTools
{
    class ParseDates
    {
        /*
         * Name:
         *      ParseDates [<filename>]
         *      
         * Description: 
         *      Open a text datafile and parse through the records looking for valid dates
         *      Send a list of valid dates to the screen
         *      
         * Input args:
         *      arg0    file name
         *      
         * Author:
         *      Kevin Millar
         *      
         * Revisions:
         *      v0   08Sep2016
         *
         * REFACTOR:
         *      input args
         *      output results to file
         *      log invalid records to error file
        */

        const string kFname = "MarketingDataFile.txt"; //default file name in case no input arg is supplied
        const string kDatePattern = "MMddyyyy"; //expected format of date being inspecting
        const int kDateSize = 8; //MMDDYYYY
        const int kMinYear = 2000; //set the minimum acceptable year
        const string kErrmsgFileNotFound = "Data file not found.";
        static DateTime maxDate = DateTime.Today; // dates >= today are ignored

        static bool isValidDate(string value, int minyear, DateTime maxyear, out DateTime dt_out)
        {
            // Determine if a passed in string is a valid date:
            //  - must match the kDatePattern format
            //  - must be within a user defined acceptable date range based on 'minyear' param

            DateTime dt = DateTime.Today;
            bool result = false;

            try
            {
                if (DateTime.TryParseExact(value, kDatePattern, System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                {
                    if (dt.Year >= minyear & dt.Year <= maxyear.Year)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                string x = ex.Message;
                //NoOp - invalid date
            }
            finally
            {
                dt_out = dt;
            }
            return result;
        }

        static List<string> ParseLines(string fname, DateTime maxdate)
        {
            // open a disk file and parse thru the data looking for valid dates

            DateTime validDate;
            string ckDate;
            List<string> result = new List<string>();
            int pos = 0;

            try
            {
                IEnumerable<string> lines2 = File.ReadLines(fname);
                try
                {
                    string dates = String.Join("", lines2); // combine all lines read in into a single string for walking

                    // iterate the string reading a chunk at a time
                    while (pos <= dates.Length - kDateSize)
                    {
                        ckDate = dates.Substring(pos, kDateSize); // parse the next chunk

                        // validate the parsed chunk
                        if (isValidDate(ckDate, kMinYear, maxdate, out validDate))
                        {
                            // valid date format
                            result.Add(ckDate);
                        }
                        else
                        {
                            // NoOp - invalid date skipped
                        }
                        
                        pos++;
                    }                        
                }
                catch (Exception)
                {
                    // REFACTOR - log to error file
                    // NoOp invalid format or missing data is skipped
                }
        
            }
            catch (FileNotFoundException)
            {

                Console.WriteLine("{0} Filename: {1}", kErrmsgFileNotFound, kFname);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Numer of checks: {0}", pos.ToString()); //DEBUG
            return result;
        }

        static void OutputList(List<string> list)
        {
            // print the input list to stdout
            foreach (var line in list)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("Number of valid dates: {0}",list.Count());
        }

        static List<string> GenTickDates()
        {
            // temp utility to look at the ticks of recent dates
            DateTime dt = DateTime.Now;
            List<string> result = new List<string>();
            
            int k = 0;
            while (k < 20)
            {
                result.Add(dt.Date.Ticks.ToString());
                dt = dt.AddDays(-1);
                k++;
            }
            return result;
        }

        static void Main(string[] args)
        {
            string fname = kFname;
            List<string> dateList = new List<string>();

            // Get input args[0]: path to input file
            if (args.Length > 0 && args[0] != String.Empty)
            {
                fname = args[0];
            }
            else
            {
                fname = kFname; // default file name
            }

            //TEST dateList = GenTickDates();
            dateList = ParseLines(fname, maxDate);
            OutputList(dateList);


             Console.WriteLine("Press return to continue");
             Console.ReadLine();
        }
    }
}
