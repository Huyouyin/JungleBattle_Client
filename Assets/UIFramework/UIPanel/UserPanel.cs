using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UserPanel : MonoBehaviour {

    private Text usernameText;
    private Text totalCountText;
    private Text winCountText;
    private float originPosX = -307.7f;
    private float enterPosX = -779;
    private float enterTime = 0.3f;
    private float exitTime = 0.3f;

    public void InitPanel()
    {
        usernameText = transform.Find("username").GetComponent<Text>();
        totalCountText = transform.Find("totalcount").GetComponent<Text>();
        winCountText = transform.Find("wincount").GetComponent<Text>();
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
}
