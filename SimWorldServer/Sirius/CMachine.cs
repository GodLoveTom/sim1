using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickData;


namespace Sirius
{
    public class CMachine
    {
        public long mId; //机器唯一id
        public long mOwnerId;
        public long mInventId; //对应的蓝图Id
        public bool mIsProductting = false; //当前是否生产中
        public long mProductT; //生产计时
        public int mDuraMax; //使用次数
        public CInventData mRefInvent;
        public PlayerBase mOwner;


        CQDDataNode mRefDataNode;

        public void CreateDataNode(CQDDataNode Node)
        {
            mRefDataNode = gDefine.gDataSys.CreateNode(Node, "machineNode", LoadFromEditor, ReadyData);
        }

        public void SendMsg( PlayerBase P, bool Isnew =false)
        {
            lock (CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Machine].mLock)
            {
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Machine].mValueArr[0].Set(mId);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Machine].mValueArr[1].Set(mInventId);
                if (Isnew)
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Machine].mValueArr[2].Set((int)2);
                else
                    CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Machine].mValueArr[2].Set(mIsProductting?(int)1:(int)0);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Machine].mValueArr[3].Set(mRefInvent.mMachineName);
                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Machine].mValueArr[4].Set(mDuraMax);

                CDyMsgPackManager.msgTempleArr[HeroPack.def_SC_Machine].SendMsg(P.client);

            }
        }

        public void UpdateProduce(PlayerBase P)
        {
            long t = System.DateTime.Now.Ticks - mProductT;
            if ( t > mRefInvent.mProductS * (long)60 * (long)10000000 ) 
            {
                mProductT = System.DateTime.Now.Ticks;

                if (P.mIron >= mRefInvent.mPMIronNum && P.mCopper >= mRefInvent.mPMCopperNum
                    && P.mGold >= mRefInvent.mPMGoldNum && P.mWood >= mRefInvent.mPMWoodNum
                     && P.mClay >= mRefInvent.mPMClayNum && P.mLimestone >= mRefInvent.mPMLimeStoneNum
                     )
                {
                    P.mIron -= mRefInvent.mPMIronNum;
                    P.mCopper -= mRefInvent.mPMCopperNum;
                    P.mGold -= mRefInvent.mPMGoldNum;
                    P.mWood -= mRefInvent.mPMWoodNum;
                    P.mClay -= mRefInvent.mPMClayNum;
                    P.mLimestone -= mRefInvent.mPMLimeStoneNum;

                    if (mRefInvent.mOutType == CDefine.eResType.IronR)
                        P.mIronR += mRefInvent.mOutNum;
                    else if (mRefInvent.mOutType == CDefine.eResType.CopperR)
                        P.mCopperR += mRefInvent.mOutNum;

                    mDuraMax -= 1;

                    NeedSave();

                    P.NeedSave();

                    if (P.IsOnLine())
                    {
                        P.SendResMsg();
                    }
                        

                }
            }
        }

        public void NeedSave()
        {
            gDefine.gDataSys.AddToSaveList(mRefDataNode);
        }

        public void ReadyData(QuickData.CQDDataNode node)
        {
            node.ClearParam();
            node.AddParam("mId", mId);
            node.AddParam("mInventId", mInventId);
            node.AddParam("mIsProductting", mIsProductting ?(int)1:(int)0);

            node.AddParam("mOwnerId", mOwnerId);
            node.AddParam("mDuraMax", mDuraMax);
        }

        public void LoadData(QuickData.CQDDataNode node)
        {
            List<QuickData.CQDParam> param = node.GetCQParam();

            mId = node.GetParamValueLong(0);
            mInventId = node.GetParamValueLong(1);
            mIsProductting = node.GetParamValueInt(2) == 1 ? true : false;
            mOwnerId = node.GetParamValueLong(3);
            mDuraMax = node.GetParamValueIntByName("mDuraMax", 1);

            mOwner = gDefine.gPlayerBase.Find(mOwnerId);
            mRefInvent = gDefine.gInvent.Find(mInventId);
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

    public class CPcMachine
    {
        public List<CMachine> mDict = new List<CMachine>();

        public void Add(CMachine Machine)
        {
            mDict.Add(Machine);
        }

        public void SendMsg(long MachineId, PlayerBase P)
        {
            if(P.IsOnLine())
            {
                if (MachineId < 0)
                {
                    foreach (var v in mDict)
                        v.SendMsg(P);
                }
                else
                {
                    foreach (var v in mDict)
                        if(v.mId == MachineId)
                        {
                            v.SendMsg(P);
                            break;
                        }
                           
                }
            }
        }


        public void BeginProduce(long MachineId, bool On, PlayerBase P)
        {
            foreach (var v in mDict)
                if (v.mId == MachineId)
                {
                    if(On)
                    {
                        if (!v.mIsProductting || v.mProductT <= 0)
                            v.mProductT = System.DateTime.Now.Ticks;
                    }
                    v.mIsProductting = On;
                    v.SendMsg(P);
                    break;
                }
        }

        public void Update(PlayerBase P)
        {
            for(int i=0; i<mDict.Count; i++)
            {
                if(mDict[i].mIsProductting)
                {
                    mDict[i].UpdateProduce(P);
                    if (mDict[i].mDuraMax <= 0)
                    {
                        CMachine machine = mDict[i];
                        machine.mOwnerId = -1;
                        machine.mOwner = null;
                        machine.NeedSave();
                        mDict.RemoveAt(i);
                        i--;
                        gDefine.gMachine.AddMachineToRecycle(machine);
                        if (P.IsOnLine())
                            machine.SendMsg(P);
                    }
                }
            }
         
        }
    }

    public class CMachineManager
    {
        long mId = 10000;
        QuickData.CQDDataNode mRefDataNode = null;
        public List<CMachine> mRecycleDict = new List<CMachine>(); //失效的在这里

        public void AddMachineToRecycle(CMachine Machine)
        {
            mRecycleDict.Add(Machine);
            Machine.mOwnerId = -1;
            Machine.NeedSave();
        }

        public void ReadyData(QuickData.CQDDataNode node)
        {
            node.ClearParam();
            node.AddParam("mId", mId);
        }

        public void Load()
        {
            string[] nodePathName = new string[1] { "Machine" };
            QuickData.CQDNode dataNode = gDefine.gDataSys.GetNode(null, nodePathName);

            if (dataNode == null)
                mRefDataNode = (QuickData.CQDDataNode)gDefine.gDataSys.CreateNode(null, nodePathName);
            else
                mRefDataNode = (QuickData.CQDDataNode)dataNode;

            mId = mRefDataNode.GetParamValueLongByName("mId", 1001);
            mRefDataNode.mReadyParamFunc = ReadyData;

            if (mRefDataNode != null)
            {
                mRefDataNode.LoadSelfChild();
                QuickData.CQDDataNode data = mRefDataNode.FirstChild();
                while (data != null)
                {
                    CMachine machine = new CMachine();
                    machine.Load(data);


                    if (machine.mOwnerId > 0)
                    {
                        PlayerBase p = gDefine.gPlayerBase.Find(machine.mOwnerId);
                        if (p != null)
                            p.mMachine.Add(machine);
                    }
                    else
                    {
                        mRecycleDict.Add(machine);
                    }
                    

                    data = (QuickData.CQDDataNode)data.refBrother;
                }
            }
        }

        public void NeedSave()
        {
            gDefine.gDataSys.AddToSaveList(mRefDataNode);
        }


        public void CreateMachine(long InventId, PlayerBase P)
        {
            CInventData invent = gDefine.gInvent.Find(InventId);
            if (invent != null)
            {
                if( P.mIron >= invent.mCMIronNum && P.mCopper >= invent.mCMCopperNum
                    && P.mGold >= invent.mCMGoldNum && P.mWood >= invent.mCMWoodNum
                    && P.mClay >= invent.mCMClayNum && P.mLimestone >= invent.mCMLimeStoneNum)
                {
                    P.mIron -= invent.mCMIronNum;
                    P.mCopper -= invent.mCMCopperNum;
                    P.mGold -= invent.mCMGoldNum;
                    P.mWood -= invent.mCMWoodNum;
                    P.mClay -= invent.mCMClayNum;
                    P.mLimestone -= invent.mCMLimeStoneNum;

                    P.NeedSave();
                 

                    CMachine machine = GetMachine();
                    machine.mId = mId++;
                    machine.mOwnerId = P.uid;
                    machine.mInventId = InventId;
                    machine.mIsProductting = false;
                    machine.mRefInvent = invent;
                    machine.mOwner = P;
                    machine.mDuraMax = 1;

                    

                    machine.NeedSave();

                    P.mMachine.Add(machine);

                    machine.SendMsg(P, true);

                    NeedSave();
                }
            }
        }

        public CMachine GetMachine()
        {
            CMachine machine;
            if (mRecycleDict.Count > 0)
            {
                machine = mRecycleDict[mRecycleDict.Count - 1];
                mRecycleDict.RemoveAt(mRecycleDict.Count - 1);
            }
            else
            {
                machine = new CMachine();
                machine.CreateDataNode(mRefDataNode);
            }
                
            return machine;
        }
    }
}
