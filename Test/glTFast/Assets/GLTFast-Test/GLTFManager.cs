using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using GLTFast;
using System.Runtime.InteropServices;
using UnityEngine.Networking;


public class GLTFManager : MonoBehaviour
{
    [DllImport("__Internal")]
    public static extern void OpenFileBrowserWebGL(string callbackObjectName, string callbackMethodName);


    public static GLTFManager Inst;

    void Awake()
    {
        if (GLTFManager.Inst == null)
        {
            GLTFManager.Inst = this;
            // DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public Material mat;

    public IButton ibuttonDown;
    public string gltfUrl = "https://s3.ap-northeast-2.amazonaws.com/com.test.addressable231/GLTF/uploads_files_3999951_heart2_glb.glb";


    void Start()
    {
#if UNITY_EDITOR
        ibuttonDown.onClick.AddListener(() => OpenFileBrowser());
#elif UNITY_WEBGL
        ibuttonDown.onDown.AddListener(() => OpenFileBrowser());
#endif
    }


    // public void LoadOnRuntime()
    // {
    //     var gltf = gameObject.AddComponent<GLTFast.GltfAsset>();
    //     gltf.Url = gltfUrl;
    // }



    public void OpenFileBrowser()
    {

#if UNITY_WEBGL && !UNITY_EDITOR
        OpenFileBrowser(this.gameObject.name, "DownloadGLBByte");
#endif

#if UNITY_EDITOR
        LoadGlbOnEditor(EditorUtility.OpenFilePanel("Load glb File", "", "glb,gltf"));
        // LoadGlb(EditorUtility.OpenFilePanel("Load glb File", "", ""));
#endif
    }

    //URL로 생성 => 에디터전용 
    async void LoadGlbOnEditor(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            Debug.Log("Path is Empty");
            return;
        }
        else if (filePath.Substring(filePath.Length - 3) != "glb" && filePath.Substring(filePath.Length - 4) != "gltf")
        {
            Debug.Log("Not GLB File");
            return;
        }

        byte[] data = File.ReadAllBytes(filePath);
        Debug.Log(data.Length);
        var gltf = new GltfImport();

        bool success = await gltf.Load(data);
        if (success)
        {

            Debug.Log(gltf.SceneCount);
            // var instantiator = new GameObjectInstantiator(gltf, transform);
            success = await gltf.InstantiateSceneAsync(transform);

            // if (success)
            // {
            //     //추가된 오브젝트 전처리 
            //     transform.GetChild(transform.childCount - 1).gameObject.name = "JJK";
            // }
        }
    }

    void DownloadGLBByte(string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            return;
        }
        Debug.Log("DownloadGLBByte:" + url);
        StartCoroutine(DownLoadGlbByteCoroutine(url, (data) =>
        {
            LoadGlb(data, url);
        }));
    }
    IEnumerator DownLoadGlbByteCoroutine(string url, Action<byte[]> act)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            if (www.isDone)
            {
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
                act(www.downloadHandler.data);
            }
        }
    }


    //binary로 생성
    async void LoadGlb(byte[] data, string url)
    {
        Debug.Log("LoadGlb:" + data.Length);
        var gltf = new GLTFast.GltfImport();
        bool success = await gltf.Load(data);
        if (success)
        {
            if (gltf.GetMaterial() == null)
            {
                Application.ExternalEval(@"alert('지원하지 않는 텍스처입니다');");
                return;
            }
            success = await gltf.InstantiateMainSceneAsync(transform);

            // if (success)
            // {
            //     Debug.Log("DDD:" + success);
            //     //추가된 오브젝트 전처리 
            //     transform.GetChild(transform.childCount - 1).gameObject.name = "JJK";
            //     Debug.Log("EEE:" + success);
            // }
        }
    }


    // void LoadGlb(string url)
    // {
    //     url = "C:/Users/user/Downloads/uploads_files_3999951_heart2_glb.glb";
    //     Debug.Log(url);
    //     Debug.Log("AAA");
    //     GameObject obj = new GameObject();
    //     Debug.Log("BBB");
    //     var gltf = obj.AddComponent<GLTFast.GltfAsset>();
    //     Debug.Log("CCC");
    //     gltf.Url = url;
    //     Debug.LogError("url:" + url);
    // }

    IEnumerator GetData(string url, byte[] data, UnityEngine.Events.UnityAction<bool> isSuccess)
    {
        using (UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            if (www.isDone)
            {
                if (!(www.result == UnityEngine.Networking.UnityWebRequest.Result.Success))
                {
                    isSuccess(false);
                    yield break;
                }

                data = www.downloadHandler.data;
                Debug.Log(data.Length);

                isSuccess(true);
            }
            www.Dispose();
        }
    }



    public void OpenFileBrowser(string callbackObjectName, string callbackMethodName)
    {
        OpenFileBrowserWebGL(callbackObjectName, callbackMethodName);


        /* 
        Application.ExternalEval(
                @"
var function_upload = function() {
    document.removeEventListener('click', function_upload);

    var fileuploader = document.getElementById('fileuploader');
    if (!fileuploader) 
    {
        fileuploader = document.createElement('input');
        fileuploader.setAttribute('style','display:none;');
        fileuploader.setAttribute('type', 'file');
        fileuploader.setAttribute('id', 'fileuploader');
        fileuploader.setAttribute('class', 'focused');
        fileuploader.setAttribute('accept', '.glb');
        document.getElementsByTagName('body')[0].appendChild(fileuploader);

        fileuploader.onchange = function(e) {
        var files = e.target.files;
            for (var i = 0, f; f = files[i]; i++) {
                // var filePath = URL.createObjectURL(f);
                if(f.name.substr(f.name.length-3)=='glb')
                {
				//URL.createObjectURL(f);    
                  SendMessage('" + callbackObjectName + @"', '" + callbackMethodName + @"', URL.createObjectURL(f)); 
                }
                else
                {
                    alert('it is not glb file');
                }
            }
        };
    }

    if (fileuploader.getAttribute('class') == 'focused') {
        fileuploader.setAttribute('class', '');
        fileuploader.click();
    }
}

var fileuploader = document.getElementById('fileuploader');
if(fileuploader)
{
	document.getElementById('fileuploader').disabled = true;
	document.removeEventListener('click', function_upload);
	fileuploader.parentNode.removeChild(fileuploader);
}

document.addEventListener('click', function_upload);
            ");
        */
    }







}
