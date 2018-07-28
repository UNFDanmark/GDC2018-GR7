using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    

    public void KeyPressed(RectTransform size, RectTransform norm)
    {
        StartCoroutine(_keyPressed(size, norm));
    }


    private IEnumerator _keyPressed(RectTransform size,RectTransform norm)
    {
        float time = 0.5f;

        float timer = 0;

        Vector3 org_size = size.localScale; 
        Vector3 org_norm = norm.localScale;
        Vector3 target = new Vector3(1.5f, 1.5f, 1.5f);
        
        while (timer < time)
        {
            timer += Time.deltaTime;
            size.localScale = Vector3.Lerp(org_size, target, time / timer);
            norm.localScale = Vector3.Lerp(org_size, Vector3.one, time / timer);
            

        }


        yield return null;
    }
}
