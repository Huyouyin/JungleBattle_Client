using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyWaitingJoinPanel : MonoBehaviour {
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


        usernameText.text = PlayerManager.UserAccount.userName;
        int win = PlayerManager.UserCompetitonCount.WinCount;
        int total = PlayerManager.UserCompetitonCount.TotalCount;
        winText.text = win.ToString();
        totalText.text = total.ToString();
        if(total == 0)
        {
            winpercentageText.text = "100%";
            return;
        }
        double winPercenttmp = win / (double)total;
        float winp = (float)System.Math.Round(winPercenttmp , 2);
        winpercentageText.text = winp.ToString();

    }
    
}
