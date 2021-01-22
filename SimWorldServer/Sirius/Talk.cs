using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;

public class CTalkData
{
    public enum eTalkDataType
    {
        Common=0,
        System=1,
    }

    public eTalkDataType talkType;

    public int index;
    public long talkUid;
    public string talkname;
    public string talkStr;
}

public class CTalk
{
    int mCurTalkIndex = 1;
    const int mMaxTalkNum = 500;

    List<CTalkData> mData = new List<CTalkData>();

    public void Add(string talkstr, string talkername, long talkUid)
    {
        CTalkData d = null;

        if (mData.Count >= mMaxTalkNum)
        {
            d = mData[0];
            mData.RemoveAt(0);
        }

        if (d == null)
            d = new CTalkData();

        d.talkname = talkername;
        d.talkStr = talkstr;
        d.talkUid = talkUid;
        d.index = mCurTalkIndex++;

        mData.Add(d);
    }

    public void GetNewTalkData(int talkIndex, IPEndPoint client)
    {
        if (mData.Count > 0)
        {
            if (talkIndex >= mCurTalkIndex - 1)
                return;

            if (talkIndex < 0)
                talkIndex = mCurTalkIndex - 5<1?1: mCurTalkIndex - 5;

            int beginIndex = mData[0].index;
            beginIndex = talkIndex - beginIndex;

            //send five talk msg...
            for (int i = 0; i < 5; i++)
            {
                if (beginIndex + i < mData.Count)
                {
                    lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Talk].mLock)

                    {
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Talk].mValueArr[0].Set(mData[beginIndex+i].talkUid);
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Talk].mValueArr[1].Set(mData[beginIndex + i].talkname);
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Talk].mValueArr[2].Set(mData[beginIndex + i].talkStr);
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Talk].mValueArr[3].Set((int)(mData[beginIndex + i].talkType) );
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Talk].mValueArr[4].Set(mData[beginIndex + i].index);
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Talk].SendMsg(client);
                    }
                }
                else
                    break;
            }
        }
    }
}