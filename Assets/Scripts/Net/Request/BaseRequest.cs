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
    protected Dictionary<ActionCode,Action<object>> callBackdic;

    public abstract void HandleReqest(ActionCode action , string data , Action<object> callback= null);

    /// <summary>
    /// 响应服务器发送过来的消息
    /// </summary>
    /// <param name="mdata"></param>
    public abstract void OnResponse(ActionCode action , string data);

    protected void InvokeCallBack(ActionCode action,object obj)
    {
        Action<object> callback = callBackdic.TryGet(action);
        if(callback != null)
        {
            callback.Invoke(obj);
            callBackdic.Remove(action);
        }
    }

    protected void AddCallBack(Action<object> callback, ActionCode action)
    {
        if(callback != null)
        {
            if(callBackdic.ContainsKey(action))
            {
                //！！！
                //加入代码执行到这里，说明发起了多次请求，
                //必须修改代码，否则当一次请求回来之后，回调执行之后被置空，不会有第二次的回调了
                throw new Exception("已经存在该类型的回调");
            }
        }
    }

}
