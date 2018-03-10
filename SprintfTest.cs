﻿using System;
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
        }
    }
}