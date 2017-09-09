using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFacade : MonoBehaviour {
    private SocketManager socketMgr;
    private UIManager uiMgr;
    private AudioManager audioMgr;
    private PlayerManager playerMgr;
    private RequestManager requestMgr;
    private CameraManager cameraMgr;

	// Use this for initialization
	void Start () {
		
	}
	
	void Init()
    {
        socketMgr = new SocketManager(this);
        uiMgr = new UIManager(this);
        cameraMgr = new CameraManager(this);
        playerMgr = new PlayerManager(this);
        audioMgr = new AudioManager(this);
        requestMgr = new RequestManager(this);
    }

    public void HandleMessage(MessageData mdata)
    {
        requestMgr.HandleRequest(mdata);
    }
}
