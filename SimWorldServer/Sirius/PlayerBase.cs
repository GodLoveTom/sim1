using System;
using System.Collections.Generic;
using System.Net;

//基础数据
public class PlayerBase
{
    //存储节点的引用
    QuickData.CQDDataNode refQDnode = null;

    public long uid;

    //账号
    public string name;
    public string pass;
    //昵称
    public string nickname;
    //邮箱
    public string email;

    public int mSex = 2;// 性别：0-男、1-女' 2 无
  
    public long mLoginOutTime = 0;// 最后退出登录时间

    //金币
    public double mMoney = 0;

    public double mIron = 0;

    public double mCopper = 0;

    public double mGold = 0;

    public double mFood = 0;

    public double mWood = 0;

    public double mClay = 0;

    public double mLimestone = 0;

    public double mDaoDe = 0;

    public double mTiLi = 5;

    public double mZhiLi = 5;

    public double mCurTiLi = 5;

    public long mCreateTime;

    public IPEndPoint client;

    public bool mIsLogin = false;

    public int mReliableMsgIndex = 1;

    public CPlayerSureMsg mSureMsg ;

    public long mOnLineT = 0;

    public long mLastGetServerT = 0;

    public long mDigMineBeginT = 0;

    public long mDigMineId = -1;
    public bool mIslogging = false; //当前是否在伐木
    public long mLoggingBT = 0; //伐木计时

    public CPlayerMineData mMineData = new CPlayerMineData();

    public long mLastSearchT = 0;

    public CPlayerTrade mTrade = new CPlayerTrade(); 

    public PlayerBase()
    {
        mSureMsg = new CPlayerSureMsg(this);
        mTrade.mRefSelf = this;
    }

    public void SendNickName()
    {
        lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_NickName].mLock)
        {
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_NickName].mValueArr[0].Set(nickname);
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_NickName].SendMsg(client);
        }
    }

    public void Rob(long TargetUId)
    {
        PlayerBase target = gDefine.gPlayerBase.Find(TargetUId);

        if( mTiLi >= 1.5 && mZhiLi >=1.5 && target != null )
        {
            CMineData [] arr = target.BeRob(mCurTiLi);
            mTiLi -= 1.5;
            mZhiLi -= 1.5;

            StopCurDig();

            if ( arr != null)
            {
                double iron = 0;
                double cobber = 0;
                double food = 0;
                double gold = 0;

                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i].mMineType == CMine.eMineType.Iron)
                        iron += arr[i].mResNum;
                    else if (arr[i].mMineType == CMine.eMineType.Copper)
                        cobber += arr[i].mResNum;
                    else if (arr[i].mMineType == CMine.eMineType.Gold)
                        gold += arr[i].mResNum;
                    else if (arr[i].mMineType == CMine.eMineType.Apple)
                        food += arr[i].mResNum;
                }

                mIron += iron;
                mCopper += cobber;
                mFood += food;
                mGold += gold;
                if (iron > 0)
                {
                    lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Rob].mLock)
                    {
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Rob].mValueArr[0].Set((int)iron);
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Rob].mValueArr[1].Set((int)cobber);
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Rob].mValueArr[2].Set((int)gold);
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Rob].mValueArr[3].Set((int)food);
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Rob].SendMsg(client);
                    }
                }
            }
            else
            {
                lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Rob].mLock)
                {
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Rob].mValueArr[0].Set(0);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Rob].mValueArr[1].Set(0);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Rob].mValueArr[2].Set(0);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Rob].mValueArr[3].Set(0);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Rob].SendMsg(client);
                }
            }

            NeedSave();
            SendDaoDeMsg();
        }   
    }


    public CMineData [] BeSteal(double ZhiLi)
    {
        if ( ZhiLi > mZhiLi )
        {
            List<CMineData> arr = new List<CMineData>();
            int num = gDefine.gRandom.Next(1, 4);
            for(int i=0; i<num; i++)
            {
                int index = gDefine.gRandom.Next(0, 4);
                CMineData mine = GiveMeOneRes(index);
                if (mine == null)
                    break;
                else
                    arr.Add(mine);
            }

            NeedSave();
            return arr.ToArray();
        }
        else
            return null;
    }

    public CMineData[] BeRob(double TiLi)
    {
        if (TiLi > mCurTiLi)
        {
            List<CMineData> arr = new List<CMineData>();
            int num = gDefine.gRandom.Next(3, 8);
            for (int i = 0; i < num; i++)
            {
                int index = gDefine.gRandom.Next(0, 4);
                CMineData mine = GiveMeOneRes(index);
                if (mine == null)
                    break;
                else
                    arr.Add(mine);
            }
            NeedSave();
            if (arr.Count == 0)
                return null;
            else
                return arr.ToArray();
        }
        else
            return null;
    }

    CMineData GiveMeOneRes(int Index)
    {
        if (mIron > 1 || mCopper > 1 || mGold > 1 || mFood > 1)
        {
            if (Index == 0 && mIron > 1)
            {
                mIron -= 1;
                CMineData data = new CMineData();
                data.mMineType = CMine.eMineType.Iron;
                data.mResNum = 1;
                return data;
            }
            else if (Index == 1 && mCopper > 1)
            {
                mCopper -= 1;
                CMineData data = new CMineData();
                data.mMineType = CMine.eMineType.Copper;
                data.mResNum = 1;
                return data;
            }
            else if (Index == 2 && mGold > 1)
            {
                mGold -= 1;
                CMineData data = new CMineData();
                data.mMineType = CMine.eMineType.Gold;
                data.mResNum = 1;
                return data;
            }
            else if (Index == 3 && mFood > 1)
            {
                mFood -= 1;
                CMineData data = new CMineData();
                data.mMineType = CMine.eMineType.Apple;
                data.mResNum = 1;
                return data;
            }
            else
            {
                Index = (Index + 1) % 4;
                return GiveMeOneRes(Index);
            }

        }
        else
            return null;
    }

    public void Steal(long TargetUId)
    {
        PlayerBase target = gDefine.gPlayerBase.Find(TargetUId);

        if (mTiLi >= 0.5 && mZhiLi >= 0.5 && target != null)
        {
            CMineData[] arr = target.BeSteal(mZhiLi);
            mTiLi -= 0.5;
            mZhiLi -= 0.5;

            StopCurDig();

            if (arr != null)
            {
                double iron = 0;
                double cobber = 0;
                double food = 0;
                double gold = 0;

                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i].mMineType == CMine.eMineType.Iron)
                        iron += arr[i].mResNum;
                    else if (arr[i].mMineType == CMine.eMineType.Copper)
                        cobber += arr[i].mResNum;
                    else if (arr[i].mMineType == CMine.eMineType.Gold)
                        gold += arr[i].mResNum;
                    else if (arr[i].mMineType == CMine.eMineType.Apple)
                        food += arr[i].mResNum;
                }

                mIron += iron;
                mCopper += cobber;
                mFood += food;
                mGold += gold;
                if (iron > 0 || cobber>0)
                {
                    lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Steal].mLock)
                    {
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Steal].mValueArr[0].Set((int)iron);
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Steal].mValueArr[1].Set((int)cobber);
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Steal].mValueArr[2].Set((int)gold);
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Steal].mValueArr[3].Set((int)food);
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Steal].SendMsg(client);
                    }
                }
            }
            else
            {
                lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Steal].mLock)
                {
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Steal].mValueArr[0].Set(0);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Steal].mValueArr[1].Set(0);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Steal].mValueArr[2].Set(0);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Steal].mValueArr[3].Set(0);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Steal].SendMsg(client);
                }
            }
            SendDaoDeMsg();
            NeedSave();
        }

    }

    public void SendSearchT()
    {
        lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchT].mLock)
        {
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchT].mValueArr[0].Set(mLastSearchT);
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchT].SendMsg(client);
        }
    }

    public void SearchMan()
    {
        PlayerBase[] dataArr = gDefine.gPlayerBase.SearchMan(this, 10);
        if( dataArr != null )
        {
            for( int i=0; i<dataArr.Length; i++)
            {
                lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMan].mLock)
                {
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMan].mValueArr[0].Set(dataArr[i].uid);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMan].mValueArr[1].Set(dataArr[i].nickname);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMan].mValueArr[2].Set(dataArr[i].mDaoDe);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMan].mValueArr[3].Set((byte)i);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMan].mValueArr[4].Set((byte)dataArr.Length);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMan].SendMsg(client);
                }
            }
        }
    }

    public void SearchMineCoin(int IsMine)
    {
        if (mMoney < 5)
            return;

        mMoney -= 5;
        SendMoneyMsg();
        NeedSave();
        //send money..
      
        StopCurDig();

        //if( System.DateTime.Now.Ticks >= mLastSearchT + (long)36000000000)
        {
            {
                CMineData[] dataArr = gDefine.gMine.SearchOldMineCoin(IsMine, this);

                if (dataArr == null)
                {
                    lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMineFail].mLock)
                    {
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMineFail].mValueArr[0].Set(1);
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMineFail].SendMsg(client);
                    }
                }
                else
                {
                    for (int i = 0; i < dataArr.Length; i++)
                    {
                        lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].mLock)
                        {
                            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].mValueArr[0].Set(dataArr[i].mId);
                            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].mValueArr[1].Set(dataArr[i].mOwnerId);
                            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].mValueArr[2].Set(dataArr[i].mRefOwner.nickname);
                            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].mValueArr[3].Set((int)dataArr[i].mMineType);
                            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].mValueArr[4].Set(dataArr[i].mResNum);
                            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].mValueArr[5].Set(dataArr[i].mPerc);
                            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].mValueArr[6].Set((byte)i);
                            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].mValueArr[7].Set((byte)dataArr.Length);
                            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].SendMsg(client);
                        }
                    }
                }
            }
        }
    }


    public void SearchMine(int IsNew, int IsMine)
    {
        if (IsNew == 0 && System.DateTime.Now.Ticks < mLastSearchT + (long)36000000000)
        {
            lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMineFail].mLock)
            {
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMineFail].mValueArr[0].Set(2);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMineFail].SendMsg(client);
            }
            return;
        }

        StopCurDig();

        //if( System.DateTime.Now.Ticks >= mLastSearchT + (long)36000000000)
        {
            
            if( IsNew == 0  )
            {

                int canAccpetNum = ((int)mDaoDe + (int)mTiLi + (int)mZhiLi) / 10 - mMineData.mOwnMineData.Count;
                if (canAccpetNum <= 0)
                {
                    lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMineFail].mLock)
                    {
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMineFail].mValueArr[0].Set(0);
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMineFail].SendMsg(client);
                    }
                }
                else
                {

                    mLastSearchT = System.DateTime.Now.Ticks;
                    SendSearchT();

                    CMineData[] dataArr = gDefine.gMine.SearchMine(IsNew, IsMine, this);
                    if( dataArr == null)
                    {
                        lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMineFail].mLock)
                        {
                            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMineFail].mValueArr[0].Set( 1);
                            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMineFail].SendMsg(client);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < canAccpetNum; i++)
                        {
                            if (i >= dataArr.Length)
                                break;

                            mMineData.mOwnMineData.Add(dataArr[i]);

                            lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].mLock)
                            {
                                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].mValueArr[0].Set(dataArr[i].mId);
                                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].mValueArr[1].Set(uid);
                                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].mValueArr[2].Set(nickname);
                                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].mValueArr[3].Set((int)dataArr[i].mMineType);
                                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].mValueArr[4].Set(dataArr[i].mResNum);
                                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].mValueArr[5].Set(dataArr[i].mPerc);
                                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].mValueArr[6].Set((byte)i);
                                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].mValueArr[7].Set((byte)canAccpetNum);
                                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].SendMsg(client);
                            }
                        }

                        SendMineMsg();
                    }
                   
                }
                
            }
            else
            {
                CMineData[] dataArr = gDefine.gMine.SearchMine(IsNew, IsMine, this);

                if ( dataArr == null )
                {
                    lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMineFail].mLock)
                    {
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMineFail].mValueArr[0].Set(1);
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMineFail].SendMsg(client);
                    }
                }
                else
                {
                    for (int i = 0; i < dataArr.Length; i++)
                    {
                        lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].mLock)
                        {
                            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].mValueArr[0].Set(dataArr[i].mId);
                            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].mValueArr[1].Set(dataArr[i].mOwnerId);
                            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].mValueArr[2].Set(dataArr[i].mRefOwner.nickname);
                            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].mValueArr[3].Set((int)dataArr[i].mMineType);
                            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].mValueArr[4].Set(dataArr[i].mResNum);
                            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].mValueArr[5].Set(dataArr[i].mPerc);
                            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].mValueArr[6].Set((byte)i);
                            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].mValueArr[7].Set((byte)dataArr.Length);
                            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SearchMine].SendMsg(client);
                        }
                    }
                }           
            }
        }
    }

    public void DelMine(long MineId)
    {
        CMineData mine = mMineData.Del(MineId);
        if( mine != null)
        {
            lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_AskMine].mLock)
            {
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_AskMine].mValueArr[0].Set(mine.mId);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_AskMine].mValueArr[1].Set((int)mine.mMineType);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_AskMine].mValueArr[2].Set(0);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_AskMine].mValueArr[3].Set(mine.mPerc);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_AskMine].SendMsg(client);
            }
        }
    }

    public void StopCurDig()
    {
        if( mDigMineId > 0)
        {
            CMineData mine = gDefine.gMine.Find(mDigMineId);
            if (mine != null)
                mine.mDigPlayerArr.Remove(this);
            mDigMineId = -1;

            SendDigMsg();
        }
    }

    public void BeginLoggin()
    {
        if (mIslogging)
            return;

        mIslogging = true;
        
        mLoggingBT = System.DateTime.Now.Ticks;
        SendLoggingMsg();

        mDigMineId = -1; //采矿将被终止
        SendDigMsg();

        NeedSave();

    }

    public void SendLoggingMsg()
    {
        if (!IsOnLine())
            return;

        lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Logging].mLock)
        {
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Logging].mValueArr[0].Set(mIslogging?1:0);
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Dig].SendMsg(client);
        }
    }


    public void BeginDig(long MineId)
    {
        if (MineId == mDigMineId)
            return;

        CMineData mine = gDefine.gMine.Find(mDigMineId);
        if (mine != null)
            mine.mDigPlayerArr.Remove(this);

        mine = gDefine.gMine.Find(MineId);
        if (mine != null && mine.mResNum > 0)
        {
            mDigMineId = MineId;
            mDigMineBeginT = System.DateTime.Now.Ticks;

            mine.mDigPlayerArr.Add(this);

            SendDigMsg();
            if(mIslogging)
            {
                mIslogging = false; //终止伐木
                SendLoggingMsg();
            }

            NeedSave();
        }
    }

    public void SendDigMsg()
    {
        if (!IsOnLine())
            return;

        CMineData mine = (mDigMineId > 0) ? gDefine.gMine.Find(mDigMineId) : null;
        
        if( mine != null && mine.mResNum > 0)
        {
            lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Dig].mLock)
            {
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Dig].mValueArr[0].Set(mDigMineId);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Dig].mValueArr[1].Set(mine.mOwnerId);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Dig].mValueArr[2].Set(mine.mRefOwner.nickname);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Dig].mValueArr[3].Set(mDigMineBeginT);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Dig].mValueArr[4].Set(mine.mResNum);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Dig].mValueArr[5].Set((int)mine.mMineType);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Dig].mValueArr[6].Set(mine.mPerc);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Dig].SendMsg(client);
            }
        }
        else
        {
            mDigMineId = -1;
            lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Dig].mLock)
            {
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Dig].mValueArr[0].Set((long)-1);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Dig].mValueArr[1].Set((long)0);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Dig].mValueArr[2].Set("");
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Dig].mValueArr[3].Set((long)0);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Dig].mValueArr[4].Set(0.0);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Dig].mValueArr[5].Set(0);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Dig].mValueArr[6].Set(0);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Dig].SendMsg(client);
            }
        }
       
    }

    public void SendMineMsg()
    {
        for (int i = 0; i < mMineData.mOwnMineData.Count; i++)
        {
            lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_AskMine].mLock)
            {
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_AskMine].mValueArr[0].Set(mMineData.mOwnMineData[i].mId);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_AskMine].mValueArr[1].Set((int)mMineData.mOwnMineData[i].mMineType);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_AskMine].mValueArr[2].Set(mMineData.mOwnMineData[i].mResNum);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_AskMine].mValueArr[3].Set(mMineData.mOwnMineData[i].mPerc);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_AskMine].SendMsg(client);
            }
        }
    }

    public void ChangeMinePrec(long MineId, int Prec)
    {
        if( Prec >= 1 && Prec < 100 )
        {
            CMineData data = mMineData.FindFromOwnMine(MineId);
            if(data!=null)
            {
                data.mPerc = Prec;
                data.NeedSave();

                lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_ChangeMinePerc].mLock)
                {
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_ChangeMinePerc].mValueArr[0].Set(data.mId);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_ChangeMinePerc].mValueArr[1].Set(Prec);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_ChangeMinePerc].SendMsg(client);
                }
            }
        }
    }

    public void SendDaoDeMsg()
    {
        lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_AskDaoDe].mLock)
        {
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_AskDaoDe].mValueArr[0].Set(mDaoDe);
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_AskDaoDe].mValueArr[1].Set(mZhiLi);
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_AskDaoDe].mValueArr[2].Set(mTiLi);
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_AskDaoDe].mValueArr[3].Set(mCurTiLi);
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_AskDaoDe].SendMsg(client);
        }
    }

    public void SendResMsg()
    {
        if (!IsOnLine())
            return;

        lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_AskRes].mLock)
        {
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_AskRes].mValueArr[0].Set(mIron);
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_AskRes].mValueArr[1].Set(mCopper);
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_AskRes].mValueArr[2].Set(mGold);
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_AskRes].mValueArr[3].Set(mFood);
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_AskRes].mValueArr[4].Set(mWood);
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_AskRes].mValueArr[5].Set(mClay);
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_AskRes].mValueArr[6].Set(mLimestone);

            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_AskRes].SendMsg(client);
        }
    }

    public void SendMoneyMsg()
    {
        lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Money].mLock)
        {
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Money].mValueArr[0].Set(mMoney);
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Money].SendMsg(client);
        }
    }

    public void SendOnLineTMsg()
    {
        lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_OnLineT].mLock)
        {
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_OnLineT].mValueArr[0].Set(mOnLineT);
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_OnLineT].SendMsg(client);
        }
    }

    public void UpdateOnLineT(long dt)
    {
        mOnLineT += dt;
        if( mOnLineT > (long)360000000000)
        {
            mOnLineT -= (long)360000000000;
            mDaoDe++;
            NeedSave();

            SendDaoDeMsg();
        }
        SendOnLineTMsg();
    }

    public QuickData.CQDDataNode GetQDDataNode()
    {
        return refQDnode;
    }

    // int reason , 0 hire hero. 1 create guild
    public void SpendMoney( double Money, int Reason )
    {
        if( mMoney >= Money )
        {
            mMoney -= Money;
            NeedSave();

            lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SpendMoney].mLock)
            {
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SpendMoney].mValueArr[0].Set(Money);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SpendMoney].mValueArr[1].Set(mMoney);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SpendMoney].mValueArr[2].Set(Reason);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SpendMoney].SendMsg(client);
            }
        }
    }

    public void GainMoney( double Money, int Reason)
    {
        mMoney += Money;
        NeedSave();

        lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SpendMoney].mLock)
        {
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SpendMoney].mValueArr[0].Set(Money);
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SpendMoney].mValueArr[1].Set(mMoney);
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SpendMoney].mValueArr[2].Set(Reason);
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_SpendMoney].SendMsg(client);
        }
    }

    public bool IsLogin()
    {
        return mIsLogin;
    }

    //仅在创建新玩家的时候调用
    public QuickData.CQDDataNode GetSaveNodeFromQD()
    {
        string[] namepath = new string[2] { "PlayerData", uid.ToString() };
        refQDnode = gDefine.gDataSys.CreateNode(null, namepath);
        refQDnode.mReadyParamFunc = ReadyData;
        refQDnode.mReadParamFunc = LoadFromEdit;
        return refQDnode; 
    }

    public void NeedSave()
    {
        gDefine.gDataSys.AddToSaveList(refQDnode);
    }


    public void ReadyData(QuickData.CQDDataNode node)
    {
        node.ClearParam();

        node.AddParam("uid", uid);
        node.AddParam("name", name);
        node.AddParam("pass", pass);
        node.AddParam("nickname", nickname);
        node.AddParam("email", email);

        node.AddParam("createTime", mCreateTime);
        node.AddParam("onLineT", mOnLineT);

        node.AddParam("money", mMoney);
       
        node.AddParam("food", mFood);
        node.AddParam("iron", mIron);
        node.AddParam("copper", mCopper);

        node.AddParam("wood", mWood);
        node.AddParam("clay", mClay);
        node.AddParam("limestone", mLimestone);



        node.AddParam("daode", mDaoDe);
        node.AddParam("zhili", mZhiLi);
        node.AddParam("tili", mTiLi);
        node.AddParam("curtili", mCurTiLi);

        node.AddParam("sex", mSex);

        node.AddParam("mDigMineBeginT", mDigMineBeginT);
        node.AddParam("mDigMineId", mDigMineId);
        node.AddParam("gold", mGold);

        node.AddParam("mIslogging", mIslogging?1:0);

    }

    public void Load(QuickData.CQDDataNode node)
    {
        refQDnode = node;
        refQDnode.mReadyParamFunc = ReadyData;
        refQDnode.mReadParamFunc = LoadFromEdit;

        List<QuickData.CQDParam> param = node.GetCQParam();
        uid = node.GetParamValueLong(0);
        name = node.GetParamValueStr(1);
        pass = node.GetParamValueStr(2);
        nickname = node.GetParamValueStr(3);
        email = node.GetParamValueStr(4);

        mCreateTime = node.GetParamValueLong(5);
        mOnLineT = node.GetParamValueLong(6);

        mMoney = node.GetParamValueDouble(7);
        mFood = node.GetParamValueDouble(8);
        mIron = node.GetParamValueDouble(9);
        mCopper = node.GetParamValueDouble(10);

        mDaoDe = node.GetParamValueDoubleByName("daode",0);
        mZhiLi = node.GetParamValueDoubleByName("zhili", 0);
        mTiLi = node.GetParamValueDoubleByName("tili", 0);
        mCurTiLi = node.GetParamValueDoubleByName("curtili", 0);

        mSex = node.GetParamValueIntByName("sex",0);
        mDigMineBeginT = node.GetParamValueLongByName("mDigMineBeginT",0);
        mDigMineId = node.GetParamValueLongByName("mDigMineId", 0);

       


        // mDaoDe = node.GetParamValueDouble(11);
        // mZhiLi = node.GetParamValueDouble(12);
        // mTiLi = node.GetParamValueDouble(13);
        // mCurTiLi = node.GetParamValueDouble(14);

        // mSex = node.GetParamValueInt(15);

        // mDigMineBeginT = node.GetParamValueLong(16);
        // mDigMineId = node.GetParamValueLong(17);


        mGold = node.GetParamValueDoubleByName("gold", 0);

        mWood = node.GetParamValueDoubleByName("wood", 0);
        mClay = node.GetParamValueDoubleByName("clay", 0);
        mLimestone = node.GetParamValueDoubleByName("limestone", 0);

        mIslogging = (node.GetParamValueIntByName("mIslogging", 0) == 0) ? false : true;
    }

    public void LoadFromEdit(QuickData.CQDDataNode node)
    {
        refQDnode = node;
        refQDnode.mReadyParamFunc = ReadyData;
        refQDnode.mReadParamFunc = LoadFromEdit;

        List<QuickData.CQDParam> param = node.GetCQParam();
        //uid = node.GetParamValueLong(0);
        //name = node.GetParamValueStr(1);
        pass = node.GetParamValueStr(2);
        //nickname = node.GetParamValueStr(3);
        email = node.GetParamValueStr(4);

        mCreateTime = node.GetParamValueLong(5);
        mOnLineT = node.GetParamValueLong(6);

        mMoney = node.GetParamValueDouble(7);
        mFood = node.GetParamValueDouble(8);
        mIron = node.GetParamValueDouble(9);
        mCopper = node.GetParamValueDouble(10);

        mDaoDe = node.GetParamValueDoubleByName("daode", 0);
        mZhiLi = node.GetParamValueDoubleByName("zhili", 0);
        mTiLi = node.GetParamValueDoubleByName("tili", 0);
        mCurTiLi = node.GetParamValueDoubleByName("curtili", 0);

        mSex = node.GetParamValueIntByName("sex", 0);
        mDigMineBeginT = node.GetParamValueLongByName("mDigMineBeginT", 0);
        mDigMineId = node.GetParamValueLongByName("mDigMineId", 0);

        //mDaoDe = node.GetParamValueDouble(11);
        //mZhiLi = node.GetParamValueDouble(12);
        //mTiLi = node.GetParamValueDouble(13);
        //mCurTiLi = node.GetParamValueDouble(14);

        //mSex = node.GetParamValueInt(15);

        //mDigMineBeginT = node.GetParamValueLong(16);
        //mDigMineId = node.GetParamValueLong(17);

        mGold = node.GetParamValueDoubleByName("gold", 0);
        mWood = node.GetParamValueDoubleByName("wood", 0);
        mClay = node.GetParamValueDoubleByName("clay", 0);
        mLimestone = node.GetParamValueDoubleByName("limestone", 0);

        mIslogging = (node.GetParamValueIntByName("mIslogging", 0) == 0) ? false : true;

        NeedSave();
    }

    public int GetDayFromCreate()
    {
        DateTime d = new DateTime(mCreateTime);
        double day0 = d.ToOADate();
        double day1 = System.DateTime.Now.ToOADate();
        return (int)(day1 - day0);
    }

    public bool IsOnLine()
    {
        if (System.DateTime.Now.Ticks < mLoginOutTime)
            return true;
        else
            return false;
    }

    public void UpdateLogging()
    {
        if (!mIslogging)
            return;

        if (mDigMineBeginT <= 0)
        {
            mDigMineBeginT = System.DateTime.Now.Ticks;
            return;
        }

        if (System.DateTime.Now.Ticks - mDigMineBeginT > (long)600000000)
        {
            int min = (int)((System.DateTime.Now.Ticks - mDigMineBeginT) / (long)600000000);
            mDigMineBeginT += min * (long)600000000;

            double digValue = 0;
            if (min <= mCurTiLi)
            {
                mCurTiLi -= min;
                digValue = mCurTiLi / (10 + (mCurTiLi - 10.0) / (100 + (mCurTiLi - 10) / 100.0));
                digValue *= min;
            }
            else
            {
                int tmpmin = (int)mCurTiLi;
                min -= (int)mCurTiLi;

                digValue = mCurTiLi / (10 + (mCurTiLi - 10.0) / (100 + (mCurTiLi - 10) / 100.0));
                digValue *= mCurTiLi;

                mCurTiLi = 0;
            }

            mWood += digValue;
            SendResMsg();
            NeedSave();
            

        }






    }

    public void UpdateDig()
    {
        if (mDigMineId <= 0)
            return;

        if( System.DateTime.Now.Ticks - mDigMineBeginT > (long)600000000)
        {
            int min = (int)((System.DateTime.Now.Ticks - mDigMineBeginT) / (long)600000000);
            mDigMineBeginT += min * (long)600000000;

            CMineData mine = gDefine.gMine.Find(mDigMineId);
            if (mine == null)
            {
                mDigMineId = -1;
                SendDigMsg();
                return;
            }
            else if (mine != null && mine.mResNum <= 0)
            {
                mDigMineId = -1;
                SendDigMsg();
                return;
            }
                

            if (mDigMineId <= 0)
            {           
                mCurTiLi += min * 2;
                if (mCurTiLi > mTiLi)
                    mCurTiLi = mTiLi;
                SendDaoDeMsg();
                return;
            }
            else
            {
                double digValue = 0;
                if ( min <= mCurTiLi )
                {
                    mCurTiLi -= min;
                    digValue = mine.Dig(min, mCurTiLi);
                }
                else
                {
                    int tmpmin = (int)mCurTiLi;
                    min -= (int)mCurTiLi;

                    digValue = mine.Dig((int)mCurTiLi, mCurTiLi);

                    mCurTiLi = 0;

                    tmpmin = min * 2 / 3;
                    if (min % 3 == 1)
                        mCurTiLi = 2;
                    else if( min % 3 == 2)
                    {
                        mCurTiLi = 1;
                        tmpmin++;
                    }

                    digValue += mine.Dig(tmpmin, 1);
                }

                if( digValue > 0 )
                {
                    double ownerValue = digValue * mine.mPerc * 0.01;

                    if (mine.mMineType == CMine.eMineType.Apple)
                    {
                        mFood += digValue - ownerValue;
                        mine.mRefOwner.mFood += ownerValue;
                        mine.mRefOwner.NeedSave();
                    }
                    else if (mine.mMineType == CMine.eMineType.Copper)
                    {
                        mCopper += digValue - ownerValue;
                        mine.mRefOwner.mCopper += ownerValue;
                        mine.mRefOwner.NeedSave();
                    }
                    else if (mine.mMineType == CMine.eMineType.Iron)
                    {
                        mIron += digValue - ownerValue;
                        mine.mRefOwner.mIron += ownerValue;
                        mine.mRefOwner.NeedSave();
                    }
                    else if (mine.mMineType == CMine.eMineType.clay)
                    {
                        mClay += digValue - ownerValue;
                        mine.mRefOwner.mClay += ownerValue;
                        mine.mRefOwner.NeedSave();
                    }
                    else if (mine.mMineType == CMine.eMineType.Limestone)
                    {
                        mLimestone += digValue - ownerValue;
                        mine.mRefOwner.mLimestone += ownerValue;
                        mine.mRefOwner.NeedSave();
                    }
                    else if (mine.mMineType == CMine.eMineType.Gold)
                    {
                        mGold += digValue - ownerValue;
                        mine.mRefOwner.mGold += ownerValue;
                        mine.mRefOwner.NeedSave();
                    }

                    SendDaoDeMsg();
                    SendDigMsg();
                    SendResMsg();
                }     
            }

            NeedSave();
        }


    }

    public void AutoEatApple()
    {
        if( mCurTiLi <= mTiLi - 3 )
        {
            double needNum = (mTiLi - mCurTiLi) / 3;
            double food = (mFood >= needNum) ? needNum : mFood;

            mFood -= food;
            mCurTiLi += food * 3;
            if (mCurTiLi > mTiLi)
                mCurTiLi = mTiLi;

            lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Eat].mLock)
            {
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Eat].mValueArr[0].Set(mFood);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Eat].mValueArr[1].Set(mCurTiLi);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Eat].SendMsg(client);
            }

            SendDaoDeMsg();
            SendResMsg();

            NeedSave();
      
        }


    }




}

