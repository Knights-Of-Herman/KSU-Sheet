using KnightsOfHerman.XUnitTesting.Helpers;

namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            TestCharacter.DBCreateComplexAsync();
        }
    }
}
