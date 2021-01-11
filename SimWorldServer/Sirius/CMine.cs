using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickData;

public class CMineData
{
    public long mId;
    public long mOwnerId;
    public CMine.eMineType mMineType = CMine.eMineType.Iron;
    public int mPerc = 50;
    public double mResNum;

    public PlayerBase mRefOwner;
    public List<PlayerBase> mDigPlayerArr = new List<PlayerBase>();

    CQDDataNode mRefDataNode;

    public void CreateDataNode( CQDDataNode Node)
    {
        mRefDataNode = gDefine.gDataSys.CreateNode(Node, "Mine", null, ReadyData);
    }

    public double Dig( int min, double TiLi)
    {
        double digNum = 0;
        if( mMineType ==  CMine.eMineType.Apple )
        {
            //    //digNum = TiLi/(10+((TiLi-10.0)/10.0)/10.0);
            digNum = TiLi / (10 + (TiLi - 10.0) / (100 + (TiLi - 10) / 100.0));
        }
        else
        {
            digNum = TiLi / (10 + (TiLi - 10.0) / (100 + (TiLi - 10) / 100.0));
        }

        digNum *= min;
        if (mResNum <= digNum)
        {
            digNum = mResNum;
            mResNum = 0;

            //del
            // gDefine.gMine.Del(mId);

            // for(int i=0; i<mDigPlayerArr.Count; i++)
            // {
            //     mDigPlayerArr[i].mDigMineId = -1;
            // }

            //  mRefOwner.DelMine(mId);
            NeedSave();
        }
        else
        {
            mResNum -= digNum;
            NeedSave();
        }

        return digNum; 
    }

    public void Load(CQDDataNode node)
    {
        mRefDataNode = node;
        mRefDataNode.mReadParamFunc = LoadFromEdit;
        mRefDataNode.mReadyParamFunc = ReadyData;

        mId = node.GetParamValueLong(0);
        mOwnerId = node.GetParamValueLong(1);
        mMineType = (CMine.eMineType) node.GetParamValueInt(2);
        mPerc = node.GetParamValueInt(3);
        mResNum = node.GetParamValueDouble(4);

        mRefOwner = gDefine.gPlayerBase.Find(mOwnerId);

        mRefOwner.mMineData.mOwnMineData.Add(this);
    }

    public void LoadFromEdit(CQDDataNode node)
    {
        mId = node.GetParamValueLong(0);
        mOwnerId = node.GetParamValueLong(1);
        mMineType = (CMine.eMineType)node.GetParamValueInt(2);
        mPerc = node.GetParamValueInt(3);
        mResNum = node.GetParamValueDouble(4);
        NeedSave();
    }

    public void ReadyData(CQDDataNode node)
    {
        node.ClearParam();
        node.AddParam("mId", mId);
        node.AddParam("mOwnerId", mOwnerId);
        node.AddParam("mMineType", (int)mMineType);
        node.AddParam("mPerc", mPerc);
        node.AddParam("mResNum", mResNum);
    }

    public void NeedSave()
    {
        gDefine.gDataSys.AddToSaveList(mRefDataNode);
    }

}

public class CPlayerMineData
{
    public List<CMineData> mSearchMineData = new List<CMineData>();
    public List<CMineData> mOwnMineData = new List<CMineData>();

    public CMineData FindFromOwnMine(long MineId)
    {
        for(int i=0; i<mOwnMineData.Count; i++)
        {
            if (mOwnMineData[i].mId == MineId)
                return mOwnMineData[i];
        }
        return null;
    }

    public CMineData Del(long MineId)
    {
        CMineData mine = null;
        for (int i = 0; i < mOwnMineData.Count; i++)
        {
            if (mOwnMineData[i].mId == MineId)
            {
                mine = mOwnMineData[i];
                mOwnMineData.RemoveAt(i);
                break;
            }

        }
        return mine;
    }

}

public  class CMine
{
    public enum eMineType
    {
        Null = 0,
        Iron,
        Copper,
        Gold,
        Apple,
        clay,
        Limestone,
    }

    public long mIdCount = 6666;

    public List<CMineData> mMineDict = new List<CMineData>();
    public List<CMineData> mMineValuedDict = new List<CMineData>();
    public List<CMineData> mMineAppleDict = new List<CMineData>();
    public List<CMineData> mMineAppleValuedDict = new List<CMineData>();
    public Dictionary<long, CMineData> mQuickDict = new Dictionary<long, CMineData>();
    public long mValueT = 0;

    QuickData.CQDDataNode mRefDataNode = null;

    public void RefreshValuedMine()
    {
        if (System.DateTime.Now.Ticks > mValueT)
        {
            mValueT = System.DateTime.Now.Ticks + 10 * 10000000;
            mMineValuedDict.Clear();
            foreach (CMineData m in mMineDict)
            {
                if (m.mResNum > 0)
                    mMineValuedDict.Add(m);
            }

            mMineAppleValuedDict.Clear();
            foreach (CMineData m in mMineAppleDict)
            {
                if (m.mResNum > 0)
                    mMineAppleValuedDict.Add(m);
            }

        }
    }

    public CMineData Find(long MineId)
    {
        CMineData data;
        mQuickDict.TryGetValue(MineId, out data);
        return data;
    }

    public void ReadyData(QuickData.CQDDataNode node)
    {
        node.ClearParam();
        node.AddParam("mIdCount", mIdCount);
    }

    public void Load()
    {
        string[] nodePathName = new string[1] { "Mine" };
        QuickData.CQDNode dataNode = gDefine.gDataSys.GetNode(null, nodePathName);

        if (dataNode == null)
            mRefDataNode = (QuickData.CQDDataNode)gDefine.gDataSys.CreateNode(null, nodePathName);
        else
            mRefDataNode = (QuickData.CQDDataNode)dataNode;

        mIdCount = mRefDataNode.GetParamValueLongByName("mIdCount", 6666);
        mRefDataNode.mReadyParamFunc = ReadyData;

        if (mRefDataNode != null)
        {
            mRefDataNode.LoadSelfChild();
            QuickData.CQDDataNode data = mRefDataNode.FirstChild();
            while (data != null)
            {
                CMineData mine = new CMineData();
                mine.Load(data);

                mQuickDict.Add(mine.mId, mine);
                if (mine.mMineType == eMineType.Apple)
                    mMineAppleDict.Add(mine);
                else
                    mMineDict.Add(mine);

                data = (QuickData.CQDDataNode)data.refBrother;
            }
        }
    }

    public void Del(long MineId)
    {
        mQuickDict.Remove(MineId);

        for(int i=0; i<mMineAppleDict.Count; i++)
        {
            if( mMineAppleDict[i].mId == MineId)
            {
                mMineAppleDict.RemoveAt(i);
                break;
            }
        }

        for (int i = 0; i < mMineDict.Count; i++)
        {
            if (mMineDict[i].mId == MineId)
            {
                mMineDict.RemoveAt(i);
                break;
            }
        }

    }

    public CMineData [] SearchOldMineCoin(int IsMine, PlayerBase P)
    {
        RefreshValuedMine();
        List<CMineData> tmpArr = new List<CMineData>();
        if (IsMine == 0)
        {
            int num = mMineValuedDict.Count > 10 ? 10 : mMineValuedDict.Count;
            if (num == 0)
                return null;

            int index = gDefine.gRandom.Next(0, mMineValuedDict.Count);

            for (int i = 0; i < num; i++)
            {
                if (mMineValuedDict[(i + index) % mMineValuedDict.Count].mOwnerId != P.uid)
                    tmpArr.Add(mMineValuedDict[(i + index) % mMineValuedDict.Count]);
            }
        }
        else
        {
            int num = mMineAppleValuedDict.Count > 10 ? 10 : mMineAppleValuedDict.Count;
            if (num == 0)
                return null;

            int index = gDefine.gRandom.Next(0, mMineAppleValuedDict.Count);

            for (int i = 0; i < num; i++)
            {
                if (mMineAppleValuedDict[(i + index) % mMineAppleValuedDict.Count].mOwnerId != P.uid)
                    tmpArr.Add(mMineAppleValuedDict[(i + index) % mMineAppleValuedDict.Count]);
            }
        }

        if (tmpArr.Count == 0)
            return null;
        else
            return tmpArr.ToArray();
    }

    public CMineData[] SearchMine(int IsNew, int IsMine, PlayerBase p)
    {
        if (IsNew == 0)
            return SearchNewMine(IsMine, p);
        else
            return SearchOldMine(IsMine, p);
    }

    public CMineData[] SearchNewMine(int IsMine, PlayerBase p)
    {
        int newMaxNum = ((int) p.mDaoDe + (int)p.mTiLi + (int)p.mZhiLi) / 10 - p.mMineData.mOwnMineData.Count;
        if ( newMaxNum <= 0 )
            return null;

        long value = gDefine.gRandom.Next(0, 10000000);
        if ( IsMine == 0 )
        {
            return CreateMine(eMineType.clay, 1, p);

            if (newMaxNum >= 3 && value < CalcSearchPrec(p.mZhiLi, 30000) * 10000000)
                return CreateMine(eMineType.Gold, 3, p);
            else if (newMaxNum >= 2 && value < CalcSearchPrec(p.mZhiLi, 20000) * 10000000)
                return CreateMine(eMineType.Gold, 2, p);
            else if (value < CalcSearchPrec(p.mZhiLi, 10000) * 10000000)
                return CreateMine(eMineType.Gold, 1, p);
            else if (newMaxNum >= 3 && value < CalcSearchPrec(p.mZhiLi, 300) * 10000000)
                return CreateMine(eMineType.Copper, 3, p);
            else if (newMaxNum >= 2 && value < CalcSearchPrec(p.mZhiLi, 200) * 10000000)
                return CreateMine(eMineType.Copper, 2, p);
            else if (value < CalcSearchPrec(p.mZhiLi, 100) * 10000000)
                return CreateMine(eMineType.Copper, 1, p);
            else if (newMaxNum >= 3 && value < CalcSearchPrec(p.mZhiLi, 30) * 10000000)
            {
                value = gDefine.gRandom.Next(0, 30);
                if(value<10)
                    return CreateMine(eMineType.Iron, 3, p);
                else if (value < 20)
                    return CreateMine(eMineType.clay, 3, p);
                else
                    return CreateMine(eMineType.Limestone, 3, p);
            }
               
            else if (newMaxNum >= 2 && value < CalcSearchPrec(p.mZhiLi, 20) * 10000000)
            {
                value = gDefine.gRandom.Next(0, 30);
                if (value < 10)
                    return CreateMine(eMineType.Iron, 2, p);
                else if (value < 20)
                    return CreateMine(eMineType.clay, 2, p);
                else
                    return CreateMine(eMineType.Limestone, 2, p);
            }
            else if (value < CalcSearchPrec(p.mZhiLi, 10) * 10000000)
            {
                value = gDefine.gRandom.Next(0, 30);
                if (value < 10)
                    return CreateMine(eMineType.Iron, 1, p);
                else if (value < 20)
                    return CreateMine(eMineType.clay, 1, p);
                else
                    return CreateMine(eMineType.Limestone, 1, p);
            }
            else
                return null;
        }
        else
        {
            //new apple.
            if (newMaxNum >= 3 &&  value < CalcSearchPrec(p.mZhiLi, 30) * 10000000)
                return CreateMine(eMineType.Apple, 3, p);
            else if (newMaxNum >= 2 && value < CalcSearchPrec(p.mZhiLi, 20) * 10000000)
                return CreateMine(eMineType.Apple, 2, p);
            else if (value < CalcSearchPrec(p.mZhiLi, 10) * 10000000)
                return CreateMine(eMineType.Apple, 1, p);
            else
                return null;
        }

    }

    public double CalcSearchPrec( double Zhili, double Value)
    {
        return Zhili / (10 + (Zhili - 10) / 10.0) / Value;
    }

    CMineData [] CreateMine( eMineType MineType, int Num, PlayerBase owner)
    {
        CMineData[] arr = new CMineData[Num];
        for( int i=0; i<Num; i++)
        {
            CMineData mine = new CMineData();

            mine.mId = mIdCount++;
            mine.mMineType = MineType;
            mine.mResNum = (MineType == eMineType.Apple) ? 10000 : 100000;
            mine.mPerc = 40;
            mine.mOwnerId = owner.uid;
            mine.mRefOwner = owner;

            owner.mMineData.mOwnMineData.Add(mine);

            mine.CreateDataNode(mRefDataNode);

            mQuickDict.Add(mine.mId, mine);
            if (MineType == eMineType.Apple)
                mMineAppleDict.Add(mine);
            else
                mMineDict.Add(mine);

            arr[i] = mine;
        }

        NeedSave();

        return arr;
    }

    public void NeedSave()
    {
        gDefine.gDataSys.AddToSaveList(mRefDataNode);
    }

    public CMineData[] SearchOldMine(int IsMine, PlayerBase P)
    {
        List<CMineData> tmpArr = new List<CMineData>();
        if( IsMine == 0 )
        {
            int num = mMineDict.Count > 10 ? 10 : mMineDict.Count;
            if (num == 0)
                return null;

            int index = gDefine.gRandom.Next(0, mMineDict.Count);

            for (int i = 0; i < num; i++)
            {
                if (mMineDict[(i + index) % mMineDict.Count].mOwnerId != P.uid)
                    tmpArr.Add(mMineDict[(i + index) % mMineDict.Count]);
            }
        }
        else
        {
            int num = mMineAppleDict.Count > 10 ? 10 : mMineAppleDict.Count;
            if (num == 0)
                return null;

            int index = gDefine.gRandom.Next(0, mMineAppleDict.Count);

            for( int i=0; i<num; i++)
            {
                if (mMineAppleDict[(i + index) % mMineAppleDict.Count].mOwnerId != P.uid)
                    tmpArr.Add(mMineAppleDict[(i + index) % mMineAppleDict.Count]);
            }
        }

        if (tmpArr.Count == 0)
            return null;
        else
            return tmpArr.ToArray();
    }


}

