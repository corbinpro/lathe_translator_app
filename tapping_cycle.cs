using System;
using System.ComponentModel;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

public class tapping_cycle : modify_code
{
    //store format
    //input thread pitch
    private string tool_number = "";
    private double thread_pitch = 0;
    //input depth
    private double depth = 0;
    //input tapping head creep
    private double tapping_head_creep = 0;

    private double feed_rate = 0;
    private double ninty_perc_feed = 0;
    private int place = 0;


    public tapping_cycle()
    {

    }

    public List<List<string>> add_tapping_cycle()
    {
        //INPUTS

        //store format
        Console.WriteLine("Enter the tool number (number and offset e.g., 0101): ");
        string tool_number = Console.ReadLine();

        //input thread pitch
        double thread_pitch;
        while (true)
        {
            try
            {
                Console.WriteLine("Enter the thread pitch (standard): ");
                thread_pitch = Convert.ToDouble(Console.ReadLine());
                break;
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a numeric value for the thread pitch.");
            }
        }

        //input depth
        double depth;
        while (true)
        {
            try
            {
                Console.WriteLine("Enter the depth of the hole (e.g., -0.500): ");
                depth = Convert.ToDouble(Console.ReadLine());
                break;
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a numeric value for the depth.");
            }
        }

        //input tapping head creep
        double tapping_head_creep;
        while (true)
        {
            try
            {
                Console.WriteLine("Enter the tapping head creep (default is 0.100): ");
                tapping_head_creep = Convert.ToDouble(Console.ReadLine());
                if (tapping_head_creep == 0)
                {
                    tapping_head_creep = 0.1;
                }
                break;
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a numeric value for the tapping head creep.");
            }
        }

        //input index to add
        int place;
        while (true)
        {
            try
            {
                Console.WriteLine("Enter what number operation to add the tapping cycle (starting at 1): ");
                place = Convert.ToInt32(Console.ReadLine());
                break;
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter an integer value for the operation number.");
            }
        }


        //MATH SECTION
        //convert thread pitch to feed rate.
        feed_rate = 1 / thread_pitch;

        //calculate 90% feed rate
        ninty_perc_feed = feed_rate * 0.9;

        //subtract head creep from depth
        depth = depth - tapping_head_creep;

        //each add is a line of the operation. 
        List<string> tapping_cycle_list = new List<string>();
        tapping_cycle_list.Add("");
        tapping_cycle_list.Add("M1");
        tapping_cycle_list.Add("T" + tool_number);
        tapping_cycle_list.Add("G99");
        tapping_cycle_list.Add("G97 S25 M13");
        tapping_cycle_list.Add("G4 X1.25");
        tapping_cycle_list.Add("G0 Z0.6");
        tapping_cycle_list.Add("G0 X0.0");
        tapping_cycle_list.Add("G0 Z0.2");
        tapping_cycle_list.Add("G32 X0. Z" + depth + " F" + ninty_perc_feed.ToString("F3"));
        tapping_cycle_list.Add("M05 G4 X0.5");
        tapping_cycle_list.Add("M14");
        tapping_cycle_list.Add("G32 Z" + depth + " F" + feed_rate.ToString("F3"));
        tapping_cycle_list.Add("Z0.6");
        tapping_cycle_list.Add("M9");
        tapping_cycle_list.Add("G28 U0.");
        tapping_cycle_list.Add("G28 W0.");
        tapping_cycle_list.Add("");

        foreach (var line in tapping_cycle_list)
        {
            Console.WriteLine(line);
        }

        Console.WriteLine($"The following code will be entered as the {place} operaion\nEnter to continue...");
        Console.ReadLine();
        
        working_program.Insert(place, tapping_cycle_list);
        foreach (var operation in working_program)
        {
            Console.WriteLine("Operation:");
            foreach (var line in operation)
            {
                Console.WriteLine(line);
            }
        }
        Console.WriteLine("Press enter to export the updated G-code.");
        Console.ReadLine();
        return working_program;
    }
    
    public override string modify_code_description()
    {
        return "The following code will be added with user input variables for tool number, thread pitch, depth, and tapping head creep:\nEnter to continue...";
    }
    
    public string view_format()
    {
        return "Tapping Cycle Format:\n" +
               "M1\n" +
               "T(tool number)\n" +
               "G99\n" +
               "G97 S25 M13\n" +
               "G4 X1.25\n" +
               "G0 Z0.6\n" +
               "G0 X0.0\n" +
               "G0 Z0.2\n" +
               "G32 X0. Z(depth - tapping head creep) F(feed rate)\n" +
               "M05 G4 X0.5\n" +
               "M14\n" +
               "G32 Z(depth - tapping head creep) F(feed rate)\n" +
               "Z0.6\n";
    }
}