using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class RegisterRequest : BaseRequest {
    public RegisterRequest()
    {
        this.requestCode = RequestCode.RegisterRequest;
    }
    public override void HandleReqest(ActionCode action , string data)
    {
        SocketMgr.SendRequest(requestCode , ActionCode.Register , data);
    }

    public override void OnResponse(MessageData mdata)
    {
        RegisterResultCode resCode = (RegisterResultCode)Enum.Parse(typeof(RegisterResultCode) , mdata.data);
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
