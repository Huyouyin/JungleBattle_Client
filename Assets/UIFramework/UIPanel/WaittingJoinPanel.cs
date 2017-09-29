using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WaittingJoinPanel : BasePanel {
    private RivalWaitingJoinPanel rivalPanel;
    private MyWaitingJoinPanel myPanel;
    private Button closeButton;
    private float originY = 555f;

    protected override void InitPanel()
    {
        base.InitPanel();
        rivalPanel = transform.Find("rivalWaitingPanel").GetComponent<RivalWaitingJoinPanel>();
        myPanel = transform.Find("myWaitingPanel").GetComponent<MyWaitingJoinPanel>();
        closeButton = transform.Find("close").GetComponent<Button>();
        enterTime = 1f;
        exitTime = 0.5f;

        rivalPanel.InitPanel();
        myPanel.InitPanel();
        closeButton.onClick.AddListener(OnClickClose);
        transform .localPosition =new Vector2(0,originY);
    }
    protected override void ResetPanel()
    {
        gameObject.SetActive(true);
    }
    
    protected override void EnterTweening()
    {
        enterTweener = transform.DOLocalMoveY(0 , enterTime).SetEase(Ease.OutBounce);
    }

    private void OnClickClose()
    {
        GameFacade.instance.PopPanel();
        exitTweener.OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
    protected override void ExitTweening()
    {
        exitTweener = transform.DOLocalMoveY(originY , exitTime).SetEase(Ease.Linear);
    }
}
