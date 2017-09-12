using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class LoginRequest :BaseRequest
{
    public LoginRequest()
    {
        this.requestCode = RequestCode.LoginRequest;
    }
    public override void HandleReqest(ActionCode action , string data)
    {
        SocketMgr.SendRequest(requestCode , ActionCode.Login , data);
    }

    public override void OnResponse(MessageData mdata)
    {
        LoginResultCode resCode = (LoginResultCode)Enum.Parse(typeof(LoginResultCode) , mdata.data);
        if(resCode== LoginResultCode.Success)
        {
            Toast.ShowToast("登陆成功");
            return;
        }
        Toast.ShowToast("用户名不存在或密码不正确");
    }
}
