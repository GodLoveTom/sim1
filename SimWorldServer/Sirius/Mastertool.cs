/////------------------------------------------
/////------------------------------------------
//using System;
//using System.Text;
//using System.Net.Sockets;
//using System.Threading;
//using System.Collections.Generic;
//using System.Web.Script.Serialization;
//using System.Xml;
//using System.IO;

//class RequestMethod
//{
//    /// <summary>
//    /// 方法签名 用于判断请求是否合法
//    /// </summary>
//    public const string REQUEST_SING = "CHECK_ACCOUNT";
//}


//class DataEntity
//{
//    //签名验证
//    private String requestSign;
//    //调用方法
//    private String requestMethod;
   
//    //
//    public string RequestSign { get => requestSign; set => requestSign = value; }
//    public string RequestMethod { get => requestMethod; set => requestMethod = value; }
    
//    /// <summary>
//    /// U币幸运抽奖信息
//    /// </summary>
//   // internal PlayerULuckyDraw PlayerULuckyDraw { get; set; }    //  { get => playerUBLucky; set => playerUBLucky = value; }
   
//}


//class JSONUtil
//{
//    /// <summary>
//    /// JSON 格式字符串拼接
//    /// 测试环境下使用，开发环境下需根据不同业务类型更改参数值
//    /// </summary>
//    /// <param name="dataEntity"></param>
//    /// <returns></returns>
//    public static string GetJSONString(DataEntity dataEntity)
//    {
//        StringBuilder stringBuilder = new StringBuilder();
//        stringBuilder.Append("{ \"requestMethod\": \"" + dataEntity.RequestMethod + "\",");
//        stringBuilder.Append("\"requestSign\":\"" + dataEntity.RequestSign + "\"");
//        if (dataEntity.Data != null || dataEntity.Data0 != null || dataEntity.Data1 != null
//        || dataEntity.Data2 != null || dataEntity.Data3 != null || dataEntity.AgentHistoryData != null || dataEntity.AgentData != null
//        || dataEntity.AgentMemberData != null || dataEntity.Applay != null || dataEntity.AgentBrorrow != null
//        || dataEntity.AgentMemberBrorrow != null || dataEntity.DrawMoenyApply != null || dataEntity.BankDepositApply != null
//        || dataEntity.SportBettingApply != null || dataEntity.SportBettingEndApply != null || dataEntity.LotteryBetting != null
//        || dataEntity.LotteryBettingEnd != null || dataEntity.SportBettingCq != null || dataEntity.SportBettingCqList != null
//        || dataEntity.MBPurse != null || dataEntity.UBPurse != null || dataEntity.RBPurse != null || dataEntity.Keno != null
//        || dataEntity.LuckyFarm != null || dataEntity.SubPurse != null || dataEntity.SignInRecord != null || dataEntity.BankCardHistory != null
//        || dataEntity.ExpRecord != null || dataEntity.VIPExpRecord != null || dataEntity.Medal != null || dataEntity.TaskRecord != null
//        || dataEntity.ProfitLoss != null || dataEntity.AgentCash != null || dataEntity.AgentApply != null || dataEntity.InterestPurchase != null
//        || dataEntity.UMConvert != null || dataEntity.InterestCash != null || dataEntity.RechargeApply != null || dataEntity.RefereeCash != null
//        || dataEntity.Prop != null || dataEntity.PropHistory != null || dataEntity.DBPurse != null || dataEntity.PlayerULuckyDraw != null || dataEntity.PlayerMLuckyDraw != null
//        || dataEntity.DrawMoenyApply != null || dataEntity.PlayerWaterBills != null || dataEntity.AgentPurse != null
//        || dataEntity.CommissionPurse != null || dataEntity.AgentGuarantee != null || dataEntity.AgentMemberWaterbillsRecord != null
//        || dataEntity.MasterAgentMemberBorrowRecord != null || dataEntity.AgentRemoveMemberApply != null || dataEntity.MasterRemoveAgentMember != null
//        || dataEntity.Tiger666History != null || dataEntity.SysEmailAdd != null || dataEntity.MobileRechargeApply != null
//       //
//       )
//        {
//            stringBuilder.Append(",\"data\":");
//            JavaScriptSerializer json = new JavaScriptSerializer();
//            if (dataEntity.Data != null)
//                json.Serialize(dataEntity.Data, stringBuilder);
//            else if (dataEntity.Data0 != null)
//                json.Serialize(dataEntity.Data0, stringBuilder);
//            else if (dataEntity.Data1 != null)
//                json.Serialize(dataEntity.Data1, stringBuilder);
//            else if (dataEntity.Data2 != null)
//                json.Serialize(dataEntity.Data2, stringBuilder);
//            else if (dataEntity.Data3 != null)
//                json.Serialize(dataEntity.Data3, stringBuilder);
//            else if (dataEntity.AgentHistoryData != null)
//                json.Serialize(dataEntity.AgentHistoryData, stringBuilder);
//            else if (dataEntity.AgentData != null)
//                json.Serialize(dataEntity.AgentData, stringBuilder);
//            else if (dataEntity.AgentMemberData != null)
//                json.Serialize(dataEntity.AgentMemberData, stringBuilder);
//            else if (dataEntity.Applay != null)
//                json.Serialize(dataEntity.Applay, stringBuilder);
//            else if (dataEntity.AgentBrorrow != null)
//                json.Serialize(dataEntity.AgentBrorrow, stringBuilder);
//            else if (dataEntity.AgentMemberBrorrow != null)
//                json.Serialize(dataEntity.AgentMemberBrorrow, stringBuilder);
//            else if (dataEntity.DrawMoenyApply != null)
//                json.Serialize(dataEntity.DrawMoenyApply, stringBuilder);
//            else if (dataEntity.BankDepositApply != null)
//                json.Serialize(dataEntity.BankDepositApply, stringBuilder);
//            else if (dataEntity.SportBettingApply != null)
//                json.Serialize(dataEntity.SportBettingApply, stringBuilder);
//            else if (dataEntity.SportBettingEndApply != null)
//                json.Serialize(dataEntity.SportBettingEndApply, stringBuilder);
//            else if (dataEntity.LotteryBetting != null)
//                json.Serialize(dataEntity.LotteryBetting, stringBuilder);
//            else if (dataEntity.LotteryBettingEnd != null)
//                json.Serialize(dataEntity.LotteryBettingEnd, stringBuilder);
//            else if (dataEntity.SportBettingCq != null)
//                json.Serialize(dataEntity.SportBettingCq, stringBuilder);
//            else if (dataEntity.SportBettingCqList != null)
//                json.Serialize(dataEntity.SportBettingCqList, stringBuilder);
//            else if (dataEntity.MBPurse != null)
//                json.Serialize(dataEntity.MBPurse, stringBuilder);
//            else if (dataEntity.UBPurse != null)
//                json.Serialize(dataEntity.UBPurse, stringBuilder);
//            else if (dataEntity.RBPurse != null)
//                json.Serialize(dataEntity.RBPurse, stringBuilder);
//            else if (dataEntity.Keno != null)
//                json.Serialize(dataEntity.Keno, stringBuilder);
//            else if (dataEntity.LuckyFarm != null)
//                json.Serialize(dataEntity.LuckyFarm, stringBuilder);
//            else if (dataEntity.SubPurse != null)
//                json.Serialize(dataEntity.SubPurse, stringBuilder);
//            else if (dataEntity.BankCardHistory != null)
//                json.Serialize(dataEntity.BankCardHistory, stringBuilder);
//            else if (dataEntity.SignInRecord != null)
//                json.Serialize(dataEntity.SignInRecord, stringBuilder);
//            else if (dataEntity.ExpRecord != null)
//                json.Serialize(dataEntity.ExpRecord, stringBuilder);
//            else if (dataEntity.VIPExpRecord != null)
//                json.Serialize(dataEntity.VIPExpRecord, stringBuilder);
//            else if (dataEntity.Medal != null)
//                json.Serialize(dataEntity.Medal, stringBuilder);
//            else if (dataEntity.TaskRecord != null)
//                json.Serialize(dataEntity.TaskRecord, stringBuilder);
//            else if (dataEntity.ProfitLoss != null)
//                json.Serialize(dataEntity.ProfitLoss, stringBuilder);
//            else if (dataEntity.AgentCash != null)
//                json.Serialize(dataEntity.AgentCash, stringBuilder);
//            else if (dataEntity.AgentApply != null)
//                json.Serialize(dataEntity.AgentApply, stringBuilder);
//            else if (dataEntity.InterestPurchase != null)
//                json.Serialize(dataEntity.InterestPurchase, stringBuilder);
//            else if (dataEntity.UMConvert != null)
//                json.Serialize(dataEntity.UMConvert, stringBuilder);
//            else if (dataEntity.InterestCash != null)
//                json.Serialize(dataEntity.InterestCash, stringBuilder);
//            else if (dataEntity.RechargeApply != null)
//                json.Serialize(dataEntity.RechargeApply, stringBuilder);
//            else if (dataEntity.RefereeCash != null)
//                json.Serialize(dataEntity.RefereeCash, stringBuilder);
//            else if (dataEntity.Prop != null)
//                json.Serialize(dataEntity.Prop, stringBuilder);
//            else if (dataEntity.PropHistory != null)
//                json.Serialize(dataEntity.PropHistory, stringBuilder);
//            else if (dataEntity.DBPurse != null)
//                json.Serialize(dataEntity.DBPurse, stringBuilder);
//            else if (dataEntity.PlayerULuckyDraw != null)   //  幸运抽奖信息  U币
//                json.Serialize(dataEntity.PlayerULuckyDraw, stringBuilder);
//            else if (dataEntity.PlayerMLuckyDraw != null)   //  幸运抽奖信息  M币
//                json.Serialize(dataEntity.PlayerMLuckyDraw, stringBuilder);
//            else if (dataEntity.DrawMoenyApply != null)
//                json.Serialize(dataEntity.DrawMoenyApply, stringBuilder);
//            else if (dataEntity.PlayerWaterBills != null)
//                json.Serialize(dataEntity.PlayerWaterBills, stringBuilder);
//            else if (dataEntity.AgentPurse != null)
//                json.Serialize(dataEntity.AgentPurse, stringBuilder);
//            else if (dataEntity.CommissionPurse != null)
//                json.Serialize(dataEntity.CommissionPurse, stringBuilder);
//            else if (dataEntity.AgentGuarantee != null)
//                json.Serialize(dataEntity.AgentGuarantee, stringBuilder);
//            else if (dataEntity.AgentMemberWaterbillsRecord != null)
//                json.Serialize(dataEntity.AgentMemberWaterbillsRecord, stringBuilder);
//            else if (dataEntity.MasterAgentMemberBorrowRecord != null)
//                json.Serialize(dataEntity.MasterAgentMemberBorrowRecord, stringBuilder);
//            else if (dataEntity.AgentRemoveMemberApply != null)
//                json.Serialize(dataEntity.AgentRemoveMemberApply, stringBuilder);
//            else if (dataEntity.MasterRemoveAgentMember != null)
//                json.Serialize(dataEntity.MasterRemoveAgentMember, stringBuilder);
//            else if (dataEntity.Tiger666History != null)
//                json.Serialize(dataEntity.Tiger666History, stringBuilder);
//            else if (dataEntity.SysEmailAdd != null)
//                json.Serialize(dataEntity.SysEmailAdd, stringBuilder);
//            else if (dataEntity.MobileRechargeApply != null)
//                json.Serialize(dataEntity.MobileRechargeApply, stringBuilder);
//        }
//        stringBuilder.Append("}");
//        return stringBuilder.ToString();
//    }
//}

