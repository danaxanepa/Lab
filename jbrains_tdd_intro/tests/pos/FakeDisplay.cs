using under_test;

namespace jbrains_tdd_intro.tests.pos
{
    internal class FakeDisplay : Display
    {
        public string LastMessage;
        public void Print(string message)
        {
            LastMessage = message;
        }
    }
}