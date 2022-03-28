using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FinalTest_Advanced_PettingZoo
{
    internal class TextFileProcessor
    {
        public static List<T> LoadFromTextFile<T>(string filePath) where T : class, new()
        {
            // Gets all data from filePath and makes to List
            var lines = System.IO.File.ReadAllLines(filePath).ToList();
            // Makes a new list
            List<T> output = new List<T>();
            // Makes a new object of T
            T entry = new T();
            // Gets the type of T and its properties
            PropertyInfo[] cols = entry.GetType().GetProperties();

            // Checks to be sure we have at least one header row and one data row
            if (lines.Count < 2)
            {
                throw new IndexOutOfRangeException("The file was either empty or missing.");
            }

            // Splits the header into one column header per entry
            var headers = lines[0].Split(',');

            // Removes the header
            lines.RemoveAt(0);

            foreach (var row in lines)
            {
                entry = new T();

                // Splits the row into individual columns.
                var vals = row.Split(',');

                // Loops through each header entry
                for (var i = 0; i < headers.Length; i++)
                {
                    foreach (var col in cols)
                    {
                        if (col.Name == headers[i])
                        {
                            // If the property is "RentingHistory"
                            if (col.Name == "RentingHistory")
                            {
                                // If theres more values than the length of headers. Is there any saved RentingHistory.
                                if (vals.Length > headers.Length)
                                {
                                    List<string> history = new List<string>();

                                    for (int x = 5; x < vals.Length; x++)
                                    {
                                        history.Add(vals[x]);
                                    }
                                    // Adds the list of history to entry
                                    col.SetValue(entry, history);
                                }
                                else
                                {
                                    List<string> history = new List<string>();
                                    // Adds the list of history to entry
                                    col.SetValue(entry, history);
                                }
                            }
                            else
                            {
                                col.SetValue(entry, Convert.ChangeType(vals[i], col.PropertyType));
                            }
                        }
                    }
                }

                output.Add(entry);
            }

            return output;
        }

        public static void SaveToTextFile<T>(List<T> data, string filePath) where T : class
        {
            // List<string> to return to TextFile
            List<string> lines = new List<string>();
            // Building one line at a time for "lines"
            StringBuilder line = new StringBuilder();

            if (data == null || data.Count == 0)
            {
                throw new ArgumentNullException("You must add at least one value.");
            }
            var cols = data[0].GetType().GetProperties();

            // Loops through each column and gets the name so it can comma
            // separate it into the header row.
            foreach (var col in cols)
            {
                line.Append(col.Name);
                line.Append(",");
            }

            // Adds the column header entries to the first line (removing
            // the last comma from the end first).
            lines.Add(line.ToString().Substring(0, line.Length - 1));

            foreach (var row in data)
            {
                line = new StringBuilder();

                foreach (var col in cols)
                {
                    line.Append(col.GetValue(row));
                    line.Append(",");
                    //If the property is "RentingHistory"
                    if (col.Name == "RentingHistory")
                    {
                        //Puts however many strings in "history"
                        List<string> history = (List<string>)(col.GetValue(row));
                        for (int y = 0; y < history.Count; y++)
                        {
                            //Adds the string + "," to line.
                            line.Append(history[y]);
                            line.Append(",");
                        }
                    }
                }

                // Adds the row to the set of lines (removing
                // the last comma from the end first).
                lines.Add(line.ToString().Substring(0, line.Length - 1));
            }

            System.IO.File.WriteAllLines(filePath, lines);
        }
    }
}