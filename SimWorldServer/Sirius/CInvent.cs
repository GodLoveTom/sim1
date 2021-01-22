using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickData;

namespace Sirius
{
    /// <summary>
    /// 发明数据
    /// </summary>
    public class CInventData
    {
        /// <summary>
        /// 发明的状态
        /// </summary>
        public enum eState
        {
            Null=0,//什么状态都不是
            Wait, //提交等待审核
            Permit, //通过
            Refuse, //拒绝
        }

        public long mInventId; //发明唯一id
        public long mCrateT;
        public long mInventPcId; //发明者的Id
        public string mName;
        public string mDes;
        public bool mHasAttachFile;
        public string mRelayStr;
        public long mPatentId; //专利Id 0 ：无专利； 1提出专利申请 ； >1000 专利Id 
        public eState mPermitState = eState.Null;
        //设备制造原料
        public double mCMIronNum; 
        public double mCMCopperNum;
        public double mCMGoldNum;
        public double mCMWoodNum;
        public double mCMClayNum;
        public double mCMLimeStoneNum;
        //生产原料
        public double mPMIronNum;
        public double mPMCopperNum;
        public double mPMGoldNum;
        public double mPMWoodNum;
        public double mPMClayNum;
        public double mPMLimeStoneNum;
        //生产出产品类型
        public string mMachineName;
        public CDefine.eResType mOutType;
        //生产出产品数量
        public double mOutNum;
        //生产一次所需花费时间
        public float mProductS;

        CQDDataNode mRefDataNode;

        public void CreateDataNode(CQDDataNode Node)
        {
            mRefDataNode = gDefine.gDataSys.CreateNode(Node, "Invent", LoadFromEditor, ReadyData);
        }

        public void SendMsg(PlayerBase P)
        {
            lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Invent].mLock)
            {
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Invent].mValueArr[0].Set(mName);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Invent].mValueArr[1].Set(mDes);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Invent].mValueArr[2].Set(mInventId);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Invent].mValueArr[3].Set(P.uid);

                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Invent].mValueArr[4].Set(mPatentId);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Invent].mValueArr[5].Set((int)mPermitState);

                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Invent].mValueArr[6].Set(mCMIronNum);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Invent].mValueArr[7].Set(mCMCopperNum);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Invent].mValueArr[8].Set(mCMGoldNum);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Invent].mValueArr[9].Set(mCMWoodNum);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Invent].mValueArr[10].Set(mCMClayNum);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Invent].mValueArr[11].Set(mCMLimeStoneNum);

                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Invent].mValueArr[12].Set(mPMIronNum);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Invent].mValueArr[13].Set(mPMCopperNum);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Invent].mValueArr[14].Set(mPMGoldNum);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Invent].mValueArr[15].Set(mPMWoodNum);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Invent].mValueArr[16].Set(mPMClayNum);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Invent].mValueArr[17].Set(mPMLimeStoneNum);


                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Invent].mValueArr[18].Set((int)mOutType);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Invent].mValueArr[19].Set(mOutNum);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Invent].mValueArr[20].Set(mProductS);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Invent].mValueArr[21].Set(mMachineName);


                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Invent].SendMsg(P.client);

            }


        }

        public void NeedSave()
        {
            gDefine.gDataSys.AddToSaveList(mRefDataNode);
        }

        public void ReadyData(QuickData.CQDDataNode node)
        {
            node.ClearParam();
            node.AddParam("mInventId", mInventId);
            node.AddParam("mCrateT", mCrateT);
            node.AddParam("mInventPcId", mInventPcId);
            node.AddParam("mName", mName);
            node.AddParam("mDes", mDes);
            node.AddParam("mRelayStr", mRelayStr);
            node.AddParam("mPatentId", mPatentId);
            node.AddParam("mPermitState", (int)mPermitState);

            node.AddParam("mCMIronNum", mCMIronNum);
            node.AddParam("mCMCopperNum", mCMCopperNum);
            node.AddParam("mCMGoldNum", mCMGoldNum);
            node.AddParam("mCMWoodNum", mCMWoodNum);
            node.AddParam("mCMClayNum", mCMClayNum);
            node.AddParam("mCMLimeStoneNum", mCMLimeStoneNum);

            node.AddParam("mPMIronNum", mPMIronNum);
            node.AddParam("mPMCopperNum", mPMCopperNum);
            node.AddParam("mPMGoldNum", mPMGoldNum);
            node.AddParam("mPMWoodNum", mPMWoodNum);
            node.AddParam("mPMClayNum", mPMClayNum);
            node.AddParam("mPMLimeStoneNum", mPMLimeStoneNum);



            node.AddParam("mOutType", (int)mOutType);
            node.AddParam("mOutNum", mOutNum);
            node.AddParam("mProductS", mProductS);
            node.AddParam("mMachineName", mMachineName);
        }

        public void LoadData(QuickData.CQDDataNode node)
        {
            List<QuickData.CQDParam> param = node.GetCQParam();
         
            mInventId =  node.GetParamValueLong(0 );
            mCrateT = node.GetParamValueLong(1);
            mInventPcId = node.GetParamValueLong(2);
            mName = node.GetParamValueStr(3);
            mDes = node.GetParamValueStr(4);
            mRelayStr = node.GetParamValueStr(5);
            mPatentId = node.GetParamValueLong(6);
            mPermitState =(eState) node.GetParamValueInt(7);

            mCMIronNum = node.GetParamValueDouble(8);
            mCMCopperNum = node.GetParamValueDouble(9);
            mCMGoldNum = node.GetParamValueDouble(10);
            mCMWoodNum = node.GetParamValueDouble(11);
            mCMClayNum = node.GetParamValueDouble(12);
            mCMLimeStoneNum = node.GetParamValueDouble(13);

            mPMIronNum = node.GetParamValueDouble(14);
            mPMCopperNum = node.GetParamValueDouble(15);
            mPMGoldNum = node.GetParamValueDouble(16);
            mPMWoodNum = node.GetParamValueDouble(17);
            mPMClayNum = node.GetParamValueDouble(18);
            mPMLimeStoneNum = node.GetParamValueDouble(19);

            mOutType =(CDefine.eResType) node.GetParamValueInt(20);
            mOutNum = node.GetParamValueDouble(21);
            mProductS = node.GetParamValueFloat(22);
            mMachineName = node.GetParamValueStr(23);
        }

        public void LoadFromEditor(QuickData.CQDDataNode node)
        {
            LoadData(node);
            NeedSave();
        }



        public void Load(QuickData.CQDDataNode node)
        {
            mRefDataNode = node;
            mRefDataNode.mReadyParamFunc = ReadyData;
            mRefDataNode.mReadParamFunc = LoadFromEditor;

            LoadData(node);
        }

    }

    public class CInventPc
    {
       public List<CInventData> mDict = new List<CInventData>();
        public void Add(CInventData Data)
        {
            mDict.Add(Data);
        }

        public void SendAllDataToPlayer(PlayerBase P)
        {
            if (!P.IsOnLine())
                return;
            foreach (var v in mDict)
                v.SendMsg(P);
        }
    }


    public class CInventManager
    {
        long mId = 1001;
        Dictionary<long, CInventData> mDict = new Dictionary<long, CInventData>();
        Dictionary<long, CInventData> mWaitDict = new Dictionary<long, CInventData>();
        public List<CInventData> mPatentApplyDict = new List<CInventData>(); //专利申请列表

        CQDDataNode mRefDataNode;

      

        public void ReadyData(QuickData.CQDDataNode node)
        {
            node.ClearParam();
            node.AddParam("mId", mId);
        }


        public void Load()
        {
            string[] nodePathName = new string[1] { "Invent" };
            QuickData.CQDNode dataNode = gDefine.gDataSys.GetNode(null, nodePathName);

            if (dataNode == null)
                mRefDataNode = (QuickData.CQDDataNode)gDefine.gDataSys.CreateNode(null, nodePathName);
            else
                mRefDataNode = (QuickData.CQDDataNode)dataNode;

            mId = mRefDataNode.GetParamValueLongByName("mId", 1001);
            mId += 10;
            mRefDataNode.mReadyParamFunc = ReadyData;

            if (mRefDataNode != null)
            {
                mRefDataNode.LoadSelfChild();
                QuickData.CQDDataNode data = mRefDataNode.FirstChild();
                while (data != null)
                {
                    CInventData invent = new CInventData();
                    invent.Load(data);

                    mDict.Add(invent.mInventId, invent);
                    if(invent.mPermitState == CInventData.eState.Wait)
                        mWaitDict.Add(invent.mInventId, invent);
                    if (invent.mPatentId == 1)
                        mPatentApplyDict.Add(invent);

                    PlayerBase p = gDefine.gPlayerBase.Find(invent.mInventPcId);
                    if(p!=null)
                        p.mInvent.Add(invent);

                    data = (QuickData.CQDDataNode)data.refBrother;
                }
            }
        }

        public void NeedSave()
        {
            gDefine.gDataSys.AddToSaveList(mRefDataNode);
        }

        /// <summary>
        /// 提交一个创造发明并等待审批
        /// </summary>
        /// <param name="Name">发明的名字</param>
        /// <param name="Des">发明的简单描述</param>
        /// <param name="P">发明提交提者</param>
        public void CreateInvent( string Name, string Des, PlayerBase P)
        {
            if (P.mMoney >= 10000)
            {
                P.mMoney -= 10000;
                P.NeedSave();

                CInventData invent = new CInventData();
                invent.mInventId = mId++;
                invent.mName = Name;
                invent.mDes = Des;
                invent.mCrateT = System.DateTime.Now.Ticks;
                invent.mInventPcId = P.uid;
                invent.mPermitState = CInventData.eState.Wait;

                invent.CreateDataNode(mRefDataNode);
                invent.NeedSave();

                mDict.Add(invent.mInventId, invent);
                mWaitDict.Add(invent.mInventId, invent);
                P.mInvent.Add(invent);

                invent.SendMsg(P);

                NeedSave();
            }
           
        }

        //
        public void ConfirmInvent(long InventId, bool Ok, string Reason="")
        {
           CInventData invent;
           if( mWaitDict.TryGetValue(InventId, out invent))
            {
                invent.mPermitState = Ok ? CInventData.eState.Permit : CInventData.eState.Refuse;
                invent.mRelayStr = Reason;
            }
        }

        public void SendInventMsg(long InventId, PlayerBase P)
        {
            if (P.IsOnLine())
            {
                CInventData invent;
                if (mDict.TryGetValue(InventId, out invent))
                {
                    invent.SendMsg(P);
                }
            }
        }

        public CInventData Find(long InventId)
        {
            CInventData invent;
            mDict.TryGetValue(InventId, out invent);
            return invent;
        }


    }
}
