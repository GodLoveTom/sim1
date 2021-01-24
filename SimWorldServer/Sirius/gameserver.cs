using System;
using System.Net;
using Sirius;


public class CGameserver
{
    public void def_CS_RegisterFunc(CDyMsgPack msg, IPEndPoint client)
    {
        string name = msg.mValueArr[0].GetString();
        string password = msg.mValueArr[1].GetString();

        if (!gDefine.gPlayerBase.IsSparePos())
        {
            //busy now
            lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Busy].mLock)
            {
                byte[] buff = CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Busy].ToByteBuff();
                gDefine.gNet.SendMsg(buff, client);
            }
            return;
        }

        int error = 0; 
        PlayerBase data = gDefine.gLoginBase.Register(name, password, out error);
        if (data != null)
        {
            //data.mRegisterIP = client.Address.ToString();
            //gDefine.gPostionChecker.AddRegister(data);

            //create new data.
            lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Register].mLock)
            {
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Register].mValueArr[0].Set(error);//result
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Register].SendMsg(client);
            }
       
        }
        else
        {
            lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Register].mLock)
            {
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Register].mValueArr[0].Set(error);//result
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Register].SendMsg(client);
            }
        }
    }

    public void def_CS_LoginFunc(CDyMsgPack msg, IPEndPoint client)
    {
        string name = msg.mValueArr[0].GetString();
        string password = msg.mValueArr[1].GetString();
        byte isUseStoreArrcount = msg.mValueArr[2].GetByte();
        string version = msg.mValueArr[3].GetString();

        if (version != "v1.0.1")
        {
            lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_LoginErr].mLock)
            {
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_LoginErr].mValueArr[0].Set(4);//result
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_LoginErr].SendMsg(client);
            }
            return;
        }

        int result = -1;
        PlayerBase data = gDefine.gLoginBase.LoginIn(name, password, ref result);

        if (data == null || result == 3)
        {
            lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_LoginErr].mLock)
            {
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_LoginErr].mValueArr[0].Set(result);//result
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_LoginErr].SendMsg(client);
            }
            Console.WriteLine("login fail:");
        }
        else
        {
            string curIp = client.Address.ToString();

            //if (data.loginIP != null && data.loginIP != curIp)
            //{
            //    ////踢线
            //    lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_KickOff].mLock)
            //    {
            //        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_KickOff].mValueArr[0].Set(0);
            //        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_KickOff].SendMsg(client);
            //    }
            //}

            data.client = client;

            data.mIsLogin = true;

            data.mLoginOutTime = System.DateTime.Now.Ticks + (long)1200000000;

            // gDefine.gPostionChecker.Add(data);

            //lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_CheckNameAndPassReback].mLock)
            //{
            //    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_CheckNameAndPassReback].mValueArr[0].Set(true);
            //    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_CheckNameAndPassReback].mValueArr[1].Set(name);
            //    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_CheckNameAndPassReback].mValueArr[2].Set(password);

            //    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_CheckNameAndPassReback].SendMsg(client);
            //}

            //昵称
            lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_NickName].mLock)
            {
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_NickName].mValueArr[0].Set(data.nickname);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_NickName].SendMsg(client);
            }


            //金币
            //lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Money].mLock)
            //{
            //    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Money].mValueArr[0].Set(data.mMoney);
            //    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Money].SendMsg(client);
            //}

            ////水晶
            //lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Crystal].mLock)
            //{
            //    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Crystal].mValueArr[0].Set(data.crystal);
            //    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Crystal].SendMsg(client);
            //}

            ////food
            //lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Food].mLock)
            //{
            //    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Food].mValueArr[0].Set(data.mFood);
            //    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Food].SendMsg(client);
            //}

            data.SendDaoDeMsg();

            data.SendMoneyMsg();

            data.SendDigMsg();

            data.SendLoggingMsg();

            data.SendMineMsg();

            data.SendResMsg();

            data.SendSearchT();

            lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_ServerT].mLock)
            {
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_ServerT].mValueArr[0].Set(System.DateTime.Now.Ticks);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_ServerT].SendMsg(client);
            }

            lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_DATAOK].mLock)
            {
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_DATAOK].mValueArr[0].Set(data.uid);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_DATAOK].SendMsg(client);
            }

            Console.WriteLine("login ok:");

            //gDefine.gWorldNoticeSys.AddNotice("欢迎" + data.nickname + "来到口袋赛金的世界~");
        }


    }

    public void def_CS_IsNamePassOK(CDyMsgPack msg, IPEndPoint client)
    {
        string name = msg.mValueArr[0].GetString();
        string password = msg.mValueArr[1].GetString();

        byte error = gDefine.gLoginBase.IsNamePassOK(name, password);
        lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_IsNamePassOK].mLock)
        {
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_IsNamePassOK].mValueArr[0].Set(error);
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_IsNamePassOK].SendMsg(client);
        }

    }

    public void def_CS_IsMailExist(CDyMsgPack msg, IPEndPoint client)
    {
        string mail = msg.mValueArr[0].GetString();
        PlayerBase p = gDefine.gPlayerBase.FindByEmail(mail);
        lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_IsMailExist].mLock)
        {
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_IsMailExist].mValueArr[0].Set(p!=null);
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_IsMailExist].SendMsg(client);
        }

    }


    public void def_CS_ServerT(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            p.client = client;

            p.mLoginOutTime = System.DateTime.Now.Ticks + (long)1200000000;

            if (System.DateTime.Now.Ticks - p.mLastGetServerT > (long)30000000)
                p.UpdateOnLineT(10000000);
            else
                p.UpdateOnLineT(System.DateTime.Now.Ticks - p.mLastGetServerT);

            p.mLastGetServerT = System.DateTime.Now.Ticks;

            lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_ServerT].mLock)
            {
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_ServerT].mValueArr[0].Set(System.DateTime.Now.Ticks);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_ServerT].SendMsg(client);
            }
        }
    }

    public void def_CS_SureMsg(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        int msgIndex = msg.mValueArr[1].GetInt();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            p.mSureMsg.mSendMsg.FinishMsg(msgIndex);
        }
    }

    public void def_CS_ReAskSureMsg(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        int msgIndex = msg.mValueArr[1].GetInt();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            p.mSureMsg.mSendMsg.AskMsg(msgIndex, client);
        }
    }

    public void def_CS_SpendMoney(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        double spendMoney = msg.mValueArr[1].GetDouble();
        int reason = msg.mValueArr[2].GetInt();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            p.SpendMoney(spendMoney, reason);
        }
    }

    public void def_CS_Talk(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        int index = msg.mValueArr[1].GetInt();
        
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            gDefine.gTalkSys.GetNewTalkData(index, p.client);
        }
    }

    public void def_CS_AskDaoDe(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            p.SendDaoDeMsg();       
        }
    }

    public void def_CS_ChangeDaoDe(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            double zhili = msg.mValueArr[1].GetDouble();
            double tili = msg.mValueArr[2].GetDouble();
            if ( p.mDaoDe >= zhili + tili )
            {
                p.mDaoDe -= zhili + tili;
                p.mZhiLi += zhili;
                p.mTiLi += tili;
                //p.mCurTiLi += tili;
                //if (p.mCurTiLi > p.mTiLi)
                //   p.mCurTiLi = p.mTiLi;
                p.NeedSave();
                p.SendDaoDeMsg();
            }
        }
    }

    public void def_CS_AskRes(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            p.SendResMsg();
        }
    }

    public void def_CS_Money(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            p.SendMoneyMsg();
        }
    }

    public void def_CS_BuyDaoDe(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            int daode = msg.mValueArr[1].GetInt();
            int money = daode * 100;
            if( p.mMoney >= money )
            {
                p.mMoney -= money;
                p.mDaoDe += daode;

                lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_BuyDaoDe].mLock)
                {
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_BuyDaoDe].mValueArr[0].Set(daode);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_BuyDaoDe].mValueArr[1].Set(p.mMoney);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_BuyDaoDe].SendMsg(client);
                }

                p.SendDaoDeMsg();
                p.SendMoneyMsg();
            }
           
        }
    }

    public void def_CS_Eat(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            int food = msg.mValueArr[1].GetInt();

            if (p.mFood >= food && p.mCurTiLi < p.mTiLi )
            {
                p.mFood -= food;
                p.mCurTiLi += food * 3;
                if (p.mCurTiLi > p.mTiLi)
                    p.mCurTiLi = p.mTiLi;

                lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Eat].mLock)
                {
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Eat].mValueArr[0].Set(p.mFood);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Eat].mValueArr[1].Set(p.mCurTiLi);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Eat].SendMsg(client);
                }

                p.SendDaoDeMsg();
                p.SendResMsg();

                p.NeedSave();
            }

        }
    }

    public void def_CS_AskMine(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            p.SendMineMsg();
        }
    }

    public void def_CS_ChangeMinePerc(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            long mineId = msg.mValueArr[1].GetLong();
            int prec = msg.mValueArr[2].GetInt();
            p.ChangeMinePrec(mineId, prec);
        }
    }

    public void def_CS_BeginDig(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            long mineId = msg.mValueArr[1].GetLong();
            p.BeginDig(mineId);
        }
    }

    public void def_CS_AskDig(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
            p.SendDigMsg();
    }

    public void def_CS_SearchMine(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            int isNew = msg.mValueArr[1].GetInt();
            int isMine = msg.mValueArr[2].GetInt();

            p.SearchMine(isNew, isMine);

        }   
           
    }

    public void def_CS_SearchMineCoin(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            int isMine = msg.mValueArr[1].GetInt();
            p.SearchMineCoin(isMine);

        }

    }

    public void def_CS_SearchMan(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            p.SearchMan();
        }

    }

    public void def_CS_Rob (CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            long targetId = msg.mValueArr[1].GetLong();
            p.Rob(targetId);
        }
    }

    public void def_CS_Steal(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            long targetId = msg.mValueArr[1].GetLong();
            p.Steal(targetId);
        }
    }

    public void def_CS_TrideByNickName(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            string name = msg.mValueArr[1].GetString();
            PlayerBase [] target = gDefine.gPlayerBase.FindbyPartOfName(name);
            if(target!=null)
            {
                for (int i = 0; i < target.Length; i++)
                {
                    lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideByNickName].mLock)
                    {
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideByNickName].mValueArr[0].Set(target[i].uid);
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideByNickName].mValueArr[1].Set(target[i].nickname);
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideByNickName].mValueArr[2].Set((byte)i);
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideByNickName].mValueArr[3].Set((byte)target.Length);
                        CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideByNickName].SendMsg(client);
                    }
                }
            }
            else
            {
                lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideByNickName].mLock)
                {
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideByNickName].mValueArr[0].Set(-1);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideByNickName].mValueArr[1].Set(" ");
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideByNickName].mValueArr[2].Set((byte)0);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideByNickName].mValueArr[3].Set((byte)0);
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_TrideByNickName].SendMsg(client);
                }
            }
            
        }
    }

    public void def_CS_TrideItem(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            Double iron = msg.mValueArr[1].GetDouble();
            Double cobber = msg.mValueArr[2].GetDouble();
            Double gold = msg.mValueArr[3].GetDouble();
            Double food = msg.mValueArr[4].GetDouble();
            Double money = msg.mValueArr[5].GetDouble();

            Double wood = msg.mValueArr[6].GetDouble();
            Double clay = msg.mValueArr[7].GetDouble();
            Double limestone = msg.mValueArr[8].GetDouble();
            Double ironR = msg.mValueArr[9].GetDouble();
            Double copperR = msg.mValueArr[10].GetDouble();

            p.mTrade.ChangeItem(iron, cobber, gold, food, money,wood,clay, limestone,ironR, copperR);
           
        }
    }

    public void def_CS_TrideLock(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            int IsLock = msg.mValueArr[1].GetInt();
            p.mTrade.Lock(IsLock);
        }
    }

    public void def_CS_TrideConfim(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            int IsConFirm = msg.mValueArr[1].GetInt();
            p.mTrade.ComFirm(IsConFirm);
        }
    }

    public void def_CS_TrideBegin(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            long targetUId = msg.mValueArr[1].GetLong();
            p.mTrade.TradeBegin(targetUId);
        }
    }

    public void def_CS_TrideAgree(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            long targetUId = msg.mValueArr[1].GetLong();
            p.mTrade.TradeAgree(targetUId);
        }
    }

    public void def_CS_TrideEnd(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            p.mTrade.TradeEnd();
        }
    }

    public void def_CS_SearchT(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            p.SendSearchT();
        }
    }

    public void def_CS_TalkSendStr(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            string str = msg.mValueArr[1].GetString();
            gDefine.gTalkSys.Add(str, p.nickname, p.uid);
        }
    }

    public void def_CS_ChangeNickName(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            string str = msg.mValueArr[1].GetString();
            gDefine.gPlayerBase.ChangeNickName(str, p);
        }
    }

    public void def_CS_BeginLogging(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            p.BeginLoggin();
        }
    }

    public void def_CS_Logging(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            p.SendLoggingMsg();
        }
    }

    //---
    public void def_CS_BeginInvent(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            string name = msg.mValueArr[1].GetString();
            string des = msg.mValueArr[2].GetString();
            gDefine.gInvent.CreateInvent(name, des, p);
        }
    }
    public void def_CS_Invent(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            long id = msg.mValueArr[1].GetLong();

            if (id < 0)
                p.mInvent.SendAllDataToPlayer(p);
            else
                gDefine.gInvent.SendInventMsg(id, p);
        }
    }
    public void def_CS_PatentApply(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            long inventId = msg.mValueArr[1].GetLong();
            CInventData invent = gDefine.gInvent.Find(inventId);
            if (invent != null && invent.mInventPcId == uid  && invent.mPatentId == 0)
            {
                invent.mPatentId = 1;
                invent.NeedSave();
                gDefine.gInvent.mPatentApplyDict.Add(invent);
                invent.SendMsg(p);
            }
        }
    }
    public void def_CS_MadeMachineByInvent(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            long inventId = msg.mValueArr[1].GetLong();
            gDefine.gMachine.CreateMachine(inventId, p);
        }
    }

    public void def_CS_Machine(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            long machineId = msg.mValueArr[1].GetLong();
            p.mMachine.SendMsg(machineId,p);
        }
    }

    public void def_CS_ProduceByMachine(CDyMsgPack msg, IPEndPoint client)
    {
        long uid = msg.mValueArr[0].GetLong();
        PlayerBase p = gDefine.gPlayerBase.Find(uid);
        if (p != null)
        {
            long machineId = msg.mValueArr[1].GetLong();
            bool on = msg.mValueArr[2].GetBool();
            p.mMachine.BeginProduce( machineId, on, p);
        }
    }




    public void Init()
    {
        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_Register, gDefine.gGameServer.def_CS_RegisterFunc);
        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_Login, gDefine.gGameServer.def_CS_LoginFunc);
        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_IsNamePassOK, gDefine.gGameServer.def_CS_IsNamePassOK);
        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_IsMailExist, gDefine.gGameServer.def_CS_IsMailExist);

        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_ServerT, gDefine.gGameServer.def_CS_ServerT);

        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_ReAskSureMsg, def_CS_ReAskSureMsg);
        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_SureMsg, def_CS_SureMsg);

        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_Talk, def_CS_Talk);

        //道德
        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_AskDaoDe, def_CS_AskDaoDe);
        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_ChangeDaoDe, def_CS_ChangeDaoDe);

        //res
        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_AskRes, def_CS_AskRes);

        //money
        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_Money, def_CS_Money);
        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_BuyDaoDe, def_CS_BuyDaoDe);

        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_Eat, def_CS_Eat);
        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_AskMine, def_CS_AskMine);

        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_ChangeMinePerc, def_CS_ChangeMinePerc);

        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_BeginDig, def_CS_BeginDig);
        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_AskDig, def_CS_AskDig);

        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_SearchMine, def_CS_SearchMine);

        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_SearchMan, def_CS_SearchMan);

        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_Rob, def_CS_Rob);

        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_Steal, def_CS_Steal);

        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_TrideByNickName, def_CS_TrideByNickName);

        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_TrideItem, def_CS_TrideItem);

        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_TrideLock, def_CS_TrideLock);

        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_TrideConfim, def_CS_TrideConfim);

        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_TrideBegin, def_CS_TrideBegin);

        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_TrideAgree, def_CS_TrideAgree);

        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_TrideEnd, def_CS_TrideEnd);

        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_SearchT, def_CS_SearchT);

        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_TalkSendStr, def_CS_TalkSendStr);

        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_ChangeNickName, def_CS_ChangeNickName);

        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_SearchMineCoin, def_CS_SearchMineCoin);

        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_BeginLogging, def_CS_BeginLogging);
        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_Logging, def_CS_Logging);

        //创造发明
        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_BeginInvent, def_CS_BeginInvent);//提交一个创造发明
        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_Invent, def_CS_Invent);// //请求创造发明数据

        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_PatentApply, def_CS_PatentApply);// //开始申请专利


        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_MadeMachineByInvent, def_CS_MadeMachineByInvent);// // //制造机器
        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_Machine, def_CS_Machine);// //申请玩家机器数据
        CDyMsgPackManager.RegisterCallBack(HeroPack.def_CS_ProduceByMachine, def_CS_ProduceByMachine);// //开始生产



 
}

}
