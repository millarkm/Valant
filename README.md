# Valant
Coding Exercise

Schedule Tracking

1030p-130a  assumptions, design, function prototype
830a-1030a  code, test, gen 1M row test data
1030a-1130a documents
1130a       github

Use Case

Marketing has received a data file that may have been corrupted when it was exported. 
We need to extract all of the dates that have occurred in the past from this document.


Criteria
1. A date is defined as MMDDYYYY
    (ie: April 3, 2011 would be 03032011)

2. A past date is defined by any date that occurred before today.
    (ie: If today is 09/29/2016, all dates 08282016 or earlier would be valid)

3. The data file does not contain time information.

4. The resulting data output should be a list of dates.


Design

1. launch app with none or 1 input arg: filename

2. open disk file

3. load file contents with a single read

4. parse date field

5. single function test for valid date

6. save valid dates to list

7. output valid date list to console


Design Rational

1. quick turn around of solution

2. seperate functionality for future refactoring

3. handle as many corruption cases as possible with minimum coding

4. configurable varitions for first version: filename, date pattern, min year range, data column index, delim, max date

5. Refactor future versions for max use and value if similar checks


Assumptions

1. File format
    ascii text
    tab delimited
    date in column 2 (index 1)

2. Valid date format: MMDDYYYY

3. Future dates (ie greater than or equal today's date) are ignored

4. Output valid dates to console assumes we have a contiguous date range for a specific period (ie one or two months).
   Any gaps or holes in the output will be easily detectable with a quick visual scan.
   
5. Input file will contain less than 1M records. Larger files may require read buffering (out of scope for this exercise).


Corruption Cases

1. emptpy lines

2. less than full record

3. string for date (not convertable due to alpha chars)

4. improper format date (missing leading zeros)


Questions

1. Do we want to save outputs (good and bad) to a file?

2. Is it important to know how many records were checked and how many were good and bad?

3. Are the file size assumptions valid?

4. How much value is there in initial configurability (input args) vs hard coded values. 
    
    
