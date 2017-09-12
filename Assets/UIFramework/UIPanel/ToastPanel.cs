using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ToastPanel : BasePanel {
    private float duringTime = 1f;
    private Text msg;
    private bool isEnter=false;
    public bool IsEnter()
    {
        return isEnter;
    }
    protected override void InitPanel()
    {
        isEnter = false;
        base.InitPanel();
        this.msg = transform.Find("message").GetComponent<Text>();
        canvasGroup.alpha = 0;
    }
    public override void OnEnter()
    {
        isEnter = true;
        transform.SetAsLastSibling();
        base.OnEnter();
    }
    /// <summary>
    /// 进入动画
    /// </summary>
    protected override void EnterTweening()
    {
        canvasGroup.DOFade(1f , enterTime);
                
    }

    private void Update()
    {
        if(isEnter)
        {
            duringTime -= Time.deltaTime;
            duringTime = Mathf.Max(0 , duringTime);
            if(duringTime==0)
            {
                Hide();
            }
        }
    }

    private void Hide()
    {
        isEnter = false;
        canvasGroup.DOFade(0 , exitTime);
    }

    public void SetDuringTime(float time)
    {
        duringTime = time;
    }
    public void SetMessage(string msg)
    {
        this.msg.text = msg;
    }
}
