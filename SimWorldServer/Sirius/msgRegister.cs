public static class msgRegister
{
    public static void RegisterMsgPack()
    {
        //注册消息
        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_Register, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_Register, CBaseValue.eBaseValue.String);//name
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_Register, CBaseValue.eBaseValue.String);//pass

        //注册成功返回
        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_Register, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_Register, CBaseValue.eBaseValue.Int);//return 1 成功 0 不成功

        //道德
        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_AskDaoDe, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_AskDaoDe, CBaseValue.eBaseValue.Long); //uid

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_AskDaoDe, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_AskDaoDe, CBaseValue.eBaseValue.Double);//daode
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_AskDaoDe, CBaseValue.eBaseValue.Double);//zhili
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_AskDaoDe, CBaseValue.eBaseValue.Double);//tili
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_AskDaoDe, CBaseValue.eBaseValue.Double);//cur tili

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_ChangeDaoDe, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_ChangeDaoDe, CBaseValue.eBaseValue.Long); //uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_ChangeDaoDe, CBaseValue.eBaseValue.Double);//zhili
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_ChangeDaoDe, CBaseValue.eBaseValue.Double);//tili

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_AskRes, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_AskRes, CBaseValue.eBaseValue.Long); //uid

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_AskRes, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_AskRes, CBaseValue.eBaseValue.Double); //iron
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_AskRes, CBaseValue.eBaseValue.Double); //copper
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_AskRes, CBaseValue.eBaseValue.Double); //gold
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_AskRes, CBaseValue.eBaseValue.Double); //apple




        //money更新消息
        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_Money, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_Money, CBaseValue.eBaseValue.Long);//uid

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_Money, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_Money, CBaseValue.eBaseValue.Double);//money

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_BuyDaoDe, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_BuyDaoDe, CBaseValue.eBaseValue.Long);//uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_BuyDaoDe, CBaseValue.eBaseValue.Int);//daode

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_BuyDaoDe, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_BuyDaoDe, CBaseValue.eBaseValue.Int);//buy dao de >0 ok 0 fail
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_BuyDaoDe, CBaseValue.eBaseValue.Double);//money


        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_Eat, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_Eat, CBaseValue.eBaseValue.Long);//uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_Eat, CBaseValue.eBaseValue.Int);//num

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_Eat, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_Eat, CBaseValue.eBaseValue.Double);//food
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_Eat, CBaseValue.eBaseValue.Double);//TiLi

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_OnLineT, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_OnLineT, CBaseValue.eBaseValue.Long);// on line t


        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_AskMine, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_AskMine, CBaseValue.eBaseValue.Long);//uid

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_AskMine, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_AskMine, CBaseValue.eBaseValue.Long);   //mine id (id==-1, sum num)
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_AskMine, CBaseValue.eBaseValue.Int);    // type.
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_AskMine, CBaseValue.eBaseValue.Double); //cur num
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_AskMine, CBaseValue.eBaseValue.Int); //perc

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_ChangeMinePerc, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_ChangeMinePerc, CBaseValue.eBaseValue.Long);//uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_ChangeMinePerc, CBaseValue.eBaseValue.Long);//mine id
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_ChangeMinePerc, CBaseValue.eBaseValue.Int);//perc

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_ChangeMinePerc, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_ChangeMinePerc, CBaseValue.eBaseValue.Long);//mine id
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_ChangeMinePerc, CBaseValue.eBaseValue.Int);//perc

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_BeginDig, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_BeginDig, CBaseValue.eBaseValue.Long);//uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_BeginDig, CBaseValue.eBaseValue.Long);//mine id

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_AskDig, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_AskDig, CBaseValue.eBaseValue.Long);//uid

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_Dig, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_Dig, CBaseValue.eBaseValue.Long);//mine uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_Dig, CBaseValue.eBaseValue.Long);//mine owner uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_Dig, CBaseValue.eBaseValue.String);//owner name
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_Dig, CBaseValue.eBaseValue.Long);//dig t
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_Dig, CBaseValue.eBaseValue.Double);//num
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_Dig, CBaseValue.eBaseValue.Int);//type
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_Dig, CBaseValue.eBaseValue.Int);//perc


        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_ChangeNickName, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_ChangeNickName, CBaseValue.eBaseValue.Long);//uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_ChangeNickName, CBaseValue.eBaseValue.String);//nick name


        //邮箱是否存在
        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_IsMailExist, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_IsMailExist, CBaseValue.eBaseValue.String);//mail

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_IsMailExist, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_IsMailExist, CBaseValue.eBaseValue.Bool);//true 存在 false 不存在



        //main data
        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_MainData, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_MainData, CBaseValue.eBaseValue.Float);//money
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_MainData, CBaseValue.eBaseValue.Long);//server time

        //检查name，psw
        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_IsNamePassOK, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_IsNamePassOK, CBaseValue.eBaseValue.String);//name
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_IsNamePassOK, CBaseValue.eBaseValue.String);//pass

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_IsNamePassOK, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_IsNamePassOK, CBaseValue.eBaseValue.Byte);//result. 0 ok, 1 name error ,2 pass error

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_ServerT, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_ServerT, CBaseValue.eBaseValue.Long);//uid

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_ServerT, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_ServerT, CBaseValue.eBaseValue.Long);//server t

        //花钱
        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_SpendMoney, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_SpendMoney, CBaseValue.eBaseValue.Long);////self uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_SpendMoney, CBaseValue.eBaseValue.Double);//spend money
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_SpendMoney, CBaseValue.eBaseValue.Int);////reason

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_SpendMoney, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_SpendMoney, CBaseValue.eBaseValue.Double);// spend money
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_SpendMoney, CBaseValue.eBaseValue.Double);// now money
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_SpendMoney, CBaseValue.eBaseValue.Int);////reason


        //确认数据
        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_SureMsg, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_SureMsg, CBaseValue.eBaseValue.Long);////self uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_SureMsg, CBaseValue.eBaseValue.Int); //msgIndex

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_ReAskSureMsg, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_ReAskSureMsg, CBaseValue.eBaseValue.Long);////self uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_ReAskSureMsg, CBaseValue.eBaseValue.Int);//msgIndex


        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_SearchMine, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_SearchMine, CBaseValue.eBaseValue.Long);////self uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_SearchMine, CBaseValue.eBaseValue.Int); //0 new, 1 exist.
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_SearchMine, CBaseValue.eBaseValue.Int); //0 mine, 1 apple.

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_SearchMine, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_SearchMine, CBaseValue.eBaseValue.Long);   //mine id (id==-1, sum num)
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_SearchMine, CBaseValue.eBaseValue.Long);    // own uid.
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_SearchMine, CBaseValue.eBaseValue.String);    // own nickname.
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_SearchMine, CBaseValue.eBaseValue.Int);    // type.
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_SearchMine, CBaseValue.eBaseValue.Double); //cur num
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_SearchMine, CBaseValue.eBaseValue.Int); //perc
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_SearchMine, CBaseValue.eBaseValue.Byte); //index
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_SearchMine, CBaseValue.eBaseValue.Byte); //search max num.

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_SearchMan, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_SearchMan, CBaseValue.eBaseValue.Long);////self uid

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_SearchMan, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_SearchMan, CBaseValue.eBaseValue.Long);   //uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_SearchMan, CBaseValue.eBaseValue.String); // own nickname.
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_SearchMan, CBaseValue.eBaseValue.Int);    // daode.
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_SearchMan, CBaseValue.eBaseValue.Byte);   //index
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_SearchMan, CBaseValue.eBaseValue.Byte);   //search max num.

        //登录消息
        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_Login, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_Login, CBaseValue.eBaseValue.String);//name
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_Login, CBaseValue.eBaseValue.String);//psw
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_Login, CBaseValue.eBaseValue.Byte);//0 game account ; 1 store account


        //登录成功返回消息
        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_DATAOK, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_DATAOK, CBaseValue.eBaseValue.Long);//uid

        //206
        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_LoginErr, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_LoginErr, CBaseValue.eBaseValue.Int);//err msg

        //
        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_Talk, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_Talk, CBaseValue.eBaseValue.Long);//uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_Talk, CBaseValue.eBaseValue.Int);//index


        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_Talk, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_Talk, CBaseValue.eBaseValue.Long);//talker uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_Talk, CBaseValue.eBaseValue.String);//str name
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_Talk, CBaseValue.eBaseValue.String);//str talk
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_Talk, CBaseValue.eBaseValue.Int);//talk type
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_Talk, CBaseValue.eBaseValue.Int);//talk index;


        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_Rob, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_Rob, CBaseValue.eBaseValue.Long);////self uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_Rob, CBaseValue.eBaseValue.Long);////rob somebody uid

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_Rob, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_Rob, CBaseValue.eBaseValue.Int);   //rob things type
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_Rob, CBaseValue.eBaseValue.Int);    //num
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_Rob, CBaseValue.eBaseValue.Int);   //index
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_Rob, CBaseValue.eBaseValue.Int); //max num.

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_Steal, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_Steal, CBaseValue.eBaseValue.Long);////self uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_Steal, CBaseValue.eBaseValue.Long);////rob somebody uid

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_Steal, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_Steal, CBaseValue.eBaseValue.Int);   //rob things type
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_Steal, CBaseValue.eBaseValue.Int);    //num
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_Steal, CBaseValue.eBaseValue.Int);   //index
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_Steal, CBaseValue.eBaseValue.Int); //max num.

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_TrideByNickName, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_TrideByNickName, CBaseValue.eBaseValue.Long);//self uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_TrideByNickName, CBaseValue.eBaseValue.String);//

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_TrideByNickName, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_TrideByNickName, CBaseValue.eBaseValue.Long);   //uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_TrideByNickName, CBaseValue.eBaseValue.String);    //nick name
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_TrideByNickName, CBaseValue.eBaseValue.Byte);    //index
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_TrideByNickName, CBaseValue.eBaseValue.Byte);    //maxNum

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_TrideItem, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_TrideItem, CBaseValue.eBaseValue.Long);//self uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_TrideItem, CBaseValue.eBaseValue.Double);//iron
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_TrideItem, CBaseValue.eBaseValue.Double);//cobber
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_TrideItem, CBaseValue.eBaseValue.Double);//gold
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_TrideItem, CBaseValue.eBaseValue.Double);//apple
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_TrideItem, CBaseValue.eBaseValue.Double);//money

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_TrideItem, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_TrideItem, CBaseValue.eBaseValue.Long);   //uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_TrideItem, CBaseValue.eBaseValue.Double);//iron
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_TrideItem, CBaseValue.eBaseValue.Double);//cobber
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_TrideItem, CBaseValue.eBaseValue.Double);//gold
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_TrideItem, CBaseValue.eBaseValue.Double);//apple
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_TrideItem, CBaseValue.eBaseValue.Double);//money

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_TrideLock, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_TrideLock, CBaseValue.eBaseValue.Long);//self uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_TrideLock, CBaseValue.eBaseValue.Int);//0 unlock ,1 luck

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_TrideLock, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_TrideLock, CBaseValue.eBaseValue.Long);   //uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_TrideLock, CBaseValue.eBaseValue.Int);   //0 unlock ,1 luck

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_TrideConfim, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_TrideConfim, CBaseValue.eBaseValue.Long);//self uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_TrideConfim, CBaseValue.eBaseValue.Int);//0 unconfim ,1 confim

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_TrideConfim, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_TrideConfim, CBaseValue.eBaseValue.Long);   //uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_TrideConfim, CBaseValue.eBaseValue.Int);   //0 unconfim ,1 confim

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_TrideBegin, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_TrideBegin, CBaseValue.eBaseValue.Long);//self uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_TrideBegin, CBaseValue.eBaseValue.Long);//uid

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_TrideBegin, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_TrideBegin, CBaseValue.eBaseValue.Long);   //uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_TrideBegin, CBaseValue.eBaseValue.String);   //nick name

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_TrideAgree, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_TrideAgree, CBaseValue.eBaseValue.Long);//self uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_TrideAgree, CBaseValue.eBaseValue.Long);//uid

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_TrideAgree, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_TrideAgree, CBaseValue.eBaseValue.Long);   //uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_TrideAgree, CBaseValue.eBaseValue.Long);   //other uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_TrideAgree, CBaseValue.eBaseValue.String);   //nick name
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_TrideAgree, CBaseValue.eBaseValue.String);   //other  nick name

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_TrideFinish, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_TrideFinish, CBaseValue.eBaseValue.Double);//iron
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_TrideFinish, CBaseValue.eBaseValue.Double);//cobber
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_TrideFinish, CBaseValue.eBaseValue.Double);//gold
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_TrideFinish, CBaseValue.eBaseValue.Double);//apple
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_TrideFinish, CBaseValue.eBaseValue.Double);//money

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_TrideEnd, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_TrideEnd, CBaseValue.eBaseValue.Long);   //uid

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_TrideEnd, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_TrideEnd, CBaseValue.eBaseValue.Long);   //uid

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_SearchMineFail, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_SearchMineFail, CBaseValue.eBaseValue.Int);   //0 没有空间 1 没有搜索到

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_SearchT, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_SearchT, CBaseValue.eBaseValue.Long);  //uid

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_SearchT, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_SearchT, CBaseValue.eBaseValue.Long);  //


        //
        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_Food, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_Food, CBaseValue.eBaseValue.Double);//food

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_NickName, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_NickName, CBaseValue.eBaseValue.String);//nick name

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_SC_ChangeNickName, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_SC_ChangeNickName, CBaseValue.eBaseValue.Int);//0 ok, 1 fail 

        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_TalkSendStr, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_TalkSendStr, CBaseValue.eBaseValue.Long);//uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_TalkSendStr, CBaseValue.eBaseValue.String);//str


        CDyMsgPackManager.RegisterClearMsg(HeroPack.def_CS_SearchMineCoin, false);
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_SearchMineCoin, CBaseValue.eBaseValue.Long);////self uid
        CDyMsgPackManager.RegisterMsgPackValue(HeroPack.def_CS_SearchMineCoin, CBaseValue.eBaseValue.Int); //0 mine, 1 apple.





        //=============================================================================================================//





    }
}


