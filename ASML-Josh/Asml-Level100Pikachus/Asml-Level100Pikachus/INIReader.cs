﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace Asml_Level100Pikachus
{
    public sealed class INIReader : FileReader
    {
        public INIReader(){}

        /// <summary>
        /// override for the ReadLines method for an ini reader
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public override List<Target> ReadLines(string filePath)
        {
            List<Target> targets = new List<Target>();
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                // First check is if the line is a blank line.
                if (line != "")
                {
                    // If not a blank line, trim the lines and split the lines delimited by an = sign
                    string trimmedLine = line.Trim();
                    string[] splits = trimmedLine.Split('=');

                    // If the trimmed line starts and ends with square brackets, then it is a group and is ok
                    if ((trimmedLine.StartsWith("[")) && (trimmedLine.EndsWith("]")))
                    {
                        Target target = new Target();
                        targets.Insert(targets.Count, target);
                    }
                    // If the line starts with and does not end with, or ends with and does not start with square brackets, we have a problem
                    // so throw exception.
                    else if ((trimmedLine.StartsWith("[") && !trimmedLine.EndsWith("]")) || (!trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]")))
                    {
                        throw new Exception("Invalid .ini format.");
                    }
                    // If the line was not split into 2 strings, the line is bad so throw exception.
                    else if (splits.Length != 2)
                    {
                        throw new Exception("Invalid .ini format.");
                    }
                    else if (splits.Length == 2)
                    {
                        splits[0] = splits[0].Trim();
                        splits[1] = splits[1].Trim();

                        //check the left side of the = to know which field to set
                        if (splits[0] == "Name")
                        {
                            targets.Last().name = splits[1];
                        }
                        else if (splits[0] == "x")
                        {
                            double x = Convert.ToDouble(splits[1]);
                            targets.Last().x = x;
                        }
                        else if (splits[0] == "y")
                        {
                            double y = Convert.ToDouble(splits[1]);
                            targets.Last().y = y;
                        }
                        else if (splits[0] == "z")
                        {
                            double z = Convert.ToDouble(splits[1]);
                            targets.Last().z = z;
                        }
                        else if (splits[0] == "Friend")
                        {
                            // set true/false appropriately
                            if (splits[1] == "yes")
                            {
                                targets.Last().friend = true;
                            }
                            else
                            {
                                targets.Last().friend = false;
                            }
                        }
                        else
                        {

                        }
                    }
                }
                // Else the line is a blank line, and there is nothing wrong with that
                else
                {

                }
            }
            // If we get to this point, none of the lines were improperly formated, so we return true
            return targets;
        }
    }
}