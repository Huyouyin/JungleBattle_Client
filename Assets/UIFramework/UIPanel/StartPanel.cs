﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class StartPanel : BasePanel {
    private Button loginButton;
    float angle = 360;
    private AudioManager audioMgr;

    protected override void InitPanel()
    {
        audioMgr = GameFacade.instance.GetManager(ManagerType.AudioManager) as AudioManager;
        base.InitPanel();
        enterTime = 0.3f;
        exitTime = 0.3f;
        loginButton = transform.Find("loginButton").GetComponent<Button>();
        loginButton.onClick.AddListener(OnLoginClick);
        loginButton.transform.rotation = Quaternion.Euler(new Vector3(0 , 0 , 180));
        loginButton.transform.localScale = Vector3.zero;
    }
    public override void OnEnter()
    {
        base.OnEnter();
    }
    protected override void EnterTweening()
    {
        loginButton.transform.DOLocalRotateQuaternion(Quaternion.Euler(new Vector3(0 , 0 , 0)) , enterTime);
        loginButton.transform.DOScale(1 , exitTime).OnComplete(() => {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        });
    }
    private void OnLoginClick()
    {
        audioMgr.PlaySound(SoundType.ButtonClick);

        uiMgr.PopPanel();
    }

    public override void OnExit()
    {
        base.OnExit();
        uiMgr.PushPanel(UIPanelType.login);
    }
    protected override void ExitTweening()
    {
        loginButton.transform.DOLocalRotateQuaternion(Quaternion.Euler(new Vector3(0 , 0 , 180)) , exitTime);
        loginButton.transform.DOScale(0 , exitTime).OnComplete(() => {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        });
    }
}
