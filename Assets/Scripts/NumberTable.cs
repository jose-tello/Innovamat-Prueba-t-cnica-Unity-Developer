using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberTable : MonoBehaviour
{
    public static NumberTable numberTranslateTable = null;

    private Dictionary<int, string> numbersTable = new Dictionary<int, string>();
    void Start()
    {
        if (numberTranslateTable != null)
            Destroy(numberTranslateTable);

        numberTranslateTable = this;

        numbersTable.Add(0, "zero");
        numbersTable.Add(1, "one");
        numbersTable.Add(2, "two");
        numbersTable.Add(3, "three");
        numbersTable.Add(4, "four");
        numbersTable.Add(5, "five");
        numbersTable.Add(6, "six");
        numbersTable.Add(7, "seven");
        numbersTable.Add(8, "eight");
        numbersTable.Add(9, "nine");
        numbersTable.Add(10, "ten");
    }

    public string GetNumberStr(int value)
    {
        numbersTable.TryGetValue(value, out string valueStr);

        return valueStr;
    }
}
