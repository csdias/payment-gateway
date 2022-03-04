using System;
using System.Text;

namespace FrameworksAndDrivers.UnitTests.Helpers
{
    public static class TestHelpers
    {
        public static Random Rnd = new Random();

        public static string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * Rnd.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        public static int RandomInt(int size, bool zero = true)
        {
            var j = zero ? 0 : 1;

            StringBuilder ch = new StringBuilder();
            for (int i = j; i < size; i++)
            {
                ch.Append("9");
            }

            return Rnd.Next(Convert.ToInt32(ch.ToString()));
        }

        public static Guid GenerateEmptyId() => Guid.Empty;
        public static Guid GenerateId() => Guid.NewGuid();
    }
}
