using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class RoomRequest : BaseRequest {

    public RoomRequest()
    {
        this.callBackdic = new Dictionary<ActionCode , Action<object>>();
        this.requestCode = RequestCode.Room;
    }

    public override void HandleReqest(ActionCode action , string data , Action<object> callback = null)
    {
        
    }

    public override void OnResponse(ActionCode action , string data)
    {
        throw new NotImplementedException();
    }
}
