using System;
using System.Runtime.CompilerServices;

class Program
{
    protected static int IntInput(string label)
    {
        Console.WriteLine($"Please enter a valid integer for {label}");
        string? input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input) || input == null)
        {
            throw new ArgumentException("Input cannot be null or empty");
        }
        if(int.TryParse(input,out int result))
        {
            if(result > 15)
            {
                throw new ArgumentException("Number too high!");
            }else if(result < 1)
            {
                throw new ArgumentException("Number too low!");
            }
            Console.WriteLine($"You entered: {result}");
            return result;
        }
        throw new ArgumentException("Not a valid integer");
    }
     static void Main(string[] args)
    {
        int first = IntInput("These Nuts: ");
        Console.WriteLine(first);
    }
     
}
