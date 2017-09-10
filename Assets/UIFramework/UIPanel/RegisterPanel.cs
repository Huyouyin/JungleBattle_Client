using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RegisterPanel : BasePanel {
    private Button closeButton;
    private Vector2 originPos = new Vector2(1024 , 0);
    public override void OnEnter()
    {
        base.OnEnter();
        closeButton = transform.Find("close").GetComponent<Button>();
        closeButton.onClick.AddListener(OnClickClose);
        EnterTweening();
    }
    private void EnterTweening()
    {
        transform.localScale = Vector2.zero;
        transform.localPosition = new Vector3(1024 , 0);
        transform.DOScale(1 , enterTime);
        transform.DOLocalMove(Vector2.zero , enterTime).OnComplete(() => {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        });
    }

    private void OnClickClose()
    {
        transform.DOScale(0 , exitTime);
        transform.DOLocalMove(originPos , exitTime).OnComplete(() => {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        });
    }

}
