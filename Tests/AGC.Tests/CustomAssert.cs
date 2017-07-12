using Apollo.Virtual.AGC;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGC.Tests
{
    public static class CustomAssert
    {
        public static void AreEqual(int expected, OnesCompliment actual)
        {
            Assert.AreEqual((ushort)expected, actual.NativeValue);
        }

        public static void AreEqual(OnesCompliment expected, OnesCompliment actual)
        {
            Assert.AreEqual(expected.NativeValue, actual.NativeValue);
        }
    }
}
