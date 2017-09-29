using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RivalWaitingJoinPanel : MonoBehaviour {

    private Text usernameText;
    private Text winText;
    private Text totalText;
    private Text winpercentageText;


    public void InitPanel()
    {
        usernameText = transform.Find("username").GetComponent<Text>();
        winText = transform.Find("wincount").GetComponent<Text>();
        totalText = transform.Find("totalcount").GetComponent<Text>();
        winpercentageText = transform.Find("winpercentage").GetComponent<Text>();
    }

    private void OnRivalEnter(object rival)
    {
        string rivalinfo = rival as string;
        string[] datas = rivalinfo.Split(',');
        usernameText.text = datas[0];
        int win = int.Parse(datas[1]);
        int total = int.Parse(datas[2]);
        winText.text = win.ToString();
        totalText.text = total.ToString();
        double winPercenttmp = win / (double)total;
        float winp = (float)System.Math.Round(winPercenttmp , 2);
        winpercentageText.text = winp.ToString();
    }
}
