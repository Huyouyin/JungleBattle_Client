using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RoomListPanel : BasePanel {
    private Button closeButton;
    private Vector2 originPos = new Vector2(0 , 640f);

    private Transform userPanel;

    private Text usernameText;
    private Text totalCountText;
    private Text winCountText;

    protected override void InitPanel()
    {
        transform.localPosition = originPos;
        enterTime = 0.3f;
        exitTime = 0.3f;
        base.InitPanel();
        closeButton = transform.Find("close").GetComponent<Button>();
        closeButton.onClick.AddListener(OnClickClose);
        userPanel = transform.Find("userpanel");
        usernameText = userPanel.Find("username").GetComponent<Text>();
        totalCountText = userPanel.Find("totalcount").GetComponent<Text>();
        winCountText = userPanel.Find("wincount").GetComponent<Text>();
    }

    public override void OnEnter()
    {
        transform.localPosition = originPos;
        transform.localScale = Vector2.zero;
        UpdatePlayerInfo();
        EnterTweening();
    }

    protected override void EnterTweening()
    {
        transform.DOLocalMoveY(0 , enterTime);
        transform.DOScale(1 , enterTime);
    }

    protected override void ExitTweening()
    {
        transform.DOLocalMoveY(originPos.y , exitTime);
        transform.DOScale(0 , exitTime);
    }

    private void OnClickClose()
    {
        GameFacade.instance.PopPanel();
    }

    private void UpdatePlayerInfo()
    {
        PlayerInfo info = GameFacade.instance.GetPlayerInfo();
        usernameText.text = info.UserName;
        winCountText.text = info.WinCount.ToString();
        totalCountText.text = info.TotalCount.ToString();
    }
}
