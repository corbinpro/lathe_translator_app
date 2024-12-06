using System;
using System.ComponentModel;
using System.Reflection.Metadata;

public class tapping_cycle : modify_code
{
    //store format
    //input thread pitch
    private string tool_number = "";
    private int thread_pitch = 0;
    //input depth
    private int depth = 0;
    //input tapping head creep
    private int tapping_head_creep = 0;

    private int feed_rate = 0;


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
        thread_pitch = Convert.ToInt32(Console.ReadLine());  
        //input depth
        Console.WriteLine("Enter the depth of the hole: ");
        depth = Convert.ToInt32(Console.ReadLine());
        //input tapping head creep
        Console.WriteLine("Enter the tapping head creep: ");
        tapping_head_creep = Convert.ToInt32(Console.ReadLine());
        //input index to add
        Console.WriteLine("Enter what number operation to add the tapping cycle(starting at 0): ");
        int place = Convert.ToInt32(Console.ReadLine());

        //math section
        //
        //

        //convert variables back into string format
        //
        //

        //each add is a line of the operation. 
        List<string> tapping_cycle_list = new List<string>();
        tapping_cycle_list.Add("");
        tapping_cycle_list.Add("");
        tapping_cycle_list.Add("");
        tapping_cycle_list.Add("");
        tapping_cycle_list.Add("");
        tapping_cycle_list.Add("");
        tapping_cycle_list.Add("");
        tapping_cycle_list.Add("");
        tapping_cycle_list.Add("");
        tapping_cycle_list.Add("");
        tapping_cycle_list.Add("");
        tapping_cycle_list.Add("");
        tapping_cycle_list.Add("");
        tapping_cycle_list.Add("");
        tapping_cycle_list.Add("");
        tapping_cycle_list.Add("");
        tapping_cycle_list.Add("");
        tapping_cycle_list.Add("");
        tapping_cycle_list.Add("");
        tapping_cycle_list.Add("");
        tapping_cycle_list.Add("");



        working_program.Insert(place, tapping_cycle_list);
        return working_program;
    }
    
    public override string modify_code_description()
    {
        return "The following code will be added with user input variables for tool number, thread pitch, depth, and tapping head creep:\n"+"";
    }
    
}