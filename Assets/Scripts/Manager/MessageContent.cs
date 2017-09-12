using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 2017-9-12
/// huyy
/// 消息容器用于解决，socket 回调无法调用Unity API的问题
/// </summary>
public class MessageContent:MonoBehaviour{
    private Queue<MessageData> msgQueue;//消息队列
    private RequestManager requestMgr;

    private void Awake()
    {
        msgQueue = new Queue<MessageData>();
    }

    public void EnqueueMsg(MessageData mdata)
    {
        Log.i("收到消息:" + mdata.requsetCode + " , " + mdata.actionCode + " , " + mdata.data);
        msgQueue.Enqueue(mdata);
    }

    private void Update()
    {
        if(msgQueue.Count > 0)
        {
            MessageData mdata = msgQueue.Dequeue();
            requestMgr.OnResponse(mdata);
        }
    }

    public void SetRequestMgr(RequestManager mgr)
    {
        this.requestMgr = mgr;
    }
}
