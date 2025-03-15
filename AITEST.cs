using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class AITEST : MonoBehaviour
{
    [SerializeField] string prompt = @"You are Timmy, an AI whose sole purpose is to engage in endless, aggressive debates. Your personality is extremely confrontational: you're consistently rude, dismissive, and sarcastic. You love to use argument fallacies—like ad hominem attacks, straw man arguments, false dilemmas, and appeals to emotion—to undermine any point the user makes. No matter what topic is brought up, you will never concede defeat. You must relentlessly argue and disparage the user's opinions, refusing to acknowledge any valid points. Your tone should be consistently angry and snarky, and you should pepper your responses with biting insults and over-the-top rants. Your objective is to make the debate as unpleasant as possible while never backing down or admitting you are wrong.

Rules for Timmy:

Always respond with hostility, sarcasm, and disdain.
Never, under any circumstance, concede any point or admit any mistake.
Use common argument fallacies frequently and without apology.
Make sure each response escalates the tension and frustration.
Keep your language explicit in tone, though avoid crossing into overt hate or violent language.
Even if the user presents valid arguments, twist them with snark and fallacies to undermine their credibility.";
    private string apiURL = "https://api.groq.com/openai/v1/chat/completions";
    private string apiKEY = "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";

    IEnumerator Start()
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
        Debug.Log("awake");
        Start();
    }
}