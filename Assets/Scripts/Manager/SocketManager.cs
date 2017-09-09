using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class SocketManager : BaseManager {
    private readonly string IP = "127.0.0.1";
    private readonly int PORT = 9527;
    private Client client;
    public SocketManager(GameFacade facade):base(facade)
    {
        client = new Client(IP , PORT);
        client.BeginReceive();
    }
    /// <summary>
    /// 每解析出来一条消息则会回掉此消息
    /// </summary>
    /// <param name="mdata"></param>
    private void ParseMessageCallBack(MessageData mdata)
    {
        gameFacade.HandleMessage(mdata);
    }
}
