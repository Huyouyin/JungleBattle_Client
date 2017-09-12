using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class UIManager:BaseManager{

    
    private Transform canvasTransform;
    private Transform CanvasTransform
    {
        get
        {
            if (canvasTransform == null)
            {
                canvasTransform = GameObject.Find("Canvas").transform;
            }
            return canvasTransform;
        }
    }
    private Dictionary<UIPanelType, string> panelPathDict;//存储所有面板Prefab的路径
    private Dictionary<UIPanelType, BasePanel> panelDict;//保存所有实例化面板的游戏物体身上的BasePanel组件
    private Stack<BasePanel> panelStack;

    public UIManager(GameFacade facade):base(facade)
    {
        ParseUIPanelTypeJson();
        panelStack = new Stack<BasePanel>();
    }

    /// <summary>
    /// 把某个页面入栈，  把某个页面显示在界面上
    /// </summary>
    public void PushPanel(UIPanelType panelType)
    {
        //判断一下栈里面是否有页面
        BasePanel panel = GetPanel(panelType);
        if(panelStack.Count > 0)
        {
            BasePanel topPanel = panelStack.Peek();
            if(topPanel==panel)//目标页面已经在栈顶了
            {
                return;
            }
            topPanel.OnPause();
        }
        panel.OnEnter();
        panelStack.Push(panel);
    }
   
    /// <summary>
    /// 出栈 ，把页面从界面上移除
    /// </summary>
    public void PopPanel()
    {
        if (panelStack.Count <= 0) return;

        //关闭栈顶页面的显示
        BasePanel topPanel = panelStack.Pop();
        topPanel.OnExit();

        if (panelStack.Count <= 0) return;
        BasePanel topPanel2 = panelStack.Peek();
        topPanel2.OnResume();
    }

    /// <summary>
    /// 根据面板类型 得到实例化的面板
    /// </summary>
    /// <returns></returns>
    private BasePanel GetPanel(UIPanelType panelType)
    {
        if (panelDict == null)
        {
            panelDict = new Dictionary<UIPanelType, BasePanel>();
        }

        //BasePanel panel;
        //panelDict.TryGetValue(panelType, out panel);//TODO

        BasePanel panel = panelDict.TryGet(panelType);

        if (panel == null)
        {
            //如果找不到，那么就找这个面板的prefab的路径，然后去根据prefab去实例化面板
            string path = panelPathDict.TryGet(panelType);
            GameObject instPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject;
            instPanel.transform.SetParent(CanvasTransform,false);
            panelDict.Add(panelType, instPanel.GetComponent<BasePanel>());
            return instPanel.GetComponent<BasePanel>();
        }
        else
        {
            return panel;
        }

    }

    public void ShowToast(string msg,float time)
    {
        if(string.IsNullOrEmpty(msg))
            return;
        ToastPanel toast = GetPanel(UIPanelType.toast) as ToastPanel;
        toast.SetDuringTime(time);
        if(!toast.IsEnter())
        {
            toast.SetMessage(msg);
            toast.OnEnter();
        }
    }

    [Serializable]
    class UIPanelTypeJson
    {
        public List<UIPanelInfo> infoList;
    }
    private void ParseUIPanelTypeJson()
    {
        panelPathDict = new Dictionary<UIPanelType, string>();

        TextAsset ta = Resources.Load<TextAsset>("UIPanelType");

        UIPanelTypeJson jsonObject = JsonUtility.FromJson<UIPanelTypeJson>(ta.text);

        foreach(UIPanelInfo info in jsonObject.infoList)
        {
            //Debug.Log(info.panelType);
            panelPathDict.Add(info.panelType , info.path);
        }
    }

    /// <summary>
    /// just for test
    /// </summary>
    public void Test()
    {
        string path ;
        panelPathDict.TryGetValue(UIPanelType.toast,out path);
        Debug.Log(path);
    }
}
public static class Toast
{
    public static void ShowToast(string msg, float duringTime = 1f)
    {
        UIManager uiMgr = GameFacade.instance.GetManager(ManagerType.UIManager) as UIManager;
        uiMgr.ShowToast(msg,duringTime);
    }
    //public static 
}
