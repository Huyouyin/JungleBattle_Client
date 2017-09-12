using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFacade : MonoBehaviour {
    public static GameFacade instance;
    private Dictionary<ManagerType , BaseManager> managerDic;
    
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
        UIManager uiMgr = GetManager(ManagerType.UIManager) as UIManager;
        uiMgr.PushPanel(UIPanelType.start);
    }
    void Init()
    {
        MessageContent msgContent = GetComponent<MessageContent>();
        managerDic = new Dictionary<ManagerType , BaseManager>();

        RequestManager requestMgr = new RequestManager(this);
        managerDic.Add(ManagerType.RequestManager , requestMgr);
        msgContent.SetRequestMgr(requestMgr);

        SocketManager socketMgr = new SocketManager(this,msgContent);
        managerDic.Add(ManagerType.SocketManager , socketMgr);

        UIManager uiMgr = new UIManager(this);
        managerDic.Add(ManagerType.UIManager , uiMgr);

        CameraManager cameraMgr = new CameraManager(this);
        managerDic.Add(ManagerType.CameraManager , cameraMgr);

        PlayerManager playerMgr = new PlayerManager(this);
        managerDic.Add(ManagerType.PlayerManager , playerMgr);

        AudioManager audioMgr = new AudioManager(this);
        managerDic.Add(ManagerType.AudioManager , audioMgr);
    }

    public BaseManager GetManager(ManagerType type)
    {
        BaseManager mgr = managerDic.TryGet(type);
        if(mgr==null)
                throw new System.Exception("没有找到对应管理器：" + type.ToString());
        return mgr;
    }

    private void OnDestroy()
    {
        foreach (var key in managerDic.Keys)
        {
            managerDic[key].OnDestroy();
        }
    }
}
public enum ManagerType
{
     SocketManager,
     UIManager ,
     AudioManager ,
     PlayerManager ,
     RequestManager ,
     CameraManager
}
