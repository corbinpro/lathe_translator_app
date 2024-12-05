using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;

class Program
{
    public static void Main(string[] args)
    {
        string? program = "";
        Console.WriteLine("Program number:");
        program = Console.ReadLine();

        int choice = 0;
        Console.WriteLine("Please select an option: \n 1. Split up rapids \n 2. Add tapping cycle \n 3. Multi offset tools \n 4. Part catcher \n 0. Exit");
        choice = Convert.ToInt32(Console.ReadLine());
    
        while(choice != 0)
        {
            //if 1
            if (choice == 1)
            {
                
            
            }
            else if (choice == 2)
            {
                //add tapping cycle
                //prompt user for tapping cycle parameters
    
            }
            else if (choice == 3)
            {
                //multi offset tools
                //prompt user for number of tools
    
            }
            else if (choice == 4)
            {
                //part catcher
                //prompt user for part catcher parameters
    
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
