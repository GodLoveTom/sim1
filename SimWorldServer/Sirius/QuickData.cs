//---------------------
//快速文档数据库
//---------------------
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickData
{
    //节点类型
    public enum eQDNodeType
    {
        Null = 0,
        MainNode,       //主节点
        StringCenNode,  //字符串主节点
        StringNode,     //字符串节点
        DataCenNode,    //数据主节点
        DataNode,       //数据节点
    }

    //数据类型
    public enum eQDValueType
    {
        Int = 0,
        Float,
        Long,
        Str,    //字符串
        Char,
        Byte,
        Double,
        Null, //未知
    }

    //所有的参数父类
    public class CQDParam
    {
        public eQDValueType valueType;    //属性的类型
        public int nodeNameStrIndex;            //属性的名称字符串索引
        public virtual void WriteValue(BinaryWriter bw) { }
        public virtual int Size() { return 0; }
        public virtual void ReadValue(BinaryReader br) { }

        public virtual int GetValueInt() { throw new Exception(); }
        public virtual float GetValueFloat() { throw new Exception(); }
        public virtual long GetValueLong() { throw new Exception(); }
        public virtual string GetValueStr() { throw new Exception(); }
        public virtual char GetValueChar() { throw new Exception(); }
        public virtual byte GetValueByte() { throw new Exception(); }
        public virtual double GetValueDouble() { throw new Exception(); }

        public virtual string ValueToStr() { return ""; }

        public virtual void ReadValueFromStr(string str) { throw new Exception(); }

        public static eQDValueType GetValueType(string str)
        {
            if (str == "Int")
                return eQDValueType.Int;
            else if (str == "Float")
                return eQDValueType.Float;
            else if (str == "Long")
                return eQDValueType.Long;
            else if (str == "Str")
                return eQDValueType.Str;
            else if (str == "Char")
                return eQDValueType.Char;
            else if (str == "Byte")
                return eQDValueType.Byte;
            else if (str == "Double")
                return eQDValueType.Double;
            else 
                return eQDValueType.Null;
        }
    }

    public class CQDParamInt : CQDParam
    {
        public CQDParamInt() { valueType = eQDValueType.Int; }
        public int value; //
        public override void WriteValue(BinaryWriter bw) { bw.Write(value); }
        public override int Size() { return sizeof(int); }
        public override void ReadValue(BinaryReader br) { value = br.ReadInt32(); }
        public override int GetValueInt() { return value; }

        public override string ValueToStr() { return value.ToString(); }

        public override void ReadValueFromStr(string str)
        {
            if( !int.TryParse(str, out value))
            {
                double v = double.Parse(str);
                value = (int)v;
            }
        }
    }

    public class CQDParamFloat : CQDParam
    {
        public CQDParamFloat() { valueType = eQDValueType.Float; }
        public float value; //
        public override void WriteValue(BinaryWriter bw) { bw.Write(value); }
        public override int Size() { return sizeof(float); }
        public override void ReadValue(BinaryReader br) { value = br.ReadSingle(); }
        public override float GetValueFloat() { return value; }

        public override string ValueToStr() { return value.ToString(); }

        public override void ReadValueFromStr(string str)
        {
            if (!float.TryParse(str, out value))
            {
                double v = double.Parse(str);
                value = (float)v;
            }
        }



    }

    public class CQDParamLong : CQDParam
    {
        public CQDParamLong() { valueType = eQDValueType.Long; }
        public long value; //
        public override void WriteValue(BinaryWriter bw) { bw.Write(value); }
        public override int Size() { return sizeof(long); }
        public override void ReadValue(BinaryReader br) { value = br.ReadInt64(); }
        public override long GetValueLong() { return value; }

        public override string ValueToStr() { return value.ToString(); }

        public override void ReadValueFromStr(string str)
        {
            if (!long.TryParse(str, out value))
            {
                double v = double.Parse(str);
                value = (long)v;
            }
        }
    }


    public class CQDParamStr : CQDParam
    {
        public CQDParamStr() { valueType = eQDValueType.Str; }
        public string value; //
        public override void WriteValue(BinaryWriter bw) { bw.Write(value); }

        public override int Size()
        {
            if (string.IsNullOrEmpty(value))
                return 0;
            else
                return Encoding.UTF8.GetBytes(value).Length; ;
        }
        public override void ReadValue(BinaryReader br) { value = br.ReadString(); }
        public override string GetValueStr() { return value; }

        public override string ValueToStr() { return value.ToString(); }

        public override void ReadValueFromStr(string str)
        {
            value = str;
        }


    }
    public class CQDParamDouble : CQDParam
    {
        public CQDParamDouble() { valueType = eQDValueType.Double; }
        public double value; //
        public override void WriteValue(BinaryWriter bw) { bw.Write(value); }
        public override int Size() { return sizeof(double); }
        public override void ReadValue(BinaryReader br) { value = br.ReadDouble(); }
        public override double GetValueDouble() { return value; }

        public override string ValueToStr() { return value.ToString(); }

        public override void ReadValueFromStr(string str)
        {
            value = double.Parse(str);
        }


    }
    public class CQDParamChar : CQDParam
    {
        public CQDParamChar() { valueType = eQDValueType.Char; }
        public char value; //
        public override void WriteValue(BinaryWriter bw) { bw.Write(value); }
        public override int Size() { return sizeof(char); }
        public override void ReadValue(BinaryReader br) { value = br.ReadChar(); }
        public override char GetValueChar() { return value; }

        public override string ValueToStr() { return value.ToString(); }

        public override void ReadValueFromStr(string str)
        {
            if (!char.TryParse(str, out value))
            {
                double v = double.Parse(str);
                value = (char)v;
            }
        }

    }

    public class CQDParamByte : CQDParam
    {
        public CQDParamByte() { valueType = eQDValueType.Byte; }
        public byte value; //
        public override void WriteValue(BinaryWriter bw) { bw.Write(value); }
        public override int Size() { return sizeof(byte); }
        public override void ReadValue(BinaryReader br) { value = br.ReadByte(); }
        public override byte GetValueByte() { return value; }

        public override string ValueToStr() { return value.ToString(); }

        public override void ReadValueFromStr(string str)
        {
            if (!byte.TryParse(str, out value))
            {
                double v = double.Parse(str);
                value = (byte)v;
            }
        }

    }

    //文件的处理相关类
    public class CQDFile
    {
        //静态部分
        public FileStream fs = null;  //静态的公用file指针
        public BinaryWriter binW = null;
        public BinaryReader binR = null;
        public string _filePath = null; //文件路径

        public FileStream FilePtr() { return fs; }

        public long GetNameIndexOffset() { return sizeof(int); }
        public long GetBrotherOffset() { return sizeof(int) * 2; }
        public long GetChildOffset() { return sizeof(int) * 2 + sizeof(long); }
        public long GetParamOffset() { return sizeof(int) * 2 + sizeof(long) * 2; }

        public void Flush()
        {
            if (fs != null)
                fs.Flush();
        }

        public BinaryReader OpenBR()
        {
            if (binR == null && fs != null)
                binR = new BinaryReader(fs);

            return binR;
        }

        public BinaryWriter OpenBW()
        {
            if (binW == null && fs != null)
                binW = new BinaryWriter(fs);

            return binW;
        }

        public bool OpenFile(bool reNewFile)
        {
            if (reNewFile)
            {
                //删除旧文件
                if (File.Exists(_filePath))
                    File.Delete(_filePath);      //删除指定文件
            }

            if (fs == null && !string.IsNullOrEmpty(_filePath))
            {
                try
                {
                    if (File.Exists(_filePath))
                        fs = new FileStream(_filePath, FileMode.Open);
                    else
                    {
                        fs = new FileStream(_filePath, FileMode.Create);
                        OpenBW().Write((long)-1);
                    }

                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }

            }
            else
                return fs != null;
        }

        public void CloseFile()
        {
            if (binW != null)
            {
                binW.Close();
                binW = null;
            }

            if (binR != null)
            {
                binR.Close();
                binR = null;
            }

            if (fs != null)
            {
                try
                {
                    fs.Close();
                }
                catch (Exception e)
                {

                }

                fs = null;
            }
        }

    }

    //所有子节点从此处继承
    public class CQDNode
    {
        public eQDNodeType nodeType = eQDNodeType.Null; //节点类型
        public int nodeNameIndex = -1; //节点名称索引

        protected long filePos = -1;  //file文件地址
        public long dataSize = 0; //数据长度
        public long usedSize = 0;  //使用长度 
        public CQDNode refPre;   //父节点,或者自己的前一个兄弟节点
        public CQDNode refChild; //子节点
        public CQDNode refBrother; //兄弟节点
        public byte nodeDeapth;  //节点深度值
        public int oldBufferSize; //过去

        public bool needSave = false;  //是否需要重新存储
        public bool isOpened = false;  //该节点是否展开

        //属性表
        protected List<CQDParam> paramArr = new List<CQDParam>();

        public string GetNodeName()
        {
            return refSystem.GetStringByIndex(nodeNameIndex);
        }

        public List<CQDParam> GetCQParam()
        {
            return paramArr;
        }

        //引用主节点
        public CQDSystem refSystem; //引用系统的节点

        public void AddChild(CQDNode node)
        {
            if (refChild == null)
            {
                refChild = node;
                node.refPre = this;

                refSystem.AddToSaveList(this);//需要更新自己
            }
            else
            {
                CQDNode tail = refChild;
                while (tail.refBrother != null)
                {
                    tail = tail.refBrother;
                }

                tail.refBrother = node;
                node.refPre = tail;

                refSystem.AddToSaveList(tail);
            }
        }

        public void AddBrother(CQDNode node)
        {
            if (refBrother == null)
            {
                refBrother = node;
                node.refPre = this;

                refSystem.AddToSaveList(this);//需要更新自己
            }
            else
            {
                CQDNode tail = refBrother;
                while (tail.refBrother != null)
                {
                    tail = tail.refBrother;
                }

                tail.refBrother = node;
                node.refPre = tail;

                refSystem.AddToSaveList(tail);
            }
        }


        public CQDNode FindChild(int strIndex)
        {
            if (!isOpened)
                LoadSelfChild();

            CQDNode refnode = refChild;
            while (refnode != null)
            {
                if (refnode.nodeNameIndex == strIndex)
                    return refnode;
                else
                    refnode = refnode.refBrother;
            }
            return null;
        }

        protected void AddParamIntValue(int strIndex, int value) { CQDParamInt n = new CQDParamInt(); n.value = value; n.nodeNameStrIndex = strIndex; paramArr.Add(n); }
        protected void AddParamFloatValue(int strIndex, float value) { CQDParamFloat n = new CQDParamFloat(); n.value = value; n.nodeNameStrIndex = strIndex; paramArr.Add(n); }
        protected void AddParamLongValue(int strIndex, long value) { CQDParamLong n = new CQDParamLong(); n.value = value; n.nodeNameStrIndex = strIndex; paramArr.Add(n); }
        protected void AddParamStrValue(int strIndex, string value) { CQDParamStr n = new CQDParamStr(); n.value = value; n.nodeNameStrIndex = strIndex; paramArr.Add(n); }
        protected void AddParamByteValue(int strIndex, byte value) { CQDParamByte n = new CQDParamByte(); n.value = value; n.nodeNameStrIndex = strIndex; paramArr.Add(n); }

        //
        public CQDParam CreateParamByType(eQDValueType paramType)
        {
            switch (paramType)
            {
                case eQDValueType.Int:
                    return new CQDParamInt();
                case eQDValueType.Float:
                    return new CQDParamFloat();
                case eQDValueType.Long:
                    return new CQDParamLong();
                case eQDValueType.Str:
                    return new CQDParamStr();
                case eQDValueType.Char:
                    return new CQDParamChar();
                case eQDValueType.Byte:
                    return new CQDParamByte();
                case eQDValueType.Double:
                    return new CQDParamDouble();
            }
            return null;
        }

        //修改指向的地址
        public void BrotherOrChildChangePosNotice(long fPos)
        {
            if (refBrother != null && refBrother.filePos == fPos)
            {
                refSystem.FilePack().FilePtr().Seek(filePos + refSystem.FilePack().GetBrotherOffset(), SeekOrigin.Begin);
                BinaryWriter bw = refSystem.FilePack().OpenBW();
                bw.Write(fPos);
            }
            else if (refChild != null && refChild.filePos == fPos)
            {
                refSystem.FilePack().FilePtr().Seek(filePos + refSystem.FilePack().GetChildOffset(), SeekOrigin.Begin);
                BinaryWriter bw = refSystem.FilePack().OpenBW();
                bw.Write(fPos);
            }
        }

        //子或兄弟改变了
        public void BrotherOrChildRemoveNotice(CQDNode node)
        {
            if (refBrother == node)
            {
                refBrother = node.refBrother;

                if (node.refBrother != null)
                {
                    node.refBrother.refPre = this;
                    refSystem.AddToSaveList(node.refBrother);
                }
                refSystem.AddToSaveList(this);
               
            }
            else if (refChild != null)
            {
                refChild = node.refBrother;

                if (node.refBrother != null)
                {
                    node.refBrother.refPre = this;
                    refSystem.AddToSaveList(node.refBrother);
                }
                refSystem.AddToSaveList(this);
            }
        }


        //存储参数等信息
        public void SaveParam()
        {
            BinaryWriter bw = refSystem.FilePack().OpenBW();
            short count = (short)paramArr.Count;
            bw.Write(count);
            for (int i = 0; i < paramArr.Count; i++)
            {
                //类型，字符串，数据
                bw.Write((int)paramArr[i].valueType);
                bw.Write(paramArr[i].nodeNameStrIndex);
                paramArr[i].WriteValue(bw);
            }
        }

        public void Save()
        {
            if (needSave)
            {
                needSave = false;
                //存储
                ReadyParam();
                ReCheckBuffSize();
                SaveNode();
                SaveParam();

            }
        }

        void ReCheckBuffSize()
        {
            //计算
            int size = 0;
            for (int i = 0; i < paramArr.Count; i++)
                size += paramArr[i].Size();

            //如果有旧的计算大小是否还够用
            //如果不够用，重新寻找一个地方。并更新父节点对自己的指向
            if (size > oldBufferSize || filePos < 0)
            {
                oldBufferSize = size;
                filePos = refSystem.FilePack().FilePtr().Seek(0, SeekOrigin.End);

                if (refPre != null)
                    refPre.BrotherOrChildChangePosNotice(filePos);
                else if (nodeType == eQDNodeType.MainNode)
                {
                    //如果没有前置节点，那么就是主节点。
                    refSystem.FilePack().FilePtr().Seek(0, SeekOrigin.Begin);
                    refSystem.FilePack().OpenBW().Write(filePos);
                }
            }
        }

        //应该重写该方法来整理数据
        public virtual void ReadyParam() { }

        //子节点可以读取数据了
        public virtual void ReadParam() { }

        //载入根节点
        public void LoadMainNode(CQDNode mainNode)
        {
            refSystem.FilePack().FilePtr().Seek(0, SeekOrigin.Begin);
            BinaryReader br = refSystem.FilePack().OpenBR();
            mainNode.filePos = br.ReadInt64();
        }

        //载入自己的所有子节点
        public void LoadSelfChild()
        {
            if (isOpened == false && filePos > 0)
            {
                refSystem.FilePack().FilePtr().Seek(filePos + refSystem.FilePack().GetChildOffset(), SeekOrigin.Begin);
                BinaryReader br = refSystem.FilePack().OpenBR();
                long childPos = br.ReadInt64();
                if (childPos > 0)
                {
                    CQDNode node = LoadNode(childPos);
                    if (node != null)
                    {
                        refChild = node;
                        refChild.refPre = this;
                        node = refChild.LoadBrother();
                        while (node != null)
                            node = node.LoadBrother();
                    }
                }
            }
            isOpened = true;
        }

        public CQDNode LoadNode(long pos)
        {
            refSystem.FilePack().FilePtr().Seek(pos, SeekOrigin.Begin);
            BinaryReader br = refSystem.FilePack().OpenBR();
            eQDNodeType nType = (eQDNodeType)br.ReadInt32();
            CQDNode n = refSystem.CreateNodeByNodeType(nType);
            n.nodeType = nType;
            n.filePos = pos;

            //节点名称字符串
            n.nodeNameIndex = br.ReadInt32();

            //readParam..
            refSystem.FilePack().FilePtr().Seek(pos + refSystem.FilePack().GetParamOffset(), SeekOrigin.Begin);
            short paramNum = br.ReadInt16();
            for (int i = 0; i < paramNum; i++)
            {
                int paramType = br.ReadInt32();
                int nameStrId = br.ReadInt32();
                CQDParam paramNode = CreateParamByType((eQDValueType)paramType);
                if (paramNode == null)
                    throw new Exception("slim file load error");
                else
                {
                    paramNode.nodeNameStrIndex = nameStrId;
                    paramNode.ReadValue(br);
                    n.paramArr.Add(paramNode);
                }
            }
            //
            n.ReadParam();

            return n;
        }

        //栽入自己的兄弟节点
        public CQDNode LoadBrother()
        {
            if (refBrother == null)
            {
                refSystem.FilePack().FilePtr().Seek(filePos + refSystem.FilePack().GetBrotherOffset(), SeekOrigin.Begin);
                BinaryReader br = refSystem.FilePack().OpenBR();
                long brotherPos = br.ReadInt64();
                if (brotherPos > 0)
                {
                    refBrother = LoadNode(brotherPos);
                    if (refBrother != null)
                    {
                        refBrother.refPre = this;
                        return refBrother;
                        //refBrother.LoadBrother();
                    }
                }
            }
            return null;
        }

        //存储节点等信息
        protected void SaveNode()
        {
            if (filePos < 0)
                filePos = refSystem.FilePack().FilePtr().Seek(0, SeekOrigin.End);
            else
                refSystem.FilePack().FilePtr().Seek(filePos, SeekOrigin.Begin);

            BinaryWriter _bw = refSystem.FilePack().OpenBW();

            //写入节点类型
            int _nodeType = (int)nodeType;
            _bw.Write(_nodeType);

            //写入节点名字的字符串
            _bw.Write(nodeNameIndex);

            //写入兄弟节点数据
            if (refBrother != null)
                _bw.Write(refBrother.filePos);
            else
                _bw.Write((long)-1);

            //写入子节点数据
            if (refChild != null)
                _bw.Write(refChild.filePos);
            else
                _bw.Write((long)-1);
        }
    }

    //字符串节点
    public class CQDStrNode : CQDNode
    {
        public int strId;
        public string str;

        public CQDStrNode(CQDSystem system)
        {
            refSystem = system;
            nodeType = eQDNodeType.StringNode;
        }

        public override void ReadParam()
        {
            strId = ((CQDParamInt)paramArr[0]).value;
            str = ((CQDParamStr)paramArr[1]).value;
        }

        public override void ReadyParam()
        {
            paramArr.Clear();
            AddParamIntValue(-1, strId);
            AddParamStrValue(-1, str);
        }
    }


    //所有的字符串表和类
    public class CQDStrCol : CQDNode
    {
        public int strIndex = 1;
        Dictionary<int, CQDStrNode> strDict = new Dictionary<int, CQDStrNode>();
        Dictionary<string, CQDStrNode> strNameDict = new Dictionary<string, CQDStrNode>();
        CQDStrNode lastChildNode = null; //最后的那个子节点

        public CQDStrCol(CQDSystem system)
        {
            refSystem = system;
            nodeType = eQDNodeType.StringCenNode;
        }

        public override void ReadyParam()
        {
            paramArr.Clear();
            AddParamIntValue(-1, strIndex);

        }

        public override void ReadParam()
        {
            strIndex = ((CQDParamInt)paramArr[0]).value;
        }

        public void ReSetData()
        {
            lastChildNode = (CQDStrNode)refChild;
            while (lastChildNode != null)
            {
                strDict.Add(lastChildNode.strId, lastChildNode);
                strNameDict.Add(lastChildNode.str, lastChildNode);
                if (lastChildNode.refBrother == null)
                    break;
                else
                    lastChildNode = (CQDStrNode)lastChildNode.refBrother;
            }
        }

        public int AddStr(string str)
        {
            if (string.IsNullOrEmpty(str))
                return -1;

            CQDStrNode n = null;
            if (!strNameDict.TryGetValue(str, out n))
            {
                CQDStrNode newone = (CQDStrNode)refSystem.CreateNodeByNodeType(eQDNodeType.StringNode);
                newone.strId = strIndex++;
                newone.str = str;

                strDict.Add(newone.strId, newone);
                strNameDict.Add(newone.str, newone);

                if (lastChildNode != null)
                {
                    lastChildNode.refBrother = newone;
                    newone.refPre = lastChildNode;
                }
                else
                {
                    refChild = newone; //此时为第一个子节点
                    newone.refPre = this;
                }

                lastChildNode = newone;

                n = newone;

                refSystem.AddToSaveList(newone);
                refSystem.AddToSaveList(this);
            }
            return n.strId;
        }

        public string GetStr(int strId)
        {
            CQDStrNode n = null;
            if (strDict.TryGetValue(strId, out n))
                return n.str;
            else
                return null;
        }

        public int GetStrIndex(string str)
        {
            CQDStrNode n = null;
            if (strNameDict.TryGetValue(str, out n))
                return n.strId;
            else
                return -202;
        }
    }

    public class CQDDataCol : CQDNode
    {
        public CQDDataCol(CQDSystem system)
        {
            refSystem = system;
            nodeType = eQDNodeType.DataCenNode;
        }
    }

    public class CQDDataNode : CQDNode
    {
        //读取参数
        public delegate void readParamFUNC(CQDDataNode node);
        public readParamFUNC mReadParamFunc = null;
        //重新写入数据
        public delegate void readyParamFUNC(CQDDataNode node);
        public readyParamFUNC mReadyParamFunc = null;

        public CQDDataNode(CQDSystem system)
        {
            nodeType = eQDNodeType.DataNode;
            refSystem = system;
        }

        /// <summary>
        /// 从CQDDataCol强制类型转换到CQDDataNode
        /// </summary>
        /// <param name="source">源对象</param>
        public static explicit operator CQDDataNode(CQDDataCol source)
        {
            var dest = new CQDDataNode(source.refSystem);
            var fields = source.GetType().GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            foreach(var field in fields)
            {
                field.SetValue(dest, field.GetValue(source));
            }
            dest.nodeType = eQDNodeType.DataNode;
            return dest;
        }

        public CQDDataNode FirstChild()
        {
            if (!isOpened)
                LoadSelfChild();

            return (CQDDataNode)refChild;
        }

        public void DelNode(CQDNode node)
        {
            //
           // ffkkhdsiee

        }



        public CQDDataNode GetChild(string name)
        {
            if (!isOpened)
                LoadSelfChild();

            if (refChild == null)
                return null;

            CQDNode child = refChild;
            int nameIndex = -1;
            if (!string.IsNullOrEmpty(name))
                nameIndex = refSystem.GetStringIndex(name);

            while (child != null)
            {
                if (child.nodeNameIndex == nameIndex)
                    return (CQDDataNode)child;
                else
                    child = child.refBrother;
            }

            return null;
        }

        public override void ReadParam()
        {
            if (mReadParamFunc != null)
                mReadParamFunc(this);
        }

        public override void ReadyParam()
        {
            if (mReadyParamFunc != null)
                mReadyParamFunc(this);
        }

        public void ClearParam()
        {
            paramArr.Clear();
        }

        public void AddParam(int value)
        {
            CQDParamInt param = new CQDParamInt();
            param.value = value;
            param.nodeNameStrIndex = -1;
            paramArr.Add(param);
        }

        public void AddParam(string paramName, int value)
        {
            int nameIndex = refSystem.AddString(paramName);

            CQDParamInt param = new CQDParamInt();
            param.value = value;
            param.nodeNameStrIndex = nameIndex;
            paramArr.Add(param);
        }
        public void AddParam(string paramName, double value)
        {
            int nameIndex = refSystem.AddString(paramName);

            CQDParamDouble param = new CQDParamDouble();
            param.value = value;
            param.nodeNameStrIndex = nameIndex;
            paramArr.Add(param);
        }
        public void AddParam(float value)
        {
            CQDParamFloat param = new CQDParamFloat();
            param.value = value;
            param.nodeNameStrIndex = -1;
            paramArr.Add(param);
        }

        public void AddParam(string paramName, float value)
        {
            int nameIndex = refSystem.AddString(paramName);

            CQDParamFloat param = new CQDParamFloat();
            param.value = value;
            param.nodeNameStrIndex = nameIndex;
            paramArr.Add(param);
        }

        public void AddParam(long value)
        {
            CQDParamLong param = new CQDParamLong();
            param.value = value;
            param.nodeNameStrIndex = -1;
            paramArr.Add(param);
        }

        public void AddParam(string paramName, long value)
        {
            int nameIndex = refSystem.AddString(paramName);

            CQDParamLong param = new CQDParamLong();
            param.value = value;
            param.nodeNameStrIndex = nameIndex;
            paramArr.Add(param);
        }

        public void AddParam(byte value)
        {
            CQDParamByte param = new CQDParamByte();
            param.value = value;
            param.nodeNameStrIndex = -1;
            paramArr.Add(param);
        }

        public void AddParam(string paramName, byte value)
        {
            int nameIndex = refSystem.AddString(paramName);

            CQDParamByte param = new CQDParamByte();
            param.value = value;
            param.nodeNameStrIndex = nameIndex;
            paramArr.Add(param);
        }

        public void AddParam(string value)
        {
            CQDParamStr param = new CQDParamStr();
            if (string.IsNullOrEmpty(value))
                param.value = "";
            else
                param.value = value;
            param.nodeNameStrIndex = -1;
            paramArr.Add(param);
        }

        public void AddParam(double value)
        {
            CQDParamDouble param = new CQDParamDouble();
            param.value = value;
            param.nodeNameStrIndex = -1;
            paramArr.Add(param);
        }

        public void AddParam(string paramName, string value)
        {
            int nameIndex = refSystem.AddString(paramName);

            CQDParamStr param = new CQDParamStr();
            if (string.IsNullOrEmpty(value))
                param.value = "";
            else
                param.value = value;
            param.nodeNameStrIndex = nameIndex;
            paramArr.Add(param);
        }

        public int GetParamValueInt(int index)
        {
            return paramArr[index].GetValueInt();
        }

        public float GetParamValueFloat(int index)
        {
            return paramArr[index].GetValueFloat();
        }

        public long GetParamValueLong(int index)
        {
            return paramArr[index].GetValueLong();
        }

        public byte GetParamValueByte(int index)
        {
            return paramArr[index].GetValueByte();
        }
        public double GetParamValueDouble(int index)
        {
            return paramArr[index].GetValueDouble();
        }
        public string GetParamValueStr(int index)
        {
            return paramArr[index].GetValueStr();
        }

        public int GetParamValueIntByName(string paramName, int defaultValue = -1)
        {
            int nameIndex = refSystem.AddString(paramName);

            for (int i = 0; i < paramArr.Count; i++)
            {
                if (paramArr[i].nodeNameStrIndex == nameIndex)
                {
                    return paramArr[i].GetValueInt();
                }
            }
            return defaultValue;
        }

        public float GetParamValueFloatByName(string paramName, float defaultValue = -1)
        {
            int nameIndex = refSystem.AddString(paramName);

            for (int i = 0; i < paramArr.Count; i++)
            {
                if (paramArr[i].nodeNameStrIndex == nameIndex)
                {
                    return paramArr[i].GetValueFloat();
                }
            }
            return defaultValue;
        }

        public long GetParamValueLongByName(string paramName, long defaultValue = -1)
        {
            int nameIndex = refSystem.AddString(paramName);

            for (int i = 0; i < paramArr.Count; i++)
            {
                if (paramArr[i].nodeNameStrIndex == nameIndex)
                {
                    return paramArr[i].GetValueLong();
                }
            }
            return defaultValue;
        }

        public double GetParamValueDoubleByName(string paramName, double defaultValue = -1)
        {
            int nameIndex = refSystem.AddString(paramName);

            for (int i = 0; i < paramArr.Count; i++)
            {
                if (paramArr[i].nodeNameStrIndex == nameIndex)
                {
                    return paramArr[i].GetValueDouble();
                }
            }
            return defaultValue;
        }

        public byte GetParamValueByteByName(string paramName, byte defaultValue = 255)
        {
            int nameIndex = refSystem.AddString(paramName);

            for (int i = 0; i < paramArr.Count; i++)
            {
                if (paramArr[i].nodeNameStrIndex == nameIndex)
                {
                    return paramArr[i].GetValueByte();
                }
            }
            return defaultValue;
        }

        public string GetParamValueStrByName(string paramName, string defaultValue = null)
        {
            int nameIndex = refSystem.AddString(paramName);

            for (int i = 0; i < paramArr.Count; i++)
            {
                if (paramArr[i].nodeNameStrIndex == nameIndex)
                {
                    return paramArr[i].GetValueStr();
                }
            }
            return defaultValue;
        }

    }

    public class CQDSystem : CQDNode
    {
        public CQDStrCol strCol = null;  //字符串节点
        public CQDDataCol dataCol = null;  //数据中心节点

        public QDList<CQDNode> mNeedRefreshNodeList = new QDList<CQDNode>(); //需要保存的列表

        //文件读写
        CQDFile mBinFilePack = new CQDFile();

        public CQDNode GetDataRootNode()
        {
            return dataCol;
        }

        public CQDDataNode FirstDataNode()
        {
            dataCol.LoadSelfChild();
            return (CQDDataNode)dataCol.refChild;
        }

        public CQDFile FilePack()
        {
            return mBinFilePack;
        }

        public void AddToSaveList(CQDNode node)
        {
            node.needSave = true;
            mNeedRefreshNodeList.AppendTail(node);
        }
        
        public CQDSystem()
        {
            refSystem = this;
            nodeType = eQDNodeType.MainNode;
        }

        public CQDNode CreateNodeByNodeType(eQDNodeType t)
        {
            switch (t)
            {
                case eQDNodeType.StringCenNode:
                    return new CQDStrCol(this);
                case eQDNodeType.StringNode:
                    return new CQDStrNode(this);
                case eQDNodeType.DataCenNode:
                    return new CQDDataCol(this);
                case eQDNodeType.DataNode:
                    return new CQDDataNode(this);
            }
            return null;
        }



        public void UpdateSave()
        {
            CQDNode node = mNeedRefreshNodeList.GetAndDelOldOne();
            while (node != null)
            {
                node.Save();
                node = mNeedRefreshNodeList.GetAndDelOldOne();
            }
            FilePack().Flush();
        }

        public void UpdateBack()
        {
            //
            FilePack().Flush();
            FilePack().CloseFile();

            string newpath = System.Environment.CurrentDirectory +"\\backup\\"
               +"back_"
                 +System.DateTime.Now.Year.ToString() + "_"
            + System.DateTime.Now.Month.ToString() + "_"
            + System.DateTime.Now.Day.ToString() + "_"
            + System.DateTime.Now.Hour.ToString() + "_"
            + System.DateTime.Now.Minute.ToString() + "_"
             + System.DateTime.Now.Second.ToString() +".data";

            File.Copy(FilePack()._filePath, newpath);

            FilePack().OpenFile(false);

        }

        public void UpdateBack5()
        {
            string newpath = System.Environment.CurrentDirectory + "\\backup\\"
               + "back_5.data";
            if(File.Exists(newpath))
                File.Delete(newpath);

            FilePack().Flush();
            FilePack().CloseFile();

            File.Copy(FilePack()._filePath, newpath);

            FilePack().OpenFile(false);
        }

        public void RemoveNode(CQDNode node)
        {
            if(node.refPre!=null)
            {
                node.refPre.BrotherOrChildRemoveNotice(node);
            }
        }

        public void RemoveNode(CQDNode rootNode, string[] name)
        {
            if (rootNode == null)
                rootNode = dataCol;
            else if (rootNode.nodeType != eQDNodeType.DataCenNode && rootNode.nodeType != eQDNodeType.DataNode)
                return ;

            for (int i = 0; i < name.Length; i++)
            {
                int nameIndex = -1;
                if (!string.IsNullOrEmpty(name[i]))
                    nameIndex = strCol.GetStrIndex(name[i]);

                CQDNode child = rootNode.refChild;
                while (child != null)
                {
                    if (child.nodeNameIndex == nameIndex)
                    {
                        rootNode = child;
                        break;
                    }
                    else
                        child = child.refBrother;
                }
            }

            if (rootNode != null && rootNode.nodeNameIndex == strCol.GetStrIndex(name[name.Length - 1]))
            {
                if (rootNode.refPre != null)
                    rootNode.refPre.BrotherOrChildRemoveNotice(rootNode);
            }
        }


        public void Init(string filePath, bool reNewFile = false)
        {
            strCol = (CQDStrCol)CreateNodeByNodeType(eQDNodeType.StringCenNode);
            AddChild(strCol);

            dataCol = (CQDDataCol)CreateNodeByNodeType(eQDNodeType.DataCenNode);
            strCol.AddBrother(dataCol);

            AddToSaveList(this);
            AddToSaveList(strCol);
            AddToSaveList(dataCol);

            //file
            mBinFilePack._filePath = filePath;
            mBinFilePack.OpenFile(reNewFile);
        }

        public CQDDataNode CreateNode(CQDDataNode father, string[] nameArr)
        {
            if (nameArr == null)
                return null;

            CQDNode node = father ?? (dataCol as CQDNode);

            //  前三个结点找不到就创建,第四个直接创建,不管找不找得到
            for (int i = 0; i < nameArr.Length; i++)
            {
                int strIndex = strCol.AddStr(nameArr[i]);
                CQDNode n = (i == nameArr.Length - 1) ? null : node.FindChild(strIndex);
                if (n == null)
                {
                    n = CreateNodeByNodeType(eQDNodeType.DataNode);
                    n.nodeNameIndex = strIndex;
                    node.AddChild(n);

                    AddToSaveList(n);

                    n.isOpened = true;
                }
                node = n;
            }


            return (CQDDataNode)node;
        }


        public CQDDataNode CreateNode(CQDDataNode father, string name, CQDDataNode.readParamFUNC readFunc, CQDDataNode.readyParamFUNC readyFunc)
        {
            CQDNode fatherNode = father;
            if (fatherNode == null)
                fatherNode = dataCol;

            CQDDataNode n = (CQDDataNode)CreateNodeByNodeType(eQDNodeType.DataNode);

            int nameIndex = -1;
            if (!string.IsNullOrEmpty(name))
                nameIndex = strCol.AddStr(name);
            n.nodeNameIndex = nameIndex;

            fatherNode.AddChild(n);

            AddToSaveList(n);

            n.mReadParamFunc = readFunc;
            n.mReadyParamFunc = readyFunc;

            n.isOpened = true;

            return n;
        }

        public CQDDataNode GetNode(CQDNode rootNode, string name)
        {
            if (rootNode == null)
                rootNode = dataCol;
            else if (rootNode.nodeType != eQDNodeType.DataCenNode && rootNode.nodeType != eQDNodeType.DataNode)
                return null;

            int nameIndex = -1;
            if (!string.IsNullOrEmpty(name))
                nameIndex = strCol.GetStrIndex(name);

            CQDNode child = rootNode.refChild;
            while (child != null)
            {
                if (child.nodeNameIndex == nameIndex)
                    return (CQDDataNode)child;
                else
                    child = child.refBrother;
            }
            return null;
        }

        public CQDNode GetNode(CQDNode rootNode, string [] name)
        {
            if (rootNode == null)
                rootNode = dataCol;
            else if (rootNode.nodeType != eQDNodeType.DataCenNode && rootNode.nodeType != eQDNodeType.DataNode)
                return null;

            if (name.Length == 1 && name[0] == "Root")
                return rootNode;

            for (int i=0; i<name.Length; i++)
            {
                int nameIndex = -1;
                if (!string.IsNullOrEmpty(name[i]))
                    nameIndex = strCol.GetStrIndex(name[i]);

                CQDNode child = rootNode.refChild;
                while (child != null)
                {
                    if (child.nodeNameIndex == nameIndex)
                    {
                        rootNode = child;
                        break;
                    }
                    else
                        child = child.refBrother;
                }
            }

            if (rootNode?.nodeNameIndex == strCol.GetStrIndex(name[name.Length - 1]))
                return rootNode;
            else
                return null;
        }

        //返回数据中心节点下第一个数据节点
        public CQDDataNode FirstNode()
        {
            return (CQDDataNode)dataCol.refChild;

        }

        public void SaveThisNode(CQDNode node)
        {
            AddToSaveList(node);
        }

        public int AddString(string str)
        {
            return strCol.AddStr(str);
        }

        public int GetStringIndex(string str)
        {
            return strCol.GetStrIndex(str);
        }

        public string GetStringByIndex(int index)
        {
            return strCol.GetStr(index);
        }

        public void Close()
        {
            UpdateSave();
            mBinFilePack.CloseFile();
        }

        public void Load()
        {
            //默认的载入部分
            LoadMainNode(this);
            LoadSelfChild();
            strCol = (CQDStrCol)refChild;
            strCol.LoadSelfChild();
            strCol.ReSetData(); //字符串重整数据

            if (strCol.refBrother != null)
                dataCol = (CQDDataCol)strCol.refBrother;

            dataCol.LoadSelfChild();
        }

    }
}
