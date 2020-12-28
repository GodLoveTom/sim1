
public class CSirDefine
{
    public enum eSirRace
    {
        Null=0,
        Viking,
        Monster,
        Savage,
        Dragon,
        Count,
    }

    public enum eProduceType
    {
        Null=0,
        Wood,
        Food,
        Stone,
        Gold,
        Iron,
    }

    public enum eMsgWorldObjType
    {
        Tree = 0,
        Torch = 1,
    }

    public enum eElement
    {
        Fire=0,
        Cold=1,
        Posion=2,
        Lighting=3,
    }

    public enum eClass
    {
        Worrior=0, //战士
        Warlock,//术士
        Shaman,//撒满
        Mage,//法师
        Paladin,//圣骑士
        Hunter,//猎人
        Druid,//德鲁伊
        Rogue,//盗贼
        Priest,//牧师
        Archer, //射手
        King, // 玩家角色
    }

    public enum eItemType
    {
        Null =0,
        Weapon=1, //武器
        ResCard,  //资源卡
        ComItem,  //普通物品  
    }

    public enum eKingEquipGird
    {
        Staff0 = 0,
        Staff1,
        Staff2,
        Staff3,

        Item0,
        Item1,
        Item2,
        Item3,
        Count,
    }

    public static long def_MapTileWidth = 90; //一个块的大小
    public static long def_MapTileHeight = 60; //一个块的大小
}



