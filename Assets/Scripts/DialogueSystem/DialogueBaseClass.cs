using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBaseClass : MonoBehaviour
{
    public bool finished { get; private set; }
    protected IEnumerator WriteText(string input, TMP_Text textHolder, Color textColour, float delay)
    {
        textHolder.color = textColour;
        for (int i=0; i<input.Length; i++)
        {
            textHolder.text += input[i];
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitUntil(() => Input.GetKey(KeyCode.KeypadEnter));
        finished = true;
    }
}
