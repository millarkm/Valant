# Valant
Coding Exercise

##Use Case
-----
Marketing has received a data file that may have been corrupted when it was exported. 
We need to extract all of the dates that have occurred in the past from this document.


##Criteria
-----
1. A date is defined as MMDDYYYY
    (ie: April 3, 2011 would be 03032011)

2. A past date is defined by any date that occurred before today.
    (ie: If today is 09/29/2016, all dates 08282016 or earlier would be valid)

3. The data file does not contain time information.

4. The resulting data output should be a list of dates.


##Design
-----
1. launch app with none or 1 input arg: filename

2. open disk file

3. load file contents with a single read

4. parse date field

5. single function test for valid date

6. save valid dates to list

7. output valid date list to console


##Design Rational
-----
1. Direct parsing of provided data, no initial transformations.

2. Alternate formats investigated:
- .Net DateTime Ticks and factors fo Ticks
- ASCII conversions
- POSIXct components


##Assumptions
-----
1. Dates could wrap lines


##Questions
-----
1. Do we want to save outputs (good and bad) to a file?

2. Is it important to know how many records were checked and how many were good and bad?
