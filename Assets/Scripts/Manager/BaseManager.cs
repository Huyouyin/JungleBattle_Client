using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 2017-9-9
/// huyy
/// 此类是所有管理器的基类
/// </summary>
public abstract class BaseManager{
    protected GameFacade gameFacade;
    public BaseManager(GameFacade facade)
    {
        this.gameFacade = facade;
    }
    public virtual void OnDestroy()
    { }
}
