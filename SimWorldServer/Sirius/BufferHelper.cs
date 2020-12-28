//消息流读取辅助类
using System;
using System.Text;

public static class BufferHelper
{
    public static void Write(Byte[] buffer, double value, ref Int32 offset)
    {
        byte[] bTemp = BitConverter.GetBytes(value);
        for (Int32 i = 0; i < bTemp.Length; i++)
        {
            buffer[offset++] = bTemp[i];
        }
    }


    public static void Write(Byte[] buffer, short value, ref Int32 offset)
    {
        byte[] bTemp = BitConverter.GetBytes(value);
        for (Int32 i = 0; i < bTemp.Length; i++)
        {
            buffer[offset++] = bTemp[i];
        }
    }

    public static short ReadShort(Byte[] buffer, ref Int32 offset)
    {
        short value = BitConverter.ToInt16(buffer, offset);
        offset += 2;
        return value;
    }

    public static void WriteByte200(Byte[] buffer, Byte []value, ref Int32 offset)
    {
        Buffer.BlockCopy(value, 0, buffer, offset, 200);
        offset += 200;
    }

    public static byte[] ReadByte200(Byte[] buffer, byte[] dest, ref Int32 offset)
    {
        Buffer.BlockCopy( buffer, offset, dest, 0,  200);
        offset += 200;
        return dest;
    }

    public static void Write(Byte[] buffer, Byte value, ref Int32 offset)
    {
        buffer[offset++] = value;
    }

    public static Byte ReadByte(Byte[] buffer, ref Int32 offset)
    {
        Byte value = buffer[offset];
        offset += 1;
        return value;
    }

    public static void Write(Byte[] buffer, long value, ref Int32 offset)
    {
        byte[] bTemp = BitConverter.GetBytes(value);
        for (Int32 i = 0; i < bTemp.Length; i++)
        {
            buffer[offset++] = bTemp[i];  
        }
    }

    public static Int64 ReadInt64(Byte[] buffer, ref Int32 offset)
    {
        Int64 value = BitConverter.ToInt64(buffer, offset);
        offset += 8;
        return value;
    }

    public static void Write(Byte[] buffer, Int32 value, ref Int32 offset)
    {
        byte[] bTemp = BitConverter.GetBytes(value);
        for (Int32 i = 0; i < bTemp.Length; i++)
        {
            buffer[offset++] = bTemp[i];
        }
    }

    public static Int32 ReadInt32(Byte[] buffer, ref Int32 offset)
    {
        Int32 value = BitConverter.ToInt32(buffer, offset);
        offset += 4;
        return value;
    }

    public static double ReadDouble(Byte[] buffer, ref Int32 offset)
    {
        double value = BitConverter.ToDouble(buffer, offset);
        offset += 8;
        return value;
    }

    public static void Write(Byte[] buffer, float value, ref Int32 offset)
    {
        byte[] bTemp = BitConverter.GetBytes(value);
        for (Int32 i = 0; i < bTemp.Length; i++)
        {
            buffer[offset++] = bTemp[i];
        }
    }

    public static Single ReadFloat(Byte[] buffer, ref Int32 offset)
    {
        Single value = BitConverter.ToSingle(buffer, offset);
        offset += 4;
        return value;
    }

    public static void Write(Byte[] buffer, bool value, ref Int32 offset)
    {
        byte[] bTemp = BitConverter.GetBytes(value);
        for (Int32 i = 0; i < bTemp.Length; i++)
        {
            buffer[offset++] = bTemp[i];
        }
    }

    public static Boolean ReadBoolean(Byte[] buffer, ref Int32 offset)
    {
        Boolean value = BitConverter.ToBoolean(buffer, offset);
        offset += 1;
        return value;
    }

    public static void Write(Byte[] buffer, String value, ref Int32 offset)
    {
        if (!String.IsNullOrEmpty(value))
        {
            byte[] bTemp = Encoding.UTF8.GetBytes(value);
            Write(buffer, bTemp.Length, ref offset);
            for (Int32 i = 0; i < bTemp.Length; i++)
            {
                buffer[offset++] = bTemp[i];
            }
        }
        else
        {
            Write(buffer, 0, ref offset);
        }
    }

    public static String ReadString(Byte[] buffer, ref Int32 offset)
    {
        Int32 len = ReadInt32(buffer, ref offset);
        String str = String.Empty;
        if (len > 0)
        {
            Byte[] bTemp = new Byte[len];

            if (buffer.Length - offset < len)
            {
                //int kkk = 1;
            }
            if (buffer.Length < offset + len || bTemp.Length < len)
                return "";

            Buffer.BlockCopy(buffer, offset, bTemp, 0, len);
            offset += len;
            str = Encoding.UTF8.GetString(bTemp);
        }
        return str;
    }
}
