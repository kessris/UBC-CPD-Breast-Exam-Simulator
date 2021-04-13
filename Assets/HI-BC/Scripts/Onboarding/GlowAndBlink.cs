using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlowAndBlink : MonoBehaviour
{
    Image glowingBorder;

    // Start is called before the first frame update
    void Start()
    {
        glowingBorder = GetComponent<Image>();
        StartBlinking();
    }

    private void OnEnable()
    {
        StartBlinking();
    }

    IEnumerator Blink()
    {
        while (true)
        {
            switch (glowingBorder.color.a.ToString())
            {
                case "0":
                    glowingBorder.color = new Color(glowingBorder.color.r, glowingBorder.color.g, glowingBorder.color.b, 1);
                    //Play sound
                    yield return new WaitForSeconds(0.5f);
                    break;
                case "1":
                    glowingBorder.color = new Color(glowingBorder.color.r, glowingBorder.color.g, glowingBorder.color.b, 0);
                    //Play sound
                    yield return new WaitForSeconds(0.5f);
                    break;
            }
        }
    }

    void StartBlinking()
    {
        StopAllCoroutines();
        StartCoroutine("Blink");
    }
}
