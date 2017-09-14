using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;

/// <summary>
/// 2017-9-9
/// huyy
/// 所有请求的基类
/// </summary>
public abstract class BaseRequest{
    protected RequestCode requestCode;

    public abstract void HandleReqest(ActionCode action , string data , Action callback= null);

    /// <summary>
    /// 响应服务器发送过来的消息
    /// </summary>
    /// <param name="mdata"></param>
    public abstract void OnResponse(ActionCode action , string data);

}
