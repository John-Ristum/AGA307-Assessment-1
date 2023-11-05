using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : Singleton<TimerBar>
{
    public Image timerFill;

    public void UpdateTimerBar(float _time)
    {
        timerFill.fillAmount = MapTo01(_time, 0, 30);
    }
}
