using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Core.Asset
{
    public class ResourceManager : MonoBehaviour
    {
        public static ResourceManager _Instance = null;

        public static string ASSET_ROOT_PATH = "Assets/";

        public static IEnumerator Init()
        {
            if (!Application.isPlaying)
                yield break;

            var resManager = FindObjectOfType(typeof(ResourceManager)) as ResourceManager;
            if(resManager == null)
            {
                var gameObj = new GameObject("_ResourceManager");
                ResourceManager rm = gameObj.GetComponent<ResourceManager>();
                if (rm == null)
                    rm = gameObj.AddComponent<ResourceManager>();
                _Instance = resManager;
            }

            yield return 1;
        }

        public static GameObject LoadPrefab(string path)
        {
            Object obj = LoadAsset(path, typeof(GameObject));
            if (obj == null)
                return null;
            return Object.Instantiate(obj) as GameObject;
        }

        private static Object LoadAsset(string assetPath, Type type)
        {
            if (string.IsNullOrEmpty(assetPath))
                return null;

            var extensions = ResourceExtensions.GetExtOfType(type);
            foreach(var ext in extensions)
            {
                var assetPathWithExt = assetPath + ext;
                Object asset = null;
#if UNITY_EDITOR
                asset = AssetDatabase.LoadAssetAtPath(ASSET_ROOT_PATH + assetPathWithExt, type);
#endif
                if (asset == null)
                    continue;

                return asset;
            }
            Debug.LogFormat("ResourceManager loading file {0} failed, type {1}", assetPath, type);
            return null;
        }
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

