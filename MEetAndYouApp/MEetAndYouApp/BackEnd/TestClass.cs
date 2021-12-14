using System;

class TestClass
{
    static void Main(string[] args)
    {
        string str_directory = Environment.CurrentDirectory.ToString();
        Console.WriteLine(str_directory);

        Console.WriteLine(System.IO.Directory.GetParent(str_directory).ToString());
        string t = System.IO.Directory.GetParent(str_directory).ToString();


        Console.WriteLine(System.IO.Directory.GetParent(t).ToString());

        string r = System.IO.Directory.GetParent(t).ToString();

        Console.WriteLine(System.IO.Directory.GetParent(r).ToString());



        string m = System.IO.Directory.GetParent(r).ToString();

        Console.WriteLine(System.IO.Directory.GetParent(m).ToString());


        string l = System.IO.Directory.GetParent(m).ToString();

        Console.WriteLine(System.IO.Directory.GetParent(m).ToString());

        string g = System.IO.Path.Combine(l, "senior-project-archives");

        if (!Directory.Exists(g))
        {
            Directory.CreateDirectory(g);
            Console.WriteLine(g);
        }
    }
}