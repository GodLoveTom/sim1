using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Threading;

//客户端输入处理类
public static class CClientInput
{
    public static void DoInput( string str)
    {
        if (!string.IsNullOrEmpty(str))
        {
            if (string.Compare("quit", str) == 0)
            {
                gDefine.gNeedQuit = true;
            }
            else if (string.Compare("open", str) == 0)
            {
                CMyWindow m = new CMyWindow();
                m.BeginMyThread();

            }
            else if (string.Compare("map", str) == 0)
            {
                CMyWindow m = new CMyWindow();
                m.mOpenMap = true;
                m.BeginMyThread();

            }
            else if (string.Compare("add", str) == 0)
            {
                string name = "bingfeng";
                string password = "12345678";
                string mail = "bingfeng@sina.xom";

                int error = 0;

                PlayerBase p = gDefine.gLoginBase.Register(name, password, out error);
                p = gDefine.gLoginBase.LoginIn(name, password, ref error);

            }
            else if (string.Compare("perc", str) == 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    long pp = gDefine.gRandom.Next(0, 10000000);
                    double value = gDefine.gMine.CalcSearchPrec(10, 10);
                    Console.WriteLine("value:" + value.ToString() + ";" + ((long)(value * 10000000)).ToString() + ";" + pp.ToString()
                        + "??" + (pp < value * 10000000 ? true : false).ToString());
                }

            }
            else if (string.Compare("refresh", str) == 0)
            {
                gDefine.gPlayerBase.Refresh();
            }
            else if(str.Length > 7 &&  str.Substring(0,5)=="name ")
            {
                string name = str.Substring(5);
                PlayerBase p = gDefine.gPlayerBase.Find(name);
                if (p != null)
                {
                    Console.WriteLine("Id:" + p.uid.ToString());
                }


            }
            else if (str.Length > 7 && str.Substring(0, 5) == "nick ")
            {
                string nick = str.Substring(5);
                PlayerBase p = gDefine.gPlayerBase.FindByNickName(nick);
                if(p!=null)
                {
                    Console.WriteLine("Id:" + p.uid.ToString());
                }


            }

        }

    }


}

public class CMyWindow
{
    QuickData.CQDSystem mRefSys;
    public bool mOpenMap = false;

    public void BeginMyThread() => BeginMyThread(null);
    public void BeginMyThread(QuickData.CQDSystem sys)
    {
        mRefSys = sys;

        Thread buffmsgThread = new Thread(new ThreadStart(WinFormThread));

        buffmsgThread.SetApartmentState(ApartmentState.STA);

        buffmsgThread.Start();
    }

    public void WinFormThread()
    {
        specialStr.QDDataViewForm form = new specialStr.QDDataViewForm();
        form.AddRefData(gDefine.gDataSys);
        form.InitTree();
        form.ShowDialog();
    }

    public void Run()
    {

    }

}



