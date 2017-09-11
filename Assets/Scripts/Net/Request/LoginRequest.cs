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
        socketMgr.SendRequest(requestCode , ActionCode.Login , data);
    }

    public override void OnResponse(MessageData mdata)
    {
        ResultCode resCode = (ResultCode)Enum.Parse(typeof(ResultCode) , mdata.data);
        if(resCode==ResultCode.Success)
        {

            Toast.ShowToast("登陆成功");

            return;
        }
        Toast.ShowToast("登陆失败");
    }
}
