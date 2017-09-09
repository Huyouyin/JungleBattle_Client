﻿using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

/// <summary>
/// 2017-9-9
/// huyy
/// 请求管理类，管理所有客户端请求
/// </summary>
public class RequestManager : BaseManager {
    private Dictionary<RequestCode , BaseRequest> requestDic;
    public RequestManager(GameFacade facade) : base(facade)
    {
        requestDic = new Dictionary<RequestCode , BaseRequest>();
    }
    private BaseRequest GetRequest(RequestCode requestcode)
    {
        BaseRequest request = requestDic.TryGet(requestcode);
        if(request == null)
        {
            throw new Exception("没有找到对应Request--->" + requestcode.ToString());
        }
        return request;
    }

    public void HandleRequest(MessageData mdata)
    {
        BaseRequest targetrequest = GetRequest(mdata.requsetCode);
        targetrequest.OnResponse(mdata);
    }
}
