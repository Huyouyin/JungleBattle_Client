using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

/// <summary>
/// 2017-9-9
/// huyy
/// 此类用于消息解析，打包等
/// </summary>
public class Message
{
    private static readonly int MAX_RECEIVE_LENGTH = 1024; //能够接受消息的最大长度
    private static readonly int MSG_HEAD_LENGTH = 4;  //接收到信息头长度
    public byte[] receiveMsg;  //收到的字节缓存
    public int currentMsgLength;   //当前缓存长度
    public int msgRemainLength;     //当前缓存剩余长度


    public Message()
    {
        receiveMsg = new byte[MAX_RECEIVE_LENGTH];
        currentMsgLength = 0;
        msgRemainLength = MAX_RECEIVE_LENGTH - currentMsgLength;
    }

    public void UpdateCacheLength(int amount)
    {
        currentMsgLength += amount;
        msgRemainLength = MAX_RECEIVE_LENGTH - currentMsgLength;
    }

    /// <summary>
    /// 左移多少位
    /// </summary>
    /// <param name="length"></param>
    private void UpdateCache(int count)
    {
        Array.Copy(receiveMsg , count , receiveMsg , 0 , MAX_RECEIVE_LENGTH - count);
        UpdateCacheLength(-count);
    }
    //解析消息
    public void ParseMessage(Action<MessageData> handleMesssage)
    {
        while(currentMsgLength > MSG_HEAD_LENGTH)
        {
            if(!Read(handleMesssage))
                break;
        }
    }

    private bool Read(Action<MessageData> handleMesssage)
    {
        int msglength = BitConverter.ToInt32(receiveMsg , 0);
        if(msglength > currentMsgLength)
        {
            return false;
        }
        int requestcode = BitConverter.ToInt32(receiveMsg , MSG_HEAD_LENGTH);
        string msg = Encoding.UTF8.GetString(receiveMsg , MSG_HEAD_LENGTH * 2 , msglength - MSG_HEAD_LENGTH * 2);
        MessageData md = new MessageData(requestcode ,msg);
        handleMesssage(md);
        UpdateCache(msglength + MSG_HEAD_LENGTH);
        return true;
    }

    //数据打包，用于发送给服务器
    public static byte[] PackData(MessageData mdata)
    {
        byte[] lengthBuffer = BitConverter.GetBytes(mdata.data.Length + MSG_HEAD_LENGTH*2);
        byte[] requestBuffer = BitConverter.GetBytes((int)mdata.requsetCode);
        byte[] actionBuffer = BitConverter.GetBytes((int)mdata.actionCode);
        byte[] strBuffer = Encoding.UTF8.GetBytes(mdata.data);

        byte[] buffer = lengthBuffer.Concat(requestBuffer).Concat(actionBuffer).Concat(strBuffer).ToArray();
        return buffer;
    }
}