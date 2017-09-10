using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ToastPanel : BasePanel {
    private UIManager uiMgr;
    private float duringTime = 1f;
    private Text msg;
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        transform.SetAsLastSibling();
        gameObject.SetActive(true);
                
        EnterTweening();
    }
    /// <summary>
    /// 进入动画
    /// </summary>
    private void EnterTweening()
    {
        canvasGroup.DOFade(1f , 0.5f).OnComplete(() =>
        {
            Invoke("Hide" , duringTime);
        });        
    }
    private void Hide()
    {
        canvasGroup.DOFade(0 , 0.5f).OnComplete(()=> {
            uiMgr.PopPanel();
            gameObject.SetActive(false);
        });
    }
    public void SetDuringTime(float time)
    {
        duringTime = time;
    }
    public void SetMessage(string msg)
    {
        if(this.msg == null)
        {
            this.msg = transform.Find("message").GetComponent<Text>();
        }
        this.msg.text = msg;
    }
    public void SetManager(UIManager mgr)
    {
        uiMgr = mgr;
    }
}
