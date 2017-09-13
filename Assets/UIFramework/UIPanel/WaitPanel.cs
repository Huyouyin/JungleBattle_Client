using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WaitPanel : BasePanel {
    private Transform imageTran;
    Tweener tweener;
    protected override void InitPanel()
    {
        base.InitPanel();
        imageTran = transform.Find("Image");
    }
    
    public override void OnEnter()
    {
        gameObject.SetActive(true);
        base.OnEnter();
        tweener = imageTran.DOLocalRotate(new Vector3 (0,0,360), 2,RotateMode.LocalAxisAdd);
        tweener.SetEase(Ease.Linear);
        tweener.OnComplete(() =>
        {
            tweener.Restart();
        });
    }
   
    public override void OnExit()
    {
        gameObject.SetActive(false);
        if(tweener != null)
            tweener.Kill();
    }
}
