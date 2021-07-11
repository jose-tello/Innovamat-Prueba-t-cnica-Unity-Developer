using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager = null;
    private bool started = false;

    //Ui value display
    public GameObject displayValue = null;

    private EaseInOutUI displayValueTransition = null;
    private Text displayValueText = null;

    //Ui select value
    [Header("Value to select")]
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

    private ValueToSelect valuePref1Script = null;
    private ValueToSelect valuePref2Script = null;
    private ValueToSelect valuePref3Script = null;

    public int smallestPossibleValue = 0;
    public int biggestPossibleValue = 10;

    private int correctValue = 0;

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

    //Game statistics
    [HideInInspector] public int victoryCount = 0;
    [HideInInspector] public int defeatCount = 0;


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

            valuePref1Script = valuePref1.GetComponent<ValueToSelect>();
            valuePref2Script = valuePref2.GetComponent<ValueToSelect>();
            valuePref3Script = valuePref3.GetComponent<ValueToSelect>();

            //Since they are instances of the same prefab, one check is enought
            if (valuePref1Transition != null)
            {
                valuePref1Transition.inDuration = selectEaseInDuration;
                valuePref1Transition.outDuration = selectEaseOutDuration;
                valuePref1Transition.MakeTransparent();

                valuePref2Transition.inDuration = selectEaseInDuration;
                valuePref2Transition.outDuration = selectEaseOutDuration;
                valuePref2Transition.MakeTransparent();

                valuePref3Transition.inDuration = selectEaseInDuration;
                valuePref3Transition.outDuration = selectEaseOutDuration;
                valuePref3Transition.MakeTransparent();
            }
        }
        else
            Debug.Log("Need to add valuePrefab to GameManager");


        if (displayValue != null)
        {
            displayValueTransition = displayValue.GetComponent<EaseInOutUI>();
            displayValueText = displayValue.GetComponent<Text>();

            if (displayValueTransition == null)
                Debug.Log("Display value GO needs EaseInOutUI component");

            if (displayValueText == null)
                Debug.Log("Display value GO needs Text component");
        }
        else
            Debug.Log("Need to add displayValue to GameManager");
    }


    void Update()
    {
        if (started == false)
        {
            StartGame();
            started = true;
        }

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
                gameState = GAME_STATE.SELECT_VALUE_START;

                //Since they are instances of the same prefab, one check is enought
                if (valuePref1Transition != null)
                {
                    valuePref1Transition.StartEaseIn();
                    valuePref2Transition.StartEaseIn();
                    valuePref3Transition.StartEaseIn();
                }

                gameStateTimer = displayEaseInDuration;
                break;

            case GAME_STATE.SELECT_VALUE_START:
                gameState = GAME_STATE.SELECT_VALUE;
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


    private void StartGame()
    {
        gameState = GAME_STATE.DISPLAY_VALUE_START;

        SetValuesToPrefabs();

        gameStateTimer = displayEaseInDuration;
    }


    private void SetValuesToPrefabs()
    {
        //Calculate possible values
        correctValue = Random.Range(smallestPossibleValue, biggestPossibleValue);
        int errorValue1 = Random.Range(smallestPossibleValue, biggestPossibleValue);
        int errorValue2 = Random.Range(smallestPossibleValue, biggestPossibleValue);

        if (errorValue1 == correctValue)
            errorValue1 += 1;

        if (errorValue2 == correctValue || errorValue2 == errorValue1)
        {
            errorValue2 += 1;

            if (errorValue2 == correctValue || errorValue2 == errorValue1)
                errorValue2 += 1;
        }

        //Display text
        if (displayValueText != null)
            displayValueText.text = correctValue.ToString();

        if (displayValueTransition != null)
        {
            displayValueTransition.inDuration = displayEaseInDuration;
            displayValueTransition.StartEaseIn();
        }

        #region SET_VALUE_POSITION
        //Random values
        if (valuePref1Script != null)
        {
            float corValPos = Random.Range(0.0f, 1.0f);
            float errValPos1 = Random.Range(0.0f, 1.0f);
            float errValPos2 = Random.Range(0.0f, 1.0f);

            if (corValPos > errValPos1 && corValPos > errValPos2)
            {
                valuePref1Script.SetValue(correctValue);

                if (errValPos1 > errValPos2)
                {
                    valuePref2Script.SetValue(errorValue1);
                    valuePref3Script.SetValue(errorValue2);
                }

                else
                {
                    valuePref2Script.SetValue(errorValue2);
                    valuePref3Script.SetValue(errorValue1);
                }
            }

            else if (errValPos1 > correctValue && errValPos1 > errValPos2)
            {
                valuePref1Script.SetValue(errorValue1);

                if (correctValue > errValPos2)
                {
                    valuePref2Script.SetValue(correctValue);
                    valuePref3Script.SetValue(errorValue2);
                }

                else
                {
                    valuePref2Script.SetValue(errorValue2);
                    valuePref3Script.SetValue(correctValue);
                }
            }

            else
            {
                valuePref1Script.SetValue(errorValue2);

                if (correctValue > errValPos1)
                {
                    valuePref2Script.SetValue(correctValue);
                    valuePref3Script.SetValue(errorValue1);
                }

                else
                {
                    valuePref2Script.SetValue(errorValue1);
                    valuePref3Script.SetValue(correctValue);
                }
            }
        }
        #endregion
    }

    public void ValueSelected(int val)
    {
        if (val == correctValue)
        {
            victoryCount++;

            //Start transition
        }

        else
        {
            defeatCount++;

            //Start transition
        }

        ChangeState();
    }
}
