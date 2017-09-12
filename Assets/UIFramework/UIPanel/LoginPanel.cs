using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Common;

public class LoginPanel : BasePanel {
    private Button closeButton;
    private Button loginButton;
    private Button registerButton;
    private InputField fieldName;
    private InputField fieldPass;
    private Vector2 originPos =new Vector2(1024,0);
    private AudioManager audioMgr;
    private RequestManager requestMgr;

    protected override void InitPanel()
    {
        base.InitPanel();
        requestMgr = GameFacade.instance.GetManager(ManagerType.RequestManager) as RequestManager;
        audioMgr = GameFacade.instance.GetManager(ManagerType.AudioManager) as AudioManager;

        loginButton = transform.Find("loginButton").GetComponent<Button>();
        loginButton.onClick.AddListener(OnClickLogin);
        registerButton = transform.Find("registerButton").GetComponent<Button>();
        registerButton.onClick.AddListener(OnClickRegister);
        closeButton = transform.Find("close").GetComponent<Button>();
        closeButton.onClick.AddListener(OnClickClose);
        fieldName = transform.Find("username/Input").GetComponent<InputField>();
        fieldPass = transform.Find("pass/Input").GetComponent<InputField>();
    }

    protected override void EnterTweening()
    {
        transform.localScale = Vector2.zero;
        transform.localPosition = new Vector3(1024 , 0);
        transform.DOScale(1 , enterTime);
        transform.DOLocalMove(Vector2.zero , enterTime).OnComplete(()=> {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        });
    }
    
    protected override void ExitTweening()
    {
        transform.DOScale(0 , exitTime);
        exitTweener = exitTweener = transform.DOLocalMove(originPos , exitTime);
    }

    protected override void ResetPanel()
    {
        fieldName.text = "";
        fieldPass.text = "";
    }

    private void OnClickClose()
    {
        audioMgr.PlaySound(SoundType.ButtonClick);
        uiMgr.PopPanel();
        exitTweener.OnComplete(() => {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            uiMgr.PushPanel(UIPanelType.start);
        });
    }

    private void OnClickLogin()
    {
        audioMgr.PlaySound(SoundType.ButtonClick);

        bool varifyRes = VarifyAccount();
        if(!varifyRes)  
        {
            return;
        }
        string data = fieldName.text + " " + fieldPass.text;
        requestMgr.HandleRequest(RequestCode.LoginRequest , ActionCode.Login , data);
    }

    /// <summary>
    /// 账号校验，此处没作什么限制，用户名和密码都不能为空，且都不能包含空格字符
    /// 因为空格字符用于做分割
    /// </summary>
    /// <returns></returns>
    private bool VarifyAccount()
    {
        string name = fieldName.text;
        string pass = fieldPass.text;
        if(string.IsNullOrEmpty(name))
        {
            Toast.ShowToast("用户名不能为空");
            return false;
        }
        if(string.IsNullOrEmpty(pass))
        {
            Toast.ShowToast("密码不能为空");
            return false;
        }
        if(name.Contains(" "))
        {
            Toast.ShowToast("用户名不能包含空格");
            return false;
        }
        if(pass.Contains(" "))
        {
            Toast.ShowToast("用户名不能包含空格");
            return false;
        }
        return true;
    }

    private void OnClickRegister()
    {
        audioMgr.PlaySound(SoundType.ButtonClick);

        uiMgr.PopPanel();
        exitTweener.OnComplete(() =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            uiMgr.PushPanel(UIPanelType.register);
        });
    }
}
