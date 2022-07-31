//
//  SprintfTest.cs
//
//  Author:
//       Roy Merkel <merkel-roy@comcast.net>
//
//  Copyright (c) 2018 Roy Merkel
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using sprintf;
using NUnit.Framework;
using System.Runtime.InteropServices;
using System.Globalization;

namespace testsprintf
{
    [TestFixture]
    public class SprintfTest
    {
		private sprintf.GetObjAddrInterface oldInter;
		private PercentPGetObjAddr mockInter;

        public SprintfTest()
        {
			oldInter = Sprintf.getAddrInter;
			mockInter = new PercentPGetObjAddr();
        }

		[SetUp]
		public void SetUp() 
		{
			Sprintf.getAddrInter = mockInter;
		}

		[TearDown]
		public void TearDown() 
		{
			Sprintf.getAddrInter = oldInter;
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

        [Test]
        public void TestPercentf()
        {
            String s = Sprintf.sprintf("%f", 0.1);
            Assert.AreEqual(s, "0.100000");

            s = Sprintf.sprintf("%.0f", 0.1);
            Assert.AreEqual(s, "0");

            s = Sprintf.sprintf("%.1f", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%.2f", 0.1);
            Assert.AreEqual(s, "0.10");

            s = Sprintf.sprintf("%0.0f", 0.1);
            Assert.AreEqual(s, "0");

            s = Sprintf.sprintf("%0.1f", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%0.2f", 0.1);
            Assert.AreEqual(s, "0.10");

            s = Sprintf.sprintf("%02.0f", 0.1);
            Assert.AreEqual(s, "00");

            s = Sprintf.sprintf("%02.1f", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%02.2f", 0.1);
            Assert.AreEqual(s, "0.10");

            s = Sprintf.sprintf("%03.0f", 0.1);
            Assert.AreEqual(s, "000");

            s = Sprintf.sprintf("%03.1f", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%03.2f", 0.1);
            Assert.AreEqual(s, "0.10");

            s = Sprintf.sprintf("%3.0f", 0.1);
            Assert.AreEqual(s, "  0");

            s = Sprintf.sprintf("%3.1f", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%3.2f", 0.1);
            Assert.AreEqual(s, "0.10");

            s = Sprintf.sprintf("% 3.0f", 0.1);
            Assert.AreEqual(s, "  0");

            s = Sprintf.sprintf("% 3.1f", 0.1);
            Assert.AreEqual(s, " 0.1");

            s = Sprintf.sprintf("% 3.2f", 0.1);
            Assert.AreEqual(s, " 0.10");

            s = Sprintf.sprintf("%+3.0f", 0.1);
            Assert.AreEqual(s, " +0");

            s = Sprintf.sprintf("%+3.1f", 0.1);
            Assert.AreEqual(s, "+0.1");

            s = Sprintf.sprintf("%+3.2f", 0.1);
            Assert.AreEqual(s, "+0.10");

            s = Sprintf.sprintf("%+10.0f", 22456.78912);
            Assert.AreEqual(s, "    +22457");

            s = Sprintf.sprintf("%+10.1f", 22456.78912);
            Assert.AreEqual(s, "  +22456.8");

            s = Sprintf.sprintf("%+10.2f", 22456.78912);
            Assert.AreEqual(s, " +22456.79");

            s = Sprintf.sprintf("% 10.0f", 22456.78912);
            Assert.AreEqual(s, "     22457");

            s = Sprintf.sprintf("% 10.1f", 22456.78912);
            Assert.AreEqual(s, "   22456.8");

            s = Sprintf.sprintf("% 10.2f", 22456.78912);
            Assert.AreEqual(s, "  22456.79");

            s = Sprintf.sprintf("% +10.0f", 22456.78912);
            Assert.AreEqual(s, "    +22457");

            s = Sprintf.sprintf("% +10.1f", 22456.78912);
            Assert.AreEqual(s, "  +22456.8");

            s = Sprintf.sprintf("% +10.2f", 22456.78912);
            Assert.AreEqual(s, " +22456.79");

            s = Sprintf.sprintf("%# +10.0f", 22456.78912);
            Assert.AreEqual(s, "   +22457.");

            s = Sprintf.sprintf("%# +10.1f", 22456.78912);
            Assert.AreEqual(s, "  +22456.8");

            s = Sprintf.sprintf("%# +10.2f", 22456.78912);
            Assert.AreEqual(s, " +22456.79");

            s = Sprintf.sprintf("%'# +10.0f", 22456.78912);
            Assert.AreEqual(s, "  +22,457.");

            s = Sprintf.sprintf("%'# +10.1f", 22456.78912);
            Assert.AreEqual(s, " +22,456.8");

            s = Sprintf.sprintf("%'# +10.2f", 22456.78912);
            Assert.AreEqual(s, "+22,456.79");

            s = Sprintf.sprintf("%-'# +10.0f", 22456.78912);
            Assert.AreEqual(s, "+22,457.  ");

            s = Sprintf.sprintf("%-'# +10.1f", 22456.78912);
            Assert.AreEqual(s, "+22,456.8 ");

            s = Sprintf.sprintf("%-'# +10.2f", 22456.78912);
            Assert.AreEqual(s, "+22,456.79");

            s = Sprintf.sprintf("%-'#010.2f", 22456000000000000000000.78912);
            //Assert.AreEqual(s, "22,456,000,000,000,002,097,152.00");
            Assert.AreEqual(s, "22,456,000,000,000,000,000,000.00");

            s = Sprintf.sprintf("%-'#010.2f", double.NegativeInfinity);
            Assert.AreEqual(s, "-inf      ");

            s = Sprintf.sprintf("%-'#010.2f", double.PositiveInfinity);
            Assert.AreEqual(s, "inf       ");

            s = Sprintf.sprintf("%-'#010.2f", double.NaN);
            Assert.AreEqual(s, "nan       ");
        }

        [Test]
        public void TestPercentF()
        {
            String s = Sprintf.sprintf("%F", 0.1);
            Assert.AreEqual(s, "0.100000");

            s = Sprintf.sprintf("%.0F", 0.1);
            Assert.AreEqual(s, "0");

            s = Sprintf.sprintf("%.1F", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%.2F", 0.1);
            Assert.AreEqual(s, "0.10");

            s = Sprintf.sprintf("%0.0F", 0.1);
            Assert.AreEqual(s, "0");

            s = Sprintf.sprintf("%0.1F", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%0.2F", 0.1);
            Assert.AreEqual(s, "0.10");

            s = Sprintf.sprintf("%02.0F", 0.1);
            Assert.AreEqual(s, "00");

            s = Sprintf.sprintf("%02.1F", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%02.2F", 0.1);
            Assert.AreEqual(s, "0.10");

            s = Sprintf.sprintf("%03.0F", 0.1);
            Assert.AreEqual(s, "000");

            s = Sprintf.sprintf("%03.1F", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%03.2F", 0.1);
            Assert.AreEqual(s, "0.10");

            s = Sprintf.sprintf("%3.0F", 0.1);
            Assert.AreEqual(s, "  0");

            s = Sprintf.sprintf("%3.1F", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%3.2F", 0.1);
            Assert.AreEqual(s, "0.10");

            s = Sprintf.sprintf("% 3.0F", 0.1);
            Assert.AreEqual(s, "  0");

            s = Sprintf.sprintf("% 3.1F", 0.1);
            Assert.AreEqual(s, " 0.1");

            s = Sprintf.sprintf("% 3.2F", 0.1);
            Assert.AreEqual(s, " 0.10");

            s = Sprintf.sprintf("%+3.0F", 0.1);
            Assert.AreEqual(s, " +0");

            s = Sprintf.sprintf("%+3.1F", 0.1);
            Assert.AreEqual(s, "+0.1");

            s = Sprintf.sprintf("%+3.2F", 0.1);
            Assert.AreEqual(s, "+0.10");

            s = Sprintf.sprintf("%+10.0F", 22456.78912);
            Assert.AreEqual(s, "    +22457");

            s = Sprintf.sprintf("%+10.1F", 22456.78912);
            Assert.AreEqual(s, "  +22456.8");

            s = Sprintf.sprintf("%+10.2F", 22456.78912);
            Assert.AreEqual(s, " +22456.79");

            s = Sprintf.sprintf("% 10.0F", 22456.78912);
            Assert.AreEqual(s, "     22457");

            s = Sprintf.sprintf("% 10.1F", 22456.78912);
            Assert.AreEqual(s, "   22456.8");

            s = Sprintf.sprintf("% 10.2F", 22456.78912);
            Assert.AreEqual(s, "  22456.79");

            s = Sprintf.sprintf("% +10.0F", 22456.78912);
            Assert.AreEqual(s, "    +22457");

            s = Sprintf.sprintf("% +10.1F", 22456.78912);
            Assert.AreEqual(s, "  +22456.8");

            s = Sprintf.sprintf("% +10.2F", 22456.78912);
            Assert.AreEqual(s, " +22456.79");

            s = Sprintf.sprintf("%# +10.0F", 22456.78912);
            Assert.AreEqual(s, "   +22457.");

            s = Sprintf.sprintf("%# +10.1F", 22456.78912);
            Assert.AreEqual(s, "  +22456.8");

            s = Sprintf.sprintf("%# +10.2F", 22456.78912);
            Assert.AreEqual(s, " +22456.79");

            s = Sprintf.sprintf("%'# +10.0F", 22456.78912);
            Assert.AreEqual(s, "  +22,457.");

            s = Sprintf.sprintf("%'# +10.1F", 22456.78912);
            Assert.AreEqual(s, " +22,456.8");

            s = Sprintf.sprintf("%'# +10.2F", 22456.78912);
            Assert.AreEqual(s, "+22,456.79");

            s = Sprintf.sprintf("%-'# +10.0F", 22456.78912);
            Assert.AreEqual(s, "+22,457.  ");

            s = Sprintf.sprintf("%-'# +10.1F", 22456.78912);
            Assert.AreEqual(s, "+22,456.8 ");

            s = Sprintf.sprintf("%-'# +10.2F", 22456.78912);
            Assert.AreEqual(s, "+22,456.79");

            s = Sprintf.sprintf("%-'#010.2F", 22456000000000000000000.78912);
            //Assert.AreEqual(s, "22,456,000,000,000,002,097,152.00");
            Assert.AreEqual(s, "22,456,000,000,000,000,000,000.00");

            s = Sprintf.sprintf("%-'#010.2F", double.NegativeInfinity);
            Assert.AreEqual(s, "-INF      ");

            s = Sprintf.sprintf("%-'#010.2F", double.PositiveInfinity);
            Assert.AreEqual(s, "INF       ");

            s = Sprintf.sprintf("%-'#010.2F", double.NaN);
            Assert.AreEqual(s, "NAN       ");
        }

        [Test]
        public void TestPercente()
        {
            String s = Sprintf.sprintf("%e", 0.1);
            Assert.AreEqual(s, "1.000000e-01");

            s = Sprintf.sprintf("%.0e", 0.1);
            Assert.AreEqual(s, "1e-01");

            s = Sprintf.sprintf("%.1e", 0.1);
            Assert.AreEqual(s, "1.0e-01");

            s = Sprintf.sprintf("%.2e", 0.1);
            Assert.AreEqual(s, "1.00e-01");

            s = Sprintf.sprintf("%0.0e", 0.1);
            Assert.AreEqual(s, "1e-01");

            s = Sprintf.sprintf("%0.1e", 0.1);
            Assert.AreEqual(s, "1.0e-01");

            s = Sprintf.sprintf("%0.2e", 0.1);
            Assert.AreEqual(s, "1.00e-01");

            s = Sprintf.sprintf("%02.0e", 0.1);
            Assert.AreEqual(s, "1e-01");

            s = Sprintf.sprintf("%02.1e", 0.1);
            Assert.AreEqual(s, "1.0e-01");

            s = Sprintf.sprintf("%02.2e", 0.1);
            Assert.AreEqual(s, "1.00e-01");

            s = Sprintf.sprintf("%03.0e", 0.1);
            Assert.AreEqual(s, "1e-01");

            s = Sprintf.sprintf("%03.1e", 0.1);
            Assert.AreEqual(s, "1.0e-01");

            s = Sprintf.sprintf("%03.2e", 0.1);
            Assert.AreEqual(s, "1.00e-01");

            s = Sprintf.sprintf("%3.0e", 0.1);
            Assert.AreEqual(s, "1e-01");

            s = Sprintf.sprintf("%3.1e", 0.1);
            Assert.AreEqual(s, "1.0e-01");

            s = Sprintf.sprintf("%3.2e", 0.1);
            Assert.AreEqual(s, "1.00e-01");

            s = Sprintf.sprintf("% 3.0e", 0.1);
            Assert.AreEqual(s, " 1e-01");

            s = Sprintf.sprintf("% 3.1e", 0.1);
            Assert.AreEqual(s, " 1.0e-01");

            s = Sprintf.sprintf("% 3.2e", 0.1);
            Assert.AreEqual(s, " 1.00e-01");

            s = Sprintf.sprintf("%+3.0e", 0.1);
            Assert.AreEqual(s, "+1e-01");

            s = Sprintf.sprintf("%+3.1e", 0.1);
            Assert.AreEqual(s, "+1.0e-01");

            s = Sprintf.sprintf("%+3.2e", 0.1);
            Assert.AreEqual(s, "+1.00e-01");

            s = Sprintf.sprintf("%+10.0e", 22456.78912);
            Assert.AreEqual(s, "    +2e+04");

            s = Sprintf.sprintf("%+10.1e", 22456.78912);
            Assert.AreEqual(s, "  +2.2e+04");

            s = Sprintf.sprintf("%+10.2e", 22456.78912);
            Assert.AreEqual(s, " +2.25e+04");

            s = Sprintf.sprintf("% 10.0e", 22456.78912);
            Assert.AreEqual(s, "     2e+04");

            s = Sprintf.sprintf("% 10.1e", 22456.78912);
            Assert.AreEqual(s, "   2.2e+04");

            s = Sprintf.sprintf("% 10.2e", 22456.78912);
            Assert.AreEqual(s, "  2.25e+04");

            s = Sprintf.sprintf("% +10.0e", 22456.78912);
            Assert.AreEqual(s, "    +2e+04");

            s = Sprintf.sprintf("% +10.1e", 22456.78912);
            Assert.AreEqual(s, "  +2.2e+04");

            s = Sprintf.sprintf("% +10.2e", 22456.78912);
            Assert.AreEqual(s, " +2.25e+04");

            s = Sprintf.sprintf("%# +10.0e", 22456.78912);
            Assert.AreEqual(s, "   +2.e+04");

            s = Sprintf.sprintf("%# +10.1e", 22456.78912);
            Assert.AreEqual(s, "  +2.2e+04");

            s = Sprintf.sprintf("%# +10.2e", 22456.78912);
            Assert.AreEqual(s, " +2.25e+04");

            s = Sprintf.sprintf("%'# +10.0e", 22456.78912);
            Assert.AreEqual(s, "   +2.e+04");

            s = Sprintf.sprintf("%'# +10.1e", 22456.78912);
            Assert.AreEqual(s, "  +2.2e+04");

            s = Sprintf.sprintf("%'# +10.2e", 22456.78912);
            Assert.AreEqual(s, " +2.25e+04");

            s = Sprintf.sprintf("%-'# +10.0e", 22456.78912);
            Assert.AreEqual(s, "+2.e+04   ");

            s = Sprintf.sprintf("%-'# +10.1e", 22456.78912);
            Assert.AreEqual(s, "+2.2e+04  ");

            s = Sprintf.sprintf("%-'# +10.2e", 22456.78912);
            Assert.AreEqual(s, "+2.25e+04 ");

            s = Sprintf.sprintf("%-'#010.2e", 22456000000000000000000.78912);
            Assert.AreEqual(s, "2.25e+22  ");

            s = Sprintf.sprintf("%-'#010.2e", double.NegativeInfinity);
            Assert.AreEqual(s, "-inf      ");

            s = Sprintf.sprintf("%-'#010.2e", double.PositiveInfinity);
            Assert.AreEqual(s, "inf       ");

            s = Sprintf.sprintf("%-'#010.2e", double.NaN);
            Assert.AreEqual(s, "nan       ");
        }

        [Test]
        public void TestPercentE()
        {
            String s = Sprintf.sprintf("%E", 0.1);
            Assert.AreEqual(s, "1.000000E-01");

            s = Sprintf.sprintf("%.0E", 0.1);
            Assert.AreEqual(s, "1E-01");

            s = Sprintf.sprintf("%.1E", 0.1);
            Assert.AreEqual(s, "1.0E-01");

            s = Sprintf.sprintf("%.2E", 0.1);
            Assert.AreEqual(s, "1.00E-01");

            s = Sprintf.sprintf("%0.0E", 0.1);
            Assert.AreEqual(s, "1E-01");

            s = Sprintf.sprintf("%0.1E", 0.1);
            Assert.AreEqual(s, "1.0E-01");

            s = Sprintf.sprintf("%0.2E", 0.1);
            Assert.AreEqual(s, "1.00E-01");

            s = Sprintf.sprintf("%02.0E", 0.1);
            Assert.AreEqual(s, "1E-01");

            s = Sprintf.sprintf("%02.1E", 0.1);
            Assert.AreEqual(s, "1.0E-01");

            s = Sprintf.sprintf("%02.2E", 0.1);
            Assert.AreEqual(s, "1.00E-01");

            s = Sprintf.sprintf("%03.0E", 0.1);
            Assert.AreEqual(s, "1E-01");

            s = Sprintf.sprintf("%03.1E", 0.1);
            Assert.AreEqual(s, "1.0E-01");

            s = Sprintf.sprintf("%03.2E", 0.1);
            Assert.AreEqual(s, "1.00E-01");

            s = Sprintf.sprintf("%3.0E", 0.1);
            Assert.AreEqual(s, "1E-01");

            s = Sprintf.sprintf("%3.1E", 0.1);
            Assert.AreEqual(s, "1.0E-01");

            s = Sprintf.sprintf("%3.2E", 0.1);
            Assert.AreEqual(s, "1.00E-01");

            s = Sprintf.sprintf("% 3.0E", 0.1);
            Assert.AreEqual(s, " 1E-01");

            s = Sprintf.sprintf("% 3.1E", 0.1);
            Assert.AreEqual(s, " 1.0E-01");

            s = Sprintf.sprintf("% 3.2E", 0.1);
            Assert.AreEqual(s, " 1.00E-01");

            s = Sprintf.sprintf("%+3.0E", 0.1);
            Assert.AreEqual(s, "+1E-01");

            s = Sprintf.sprintf("%+3.1E", 0.1);
            Assert.AreEqual(s, "+1.0E-01");

            s = Sprintf.sprintf("%+3.2E", 0.1);
            Assert.AreEqual(s, "+1.00E-01");

            s = Sprintf.sprintf("%+10.0E", 22456.78912);
            Assert.AreEqual(s, "    +2E+04");

            s = Sprintf.sprintf("%+10.1E", 22456.78912);
            Assert.AreEqual(s, "  +2.2E+04");

            s = Sprintf.sprintf("%+10.2E", 22456.78912);
            Assert.AreEqual(s, " +2.25E+04");

            s = Sprintf.sprintf("% 10.0E", 22456.78912);
            Assert.AreEqual(s, "     2E+04");

            s = Sprintf.sprintf("% 10.1E", 22456.78912);
            Assert.AreEqual(s, "   2.2E+04");

            s = Sprintf.sprintf("% 10.2E", 22456.78912);
            Assert.AreEqual(s, "  2.25E+04");

            s = Sprintf.sprintf("% +10.0E", 22456.78912);
            Assert.AreEqual(s, "    +2E+04");

            s = Sprintf.sprintf("% +10.1E", 22456.78912);
            Assert.AreEqual(s, "  +2.2E+04");

            s = Sprintf.sprintf("% +10.2E", 22456.78912);
            Assert.AreEqual(s, " +2.25E+04");

            s = Sprintf.sprintf("%# +10.0E", 22456.78912);
            Assert.AreEqual(s, "   +2.E+04");

            s = Sprintf.sprintf("%# +10.1E", 22456.78912);
            Assert.AreEqual(s, "  +2.2E+04");

            s = Sprintf.sprintf("%# +10.2E", 22456.78912);
            Assert.AreEqual(s, " +2.25E+04");

            s = Sprintf.sprintf("%'# +10.0E", 22456.78912);
            Assert.AreEqual(s, "   +2.E+04");

            s = Sprintf.sprintf("%'# +10.1E", 22456.78912);
            Assert.AreEqual(s, "  +2.2E+04");

            s = Sprintf.sprintf("%'# +10.2E", 22456.78912);
            Assert.AreEqual(s, " +2.25E+04");

            s = Sprintf.sprintf("%-'# +10.0E", 22456.78912);
            Assert.AreEqual(s, "+2.E+04   ");

            s = Sprintf.sprintf("%-'# +10.1E", 22456.78912);
            Assert.AreEqual(s, "+2.2E+04  ");

            s = Sprintf.sprintf("%-'# +10.2E", 22456.78912);
            Assert.AreEqual(s, "+2.25E+04 ");

            s = Sprintf.sprintf("%-'#010.2E", 22456000000000000000000.78912);
            Assert.AreEqual(s, "2.25E+22  ");

            s = Sprintf.sprintf("%-'#010.2E", double.NegativeInfinity);
            Assert.AreEqual(s, "-INF      ");

            s = Sprintf.sprintf("%-'#010.2E", double.PositiveInfinity);
            Assert.AreEqual(s, "INF       ");

            s = Sprintf.sprintf("%-'#010.2E", double.NaN);
            Assert.AreEqual(s, "NAN       ");
        }


        [Test]
        public void TestPercentg()
        {
            String s = Sprintf.sprintf("%g", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%.0g", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%.1g", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%.2g", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%0.0g", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%0.1g", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%0.2g", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%02.0g", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%02.1g", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%02.2g", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%03.0g", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%03.1g", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%03.2g", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%3.0g", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%3.1g", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%3.2g", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("% 3.0g", 0.1);
            Assert.AreEqual(s, " 0.1");

            s = Sprintf.sprintf("% 3.1g", 0.1);
            Assert.AreEqual(s, " 0.1");

            s = Sprintf.sprintf("% 3.2g", 0.1);
            Assert.AreEqual(s, " 0.1");

            s = Sprintf.sprintf("%+3.0g", 0.1);
            Assert.AreEqual(s, "+0.1");

            s = Sprintf.sprintf("%+3.1g", 0.1);
            Assert.AreEqual(s, "+0.1");

            s = Sprintf.sprintf("%+3.2g", 0.1);
            Assert.AreEqual(s, "+0.1");

            s = Sprintf.sprintf("%+10.0g", 22456.78912);
            Assert.AreEqual(s, "    +2e+04");

            s = Sprintf.sprintf("%+10.1g", 22456.78912);
            Assert.AreEqual(s, "    +2e+04");

            s = Sprintf.sprintf("%+10.2g", 22456.78912);
            Assert.AreEqual(s, "  +2.2e+04");

            s = Sprintf.sprintf("% 10.0g", 22456.78912);
            Assert.AreEqual(s, "     2e+04");

            s = Sprintf.sprintf("% 10.1g", 22456.78912);
            Assert.AreEqual(s, "     2e+04");

            s = Sprintf.sprintf("% 10.2g", 22456.78912);
            Assert.AreEqual(s, "   2.2e+04");

            s = Sprintf.sprintf("% +10.0g", 22456.78912);
            Assert.AreEqual(s, "    +2e+04");

            s = Sprintf.sprintf("% +10.1g", 22456.78912);
            Assert.AreEqual(s, "    +2e+04");

            s = Sprintf.sprintf("% +10.2g", 22456.78912);
            Assert.AreEqual(s, "  +2.2e+04");

            s = Sprintf.sprintf("%# +10.0g", 22456.78912);
            Assert.AreEqual(s, "   +2.e+04");

            s = Sprintf.sprintf("%# +10.1g", 22456.78912);
            Assert.AreEqual(s, "   +2.e+04");

            s = Sprintf.sprintf("%# +10.2g", 22456.78912);
            Assert.AreEqual(s, "  +2.2e+04");

            s = Sprintf.sprintf("%'# +10.0g", 22456.78912);
            Assert.AreEqual(s, "   +2.e+04");

            s = Sprintf.sprintf("%'# +10.1g", 22456.78912);
            Assert.AreEqual(s, "   +2.e+04");

            s = Sprintf.sprintf("%'# +10.2g", 22456.78912);
            Assert.AreEqual(s, "  +2.2e+04");

            s = Sprintf.sprintf("%-'# +10.0g", 22456.78912);
            Assert.AreEqual(s, "+2.e+04   ");

            s = Sprintf.sprintf("%-'# +10.1g", 22456.78912);
            Assert.AreEqual(s, "+2.e+04   ");

            s = Sprintf.sprintf("%-'# +10.2g", 22456.78912);
            Assert.AreEqual(s, "+2.2e+04  ");

            s = Sprintf.sprintf("%-'#010.2g", 22456000000000000000000.78912);
            Assert.AreEqual(s, "2.2e+22   ");

            s = Sprintf.sprintf("%-'#010.2g", double.NegativeInfinity);
            Assert.AreEqual(s, "-inf      ");

            s = Sprintf.sprintf("%-'#010.2g", double.PositiveInfinity);
            Assert.AreEqual(s, "inf       ");

            s = Sprintf.sprintf("%-'#010.2g", double.NaN);
            Assert.AreEqual(s, "nan       ");

            s = Sprintf.sprintf("%-'#0.30g", 22456000000000000000000.78912);
            Assert.AreEqual(s, "22,456,000,000,000,002,000,000.");
        }

        [Test]
        public void TestPercentG()
        {
            String s = Sprintf.sprintf("%G", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%.0G", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%.1G", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%.2G", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%0.0G", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%0.1G", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%0.2G", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%02.0G", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%02.1G", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%02.2G", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%03.0G", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%03.1G", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%03.2G", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%3.0G", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%3.1G", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("%3.2G", 0.1);
            Assert.AreEqual(s, "0.1");

            s = Sprintf.sprintf("% 3.0G", 0.1);
            Assert.AreEqual(s, " 0.1");

            s = Sprintf.sprintf("% 3.1G", 0.1);
            Assert.AreEqual(s, " 0.1");

            s = Sprintf.sprintf("% 3.2G", 0.1);
            Assert.AreEqual(s, " 0.1");

            s = Sprintf.sprintf("%+3.0G", 0.1);
            Assert.AreEqual(s, "+0.1");

            s = Sprintf.sprintf("%+3.1G", 0.1);
            Assert.AreEqual(s, "+0.1");

            s = Sprintf.sprintf("%+3.2G", 0.1);
            Assert.AreEqual(s, "+0.1");

            s = Sprintf.sprintf("%+10.0G", 22456.78912);
            Assert.AreEqual(s, "    +2E+04");

            s = Sprintf.sprintf("%+10.1G", 22456.78912);
            Assert.AreEqual(s, "    +2E+04");

            s = Sprintf.sprintf("%+10.2G", 22456.78912);
            Assert.AreEqual(s, "  +2.2E+04");

            s = Sprintf.sprintf("% 10.0G", 22456.78912);
            Assert.AreEqual(s, "     2E+04");

            s = Sprintf.sprintf("% 10.1G", 22456.78912);
            Assert.AreEqual(s, "     2E+04");

            s = Sprintf.sprintf("% 10.2G", 22456.78912);
            Assert.AreEqual(s, "   2.2E+04");

            s = Sprintf.sprintf("% +10.0G", 22456.78912);
            Assert.AreEqual(s, "    +2E+04");

            s = Sprintf.sprintf("% +10.1G", 22456.78912);
            Assert.AreEqual(s, "    +2E+04");

            s = Sprintf.sprintf("% +10.2G", 22456.78912);
            Assert.AreEqual(s, "  +2.2E+04");

            s = Sprintf.sprintf("%# +10.0G", 22456.78912);
            Assert.AreEqual(s, "   +2.E+04");

            s = Sprintf.sprintf("%# +10.1G", 22456.78912);
            Assert.AreEqual(s, "   +2.E+04");

            s = Sprintf.sprintf("%# +10.2G", 22456.78912);
            Assert.AreEqual(s, "  +2.2E+04");

            s = Sprintf.sprintf("%'# +10.0G", 22456.78912);
            Assert.AreEqual(s, "   +2.E+04");

            s = Sprintf.sprintf("%'# +10.1G", 22456.78912);
            Assert.AreEqual(s, "   +2.E+04");

            s = Sprintf.sprintf("%'# +10.2G", 22456.78912);
            Assert.AreEqual(s, "  +2.2E+04");

            s = Sprintf.sprintf("%-'# +10.0G", 22456.78912);
            Assert.AreEqual(s, "+2.E+04   ");

            s = Sprintf.sprintf("%-'# +10.1G", 22456.78912);
            Assert.AreEqual(s, "+2.E+04   ");

            s = Sprintf.sprintf("%-'# +10.2G", 22456.78912);
            Assert.AreEqual(s, "+2.2E+04  ");

            s = Sprintf.sprintf("%-'#010.2G", 22456000000000000000000.78912);
            Assert.AreEqual(s, "2.2E+22   ");

            s = Sprintf.sprintf("%-'#010.2G", double.NegativeInfinity);
            Assert.AreEqual(s, "-INF      ");

            s = Sprintf.sprintf("%-'#010.2G", double.PositiveInfinity);
            Assert.AreEqual(s, "INF       ");

            s = Sprintf.sprintf("%-'#010.2G", double.NaN);
            Assert.AreEqual(s, "NAN       ");

            s = Sprintf.sprintf("%-'#0.30G", 22456000000000000000000.78912);
            Assert.AreEqual(s, "22,456,000,000,000,002,000,000.");
        }

        [Test]
        public void TestPercenta()
        {
            String s = Sprintf.sprintf("%a", 0.1);
            Assert.AreEqual(s, "0x1.999999999999ap-4");

            s = Sprintf.sprintf("%.0a", 0.1);
            Assert.AreEqual(s, "0x2p-4");

            s = Sprintf.sprintf("%.1a", 0.1);
            Assert.AreEqual(s, "0x1.ap-4");

            s = Sprintf.sprintf("%.2a", 0.1);
            Assert.AreEqual(s, "0x1.9ap-4");

            s = Sprintf.sprintf("%0.0a", 0.1);
            Assert.AreEqual(s, "0x2p-4");

            s = Sprintf.sprintf("%0.1a", 0.1);
            Assert.AreEqual(s, "0x1.ap-4");

            s = Sprintf.sprintf("%0.2a", 0.1);
            Assert.AreEqual(s, "0x1.9ap-4");

            s = Sprintf.sprintf("%02.0a", 0.1);
            Assert.AreEqual(s, "0x2p-4");

            s = Sprintf.sprintf("%02.1a", 0.1);
            Assert.AreEqual(s, "0x1.ap-4");

            s = Sprintf.sprintf("%02.2a", 0.1);
            Assert.AreEqual(s, "0x1.9ap-4");

            s = Sprintf.sprintf("%03.0a", 0.1);
            Assert.AreEqual(s, "0x2p-4");

            s = Sprintf.sprintf("%03.1a", 0.1);
            Assert.AreEqual(s, "0x1.ap-4");

            s = Sprintf.sprintf("%03.2a", 0.1);
            Assert.AreEqual(s, "0x1.9ap-4");

            s = Sprintf.sprintf("%3.0a", 0.1);
            Assert.AreEqual(s, "0x2p-4");

            s = Sprintf.sprintf("%3.1a", 0.1);
            Assert.AreEqual(s, "0x1.ap-4");

            s = Sprintf.sprintf("%3.2a", 0.1);
            Assert.AreEqual(s, "0x1.9ap-4");

            s = Sprintf.sprintf("% 3.0a", 0.1);
            Assert.AreEqual(s, " 0x2p-4");

            s = Sprintf.sprintf("% 3.1a", 0.1);
            Assert.AreEqual(s, " 0x1.ap-4");

            s = Sprintf.sprintf("% 3.2a", 0.1);
            Assert.AreEqual(s, " 0x1.9ap-4");

            s = Sprintf.sprintf("%+3.0a", 0.1);
            Assert.AreEqual(s, "+0x2p-4");

            s = Sprintf.sprintf("%+3.1a", 0.1);
            Assert.AreEqual(s, "+0x1.ap-4");

            s = Sprintf.sprintf("%+3.2a", 0.1);
            Assert.AreEqual(s, "+0x1.9ap-4");

            s = Sprintf.sprintf("%+10.0a", 22456.78912);
            Assert.AreEqual(s, "  +0x1p+14");

            s = Sprintf.sprintf("%+10.1a", 22456.78912);
            Assert.AreEqual(s, "+0x1.6p+14");

            s = Sprintf.sprintf("%+10.2a", 22456.78912);
            Assert.AreEqual(s, "+0x1.5fp+14");

            s = Sprintf.sprintf("% 10.0a", 22456.78912);
            Assert.AreEqual(s, "   0x1p+14");

            s = Sprintf.sprintf("% 10.1a", 22456.78912);
            Assert.AreEqual(s, " 0x1.6p+14");

            s = Sprintf.sprintf("% 10.2a", 22456.78912);
            Assert.AreEqual(s, " 0x1.5fp+14");

            s = Sprintf.sprintf("% +10.0a", 22456.78912);
            Assert.AreEqual(s, "  +0x1p+14");

            s = Sprintf.sprintf("% +10.1a", 22456.78912);
            Assert.AreEqual(s, "+0x1.6p+14");

            s = Sprintf.sprintf("% +10.2a", 22456.78912);
            Assert.AreEqual(s, "+0x1.5fp+14");

            s = Sprintf.sprintf("%# +10.0a", 22456.78912);
            Assert.AreEqual(s, " +0x1.p+14");

            s = Sprintf.sprintf("%# +10.1a", 22456.78912);
            Assert.AreEqual(s, "+0x1.6p+14");

            s = Sprintf.sprintf("%# +10.2a", 22456.78912);
            Assert.AreEqual(s, "+0x1.5fp+14");

            s = Sprintf.sprintf("%'# +10.0a", 22456.78912);
            Assert.AreEqual(s, " +0x1.p+14");

            s = Sprintf.sprintf("%'# +10.1a", 22456.78912);
            Assert.AreEqual(s, "+0x1.6p+14");

            s = Sprintf.sprintf("%'# +10.2a", 22456.78912);
            Assert.AreEqual(s, "+0x1.5fp+14");

            s = Sprintf.sprintf("%-'# +10.0a", 22456.78912);
            Assert.AreEqual(s, "+0x1.p+14 ");

            s = Sprintf.sprintf("%-'# +10.1a", 22456.78912);
            Assert.AreEqual(s, "+0x1.6p+14");

            s = Sprintf.sprintf("%-'# +10.2a", 22456.78912);
            Assert.AreEqual(s, "+0x1.5fp+14");

            s = Sprintf.sprintf("%-'#010.2a", 22456000000000000000000.78912);
            Assert.AreEqual(s, "0x1.30p+74");

            s = Sprintf.sprintf("%-'#010.2a", double.NegativeInfinity);
            Assert.AreEqual(s, "-inf      ");

            s = Sprintf.sprintf("%-'#010.2a", double.PositiveInfinity);
            Assert.AreEqual(s, "inf       ");

            s = Sprintf.sprintf("%-'#010.2a", double.NaN);
            Assert.AreEqual(s, "nan       ");

            s = Sprintf.sprintf("%-'#0.30a", 22456000000000000000000.78912);
            Assert.AreEqual(s, "0x1.3055e697c0d3000000000000000000p+74");

            s = Sprintf.sprintf("%-'#a", 1.0);
            Assert.AreEqual(s, "0x1.p+0");

            s = Sprintf.sprintf("%-'#0.0a", 1.0);
            Assert.AreEqual(s, "0x1.p+0");

            s = Sprintf.sprintf("%-'#0.1a", 1.0);
            Assert.AreEqual(s, "0x1.0p+0");

            s = Sprintf.sprintf("%-'#0.2a", 1.0);
            Assert.AreEqual(s, "0x1.00p+0");

            s = Sprintf.sprintf("%-'#a", 2.0);
            Assert.AreEqual(s, "0x1.p+1");

            s = Sprintf.sprintf("%-'#0.0a", 2.0);
            Assert.AreEqual(s, "0x1.p+1");

            s = Sprintf.sprintf("%-'#0.1a", 2.0);
            Assert.AreEqual(s, "0x1.0p+1");

            s = Sprintf.sprintf("%-'#0.2a", 2.0);
            Assert.AreEqual(s, "0x1.00p+1");

            s = Sprintf.sprintf("%-'#a", 5.0);
            Assert.AreEqual(s, "0x1.4p+2");

            s = Sprintf.sprintf("%-'#0.0a", 5.0);
            Assert.AreEqual(s, "0x1.p+2");

            s = Sprintf.sprintf("%-'#0.1a", 5.0);
            Assert.AreEqual(s, "0x1.4p+2");

            s = Sprintf.sprintf("%-'#0.2a", 5.0);
            Assert.AreEqual(s, "0x1.40p+2");

            s = Sprintf.sprintf("%-'#a", 255.0);
            Assert.AreEqual(s, "0x1.fep+7");

            s = Sprintf.sprintf("%-'#0.0a", 255.0);
            Assert.AreEqual(s, "0x2.p+7");

            s = Sprintf.sprintf("%-'#0.1a", 255.0);
            Assert.AreEqual(s, "0x2.0p+7");

            s = Sprintf.sprintf("%-'#0.2a", 255.0);
            Assert.AreEqual(s, "0x1.fep+7");

            s = Sprintf.sprintf("%-'#a", 0.0);
            Assert.AreEqual(s, "0x0.p+0");

            s = Sprintf.sprintf("%-'#0.0a", 0.0);
            Assert.AreEqual(s, "0x0.p+0");

            s = Sprintf.sprintf("%-'#0.1a", 0.0);
            Assert.AreEqual(s, "0x0.0p+0");

            s = Sprintf.sprintf("%-'#0.2a", 0.0);
            Assert.AreEqual(s, "0x0.00p+0");

            s = Sprintf.sprintf("%-'a", 0.0);
            Assert.AreEqual(s, "0x0p+0");

            s = Sprintf.sprintf("%-'0.0a", 0.0);
            Assert.AreEqual(s, "0x0p+0");

            s = Sprintf.sprintf("%-'0.1a", 0.0);
            Assert.AreEqual(s, "0x0.0p+0");

            s = Sprintf.sprintf("%-'0.2a", 0.0);
            Assert.AreEqual(s, "0x0.00p+0");

            s = Sprintf.sprintf("%-'a", 1.0);
            Assert.AreEqual(s, "0x1p+0");

            s = Sprintf.sprintf("%-'0.0a", 1.0);
            Assert.AreEqual(s, "0x1p+0");

            s = Sprintf.sprintf("%-'0.1a", 1.0);
            Assert.AreEqual(s, "0x1.0p+0");

            s = Sprintf.sprintf("%-'0.2a", 1.0);
            Assert.AreEqual(s, "0x1.00p+0");
        }

        [Test]
        public void TestPercentA()
        {
            String s = Sprintf.sprintf("%A", 0.1);
            Assert.AreEqual(s, "0X1.999999999999AP-4");

            s = Sprintf.sprintf("%.0A", 0.1);
            Assert.AreEqual(s, "0X2P-4");

            s = Sprintf.sprintf("%.1A", 0.1);
            Assert.AreEqual(s, "0X1.AP-4");

            s = Sprintf.sprintf("%.2A", 0.1);
            Assert.AreEqual(s, "0X1.9AP-4");

            s = Sprintf.sprintf("%0.0A", 0.1);
            Assert.AreEqual(s, "0X2P-4");

            s = Sprintf.sprintf("%0.1A", 0.1);
            Assert.AreEqual(s, "0X1.AP-4");

            s = Sprintf.sprintf("%0.2A", 0.1);
            Assert.AreEqual(s, "0X1.9AP-4");

            s = Sprintf.sprintf("%02.0A", 0.1);
            Assert.AreEqual(s, "0X2P-4");

            s = Sprintf.sprintf("%02.1A", 0.1);
            Assert.AreEqual(s, "0X1.AP-4");

            s = Sprintf.sprintf("%02.2A", 0.1);
            Assert.AreEqual(s, "0X1.9AP-4");

            s = Sprintf.sprintf("%03.0A", 0.1);
            Assert.AreEqual(s, "0X2P-4");

            s = Sprintf.sprintf("%03.1A", 0.1);
            Assert.AreEqual(s, "0X1.AP-4");

            s = Sprintf.sprintf("%03.2A", 0.1);
            Assert.AreEqual(s, "0X1.9AP-4");

            s = Sprintf.sprintf("%3.0A", 0.1);
            Assert.AreEqual(s, "0X2P-4");

            s = Sprintf.sprintf("%3.1A", 0.1);
            Assert.AreEqual(s, "0X1.AP-4");

            s = Sprintf.sprintf("%3.2A", 0.1);
            Assert.AreEqual(s, "0X1.9AP-4");

            s = Sprintf.sprintf("% 3.0A", 0.1);
            Assert.AreEqual(s, " 0X2P-4");

            s = Sprintf.sprintf("% 3.1A", 0.1);
            Assert.AreEqual(s, " 0X1.AP-4");

            s = Sprintf.sprintf("% 3.2A", 0.1);
            Assert.AreEqual(s, " 0X1.9AP-4");

            s = Sprintf.sprintf("%+3.0A", 0.1);
            Assert.AreEqual(s, "+0X2P-4");

            s = Sprintf.sprintf("%+3.1A", 0.1);
            Assert.AreEqual(s, "+0X1.AP-4");

            s = Sprintf.sprintf("%+3.2A", 0.1);
            Assert.AreEqual(s, "+0X1.9AP-4");

            s = Sprintf.sprintf("%+10.0A", 22456.78912);
            Assert.AreEqual(s, "  +0X1P+14");

            s = Sprintf.sprintf("%+10.1A", 22456.78912);
            Assert.AreEqual(s, "+0X1.6P+14");

            s = Sprintf.sprintf("%+10.2A", 22456.78912);
            Assert.AreEqual(s, "+0X1.5FP+14");

            s = Sprintf.sprintf("% 10.0A", 22456.78912);
            Assert.AreEqual(s, "   0X1P+14");

            s = Sprintf.sprintf("% 10.1A", 22456.78912);
            Assert.AreEqual(s, " 0X1.6P+14");

            s = Sprintf.sprintf("% 10.2A", 22456.78912);
            Assert.AreEqual(s, " 0X1.5FP+14");

            s = Sprintf.sprintf("% +10.0A", 22456.78912);
            Assert.AreEqual(s, "  +0X1P+14");

            s = Sprintf.sprintf("% +10.1A", 22456.78912);
            Assert.AreEqual(s, "+0X1.6P+14");

            s = Sprintf.sprintf("% +10.2A", 22456.78912);
            Assert.AreEqual(s, "+0X1.5FP+14");

            s = Sprintf.sprintf("%# +10.0A", 22456.78912);
            Assert.AreEqual(s, " +0X1.P+14");

            s = Sprintf.sprintf("%# +10.1A", 22456.78912);
            Assert.AreEqual(s, "+0X1.6P+14");

            s = Sprintf.sprintf("%# +10.2A", 22456.78912);
            Assert.AreEqual(s, "+0X1.5FP+14");

            s = Sprintf.sprintf("%'# +10.0A", 22456.78912);
            Assert.AreEqual(s, " +0X1.P+14");

            s = Sprintf.sprintf("%'# +10.1A", 22456.78912);
            Assert.AreEqual(s, "+0X1.6P+14");

            s = Sprintf.sprintf("%'# +10.2A", 22456.78912);
            Assert.AreEqual(s, "+0X1.5FP+14");

            s = Sprintf.sprintf("%-'# +10.0A", 22456.78912);
            Assert.AreEqual(s, "+0X1.P+14 ");

            s = Sprintf.sprintf("%-'# +10.1A", 22456.78912);
            Assert.AreEqual(s, "+0X1.6P+14");

            s = Sprintf.sprintf("%-'# +10.2A", 22456.78912);
            Assert.AreEqual(s, "+0X1.5FP+14");

            s = Sprintf.sprintf("%-'#010.2A", 22456000000000000000000.78912);
            Assert.AreEqual(s, "0X1.30P+74");

            s = Sprintf.sprintf("%-'#010.2A", double.NegativeInfinity);
            Assert.AreEqual(s, "-INF      ");

            s = Sprintf.sprintf("%-'#010.2A", double.PositiveInfinity);
            Assert.AreEqual(s, "INF       ");

            s = Sprintf.sprintf("%-'#010.2A", double.NaN);
            Assert.AreEqual(s, "NAN       ");

            s = Sprintf.sprintf("%-'#0.30A", 22456000000000000000000.78912);
            Assert.AreEqual(s, "0X1.3055E697C0D3000000000000000000P+74");

            s = Sprintf.sprintf("%-'#A", 1.0);
            Assert.AreEqual(s, "0X1.P+0");

            s = Sprintf.sprintf("%-'#0.0A", 1.0);
            Assert.AreEqual(s, "0X1.P+0");

            s = Sprintf.sprintf("%-'#0.1A", 1.0);
            Assert.AreEqual(s, "0X1.0P+0");

            s = Sprintf.sprintf("%-'#0.2A", 1.0);
            Assert.AreEqual(s, "0X1.00P+0");

            s = Sprintf.sprintf("%-'#A", 2.0);
            Assert.AreEqual(s, "0X1.P+1");

            s = Sprintf.sprintf("%-'#0.0A", 2.0);
            Assert.AreEqual(s, "0X1.P+1");

            s = Sprintf.sprintf("%-'#0.1A", 2.0);
            Assert.AreEqual(s, "0X1.0P+1");

            s = Sprintf.sprintf("%-'#0.2A", 2.0);
            Assert.AreEqual(s, "0X1.00P+1");

            s = Sprintf.sprintf("%-'#A", 5.0);
            Assert.AreEqual(s, "0X1.4P+2");

            s = Sprintf.sprintf("%-'#0.0A", 5.0);
            Assert.AreEqual(s, "0X1.P+2");

            s = Sprintf.sprintf("%-'#0.1A", 5.0);
            Assert.AreEqual(s, "0X1.4P+2");

            s = Sprintf.sprintf("%-'#0.2A", 5.0);
            Assert.AreEqual(s, "0X1.40P+2");

            s = Sprintf.sprintf("%-'#A", 255.0);
            Assert.AreEqual(s, "0X1.FEP+7");

            s = Sprintf.sprintf("%-'#0.0A", 255.0);
            Assert.AreEqual(s, "0X2.P+7");

            s = Sprintf.sprintf("%-'#0.1A", 255.0);
            Assert.AreEqual(s, "0X2.0P+7");

            s = Sprintf.sprintf("%-'#0.2A", 255.0);
            Assert.AreEqual(s, "0X1.FEP+7");

            s = Sprintf.sprintf("%-'#A", 0.0);
            Assert.AreEqual(s, "0X0.P+0");

            s = Sprintf.sprintf("%-'#0.0A", 0.0);
            Assert.AreEqual(s, "0X0.P+0");

            s = Sprintf.sprintf("%-'#0.1A", 0.0);
            Assert.AreEqual(s, "0X0.0P+0");

            s = Sprintf.sprintf("%-'#0.2A", 0.0);
            Assert.AreEqual(s, "0X0.00P+0");

            s = Sprintf.sprintf("%-'A", 0.0);
            Assert.AreEqual(s, "0X0P+0");

            s = Sprintf.sprintf("%-'0.0A", 0.0);
            Assert.AreEqual(s, "0X0P+0");

            s = Sprintf.sprintf("%-'0.1A", 0.0);
            Assert.AreEqual(s, "0X0.0P+0");

            s = Sprintf.sprintf("%-'0.2A", 0.0);
            Assert.AreEqual(s, "0X0.00P+0");

            s = Sprintf.sprintf("%-'A", 1.0);
            Assert.AreEqual(s, "0X1P+0");

            s = Sprintf.sprintf("%-'0.0A", 1.0);
            Assert.AreEqual(s, "0X1P+0");

            s = Sprintf.sprintf("%-'0.1A", 1.0);
            Assert.AreEqual(s, "0X1.0P+0");

            s = Sprintf.sprintf("%-'0.2A", 1.0);
            Assert.AreEqual(s, "0X1.00P+0");
        }

        [Test]
        public void TestPercents()
        {
            String s = Sprintf.sprintf("%s", "abc123");
            Assert.AreEqual(s, "abc123");

            s = Sprintf.sprintf("%010s", "abc123");
            Assert.AreEqual(s, "    abc123");

            s = Sprintf.sprintf("%010.6s", "abc123");
            Assert.AreEqual(s, "    abc123");

            s = Sprintf.sprintf("%-010s", "abc123");
            Assert.AreEqual(s, "abc123    ");

            s = Sprintf.sprintf("%-010.6s", "abc123");
            Assert.AreEqual(s, "abc123    ");

            s = Sprintf.sprintf("%-#010s", "abc123");
            Assert.AreEqual(s, "abc123    ");

            s = Sprintf.sprintf("%-#010.6s", "abc123");
            Assert.AreEqual(s, "abc123    ");

            s = Sprintf.sprintf("%-#'010s", "abc123");
            Assert.AreEqual(s, "abc123    ");

            s = Sprintf.sprintf("%-#'010.6s", "abc123");
            Assert.AreEqual(s, "abc123    ");

            s = Sprintf.sprintf("%-#'010s", "abc123");
            Assert.AreEqual(s, "abc123    ");

            s = Sprintf.sprintf("%-#'010.3s", "abc123");
            Assert.AreEqual(s, "abc       ");

            s = Sprintf.sprintf("%-#'010.0s", "abc123");
            Assert.AreEqual(s, "          ");
        }

        [Test]
        public void TestPercentS()
        {
            String s = Sprintf.sprintf("%S", "abc123");
            Assert.AreEqual(s, "abc123");

            s = Sprintf.sprintf("%010S", "abc123");
            Assert.AreEqual(s, "    abc123");

            s = Sprintf.sprintf("%010.6S", "abc123");
            Assert.AreEqual(s, "    abc123");

            s = Sprintf.sprintf("%-010S", "abc123");
            Assert.AreEqual(s, "abc123    ");

            s = Sprintf.sprintf("%-010.6S", "abc123");
            Assert.AreEqual(s, "abc123    ");

            s = Sprintf.sprintf("%-#010S", "abc123");
            Assert.AreEqual(s, "abc123    ");

            s = Sprintf.sprintf("%-#010.6S", "abc123");
            Assert.AreEqual(s, "abc123    ");

            s = Sprintf.sprintf("%-#'010S", "abc123");
            Assert.AreEqual(s, "abc123    ");

            s = Sprintf.sprintf("%-#'010.6S", "abc123");
            Assert.AreEqual(s, "abc123    ");

            s = Sprintf.sprintf("%-#'010S", "abc123");
            Assert.AreEqual(s, "abc123    ");

            s = Sprintf.sprintf("%-#'010.3S", "abc123");
            Assert.AreEqual(s, "abc       ");

            s = Sprintf.sprintf("%-#'010.0S", "abc123");
            Assert.AreEqual(s, "          ");
        }

        [Test]
        public void TestPercentc()
        {
            String s = Sprintf.sprintf("%c", 'a');
            Assert.AreEqual(s, "a");

            s = Sprintf.sprintf("%0.0c", 'a');
            Assert.AreEqual(s, "a");

            s = Sprintf.sprintf("%01.0c", 'a');
            Assert.AreEqual(s, "a");

            s = Sprintf.sprintf("%02.0c", 'a');
            Assert.AreEqual(s, " a");

            s = Sprintf.sprintf("%02.0c", '\0');
            Assert.AreEqual(s, " ");

            s = Sprintf.sprintf("%02.0c", '\n');
            Assert.AreEqual(s, " \n");

            s = Sprintf.sprintf("%-02.0c", 'a');
            Assert.AreEqual(s, "a ");

            s = Sprintf.sprintf("%-#02.0c", 'a');
            Assert.AreEqual(s, "a ");

            s = Sprintf.sprintf("%-#'02.0c", 'a');
            Assert.AreEqual(s, "a ");
        }

        [Test]
        public void TestPercentC()
        {
            String s = Sprintf.sprintf("%C", 'a');
            Assert.AreEqual(s, "a");

            s = Sprintf.sprintf("%0.0C", 'a');
            Assert.AreEqual(s, "a");

            s = Sprintf.sprintf("%01.0C", 'a');
            Assert.AreEqual(s, "a");

            s = Sprintf.sprintf("%02.0C", 'a');
            Assert.AreEqual(s, " a");

            s = Sprintf.sprintf("%02.0C", '\0');
            Assert.AreEqual(s, " ");

            s = Sprintf.sprintf("%02.0C", '\n');
            Assert.AreEqual(s, " \n");

            s = Sprintf.sprintf("%-02.0C", 'a');
            Assert.AreEqual(s, "a ");

            s = Sprintf.sprintf("%-#02.0C", 'a');
            Assert.AreEqual(s, "a ");

            s = Sprintf.sprintf("%-#'02.0C", 'a');
            Assert.AreEqual(s, "a ");
        }

		public class PercentPGetObjAddr : sprintf.GetObjAddrInterface
		{
			public long retVal = 0;

			public PercentPGetObjAddr()
			{
			}

			public long getObjAddr(object arg)
			{
				return retVal;
			}
		}

        [Test]
        public void TestPercentP()
        {
            String s;

			mockInter.retVal = 1;
            s = Sprintf.sprintf("%p", 1);
            Assert.AreEqual(s, "0x1");

            s = Sprintf.sprintf("%0p", 1);
            Assert.AreEqual(s, "0x1");

            s = Sprintf.sprintf("%010p", 1);
            Assert.AreEqual(s, "0x00000001");

            s = Sprintf.sprintf("%+010p", 1);
            Assert.AreEqual(s, "+0x0000001");

            s = Sprintf.sprintf("%-+010p", 1);
            Assert.AreEqual(s, "+0x1      ");

            s = Sprintf.sprintf("% 010p", 1);
            Assert.AreEqual(s, " 0x0000001");

            s = Sprintf.sprintf("% 10p", 1);
            Assert.AreEqual(s, "       0x1");

            s = Sprintf.sprintf("%010.5p", 1);
            Assert.AreEqual(s, "   0x00001");

            s = Sprintf.sprintf("%+010.5p", 1);
            Assert.AreEqual(s, "  +0x00001");

            s = Sprintf.sprintf("%+-010.5p", 1);
            Assert.AreEqual(s, "+0x00001  ");

            s = Sprintf.sprintf("%+-'010.5p", 1);
            Assert.AreEqual(s, "+0x00001  ");

			mockInter.retVal = 1000000000;
            s = Sprintf.sprintf("%+-'010.5p", 1000000000);
            Assert.AreEqual(s, "+0x3b9aca00");

			mockInter.retVal = 32767;
            s = Sprintf.sprintf("%+-'010.5hp", 32767);
            Assert.AreEqual(s, "+0x07fff  ");

			mockInter.retVal = 127;
            s = Sprintf.sprintf("%+-'010.5hhp", 127);
            Assert.AreEqual(s, "+0x0007f  ");

			mockInter.retVal = 1000000000;
            s = Sprintf.sprintf("%+-'010.5lp", 1000000000);
            Assert.AreEqual(s, "+0x3b9aca00");

            s = Sprintf.sprintf("%+-'010.5llp", 1000000000);
            Assert.AreEqual(s, "+0x3b9aca00");

            s = Sprintf.sprintf("%+-'010.5zp", 1000000000);
            Assert.AreEqual(s, "+0x3b9aca00");

            s = Sprintf.sprintf("%+-'010.5jp", 1000000000);
            Assert.AreEqual(s, "+0x3b9aca00");

            s = Sprintf.sprintf("%+-'010.5tp", 1000000000);
            Assert.AreEqual(s, "+0x3b9aca00");

            s = Sprintf.sprintf("%+-'010.5qp", 1000000000);
            Assert.AreEqual(s, "+0x3b9aca00");

            s = Sprintf.sprintf("%+-'010.5Ip", 1000000000);
            Assert.AreEqual(s, "+0x3b9aca00");

            s = Sprintf.sprintf("%+-'010.5I32p", 1000000000);
            Assert.AreEqual(s, "+0x3b9aca00");

            s = Sprintf.sprintf("%+-'010.5I64p", 1000000000);
            Assert.AreEqual(s, "+0x3b9aca00");

            s = Sprintf.sprintf("%+-'020.5I64p", 1000000000);
            Assert.AreEqual(s, "+0x3b9aca00         ");

            s = Sprintf.sprintf("%+'020.5I64p", 1000000000);
            Assert.AreEqual(s, "         +0x3b9aca00");

			mockInter.retVal = 0;
            s = Sprintf.sprintf("%+-'020.0p", 0);
            Assert.AreEqual(s, "(nil)               ");

            s = Sprintf.sprintf("%+'020.0p", 0);
            Assert.AreEqual(s, "               (nil)");

            s = Sprintf.sprintf("%+'0*.*p", 20, 0, 0);
            Assert.AreEqual(s, "               (nil)");

            s = Sprintf.sprintf("ab%+'0*.*pcd", 20, 0, 0);
            Assert.AreEqual(s, "ab               (nil)cd");

            s = Sprintf.sprintf("\"%p\"\n", 0);
            Assert.AreEqual(s, "\"(nil)\"\n");

            s = Sprintf.sprintf("\"%+0*p\"\n", 20, 0);
            Assert.AreEqual(s, "\"               (nil)\"\n");

			mockInter.retVal = 255;
            s = Sprintf.sprintf("%hhp", 255);
            Assert.AreEqual(s, "0xff");

			mockInter.retVal = -128;
            s = Sprintf.sprintf("%hhp", -128);
            Assert.AreEqual(s, "0xffffff80");

            s = Sprintf.sprintf("%010hhp", -128);
            Assert.AreEqual(s, "0xffffff80");

            s = Sprintf.sprintf("%0.10hhp", -128);
            Assert.AreEqual(s, "0x00ffffff80");

            s = Sprintf.sprintf("%020.10hhp", -128);
            Assert.AreEqual(s, "        0x00ffffff80");

			mockInter.retVal = 32767;
            s = Sprintf.sprintf("%'#020.5hp", 32767);
            Assert.AreEqual(s, "             0x07fff");

            s = Sprintf.sprintf("%'#020.10hp", 32767);
            Assert.AreEqual(s, "        0x0000007fff");

            s = Sprintf.sprintf("%'#hp", 32767);
            Assert.AreEqual(s, "0x7fff");

            s = Sprintf.sprintf("%#020.5hp", 32767);
            Assert.AreEqual(s, "             0x07fff");

            s = Sprintf.sprintf("%#020.10hp", 32767);
            Assert.AreEqual(s, "        0x0000007fff");

            s = Sprintf.sprintf("%#hp", 32767);
            Assert.AreEqual(s, "0x7fff");
        }

		struct Test
		{
			public int a;
			public int b;
			public int c;

			public Test(int a, int b, int c)
			{
				this.a = 0xBADF;
				this.b = 0xDFBA;
				this.c = 0xBABA;
			}
		}

		class Test2
		{
			public int a;
			public int b;
			public int c;

			public Test2(int a, int b, int c)
			{
				this.a = 0xFBAD;
				this.b = 0xADFB;
				this.c = 0xABAB;
			}
		}

		[Test]
		public unsafe void TestPercentPAddress()
		{
			Test obj1 = new Test(1, 2, 3);
			Test2 obj2 = new Test2(1, 2, 3);
			String s;
			IntPtr addr;
			int *objData;

			Sprintf.getAddrInter = oldInter;
			s = Sprintf.sprintf("%p", obj1);
			addr = new IntPtr(int.Parse(s.Substring(2), NumberStyles.HexNumber));
			objData = (int *)addr;

			Assert.AreEqual(objData[0], obj1.a);
            Assert.AreEqual(objData[1], obj1.b);
            Assert.AreEqual(objData[2], obj1.c);

			s = Sprintf.sprintf("%p", obj2);
			addr = new IntPtr(int.Parse(s.Substring(2), NumberStyles.HexNumber));
			objData = (int *)addr;

			Assert.AreEqual(objData[0], obj2.a);
			Assert.AreEqual(objData[1], obj2.b);
			Assert.AreEqual(objData[2], obj2.c);

		}

        [Test]
        public void TestPercentChain()
        {
            String s = Sprintf.sprintf("%-+#'0*.*f%d\n", 20, 5, 1.0, 2500000);
            Assert.AreEqual("+1.00000            2500000\n", s);
        }

        [Test]
        public void TestPositionalParams()
        {
            String s = Sprintf.sprintf("%5$04x\n", 1, 2, 3, 4, 5);
            Assert.AreEqual("0005\n", s);

            s = Sprintf.sprintf("%05$4x\n", 1, 2, 3, 4, 5);
            Assert.AreEqual("   5\n", s);

            s = Sprintf.sprintf("%04x\n", 1, 2, 3, 4, 5);
            Assert.AreEqual("0001\n", s);

            s = Sprintf.sprintf("%0*x\n", 1, 2, 3, 4, 5);
            Assert.AreEqual("2\n", s);

            s = Sprintf.sprintf("%5$04x %x\n", 1, 2, 3, 4, 5);
            Assert.AreEqual("0005 1\n", s);

            s = Sprintf.sprintf("%05$04x %x\n", 1, 2, 3, 4, 5);
            Assert.AreEqual("0005 1\n", s);

            s = Sprintf.sprintf("%9$04x %x\n", 1, 2, 3, 4, 5, 6, 7, 8, 9);
            Assert.AreEqual("0009 1\n", s);

            s = Sprintf.sprintf("%09$04x %x\n", 1, 2, 3, 4, 5, 6, 7, 8, 9);
            Assert.AreEqual("0009 1\n", s);

            s = Sprintf.sprintf("%0x09$04x %x\n", 1, 2, 3, 4, 5, 6, 7, 8, 9);
            Assert.AreEqual("109$04x 2\n", s);

            s = Sprintf.sprintf("%0A$04x %x\n", 1, 2, 3, 4, 5, 6, 7, 8, 9);
            Assert.AreEqual("0X1P+0$04x 2\n", s);

            s = Sprintf.sprintf("%10$04x %x\n", 1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
            Assert.AreEqual("000a 1\n", s);

            s = Sprintf.sprintf("%10$04x %x\n", 1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
            Assert.AreEqual("000a 1\n", s);

            s = Sprintf.sprintf("%0$s\n", "a", "b");
            Assert.IsNull(s);

            s = Sprintf.sprintf("%7$#x\n", 1, 2, 3, 4, 5, 6, 7);
            Assert.AreEqual("0x7\n", s);

            s = Sprintf.sprintf("%7$6x\n", 1, 2, 3, 4, 5, 6, 7);
            Assert.AreEqual("     7\n", s);

            s = Sprintf.sprintf("%7$06x\n", 1, 2, 3, 4, 5, 6, 7);
            Assert.AreEqual("000007\n", s);

            s = Sprintf.sprintf("%7$06.05x\n", 1, 2, 3, 4, 5, 6, 7);
            Assert.AreEqual(" 00007\n", s);
        }

        [Test]
        public void TestPositionalWidth()
        {
            String s = Sprintf.sprintf("%*0x\n", 5, 4);
            Assert.IsNull(s);

            s = Sprintf.sprintf("%0*x\n", 5, 4);
            Assert.AreEqual("00004\n", s);

            s = Sprintf.sprintf("%*0$s\n", "a", "b");
            Assert.IsNull(s);

            s = Sprintf.sprintf("%0*4$x\n", 2, 4, 6, 8, 10);
            Assert.AreEqual("00000002\n", s);

            s = Sprintf.sprintf("%*4$0x\n", 2, 4, 6, 8, 10);
            Assert.IsNull(s);
        }

        [Test]
        public void TestPositionalPrecision()
        {
            String s = Sprintf.sprintf("%.*0x\n", 5, 4);
            Assert.IsNull(s);

            s = Sprintf.sprintf("%.*5$x\n", 1, 2, 3, 4, 5);
            Assert.AreEqual("00001\n", s);
        }

        [Test]
        public void TestPositionalPrecisionAndWidth()
        {
            String s = Sprintf.sprintf("%*7$.*5$x\n", 1, 2, 3, 4, 5, 6, 7);
            Assert.AreEqual("  00001\n", s);
        }

        [Test]
        public void TestPositionalParamAndPrecision()
        {
            String s = Sprintf.sprintf("%7$.*5$x\n", 1, 2, 3, 4, 5, 6, 7);
            Assert.AreEqual("00007\n", s);
        }

        [Test]
        public void TestPositionalParamAndWidth()
        {
            String s = Sprintf.sprintf("%7$*5$x\n", 1, 2, 3, 4, 5, 6, 7);
            Assert.AreEqual("    7\n", s);
        }

        [Test]
        public void TestPositionalTrio()
        {
            String s = Sprintf.sprintf("%7$*6$.*5$x\n", 1, 2, 3, 4, 5, 6, 7);
            Assert.AreEqual(" 00007\n", s);
        }
    }
}
