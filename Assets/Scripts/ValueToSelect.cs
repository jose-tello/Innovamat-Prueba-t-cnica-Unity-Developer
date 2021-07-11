using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ValueToSelect : MonoBehaviour
{
    private Text txt = null;
    private Button button = null;
    public GameObject textChildGO = null;

    void Start()
    {
        if (textChildGO != null)
        {
            txt = textChildGO.GetComponent<Text>();

            if (txt == null)
            {
                Debug.Log("Need to add text component!");
                gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log("Need to add child with text component!");
            gameObject.SetActive(false);
        }

        button = GetComponent<Button>();

        if (button == null)
        {
            Debug.Log("Need to add button component!");
            gameObject.SetActive(false);
        }
    }

    public void ValueSelected()
    {
        Int32.TryParse(txt.text, out int value);
        GameManager.gameManager.ValueSelected(value);
    }

    public void SetValue(int value)
    {
        if (txt != null)
            txt.text = value.ToString();
    }
}
