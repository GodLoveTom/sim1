public class HeroPack
{
    //server
    public const int def_CS_Register = 100;
    public const int def_CS_Login = 101;
    public const int def_CS_AskMainData = 102;

    public const int def_SC_Register = 103;

    public const int def_CS_AskDaoDe = 104;
    public const int def_SC_AskDaoDe = 105;
    public const int def_CS_ChangeDaoDe = 106;

    //查询资源
    public const int def_CS_AskRes = 107;
    public const int def_SC_AskRes = 108;

    public const int def_CS_Money = 109;
    public const int def_SC_Money = 110;

    public const int def_CS_BuyDaoDe = 111;
    public const int def_SC_BuyDaoDe = 112;

    public const int def_CS_Eat = 113;
    public const int def_SC_Eat = 114;

    public const int def_SC_OnLineT = 115;

    public const int def_CS_AskMine = 116;
    public const int def_SC_AskMine = 117;

    public const int def_CS_ChangeMinePerc = 118;
    public const int def_SC_ChangeMinePerc = 119;

    public const int def_CS_BeginDig = 120;
    public const int def_CS_AskDig = 121;
    public const int def_SC_Dig = 122;

    public const int def_CS_Mail = 124;
    public const int def_CS_Ver = 133;

    public const int def_CS_HeartJump = 136;
    public const int def_CS_ChangeNickName = 137;

    public const int def_CS_IsMailExist = 150;
    public const int def_SC_IsMailExist = 151;

    public const int def_CS_IsNamePassOK = 152;
    public const int def_SC_IsNamePassOK = 153;

    public const int def_CS_ServerT = 195; //获得服务器时间
    public const int def_SC_ServerT = 196; //服务器时间返回

    public const int def_CS_SpendMoney = 197; //spend money
    public const int def_SC_SpendMoney = 198; //spend money reback

    public const int def_CS_SureMsg = 199; //确认数据
    public const int def_CS_ReAskSureMsg = 200; //请求丢失数据


    public const int def_CS_SearchMine = 201; //
    public const int def_SC_SearchMine = 202; //

    public const int def_CS_SearchMan = 203; //
    public const int def_SC_SearchMan = 204; //



    //======================================================//
    //client

    public const int def_SC_Busy = 205;
    public const int def_SC_LoginErr = 206;
    public const int def_SC_WAITDATA = 207;
    public const int def_SC_DATAOK = 208;
    public const int def_SC_MainData = 209;
    //=========================================================
    public const int def_SC_Talk = 210;
    public const int def_CS_Talk = 211;
    //---------------------------------------------------------


    public const int def_CS_Rob = 212; //
    public const int def_SC_Rob = 213; //

    public const int def_CS_Steal = 214; //
    public const int def_SC_Steal = 215; //

    public const int def_CS_TrideByNickName = 216; //
    public const int def_SC_TrideByNickName = 217; //

    public const int def_CS_TrideItem = 218; //
    public const int def_SC_TrideItem = 219; //

    public const int def_CS_TrideLock = 220; //
    public const int def_SC_TrideLock = 221; //

    public const int def_CS_TrideConfim = 222; //
    public const int def_SC_TrideConfim = 223; //

    public const int def_CS_TrideBegin = 224; //
    public const int def_SC_TrideBegin = 225; //

    public const int def_CS_TrideAgree = 226; //
    public const int def_SC_TrideAgree = 227; //

    public const int def_SC_TrideFinish = 228; //

    public const int def_CS_TrideEnd = 229; //
    public const int def_SC_TrideEnd = 230; //

    public const int def_SC_SearchMineFail = 231; //

    public const int def_CS_SearchT = 232; //
    public const int def_SC_SearchT = 233; //

    public const int def_SC_Food = 246;
    public const int def_SC_NickName = 250;
    public const int def_SC_ChangeNickName = 251;

    public const int def_CS_TalkSendStr = 253;
    //探索-金币探索
    public const int def_CS_SearchMineCoin = 254; //

    //开始砍伐木材
    public const int def_CS_BeginLogging = 255; //开始伐木
    public const int def_CS_Logging = 256;//查询伐木状态
    public const int def_SC_Logging = 257; //返回伐木状态

    //创造发明
    public const int def_CS_BeginInvent = 258; //提交一个创造发明
    public const int def_CS_Invent = 259; //请求创造发明数据
    public const int def_SC_Invent = 260; //创造发明数据

    //专利
    public const int def_CS_PatentApply = 261; //开始申请专利

    //制造机器
    public const int def_CS_MadeMachineByInvent = 262; //制造机器
    public const int def_SC_MadeMachineByInvent = 263; //制造机器返回
    public const int def_CS_Machine = 264; //申请玩家机器数据
    public const int def_SC_Machine = 265; //返回玩家机器数据

    //机器生产
    public const int def_CS_ProduceByMachine = 266; //开始生产


}