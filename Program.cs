using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using System.Xml.Serialization;

class Program
{
    public static void Main(string[] args)
    {
        string filename = "";
        int choice = 7;
    
        while(choice != 0)
        {
            Console.WriteLine("Please select an option: \n 1. Split up rapids \n 2. Add tapping cycle \n 3. View Tapping Cycle Format \n 0. Exit");
            try
            {
            choice = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid choice. Please choose a number between 1 and 4.");
            }
            //if 1
            if (choice == 1)
            {
                modify_code modify = new modify_code();
                Console.WriteLine(modify.modify_code_description());
                Thread.Sleep(1000);
                filename = modify.ReadCode();
                parse_gcode parse = new parse_gcode(modify.rapid_split());
                parse.write_to_file(filename);
                
            
            }
            else if (choice == 2)
            {
                //add tapping cycle
                tapping_cycle tap = new tapping_cycle();
                Console.WriteLine(tap.modify_code_description());
                Thread.Sleep(1000);
                filename = tap.ReadCode();
                parse_gcode parse = new parse_gcode(tap.add_tapping_cycle());
                parse.write_to_file(filename);
    
            }
            else if (choice == 3)
            {
                //view tapping cycle format
                tapping_cycle tap = new tapping_cycle();
                //print out tapping cycle format
                Console.WriteLine(tap.view_format());
    
            }            
            else if (choice == 0)
            {
                //exit
                Console.WriteLine("Goodbye!");
            }

            else
            {
                Console.WriteLine("Invalid choice. Please choose a number between 1 and 4.");
            }
        }
    }

}
