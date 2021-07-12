using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryCounter : MonoBehaviour
{
    private Text textComponent = null;
    void Start()
    {
        textComponent = GetComponent<Text>();

        if (textComponent == null)
        {
            Debug.Log("Add text component to: " + gameObject.name);
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        textComponent.text = "Victories: " + GameManager.gameManager.victoryCount.ToString();
    }
}
