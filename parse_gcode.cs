using System;
using System.Reflection.Metadata;
using Microsoft.Extensions.Configuration;

public class parse_gcode 
{
    private string? baseFilepath;
    private string? filepath;
    private string Program_name = "";
    private List<List<string>> writeFile = new List<List<string>>();
    public parse_gcode()
    {
        var config = readconfig();
        filepath = config["filepath"];
        baseFilepath = config["filepath"];
        if (filepath == null)
        {
            Console.WriteLine("filepath not found in config.");
            Environment.Exit(1);
        }
        while (!File.Exists(filepath))
        {
            filepath = baseFilepath;
            Console.WriteLine("Enter the name of the program: ");
            Program_name = Console.ReadLine();
            filepath = filepath + Program_name + ".nc";  
            Console.WriteLine(filepath);
            try
            {
                string checkFile = File.ReadAllText(filepath);
                break; 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
            }
        }
    }

    public parse_gcode(List<List<string>> updatedCompoundList)
    {
        var config = readconfig();
        filepath = config["filepath"];
        if (filepath == null)
        {
            Console.WriteLine("filepath not found in config.");
        }
        writeFile = updatedCompoundList;
    }

    public IConfigurationRoot readconfig()
    {
        var config = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("parameters.json", optional: false, reloadOnChange: true)
        .Build();
        return config;
    }

    public List<List<string>> getCompoundList()
    {
        string gcode = File.ReadAllText(filepath);

        //Split gcode into operations
        string[] operations = gcode.Split(new string[] { "\n\n" }, StringSplitOptions.None);

        //Outer list to hold operations
        List<List<string>> compoundListProgram = new List<List<string>>();

        foreach (var operation in operations)
        {
            //Split each operation into individual lines
            string[] lines = operation.Split(new string[] { "\n" }, StringSplitOptions.None);

            // Add the lines as an inner list to the compound list
            compoundListProgram.Add(new List<string>(lines));
        }

        // Output the compound list (for testing)
        foreach (var operation in compoundListProgram)
        {
            Console.WriteLine("Operation:");
            foreach (var line in operation)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine(); // Add a blank line between operations
        }   

        return compoundListProgram;
    }

    public void write_to_file(string filename)
    {
        // Check if writeFile is populated
        if (writeFile.Count == 0)
        {
            Console.WriteLine("No updated G-code to write.");
            return;
        }

        // Convert the compound list back to a string
        List<string> updatedLines = new List<string>();
        
        foreach (var operation in writeFile)
        {
            // Join each operation's lines back together
            updatedLines.Add(string.Join("\n", operation));
        }

        // Join all operations with double newlines to match the original format
        string updatedGCode = string.Join("\n\n", updatedLines);

        // Write the updated G-code back to the file
        try
        {
            File.WriteAllText(filename, updatedGCode);
            Console.WriteLine("G-code has been successfully updated and written to the file.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error writing to file: " + ex.Message);
        }        
    }

    public string getFilepath()
    {
        return filepath;
    }

}