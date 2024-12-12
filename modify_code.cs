using System;
using System.Reflection.Metadata;

public class modify_code
{
    private string filepath = "";
    protected List<List<string>> working_program = new List<List<string>>();
    //constructor
    public modify_code()
    {

    } 
    //receive compound list
    public string ReadCode()
    {
        parse_gcode parse = new parse_gcode();
        working_program = parse.getCompoundList();
        return parse.getFilepath();
    }

    //rapid split method 
    public List<List<string>> rapid_split()
    {
        var updatedCompoundList = new List<List<string>>();

        foreach (var operation in working_program)
        {
            var updatedOperation = new List<string>();

            foreach (var line in operation)
            {
                //split the line into parts
                var parts = new List<string>(line.Split(' '));

                //Check if line contains G0, X, Z
                if (parts.Contains("G0") && ContainsCoordinate(parts, "X") && ContainsCoordinate(parts, "Z"))
                {
                    //Extract the X value
                    string xValue = ExtractCoordinate(parts, "X");

                    //Remove the X value from the original line
                    RemoveCoordinate(parts, "X");

                    //Add the modified G0 line back to the updated operation
                    updatedOperation.Add(string.Join(" ", parts));

                    //Add a new line for the X value prefixed with G0
                    updatedOperation.Add($"G0 {xValue}");
                }
                else
                {
                    //If the line doesn't match, just add it as-is
                    updatedOperation.Add(string.Join(" ", parts));
                }
            }

            updatedCompoundList.Add(updatedOperation);
        }

        return updatedCompoundList;
    }

    private bool ContainsCoordinate(List<string> parts, string axis)
    {
        // Check if the list contains a part that starts with the given prefix
        foreach (var part in parts)
        {
            if (part.StartsWith(axis))
            {
                return true;
            }
        }
        return false;
    }

    private string ExtractCoordinate(List<string> parts, string axis)
    {
        // Find the part that starts with the given prefix and return it
        foreach (var part in parts)
        {
            if (part.StartsWith(axis))
            {
                return part; // Return the coordinate
            }
        }
        return string.Empty;
    }
    
    private void RemoveCoordinate(List<string> parts, string axis)
    {
        // Remove the part that starts with the given prefix
        parts.RemoveAll(part => part.StartsWith(axis));
    }

    //description of modify code(use override in tapping cycle)
    public virtual string modify_code_description()
    {
        return "Seperating all G0 lines containing both x and z values into two separate lines...";
    }

}