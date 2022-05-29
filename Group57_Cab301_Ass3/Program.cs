using System;
//using BSTreeInterface;
namespace Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            MovieSystem system = new MovieSystem();
            Member Shaun = new Member("Shaun", "Hoang", "0450880110", "2001");
            Member Marshall = new Member("Marshall", "Le", "0987654321", "1234");
            Member Ni = new Member("Ni", "Le", "0123456789", "5678");

            system.addMember(Shaun);
            system.addMember(Marshall);
            system.addMember(Ni);
            system.MainMenu();
        }
    }
}


