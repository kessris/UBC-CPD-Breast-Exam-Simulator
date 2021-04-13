using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionControl : MonoBehaviour
{
    public float timeToWait = 2f;

    private void OnEnable()
    {
        StartCoroutine(HideElement());
    }

    IEnumerator HideElement()
    {
        yield return new WaitForSeconds(timeToWait);
        gameObject.SetActive(false);
    }
}
