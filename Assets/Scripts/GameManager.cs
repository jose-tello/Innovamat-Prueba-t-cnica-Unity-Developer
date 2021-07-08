using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager = null;

    //Ui value display
    public GameObject displayValue = null;

    private EaseInOutUI displayValueTransition = null;

    //Ui select value
    public GameObject valuePrefab = null;

    public GameObject positionValue1 = null;
    public GameObject positionValue2 = null;
    public GameObject positionValue3 = null;

    private GameObject valuePref1 = null;
    private GameObject valuePref2 = null;
    private GameObject valuePref3 = null;

    private EaseInOutUI valuePref1Transition = null;
    private EaseInOutUI valuePref2Transition = null;
    private EaseInOutUI valuePref3Transition = null;

    private int correctValue = 0;
    private int errorValue1 = 0;
    private int errorValue2 = 0;

    //Game state
    enum GAME_STATE : int
    {
        ERROR_STATE = -1,

        DISPLAY_VALUE_START,
        DISPLAY_VALUE,
        DISPLAY_VALUE_END,

        SELECT_VALUE_START,
        SELECT_VALUE,
        SELECT_VALUE_END,

        MAX
    }

    public float displayEaseInDuration = 2.0f;
    public float displayDuration = 2.0f;
    public float displayEaseOutDuration = 2.0f;

    public float selectEaseInDuration = 2.0f;
    public float selectEaseOutDuration = 2.0f;

    private float gameStateTimer = 0.0f;

    private GAME_STATE gameState = GAME_STATE.ERROR_STATE;


    void Start()
    {
        if (gameManager != null)
            GameObject.Destroy(gameManager);

        gameManager = this;

        if (valuePrefab != null)
        {
            valuePref1 = Instantiate(valuePrefab, positionValue1.transform);
            valuePref2 = Instantiate(valuePrefab, positionValue2.transform);
            valuePref3 = Instantiate(valuePrefab, positionValue3.transform);

            valuePref1Transition = valuePref1.GetComponent<EaseInOutUI>();
            valuePref2Transition = valuePref2.GetComponent<EaseInOutUI>();
            valuePref3Transition = valuePref3.GetComponent<EaseInOutUI>();

            valuePref1.SetActive(false);
            valuePref2.SetActive(false);
            valuePref3.SetActive(false);
        }
        else
            Debug.Log("Need to add valuePrefab to GameManager");

        gameState = GAME_STATE.DISPLAY_VALUE_START;
        gameStateTimer = displayEaseInDuration;

        if (displayValue != null)
        {
            displayValueTransition = displayValue.GetComponent<EaseInOutUI>();

            if (displayValueTransition != null)
            {
                displayValueTransition.inDuration = displayEaseInDuration;
                displayValueTransition.StartEaseIn();

                gameStateTimer = displayEaseInDuration;
            }
            else
                Debug.Log("Display value GO needs EaseInOutUI component");
        }
        else
            Debug.Log("Need to add displayValue to GameManager");
    }


    void Update()
    {
        gameStateTimer -= Time.deltaTime;

        if (gameStateTimer <= 0)
            ChangeState();
    }


    private void ChangeState()
    {
        switch (gameState)
        {
            case GAME_STATE.ERROR_STATE:
                Debug.Log("ERROR GAME STATE");
                break;

            case GAME_STATE.DISPLAY_VALUE_START:
                gameState = GAME_STATE.DISPLAY_VALUE;

                gameStateTimer = displayDuration;
                break;

            case GAME_STATE.DISPLAY_VALUE:
                gameState = GAME_STATE.DISPLAY_VALUE_END;

                if (displayValueTransition != null)
                {
                    displayValueTransition.outDuration = displayEaseOutDuration;
                    displayValueTransition.StartEaseOut();

                    gameStateTimer = displayEaseInDuration;
                }
                break;

            case GAME_STATE.DISPLAY_VALUE_END:
                break;

            case GAME_STATE.SELECT_VALUE_START:
                break;

            case GAME_STATE.SELECT_VALUE:
                break;

            case GAME_STATE.SELECT_VALUE_END:
                break;

            default:
                Debug.Log("NEED TO ADD GAME STATE");
                break;
        }
    }


    private void UpdateDisplayValueStart()
    {

    }
}
