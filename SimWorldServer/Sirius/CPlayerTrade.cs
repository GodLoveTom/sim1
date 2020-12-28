using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public  class CPlayerTrade
{
    public enum eState
    {
        Close,
        Create,
        Open,
        Lock,
        ConFirm,
        WaitTrade,
    }
    public eState mTradeState = eState.Close;

    public PlayerBase mRefSelf;

    public double mIron;
    public double mCopper;
    public double mGold;
    public double mFood;
    public double mMoney;

    CPlayerTrade mOther = null;


    public void ChangeItem (double Iron, double Copper, double Gold, double Food, double Money)
    {
        if( mTradeState == eState.Open )
        {
           if(mRefSelf.mIron >= Iron && mRefSelf.mCopper >= Copper && mRefSelf.mGold >= Gold && mRefSelf.mMoney >= Money )
            {
                mIron = Iron;
                mCopper = Copper;
                mGold = Gold;
                mFood = Food;
                mMoney = Money;

                lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideItem].mLock)
                {
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideItem].mValueArr[0].Set(mRefSelf.uid);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideItem].mValueArr[1].Set(Iron);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideItem].mValueArr[2].Set(Copper);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideItem].mValueArr[3].Set(Gold);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideItem].mValueArr[4].Set(Food);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideItem].mValueArr[5].Set(Money);

                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideItem].SendMsg(mRefSelf.client);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideItem].SendMsg(mOther.mRefSelf.client);
                }
            }
        }
       
    }

    public void ComFirm(int IsComFirm)
    {
        if (mTradeState == eState.Lock )
        {
            if( IsComFirm == 1 && mTradeState == eState.Lock && mOther.mTradeState >= eState.Lock)
            {
                mTradeState = eState.ConFirm;

                lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideConfim].mLock)
                {
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideConfim].mValueArr[0].Set(mRefSelf.uid);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideConfim].mValueArr[1].Set(1);

                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideConfim].SendMsg(mRefSelf.client);

                    if (mOther != null)
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideConfim].SendMsg(mOther.mRefSelf.client);
                }

                if (mTradeState == eState.ConFirm && mOther.mTradeState == eState.ConFirm)
                {
                    mTradeState = eState.WaitTrade;
                    mOther.mTradeState = eState.WaitTrade;

                   

                    FinishTrade();

                    mOther.FinishTrade();

                }
            }
            else if(IsComFirm ==0 && mTradeState == eState.ConFirm )
            {
                mTradeState = eState.Lock;

                lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideConfim].mLock)
                {
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideConfim].mValueArr[0].Set(mRefSelf.uid);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideConfim].mValueArr[1].Set(0);

                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideConfim].SendMsg(mRefSelf.client);

                    if (mOther != null)
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideConfim].SendMsg(mOther.mRefSelf.client);
                }
            }
        }

    }

    public void FinishTrade()
    {
        //begin trade
        mRefSelf.mIron = mRefSelf.mIron - mIron + mOther.mIron;
        mRefSelf.mCopper = mRefSelf.mCopper - mCopper + mOther.mCopper;
        mRefSelf.mGold = mRefSelf.mGold - mGold + mOther.mGold;
        mRefSelf.mFood = mRefSelf.mFood - mFood + mOther.mFood;
        mRefSelf.mMoney = mRefSelf.mMoney - mMoney + mOther.mMoney;

        mRefSelf.NeedSave();
        mRefSelf.SendResMsg();
        mRefSelf.SendMoneyMsg();

        lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideFinish].mLock)
        {
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideFinish].mValueArr[0].Set(mOther.mIron);
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideFinish].mValueArr[1].Set(mOther.mCopper);
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideFinish].mValueArr[2].Set(mOther.mGold);
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideFinish].mValueArr[3].Set(mOther.mFood);
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideFinish].mValueArr[4].Set(mOther.mMoney);
       
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideFinish].SendMsg(mRefSelf.client);
        }

    }

    public void TradeBegin(long TargetUId)
    {
        PlayerBase target = gDefine.gPlayerBase.Find(TargetUId);
        if( mTradeState == eState.Close && target!= null && target.mTrade.mTradeState == eState.Close )
        {
            mOther = target.mTrade;
            lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideBegin].mLock)
            {
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideBegin].mValueArr[0].Set(mRefSelf.uid);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideBegin].mValueArr[1].Set(mRefSelf.nickname);

                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideBegin].SendMsg(target.client);
            }
        }
    }

    public void TradeAgree( long TargetUId)
    {
        PlayerBase target = gDefine.gPlayerBase.Find(TargetUId);
        if (mTradeState == eState.Close && target != null && target.mTrade.mTradeState == eState.Close
            && target.mTrade.mOther.mRefSelf.uid == mRefSelf.uid)
        {
            mTradeState = eState.Open;
            mOther = target.mTrade;
    
            target.mTrade.mTradeState = eState.Open;
            target.mTrade.mOther = this;

            mIron = mCopper = mGold = mFood = mMoney = 0;
            mOther.mIron = mOther.mCopper = mOther.mGold = mOther.mFood = mOther.mMoney = 0;

            lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideAgree].mLock)
            {
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideAgree].mValueArr[0].Set(mRefSelf.uid);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideAgree].mValueArr[1].Set(TargetUId);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideAgree].mValueArr[2].Set(mRefSelf.nickname);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideAgree].mValueArr[3].Set(target.nickname);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideAgree].SendMsg(mRefSelf.client);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideAgree].SendMsg(target.client);
            }
        }
    }

    public void Lock(int IsLock)
    {
        if (mTradeState == eState.Open && IsLock == 1)
        {
            mTradeState = eState.Lock;

            lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideLock].mLock)
            {
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideLock].mValueArr[0].Set(mRefSelf.uid);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideLock].mValueArr[1].Set(1);

                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideLock].SendMsg(mRefSelf.client);

                if (mOther != null)
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideLock].SendMsg(mOther.mRefSelf.client);
            }
        }
        else if(mTradeState == eState.Lock && IsLock == 0 )
        {
            mTradeState = eState.Open;
            if (mOther.mTradeState == eState.ConFirm)
            {
                mOther.mTradeState = eState.Lock;
                lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideConfim].mLock)
                {
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideConfim].mValueArr[0].Set(mOther.mRefSelf.uid);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideConfim].mValueArr[1].Set(0);

                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideConfim].SendMsg(mRefSelf.client);

                    if (mOther != null)
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideConfim].SendMsg(mOther.mRefSelf.client);
                }

            }

            lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideLock].mLock)
            {
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideLock].mValueArr[0].Set(mRefSelf.uid);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideLock].mValueArr[1].Set(0);

                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideLock].SendMsg(mRefSelf.client);

                if (mOther != null)
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideLock].SendMsg(mOther.mRefSelf.client);
            }


        }

    }

    public void TradeEnd()
    {
        if( mTradeState != eState.Close )
        {
            mTradeState = eState.Close;

            lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideEnd].mLock)
            {
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideEnd].mValueArr[0].Set(mRefSelf.uid);

                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideEnd].SendMsg(mRefSelf.client);

                if (mOther != null)
                {
                    mOther.mTradeState = eState.Close;
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideEnd].SendMsg(mOther.mRefSelf.client);
                    mOther.mOther = null;
                }
            }
            mOther = null;
        }
    }



}

