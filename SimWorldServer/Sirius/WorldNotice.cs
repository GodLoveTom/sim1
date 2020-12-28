////世界通告
//using System.Collections.Generic;
//using System.Net;

//public class CWorldNoticeData
//{
//    public int id = -1; //来自后台时会有id
//    public long bt = 0; //开始时间
//    public long et = 0; //结束时间
//    public long spareT = 300000000; //默认是300s间隔显示一次
//    public string str;  //内容
//    public bool isFromMaster = false;// true,来自后台
//    public bool isOpen = true;       //来自后台，有可能不显示
//}

//public class CWorldNoticeSys
//{
//    List<CWorldNoticeData> mDict = new List<CWorldNoticeData>();
//    public static readonly object mDataLock = new object(); //数据锁

//    public void GetWorldNoticeForPlayer(CS_ClientAskSysNotice msg, PlayerBase p)
//    {
//      //  GetMsgByT(p.client, msg.isFirst);
//    }

//    public void GetMsgByT(IPEndPoint client, bool isFirst)
//    {
//        long t = System.DateTime.Now.Ticks;
//        CWorldNoticeData data = null;
//        for (int i=0; i<mDict.Count; i++ )
//        {
//            lock(mDataLock)
//            {
//                data = mDict[i];
//            }
            
//            if ( data != null && t <= data.et && data.isOpen )
//            {
//                if( isFirst && data.isFromMaster)
//                    SendMsg(data, client);
//                else
//                {
//                    long num = (t - data.bt) / data.spareT;
//                    long dt = t - data.bt - num * data.spareT;
//                    if (dt >= 0 && dt <= 600000000)
//                    {
//                        SendMsg(data, client);
//                    }
//                }
//            }   
//        }
//    }

//    void SendMsg(CWorldNoticeData data, IPEndPoint client)
//    {
//        int num = (data.str.Length+79) / 80;
//        for(int i=0;i<num; i++)
//        {
//            SC_ClientAskSysNoticeReback pack = new SC_ClientAskSysNoticeReback();
//            pack.msgtype = HeroPack.def_SC_ClientAskSysNoticeReback;
//            int len = i < num - 1 ? 80 : data.str.Length - i * 80;
//            pack.str = data.str.Substring(i*80,len);
//            pack.isFromMaster = data.isFromMaster;
//            pack.index = (byte)i;
//            pack.sumNum = (byte)num;
//            gDefine.gNet.SendMsg(pack, client);
//        }
//    }


//    //num 要重复几次？
//    public void AddNotice(string str, long bt =-1, long et=-1,  long sparet = 3000000000, int id = -1,  bool isFromMaster= false, bool isOpen = true)
//    {
//        if ( Clear( id < 0 ? str : "") )
//            return;

//        if ( bt < 0 )
//            bt = System.DateTime.Now.Ticks;

//        if ( et < 0 )
//            et = System.DateTime.Now.Ticks + (long)500000000;

//        CWorldNoticeData n = null;
//        if( id >= 0 )
//            n = FindById(id);

//        if(n==null)
//        {
//            n = new CWorldNoticeData();
//            lock (mDataLock)
//            {
//                mDict.Add(n);
//            }
//        }

//        n.str = str;
//        n.bt = bt;
//        n.et = et;
//        n.id = id;
//        n.spareT = sparet;
//        n.isFromMaster = isFromMaster;
//        n.isOpen = isOpen;
//    }

//    CWorldNoticeData FindById(int id)
//    {
//        for (int i = 0; i < mDict.Count; i++)
//        {
//            if (mDict[i].id == id)
//            {
//                return mDict[i];
//            }
//        }
//        return null;
//    }

//    /// <summary>
//    /// 清除过时的数据,并且str!=null,查询这个字符串是否存在
//    /// </summary>
//    /// <param name="str"></param>
//    /// <returns></returns>
//    bool Clear(string str)
//    {
//        //默认半小时后，通知被消除
//        bool strExist = false;
//        long t = System.DateTime.Now.Ticks;
//        lock(mDataLock)
//        {
//            for (int i = 0; i < mDict.Count; i++)
//            {
//                if (t - mDict[i].et >= (long)10000000)
//                {
//                    mDict.RemoveAt(i);
//                    i--;
//                }
//                else if (str == mDict[i].str)
//                    strExist = true;
//            }
//        }

//        return strExist;
//    }

//    /// <summary>
//    /// 删除某个id的所有的公告
//    /// </summary>
//    /// <param name="id"></param>
//    void RemoveById(int id)
//    {
//        lock (mDataLock)
//        {
//            for (int i = 0; i < mDict.Count; i++)
//            {
//                if ( mDict[i].id == id )
//                {
//                    mDict.RemoveAt(i);
//                }
//            }
//        }
//    }

  

//}