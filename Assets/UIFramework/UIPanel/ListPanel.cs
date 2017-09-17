using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Common;

public class ListPanel : MonoBehaviour {


    private float originPosX = 155f;
    private float enterPosX = 941f;
    private float enterTime = 0.3f;
    private float exitTime = 0.3f;

    private Text noRoomText;

    public void InitPanel()
    {
        float y = transform.position.y;
        transform.position = new Vector2(enterPosX , y);
        noRoomText = transform.Find("noroom").GetComponent<Text>();
        noRoomText.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void onEnter()
    {
        gameObject.SetActive(true);
        transform.DOLocalMoveX(originPosX , enterTime);
        GetRoomList();
    }

    /// <summary>
    /// 取得未开始的房间列表
    /// </summary>
    private void GetRoomList()
    {
        GameFacade.instance.HandleRequest(RequestCode.Room , ActionCode.RoomListUnStart , "" , ProcessRoomList);
    }
    private void ProcessRoomList(object roomlist)
    {
        List<Room> roomList = roomlist as List<Room>;
        if(roomList.Count == 0)
        {
            ShowNoRoomText();
            return;
        }
    }


    private void ShowNoRoomText()
    {
        noRoomText.text = "暂时没有房间";
        noRoomText.gameObject.SetActive(true);
    }

    public Tweener onExit()
    {
        return transform.DOLocalMoveX(enterPosX , exitTime);
    }
}
