//基于某种规则的对连入ip及其端口进行管控

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//连接控制数据
public class CIPPortGuardData
{
    public int port;
    public int connectNum; //连接次数
    public long lastT;
    public long forbidT = 0;
}

//连接控制数据
public class CIPGuardData
{
    public string Ip;
    public long  lastt;     
    public int connectNum;  //连接次数
    public List<CIPPortGuardData> mPortData = new List<CIPPortGuardData>();
    public long forbidT = 0; //

    public CIPPortGuardData Find(int port)
    {
        for (int i = 0; i < mPortData.Count; i++)
            if (mPortData[i].port == port)
                return mPortData[i];

        return null;
    }
}

//连接监视
public class CIPGuard
{
    public Dictionary<string, CIPGuardData> mData = new Dictionary<string, CIPGuardData>();
    public long lastRefreshT = System.DateTime.Now.Ticks;

    public void Connect(string ip, int port)
    {
        //建立数据监控
        CIPGuardData data;
        if (!mData.TryGetValue(ip, out data))
        {
            //create ip.
            data = new CIPGuardData();
            data.Ip = ip;
            mData.Add(ip, data);
            data.lastt = System.DateTime.Now.Ticks;
           
        }
        data.connectNum++;

        CIPPortGuardData person = data.Find(port);
        if (person == null)
        {
            //add person.
            person = new CIPPortGuardData();
            person.port = port;
            data.mPortData.Add(person);
            
            person.lastT = System.DateTime.Now.Ticks;
            
        }
        person.connectNum++;

        //查验数据监控ip
        int s = (int) ((System.DateTime.Now.Ticks - data.lastt) / (long)10000000);
        if ( s >= 1 )
        {
            int num = data.connectNum / data.mPortData.Count / s;
            data.lastt = System.DateTime.Now.Ticks;
            if ( num > 60 )
            {
                data.forbidT = System.DateTime.Now.Ticks + (long)600 * (long)10000000;
                return;
            }
            data.connectNum = 0;
        }
        
        //端口数据查封
        if( data.mPortData.Count>1)
        {
            for( int i=0; i<data.mPortData.Count; i++ )
            {
                CIPPortGuardData persondata = data.mPortData[i];
                s = (int)((System.DateTime.Now.Ticks - persondata.lastT) / (long)10000000);
                if (s >= 1)
                {
                    persondata.lastT = System.DateTime.Now.Ticks;
                    if ( persondata.connectNum / s > 150 )
                    {
                        persondata.forbidT = System.DateTime.Now.Ticks + (long)600 * (long)10000000;
                        return;
                    }
                    persondata.connectNum = 0;
                }
            }
        }
    }


    public bool IsForbid(string ip, int port)
    {
        CIPGuardData data;
        if (mData.TryGetValue(ip, out data))
        {
            if ( data.forbidT > System.DateTime.Now.Ticks )
                return true;
            
            CIPPortGuardData person = data.Find(port);
            if (person != null && person.forbidT > System.DateTime.Now.Ticks)
                return true;
        }
        return false;
    }

    //强行设置某个ip是否被禁止连接
    public void BanIP(string ip, long forbidT)
    {
        CIPGuardData data;
        if (mData.TryGetValue(ip, out data))
        {
            data.forbidT = forbidT;
        }
    }

    public void Update()
    {
        if (System.DateTime.Now.Ticks > lastRefreshT + (long)21 * (long)10000000)
        {
            lastRefreshT = System.DateTime.Now.Ticks;

            //每隔一段时间，查验所有连接数据。 将不在线的数据清退(20s 没有连接，就退出记录)
            List<string> keyTmp = new List<string>();
            foreach (CIPGuardData data in mData.Values)
            {
                if (System.DateTime.Now.Ticks > data.lastt + (long)20 * (long)10000000)
                    keyTmp.Add(data.Ip);
                else
                {
                    if (data.mPortData.Count > 1)
                    {
                        for (int i = 0; i < data.mPortData.Count; i++)
                        {
                            CIPPortGuardData person = data.mPortData[i];
                            if (System.DateTime.Now.Ticks > person.lastT + (long)20 * (long)10000000)
                            {
                                data.mPortData.RemoveAt(i);
                                i--;
                            }
                        }
                    }
                }
            }
        }
    }
}

