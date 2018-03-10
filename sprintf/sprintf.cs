using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections;

namespace sprintf
{
    public static class Sprintf
    {
        [StructLayout(LayoutKind.Explicit)]
        struct ByteConv
        {
            [FieldOffset(0)] public SByte Int8;
            [FieldOffset(0)] public Byte UInt8;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct Int16Conv
        {
            [FieldOffset(0)] public Int16 Int16;
            [FieldOffset(0)] public UInt16 UInt16;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct Int32Conv
        {
            [FieldOffset(0)] public Int32 Int32;
            [FieldOffset(0)] public UInt32 UInt32;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct Int64Conv
        {
            [FieldOffset(0)] public Int64 Int64;
            [FieldOffset(0)] public UInt64 UInt64;
        }

        private static ByteConv byteConv = new ByteConv();
        private static Int16Conv i16Conv = new Int16Conv();
        private static Int32Conv i32Conv = new Int32Conv();
        private static Int64Conv i64Conv = new Int64Conv();
        private readonly static CultureInfo currentCulture = CultureInfo.CurrentCulture;
        private readonly static NumberFormatInfo currentCultureNumberFormat = currentCulture.NumberFormat;

        private static bool Is64BitProcess()
        {
            return Marshal.SizeOf(typeof(IntPtr)) == 8;
        }

        public static void Frexp(double d, out bool negative, out long mantissa, out int exponent)
        {

            // Translate the double into sign, exponent and mantissa.
            long bits = BitConverter.DoubleToInt64Bits(d);
            // Note that the shift is sign-extended, hence the test against -1 not 1
            negative = (bits < 0);
            exponent = (int)((bits >> 52) & 0x7ffL);
            mantissa = bits & 0xfffffffffffffL;

            if (mantissa == 0 && exponent == 0)
            {
                return;
            }
            else if (exponent == 0x7ff && mantissa == 0)
            {
                return;
            }
            else if (exponent == 0x7ff && mantissa != 0)
            {
                return;
            }

            exponent -= 1023;
        }

        private static void AddFloatPeriod(ref StringBuilder val)
        {
            bool found = false;
            int i = 0;
            int len = val.Length;

            for (i = 0; i < len; i++)
            {
                if (val[i] == 'e' || val[i] == 'E')
                {
                    found = true;
                    val.Insert(i, '.');
                    break;
                }
                else if (val[i] == '.')
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                val.Append('.');
            }
        }

        private static void GroupHexOctal(ref StringBuilder val, NumberFormatInfo nf)
        {
            string numberGroupSeparator = nf.NumberGroupSeparator;

            int lastn = -1;
            int substrlength = val.Length;
            foreach (int n in nf.NumberGroupSizes)
            {
                lastn = n;

                if (substrlength - n <= 0)
                {
                    break;
                }

                val.Insert(substrlength - n, numberGroupSeparator);
                substrlength -= n;
            }

            while (lastn > 0 && substrlength > lastn)
            {
                if (substrlength - lastn <= 0)
                {
                    break;
                }

                val.Insert(substrlength - lastn, numberGroupSeparator);
                substrlength -= lastn;
            }
        }

        private static void Capitalize(ref StringBuilder str)
        {
            int len = str.Length;

            for (int i = 0; i < len; i++)
            {
                str[i] = char.ToUpper(str[i]);
            }
        }

        private static StringBuilder SprintfParseFloatArg(bool groupDigits, bool scientificNotation, bool decimalFloatingPoint, object arg, string length, bool hasPrec, int prec)
        {
            StringBuilder valStr;
            float f;
            double d;

            switch (length)
            {
                case "L":
                    /* TODO: find a c# long double type, one doesn't seem to exist... */
                    try
                    {
                        f = Convert.ToSingle(arg);
                        d = (double)f;
                    }
                    catch (Exception)
                    {
                        try
                        {
                            d = Convert.ToDouble(arg);
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    }

                    if (scientificNotation && decimalFloatingPoint)
                    {
                        if (groupDigits)
                        {
                            if (hasPrec)
                            {
                                valStr = new StringBuilder(d.ToString("G" + (prec + 1).ToString(), Sprintf.currentCultureNumberFormat));
                            }
                            else
                            {
                                valStr = new StringBuilder(d.ToString("G", Sprintf.currentCultureNumberFormat));
                            }
                        }
                        else
                        {
                            if (hasPrec)
                            {
                                valStr = new StringBuilder(d.ToString("G" + (prec + 1).ToString()));
                            }
                            else
                            {
                                valStr = new StringBuilder(d.ToString("G"));
                            }
                        }
                    }
                    else if (scientificNotation)
                    {
                        if (groupDigits)
                        {
                            if (hasPrec)
                            {
                                valStr = new StringBuilder(d.ToString("E" + prec.ToString(), Sprintf.currentCultureNumberFormat));
                            }
                            else
                            {
                                valStr = new StringBuilder(d.ToString("E", Sprintf.currentCultureNumberFormat));
                            }
                        }
                        else
                        {
                            if (hasPrec)
                            {
                                valStr = new StringBuilder(d.ToString("E" + prec.ToString()));
                            }
                            else
                            {
                                valStr = new StringBuilder(d.ToString("E"));
                            }
                        }
                    }
                    else if (decimalFloatingPoint)
                    {
                        if (groupDigits)
                        {
                            if (hasPrec)
                            {
                                valStr = new StringBuilder(d.ToString("F" + prec.ToString(), Sprintf.currentCultureNumberFormat));
                            }
                            else
                            {
                                valStr = new StringBuilder(d.ToString("F", Sprintf.currentCultureNumberFormat));
                            }
                        }
                        else
                        {
                            if (hasPrec)
                            {
                                valStr = new StringBuilder(d.ToString("F" + prec.ToString()));
                            }
                            else
                            {
                                valStr = new StringBuilder(d.ToString("F"));
                            }
                        }
                    }
                    else
                    {
                        return null;
                    }

                    break;
                case "":
                    try
                    {
                        f = Convert.ToSingle(arg);
                        d = (double)f;
                    }
                    catch (Exception)
                    {
                        try
                        {
                            d = Convert.ToDouble(arg);
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    }

                    if (scientificNotation && decimalFloatingPoint)
                    {
                        if (groupDigits)
                        {
                            if (hasPrec)
                            {
                                valStr = new StringBuilder(d.ToString("G" + (prec + 1).ToString(), Sprintf.currentCultureNumberFormat));
                            }
                            else
                            {
                                valStr = new StringBuilder(d.ToString("G", Sprintf.currentCultureNumberFormat));
                            }
                        }
                        else
                        {
                            if (hasPrec)
                            {
                                valStr = new StringBuilder(d.ToString("G" + (prec + 1).ToString()));
                            }
                            else
                            {
                                valStr = new StringBuilder(d.ToString("G"));
                            }
                        }
                    }
                    else if (scientificNotation)
                    {
                        if (groupDigits)
                        {
                            if (hasPrec)
                            {
                                valStr = new StringBuilder(d.ToString("E" + prec.ToString(), Sprintf.currentCultureNumberFormat));
                            }
                            else
                            {
                                valStr = new StringBuilder(d.ToString("E", Sprintf.currentCultureNumberFormat));
                            }
                        }
                        else
                        {
                            if (hasPrec)
                            {
                                valStr = new StringBuilder(d.ToString("E" + prec.ToString()));
                            }
                            else
                            {
                                valStr = new StringBuilder(d.ToString("E"));
                            }
                        }
                    }
                    else if (decimalFloatingPoint)
                    {
                        if (groupDigits)
                        {
                            if (hasPrec)
                            {
                                valStr = new StringBuilder(d.ToString("F" + prec.ToString(), Sprintf.currentCultureNumberFormat));
                            }
                            else
                            {
                                valStr = new StringBuilder(d.ToString("F", Sprintf.currentCultureNumberFormat));
                            }
                        }
                        else
                        {
                            if (hasPrec)
                            {
                                valStr = new StringBuilder(d.ToString("F" + prec.ToString()));
                            }
                            else
                            {
                                valStr = new StringBuilder(d.ToString("F"));
                            }
                        }
                    }
                    else
                    {
                        return null;
                    }

                    break;
                default:
                    return null;
            }

            return valStr;
        }

        private static StringBuilder sprintfParseIntArg(bool groupDigits, bool unsigned, bool hex, bool octal, object arg, string length)
        {
            StringBuilder valStr;
            switch (length)
            {
                case "hh":
                    byte uchval;
                    sbyte chval;

                    try
                    {
                        uchval = Convert.ToByte(arg);

                        byteConv.UInt8 = uchval;

                        chval = byteConv.Int8;
                    }
                    catch (Exception)
                    {
                        try
                        {
                            chval = Convert.ToSByte(arg);

                            byteConv.Int8 = chval;

                            uchval = byteConv.UInt8;
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    }

                    if (hex)
                    {
                        valStr = new StringBuilder(Convert.ToString(uchval, 16));

                        if (groupDigits)
                        {
                            Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                        }
                    }
                    else if (octal)
                    {
                        valStr = new StringBuilder(Convert.ToString(uchval, 8));

                        if (groupDigits)
                        {
                            GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                        }
                    }
                    else if (unsigned)
                    {
                        if (groupDigits)
                        {
                            valStr = new StringBuilder(uchval.ToString("N0", Sprintf.currentCultureNumberFormat));
                        }
                        else
                        {
                            valStr = new StringBuilder(uchval.ToString());
                        }
                    }
                    else
                    {
                        if (groupDigits)
                        {
                            valStr = new StringBuilder(chval.ToString("N0", Sprintf.currentCultureNumberFormat));
                        }
                        else
                        {
                            valStr = new StringBuilder(chval.ToString());
                        }
                    }

                    break;
                case "h":
                    Int16 shval;
                    UInt16 ushval;

                    try
                    {
                        ushval = Convert.ToUInt16(arg);

                        i16Conv.UInt16 = ushval;

                        shval = i16Conv.Int16;
                    }
                    catch (Exception)
                    {
                        try
                        {
                            shval = Convert.ToInt16(arg);

                            i16Conv.Int16 = shval;

                            ushval = i16Conv.UInt16;
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    }

                    if (hex)
                    {
                        valStr = new StringBuilder(Convert.ToString(ushval, 16));

                        if (groupDigits)
                        {
                            Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                        }
                    }
                    else if (octal)
                    {
                        valStr = new StringBuilder(Convert.ToString(ushval, 8));

                        if (groupDigits)
                        {
                            Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                        }
                    }
                    else if (unsigned)
                    {
                        if (groupDigits)
                        {
                            valStr = new StringBuilder(ushval.ToString("N0", Sprintf.currentCultureNumberFormat));
                        }
                        else
                        {
                            valStr = new StringBuilder(ushval.ToString());
                        }
                    }
                    else
                    {
                        if (groupDigits)
                        {
                            valStr = new StringBuilder(shval.ToString("N0", Sprintf.currentCultureNumberFormat));
                        }
                        else
                        {
                            valStr = new StringBuilder(shval.ToString());
                        }
                    }

                    break;
                case "l":
                    if (!Is64BitProcess())
                    {
                        Int64 lval;
                        UInt64 ulval;

                        try
                        {
                            ulval = Convert.ToUInt64(arg);

                            i64Conv.UInt64 = ulval;

                            lval = i64Conv.Int64;
                        }
                        catch (Exception)
                        {
                            try
                            {
                                lval = Convert.ToInt64(arg);

                                i64Conv.Int64 = lval;

                                ulval = i64Conv.UInt64;
                            }
                            catch (Exception)
                            {
                                return null;
                            }
                        }

                        if (hex)
                        {
                            valStr = new StringBuilder(Convert.ToString(lval, 16));

                            if (groupDigits)
                            {
                                Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                            }
                        }
                        else if (octal)
                        {
                            valStr = new StringBuilder(Convert.ToString(lval, 8));

                            if (groupDigits)
                            {
                                Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                            }
                        }
                        else if (unsigned)
                        {
                            if (groupDigits)
                            {
                                valStr = new StringBuilder(ulval.ToString("N0", Sprintf.currentCultureNumberFormat));
                            }
                            else
                            {
                                valStr = new StringBuilder(ulval.ToString());
                            }
                        }
                        else
                        {
                            if (groupDigits)
                            {
                                valStr = new StringBuilder(lval.ToString("N0", Sprintf.currentCultureNumberFormat));
                            }
                            else
                            {
                                valStr = new StringBuilder(lval.ToString());
                            }
                        }

                    }
                    else
                    {
                        Int32 lval;
                        UInt32 ulval;

                        try
                        {
                            ulval = Convert.ToUInt32(arg);

                            i32Conv.UInt32 = ulval;

                            lval = i32Conv.Int32;
                        }
                        catch (Exception)
                        {
                            try
                            {
                                lval = Convert.ToInt32(arg);

                                i32Conv.Int32 = lval;

                                ulval = i32Conv.UInt32;
                            }
                            catch (Exception)
                            {
                                return null;
                            }
                        }


                        if (hex)
                        {
                            valStr = new StringBuilder(Convert.ToString(ulval, 16));

                            if (groupDigits)
                            {
                                Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                            }
                        }
                        else if (octal)
                        {
                            valStr = new StringBuilder(Convert.ToString(ulval, 8));

                            if (groupDigits)
                            {
                                Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                            }
                        }
                        else if (unsigned)
                        {
                            if (groupDigits)
                            {
                                valStr = new StringBuilder(ulval.ToString("N0", Sprintf.currentCultureNumberFormat));
                            }
                            else
                            {
                                valStr = new StringBuilder(ulval.ToString());
                            }
                        }
                        else
                        {
                            if (groupDigits)
                            {
                                valStr = new StringBuilder(lval.ToString("N0", Sprintf.currentCultureNumberFormat));
                            }
                            else
                            {
                                valStr = new StringBuilder(lval.ToString());
                            }
                        }

                    }

                    break;
                case "ll":
                    Int64 llval;
                    UInt64 ullval;

                    try
                    {
                        ullval = Convert.ToUInt64(arg);

                        i64Conv.UInt64 = ullval;

                        llval = i64Conv.Int64;
                    }
                    catch (Exception)
                    {
                        try
                        {
                            llval = Convert.ToInt64(arg);

                            i64Conv.Int64 = llval;

                            ullval = i64Conv.UInt64;
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    }

                    if (hex)
                    {
                        valStr = new StringBuilder(Convert.ToString(llval, 16));

                        if (groupDigits)
                        {
                            Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                        }
                    }
                    else if (octal)
                    {
                        valStr = new StringBuilder(Convert.ToString(llval, 8));

                        if (groupDigits)
                        {
                            Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                        }
                    }
                    else if (unsigned)
                    {
                        if (groupDigits)
                        {
                            valStr = new StringBuilder(ullval.ToString("N0", Sprintf.currentCultureNumberFormat));
                        }
                        else
                        {
                            valStr = new StringBuilder(ullval.ToString());
                        }
                    }
                    else
                    {
                        if (groupDigits)
                        {
                            valStr = new StringBuilder(llval.ToString("N0", Sprintf.currentCultureNumberFormat));
                        }
                        else
                        {
                            valStr = new StringBuilder(llval.ToString());
                        }
                    }

                    break;
                case "z":
                    if (!Is64BitProcess())
                    {
                        Int64 stval;
                        UInt64 ustval;

                        try
                        {
                            ustval = Convert.ToUInt64(arg);

                            i64Conv.UInt64 = ustval;

                            stval = i64Conv.Int64;
                        }
                        catch (Exception)
                        {
                            try
                            {
                                stval = Convert.ToInt64(arg);

                                i64Conv.Int64 = stval;

                                ustval = i64Conv.UInt64;
                            }
                            catch (Exception)
                            {
                                return null;
                            }
                        }

                        if (hex)
                        {
                            valStr = new StringBuilder(Convert.ToString(stval, 16));

                            if (groupDigits)
                            {
                                Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                            }
                        }
                        else if (octal)
                        {
                            valStr = new StringBuilder(Convert.ToString(stval, 8));

                            if (groupDigits)
                            {
                                Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                            }
                        }
                        else if (unsigned)
                        {
                            if (groupDigits)
                            {
                                valStr = new StringBuilder(ustval.ToString("N0", Sprintf.currentCultureNumberFormat));
                            }
                            else
                            {
                                valStr = new StringBuilder(ustval.ToString());
                            }
                        }
                        else
                        {
                            if (groupDigits)
                            {
                                valStr = new StringBuilder(stval.ToString("N0", Sprintf.currentCultureNumberFormat));
                            }
                            else
                            {
                                valStr = new StringBuilder(stval.ToString());
                            }
                        }

                    }
                    else
                    {
                        Int32 stval;
                        UInt32 ustval;

                        try
                        {
                            ustval = Convert.ToUInt32(arg);

                            i32Conv.UInt32 = ustval;

                            stval = i32Conv.Int32;
                        }
                        catch (Exception)
                        {
                            try
                            {
                                stval = Convert.ToInt32(arg);

                                i32Conv.Int32 = stval;

                                ustval = i32Conv.UInt32;
                            }
                            catch (Exception)
                            {
                                return null;
                            }
                        }

                        if (hex)
                        {
                            valStr = new StringBuilder(Convert.ToString(ustval, 16));

                            if (groupDigits)
                            {
                                Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                            }
                        }
                        else if (octal)
                        {
                            valStr = new StringBuilder(Convert.ToString(ustval, 8));

                            if (groupDigits)
                            {
                                Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                            }
                        }
                        else if (unsigned)
                        {
                            if (groupDigits)
                            {
                                valStr = new StringBuilder(ustval.ToString("N0", Sprintf.currentCultureNumberFormat));
                            }
                            else
                            {
                                valStr = new StringBuilder(ustval.ToString());
                            }
                        }
                        else
                        {
                            if (groupDigits)
                            {
                                valStr = new StringBuilder(stval.ToString("N0", Sprintf.currentCultureNumberFormat));
                            }
                            else
                            {
                                valStr = new StringBuilder(stval.ToString());
                            }
                        }
                    }

                    break;
                case "j":
                    Int64 imxval;
                    UInt64 uimxval;

                    try
                    {
                        uimxval = Convert.ToUInt64(arg);

                        i64Conv.UInt64 = uimxval;

                        imxval = i64Conv.Int64;
                    }
                    catch (Exception)
                    {
                        try
                        {
                            imxval = Convert.ToInt64(arg);

                            i64Conv.Int64 = imxval;

                            uimxval = i64Conv.UInt64;
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    }

                    if (hex)
                    {
                        valStr = new StringBuilder(Convert.ToString(imxval, 16));

                        if (groupDigits)
                        {
                            Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                        }
                    }
                    else if (octal)
                    {
                        valStr = new StringBuilder(Convert.ToString(imxval, 8));

                        if (groupDigits)
                        {
                            Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                        }
                    }
                    else if (unsigned)
                    {
                        if (groupDigits)
                        {
                            valStr = new StringBuilder(uimxval.ToString("N0", Sprintf.currentCultureNumberFormat));
                        }
                        else
                        {
                            valStr = new StringBuilder(uimxval.ToString());
                        }
                    }
                    else
                    {
                        if (groupDigits)
                        {
                            valStr = new StringBuilder(imxval.ToString("N0", Sprintf.currentCultureNumberFormat));
                        }
                        else
                        {
                            valStr = new StringBuilder(imxval.ToString());
                        }
                    }

                    break;
                case "t":
                    if (!Is64BitProcess())
                    {
                        Int64 pdval;
                        UInt64 updval;

                        try
                        {
                            updval = Convert.ToUInt64(arg);

                            i64Conv.UInt64 = updval;

                            pdval = i64Conv.Int64;
                        }
                        catch (Exception)
                        {
                            try
                            {
                                pdval = Convert.ToInt64(arg);

                                i64Conv.Int64 = pdval;

                                updval = i64Conv.UInt64;
                            }
                            catch (Exception)
                            {
                                return null;
                            }
                        }

                        if (hex)
                        {
                            valStr = new StringBuilder(Convert.ToString(pdval, 16));

                            if (groupDigits)
                            {
                                Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                            }
                        }
                        else if (octal)
                        {
                            valStr = new StringBuilder(Convert.ToString(pdval, 8));

                            if (groupDigits)
                            {
                                Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                            }
                        }
                        else if (unsigned)
                        {
                            if (groupDigits)
                            {
                                valStr = new StringBuilder(updval.ToString("N0", Sprintf.currentCultureNumberFormat));
                            }
                            else
                            {
                                valStr = new StringBuilder(updval.ToString());
                            }
                        }
                        else
                        {
                            if (groupDigits)
                            {
                                valStr = new StringBuilder(pdval.ToString("N0", Sprintf.currentCultureNumberFormat));
                            }
                            else
                            {
                                valStr = new StringBuilder(pdval.ToString());
                            }
                        }

                    }
                    else
                    {
                        Int32 pdval;
                        UInt32 updval;

                        try
                        {
                            updval = Convert.ToUInt32(arg);

                            i32Conv.UInt32 = updval;

                            pdval = i32Conv.Int32;
                        }
                        catch (Exception)
                        {
                            try
                            {
                                pdval = Convert.ToInt32(arg);

                                i32Conv.Int32 = pdval;

                                updval = i32Conv.UInt32;
                            }
                            catch (Exception)
                            {
                                return null;
                            }
                        }

                        if (hex)
                        {
                            valStr = new StringBuilder(Convert.ToString(updval, 16));

                            if (groupDigits)
                            {
                                Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                            }
                        }
                        else if (octal)
                        {
                            valStr = new StringBuilder(Convert.ToString(updval, 8));

                            if (groupDigits)
                            {
                                Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                            }
                        }
                        else if (unsigned)
                        {
                            if (groupDigits)
                            {
                                valStr = new StringBuilder(updval.ToString("N0", Sprintf.currentCultureNumberFormat));
                            }
                            else
                            {
                                valStr = new StringBuilder(updval.ToString());
                            }
                        }
                        else
                        {
                            if (groupDigits)
                            {
                                valStr = new StringBuilder(pdval.ToString("N0", Sprintf.currentCultureNumberFormat));
                            }
                            else
                            {
                                valStr = new StringBuilder(pdval.ToString());
                            }
                        }
                    }

                    break;
                case "q":
                    Int64 qval;
                    UInt64 uqval;

                    try
                    {
                        uqval = Convert.ToUInt64(arg);

                        i64Conv.UInt64 = uqval;

                        qval = i64Conv.Int64;
                    }
                    catch (Exception)
                    {
                        try
                        {
                            qval = Convert.ToInt64(arg);

                            i64Conv.Int64 = qval;

                            uqval = i64Conv.UInt64;
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    }

                    if (hex)
                    {
                        valStr = new StringBuilder(Convert.ToString(qval, 16));

                        if (groupDigits)
                        {
                            Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                        }
                    }
                    else if (octal)
                    {
                        valStr = new StringBuilder(Convert.ToString(qval, 8));

                        if (groupDigits)
                        {
                            Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                        }
                    }
                    else if (unsigned)
                    {
                        if (groupDigits)
                        {
                            valStr = new StringBuilder(uqval.ToString("N0", Sprintf.currentCultureNumberFormat));
                        }
                        else
                        {
                            valStr = new StringBuilder(uqval.ToString());
                        }
                    }
                    else
                    {
                        if (groupDigits)
                        {
                            valStr = new StringBuilder(qval.ToString("N0", Sprintf.currentCultureNumberFormat));
                        }
                        else
                        {
                            valStr = new StringBuilder(qval.ToString());
                        }
                    }

                    break;
                case "I":
                    if (!Is64BitProcess())
                    {
                        Int64 pdval;
                        UInt64 updval;

                        try
                        {
                            updval = Convert.ToUInt64(arg);

                            i64Conv.UInt64 = updval;

                            pdval = i64Conv.Int64;
                        }
                        catch (Exception)
                        {
                            try
                            {
                                pdval = Convert.ToInt64(arg);

                                i64Conv.Int64 = pdval;

                                updval = i64Conv.UInt64;
                            }
                            catch (Exception)
                            {
                                return null;
                            }
                        }

                        if (hex)
                        {
                            valStr = new StringBuilder(Convert.ToString(pdval, 16));

                            if (groupDigits)
                            {
                                Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                            }
                        }
                        else if (octal)
                        {
                            valStr = new StringBuilder(Convert.ToString(pdval, 8));

                            if (groupDigits)
                            {
                                Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                            }
                        }
                        else if (unsigned)
                        {
                            if (groupDigits)
                            {
                                valStr = new StringBuilder(updval.ToString("N0", Sprintf.currentCultureNumberFormat));
                            }
                            else
                            {
                                valStr = new StringBuilder(updval.ToString());
                            }
                        }
                        else
                        {
                            if (groupDigits)
                            {
                                valStr = new StringBuilder(pdval.ToString("N0", Sprintf.currentCultureNumberFormat));
                            }
                            else
                            {
                                valStr = new StringBuilder(pdval.ToString());
                            }
                        }

                    }
                    else
                    {
                        Int32 pdval;
                        UInt32 updval;

                        try
                        {
                            updval = Convert.ToUInt32(arg);

                            i32Conv.UInt32 = updval;

                            pdval = i32Conv.Int32;
                        }
                        catch (Exception)
                        {
                            try
                            {
                                pdval = Convert.ToInt32(arg);

                                i32Conv.Int32 = pdval;

                                updval = i32Conv.UInt32;
                            }
                            catch (Exception)
                            {
                                return null;
                            }
                        }

                        if (hex)
                        {
                            valStr = new StringBuilder(Convert.ToString(updval, 16));

                            if (groupDigits)
                            {
                                Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                            }
                        }
                        else if (octal)
                        {
                            valStr = new StringBuilder(Convert.ToString(updval, 8));

                            if (groupDigits)
                            {
                                Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                            }
                        }
                        else if (unsigned)
                        {
                            if (groupDigits)
                            {
                                valStr = new StringBuilder(updval.ToString("N0", Sprintf.currentCultureNumberFormat));
                            }
                            else
                            {
                                valStr = new StringBuilder(updval.ToString());
                            }
                        }
                        else
                        {
                            if (groupDigits)
                            {
                                valStr = new StringBuilder(pdval.ToString("N0", Sprintf.currentCultureNumberFormat));
                            }
                            else
                            {
                                valStr = new StringBuilder(pdval.ToString());
                            }
                        }
                    }

                    break;
                case "I32":
                    Int32 i32val;
                    UInt32 ui32val;

                    try
                    {
                        ui32val = Convert.ToUInt32(arg);

                        i32Conv.UInt32 = ui32val;

                        i32val = i32Conv.Int32;
                    }
                    catch (Exception)
                    {
                        try
                        {
                            i32val = Convert.ToInt32(arg);

                            i32Conv.Int32 = i32val;

                            ui32val = i32Conv.UInt32;
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    }

                    if (hex)
                    {
                        valStr = new StringBuilder(Convert.ToString(ui32val, 16));

                        if (groupDigits)
                        {
                            Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                        }
                    }
                    else if (octal)
                    {
                        valStr = new StringBuilder(Convert.ToString(ui32val, 8));

                        if (groupDigits)
                        {
                            Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                        }
                    }
                    else if (unsigned)
                    {
                        if (groupDigits)
                        {
                            valStr = new StringBuilder(ui32val.ToString("N0", Sprintf.currentCultureNumberFormat));
                        }
                        else
                        {
                            valStr = new StringBuilder(ui32val.ToString());
                        }
                    }
                    else
                    {
                        if (groupDigits)
                        {
                            valStr = new StringBuilder(i32val.ToString("N0", Sprintf.currentCultureNumberFormat));
                        }
                        else
                        {
                            valStr = new StringBuilder(i32val.ToString());
                        }
                    }
                    break;
                case "I64":
                    Int64 i64val;
                    UInt64 ui64val;

                    try
                    {
                        ui64val = Convert.ToUInt64(arg);

                        i64Conv.UInt64 = ui64val;

                        i64val = i64Conv.Int64;
                    }
                    catch (Exception)
                    {
                        try
                        {
                            i64val = Convert.ToInt64(arg);

                            i64Conv.Int64 = i64val;

                            ui64val = i64Conv.UInt64;
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    }

                    if (hex)
                    {
                        valStr = new StringBuilder(Convert.ToString(i64val, 16));

                        if (groupDigits)
                        {
                            Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                        }
                    }
                    else if (octal)
                    {
                        valStr = new StringBuilder(Convert.ToString(i64val, 8));

                        if (groupDigits)
                        {
                            Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                        }
                    }
                    else if (unsigned)
                    {
                        if (groupDigits)
                        {
                            valStr = new StringBuilder(ui64val.ToString("N0", Sprintf.currentCultureNumberFormat));
                        }
                        else
                        {
                            valStr = new StringBuilder(ui64val.ToString());
                        }
                    }
                    else
                    {
                        if (groupDigits)
                        {
                            valStr = new StringBuilder(i64val.ToString("N0", Sprintf.currentCultureNumberFormat));
                        }
                        else
                        {
                            valStr = new StringBuilder(i64val.ToString());
                        }
                    }

                    break;
                case "":
                    Int32 ival;
                    UInt32 uival;

                    try
                    {
                        uival = Convert.ToUInt32(arg);

                        i32Conv.UInt32 = uival;

                        ival = i32Conv.Int32;
                    }
                    catch (Exception)
                    {
                        try
                        {
                            ival = Convert.ToInt32(arg);

                            i32Conv.Int32 = ival;

                            uival = i32Conv.UInt32;
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    }

                    if (hex)
                    {
                        valStr = new StringBuilder(Convert.ToString(uival, 16));

                        if (groupDigits)
                        {
                            Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                        }
                    }
                    else if (octal)
                    {
                        valStr = new StringBuilder(Convert.ToString(uival, 8));

                        if (groupDigits)
                        {
                            Sprintf.GroupHexOctal(ref valStr, Sprintf.currentCultureNumberFormat);
                        }
                    }
                    else if (unsigned)
                    {
                        if (groupDigits)
                        {
                            valStr = new StringBuilder(uival.ToString("N0", Sprintf.currentCultureNumberFormat));
                        }
                        else
                        {
                            valStr = new StringBuilder(uival.ToString());
                        }
                    }
                    else
                    {
                        if (groupDigits)
                        {
                            valStr = new StringBuilder(ival.ToString("N0", Sprintf.currentCultureNumberFormat));
                        }
                        else
                        {
                            valStr = new StringBuilder(ival.ToString());
                        }
                    }
                    break;
                default:
                    return null;
            }
            return valStr;
        }

        public static string sprintf(string format, params object[] args)
        {
            int state = 0;
            int numWritten = 0;
            bool minusFlag = false;
            bool plusFlag = false;
            bool spaceFlag = false;
            bool zeroFlag = false;
            bool hashFlag = false;
            bool quoteFlag = false;
            bool IFlag = false;
            StringBuilder width = new StringBuilder();
            StringBuilder precision = new StringBuilder();
            StringBuilder length = new StringBuilder();
            char[] lengthLookback = new char[3] { '\0', '\0', '\0' };
            StringBuilder buf = new StringBuilder();
            bool widthStar = false;
            bool precisionStar = false;
            bool hasWidth = false;
            bool hasPrecision = false;
            CharEnumerator iter = format.GetEnumerator();
            bool hasNext = iter.MoveNext();
            IEnumerator argsIter = args.GetEnumerator();
            bool hasArg = argsIter.MoveNext();

            while (hasNext)
            {
                char ch = iter.Current;

                switch (state)
                {
                    case 0:
                        switch (ch)
                        {
                            case '%':
                                state = 1;
                                break;
                            default:
                                buf.Append(ch);
                                numWritten++;
                                break;
                        }
                        hasNext = iter.MoveNext();
                        break;
                    case 1:
                        switch (ch)
                        {
                            case '%':
                                minusFlag = false;
                                plusFlag = false;
                                spaceFlag = false;
                                zeroFlag = false;
                                hashFlag = false;
                                quoteFlag = false;
                                IFlag = false;
                                widthStar = false;
                                hasWidth = false;
                                hasPrecision = false;
                                width.Clear();
                                precision.Clear();
                                length.Clear();
                                lengthLookback[0] = '\0';
                                lengthLookback[1] = '\0';
                                lengthLookback[2] = '\0';
                                buf.Append(ch);
                                numWritten++;
                                state = 0;
                                hasNext = iter.MoveNext();
                                break;
                            case '-':
                                minusFlag = true;
                                hasNext = iter.MoveNext();
                                break;
                            case '+':
                                plusFlag = true;
                                hasNext = iter.MoveNext();
                                break;
                            case ' ':
                                spaceFlag = true;
                                hasNext = iter.MoveNext();
                                break;
                            case '\'':
                                quoteFlag = true;
                                hasNext = iter.MoveNext();
                                break;
                            case '0':
                                zeroFlag = true;
                                hasNext = iter.MoveNext();
                                break;
                            case '#':
                                hashFlag = true;
                                hasNext = iter.MoveNext();
                                break;
                            default:
                                state = 2;
                                break;
                        }
                        break;
                    case 2:
                        switch (ch)
                        {
                            case '*':
                                if (width.Length > 0)
                                {
                                    return null;
                                }

                                hasWidth = true;
                                widthStar = true;
                                width.Append('*');
                                hasNext = iter.MoveNext();
                                break;
                            case '0':
                            case '1':
                            case '2':
                            case '3':
                            case '4':
                            case '5':
                            case '6':
                            case '7':
                            case '8':
                            case '9':
                                if (widthStar)
                                {
                                    return null;
                                }

                                hasWidth = true;
                                width.Append(ch);
                                hasNext = iter.MoveNext();
                                break;
                            case '.':
                                state = 3;
                                hasNext = iter.MoveNext();
                                break;
                            default:
                                state = 4;
                                break;
                        }
                        break;
                    case 3:
                        switch (ch)
                        {
                            case '*':
                                if (precision.Length > 0)
                                {
                                    return null;
                                }

                                precision.Append('*');
                                precisionStar = true;
                                hasPrecision = true;
                                hasNext = iter.MoveNext();
                                break;
                            case '0':
                            case '1':
                            case '2':
                            case '3':
                            case '4':
                            case '5':
                            case '6':
                            case '7':
                            case '8':
                            case '9':
                                if (precisionStar)
                                {
                                    return null;
                                }

                                hasPrecision = true;
                                precision.Append(ch);
                                hasNext = iter.MoveNext();
                                break;
                            default:
                                state = 4;
                                break;
                        }
                        break;
                    case 4:
                        switch (ch)
                        {
                            case 'h':
                                if (lengthLookback[0] == 'h')
                                {
                                    lengthLookback[1] = 'h';
                                    length.Append(ch);
                                    hasNext = iter.MoveNext();
                                }
                                else if (lengthLookback[0] == '\0')
                                {
                                    lengthLookback[0] = 'h';
                                    length.Append(ch);
                                    hasNext = iter.MoveNext();
                                }
                                else
                                {
                                    state = 5;
                                }

                                break;
                            case 'l':
                                if (lengthLookback[0] == 'l')
                                {
                                    lengthLookback[1] = 'l';
                                    length.Append(ch);
                                    hasNext = iter.MoveNext();
                                }
                                else if (lengthLookback[0] == '\0')
                                {
                                    lengthLookback[0] = 'l';
                                    length.Append(ch);
                                    hasNext = iter.MoveNext();
                                }
                                else
                                {
                                    state = 5;
                                }

                                break;
                            case 'L':
                                if (lengthLookback[0] == '\0')
                                {
                                    lengthLookback[0] = 'L';
                                    length.Append(ch);
                                    hasNext = iter.MoveNext();
                                }
                                else
                                {
                                    state = 5;
                                }
                                break;
                            case 'z':
                                if (lengthLookback[0] == '\0')
                                {
                                    lengthLookback[0] = 'z';
                                    length.Append(ch);
                                    hasNext = iter.MoveNext();
                                }
                                else
                                {
                                    state = 5;
                                }
                                break;
                            case 'j':
                                if (lengthLookback[0] == '\0')
                                {
                                    lengthLookback[0] = 'j';
                                    length.Append(ch);
                                    hasNext = iter.MoveNext();
                                }
                                else
                                {
                                    state = 5;
                                }
                                break;
                            case 't':
                                if (lengthLookback[0] == '\0')
                                {
                                    lengthLookback[0] = 't';
                                    length.Append(ch);
                                    hasNext = iter.MoveNext();
                                }
                                else
                                {
                                    state = 5;
                                }
                                break;
                            case 'I':
                                if (lengthLookback[0] == '\0')
                                {
                                    lengthLookback[0] = 'I';
                                    length.Append(ch);
                                    hasNext = iter.MoveNext();
                                }
                                else
                                {
                                    state = 5;
                                }
                                break;
                            case '2':
                                if (lengthLookback[0] == 'I' && lengthLookback[1] == '3')
                                {
                                    lengthLookback[2] = '2';
                                    length.Append(ch);
                                    hasNext = iter.MoveNext();
                                }
                                else
                                {
                                    state = 5;
                                }
                                break;
                            case '3':
                                if (lengthLookback[0] == 'I')
                                {
                                    lengthLookback[1] = '3';
                                    length.Append(ch);
                                    hasNext = iter.MoveNext();
                                }
                                else
                                {
                                    state = 5;
                                }
                                break;
                            case '4':
                                if (lengthLookback[0] == 'I' && lengthLookback[1] == '6')
                                {
                                    lengthLookback[2] = '4';
                                    length.Append(ch);
                                    hasNext = iter.MoveNext();
                                }
                                else
                                {
                                    state = 5;
                                }
                                break;
                            case '6':
                                if (lengthLookback[0] == 'I')
                                {
                                    lengthLookback[1] = '6';
                                    length.Append(ch);
                                    hasNext = iter.MoveNext();
                                }
                                else
                                {
                                    state = 5;
                                }
                                break;
                            case 'q':
                                if (lengthLookback[0] == '\0')
                                {
                                    lengthLookback[0] = 'q';
                                    length.Append(ch);
                                    hasNext = iter.MoveNext();
                                }
                                else
                                {
                                    state = 5;
                                }
                                break;
                            default:
                                state = 5;
                                break;
                        }
                        break;
                    case 5:
                        int prec = 0;
                        int widt = 0;

                        if (hasWidth)
                        {
                            if (widthStar)
                            {
                                if (!hasArg)
                                {
                                    return null;
                                }
                                else
                                {
                                    try
                                    {
                                        widt = int.Parse(argsIter.Current.ToString());
                                    }
                                    catch (Exception)
                                    {
                                    }
                                    hasArg = argsIter.MoveNext();
                                }
                            }
                            else
                            {
                                try
                                {
                                    widt = int.Parse(width.ToString());
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }

                        if (hasPrecision)
                        {
                            if (precisionStar)
                            {
                                if (!hasArg)
                                {
                                    return null;
                                }
                                else
                                {
                                    try
                                    {
                                        prec = int.Parse(argsIter.Current.ToString());
                                    }
                                    catch (Exception)
                                    {
                                    }
                                    hasArg = argsIter.MoveNext();
                                }
                            }
                            else
                            {
                                try
                                {
                                    prec = int.Parse(precision.ToString());
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }

                        switch (ch)
                        {
                            case 'd':
                            case 'i':
                                if (!hasArg)
                                {
                                    return null;
                                }
                                else
                                {
                                    bool hasNeg = false;
                                    object arg = argsIter.Current;
                                    hasArg = argsIter.MoveNext();

                                    if (arg == null)
                                    {
                                        return null;
                                    }

                                    StringBuilder valStr = Sprintf.sprintfParseIntArg(quoteFlag, false, false, false, arg, length.ToString());
                                    if (valStr == null)
                                    {
                                        return null;
                                    }

                                    if (valStr.Length > 0 && valStr[0] == '-')
                                    {
                                        hasNeg = true;
                                        valStr.Remove(0, 1);
                                    }

                                    if (hasPrecision && prec == 0 && valStr.Length == 1 && valStr[0] == '0')
                                    {
                                        valStr.Clear();
                                    }
                                    else
                                    {
                                        while (valStr.Length < prec)
                                        {
                                            valStr.Insert(0, '0');
                                        }
                                    }

                                    if (!hasPrecision && !minusFlag && zeroFlag)
                                    {
                                        if (hasNeg || plusFlag || spaceFlag || hasNeg)
                                        {
                                            while (valStr.Length < widt - 1)
                                            {
                                                valStr.Insert(0, '0');
                                            }
                                        }
                                        else
                                        {
                                            while (valStr.Length < widt)
                                            {
                                                valStr.Insert(0, '0');
                                            }
                                        }
                                    }

                                    if (hasNeg)
                                    {
                                        valStr.Insert(0, '-');
                                    }
                                    if (!hasNeg && plusFlag)
                                    {
                                        valStr.Insert(0, '+');
                                    }
                                    else if (!hasNeg && spaceFlag)
                                    {
                                        valStr.Insert(0, ' ');
                                    }

                                    if (minusFlag)
                                    {
                                        while (valStr.Length < widt)
                                        {
                                            valStr.Append(' ');
                                        }
                                    }
                                    else
                                    {
                                        while (valStr.Length < widt)
                                        {
                                            valStr.Insert(0, ' ');
                                        }
                                    }

                                    numWritten += valStr.Length;
                                    buf.Append(valStr);
                                }

                                break;
                            case 'u':
                                if (!hasArg)
                                {
                                    return null;
                                }
                                else
                                {
                                    object arg = argsIter.Current;
                                    hasArg = argsIter.MoveNext();

                                    if (arg == null)
                                    {
                                        return null;
                                    }

                                    StringBuilder valStr = Sprintf.sprintfParseIntArg(quoteFlag, true, false, false, arg, length.ToString());
                                    if (valStr == null)
                                    {
                                        return null;
                                    }

                                    if (hasPrecision && prec == 0 && valStr.Length == 1 && valStr[0] == '0')
                                    {
                                        valStr.Clear();
                                    }
                                    else
                                    {
                                        while (valStr.Length < prec)
                                        {
                                            valStr.Insert(0, '0');
                                        }
                                    }

                                    if (!hasPrecision && !minusFlag && zeroFlag)
                                    {
                                        if (plusFlag || spaceFlag)
                                        {
                                            while (valStr.Length < widt - 1)
                                            {
                                                valStr.Insert(0, '0');
                                            }
                                        }
                                        else
                                        {
                                            while (valStr.Length < widt)
                                            {
                                                valStr.Insert(0, '0');
                                            }
                                        }
                                    }

                                    if (plusFlag)
                                    {
                                        valStr.Insert(0, '+');
                                    }
                                    else if (spaceFlag)
                                    {
                                        valStr.Insert(0, ' ');
                                    }

                                    if (minusFlag)
                                    {
                                        while (valStr.Length < widt)
                                        {
                                            valStr.Append(' ');
                                        }
                                    }
                                    else
                                    {
                                        while (valStr.Length < widt)
                                        {
                                            valStr.Insert(0, ' ');
                                        }
                                    }

                                    numWritten += valStr.Length;
                                    buf.Append(valStr);
                                }

                                break;
                            case 'x':
                            case 'X':
                                if (!hasArg)
                                {
                                    return null;
                                }
                                else
                                {
                                    bool isUpper = (ch == 'X');
                                    object arg = argsIter.Current;
                                    hasArg = argsIter.MoveNext();
                                    if (arg == null)
                                    {
                                        return null;
                                    }

                                    StringBuilder valStr = Sprintf.sprintfParseIntArg(quoteFlag, false, true, false, arg, length.ToString());
                                    if (valStr == null)
                                    {
                                        return null;
                                    }

                                    if (hasPrecision && prec == 0 && valStr.Length == 1 && valStr[0] == '0')
                                    {
                                        valStr.Clear();
                                    }
                                    else
                                    {
                                        while (valStr.Length < prec)
                                        {
                                            valStr.Insert(0, '0');
                                        }
                                    }

                                    if (!hasPrecision && !minusFlag && zeroFlag)
                                    {
                                        if (hashFlag)
                                        {
                                            while (valStr.Length < widt - 2)
                                            {
                                                valStr.Insert(0, '0');
                                            }

                                            valStr.Insert(0, "0x");
                                        }
                                        else
                                        {
                                            while (valStr.Length < widt)
                                            {
                                                valStr.Insert(0, '0');
                                            }
                                        }
                                    }

                                    if (hashFlag)
                                    {
                                        valStr.Insert(0, "0x");
                                    }

                                    if (isUpper)
                                    {
                                        Sprintf.Capitalize(ref valStr);
                                    }

                                    if (minusFlag)
                                    {
                                        while (valStr.Length < widt)
                                        {
                                            valStr.Append(' ');
                                        }
                                    }
                                    else
                                    {
                                        while (valStr.Length < widt)
                                        {
                                            valStr.Insert(0, ' ');
                                        }
                                    }

                                    numWritten += valStr.Length;
                                    buf.Append(valStr);
                                }

                                break;
                            case 'o':
                                if (!hasArg)
                                {
                                    return null;
                                }
                                else
                                {
                                    object arg = argsIter.Current;
                                    hasArg = argsIter.MoveNext();
                                    if (arg == null)
                                    {
                                        return null;
                                    }

                                    StringBuilder valStr = Sprintf.sprintfParseIntArg(quoteFlag, false, false, true, arg, length.ToString());
                                    if (valStr == null)
                                    {
                                        return null;
                                    }

                                    if (hasPrecision && prec == 0 && valStr.Length == 1 && valStr[0] == '0')
                                    {
                                        valStr.Clear();
                                    }
                                    else
                                    {
                                        if (hashFlag)
                                        {
                                            while (valStr.Length < prec - 1)
                                            {
                                                valStr.Insert(0, '0');
                                            }
                                        }
                                        else
                                        {
                                            while (valStr.Length < prec)
                                            {
                                                valStr.Insert(0, '0');
                                            }
                                        }
                                    }

                                    if (hashFlag)
                                    {
                                        valStr.Insert(0, '0');
                                    }

                                    if (!hasPrecision && !minusFlag && zeroFlag)
                                    {
                                        while (valStr.Length < widt)
                                        {
                                            valStr.Insert(0, '0');
                                        }
                                    }

                                    if (minusFlag)
                                    {
                                        while (valStr.Length < widt)
                                        {
                                            valStr.Append(' ');
                                        }
                                    }
                                    else
                                    {
                                        while (valStr.Length < widt)
                                        {
                                            valStr.Insert(0, ' ');
                                        }
                                    }

                                    numWritten += valStr.Length;
                                    buf.Append(valStr);
                                }

                                break;
                            case 'e':
                            case 'E':
                            case 'f':
                            case 'F':
                            case 'g':
                            case 'G':
                                if (!hasArg)
                                {
                                    return null;
                                }
                                else
                                {
                                    bool hasNeg = false;
                                    bool scientificNotation = (ch == 'e' || ch == 'E' || ch == 'g' || ch == 'G');
                                    bool decimalFloatingPoint = (ch == 'f' || ch == 'F' || ch == 'g' || ch == 'G');
                                    bool lower = (ch == 'e' || ch == 'f' || ch == 'g');
                                    object arg = argsIter.Current;
                                    hasArg = argsIter.MoveNext();
                                    if (arg == null)
                                    {
                                        return null;
                                    }

                                    StringBuilder valStr = SprintfParseFloatArg(quoteFlag, scientificNotation, decimalFloatingPoint, arg, length.ToString(), hasPrecision, prec);
                                    if (valStr == null)
                                    {
                                        return null;
                                    }

                                    if (hashFlag)
                                    {
                                        AddFloatPeriod(ref valStr);
                                    }

                                    if (valStr.Length > 0 && valStr[0] == '-')
                                    {
                                        hasNeg = true;
                                        valStr.Remove(0, 1);
                                    }

                                    if (zeroFlag && !minusFlag)
                                    {
                                        if (hasNeg || spaceFlag || plusFlag)
                                        {
                                            while (valStr.Length < widt - 1)
                                            {
                                                valStr.Insert(0, '0');
                                            }
                                        }
                                        else
                                        {
                                            while (valStr.Length < widt)
                                            {
                                                valStr.Insert(0, '0');
                                            }
                                        }
                                    }

                                    if (hasNeg)
                                    {
                                        valStr.Insert(0, '-');
                                    }
                                    else if (spaceFlag)
                                    {
                                        valStr.Insert(0, ' ');
                                    }
                                    else if (plusFlag)
                                    {
                                        valStr.Insert(0, '+');
                                    }

                                    if (minusFlag)
                                    {
                                        while (valStr.Length < widt)
                                        {
                                            valStr.Append(' ');
                                        }
                                    }
                                    else
                                    {
                                        while (valStr.Length < widt)
                                        {
                                            valStr.Insert(0, ' ');
                                        }
                                    }

                                    buf.Append(valStr);
                                    numWritten += valStr.Length;
                                }
                                break;
                            case 'a':
                            case 'A':
                                {
                                    if (!hasArg)
                                    {
                                        return null;
                                    }

                                    bool lower = (ch == 'a');
                                    object arg = argsIter.Current;
                                    hasArg = argsIter.MoveNext();

                                    if (arg == null)
                                    {
                                        return null;
                                    }

                                    float f;
                                    double d;

                                    switch (length.ToString())
                                    {
                                        case "L":
                                            /* TODO: find a c# long double type, one doesn't seem to exist... */
                                            try
                                            {
                                                f = Convert.ToSingle(arg);
                                                d = (double)f;
                                            }
                                            catch (Exception)
                                            {
                                                try
                                                {
                                                    d = Convert.ToDouble(arg);
                                                }
                                                catch (Exception)
                                                {
                                                    return null;
                                                }
                                            }

                                            break;
                                        case "":
                                            try
                                            {
                                                f = Convert.ToSingle(arg);
                                                d = (double)f;
                                            }
                                            catch (Exception)
                                            {
                                                try
                                                {
                                                    d = Convert.ToDouble(arg);
                                                }
                                                catch (Exception)
                                                {
                                                    return null;
                                                }
                                            }

                                            break;
                                        default:
                                            return null;
                                    }

                                    StringBuilder valStr;
                                    if (double.IsNaN(d))
                                    {
                                        if (lower)
                                        {
                                            valStr = new StringBuilder("nan");
                                        }
                                        else
                                        {
                                            valStr = new StringBuilder("NAN");
                                        }
                                    }
                                    else if (double.IsNegativeInfinity(d))
                                    {
                                        if (lower)
                                        {
                                            valStr = new StringBuilder("-inf");
                                        }
                                        else
                                        {
                                            valStr = new StringBuilder("-INF");
                                        }
                                    }
                                    else if (double.IsPositiveInfinity(d))
                                    {
                                        if (lower)
                                        {
                                            valStr = new StringBuilder("inf");
                                        }
                                        else
                                        {
                                            valStr = new StringBuilder("INF");
                                        }
                                    }
                                    else
                                    {
                                        bool negative;
                                        long mantissa;
                                        int exponent;
                                        bool fullPrec = false;
                                        Frexp(d, out negative, out mantissa, out exponent);
                                        string mantStr = Convert.ToString(mantissa, 16);
                                        if (prec == 0)
                                        {
                                            mantStr = "";
                                        }
                                        else if (mantStr.Length <= prec)
                                        {
                                            fullPrec = true;
                                        }
                                        else if (mantStr.Length > prec - 1)
                                        {
                                            mantStr = mantStr.Substring(0, prec - 1);
                                        }

                                        valStr = new StringBuilder(mantStr);
                                        if (fullPrec)
                                        {
                                            while (valStr.Length < prec)
                                            {
                                                valStr.Append('0');
                                            }
                                        }
                                        else
                                        {
                                            while (valStr.Length < prec - 1)
                                            {
                                                valStr.Append('0');
                                            }
                                        }

                                        if (prec > 0 || hashFlag)
                                        {
                                            if (mantissa == 0 && exponent == 0)
                                            {
                                                valStr.Insert(0, "0.");
                                            }
                                            else
                                            {
                                                valStr.Insert(0, "1.");
                                            }
                                        }
                                        else
                                        {
                                            if (mantissa == 0 && exponent == 0)
                                            {
                                                valStr.Insert(0, "0");
                                            }
                                            else
                                            {
                                                valStr.Insert(0, "1");
                                            }
                                        }

                                        valStr.Append("P+");

                                        valStr.Append(exponent.ToString());

                                        if (zeroFlag && !minusFlag)
                                        {
                                            if (negative || plusFlag || spaceFlag)
                                            {
                                                while (valStr.Length < widt - 3)
                                                {
                                                    valStr.Insert(0, '0');
                                                }
                                            }
                                            else
                                            {
                                                while (valStr.Length < widt - 2)
                                                {
                                                    valStr.Insert(0, '0');
                                                }
                                            }
                                        }

                                        valStr.Insert(0, "0X");

                                        if (negative)
                                        {
                                            valStr.Insert(0, '-');
                                        }
                                        else if (plusFlag)
                                        {
                                            valStr.Insert(0, '+');
                                        }
                                        else if (spaceFlag)
                                        {
                                            valStr.Insert(0, ' ');
                                        }

                                        if (minusFlag)
                                        {
                                            while (valStr.Length < widt)
                                            {
                                                valStr.Append(' ');
                                            }
                                        }
                                        else
                                        {
                                            while (valStr.Length < widt)
                                            {
                                                valStr.Insert(0, ' ');
                                            }
                                        }
                                    }

                                    buf.Append(valStr);

                                    numWritten += valStr.Length;
                                }
                                break;
                            case 'c':
                            case 'C':
                                {
                                    if (!hasArg)
                                    {
                                        return null;
                                    }

                                    /* %c and %C are the same in C# because char type is already 16bit unicode. */
                                    char cha;
                                    object arg = argsIter.Current;
                                    hasArg = argsIter.MoveNext();

                                    if (arg == null)
                                    {
                                        return null;
                                    }

                                    try
                                    {
                                        cha = Convert.ToChar(arg);
                                    }
                                    catch (Exception)
                                    {
                                        return null;
                                    }

                                    buf.Append(cha);
                                    numWritten++;
                                }
                                break;
                            case 's':
                            case 'S':
                                {
                                    if (!hasArg)
                                    {
                                        return null;
                                    }

                                    /* %s and %S are the same in C# because string type is already 16bit unicode. */
                                    string s;
                                    object arg = argsIter.Current;
                                    hasArg = argsIter.MoveNext();

                                    if (arg == null)
                                    {
                                        return null;
                                    }

                                    s = arg.ToString();
                                    buf.Append(s);
                                    numWritten += s.Length;
                                }
                                break;
                            case 'p':
                                {
                                    if (!hasArg)
                                    {
                                        return null;
                                    }

                                    object arg = argsIter.Current;
                                    hasArg = argsIter.MoveNext();

                                    StringBuilder valStr;

                                    if (Is64BitProcess())
                                    {
                                        long addr;

                                        if (arg == null)
                                        {
                                            addr = 0;
                                        }
                                        else
                                        {
                                            GCHandle gch = GCHandle.Alloc(arg, GCHandleType.WeakTrackResurrection);

                                            IntPtr pObj = gch.AddrOfPinnedObject();

                                            addr = pObj.ToInt64();
                                        }

                                        valStr = new StringBuilder(Convert.ToString(addr, 16));
                                    }
                                    else
                                    {
                                        int addr;

                                        if (arg == null)
                                        {
                                            addr = 0;
                                        }
                                        else
                                        {
                                            GCHandle gch = GCHandle.Alloc(arg, GCHandleType.WeakTrackResurrection);

                                            IntPtr pObj = gch.AddrOfPinnedObject();

                                            addr = pObj.ToInt32();
                                        }

                                        valStr = new StringBuilder(Convert.ToString(addr, 16));
                                    }

                                    while (valStr.Length < prec)
                                    {
                                        valStr.Insert(0, '0');
                                    }

                                    if (!hasPrecision && !minusFlag && zeroFlag)
                                    {
                                        if (spaceFlag || plusFlag)
                                        {
                                            while (valStr.Length < widt - 3)
                                            {
                                                valStr.Insert(0, '0');
                                            }
                                        }
                                        else
                                        {
                                            while (valStr.Length < widt - 2)
                                            {
                                                valStr.Insert(0, '0');
                                            }
                                        }
                                    }

                                    valStr.Insert(0, "0x");

                                    if (plusFlag)
                                    {
                                        valStr.Insert(0, '+');
                                    }
                                    else if (spaceFlag)
                                    {
                                        valStr.Insert(0, ' ');
                                    }

                                    if (minusFlag)
                                    {
                                        while (valStr.Length < widt)
                                        {
                                            valStr.Append(' ');
                                        }
                                    }
                                    else
                                    {
                                        while (valStr.Length < widt)
                                        {
                                            valStr.Insert(0, ' ');
                                        }
                                    }

                                    buf.Append(valStr);
                                    numWritten += valStr.Length;
                                }

                                break;
                            case 'n':
                                /* TODO: write numWritten into the passed argument, find out how to pass in ref object. */
                                break;
                            case 'm':
                                /* write strerror(errno) to buf, find out how to do this. */
                                break;
                            default:
                                return null;
                        }

                        minusFlag = false;
                        plusFlag = false;
                        spaceFlag = false;
                        zeroFlag = false;
                        hashFlag = false;
                        quoteFlag = false;
                        IFlag = false;
                        widthStar = false;
                        hasWidth = false;
                        hasPrecision = false;
                        width.Clear();
                        precision.Clear();
                        length.Clear();
                        lengthLookback[0] = '\0';
                        lengthLookback[1] = '\0';
                        lengthLookback[2] = '\0';
                        state = 0;
                        hasNext = iter.MoveNext();
                        break;
                }
            }

            return buf.ToString();
        }
    }
}