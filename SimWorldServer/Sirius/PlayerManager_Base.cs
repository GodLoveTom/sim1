using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Xml;


public class PlayerManager_Base
{ 
    const int mPlayerMaxNum = 6000;             //服务器玩家最大可注册数量
    const int mUidOffset = 101;                 //
    public long mUIdIndex = 101;                //uid计数
    public PlayerBase[] mPlayer = null;         //玩家列表

    //find quickly dict
    Dictionary<long, PlayerBase> mPlayerIdDict = new Dictionary<long, PlayerBase>();
    Dictionary<string, PlayerBase> mPlayerNameDict = new Dictionary<string, PlayerBase>();
    Dictionary<string, PlayerBase> mPlayerNickNameDict = new Dictionary<string, PlayerBase>();
   
    Dictionary<string, PlayerBase> mForbidNameDict = new Dictionary<string, PlayerBase>();
    Dictionary<string, PlayerBase> mMailDict = new Dictionary<string, PlayerBase>();//会员的邮箱

    QuickData.CQDDataNode mRefDataNode;

    long mLastUpdateT = 0;
    long mLastRefreshT = 0; // 

    public void AddForbidPlayer(PlayerBase p)
    {
        PlayerBase tmp = null;
        if (!mForbidNameDict.TryGetValue(p.name, out tmp))
            mForbidNameDict.Add(p.name, p);
    }

    public void DelForbidPlayer(PlayerBase p)
    {
        mForbidNameDict.Remove(p.name);
    }

    public bool IsInForbidList(string name)
    {
        PlayerBase p = null;
        return mForbidNameDict.TryGetValue(name, out p);
    }

    public PlayerBase [] FindbyPartOfName(string name)
    {
        List<PlayerBase> arr = new List<PlayerBase>();
        for(int i=0; i< mUIdIndex- mUidOffset; i++)
        {
            if (mPlayer[i].nickname.Contains(name))
                arr.Add(mPlayer[i]);
        }
        if (arr.Count > 0)
            return arr.ToArray();
        else
            return null;
    }

    //
    public bool IsSparePos()//
    {
        return mUIdIndex - mUidOffset + 1 < mPlayerMaxNum ? true : false;
    }

    public void Init()
    {
        mPlayer = new PlayerBase[mPlayerMaxNum];
    }

    public PlayerBase Find(long id)
    {
        PlayerBase p = null;
        mPlayerIdDict.TryGetValue(id, out p);
        return p;
    }

    public PlayerBase Find(string name)
    {
        PlayerBase p = null;
        mPlayerNameDict.TryGetValue(name, out p);
        return p;
    }

    public PlayerBase FindByNickName(string nickname)
    {
        PlayerBase p = null;
        mPlayerNickNameDict.TryGetValue(nickname, out p);
        return p;
    }

    public PlayerBase FindByEmail(string mail)
    {
        PlayerBase p = null;
        mMailDict.TryGetValue(mail, out p);
        return p;
    }


    //查看字典里是否有这个邮箱
    //public PlayerBase FindEmail(string Email)
    //{
    //    PlayerBase p = null;
    //    mPlayerEmailDict.TryGetValue(Email, out p);
    //    return p;
    //}

    //发送邮件给指定的邮箱
    public void SendEmail(string email,string pass)
    {
        //新浪
        MailMessage mail = new MailMessage();
        //发送邮箱的地址
        mail.From = new MailAddress("koudaisaijin@sina.com");
        //收件人邮箱地址 如需发送多个人添加多个Add即可
        mail.To.Add(email);
        //标题
        mail.Subject = "口袋赛金";
        //正文
        mail.Body = "尊敬的用户，您在口袋赛金的登录密码是：\n" + pass+"，请妥善保管您的登录密码哦~";
        //添加一个本地附件 
        //mail.Attachments.Add(new Attachment(UnityPath));

        //所使用邮箱的SMTP服务器
        SmtpClient smtpServer = new SmtpClient("smtp.sina.com");
        //SMTP端口
        smtpServer.Port = 587;
        //账号密码 一般邮箱会提供一串字符来代替密码
        smtpServer.Credentials = new System.Net.NetworkCredential("koudaisaijin@sina.com", "koudaisaijin1234") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        System.Net.ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };

        smtpServer.Send(mail);

        //QQ邮箱发送
        //MailMessage mail = new MailMessage();
        ////发送人的邮箱
        //mail.From = new MailAddress("koudaisaijin@sina.com");
        ////收件人的邮箱
        //mail.To.Add(email);
        ////主题
        //mail.Subject = "口袋赛金";
        ////内容
        //mail.Body = "尊敬的用户，您在口袋赛金的登录密码是：\n" + pass;
        ////mail.Attachments.Add(new Attachment("screen.png"));

        //SmtpClient smtpServer = new SmtpClient("imap.sina.com");
        ////发送人的邮箱，IMAP密码
        //smtpServer.Credentials = new System.Net.NetworkCredential("koudaisaijin@sina.com", "587") as System.Net.ICredentialsByHost;
        //smtpServer.EnableSsl = true;
        //System.Net.ServicePointManager.ServerCertificateValidationCallback =
        //        delegate (object s, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        //        { return true; };
        //smtpServer.Send(mail);
    }


    public bool Master_ChangeNickName(string nickname, PlayerBase p)
    {
        if (string.IsNullOrEmpty(nickname))
            return false;

        if (p.nickname == nickname)
            return false;

        //chang...
        mPlayerNickNameDict.Remove(nickname);
        p.nickname = nickname;
        mPlayerNickNameDict.Add(nickname, p);

        return true;
    }

    public bool CheckNamePass(string name, string pass)
    {
        PlayerBase p = Find(name);
        if (p != null && p.pass == pass)
            return true;
        else
            return false;
    }


    public void ChangeNickName(string nickName, PlayerBase p)
    {
        bool canChange = false;
        int err = 0;
        if (string.IsNullOrEmpty(nickName))
        {
            err = 3;
        }
        else if (p.nickname == nickName)
        {
            err = 4;
        }
        else if (p.name != p.nickname)
        {
            err = 2;
        }
        else
        {
            PlayerBase other = null;
            if (mPlayerNickNameDict.TryGetValue(nickName, out other) == true)
            {
                //nick name already exist...
                err = 1;
            }
            else
                canChange = true;
        }

        if( canChange )
        {
            //修改昵称
            mPlayerNickNameDict.Remove(p.nickname);
            p.nickname = nickName;
            mPlayerNickNameDict.Add(nickName, p);
            p.NeedSave();
            p.SendNickName();
        }

        lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_ChangeNickName].mLock)
        {
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_ChangeNickName].mValueArr[0].Set(err);
            CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_ChangeNickName].SendMsg(p.client);
        }

    }

    public PlayerBase AddNewOne(string name, string pass)
    {
        PlayerBase p = new PlayerBase();
        p.uid = mUIdIndex++;
        p.name = name;
        p.pass = pass;
        p.email = "";
        p.mMoney = 0;
        p.mIron = p.mCopper = p.mGold = 0;
        p.mFood = 10;
       
        p.nickname = name;

        p.mCreateTime = System.DateTime.Now.Ticks;
        p.mLoginOutTime = System.DateTime.Now.Ticks + (long)1200000000;
      
        mPlayer[p.uid-mUidOffset] = p;
        mPlayerIdDict.Add( p.uid, p);
        mPlayerNameDict.Add(p.name, p);

        if (mPlayerNickNameDict.ContainsKey(p.nickname))
            p.nickname += gDefine.gRandom.Next(0, 999999);
        mPlayerNickNameDict.Add(p.nickname, p);

        p.GetSaveNodeFromQD();
        p.NeedSave();
        NeedSave();

        return p;
    }


    public PlayerBase RandomFindPlayer()
    {
        if ( mUIdIndex == mUidOffset )
            return null;
        else
        {
            int r =  gDefine.gRandom.Next((int)(mUIdIndex-mUidOffset));
            return mPlayer[r];
        }
    }

    void NeedSave()
    {
        gDefine.gDataSys.AddToSaveList(mRefDataNode);
    }

    public void ReadyData(QuickData.CQDDataNode node)
    {
        node.ClearParam();
        node.AddParam("mUIdIndex", mUIdIndex);
    }

    public void Load()
    {
        string[] nodePathName = new string[1] { "PlayerData" };
        QuickData.CQDNode dataNode =  gDefine.gDataSys.GetNode(null, nodePathName);

        if (dataNode == null)
            mRefDataNode = (QuickData.CQDDataNode)gDefine.gDataSys.CreateNode(null, nodePathName);
        else
            mRefDataNode = (QuickData.CQDDataNode)dataNode;

        mUIdIndex = mRefDataNode.GetParamValueLongByName("mUIdIndex",101);
        mRefDataNode.mReadyParamFunc = ReadyData;

        if (mRefDataNode != null)
        {
            mRefDataNode.LoadSelfChild();
            QuickData.CQDDataNode data = mRefDataNode.FirstChild();
            while (data != null)
            {
                PlayerBase p = new PlayerBase();
                p.Load(data);
                mPlayer[p.uid - mUidOffset] = p;

                mPlayerIdDict.Add(p.uid, p);
                mPlayerNameDict.Add(p.name, p);
                mPlayerNickNameDict.Add(p.nickname, p);
               // if(!string.IsNullOrEmpty(p.email))
                  //  mMailDict.Add(p.email, p);

                data = (QuickData.CQDDataNode)data.refBrother;
            }
        }
    }

    public int CalcOnLineNum()
    {
        int count = 0;
        foreach (PlayerBase p in mPlayerIdDict.Values)
        {
            if (p.IsOnLine())
                count++;
        }
        return count;
    }

    public void Update()
    {
        if( System.DateTime.Now.Ticks - mLastUpdateT >= (long)600000000)
        {
            mLastUpdateT = System.DateTime.Now.Ticks;

            foreach (PlayerBase p in mPlayerIdDict.Values)
            {
                p.UpdateLogging();
                p.UpdateDig();
                p.AutoEatApple();
            }
        }

        //if (System.DateTime.Now.Ticks - mLastRefreshT >= (long)100000000)
        //{
        //    mLastRefreshT = System.DateTime.Now.Ticks;

        //    Refresh(); 
        //}

        


    }

    public void ReCalcMine()
    {
        foreach (PlayerBase p in mPlayerIdDict.Values)
        {
            if( p.mDigMineId > 0 )
            {
                CMineData mine = gDefine.gMine.Find(p.mDigMineId);
                if(mine!=null)
                {
                    mine.mDigPlayerArr.Add(p);
                }
            }
        }
    }

    public PlayerBase [] SearchMan(PlayerBase ExceptPlayer, int Num)
    {
        int curMaxNum =(int)( mUIdIndex - mUidOffset);

        if (Num > curMaxNum - 1)
            Num = curMaxNum - 1;

        PlayerBase[] arr = new PlayerBase[Num];

        int bIndex = gDefine.gRandom.Next(0, curMaxNum);
        int index = 0;
        for(int i=0; i<curMaxNum; i++)
        {
            if (mPlayer[(i + bIndex) % curMaxNum].uid == ExceptPlayer.uid)
                continue;
            else
            {
                arr[index++] = mPlayer[(i + bIndex) % curMaxNum];
                if (index == arr.Length)
                    break;
            }
        }

        return arr;
    }

    public void Refresh()
    {
        XmlDocument doc = new XmlDocument();
        string path = System.Environment.CurrentDirectory + "\\refresh\\refresh.xml";
        try
        {
            
            doc.Load(path);
        }
        catch(Exception)
        {
            Console.WriteLine("没有refresh.xml文件需要处理");
            return;
        }
        try
        {
            XmlNode root = doc.SelectSingleNode("Root");
            if (root != null)
            {
                XmlNodeList nodeList = root.ChildNodes;
                if (nodeList != null)
                {
                    foreach (XmlNode n in nodeList)
                    {
                        XmlElement e = n as XmlElement;
                        string account = e.GetAttribute("account");
                        string pass = e.GetAttribute("pass");
                       

                        PlayerBase p = Find(account);
                        //如果不存在，创建一个 
                        if (p == null)
                        {
                            int error = 0;
                            p = gDefine.gLoginBase.Register(account, pass, out error);
                        }

                        if(e.HasAttribute("money"))
                        {
                            double money = double.Parse(e.GetAttribute("money"));
                            p.mMoney = money;
                        }

                        if(e.HasAttribute("daode"))
                        {
                            double daode = double.Parse(e.GetAttribute("daode"));
                            p.mDaoDe = daode;
                        }

                        p.NeedSave();
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        //chang file name...
        string newpath = System.Environment.CurrentDirectory + "\\refresh\\refresh_"
            + System.DateTime.Now.Year.ToString() + "_"
            + System.DateTime.Now.Month.ToString() + "_"
            + System.DateTime.Now.Day.ToString() + "_"
            + System.DateTime.Now.Hour.ToString() + "_"
            + System.DateTime.Now.Minute.ToString() + "_"
             + System.DateTime.Now.Second.ToString() +
            ".xml";
        File.Move( @path, newpath);

        //
        Console.WriteLine("refresh.xml 处理完毕~~！");

    }






    







}

