using System;
using System.Collections.Generic;

public class CPlayerSureMsgData
{
    public int msgIndex;
    public byte[] data;
}



public class CPlayerSureMsg
{ 
    PlayerBase mRefPlayer;

    public CPlayerSureSendMsg mSendMsg;
    public CPlayerSureGetMsg mGetMsg;

    public CPlayerSureMsg(PlayerBase p)
    {
        mRefPlayer = p;
    }

    public void Reset()
    {
        mSendMsg.Reset();
        mGetMsg.Reset();
    }

    public long GetPlayerId()
    {
        return mRefPlayer.uid;
    }
}

public class CPlayerSureSendMsg
{
    public int mCurSendMsgIndex = 1;
    public QuickData.QDList<CPlayerSureMsgData> mMsgList = new QuickData.QDList<CPlayerSureMsgData>();
    public void Reset()
    {
        mCurSendMsgIndex = 1;
        mMsgList.ClearAll();
    }

    public byte[] AddMsg(byte [] data)
    {
        int offset = 4;
        BufferHelper.Write(data, mCurSendMsgIndex, ref offset );

        CPlayerSureMsgData d = new CPlayerSureMsgData();
        d.msgIndex = mCurSendMsgIndex++;
        d.data = data;
        mMsgList.Add(d);

       
        return data;
    }

    public void FinishMsg(int msgIndex)
    {
        CPlayerSureMsgData d = mMsgList.GetFirst();
        while(d !=null)
        {
            if (d.msgIndex <= msgIndex)
            {
                mMsgList.GetFirstAndRemove();
                d = mMsgList.GetFirst();
            }
            else
                break;      
        }
    }

    public void AskMsg(int msgIndex, System.Net.IPEndPoint iPEnd )
    {
        foreach (QuickData.QDListNode<CPlayerSureMsgData> d in mMsgList )
        {
            if(d.Data.msgIndex == msgIndex)
            {
                gDefine.gNet.SendMsg(d.Data.data, iPEnd);
                break;
            }
        }
    }

}

public class CPlayerSureGetMsg
{
    public int mCurReceiveMsgIndex = 0;
    public QuickData.QDList<CGetMsgData> mMsgList = new QuickData.QDList<CGetMsgData>();
    long mLastCheckT = 0;

    public class CGetMsgData
    {
        public int msgIndex;
        public CMyNetBuffData data;
    }

    public void Reset()
    {
        mCurReceiveMsgIndex = 0;
        mMsgList.ClearAll();
        mLastCheckT = 0;
    }

    public CMyNetBuffData CheckMsg( int msgIndex, CMyNetBuffData buff)
    {
        if (msgIndex == mCurReceiveMsgIndex + 1)
        {
            mCurReceiveMsgIndex++;
            ReCheckBuff();
            return buff;
        }
        else
        {
            CGetMsgData d = new CGetMsgData();
            d.data = buff;
            d.msgIndex = msgIndex;

            QuickData.QDListNode<CGetMsgData> node = mMsgList.FirstNode();
            while( node != null )
            {
                if (node.Data.msgIndex > msgIndex)
                {
                    mMsgList.InsertBeforeNode(node, d);
                    d = null;
                    break;
                }
            }
            if (d != null)
                mMsgList.Add(d);

            return null;
        }

    }

    public void ReCheckBuff()
    {
        QuickData.QDListNode<CGetMsgData> node = mMsgList.FirstNode();
       
        while (node != null)
        {
            if (node.Data.msgIndex == mCurReceiveMsgIndex + 1)
            {
                gDefine.gNet.AddSureMsgToSureList(node.Data.data);
                mMsgList.GetFirstAndRemove();
                node = mMsgList.FirstNode();
            }
            else
                break;
        }
    }

    

}