using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RegisterPanel : BasePanel {
    private Button closeButton;
    private Vector2 originPos = new Vector2(1024 , 0);

    protected override void InitPanel()
    {
        base.InitPanel();
        closeButton = transform.Find("close").GetComponent<Button>();
        closeButton.onClick.AddListener(OnClickClose);
    }
    public override void OnEnter()
    {
        base.OnEnter();
    }
    protected override void EnterTweening()
    {
        transform.localScale = Vector2.zero;
        transform.localPosition = new Vector3(1024 , 0);
        transform.DOScale(1 , enterTime);
        transform.DOLocalMove(Vector2.zero , enterTime).OnComplete(() => {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        });
    }
    protected override void ExitTweening()
    {
        base.ExitTweening();
        transform.DOLocalMove(originPos , exitTime);
        exitTweener = transform.DOScale(0 , exitTime);
    }
    private void OnClickClose()
    {
        uiMgr.PopPanel();
        exitTweener.OnComplete(() => {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            uiMgr.PushPanel(UIPanelType.login);
        });
    }

}
