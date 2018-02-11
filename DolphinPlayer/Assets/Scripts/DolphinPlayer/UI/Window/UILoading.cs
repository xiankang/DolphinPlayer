using Core.UI;
using Core.Utils;
using DolphinPlayer.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoading : UIWindowBase
{
    private Text txtCountDown;
    private static uint countDownTimer = 0;
    override public void Init()
    {
        base.Init();
        //txtCountDown = transform.Find("txtCountDown").GetComponent<Text>();
    }

    public override void OnShow()
    {
        base.OnShow();
        int countDown = 3;
        countDownTimer = TimerHeap.AddTimer(0, 1000, delegate
        {
             if (countDown == 0)
             {
                 TimerHeap.DelTimer(UILoading.countDownTimer);
                 CloseWindow<UILoading>(true);
                 OpenWindow<UINormal>();
             }
             SetText("txtCountDown", Convert.ToString(countDown));
             countDown -= 1;
         });
    }
}
