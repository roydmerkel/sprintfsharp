using System;
using sprintf;
using NUnit.Framework;

namespace testsprintf
{
    [TestFixture]
    public class SprintfTest
    {
        public SprintfTest()
        {
        }

        [Test]
        public void TestPercentD()
        {
            String s;
            s = Sprintf.sprintf("%d", 1);
            Assert.AreEqual(s, "1");

            s = Sprintf.sprintf("%0d", 1);
            Assert.AreEqual(s, "1");

            s = Sprintf.sprintf("%010d", 1);
            Assert.AreEqual(s, "0000000001");

            s = Sprintf.sprintf("%+010d", 1);
            Assert.AreEqual(s, "+000000001");

            s = Sprintf.sprintf("%-+010d", 1);
            Assert.AreEqual(s, "+1        ");

            s = Sprintf.sprintf("% 010d", 1);
            Assert.AreEqual(s, " 000000001");

            s = Sprintf.sprintf("% 10d", 1);
            Assert.AreEqual(s, "         1");

            s = Sprintf.sprintf("%010.5d", 1);
            Assert.AreEqual(s, "     00001");

            s = Sprintf.sprintf("%+010.5d", 1);
            Assert.AreEqual(s, "    +00001");

            s = Sprintf.sprintf("%+-010.5d", 1);
            Assert.AreEqual(s, "+00001    ");

            s = Sprintf.sprintf("%+-'010.5d", 1);
            Assert.AreEqual(s, "+00001    ");

            s = Sprintf.sprintf("%+-'010.5d", 1000000000);
            Assert.AreEqual(s, "+1,000,000,000");

            s = Sprintf.sprintf("%+-'010.5hd", 32767);
            Assert.AreEqual(s, "+32,767   ");

            s = Sprintf.sprintf("%+-'010.5hhd", 127);
            Assert.AreEqual(s, "+00127    ");

            s = Sprintf.sprintf("%+-'010.5ld", 1000000000);
            Assert.AreEqual(s, "+1,000,000,000");

            s = Sprintf.sprintf("%+-'010.5lld", 1000000000);
            Assert.AreEqual(s, "+1,000,000,000");

            s = Sprintf.sprintf("%+-'010.5zd", 1000000000);
            Assert.AreEqual(s, "+1,000,000,000");

            s = Sprintf.sprintf("%+-'010.5jd", 1000000000);
            Assert.AreEqual(s, "+1,000,000,000");

            s = Sprintf.sprintf("%+-'010.5td", 1000000000);
            Assert.AreEqual(s, "+1,000,000,000");

            s = Sprintf.sprintf("%+-'010.5qd", 1000000000);
            Assert.AreEqual(s, "+1,000,000,000");

            s = Sprintf.sprintf("%+-'010.5Id", 1000000000);
            Assert.AreEqual(s, "+1,000,000,000");

            s = Sprintf.sprintf("%+-'010.5I32d", 1000000000);
            Assert.AreEqual(s, "+1,000,000,000");

            s = Sprintf.sprintf("%+-'010.5I64d", 1000000000);
            Assert.AreEqual(s, "+1,000,000,000");

            s = Sprintf.sprintf("%+-'020.5I64d", 1000000000);
            Assert.AreEqual(s, "+1,000,000,000      ");

            s = Sprintf.sprintf("%+'020.5I64d", 1000000000);
            Assert.AreEqual(s, "      +1,000,000,000");

            s = Sprintf.sprintf("%+-'020.0d", 0);
            Assert.AreEqual(s, "+                   ");

            s = Sprintf.sprintf("%+'020.0d", 0);
            Assert.AreEqual(s, "                   +");

            s = Sprintf.sprintf("%+'0*.*d", 20, 0, 0);
            Assert.AreEqual(s, "                   +");

            s = Sprintf.sprintf("ab%+'0*.*dcd", 20, 0, 0);
            Assert.AreEqual(s, "ab                   +cd");

            s = Sprintf.sprintf("\"%d\"\n", 0);
            Assert.AreEqual(s, "\"0\"\n");

            s = Sprintf.sprintf("\"%+0*d\"\n", 20, 0);
            Assert.AreEqual(s, "\"+0000000000000000000\"\n");

            s = Sprintf.sprintf("%hhd", 255);
            Assert.AreEqual(s, "-1");

            s = Sprintf.sprintf("%hhd", -128);
            Assert.AreEqual(s, "-128");

            s = Sprintf.sprintf("%010hhd", -128);
            Assert.AreEqual(s, "-000000128");

            s = Sprintf.sprintf("%0.10hhd", -128);
            Assert.AreEqual(s, "-0000000128");

            s = Sprintf.sprintf("%020.10hhd", -128);
            Assert.AreEqual(s, "         -0000000128");

            s = Sprintf.sprintf("%'#020.5hd", 32767);
            Assert.AreEqual(s, "              32,767");

            s = Sprintf.sprintf("%'#020.10hd", 32767);
            Assert.AreEqual(s, "          000032,767");

            s = Sprintf.sprintf("%'#hd", 32767);
            Assert.AreEqual(s, "32,767");

            s = Sprintf.sprintf("%#020.5hd", 32767);
            Assert.AreEqual(s, "               32767");

            s = Sprintf.sprintf("%#020.10hd", 32767);
            Assert.AreEqual(s, "          0000032767");

            s = Sprintf.sprintf("%#hd", 32767);
            Assert.AreEqual(s, "32767");
        }

        [Test]
        public void TestPercentU()
        {
            String s;
            s = Sprintf.sprintf("%u", 1);
            Assert.AreEqual(s, "1");

            s = Sprintf.sprintf("%0u", 1);
            Assert.AreEqual(s, "1");

            s = Sprintf.sprintf("%010u", 1);
            Assert.AreEqual(s, "0000000001");

            s = Sprintf.sprintf("%+010u", 1);
            Assert.AreEqual(s, "+000000001");

            s = Sprintf.sprintf("%-+010u", 1);
            Assert.AreEqual(s, "+1        ");

            s = Sprintf.sprintf("% 010u", 1);
            Assert.AreEqual(s, " 000000001");

            s = Sprintf.sprintf("% 10u", 1);
            Assert.AreEqual(s, "         1");

            s = Sprintf.sprintf("%010.5u", 1);
            Assert.AreEqual(s, "     00001");

            s = Sprintf.sprintf("%+010.5u", 1);
            Assert.AreEqual(s, "    +00001");

            s = Sprintf.sprintf("%+-010.5u", 1);
            Assert.AreEqual(s, "+00001    ");

            s = Sprintf.sprintf("%+-'010.5u", 1);
            Assert.AreEqual(s, "+00001    ");

            s = Sprintf.sprintf("%+-'010.5u", 1000000000);
            Assert.AreEqual(s, "+1,000,000,000");

            s = Sprintf.sprintf("%+-'010.5hu", 32767);
            Assert.AreEqual(s, "+32,767   ");

            s = Sprintf.sprintf("%+-'010.5hhu", 127);
            Assert.AreEqual(s, "+00127    ");

            s = Sprintf.sprintf("%+-'010.5lu", 1000000000);
            Assert.AreEqual(s, "+1,000,000,000");

            s = Sprintf.sprintf("%+-'010.5llu", 1000000000);
            Assert.AreEqual(s, "+1,000,000,000");

            s = Sprintf.sprintf("%+-'010.5zu", 1000000000);
            Assert.AreEqual(s, "+1,000,000,000");

            s = Sprintf.sprintf("%+-'010.5ju", 1000000000);
            Assert.AreEqual(s, "+1,000,000,000");

            s = Sprintf.sprintf("%+-'010.5tu", 1000000000);
            Assert.AreEqual(s, "+1,000,000,000");

            s = Sprintf.sprintf("%+-'010.5qu", 1000000000);
            Assert.AreEqual(s, "+1,000,000,000");

            s = Sprintf.sprintf("%+-'010.5Iu", 1000000000);
            Assert.AreEqual(s, "+1,000,000,000");

            s = Sprintf.sprintf("%+-'010.5I32u", 1000000000);
            Assert.AreEqual(s, "+1,000,000,000");

            s = Sprintf.sprintf("%+-'010.5I64u", 1000000000);
            Assert.AreEqual(s, "+1,000,000,000");

            s = Sprintf.sprintf("%+-'020.5I64u", 1000000000);
            Assert.AreEqual(s, "+1,000,000,000      ");

            s = Sprintf.sprintf("%+'020.5I64u", 1000000000);
            Assert.AreEqual(s, "      +1,000,000,000");

            s = Sprintf.sprintf("%+-'020.0u", 0);
            Assert.AreEqual(s, "+                   ");

            s = Sprintf.sprintf("%+'020.0u", 0);
            Assert.AreEqual(s, "                   +");

            s = Sprintf.sprintf("%+'0*.*u", 20, 0, 0);
            Assert.AreEqual(s, "                   +");

            s = Sprintf.sprintf("ab%+'0*.*ucd", 20, 0, 0);
            Assert.AreEqual(s, "ab                   +cd");

            s = Sprintf.sprintf("\"%u\"\n", 0);
            Assert.AreEqual(s, "\"0\"\n");

            s = Sprintf.sprintf("\"%+0*u\"\n", 20, 0);
            Assert.AreEqual(s, "\"+0000000000000000000\"\n");

            s = Sprintf.sprintf("%hhu", 255);
            Assert.AreEqual(s, "255");

            s = Sprintf.sprintf("%hhu", -128);
            Assert.AreEqual(s, "128");

            s = Sprintf.sprintf("%010hhu", -128);
            Assert.AreEqual(s, "0000000128");

            s = Sprintf.sprintf("%0.10hhu", -128);
            Assert.AreEqual(s, "0000000128");

            s = Sprintf.sprintf("%020.10hhu", -128);
            Assert.AreEqual(s, "          0000000128");

            s = Sprintf.sprintf("%'#020.5hu", 32767);
            Assert.AreEqual(s, "              32,767");

            s = Sprintf.sprintf("%'#020.10hu", 32767);
            Assert.AreEqual(s, "          000032,767");

            s = Sprintf.sprintf("%'#hu", 32767);
            Assert.AreEqual(s, "32,767");

            s = Sprintf.sprintf("%#020.5hu", 32767);
            Assert.AreEqual(s, "               32767");

            s = Sprintf.sprintf("%#020.10hu", 32767);
            Assert.AreEqual(s, "          0000032767");

            s = Sprintf.sprintf("%#hu", 32767);
            Assert.AreEqual(s, "32767");
        }

        [Test]
        public void TestPercentO()
        {
            String s;
            s = Sprintf.sprintf("%o", 1);
            Assert.AreEqual(s, "1");

            s = Sprintf.sprintf("%0o", 1);
            Assert.AreEqual(s, "1");

            s = Sprintf.sprintf("%010o", 1);
            Assert.AreEqual(s, "0000000001");

            s = Sprintf.sprintf("%+010o", 1);
            Assert.AreEqual(s, "0000000001");

            s = Sprintf.sprintf("%-+010o", 1);
            Assert.AreEqual(s, "1         ");

            s = Sprintf.sprintf("% 010o", 1);
            Assert.AreEqual(s, "0000000001");

            s = Sprintf.sprintf("% 10o", 1);
            Assert.AreEqual(s, "         1");

            s = Sprintf.sprintf("%010.5o", 1);
            Assert.AreEqual(s, "     00001");

            s = Sprintf.sprintf("%+010.5o", 1);
            Assert.AreEqual(s, "     00001");

            s = Sprintf.sprintf("%+-010.5o", 1);
            Assert.AreEqual(s, "00001     ");

            s = Sprintf.sprintf("%+-'010.5o", 1);
            Assert.AreEqual(s, "00001     ");

            s = Sprintf.sprintf("%+-'010.5o", 1000000000);
            Assert.AreEqual(s, "7,346,545,000");

            s = Sprintf.sprintf("%+-'010.5ho", 32767);
            Assert.AreEqual(s, "77,777    ");

            s = Sprintf.sprintf("%+-'010.5hho", 127);
            Assert.AreEqual(s, "00177     ");

            s = Sprintf.sprintf("%+-'010.5lo", 1000000000);
            Assert.AreEqual(s, "7,346,545,000");

            s = Sprintf.sprintf("%+-'010.5llo", 1000000000);
            Assert.AreEqual(s, "7,346,545,000");

            s = Sprintf.sprintf("%+-'010.5zo", 1000000000);
            Assert.AreEqual(s, "7,346,545,000");

            s = Sprintf.sprintf("%+-'010.5jo", 1000000000);
            Assert.AreEqual(s, "7,346,545,000");

            s = Sprintf.sprintf("%+-'010.5to", 1000000000);
            Assert.AreEqual(s, "7,346,545,000");

            s = Sprintf.sprintf("%+-'010.5qo", 1000000000);
            Assert.AreEqual(s, "7,346,545,000");

            s = Sprintf.sprintf("%+-'010.5Io", 1000000000);
            Assert.AreEqual(s, "7,346,545,000");

            s = Sprintf.sprintf("%+-'010.5I32o", 1000000000);
            Assert.AreEqual(s, "7,346,545,000");

            s = Sprintf.sprintf("%+-'010.5I64o", 1000000000);
            Assert.AreEqual(s, "7,346,545,000");

            s = Sprintf.sprintf("%+-'020.5I64o", 1000000000);
            Assert.AreEqual(s, "7,346,545,000       ");

            s = Sprintf.sprintf("%+'020.5I64o", 1000000000);
            Assert.AreEqual(s, "       7,346,545,000");

            s = Sprintf.sprintf("%+-'020.0o", 0);
            Assert.AreEqual(s, "                    ");

            s = Sprintf.sprintf("%+'020.0o", 0);
            Assert.AreEqual(s, "                    ");

            s = Sprintf.sprintf("%+'0*.*o", 20, 0, 0);
            Assert.AreEqual(s, "                    ");

            s = Sprintf.sprintf("ab%+'0*.*ocd", 20, 0, 0);
            Assert.AreEqual(s, "ab                    cd");

            s = Sprintf.sprintf("\"%o\"\n", 0);
            Assert.AreEqual(s, "\"0\"\n");

            s = Sprintf.sprintf("\"%+0*o\"\n", 20, 0);
            Assert.AreEqual(s, "\"00000000000000000000\"\n");

            s = Sprintf.sprintf("%hho", 255);
            Assert.AreEqual(s, "377");

            s = Sprintf.sprintf("%hho", -128);
            Assert.AreEqual(s, "200");

            s = Sprintf.sprintf("%010hho", -128);
            Assert.AreEqual(s, "0000000200");

            s = Sprintf.sprintf("%0.10hho", -128);
            Assert.AreEqual(s, "0000000200");

            s = Sprintf.sprintf("%020.10hho", -128);
            Assert.AreEqual(s, "          0000000200");

            s = Sprintf.sprintf("%'#020.5ho", 32767);
            Assert.AreEqual(s, "             077,777");

            s = Sprintf.sprintf("%'#020.10ho", 32767);
            Assert.AreEqual(s, "          000077,777");

            s = Sprintf.sprintf("%'#ho", 32767);
            Assert.AreEqual(s, "077,777");

            s = Sprintf.sprintf("%#020.5ho", 32767);
            Assert.AreEqual(s, "              077777");

            s = Sprintf.sprintf("%#020.10ho", 32767);
            Assert.AreEqual(s, "          0000077777");

            s = Sprintf.sprintf("%#ho", 32767);
            Assert.AreEqual(s, "077777");
        }

        [Test]
        public void TestPercentx()
        {
            String s;
            s = Sprintf.sprintf("%x", 1);
            Assert.AreEqual(s, "1");

            s = Sprintf.sprintf("%0x", 1);
            Assert.AreEqual(s, "1");

            s = Sprintf.sprintf("%010x", 1);
            Assert.AreEqual(s, "0000000001");

            s = Sprintf.sprintf("%+010x", 1);
            Assert.AreEqual(s, "0000000001");

            s = Sprintf.sprintf("%-+010x", 1);
            Assert.AreEqual(s, "1         ");

            s = Sprintf.sprintf("% 010x", 1);
            Assert.AreEqual(s, "0000000001");

            s = Sprintf.sprintf("% 10x", 1);
            Assert.AreEqual(s, "         1");

            s = Sprintf.sprintf("%010.5x", 1);
            Assert.AreEqual(s, "     00001");

            s = Sprintf.sprintf("%+010.5x", 1);
            Assert.AreEqual(s, "     00001");

            s = Sprintf.sprintf("%+-010.5x", 1);
            Assert.AreEqual(s, "00001     ");

            s = Sprintf.sprintf("%+-'010.5x", 1);
            Assert.AreEqual(s, "00001     ");

            s = Sprintf.sprintf("%+-'010.5x", 1000000000);
            Assert.AreEqual(s, "3b,9ac,a00");

            s = Sprintf.sprintf("%+-'010.5hx", 32767);
            Assert.AreEqual(s, "7,fff     ");

            s = Sprintf.sprintf("%+-'010.5hhx", 127);
            Assert.AreEqual(s, "0007f     ");

            s = Sprintf.sprintf("%+-'010.5lx", 1000000000);
            Assert.AreEqual(s, "3b,9ac,a00");

            s = Sprintf.sprintf("%+-'010.5llx", 1000000000);
            Assert.AreEqual(s, "3b,9ac,a00");

            s = Sprintf.sprintf("%+-'010.5zx", 1000000000);
            Assert.AreEqual(s, "3b,9ac,a00");

            s = Sprintf.sprintf("%+-'010.5jx", 1000000000);
            Assert.AreEqual(s, "3b,9ac,a00");

            s = Sprintf.sprintf("%+-'010.5tx", 1000000000);
            Assert.AreEqual(s, "3b,9ac,a00");

            s = Sprintf.sprintf("%+-'010.5qx", 1000000000);
            Assert.AreEqual(s, "3b,9ac,a00");

            s = Sprintf.sprintf("%+-'010.5Ix", 1000000000);
            Assert.AreEqual(s, "3b,9ac,a00");

            s = Sprintf.sprintf("%+-'010.5I32x", 1000000000);
            Assert.AreEqual(s, "3b,9ac,a00");

            s = Sprintf.sprintf("%+-'010.5I64x", 1000000000);
            Assert.AreEqual(s, "3b,9ac,a00");

            s = Sprintf.sprintf("%+-'020.5I64x", 1000000000);
            Assert.AreEqual(s, "3b,9ac,a00          ");

            s = Sprintf.sprintf("%+'020.5I64x", 1000000000);
            Assert.AreEqual(s, "          3b,9ac,a00");

            s = Sprintf.sprintf("%+-'020.0x", 0);
            Assert.AreEqual(s, "                    ");

            s = Sprintf.sprintf("%+'020.0x", 0);
            Assert.AreEqual(s, "                    ");

            s = Sprintf.sprintf("%+'0*.*x", 20, 0, 0);
            Assert.AreEqual(s, "                    ");

            s = Sprintf.sprintf("ab%+'0*.*xcd", 20, 0, 0);
            Assert.AreEqual(s, "ab                    cd");

            s = Sprintf.sprintf("\"%x\"\n", 0);
            Assert.AreEqual(s, "\"0\"\n");

            s = Sprintf.sprintf("\"%+0*x\"\n", 20, 0);
            Assert.AreEqual(s, "\"00000000000000000000\"\n");

            s = Sprintf.sprintf("%hhx", 255);
            Assert.AreEqual(s, "ff");

            s = Sprintf.sprintf("%hhx", -128);
            Assert.AreEqual(s, "80");

            s = Sprintf.sprintf("%010hhx", -128);
            Assert.AreEqual(s, "0000000080");

            s = Sprintf.sprintf("%0.10hhx", -128);
            Assert.AreEqual(s, "0000000080");

            s = Sprintf.sprintf("%020.10hhx", -128);
            Assert.AreEqual(s, "          0000000080");

            s = Sprintf.sprintf("%'#020.5hx", 32767);
            Assert.AreEqual(s, "             0x7,fff");

            s = Sprintf.sprintf("%'#020.10hx", 32767);
            Assert.AreEqual(s, "        0x000007,fff");

            s = Sprintf.sprintf("%'#hx", 32767);
            Assert.AreEqual(s, "0x7,fff");

            s = Sprintf.sprintf("%#020.5hx", 32767);
            Assert.AreEqual(s, "             0x07fff");

            s = Sprintf.sprintf("%#020.10hx", 32767);
            Assert.AreEqual(s, "        0x0000007fff");

            s = Sprintf.sprintf("%#hx", 32767);
            Assert.AreEqual(s, "0x7fff");
        }

        [Test]
        public void TestPercentX()
        {
            String s;
            s = Sprintf.sprintf("%X", 1);
            Assert.AreEqual(s, "1");

            s = Sprintf.sprintf("%0X", 1);
            Assert.AreEqual(s, "1");

            s = Sprintf.sprintf("%010X", 1);
            Assert.AreEqual(s, "0000000001");

            s = Sprintf.sprintf("%+010X", 1);
            Assert.AreEqual(s, "0000000001");

            s = Sprintf.sprintf("%-+010X", 1);
            Assert.AreEqual(s, "1         ");

            s = Sprintf.sprintf("% 010X", 1);
            Assert.AreEqual(s, "0000000001");

            s = Sprintf.sprintf("% 10X", 1);
            Assert.AreEqual(s, "         1");

            s = Sprintf.sprintf("%010.5X", 1);
            Assert.AreEqual(s, "     00001");

            s = Sprintf.sprintf("%+010.5X", 1);
            Assert.AreEqual(s, "     00001");

            s = Sprintf.sprintf("%+-010.5X", 1);
            Assert.AreEqual(s, "00001     ");

            s = Sprintf.sprintf("%+-'010.5X", 1);
            Assert.AreEqual(s, "00001     ");

            s = Sprintf.sprintf("%+-'010.5X", 1000000000);
            Assert.AreEqual(s, "3B,9AC,A00");

            s = Sprintf.sprintf("%+-'010.5hX", 32767);
            Assert.AreEqual(s, "7,FFF     ");

            s = Sprintf.sprintf("%+-'010.5hhX", 127);
            Assert.AreEqual(s, "0007F     ");

            s = Sprintf.sprintf("%+-'010.5lX", 1000000000);
            Assert.AreEqual(s, "3B,9AC,A00");

            s = Sprintf.sprintf("%+-'010.5llX", 1000000000);
            Assert.AreEqual(s, "3B,9AC,A00");

            s = Sprintf.sprintf("%+-'010.5zX", 1000000000);
            Assert.AreEqual(s, "3B,9AC,A00");

            s = Sprintf.sprintf("%+-'010.5jX", 1000000000);
            Assert.AreEqual(s, "3B,9AC,A00");

            s = Sprintf.sprintf("%+-'010.5tX", 1000000000);
            Assert.AreEqual(s, "3B,9AC,A00");

            s = Sprintf.sprintf("%+-'010.5qX", 1000000000);
            Assert.AreEqual(s, "3B,9AC,A00");

            s = Sprintf.sprintf("%+-'010.5IX", 1000000000);
            Assert.AreEqual(s, "3B,9AC,A00");

            s = Sprintf.sprintf("%+-'010.5I32X", 1000000000);
            Assert.AreEqual(s, "3B,9AC,A00");

            s = Sprintf.sprintf("%+-'010.5I64X", 1000000000);
            Assert.AreEqual(s, "3B,9AC,A00");

            s = Sprintf.sprintf("%+-'020.5I64X", 1000000000);
            Assert.AreEqual(s, "3B,9AC,A00          ");

            s = Sprintf.sprintf("%+'020.5I64X", 1000000000);
            Assert.AreEqual(s, "          3B,9AC,A00");

            s = Sprintf.sprintf("%+-'020.0X", 0);
            Assert.AreEqual(s, "                    ");

            s = Sprintf.sprintf("%+'020.0X", 0);
            Assert.AreEqual(s, "                    ");

            s = Sprintf.sprintf("%+'0*.*X", 20, 0, 0);
            Assert.AreEqual(s, "                    ");

            s = Sprintf.sprintf("ab%+'0*.*Xcd", 20, 0, 0);
            Assert.AreEqual(s, "ab                    cd");

            s = Sprintf.sprintf("\"%X\"\n", 0);
            Assert.AreEqual(s, "\"0\"\n");

            s = Sprintf.sprintf("\"%+0*X\"\n", 20, 0);
            Assert.AreEqual(s, "\"00000000000000000000\"\n");

            s = Sprintf.sprintf("%hhX", 255);
            Assert.AreEqual(s, "FF");

            s = Sprintf.sprintf("%hhX", -128);
            Assert.AreEqual(s, "80");

            s = Sprintf.sprintf("%010hhX", -128);
            Assert.AreEqual(s, "0000000080");

            s = Sprintf.sprintf("%0.10hhX", -128);
            Assert.AreEqual(s, "0000000080");

            s = Sprintf.sprintf("%020.10hhX", -128);
            Assert.AreEqual(s, "          0000000080");

            s = Sprintf.sprintf("%'#020.5hX", 32767);
            Assert.AreEqual(s, "             0X7,FFF");

            s = Sprintf.sprintf("%'#020.10hX", 32767);
            Assert.AreEqual(s, "        0X000007,FFF");

            s = Sprintf.sprintf("%'#hX", 32767);
            Assert.AreEqual(s, "0X7,FFF");

            s = Sprintf.sprintf("%#020.5hX", 32767);
            Assert.AreEqual(s, "             0X07FFF");

            s = Sprintf.sprintf("%#020.10hX", 32767);
            Assert.AreEqual(s, "        0X0000007FFF");

            s = Sprintf.sprintf("%#hX", 32767);
            Assert.AreEqual(s, "0X7FFF");
        }

        [Test]
        public void TestPercentPercent()
        {
            String s = Sprintf.sprintf("%%");
            Assert.AreEqual(s, "%");
        }
    }
}
