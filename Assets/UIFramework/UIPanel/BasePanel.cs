using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BasePanel : MonoBehaviour {
    protected CanvasGroup canvasGroup;
    protected float enterTime = 0.5f;//所有面板的进入时间  可以另行设置
    protected float exitTime = 0.5f;//所有面板的退出时间   可以另性设置
    protected Tweener enterTweener;
    protected Tweener exitTweener;


    private void Awake()
    {
        InitPanel();
    }

    protected virtual void InitPanel() {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    
    /// <summary>
    /// 界面被显示出来
    /// </summary>
    public virtual void OnEnter()
    {
        ResetPanel();
        EnterTweening();
    }
    protected virtual void EnterTweening() { }
    /// <summary>
    /// 界面暂停
    /// </summary>
    public virtual void OnPause()
    {
        canvasGroup.interactable = false;
    }

    /// <summary>
    /// 界面继续
    /// </summary>
    public virtual void OnResume()
    {
        canvasGroup.interactable = true;
    }

    /// <summary>
    /// 界面不显示,退出这个界面，界面被关系
    /// </summary>
    public virtual void OnExit()
    {
        ExitTweening();
    }

    protected virtual void ExitTweening()
    { }
    protected virtual void ResetPanel()
    {

    }
}
