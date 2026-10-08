using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueLine : DialogueBaseClass
{
    private TMP_Text textHolder;

    [Header ("Text Options")]
    [SerializeField] private string input;
    [SerializeField] private Color textColor = Color.black;

    [Header ("Time parameters")]
    [SerializeField] private float delay;

    private void Awake()
    {
        textHolder = GetComponent<TMP_Text>();
        textHolder.text = "";
    }

    private void Start()
    {
        StartCoroutine(WriteText(input, textHolder, textColor, delay));
    }
}
