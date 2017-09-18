using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Account
{
    public int userid
    {
        get;
        private set;
    }
    public string userName
    {
        get;
        private set;
    }
    public string userPass
    {
        get;
        private set;
    }

    public Account(int id , string name , string pass)
    {
        this.userid = id;
        this.userName = name;
        this.userPass = pass;
    }

    public void SetAccount(string name , string pass)
    {
        userName = name;
        userPass = pass;
    }
    public override string ToString()
    {
        string str = string.Format("ID:{0}  用户名:{1}   用户密码:{2}" , userid , userName , userPass);
        return str;
    }
}
