using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class MessageData{

    public RequestCode requsetCode;
    public ActionCode actionCode;
    public string data;
    public MessageData(int request , int action , string data)
    {
        this.requsetCode = (RequestCode)request;
        this.actionCode = (ActionCode)action;
        this.data = data;
    }
    public MessageData(RequestCode request , ActionCode action , string data)
    {
        this.requsetCode = request;
        this.actionCode = action;
        this.data = data;
    }
    public MessageData(int request , string data)
    {
        this.requsetCode = (RequestCode)request;
        this.actionCode = ActionCode.None;
        this.data = data;
    }
}
