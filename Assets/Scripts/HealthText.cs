using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{

    // pixels per second
    public Vector3 moveSpeed = new Vector3(0, 100, 0);
    public float timeToFade = 1f;

    RectTransform textTransform;
    TextMeshProUGUI textMeshPro;

    private float timeElapsed = 0f;
    private Color startColour;

    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startColour = textMeshPro.color;
    }

    // Update is called once per frame
    private void Update()
    {
        textTransform.position += moveSpeed * Time.deltaTime;
        timeElapsed += Time.deltaTime;

        if (timeElapsed < timeToFade)
        {
            float fadeAlpha = startColour.a * (1 - (timeElapsed / timeToFade));
            textMeshPro.color = new Color(startColour.r, startColour.g, startColour.b, fadeAlpha);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
