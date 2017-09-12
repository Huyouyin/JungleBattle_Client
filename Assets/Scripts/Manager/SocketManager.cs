using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using Common;

public class SocketManager : BaseManager {
    private readonly string IP = "127.0.0.1";
    private readonly int PORT = 9527;
    private Client client;
    private MessageContent msgContent;

    public SocketManager(GameFacade facade,MessageContent msgContent):base(facade)
    {
        this.msgContent = msgContent;
        client = new Client(IP , PORT,ParseMessageCallBack);
        client.BeginReceive();
    }
    /// <summary>
    /// 每解析出来一条消息则会回掉此消息
    /// </summary>
    /// <param name="mdata"></param>
    private void ParseMessageCallBack(MessageData mdata)
    {
        msgContent.EnqueueMsg(mdata);
    }
    
    public void SendRequest(RequestCode reCode,ActionCode acCode,string data)
    {
        client.SendMessage(reCode , acCode , data);
    }

    public override void OnDestroy()
    {
        client.OnDestroy();
    }
}
