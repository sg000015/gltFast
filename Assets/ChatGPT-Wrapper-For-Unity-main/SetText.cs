using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetText : MonoBehaviour
{

    public ChatGPTWrapper.ChatGPTConversation gpt;

    private string prompt = "Act as a random stranger in a chat room and reply to the questions" +
                            " Don't break character." +
                            " Don't ever mention that you are an AI model.";

    public InputField text;
    public Text result;

    void OnEnable()
    {
        gpt.chatGPTResponse.AddListener(rerere);
    }


    [ContextMenu("SendToChatGPT")]
    public void asdf()
    {
        gpt.SendToChatGPT(prompt + text.text);
    }


    [ContextMenu("Invoke")]
    public void rerere(string s)
    {

        result.text = s;
        Debug.Log(s);
    }


}
