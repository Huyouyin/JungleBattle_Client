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

    protected override void InitPanel()
    {
        base.InitPanel();
        
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
        exitTweener = transform.DOLocalMove(originPos , exitTime);
    }

    protected override void ResetPanel()
    {
        fieldName.text = "";
        fieldPass.text = "";
    }

    private void OnClickClose()
    {
        GameFacade.instance.PlaySound(SoundType.ButtonClick);
        GameFacade.instance.PopPanel();
        exitTweener.OnComplete(() => {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            GameFacade.instance.PushPanel(UIPanelType.start);
        });
    }

    private void OnClickLogin()
    {
        GameFacade.instance.PlaySound(SoundType.ButtonClick);

        bool varifyRes = VarifyAccount();
        if(!varifyRes)  
        {
            return;
        }
        string data = Message.PackContentData(' ' , fieldName.text , fieldPass.text);
        GameFacade.instance.HandleRequest(RequestCode.User , ActionCode.Login , data,LoginCallBack);
    }

    /// <summary>
    /// 登陆成功回调
    /// </summary>
    private void LoginCallBack(object isSuccess)
    {
        GameFacade.instance.SetAccount(fieldName.text , fieldPass.text);
        GameFacade.instance.PopPanel();
        exitTweener.OnComplete(() =>
        {
            GameFacade.instance.PushPanel(UIPanelType.roomlist);
        });
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
        GameFacade.instance.PlaySound(SoundType.ButtonClick);

        GameFacade.instance.PopPanel();
        exitTweener.OnComplete(() =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            GameFacade.instance.PushPanel(UIPanelType.register);
        });
    }
}
