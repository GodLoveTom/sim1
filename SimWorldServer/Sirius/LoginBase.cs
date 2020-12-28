using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class LoginBase
{
    public PlayerBase Get(int uid)
    {
        PlayerBase r = gDefine.gPlayerBase.Find(uid);
        return null;
    }
    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="name"></param>
    /// <param name="passWord"></param>
    /// <param name="mail"></param>
    /// <param name="error">0 ok, 1 account error </param>
    /// <returns></returns>
    public PlayerBase Register(string name, string passWord,  out int error)
    {
        PlayerBase r = gDefine.gPlayerBase.Find(name);

        if(r!=null)
        {
            error = 1;
            return null;
        }

       

        r = gDefine.gPlayerBase.AddNewOne(name, passWord);
        error = 0;
        return r;
       
    }

    public byte IsNamePassOK(string name, string passWord)
    {
        byte error = 0;
        PlayerBase r = gDefine.gPlayerBase.Find(name);
        if (r == null)
            error = 1;
        else if (r.pass != passWord)
            error = 2;

        return error;
    }

    // 0 ok
    // 1 name wrong
    // 2 password wrong
    // 3 ban
    // >=100 uid 
    public PlayerBase LoginIn(string name, string passWord, ref int result)
    {
        if (gDefine.gPlayerBase.IsInForbidList(name))
        {
            result = 3;
            return null;
        }

        PlayerBase r = gDefine.gPlayerBase.Find(name);
        if (r == null)
            result = 1;
        else if (string.Compare(passWord, r.pass) == 0)
        {
            result = 0;
            
        }
        else
        {
            result = 2;
            r = null;
        }
        return r;
    }

/*

    public void SetMail(int mailid, int reason)
    {
        int itemtype = 99;
        int itemnum = 10;
        if (reason == 1)
            itemnum = 20;
        long t = System.DateTime.Now.Ticks;

        gDefine.gDataServer.UpdateMailNewData(reason, itemtype, itemnum, t, mailid);
        gDefine.gPlayerData.SetMail(reason, mailid, itemtype, itemnum, t);
    }

    public bool IsNickNameExist(string nickname)
    {
        if (mNickDict.TryGetValue(nickname, out checkResult))
            return true;
        else
            return false;
    }

    public bool IsLoginNameExist(string name)
    {
        if (mDict.TryGetValue(name, out checkResult))
            return true;
        else
            return false;
    }



    public void ChangeName(int uid, string name, string nickname, string psw)
    {
        cLoginCheck data = Get(uid);
        if (data != null)
        {
            mDict.Remove(data.userName);
            mNickDict.Remove(data.nickName);
            data.userName = name;
            data.passWord = psw;
            data.nickName = nickname;
            mDict.Add(name, data);
            mNickDict.Add(nickname, data);

        }
    }
*/





}

