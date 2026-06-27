using PatternMatchingConsoleDemo.Examples;

namespace PatternMatchingConsoleDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PropertyPatternExamples.Run();
            RelationalPatternExamples.Run();
            LogicalPatternExamples.Run();
            TypePatternExamples.Run();
            RecursivePatternExamples.Run();
            SwitchExpressionExamples.Run();

            Console.WriteLine("End");
        }
    }
}
