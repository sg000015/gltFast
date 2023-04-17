using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class TTSManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SpeakInit();
    [DllImport("__Internal")]
    private static extern void Speak(string str);

    [DllImport("__Internal")]
    private static extern void StopSpeak();

    string[] myTest = new string[3] {
             "반갑습니다", "안녕,안녕,안녕,안녕하세요", "감사 감사 감사합니다"
             };


    public TMPro.TMP_InputField inputField;


    void Start()
    {
        SpeakInit();
    }

    public void SpeakText(int index)
    {
        Speak(myTest[index]);
    }

    public void SpeakCustom()
    {
        Debug.Log(inputField.text);

        Speak(inputField.text);
    }



    public void Stop()
    {
        StopSpeak();
    }



}
