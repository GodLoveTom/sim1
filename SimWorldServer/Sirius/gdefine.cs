using System.Xml;
using System.IO;
using System;
using Sirius;

public static class gDefine
{
    public static bool gIsLoading = true;

    public static CTalk gTalkSys = new CTalk();

    public static CMine gMine = new CMine();

    public static CInventManager gInvent = new CInventManager();

    public static CMachineManager gMachine = new CMachineManager();

    //globe random
    public static System.Random gRandom = new System.Random();
    public static HeroPack gHeroPack = new HeroPack();

    public static bool gIsReadyClose = false;
    public static bool gNeedQuit = false; 

    //sys
    public static CGameserver gGameServer = new CGameserver();
    public static CMyNet gNet = new CMyNet();

    //sys base...
    public static LoginBase gLoginBase = new LoginBase();
    public static PlayerManager_Base gPlayerBase = new PlayerManager_Base();

    //位置查询
    public static CPositionChecker gPostionChecker = new CPositionChecker();

    //唯一数据中心
    public static QuickData.CQDSystem gDataSys = new QuickData.CQDSystem();

    public static CIPGuard gIpGuardSys = new CIPGuard();

    public static long gBackUpT = System.DateTime.Now.Ticks;
    public static long gBackUp5T = System.DateTime.Now.Ticks;

    //backup file path
    public static string gBackUpFilePathName;


    public static void Init()
    {
        msgRegister.RegisterMsgPack();
        gDefine.gGameServer.Init();

        string path = System.Environment.CurrentDirectory + "/data/sirius.data";
        gDataSys.Init( path );
        gDataSys.Load();

        //LoadConfig();


        gPlayerBase.Init();
        gPlayerBase.Load();

        gMine.Load();

        gPlayerBase.ReCalcMine();

        gInvent.Load();

        gMachine.Load();

        gPostionChecker.Init();

        gIsLoading = false;

    }

    public static bool IsValuedString( string str)
    {
        char [] c =  str.ToCharArray();

        for (int i=0;i<c.Length; i++)
        {
            if (!char.IsLetterOrDigit(c[i]) && !char.IsWhiteSpace(c[i]) && !char.IsSymbol(c[i]) && !char.IsPunctuation(c[i]))
                return false;
        }

        return true;
    }
    

    
}


