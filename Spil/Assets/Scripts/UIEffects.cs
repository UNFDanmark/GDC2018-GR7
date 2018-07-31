using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <Credit>
/// Author: Julius 'Bukz' Bendt, Juto Studio
/// https://wwww.juto.dk
/// Free-for-all license
/// </Credit>

public class UIEffects : MonoBehaviour {




    public delegate void OnEventOver();

    public IEnumerator MoveRect(RectTransform rect, Vector3 targetPos,float time)
    {
        float timer = 0.0f;

        Vector3 org = rect.transform.localPosition;


        while(timer < time)
        {
            timer += Time.deltaTime;

            rect.transform.localPosition = Vector3.Lerp(org, targetPos, (timer / time));
            yield return null;
        }

        yield return null;
    }

    public IEnumerator TextFade(TextMeshProUGUI text,Color color, float time,OnEventOver onFinish = null)
    {
        float timer = 0.0f;

        Color org = text.color;

        while (timer < time)
        {
            timer += Time.deltaTime;
            text.color = Color.Lerp(org, color, (timer / time));
            yield return null;
        }

        if (onFinish != null)
            onFinish.Invoke();
    }

    public IEnumerator ImageFade(Image image, Color color, float time,OnEventOver onFinish = null)
    {
        float timer = 0.0f;

        Color org = image.color;

        while (timer < time)
        {
            timer += Time.deltaTime;
            image.color = Color.Lerp(org, color, (timer / time));
            yield return null;
        }

        if (onFinish != null)
            onFinish.Invoke();
    }

    public IEnumerator SmoothTextAdd(TextMeshProUGUI text, string targetText,float time, OnEventOver onFinish = null, bool clearText = true)
    {

        if (clearText)
            text.text = "";

        int charPrSec = (int)(targetText.Length / time) +1;
        int c_index = 0;
        float timer = 0;

        while(timer < time)
        {
            timer += Time.deltaTime;
            for (int i = 0; i < charPrSec; i++)
            {
                if(c_index < targetText.Length)
                    text.text += targetText[c_index];


                c_index++;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            yield return null;

        }

        if (onFinish != null)
            onFinish.Invoke();
    }

}
