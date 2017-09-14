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

    public void InitPanel()
    {
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
    }

    public Tweener onExit()
    {
        return transform.DOLocalMoveX(enterPosX , exitTime);
    }

    public void UpdatePlayerInfo()
    {
        PlayerInfo info = GameFacade.instance.GetPlayerInfo();
        usernameText.text = info.UserName;
        winCountText.text = info.WinCount.ToString();
        totalCountText.text = info.TotalCount.ToString();
    }

    private void OnClickCreate()
    {
        GameFacade.instance.HandleRequest(RequestCode.Room , ActionCode.CreateRoom , usernameText.text);
    }
    private void CreateRoomCallBack(object obj)
    {
        string datas = obj as string;
        string[] dataArray = datas.Split(',');
        RoomResultCode resCode = (RoomResultCode)Enum.Parse(typeof(RoomResultCode) , dataArray[0]);
        switch(resCode)
        {
            case RoomResultCode.CreateSuccess:
                break;
            case RoomResultCode.CreateFail:
                break;
            default:
                throw new Exception("创建房间返回码错误！！  返回码：" + resCode);
        }
        Toast.ShowToast("创建成功,房间ID:" + dataArray[1]);
    }
}
