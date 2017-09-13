using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : BaseManager {

    PlayerInfo playerInfo;

    public PlayerManager(GameFacade facade) : base(facade)
    {
        playerInfo = new PlayerInfo();
    }

    public void SetGameCount(int total , int win)
    {
        playerInfo.SetGameCount(total , win);
    }
    public void SetAccount(string name , string pass)
    {
        playerInfo.SetAccount(name , pass);
    }

    public PlayerInfo GetPlayerInfo()
    {
        return playerInfo;
    }
}

 public class PlayerInfo
{
    public int TotalCount
    {
        get;
        private set;
    }
    public int WinCount
    {
        get;
        private set;
    }
    public string UserName
    {
        get;
        private set;
    }
    public string UserPass
    {
        get;
        private set;
    }

    public void SetGameCount(int total , int win)
    {
        WinCount = win;
        TotalCount = total;
    }
    public void SetAccount(string name , string pass)
    {
        UserName = name;
        UserPass = pass;
    }
}
