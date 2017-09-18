using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Common;
using System;

public class UserPanel : MonoBehaviour {

    private Text usernameText;
    private Text totalCountText;
    private Text winCountText;
    private Button createButton;
    private float originPosX = -307.7f;
    private float enterPosX = -779;
    private float enterTime = 0.3f;
    private float exitTime = 0.3f;
    private Account userAccount;
    private RoomListPanel parentPanel;

    public void InitPanel(RoomListPanel parentPanel)
    {
        this.parentPanel = parentPanel;
        usernameText = transform.Find("username").GetComponent<Text>();
        totalCountText = transform.Find("totalcount").GetComponent<Text>();
        winCountText = transform.Find("wincount").GetComponent<Text>();
        createButton = transform.Find("createButton").GetComponent<Button>();

        createButton.onClick.AddListener(OnClickCreate);
        float y = transform.position.y;
        transform.position = new Vector2(enterPosX , y);
        gameObject.SetActive(false);
    }

    public void onEnter()
    {
        gameObject.SetActive(true);
        transform.DOLocalMoveX(originPosX , enterTime);
        userAccount = GameFacade.instance.GetAccount();
        usernameText.text = userAccount.userName;
        GetBattleCount();
    }

    private void GetBattleCount()
    {
        GameFacade.instance.HandleRequest(RequestCode.User , ActionCode.BattleCount , userAccount.userid.ToString() , ProcessBattleCount);
    }

    private void ProcessBattleCount(object obj)
    {
        string data = obj as string;
        string[] datas = data.Split(',');
        UpdateBattleCount(datas[0] , datas[1]);
    }

    private void UpdateBattleCount(string totalcount,string wincount)
    {
        totalCountText.text = totalcount;
        winCountText.text = wincount;
    }

    public Tweener onExit()
    {
        return transform.DOLocalMoveX(enterPosX , exitTime);
    }

    private void OnClickCreate()
    {
        GameFacade.instance.HandleRequest(RequestCode.Room , ActionCode.CreateRoom , userAccount.userName.ToString(),CreateRoomCallBack);
    }
    private void CreateRoomCallBack(object obj)
    {
        string datas = obj as string;
        Log.i(datas);
        string[] dataArray = datas.Split(',');
        CreateRoomResultCode resCode = (CreateRoomResultCode)Enum.Parse(typeof(CreateRoomResultCode) , dataArray[0]);
        switch(resCode)
        {
            case CreateRoomResultCode.CreateSuccess:
                Toast.ShowToast("创建成功,房间ID:" + dataArray[1],2f);
                parentPanel.CreateRoomItem(dataArray[1] , PlayerManager.UserAccount.userName);
                break;
            case CreateRoomResultCode.CreateFail:
                break;
            case CreateRoomResultCode.RepeatCreate:
                Toast.ShowToast("你已经创建了房间，解散前不能重复创建",2f);
                break;
            default:
                throw new Exception("创建房间返回码错误！！  返回码：" + resCode);
        }
    }
}
