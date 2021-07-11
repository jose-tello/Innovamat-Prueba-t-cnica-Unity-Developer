using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefeatCounter : MonoBehaviour
{
    private Text textComponent = null;
    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<Text>();

        if (textComponent == null)
        {
            Debug.Log("Add text component to: " + gameObject.name);
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        textComponent.text = "Defeats: " + GameManager.gameManager.defeatCount.ToString();
    }
}
