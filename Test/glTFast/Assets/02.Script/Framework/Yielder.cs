using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;



///<summary>
///메모리 최적화를 위한 스크립트로 추정 (new 최소화)
///</summary>
public static class Yielder
{

    //Use In Client
    static Dictionary<float, WaitForSeconds> IntervalTime = new Dictionary<float, WaitForSeconds>(10);

    static WaitForEndOfFrame _endOfFrame = new WaitForEndOfFrame();
    public static WaitForEndOfFrame EndOfFrame { get { return _endOfFrame; } }

    static WaitForFixedUpdate _fixedUpdate = new WaitForFixedUpdate();
    public static WaitForFixedUpdate FixedUpdate { get { return _fixedUpdate; } }

    public static WaitForSeconds Get(float seconds)
    {
        if (!IntervalTime.ContainsKey(seconds))
        {
            IntervalTime.Add(seconds, new WaitForSeconds(seconds));
        }
        return IntervalTime[seconds];
    }


    //Use In ServerManager
    static Dictionary<UnityWebRequest, UnityWebRequestAsyncOperation> ServerRequest = new Dictionary<UnityWebRequest, UnityWebRequestAsyncOperation>(100);
    public static UnityWebRequestAsyncOperation GetRequestAsyncOperation(UnityWebRequest webRequest)
    {
        if (!ServerRequest.ContainsKey(webRequest))
        {
            ServerRequest.Add(webRequest, webRequest.SendWebRequest());
        }
        return ServerRequest[webRequest];
    }
}

