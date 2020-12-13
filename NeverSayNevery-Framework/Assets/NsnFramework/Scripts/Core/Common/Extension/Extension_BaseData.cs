using System.Collections;
using System.Collections.Generic;

public static class Extension_BaseData
{
    public static bool IsNullOrEmpty(this string str)
    {
        return string.IsNullOrEmpty(str);
    }

    public static int ToInt(this float num)
    {
        return (int)num;
    }

    public static int ToInt(this double num)
    {
        return (int)num;
    }

    public static int ToInt(this string str)
    {
        return int.Parse(str);
    }

    public static long ToLong(this float num)
    {
        return (long)num;
    }

    public static long ToLong(this double num)
    {
        return (long)num;
    }

    public static long ToLong(this string str)
    {
        return long.Parse(str);
    }
}
