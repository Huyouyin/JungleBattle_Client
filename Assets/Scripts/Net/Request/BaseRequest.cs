using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

/// <summary>
/// 2017-9-9
/// huyy
/// 所有请求的基类
/// </summary>
public abstract class BaseRequest{
    protected RequestCode requestCode;
    private SocketManager socketMgr;
    protected SocketManager SocketMgr
    {
        get
        {
            if(socketMgr==null)
            {
                socketMgr = GameFacade.instance.GetManager(ManagerType.SocketManager) as SocketManager;
            }
            return socketMgr;
        }
    }

    public abstract void HandleReqest(ActionCode action , string data);

    /// <summary>
    /// 响应服务器发送过来的消息
    /// </summary>
    /// <param name="mdata"></param>
    public abstract void OnResponse(MessageData mdata);

}
