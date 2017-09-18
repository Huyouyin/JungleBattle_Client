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
    private List<RoomItem> roomItemList;
    private Transform contentTransform;
    private Text noRoomText;
    GameObject itemPrefab;
    private RoomListPanel parentPanel;


    private GameObject ItemPrefab
    {
        get
        {
            if(itemPrefab == null)
            {
                itemPrefab = Resources.Load<GameObject>("Element/Room/roomitem");
            }
            return itemPrefab;
        }
    }

    public void InitPanel(RoomListPanel parentPanel)
    {
        this.parentPanel = parentPanel;

        float y = transform.position.y;
        transform.position = new Vector2(enterPosX , y);
        noRoomText = transform.Find("noroom").GetComponent<Text>();
        contentTransform = transform.Find("mask/content");
        roomItemList = new List<RoomItem>();

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
    private void ProcessRoomList(object roomlistArray)
    {
        List<Room> roomList = roomlistArray as List<Room>;

        if(roomList.Count == 0)
        {
            ShowNoRoomText();
            return;
        }

        foreach(Room r in roomList)
        {
            string username = r.ownerName;
            string roomid = r.roomId.ToString();
            CreateRoomItem(username , roomid);
        }
    }

    public void CreateRoomItem(string username, string roomid)
    {
        RoomItem item = Instantiate<GameObject>(ItemPrefab).GetComponent<RoomItem>();
        item.InitItem(roomid , username);
        item.transform.SetParent(contentTransform);
        if(username.Equals(PlayerManager.UserAccount.userName))
        {
            item.transform.SetAsFirstSibling();
            item.ShowMyRoom();
        }
        roomItemList.Add(item);
    }

    private void ShowNoRoomText()
    {
        noRoomText.text = "暂时没有房间";
        noRoomText.gameObject.SetActive(true);
    }

    public Tweener onExit()
    {
        if(itemPrefab != null)
        {
            Resources.UnloadAsset(itemPrefab);
        }
        return transform.DOLocalMoveX(enterPosX , exitTime);
    }
}
