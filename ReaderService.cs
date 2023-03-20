using CSV_FileReader.Models;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_FileReader
{
    public class ReaderService
    {
        private readonly ApplicationDbContext dbc;

        public ReaderService(ApplicationDbContext dbc) 
        {
            this.dbc = dbc;
        }

        /// <summary>
        /// Imports the month from the given file path
        /// </summary>
        /// <param name="filePath">path to the CSV-file</param>
        public void ImportMonths(string filePath)
        {
            
            dbc.Times.RemoveRange(dbc.Times);//clears the table TIMES

            var data = File.ReadAllLines(filePath)//reads all lines of the .csv-file
                          .Skip(1)//skips the first one, because it is the header
                          .Select(l => l.Split(";"))//splits the line after every semicolom
                          .Select(csv => //csv is an array with the texts splitted
                          {
                              DateTime.TryParseExact(csv[1], "MMMM yyyy", CultureInfo.CurrentCulture,
                                 DateTimeStyles.None, out DateTime date);//csv[1] is the column with the month+ year, it parses the given date in a DateTime format
                              return new Time
                              {
                                  TimeID = csv[0],//first string before the first semicolum
                                  Month = date.ToString("MMMM"),//month of the date variable
                                  Year = date.Year,//year of the date variable (still an error if month is januar the year is always 1)
                              };
                          }).ToList();

            dbc.Times.AddRange(data);//AddRange adds the whole csv to the db
            dbc.SaveChanges();
        }

        /// <summary>
        /// Imports the data for the manufacturers from given file path
        /// </summary>
        /// <param name="filePath">path to the CSV-file</param>
        public void ImportManufacturer(string filePath)
        {
            dbc.Manufacturers.RemoveRange(dbc.Manufacturers);

            var lines = File.ReadLines(filePath);

            var csv = lines.Skip(1)
                        .Select(l => l.Split(";"))
                        .Select(csv => new Manufacturer
                        {
                            ManufacturerID = csv[0],
                            Title = csv[1],
                        });
           
            dbc.AddRange(csv);
            dbc.SaveChanges();
        }

        /// <summary>
        /// Imports the data for registrations of the given filepath
        /// </summary>
        /// <param name="filePath">path to the CSV-file</param>
        public void ImportRegistrations(string filePath)
        {
            dbc.Registrations.RemoveRange(dbc.Registrations);
            var lines = File.ReadLines(filePath);

            var csv = lines.Skip(1)
                        .Select(l => l.Split(";"))
                        .Select(csv => new Registration
                        {
                            ManufacturerID = csv[0],
                            TimeID = csv[1],
                            RegistrationType = csv[2],
                            Amount = int.Parse(csv[3]),                           
                        });

            dbc.AddRange(csv);
            dbc.SaveChanges();
        }

        /// <summary>
        /// Gets the top 3 manufacturers
        /// </summary>
        /// <returns>List with 3 manufacturer</returns>
        public List<string> MostRegisteredManufacturers()
        {
            var manufacturers = from registrations in dbc.Registrations
                        join manufactureres in dbc.Manufacturers 
                        on registrations.ManufacturerID equals manufactureres.ManufacturerID
                        group registrations by manufactureres.Title into g
                        orderby g.Count() descending
                        select g.Key;

            return manufacturers.Take(3).ToList();
        }

        /// <summary>
        /// Gets the month with most registrations
        /// </summary>
        /// <returns>string o fmonth</returns>
        public string MonthWithMostRegistrations()
        {
            var month = from reg in dbc.Registrations
                        group reg by reg.Time.Month into g
                        orderby g.Count() descending
                        select g.Key;

            var mostRegisteredMonth = month.FirstOrDefault();//else the month is a queriable string

            return mostRegisteredMonth;
        }

        /// <summary>
        /// Gets tthe year where the most renaults got registered
        /// </summary>
        /// <returns>int of year</returns>
        public int YearWithMostRegisteredRenaults()
        {
            var year = from reg in dbc.Registrations
                        where reg.Manufacturer.Title.Contains("Renault") && reg.Time.Year != 1
                        group reg by reg.Time.Year into g
                        orderby g.Count() descending
                        select g.Key;

            return year.FirstOrDefault();//gets year with most renaults
        }

    }
}
