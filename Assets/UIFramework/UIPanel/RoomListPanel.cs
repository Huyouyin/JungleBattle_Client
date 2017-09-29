using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RoomListPanel : BasePanel {
    private Button closeButton;
    private Vector2 originPos = new Vector2(0 , 640f);
    
    private UserPanel userPanel;
    private ListPanel listPanel;

    protected override void InitPanel()
    {
        transform.localPosition = originPos;
        enterTime = 0.3f;
        exitTime = 0.3f;
        base.InitPanel();
        closeButton = transform.Find("close").GetComponent<Button>();
        userPanel = transform.Find("userpanel").GetComponent<UserPanel>();
        listPanel = transform.Find("listpanel").GetComponent<ListPanel>();

        userPanel.InitPanel(this);
        listPanel.InitPanel(this);
        closeButton.onClick.AddListener(OnClickClose);
    }

    protected override void ResetPanel()
    {
        gameObject.SetActive(true);
        transform.localPosition = originPos;
        transform.localScale = Vector2.zero;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        enterTweener.OnComplete(() =>
        {
            userPanel.onEnter();
            listPanel.onEnter();
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        });
    }

    protected override void EnterTweening()
    {
        transform.DOLocalMoveY(0 , enterTime);
        enterTweener = transform.DOScale(1 , enterTime);
    }

    protected override void ExitTweening()
    {
        Tweener tmptween = userPanel.onExit();
        listPanel.onExit();
        tmptween.OnComplete(() =>
        {
            userPanel.gameObject.SetActive(false);
            transform.DOLocalMoveY(originPos.y , exitTime);
            exitTweener = transform.DOScale(0 , exitTime);
        });
    }

    private void OnClickClose()
    {
        GameFacade.instance.PopPanel();
        exitTweener.OnComplete(() =>
        {
            gameObject.SetActive(false);
            GameFacade.instance.PushPanel(UIPanelType.login);
        });
    }

    public void CreateRoomItem(string roomid,string username)
    {
        listPanel.CreateRoomItem(username , roomid);
    }
}
