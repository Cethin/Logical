using System;

public static class BinaryUtility
{
    public static string intToBinString(int value)
    {
        return Convert.ToString(value, 2);
    }

    public static string intToBinString(int value, int numBits)
    {
        return Convert.ToString(value, 2).PadLeft(numBits, '0');
    }

    public static bool getBit(int value, int index)
    {
        return (value & (0b1 << index)) > 0;
    }
}
