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
        uiMgr.PushPanel(UIPanelType.register);
    }
    void Init()
    {
        //SocketManager socketMgr = new SocketManager(this);
        UIManager uiMgr = new UIManager(this);
        CameraManager cameraMgr = new CameraManager(this);
        PlayerManager playerMgr = new PlayerManager(this);
        AudioManager audioMgr = new AudioManager(this);
        RequestManager requestMgr = new RequestManager(this);

        managerDic = new Dictionary<ManagerType , BaseManager>();
        managerDic.Add(ManagerType.AudioManager , audioMgr);
        managerDic.Add(ManagerType.PlayerManager , playerMgr);
        managerDic.Add(ManagerType.UIManager , uiMgr);
        //managerDic.Add(ManagerType.SocketManager , socketMgr);
        managerDic.Add(ManagerType.RequestManager , requestMgr);
        managerDic.Add(ManagerType.CameraManager , cameraMgr);

    }

    public BaseManager GetManager(ManagerType type)
    {
        BaseManager mgr = managerDic.TryGet(type);
        if(mgr==null)
                throw new System.Exception("没有找到对应管理器：" + type.ToString());
        return mgr;
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
