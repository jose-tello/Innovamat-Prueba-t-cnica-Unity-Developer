using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaseInOutUI : MonoBehaviour
{
    private bool updateEaseIn = false;
    private bool updateEaseOut = false;
    private float timer = 0.0f;

    private Transform trans = null; 

    [Header("Ease in")]
    public float inDuration = 2.0f;
    public Vector3 inEndPosition = new Vector3(0.0f, 0.0f, 0.0f);
    public bool inFade = false;

    [Header("Ease out")]
    public float outDuration = 2.0f;
    public Vector3 outEndPosition = new Vector3(0.0f, 0.0f, 0.0f);
    public bool outFade = false;


    void Start()
    {
        trans = GetComponent<Transform>();
    }


    void Update()
    {
        if (updateEaseIn == true)
            UpdateEaseIn();

        if (updateEaseOut == true)
            UpdateEaseOut();
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

        updateEaseIn = true;
        timer = inDuration;
    }


    public void UpdateEaseIn()
    {
        Mathf.Lerp(0.0f, 255.0f, timer / inDuration);

        timer -= Time.deltaTime;

        //Move stuff
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

        updateEaseOut = true;
        timer = outDuration;
    }


    public void UpdateEaseOut()
    {
        Mathf.Lerp(255.0f, 0.0f, timer / outDuration);

        timer -= Time.deltaTime;

        //Move stuff
    }
    #endregion
}
