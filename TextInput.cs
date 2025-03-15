using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class TextInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField userInput;
    [SerializeField] private TextMeshProUGUI display;
    string data = "";

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            InputFunction();
            SaveData();
        }
    }
    private void InputFunction()
    {
        display.text = userInput.text;
    }

    private void SaveData()
    {
        data += "/n" + userInput.text.ToString();
        Debug.Log(data);
        File.WriteAllText("E:/justdab/Unityprojects/SCRAPYARD/Assets/savedata.json", data);
    }
}

[System.Serializable]
public class InputReceived
{
    public string rhetoricUsed;
}
