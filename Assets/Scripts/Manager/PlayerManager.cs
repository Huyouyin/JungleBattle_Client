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
    

    public void SetGameCount(int total , int win)
    {
        WinCount = win;
        TotalCount = total;
    }    
}
