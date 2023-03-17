using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using GLTFast;
using System.Runtime.InteropServices;
using UnityEngine.Networking;
using Dtype;


namespace DtypeUI
{
    public class Management : MonoBehaviour
    {

        [DllImport("__Internal")]
        public static extern void OpenFileBrowserWebGL(string callbackObjectName, string callbackMethodName);



        public IButton ibuttonDown;

        void Start()
        {
#if UNITY_EDITOR
            ibuttonDown.onClick.AddListener(() => OpenFileBrowser());
#elif UNITY_WEBGL
                ibuttonDown.onDown.AddListener(() => OpenFileBrowser());
#endif
        }



        //todo 이미지/영상/음악파일 불러오기 
        public void OpenFileBrowser()
        {

#if UNITY_WEBGL && !UNITY_EDITOR
        OpenFileBrowserWebGL(this.gameObject.name);
#elif UNITY_EDITOR
            OpenFileBrowserEditor(EditorUtility.OpenFilePanel("Load File", "", "glb,gltf"));
#endif
        }

        //URL로 생성 => 에디터전용 
        void OpenFileBrowserEditor(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                UIManager.Notice(0, "Path is Empty");
                return;
            }

            //todo 이미지/영상/음악파일 불러오기 
            if (filePath.Substring(filePath.Length - 3) == "glb" || filePath.Substring(filePath.Length - 4) == "gltf")
            {
                LoadGlb(File.ReadAllBytes(filePath));
                return;
            }

            UIManager.Notice(1, "Not supported file");
            return;
        }



        #region jslib-Callback

        //자바스크립트에서 호출받음
        //파일형태에 따라 따로 호출
        //Plugin/WebGL/FileBrowser.jslib 참고

        void LoadGlbWebGL(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return;
            }

            ServerManager.Inst.DownloadGLBByte(url, data =>
            {
                LoadGlb(data);
            });
        }

        //todo 이미지파일, 동영상파일, mp3파일 등
        void LoadImageWebGL(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return;
            }
        }


        #endregion

        //binary로 생성
        async void LoadGlb(byte[] data)
        {
            Debug.Log("LoadGlb:" + data.Length);
            var gltf = new GLTFast.GltfImport();
            bool success = await gltf.Load(data);
            if (success)
            {
                try
                {
                    success = await gltf.InstantiateSceneAsync(DtypeObjectManager.Dt_Transorm);
                    DtypeObjectManager.Inst.AddObject(data);
                }
                catch
                {
                    if (gltf.GetMaterial() == null)
                    {
                        UIManager.Notice(0, "지원되지 않는 텍스처입니다");
                    }
                    else if (gltf.GetMeshes() == null)
                    {
                        UIManager.Notice(0, "지원되지 않는 메쉬입니다");
                    }
                    else if (gltf.SceneCount == 0)
                    {
                        UIManager.Notice(0, "파일 정보가 없습니다");
                    }
                    else
                    {
                        UIManager.Notice(0, "사용할 수 없는 파일입니다");
                    }
                }
            }
        }
        async void LoadGlb(byte[] data, string uri)
        {
            var gltf = new GLTFast.GltfImport();
            bool success = await gltf.Load(data, new Uri(uri));
            if (success)
            {
                try
                {
                    success = await gltf.InstantiateSceneAsync(DtypeObjectManager.Dt_Transorm);
                    DtypeObjectManager.Inst.AddObject(data);
                }
                catch
                {
                    if (gltf.GetMaterial() == null)
                    {
                        UIManager.Notice(0, "지원되지 않는 텍스처입니다");
                    }
                    else if (gltf.GetMeshes() == null)
                    {
                        UIManager.Notice(0, "지원되지 않는 메쉬입니다");
                    }
                    else if (gltf.SceneCount == 0)
                    {
                        UIManager.Notice(0, "파일 정보가 없습니다");
                    }
                    else
                    {
                        UIManager.Notice(0, "사용할 수 없는 파일입니다");
                    }
                }
            }
        }



    }
}
