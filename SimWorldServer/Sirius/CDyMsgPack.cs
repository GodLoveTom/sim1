//动态注册消息结构类
using System.Collections.Generic;
using System;
using System.Net;

//动态消息类
public class CDyMsgPack
{
    // 消息id
    public int msgid = -1;
    // 处理方向是否注册为lua脚本
    public bool isScript = false;
    // 属性列表
    public List<CBaseValue> mValueArr = new List<CBaseValue>();

    public delegate void CALLBACKFUNC(CDyMsgPack pack, IPEndPoint client);
    public CALLBACKFUNC mCallBackFunc = null;

    //锁(发送消息出去，全局一把锁)
    public readonly object mLock = new object();

    // 消息流大小
    public Int32 Size
    {
        get
        {
            int size = 0;
            foreach (CBaseValue v in mValueArr)
            {
                size += v.Size();
            }
            return size;
        }
    }

    public void Clear()
    {
        isScript = false;
        mValueArr.Clear();
    }

    //属性注册
    public void RegisterValueType(CBaseValue.eBaseValue valueType)
    {
        CBaseValue value = null;
        switch (valueType)
        {
            case CBaseValue.eBaseValue.Bool:
                {
                    value = new CValueBool();
                    break;
                }

            case CBaseValue.eBaseValue.Float:
                {
                    value = new CValueFloat();
                    break;
                }
            case CBaseValue.eBaseValue.Int:
                {
                    value = new CValueInt();
                    break;
                }
            case CBaseValue.eBaseValue.Long:
                {
                    value = new CValueLong();
                    break;
                }
            case CBaseValue.eBaseValue.String:
                {
                    value = new CValueString();
                    break;
                }
            case CBaseValue.eBaseValue.Byte:
                {
                    value = new CValueByte();
                    break;
                }
            case CBaseValue.eBaseValue.ByteArr200:
                {
                    value = new CValueByte200();
                    break;
                }
            case CBaseValue.eBaseValue.Short:
                {
                    value = new CValueShort();
                    break;
                }
            case CBaseValue.eBaseValue.IntArr:
                {
                    value = new CValueIntArr();
                    break;
                }
            case CBaseValue.eBaseValue.LongArr:
                {
                    value = new CValueLongArr();
                    break;
                }
            case CBaseValue.eBaseValue.Double:
                {
                    value = new CValueDouble();
                    break;
                }
        }

        if (value != null)
            mValueArr.Add(value);
    }


    //从数据流中读取数据
    public void ReadPackFromStream(Byte[] buffer)
    {
        int offset = 8;
        foreach (CBaseValue v in mValueArr)
        {
            v.Read(buffer, ref offset);
        }
    }

    //发送消息流去服务器
    public void WriteToStreamBuffer(Byte[] buffer, int offset)
    {
        foreach (CBaseValue v in mValueArr)
        {
            v.Write(buffer, ref offset);
        }
    }

    public CDyMsgPack CloneSelf()
    {
        CDyMsgPack pack = new CDyMsgPack();
        pack.msgid = msgid;
        pack.mCallBackFunc = mCallBackFunc;
        for (int i = 0; i < mValueArr.Count; i++)
        {
            CBaseValue value = mValueArr[i].CloneSelf();
            pack.mValueArr.Add(value);
        }
        return pack;
    }

    public byte[] ToByteBuff(int msgIndex = -1)
    {
        byte[] buffer = new byte[Size + 8];
        int index = 0;
        BufferHelper.Write(buffer, msgid, ref index);
        BufferHelper.Write(buffer, msgIndex, ref index);

        WriteToStreamBuffer(buffer, 8);
        return buffer;
    }

    public void SendMsg(IPEndPoint client, int msgIndex=-1)
    {
        if( client != null )
        {
            byte[] buffer = new byte[Size + 8];
            int index = 0;
            BufferHelper.Write(buffer, msgid, ref index);
            BufferHelper.Write(buffer, msgIndex, ref index);
            WriteToStreamBuffer(buffer, index);
            gDefine.gNet.SendMsg(buffer, client);
        }
       
    }

    public byte[] GetMsgBuff(int msgIndex = -1)
    {
        byte[] buffer = new byte[Size + 8];
        int index = 0;
        BufferHelper.Write(buffer, msgid, ref index);
        BufferHelper.Write(buffer, msgIndex, ref index);
        WriteToStreamBuffer(buffer, index);
        return buffer;
    }

}

//所有消息结构类
public static class CDyMsgPackManager
{
    public static CDyMsgPack[] msgTempleArr = new CDyMsgPack[1800];

    //清理消息结构
    public static void RegisterClearMsg(int msgId, bool isScript)
    {
        SafeCheckMsgArr(msgId);
        msgTempleArr[msgId].Clear();
        msgTempleArr[msgId].isScript = isScript;
    }

    //注册消息结构
    public static void RegisterMsgPackValue(int msgId, int valueType)
    {
        SafeCheckMsgArr(msgId);
        msgTempleArr[msgId].RegisterValueType((CBaseValue.eBaseValue)valueType);
    }

    public static void RegisterMsgPackValue(int msgId, CBaseValue.eBaseValue valueType)
    {
        SafeCheckMsgArr(msgId);
        msgTempleArr[msgId].RegisterValueType(valueType);
    }

    public static void RegisterCallBack(int msgId, CDyMsgPack.CALLBACKFUNC func)
    {
        SafeCheckMsgArr(msgId);
        msgTempleArr[msgId].mCallBackFunc = func;
    }



    //安全检查消息结构大小
    public static void SafeCheckMsgArr(int msgId)
    {
        if (msgTempleArr.Length <= msgId)
        {   //检查消息结构范围
            CDyMsgPack[] arr = new CDyMsgPack[msgId + 100];
            for (int i = 0; i < msgTempleArr.Length; i++)
            {
                arr[i] = msgTempleArr[i];
            }
            msgTempleArr = arr;
        }

        if (msgTempleArr[msgId] == null)
        {
            //初始化一下
            msgTempleArr[msgId] = new CDyMsgPack();
            msgTempleArr[msgId].msgid = msgId;
        }
    }


    public static CDyMsgPack GetMsgPack(int msgId)
    {
        return msgTempleArr[msgId];
    }

    public static void DoMsg(CMyNetBuffData msg, int off = 4)
    {
        int msgId = BitConverter.ToInt32(msg.data, 0);

        if (msgId >= 0 && msgId < msgTempleArr.Length &&
                msgTempleArr[msgId] != null && msgTempleArr[msgId].mCallBackFunc != null)
        {
            CDyMsgPack pack = msgTempleArr[msgId].CloneSelf();
            try
            {
                pack.ReadPackFromStream(msg.data);
                pack.mCallBackFunc(pack, msg.client);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in DoMsg " + e.ToString());
            }
            
        }
    }

}
