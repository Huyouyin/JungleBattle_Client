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

    public override void HandleReqest(ActionCode action , string data , Action<object> callback)
    {
        switch(action)
        {
            case ActionCode.CreateRoom:
                RoomCreateRequest(callback , data);
                break;
            case ActionCode.RoomListUnStart:
                RoomListRequset(callback);
                break;
            default:
                break;
        }
    }

    public override void OnResponse(ActionCode action , string data)
    {
        switch(action)
        {
            case ActionCode.CreateRoom:
                OnResponseCreateRoom(data);
                break;
            case ActionCode.RoomListUnStart:
                OnResponseRoomList(data);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 请求当前未开始房间列表
    /// </summary>
    private void RoomListRequset(Action<object> callback)
    {
        GameFacade.instance.SendRequest(RequestCode.Room , ActionCode.RoomListUnStart , "" );
        AddCallBack(callback , ActionCode.RoomListUnStart);
    }

    /// <summary>
    /// 响应房间列表
    /// </summary>
    /// <param name="data"></param>
    private void OnResponseRoomList(string data)
    {
        string[] datas = data.Split(',');
        int roomCount = int.Parse(datas[0]);
        Log.i("房间个数:"+roomCount);
        //InvokeCallBack(ActionCode.RoomListUnStart , data);
    }

    /// <summary>
    /// 请求创建房间
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="userid"></param>
    private void RoomCreateRequest(Action<object> callback,string userid)
    {
        GameFacade.instance.SendRequest(RequestCode.Room , ActionCode.CreateRoom , userid);
        AddCallBack(callback,ActionCode.CreateRoom);
    }


    /// <summary>
    /// 响应创建房间
    /// </summary>
    /// <param name="data"></param>
    private void OnResponseCreateRoom(string data)
    {
        Log.i(data);
        InvokeCallBack(ActionCode.CreateRoom , data);
    }


}
