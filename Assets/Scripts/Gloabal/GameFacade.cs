using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;

public class GameFacade :MonoBehaviour
{
    public static GameFacade instance;

    MessageContent msgContent;

    RequestManager requestMgr;
    SocketManager socketMgr;
    UIManager uiMgr;
    CameraManager cameraMgr;
    PlayerManager playerMgr;
    AudioManager audioMgr;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        Init();
    }
    private void Start()
    {
        uiMgr.PushPanel(UIPanelType.start);
    }
    void Init()
    {
        msgContent = GetComponent<MessageContent>();

        requestMgr = new RequestManager(this);

        socketMgr = new SocketManager(this);

        uiMgr = new UIManager(this);

        cameraMgr = new CameraManager(this);

        playerMgr = new PlayerManager(this);

        audioMgr = new AudioManager(this);
    }

    public void EnqueueMsg(MessageData mdata)
    {
        msgContent.EnqueueMsg(mdata);
    }

    public void ShowToast(string msg , float duringTime = 1f)
    {
        uiMgr.ShowToast(msg , duringTime);
    }

    public void SendRequest(RequestCode reCode , ActionCode acCode ,string data)
    {
        socketMgr.SendRequest(reCode , acCode , data);
    }

    public void SetPlayerGameCount(int total,int win)
    {
       playerMgr.SetGameCount(total , win);
    }
    public void SetAccount(string name , string pass)
    {
        playerMgr.SetAccount(name, pass);
    }


    public void PopPanel()
    {
        uiMgr.PopPanel();
    }

    public void PushPanel(UIPanelType paneltype)
    {
        uiMgr.PushPanel(paneltype);
    }

    public void PlaySound(SoundType soundtype)
    {
        audioMgr.PlaySound(soundtype);
    }
    public void PlayBg(SoundType soundtype)
    {
        audioMgr.PlayBg(soundtype);
    }

    public void HandleRequest(RequestCode reCode, ActionCode acCode ,string data,Action<object> callback=null)
    {
        requestMgr.HandleRequest(reCode , acCode , data, callback);
    }

    public void OnResponse(MessageData mdata)
    {
        requestMgr.OnResponse(mdata);
    }
    
    public PlayerInfo GetPlayerInfo()
    {
        return playerMgr.GetPlayerInfo();
    }

    public void ShowWait()
    {
        uiMgr.PushPanel(UIPanelType.wait);
    }

    private void OnDestroy()
    {
        requestMgr.OnDestroy();
        socketMgr.OnDestroy();
        uiMgr.OnDestroy();
        cameraMgr.OnDestroy();
        playerMgr.OnDestroy();
        audioMgr.OnDestroy();
    }
}
