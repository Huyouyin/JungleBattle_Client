using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ListPanel : MonoBehaviour {


    private float originPosX = 155f;
    private float enterPosX = 941f;
    private float enterTime = 0.3f;
    private float exitTime = 0.3f;

    public void InitPanel()
    {
        float y = transform.position.y;
        transform.position = new Vector2(enterPosX , y);
        gameObject.SetActive(false);
    }

    public void onEnter()
    {
        gameObject.SetActive(true);
        transform.DOLocalMoveX(originPosX , enterTime);
    }

    public Tweener onExit()
    {
        return transform.DOLocalMoveX(enterPosX , exitTime);
    }
}
