using System;
using System.ComponentModel;
using System.Reflection.Metadata;

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
        //store format
        Console.WriteLine("Enter the tool number(number and offset eg.0101): ");
        tool_number = Console.ReadLine();
        //input thread pitch
        Console.WriteLine("Enter the thread pitch (standard): ");
        thread_pitch = Convert.ToDouble(Console.ReadLine());  
        //input depth
        Console.WriteLine("Enter the depth of the hole(EG: -.500): ");
        depth = Convert.ToDouble(Console.ReadLine());
        //input tapping head creep
        Console.WriteLine("Enter the tapping head creep(.100 default): ");
        tapping_head_creep = Convert.ToDouble(Console.ReadLine());
        if (tapping_head_creep == 0)
        {
            tapping_head_creep = 0.1;
        }
        //input index to add
        Console.WriteLine("Enter what number operation to add the tapping cycle(starting at 1): ");
        int place = Convert.ToInt32(Console.ReadLine());

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
        tapping_cycle_list.Add("");

        foreach (var line in tapping_cycle_list)
        {
            Console.WriteLine(line);
        }

        Console.WriteLine("Enter to continue...");
        Console.ReadLine();

        working_program.Insert(place, tapping_cycle_list);
        return working_program;
    }
    
    public override string modify_code_description()
    {
        return "The following code will be added with user input variables for tool number, thread pitch, depth, and tapping head creep:\nEnter to continue...";
    }
    
}