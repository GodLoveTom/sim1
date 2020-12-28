using System;
using System.Threading;
using System.IO;

namespace Sirius
{
    public class main
    {
        static public void CheckInput()
        {
            while (!gDefine.gNeedQuit)
            {
                string str = Console.ReadLine();
                Console.WriteLine(str + "doover");
                CClientInput.DoInput(str);
            }
        } 

        static void Main(string[] args)
        {
           // string fname = System.Environment.CurrentDirectory + "\\refresh\\refresh1.xml";
          // string fname1 = System.Environment.CurrentDirectory + "\\refresh\\refresh"
           //     +System.DateTime.Now.ToString()+".xml";
           // File.Move(@fname, @fname1);


            //begin.
            Console.WriteLine("Welocme baby,SimWorld server begin...... ");

            //载入数据
            gDefine.Init();

            //
            Thread thread1 = new Thread(new ThreadStart(CheckInput));
            thread1.IsBackground = true;//当初始化一个线程，把Thread,IsBackground=true的时候，指示该线程为后台线程，后台线程将会随着主线程的退出而退出
            thread1.Start();

            gDefine.gNet.BeginUDPServer();

            //update
            gDefine.gNeedQuit = false;
            while (!gDefine.gNeedQuit)
            {
                gDefine.gPlayerBase.Update();

                gDefine.gIpGuardSys.Update();

                gDefine.gNet.Update();

                gDefine.gDataSys.UpdateSave();

                if(System.DateTime.Now.Ticks > gDefine.gBackUpT + 36000000000)
                {
                    gDefine.gBackUpT = System.DateTime.Now.Ticks;
                    gDefine.gDataSys.UpdateBack();
                }

                if (System.DateTime.Now.Ticks > gDefine.gBackUp5T + 3000000000)
                {
                    gDefine.gBackUp5T = System.DateTime.Now.Ticks;
                    gDefine.gDataSys.UpdateBack5();
                }

                Thread.Sleep(1);

            }

            //quit

            gDefine.gDataSys.Close();

            Console.WriteLine("data sys close. ");

            gDefine.gNet.Close();

            Console.WriteLine("server. close. ");

            Thread.Sleep(8000);
        }

    }
}
