//ip的物理地区查询
using System.Web.Script.Serialization;
using System;
using System.Threading;
//using System.Reflection;
using System.Net;


public class  CPositionChecker
{
    QuickData.QDList<PlayerBase> mNeedCheckList = new QuickData.QDList<PlayerBase>();
    QuickData.QDList<PlayerBase> mRegisterList = new QuickData.QDList<PlayerBase>();
    WebClient mWebClient = new WebClient();
    JavaScriptSerializer mJson = new JavaScriptSerializer();
    public static readonly object mDataLock = new object(); //数据锁

    public void Add(PlayerBase p)
    {
        lock(mDataLock)
        {
            mNeedCheckList.Add(p);
        }
    }

    public void AddRegister(PlayerBase p)
    {
        lock (mDataLock)
        {
            mRegisterList.Add(p);
        }
    }

    public void Init()
    {
        Thread buffmsgThread = new Thread(new ThreadStart(Update));
        buffmsgThread.Start();
    }

    public void Update()
    {
        while( !gDefine.gNeedQuit )
        {
            if (mNeedCheckList.Count > 0)
            {
                PlayerBase p;
                lock (mDataLock)
                {
                    p = mNeedCheckList.GetFirstAndRemove();
                }

                //if( !string .IsNullOrEmpty( p.loginIP) )
                //{
                //    string info = mWebClient.DownloadString("http://ipinfo.io/"+ p.loginIP);
                    
                //    IpInfo ipInfo = mJson.Deserialize<IpInfo>(info);

                //    string posInfo = ipInfo.Country + "-" + ipInfo.Region+"-" + ipInfo.City;

                //    p.loginIPPosition = posInfo;
                //}

            }
            else if (mRegisterList.Count > 0)
            {
                PlayerBase p;
                lock (mDataLock)
                {
                    p = mRegisterList.GetFirstAndRemove();
                }

                //if (!string.IsNullOrEmpty(p.registerIP))
                //{
                //    string info = mWebClient.DownloadString("http://ipinfo.io/" + p.registerIP);

                //    IpInfo ipInfo = mJson.Deserialize<IpInfo>(info);

                //    string posInfo = ipInfo.Country + "-" + ipInfo.Region + "-" + ipInfo.City;

                //    p.registerAddress = posInfo;
                //}

            }
            else
                Thread.Sleep(10);
        }

    }

}


public class IpInfo
{
    public string Ip { get; set; }

    public string Hostname { get; set; }

    public string City { get; set; }

    public string Region { get; set; }

    public string Country { get; set; }

    public string Loc { get; set; }

    public string Org { get; set; }

    public string Postal { get; set; }
}