using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EaseInOutUI : MonoBehaviour
{
    private Transform trans = null;

    private List<Text> textList = new List<Text>();
    private List<Button> buttonList = new List<Button>();
    private List<Image> imageList = new List<Image>();

    private bool updateEaseIn = false;
    private bool updateEaseOut = false;
    private float timer = 0.0f;

    [Header("Ease in")]
    public float inDuration = 2.0f;
    public Vector3 inEndPosition = new Vector3(0.0f, 0.0f, 0.0f);
    public bool inFade = false;

    [Header("Ease out")]
    public float outDuration = 2.0f;
    public Vector3 outEndPosition = new Vector3(0.0f, 0.0f, 0.0f);
    public bool outFade = false;


    private void Start()
    {
        trans = GetComponent<Transform>();

        Text txt = GetComponent<Text>();
        Text[] textArray = GetComponentsInChildren<Text>(true);
        
        if (txt != null)
            textList.Add(txt);

        for (int i = 0; i < textArray.Length; ++i)
            textList.Add(textArray[i]);
        

        Button button = GetComponent<Button>();
        Button[] buttonArray = GetComponentsInChildren<Button>(true);

        if (button != null)
            buttonList.Add(button);

        for (int i = 0; i < buttonArray.Length; ++i)
            buttonList.Add(buttonArray[i]);

        Image img = GetComponent<Image>();
        Image[] imageArray = GetComponentsInChildren<Image>(true);

        if (img != null)
            imageList.Add(img);

        for (int i = 0; i < imageArray.Length; ++i)
            imageList.Add(imageArray[i]);

        MakeTransparent();
    }


    void Update()
    {
        if (timer >= 0)
        {
            if (updateEaseIn == true)
                UpdateEaseIn();

            if (updateEaseOut == true)
                UpdateEaseOut();
        }

        else if (timer < 0 && updateEaseIn == true)
        {
            updateEaseIn = false;

            for (int i = 0; i < buttonList.Count; ++i)
                buttonList[i].interactable = true;
        }

        else if (timer < 0 && updateEaseOut == true)
            updateEaseOut = false;
    }

    public void MakeTransparent()
    {
        for (int i = 0; i < textList.Count; ++i)
            textList[i].color = new Color(textList[i].color.r, textList[i].color.g, textList[i].color.b, 0.0f);

        for (int i = 0; i < imageList.Count; ++i)
            imageList[i].color = new Color(imageList[i].color.r, imageList[i].color.g, imageList[i].color.b, 0.0f);
    }

    public void SetPosition(Vector3 pos)
    {
        trans.position = pos;
    }


    #region EASE_IN 
    public void SetEaseInEndPosition(Vector3 pos)
    {
        inEndPosition = pos;
    }


    public void StartEaseIn()
    {
        if (updateEaseOut == true)
        {
            Debug.Log("Trying to start ease in while updating ease out");
            return;
        }

        for (int i = 0; i < textList.Count; ++i)
            textList[i].color = new Color(textList[i].color.r, textList[i].color.g, textList[i].color.b, 0.0f);

        for (int i = 0; i < buttonList.Count; ++i)
            buttonList[i].interactable = false;

        for (int i = 0; i < imageList.Count; ++i)
            imageList[i].color = new Color(imageList[i].color.r, imageList[i].color.g, imageList[i].color.b, 0.0f);

        updateEaseIn = true;
        timer = inDuration;
    }


    public void UpdateEaseIn()
    {
        for (int i = 0; i < textList.Count; ++i)
            textList[i].color = new Color(textList[i].color.r, textList[i].color.g, textList[i].color.b, Mathf.Lerp(1.0f, 0.0f, timer / inDuration));

        for (int i = 0; i < imageList.Count; ++i)
            imageList[i].color = new Color(imageList[i].color.r, imageList[i].color.g, imageList[i].color.b, Mathf.Lerp(1.0f, 0.0f, timer / inDuration));
        

        timer -= Time.deltaTime;
    }
    #endregion

    #region EASE_OUT
    public void SetEaseOutEndPosition(Vector3 pos)
    {
        outEndPosition = pos;
    }


    public void StartEaseOut()
    {
        if (updateEaseIn == true)
        {
            Debug.Log("Trying to start ease out while updating ease in");
            return;
        }

        for (int i = 0; i < textList.Count; ++i)
            textList[i].color = new Color(textList[i].color.r, textList[i].color.g, textList[i].color.b, 1.0f);

        for (int i = 0; i < buttonList.Count; ++i)
            buttonList[i].interactable = false;

        for (int i = 0; i < imageList.Count; ++i)
            imageList[i].color = new Color(imageList[i].color.r, imageList[i].color.g, imageList[i].color.b, 1.0f);

        updateEaseOut = true;
        timer = outDuration;
    }


    public void UpdateEaseOut()
    {
        for (int i = 0; i < textList.Count; ++i)
            textList[i].color = new Color(textList[i].color.r, textList[i].color.g, textList[i].color.b, Mathf.Lerp(0.0f, 1.0f, timer / inDuration));

        for (int i = 0; i < imageList.Count; ++i)
            imageList[i].color = new Color(imageList[i].color.r, imageList[i].color.g, imageList[i].color.b, Mathf.Lerp(0.0f, 1.0f, timer / inDuration));

        timer -= Time.deltaTime;
    }
    #endregion
}
