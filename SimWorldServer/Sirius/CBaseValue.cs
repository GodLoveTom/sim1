//动态数据类型
using System;
using System.Text;

public class CBaseValue
{
    //数据类型枚举
    public enum eBaseValue
    {
        Null = 0,
        Int = 1,
        Float = 2,
        Long = 3,
        Bool = 4,
        String = 5,
        Byte = 6,
        ByteArr200 = 7,
        Short = 8,
        ByteArr = 9,
        IntArr = 10,
        LongArr = 11,
        Double = 12,
    }

    public eBaseValue valueType;  //数据类型

    //动态计算数据大小
    public virtual int Size()
    {
        return 0;
    }

    public virtual void Read(byte[] buffer, ref int offset)
    {

    }

    public virtual void Write(byte[] buffer, ref int offset)
    {

    }

    public virtual bool GetBool()
    {
        return false;
    }

    public virtual int GetInt()
    {
        return 0;
    }

    public virtual float GetFloat()
    {
        return 0;
    }

    public virtual long GetLong()
    {
        return 0;
    }

    public virtual double GetDouble()
    {
        return 0;
    }

    public virtual string GetString()
    {
        return null;
    }

    public virtual byte GetByte()
    {
        return 0;
    }

    public virtual short GetShort()
    {
        return 0;
    }

    public virtual byte[] GetByte200()
    {
        return null;
    }

    public virtual byte[] GetByteArr()
    {
        return null;
    }

    public virtual int[] GetIntArr()
    {
        return null;
    }

    public virtual long[] GetLongArr()
    {
        return null;
    }

    public virtual void Set(short value)
    {
    }

    public virtual void Set(bool value)
    {
    }

    public virtual void Set(int value)
    {
    }

    public virtual void Set(float value)
    {
    }

    public virtual void Set(long value)
    {
    }

    public virtual void Set(string value)
    {
    }

    public virtual void Set(byte value)
    {
    }

    public virtual void Set(double value)
    {

    }

    public virtual void SetByte200(byte[] value, int off, int len)
    {
    }

    public virtual void SetIntArr(byte[] value, int off, int len)
    {
    }

    public virtual int GetArrSize()
    {
        return 0;
    }

    public virtual void SetArrSize(int len)
    {
    }

    public virtual void SetIntArrValue(int index, int value)
    {
    }

    public virtual void SetLongArrValue(int index, long value)
    {
    }

    public virtual CBaseValue CloneSelf()
    { return null; }

}


//int型数据扩展
public class CValueByte200 : CBaseValue
{
    public byte[] value = new byte[200];

    public CValueByte200()
    {
        valueType = eBaseValue.ByteArr200;
    }

    public override int Size()
    {
        return sizeof(byte) * 200;
    }

    public override CBaseValue CloneSelf()
    {
        CValueByte200 v = new CValueByte200();
        Buffer.BlockCopy(value, 0, v.value, 0, 200);
        return v;
    }

    public override byte[] GetByte200()
    {
        return value;
    }

    public override void SetByte200(byte[] value, int off, int size)
    {

        Buffer.BlockCopy(value, off, this.value, 0, size);
    }

    public override void Read(byte[] buffer, ref int offset)
    {
        BufferHelper.ReadByte200(buffer, value, ref offset);
    }

    public override void Write(byte[] buffer, ref int offset)
    {
        BufferHelper.WriteByte200(buffer, value, ref offset);
    }
}



//int型数据扩展
public class CValueInt : CBaseValue
{
    public int value;

    public CValueInt()
    {
        valueType = eBaseValue.Int;
    }

    public override int Size()
    {
        return sizeof(int);
    }

    public override CBaseValue CloneSelf()
    {
        CValueInt v = new CValueInt();
        v.value = value;
        return v;
    }

    public override int GetInt()
    {
        return value;
    }

    public override void Set(int value)
    {
        this.value = value;
    }

    public override void Read(byte[] buffer, ref int offset)
    {
        value = BufferHelper.ReadInt32(buffer, ref offset);
    }

    public override void Write(byte[] buffer, ref int offset)
    {
        BufferHelper.Write(buffer, value, ref offset);
    }
}


//short型数据扩展
public class CValueShort : CBaseValue
{
    public short value;

    public CValueShort()
    {
        valueType = eBaseValue.Short;
    }

    public override int Size()
    {
        return sizeof(short);
    }

    public override CBaseValue CloneSelf()
    {
        CValueShort v = new CValueShort();
        v.value = value;
        return v;
    }

    public override short GetShort()
    {
        return value;
    }

    public override void Set(short value)
    {
        this.value = value;
    }

    public override void Read(byte[] buffer, ref int offset)
    {
        value = BufferHelper.ReadShort(buffer, ref offset);
    }

    public override void Write(byte[] buffer, ref int offset)
    {
        BufferHelper.Write(buffer, value, ref offset);
    }
}



//float型数据扩展
public class CValueFloat : CBaseValue
{
    public float value;

    public CValueFloat()
    {
        valueType = eBaseValue.Float;
    }

    public override int Size()
    {
        return sizeof(float);
    }

    public override CBaseValue CloneSelf()
    {
        CValueFloat v = new CValueFloat();
        v.value = value;
        return v;
    }

    public override float GetFloat()
    {
        return value;
    }

    public override void Set(float value)
    {
        this.value = value;
    }

    public override void Read(byte[] buffer, ref int offset)
    {
        value = BufferHelper.ReadFloat(buffer, ref offset);
    }

    public override void Write(byte[] buffer, ref int offset)
    {
        BufferHelper.Write(buffer, value, ref offset);
    }
}
//long型数据扩展
public class CValueLong : CBaseValue
{
    public long value;

    public CValueLong()
    {
        valueType = eBaseValue.Long;
    }

    public override int Size()
    {
        return sizeof(long);
    }

    public override CBaseValue CloneSelf()
    {
        CValueLong v = new CValueLong();
        v.value = value;
        return v;
    }

    public override long GetLong()
    {
        return value;
    }

    public override void Set(long value)
    {
        this.value = value;
    }

    public override void Read(byte[] buffer, ref int offset)
    {
        value = BufferHelper.ReadInt64(buffer, ref offset);
    }

    public override void Write(byte[] buffer, ref int offset)
    {
        BufferHelper.Write(buffer, value, ref offset);
    }
}
//bool型数据扩展
public class CValueBool : CBaseValue
{
    public bool value;

    public CValueBool()
    {
        valueType = eBaseValue.Bool;
    }

    public override int Size()
    {
        return sizeof(bool);
    }

    public override CBaseValue CloneSelf()
    {
        CValueBool v = new CValueBool();
        v.value = value;
        return v;
    }

    public override bool GetBool()
    {
        return value;
    }

    public override void Set(bool value)
    {
        this.value = value;
    }

    public override void Read(byte[] buffer, ref int offset)
    {
        value = BufferHelper.ReadBoolean(buffer, ref offset);
    }

    public override void Write(byte[] buffer, ref int offset)
    {
        BufferHelper.Write(buffer, value, ref offset);
    }
}

//byte型数据扩展
public class CValueByte : CBaseValue
{
    public byte value;

    public CValueByte()
    {
        valueType = eBaseValue.Byte;
    }

    public override int Size()
    {
        return sizeof(byte);
    }

    public override CBaseValue CloneSelf()
    {
        CValueByte v = new CValueByte();
        v.value = value;
        return v;
    }

    public override byte GetByte()
    {
        return value;
    }

    public override void Set(byte value)
    {
        this.value = value;
    }

    public override void Read(byte[] buffer, ref int offset)
    {
        value = BufferHelper.ReadByte(buffer, ref offset);
    }

    public override void Write(byte[] buffer, ref int offset)
    {
        BufferHelper.Write(buffer, value, ref offset);
    }
}


//byte型数据扩展
public class CValueDouble : CBaseValue
{
    public double value;

    public CValueDouble()
    {
        valueType = eBaseValue.Double;
    }

    public override int Size()
    {
        return sizeof(double);
    }

    public override CBaseValue CloneSelf()
    {
        CValueDouble v = new CValueDouble();
        v.value = value;
        return v;
    }

    public override double GetDouble()
    {
        return value;
    }

    public override void Set(double value)
    {
        this.value = value;
    }

    public override void Read(byte[] buffer, ref int offset)
    {
        value = BufferHelper.ReadDouble(buffer, ref offset);
    }

    public override void Write(byte[] buffer, ref int offset)
    {
        BufferHelper.Write(buffer, value, ref offset);
    }
}



//IntArr型数据扩展
public class CValueIntArr : CBaseValue
{
    public int size = 0;
    public int[] value = null;

    public CValueIntArr()
    {
        valueType = eBaseValue.IntArr;
    }

    public override int Size()
    {
        return sizeof(int) * size;
    }

    public override CBaseValue CloneSelf()
    {
        CValueIntArr v = new CValueIntArr();
        v.size = size;
        v.value = new int[size];
        for (int i = 0; i < value.Length; i++)
            v.value[i] = value[i];
        return v;
    }


    public override void SetArrSize(int len)
    {
        size = len;
        value = new int[size];
    }

    public override int GetArrSize()
    {
        return size;
    }

    public override int[] GetIntArr()
    {
        return value;
    }

    public override void Read(byte[] buffer, ref int offset)
    {
        size = BufferHelper.ReadInt32(buffer, ref offset);
        value = new int[size];
        for (int i = 0; i < size; i++)
            value[i] = BufferHelper.ReadInt32(buffer, ref offset);
    }

    public override void Write(byte[] buffer, ref int offset)
    {
        BufferHelper.Write(buffer, size, ref offset);
        for (int i = 0; i < size; i++)
            BufferHelper.Write(buffer, value[i], ref offset);
    }

    public override void SetIntArrValue(int index, int v)
    {
        if (index >= 0 && index < size)
            value[index] = v;
    }
}



//IntArr型数据扩展
public class CValueLongArr : CBaseValue
{
    public int size = 0;
    public long[] value = null;

    public CValueLongArr()
    {
        valueType = eBaseValue.LongArr;
    }

    public override int Size()
    {
        return sizeof(long) * size;
    }

    public override CBaseValue CloneSelf()
    {
        CValueLongArr v = new CValueLongArr();
        v.size = size;
        v.value = new long[size];
        for (int i = 0; i < value.Length; i++)
            v.value[i] = value[i];
        return v;
    }

    public override long[] GetLongArr()
    {
        return value;
    }

    public override void SetArrSize(int len)
    {
        size = len;
        value = new long[size];
    }

    public override int GetArrSize()
    {
        return size;
    }

    public override void Read(byte[] buffer, ref int offset)
    {
        size = BufferHelper.ReadInt32(buffer, ref offset);
        value = new long[size];
        for (int i = 0; i < size; i++)
            value[i] = BufferHelper.ReadInt64(buffer, ref offset);
    }

    public override void Write(byte[] buffer, ref int offset)
    {
        BufferHelper.Write(buffer, size, ref offset);
        for (int i = 0; i < size; i++)
            BufferHelper.Write(buffer, value[i], ref offset);
    }

    public override void SetLongArrValue(int index, long v)
    {
        if (index >= 0 && index < size)
            value[index] = v;
    }
}



//string型数据扩展
public class CValueString : CBaseValue
{
    public string value;

    public CValueString()
    {
        valueType = eBaseValue.String;
    }

    public override int Size()
    {
        if (string.IsNullOrEmpty(value))
        {
            return sizeof(int);
        }
        else
        {
            return Encoding.UTF8.GetBytes(value).Length + sizeof(int);
        }

    }

    public override CBaseValue CloneSelf()
    {
        CValueString v = new CValueString();
        v.value = value;

        return v;
    }

    public override string GetString()
    {
        return value;
    }

    public override void Set(string value)
    {
        this.value = value;
    }

    public override void Read(byte[] buffer, ref int offset)
    {
        value = BufferHelper.ReadString(buffer, ref offset);
    }

    public override void Write(byte[] buffer, ref int offset)
    {
        BufferHelper.Write(buffer, value, ref offset);
    }

}