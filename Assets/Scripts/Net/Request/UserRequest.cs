using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class UserRequest :BaseRequest
{
    public UserRequest()
    {
        this.requestCode = RequestCode.User;
        callBackdic = new Dictionary<ActionCode , Action<object>>();
    }

    public override void HandleReqest(ActionCode action , string data , Action<object> callback )
    {
        switch(action)
        {
            case ActionCode.Login:
                RequestLogin(data , callback);
                break;
            case ActionCode.Register:
                RequestRegister(data , callback);
                break;
            case ActionCode.BattleCount:
                RequestBattleCount(data,callback);
                break;
            default:
                throw new Exception("没有找到对应方法：" + action.ToString());
        }
    }

    public override void OnResponse(ActionCode action , string data)
    {
        switch(action)
        {
            case ActionCode.Login:
                OnResponseLogin(data);
                break;
            case ActionCode.Register:
                OnResponseRegister(data);
                break;
            case ActionCode.BattleCount:
                OnResponseBattleCount(data);
                break;
            default:
                throw new Exception("没有找到对应响应：" + action.ToString());
        }
    }

    //登陆请求
    private void RequestLogin(string data , Action<object> callback )
    {
        GameFacade.instance.SendRequest(requestCode , ActionCode.Login , data);
        AddCallBack(callback,ActionCode.Login);
    }
    //注册请求
    private void RequestRegister(string data , Action<object> callback )
    {
        GameFacade.instance.SendRequest(requestCode , ActionCode.Register , data);
        AddCallBack(callback , ActionCode.Register);
    }
    //战斗次数请求
    private void RequestBattleCount(string data ,Action<object> callback )
    {
        GameFacade.instance.SendRequest(requestCode , ActionCode.BattleCount , data);
        AddCallBack(callback , ActionCode.BattleCount);
    }
    //--------------------------------------------------------------------------------------------------------------------------
    //下面对应响应
    //--------------------------------------------------------------------------------------------------------------------------

    //登陆响应
    private void OnResponseLogin(string data)
    {
        string[] datas = data.Split(' ');
        LoginResultCode resCode = (LoginResultCode)Enum.Parse(typeof(LoginResultCode) , datas[0]);
        if(resCode == LoginResultCode.Success)
        {
            int userid = int.Parse(datas[1]);
            string username = datas[2];
            string userpass = datas[3];
            Account account = new Account(userid , username , userpass);
            InvokeCallBack(ActionCode.Login, account);
        }
    }
    //注册响应
    private void OnResponseRegister(string data)
    {
        InvokeCallBack(ActionCode.Register , data);
    }
    
    //战斗次数响应
    private void OnResponseBattleCount(string data)
    {
        InvokeCallBack(ActionCode.BattleCount , data);
    }

}
