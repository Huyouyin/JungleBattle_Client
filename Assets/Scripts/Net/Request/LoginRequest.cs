using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class LoginRequest :BaseRequest
{
    private Action callBack;
    public LoginRequest()
    {
        this.requestCode = RequestCode.LoginRequest;
    }

    public override void HandleReqest(ActionCode action , string data , Action callback = null)
    {
        GameFacade.instance.SendRequest(requestCode , ActionCode.Login , data);
        this.callBack = callback;
    }

    public override void OnResponse(MessageData mdata)
    {
        string[] datas = mdata.data.Split(',');        
        LoginResultCode resCode = (LoginResultCode)Enum.Parse(typeof(LoginResultCode) , datas[0]);
        if(resCode== LoginResultCode.Success)
        {
            Toast.ShowToast("登陆成功");
            int totalcount = int.Parse(datas[1]);
            int wincount = int.Parse(datas[2]);
            //Log.i("总场数：" + totalcount + "  胜场:" + wincount);
            GameFacade.instance.SetPlayerGameCount( totalcount , wincount);
            if(this .callBack != null)
            {
                this.callBack.Invoke();
                callBack = null;
            }
            return;
        }
        Toast.ShowToast("用户名不存在或密码不正确");
    }
}
