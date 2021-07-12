using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextColorTransition : MonoBehaviour
{
    public List<Color> colorTransitions = new List<Color>();
    private Color currentTransition = new Color(0.0f, 0.0f, 0.0f);

    public float transitionDuration = 1.0f;
    private float transitionTimer = 0.0f;

    private List<Text> textList = new List<Text>();
    private List<Color> originalColorList = new List<Color>();

    private bool transitionToColor = false;
    private bool transitionToOriginal = false;

    // Start is called before the first frame update
    void Start()
    {
        Text text = GetComponent<Text>();

        if (text != null)
        {
            textList.Add(text);
            originalColorList.Add(new Color(text.color.r, text.color.g, text.color.b));
        }

        Text[] textChilds = GetComponentsInChildren<Text>();

        for (int i = 0; i < textChilds.Length; ++i)
        {
            textList.Add(textChilds[i]);
            originalColorList.Add(new Color(textChilds[i].color.r, textChilds[i].color.g, textChilds[i].color.b, textChilds[i].color.a));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transitionTimer >= 0.0f)
        {
            if (transitionToColor == true)
                TransitionToColor();

            else if (transitionToOriginal == true)
                TransitionToOriginal();
        }

        else if (transitionTimer < 0.0f && transitionToColor == true)
        {
            transitionToColor = false;
            transitionToOriginal = true;

            transitionTimer = transitionDuration * 0.5f;
        }

        else
            transitionToOriginal = false;

        transitionTimer -= Time.deltaTime;
    }

    private void TransitionToColor()
    {
        for (int i = 0; i < textList.Count; ++i)
        {
            float r = Mathf.Lerp(currentTransition.r, originalColorList[i].r, transitionTimer / transitionDuration * 0.5f);
            float g = Mathf.Lerp(currentTransition.g, originalColorList[i].g, transitionTimer / transitionDuration * 0.5f);
            float b = Mathf.Lerp(currentTransition.b, originalColorList[i].b, transitionTimer / transitionDuration * 0.5f);

            textList[i].color = new Color(r, g, b);
        }
    }

    private void TransitionToOriginal()
    {
        for (int i = 0; i < textList.Count; ++i)
        {
            float r = Mathf.Lerp(originalColorList[i].r, currentTransition.r, transitionTimer / transitionDuration * 0.5f);
            float g = Mathf.Lerp(originalColorList[i].g, currentTransition.g, transitionTimer / transitionDuration * 0.5f);
            float b = Mathf.Lerp(originalColorList[i].b, currentTransition.b, transitionTimer / transitionDuration * 0.5f);

            textList[i].color = new Color(r, g, b);
        }
        
    }

    public void StartTransition(int transitionNum)
    {
        if (transitionNum >= colorTransitions.Count)
        {
            Debug.Log("Invalid color transition");
            return;
        }

        currentTransition = colorTransitions[transitionNum];

        transitionToColor = true;
        transitionTimer = transitionDuration * 0.5f;
    }
}
