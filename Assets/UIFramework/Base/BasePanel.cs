using UnityEngine;
using System.Collections;

public class BasePanel : MonoBehaviour {
    protected CanvasGroup canvasGroup;
    protected float enterTime = 1f;//所有面板的进入时间  可以另行设置
    protected float exitTime = 1f;//所有面板的退出时间   可以另性设置


    /// <summary>
    /// 界面被显示出来
    /// </summary>
    public virtual void OnEnter()
    {
        if(canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// 界面暂停
    /// </summary>
    public virtual void OnPause()
    {

    }

    /// <summary>
    /// 界面继续
    /// </summary>
    public virtual void OnResume()
    {

    }

    /// <summary>
    /// 界面不显示,退出这个界面，界面被关系
    /// </summary>
    public virtual void OnExit()
    {

    }
}
