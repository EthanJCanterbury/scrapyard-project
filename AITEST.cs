using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System.IO;

public class AITEST : MonoBehaviour
{
    [SerializeField] string prompt;
    private string apiURL = "https://api.groq.com/openai/v1/chat/completions";
    private string apiKEY = "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";

    IEnumerator GPTSTART()
    {
        string jsonPayload = "{\"model\": \"llama-3.3-70b-versatile\", \"messages\": [{\"role\": \"user\", \"content\": \"" + prompt + "\"}]}";
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);

        using (UnityWebRequest request = new UnityWebRequest(apiURL, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + apiKEY);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string response = request.downloadHandler.text;
                int contentStart = response.IndexOf("\"content\":\"") + 11;
                int contentEnd = response.IndexOf("\"", contentStart);
                string messageContent = response.Substring(contentStart, contentEnd - contentStart);
                print(messageContent);
            }
            else
            {
                print("Error: " + request.error);
            }
        }
    }

    void Awake()
    {
        prompt = File.ReadAllText("E:/justdab/Unityprojects/PassionFruits/Assets/chatPrompt.txt");
        Debug.Log("awake");
        StartCoroutine("GPTSTART");
    }
}
