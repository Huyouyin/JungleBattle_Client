using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class UserRequest :BaseRequest
{
    private Action loginCallBack;
    public UserRequest()
    {
        this.requestCode = RequestCode.User;
    }

    public override void HandleReqest(ActionCode action , string data , Action callback = null)
    {
        switch(action)
        {
            case ActionCode.Login:
                RequestLogin(data , callback);
                break;
            case ActionCode.Register:
                RequestRegister(data , callback);
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
            default:
                throw new Exception("没有找到对应响应：" + action.ToString());
        }
    }

    //登陆请求
    private void RequestLogin(string data , Action callback = null)
    {
        GameFacade.instance.SendRequest(requestCode , ActionCode.Login , data);
        this.loginCallBack = callback;
    }
    //注册请求
    private void RequestRegister(string data , Action callback = null)
    {
        GameFacade.instance.SendRequest(requestCode , ActionCode.Register , data);
    }

    //--------------------------------------------------------------------------------------------------------------------------
    //下面对应响应
    //--------------------------------------------------------------------------------------------------------------------------

    //登陆响应
    private void OnResponseLogin(string data)
    {
        string[] datas = data.Split(',');
        LoginResultCode resCode = (LoginResultCode)Enum.Parse(typeof(LoginResultCode) , datas[0]);
        if(resCode == LoginResultCode.Success)
        {
            Toast.ShowToast("登陆成功");
            int totalcount = int.Parse(datas[1]);
            int wincount = int.Parse(datas[2]);
            //Log.i("总场数：" + totalcount + "  胜场:" + wincount);
            GameFacade.instance.SetPlayerGameCount(totalcount , wincount);
            if(this.loginCallBack != null)
            {
                this.loginCallBack.Invoke();
                loginCallBack = null;
            }
            return;
        }
        Toast.ShowToast("用户名不存在或密码不正确");
    }
    //注册响应
    private void OnResponseRegister(string data)
    {
        RegisterResultCode resCode = (RegisterResultCode)Enum.Parse(typeof(RegisterResultCode) , data);
        switch(resCode)
        {
            case RegisterResultCode.Success:
                Toast.ShowToast("注册成功");
                break;
            case RegisterResultCode.Fail:
                Toast.ShowToast("注册失败");
                break;
            case RegisterResultCode.AlreadyExit:
                Toast.ShowToast("用户名重复");
                break;
            default:
                throw new Exception("返回码出错" + resCode);
        }
    }

}
