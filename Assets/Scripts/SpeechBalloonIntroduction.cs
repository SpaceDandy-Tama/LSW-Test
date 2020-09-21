using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBalloonIntroduction : MonoBehaviour
{
    public Text Text;
    public float SpeechSpeed = 0.1f;

    private string[] Strings =
    {
        "Welcome!",
        "Please browse at the clothing rack to your hearts content!",
        "Hover your mouse over it and click.",
    };
    private int CurrentIndex = -1;
    private float TimeStartedCurrentIndex = -1.0f;

    private void Update()
    {
        if(CurrentIndex < 0)
        {
            CurrentIndex = 0;
            TimeStartedCurrentIndex = Time.timeSinceLevelLoad;
            Text.text = Strings[CurrentIndex];
        }

        float timeRequiredToAdvanceText = Mathf.Min(3.0f, Strings[CurrentIndex].Length * SpeechSpeed);

        if (Time.time > TimeStartedCurrentIndex + timeRequiredToAdvanceText)
        {
            CurrentIndex++;

            if (CurrentIndex >= Strings.Length)
            {
                Text.text = "";
                gameObject.SetActive(false);
                return;
            }

            TimeStartedCurrentIndex = Time.timeSinceLevelLoad;
            Text.text = Strings[CurrentIndex];
        }
    }
}
