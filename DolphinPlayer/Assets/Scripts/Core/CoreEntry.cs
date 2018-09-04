using Core.UI;
using Core.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Asset;

namespace Core
{
    public class CoreEntry : MonoBehaviour
    {
        public static CoreEntry Instance = null;

        public LifeCycle _LifeCycle = null;

        public bool InitFinished { get; private set; }

        private IEnumerator InitCore()
        {
            //启动驱动逻辑
            InvokeRepeating("Tick", 1, 0.02f);

            yield return ResourceManager.Init();
            //分批初始化管理器
            yield return _LifeCycle.Singletons.InitCoroutine(0, RefreshProgress);

            yield return 1;

            InitFinished = true;
        }

        IEnumerator RefreshProgress(string stepName, int step, int count)
        {
            Debug.LogFormat("CoreEntry RefreshProgress stepName {0},step {1}, count {2}", stepName, step, count);
            yield return 1;
        }

        void Awake()
        {
            Debug.Log("CoreEntry Awake");
            Instance = this;
            _LifeCycle = null;
            InitFinished = false;
            DontDestroyOnLoad(gameObject);
            Screen.sleepTimeout = (int)SleepTimeout.NeverSleep;

#if (UNITY_ANDROID || UNITY_IOS)
		    Application.targetFrameRate = 30;
#else
            Application.targetFrameRate = 60;
#endif

        }
        // Use this for initialization
        void Start()
        {
            _LifeCycle = gameObject.AddComponent<LifeCycle>();
            _LifeCycle.Add(0, new UIManager());//UI
            StartCoroutine(InitCore());
        }

        // Update is called once per frame
        void Update()
        {

        }
        void LateUpdate()
        {
        }
        void FixedUpdate()
        {
        }
        void EndOfFrame()
        {

        }

        void OnApplicationQuit()
        {
            _LifeCycle.Singletons.Exit();

            //if (CoreEnv._World != null)
            //    CoreEnv._World.UnInit();

            //GameObjectPool.Clear();

            //WrapHelper.FreeLibrary();

            //LogHelper.FlushFile();
            ////LogHelper.DEBUG("CoreEntry", "Core Quit");
        }

        void Tick()
        {
            if (!InitFinished)
                return;
            TimerHeap.Tick();
            FrameTimerHeap.Tick();
        }

        public static void Invoke(Action action)
        {
            TimerHeap.AddTimer(0, 0, action);
        }
    }
}

