using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

/// <summary>
/// 2017-9-9
/// huyy
/// 此类用于和服务器的交互
/// </summary>
public class Client{
    private Socket clientSocket;
    private IPEndPoint ipendPoint;
    private Message msg;
    private Action<MessageData> parseMessageCallBack;   //解析消息回调
    public Client(string ip,int port)
    {
        clientSocket = new Socket(AddressFamily.InterNetwork , SocketType.Stream , ProtocolType.Tcp);
        msg = new Message();
        ipendPoint = new IPEndPoint(IPAddress.Parse(ip) , port);
        try
        {
            clientSocket.Connect(ipendPoint);
        }
        catch
        {
            throw new Exception("链接服务器出错");
        }
    }
    
    public void BeginReceive()
    {
        clientSocket.BeginReceive(msg.receiveMsg , msg.currentMsgLength , msg.msgRemainLength , SocketFlags.None , ReceiveCallBack , null);
    }

    private void ReceiveCallBack(IAsyncResult ar)
    {
        int count = clientSocket.EndReceive(ar);
        msg.UpdateCacheLength(count);
        msg.ParseMessage(parseMessageCallBack);
        BeginReceive();
    }

    public void Send(byte[] buffer)
    {
        clientSocket.Send(buffer);
    }
    
}
