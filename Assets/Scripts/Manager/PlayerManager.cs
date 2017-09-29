using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : BaseManager {

    static Account userAccount;
    public static Account UserAccount
    {
        get
        {
            return userAccount;
        }
    }
    static CompetiticonCount competitonCount;
    public static CompetiticonCount UserCompetitonCount
    {
        get
        {
            return competitonCount;
        }
    }

    public static string myWaitingRoomID
    {
        get;
        private set;
    }

    public PlayerManager(GameFacade facade) : base(facade)
    {
    }

    public void SetCompetiticonCount(CompetiticonCount ccount)
    {
        competitonCount = ccount;
    }
    public void SetAccount(Account account)
    {
        userAccount = account;
    }
    public void SetWaitingRoomID(string roomid)
    {
        myWaitingRoomID = roomid;
    }
    public Account GetAccout()
    {
        return userAccount;
    }
    public CompetiticonCount getCompetitionCount()
    {
        return competitonCount;
    }
}

 public class CompetiticonCount
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
    
    public CompetiticonCount(int total,int win)
    {
        SetGameCount(total , win);
    }
    public void SetGameCount(int total , int win)
    {
        WinCount = win;
        TotalCount = total;
    }    
}
