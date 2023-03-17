using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using Dtype;
using System.IO;




///<summary>
/// Http통신, 파일 다운로드 전체 관리 (coroutine에 의해 Instance(Monobehaviour) 필요)
///</summary>
public class ServerManager : Singleton<ServerManager>
{

    #region Save/Load

    ///<summary>
    ///DataManager 싱글톤에서 전체 데이터 관리, 규격화해서 종류별로 저장
    ///</summary>
    [ContextMenu("SaveFile")]
    void SaveFile()
    {
        string resourcesPath = "Assets/Resources/SaveData";
        string path;

        path = Path.Combine(resourcesPath, "DtypeData.json");
        File.WriteAllText(path, JsonUtility.ToJson(DataManager.Inst.dtypeSaveData));

        path = Path.Combine(resourcesPath, "sample.json");
        File.WriteAllText(path, JsonUtility.ToJson(DataManager.Inst.sampleData));
    }

    ///<summary>
    ///DataManager 싱글톤에서 전체 데이터 관리, 데이터 할당시 생성로직 적용할것
    ///</summary>
    [ContextMenu("LoadFile")]
    void LoadFile()
    {
        string resourcesPath = "Assets/Resources/SaveData";
        string path;

        path = Path.Combine(resourcesPath, "DtypeData.json");
        var dtype = JsonUtility.FromJson<DataManager.DtypeSaveData>(File.ReadAllText(path));
        DataManager.Inst.dtypeSaveData = dtype;

        path = Path.Combine(resourcesPath, "DtypeData.json");
        var sample = JsonUtility.FromJson<DataManager.SampleData>(File.ReadAllText(path));
        DataManager.Inst.sampleData = sample;
    }

    #endregion




    #region WebRequest

    public void DownloadGLBByte(string url, Action<byte[]> callback)
    {
        if (string.IsNullOrEmpty(url))
        {
            return;
        }
        Debug.Log("DownloadGLBByte:" + url);
        StartCoroutine(DownLoadGlbByteCoroutine(url, (data) =>
        {
            callback(data);
            // LoadGlb(data, url);
        }));
    }
    IEnumerator DownLoadGlbByteCoroutine(string url, Action<byte[]> callback)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return Yielder.GetRequestAsyncOperation(www);
            if (www.isDone)
            {
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
                callback(www.downloadHandler.data);
            }
        }
    }

    #endregion

}
