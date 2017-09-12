using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Common;

public class RegisterPanel : BasePanel {
    private Button closeButton;
    private Vector2 originPos = new Vector2(1024 , 0);
    private InputField fieldname;
    private InputField fieldpass;
    private InputField fieldconfirm;
    private Button registerButton;

    private RequestManager requestMgr;
    private AudioManager audioMgr;

    protected override void InitPanel()
    {
        base.InitPanel();
        requestMgr = GameFacade.instance.GetManager(ManagerType.RequestManager) as RequestManager;
        audioMgr = GameFacade.instance.GetManager(ManagerType.AudioManager) as AudioManager;
        closeButton = transform.Find("close").GetComponent<Button>();
        closeButton.onClick.AddListener(OnClickClose);
        fieldname = transform.Find("username/Input").GetComponent<InputField>();
        fieldpass = transform.Find("pass/Input").GetComponent<InputField>();
        fieldconfirm = transform.Find("confirmpass/Input").GetComponent<InputField>();
        registerButton = transform.Find("registerButton").GetComponent<Button>();
        registerButton.onClick.AddListener(OnClickRegister);
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
        transform.DOLocalMove(originPos , exitTime);
        exitTweener = transform.DOScale(0 , exitTime);
    }

    protected override void ResetPanel()
    {
        fieldconfirm.text = "";
        fieldname.text = "";
        fieldpass.text = "";
    }

    private void OnClickClose()
    {
        audioMgr.PlaySound(SoundType.ButtonClick);

        uiMgr.PopPanel();
        exitTweener.OnComplete(() => {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            uiMgr.PushPanel(UIPanelType.login);
        });
    }

    private void OnClickRegister()
    {
        audioMgr.PlaySound(SoundType.ButtonClick);

        if(VarifyAccount())
        {
            string data = Message.PackContentData(fieldname.text , fieldpass.text);
            requestMgr.HandleRequest(RequestCode.RegisterRequest , ActionCode.Register , data);
        }
    }

    /// <summary>
    /// 校验注册账号
    /// </summary>
    /// <returns></returns>
    private bool VarifyAccount()
    {
        string name = fieldname.text;
        string pass = fieldpass.text;
        string confirmpass = fieldconfirm.text;
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
        if(string.IsNullOrEmpty(confirmpass))
        {
            Toast.ShowToast("请输入确认密码");
            return false;
        }
        if(!confirmpass.Equals(pass))
        {
            Toast.ShowToast("两次密码不一致");
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
        if(confirmpass.Contains(" "))
        {
            Toast.ShowToast("确认密码不能包含空格");
            return false;
        }
        return true;
    }

    
}
