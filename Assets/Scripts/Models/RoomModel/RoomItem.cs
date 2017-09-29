using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour {
    private Text roomIDText;
    private Text ownerNameText;
    private Button joinButton;
    private Button myroomButton;

    public void InitItem(string roomid,string ownername)
    {
        roomIDText = transform.Find("idroom").GetComponent<Text>();
        ownerNameText = transform.Find("ownername").GetComponent<Text>();
        joinButton = transform.Find("joinButton").GetComponent<Button>();
        myroomButton = transform.Find("myroom").GetComponent<Button>();

        joinButton.onClick.AddListener(OnClickJoin);
        myroomButton.onClick.AddListener(OnClickMyRoom);
        SetItem(roomid , ownername);
        ShowMyRoom(false);
    }
    public void SetItem(string roomid,string ownername)
    {
        roomIDText.text = roomid;
        ownerNameText.text = ownername;
    }

    private void OnClickJoin()
    {
        Log.i("加入：" + roomIDText.text);
    }
	
    private void OnClickMyRoom()
    {
        GameFacade.instance.PushPanel(UIPanelType.waitingjoin);
    }

    public void ShowMyRoom(bool isShowmyroom = true)
    {
        myroomButton.gameObject.SetActive(isShowmyroom);
        joinButton.gameObject.SetActive(!isShowmyroom);
    }
    
}
