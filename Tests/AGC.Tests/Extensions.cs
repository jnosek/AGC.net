using Apollo.Virtual.AGC;

namespace AGC.Tests
{
    static class Extensions
    {
        public static void Execute(this Processor processor, int cycles)
        {
            for(int i = 0; i < cycles; i++)
            {
                processor.Execute();
            }
        }
    }
}
