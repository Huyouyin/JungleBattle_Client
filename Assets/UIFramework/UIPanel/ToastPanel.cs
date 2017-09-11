using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ToastPanel : BasePanel {
    private float duringTime = 1f;
    private Text msg;
    protected override void InitPanel()
    {
        base.InitPanel();
        this.msg = transform.Find("message").GetComponent<Text>();
        canvasGroup.alpha = 0;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        
        transform.SetAsLastSibling();
        EnterTweening();
    }
    /// <summary>
    /// 进入动画
    /// </summary>
    protected override void EnterTweening()
    {
        canvasGroup.DOFade(1f , enterTime).OnComplete(() =>
        {
            Invoke("Hide" , duringTime);
        });        
    }
    private void Hide()
    {
        canvasGroup.DOFade(0 , exitTime).OnComplete(()=> {
            uiMgr.PopPanel();
        });
    }
    public void SetDuringTime(float time)
    {
        duringTime = time;
    }
    public void SetMessage(string msg)
    {
        this.msg.text = msg;
    }
    public void SetManager(UIManager mgr)
    {
        uiMgr = mgr;
    }
}
