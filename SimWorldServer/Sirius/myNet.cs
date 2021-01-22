using System;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Text;


public class CMyNetBuffData//发送，接受缓存区消息包数据
{
    public byte[] data;
    public IPEndPoint client;
}

public class CMyNet
{
    QuickData.QDList<CMyNetBuffData> mReceiveBuffDict = new QuickData.QDList<CMyNetBuffData>();
    private static readonly object mReceiveBuffLock = new object(); //接受信息锁

    QuickData.QDList<CMyNetBuffData> mSendBuffDict = new QuickData.QDList<CMyNetBuffData>();
    private static readonly object mSendBuffLock = new object(); //发送缓冲信息锁

    QuickData.QDList<CMyNetBuffData> mSureBuffDict = new QuickData.QDList<CMyNetBuffData>(); //可靠数据缓冲

    public int mPort = 911;

    public UdpClient udpServer;

    Thread serverLinternThread;

    Thread serverSendThread;


    public void Close()
    {
        try
        {
            serverLinternThread.Abort();
            serverSendThread.Abort();
            udpServer.Close();
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
       
    }

    public void BeginUDPServer()
    {
        uint SIO_UDP_CONNRESET = 2550136844;

        IPEndPoint localIpep = new IPEndPoint(IPAddress.Parse("0.0.0.0"), mPort); // 本机IP和监听端口号
        udpServer = new UdpClient(localIpep);

        udpServer.Client.IOControl((int)SIO_UDP_CONNRESET, new byte[1], null);

        serverLinternThread = new Thread(new ParameterizedThreadStart(UPDServerListenLoop));
        serverLinternThread.Start();
        serverSendThread = new Thread(new ParameterizedThreadStart(UDPSendMsgLoop));
        serverSendThread.Start();
    }

    public void UPDServerListenLoop(object obj)
    {
        while (!gDefine.gNeedQuit)
        {
            try
            {
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = udpServer.Receive(ref remoteEP);      // listen on port 11000\

                lock (mReceiveBuffLock)
                {
                    CMyNetBuffData m = new CMyNetBuffData();
                    m.data = data;
                    m.client = remoteEP;
                    mReceiveBuffDict.Add(m);
                }

            }
            catch(Exception e)
            {
                Console.WriteLine("Exception in UPDServerListenLoop . udp" + e.ToString());
                Close();
                IPEndPoint localIpep = new IPEndPoint(IPAddress.Parse("0.0.0.0"), mPort); // 本机IP和监听端口号
                udpServer = new UdpClient(localIpep);
            }
           
        }
    }

    public void Update()
    {
        int num = mReceiveBuffDict.Count > 50 ? 50 : mReceiveBuffDict.Count;
        for (int i = 0; i < num; i++)
        {
            if (mReceiveBuffDict.Count > 0)
            {
                CMyNetBuffData msg = null;
                lock (mReceiveBuffLock)
                {
                    msg = mReceiveBuffDict.GetFirstAndRemove();
                }

                int msgIndex = BitConverter.ToInt32(msg.data, 4);
                if(msgIndex>0)
                {
                    long playerId = BitConverter.ToInt64(msg.data, 8);
                    PlayerBase p = gDefine.gPlayerBase.Find(playerId);
                    msg = p.mSureMsg.mGetMsg.CheckMsg(msgIndex, msg);
                    if( msg != null )
                        CDyMsgPackManager.DoMsg(msg);
                }
                else
                    CDyMsgPackManager.DoMsg(msg);
            }
            else
                break;
        }

        num = mSureBuffDict.Count > 50 ? 50 : mSureBuffDict.Count;
        for (int i = 0; i < num; i++)
        {
            if (mSureBuffDict.Count > 0)
            {
                CMyNetBuffData msg = null;
                lock (mReceiveBuffLock)
                {
                    msg = mSureBuffDict.GetFirstAndRemove();
                }

                CDyMsgPackManager.DoMsg(msg);
            }
            else
                break;
        }
    }

    public void SendMsg(byte[] data, IPEndPoint client)//发送缓存消息（消息包）
    {
        CMyNetBuffData d = new CMyNetBuffData();
        d.data = data;
        d.client = client;
        lock (mSendBuffLock)
        {
            mSendBuffDict.Add(d);
        }
    }

    void UDPSendMsgLoop(object obj)//更新缓存发送消息区
    {
        while (!gDefine.gNeedQuit)
        {
            if (mSendBuffDict.Count > 0)
            {
                CMyNetBuffData d = null;
                lock (mSendBuffLock)
                {
                    d = mSendBuffDict[0];
                    mSendBuffDict.RemoveAt(0);
                }

                try
                {
                   udpServer.Send(d.data, d.data.Length, d.client);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            else
                System.Threading.Thread.Sleep(1);
        }
    }

    public void AddSureMsgToSureList(CMyNetBuffData sureData)
    {
        mSureBuffDict.Add(sureData);
    }
}
   