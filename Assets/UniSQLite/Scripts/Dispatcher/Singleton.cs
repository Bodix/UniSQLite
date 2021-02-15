﻿// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace UniSQLite.Dispatcher
{
    public abstract class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
    {
        [SerializeField]
        protected bool dontDestroyOnLoad = true;

        public static bool Logs = true;
        public static bool FindInstanceIfNull = false;
        public static bool CreateInstanceIfNull = false;

        private static readonly object lockObject = new object();
        private static readonly string debugPrefix = $"[Singleton<{typeof(T).Name}>] ";
        private static T instance;
        private static bool isDestroyed;

        public static T Instance
        {
            get
            {
                if (isDestroyed)
                {
                    if (Logs)
                        Debug.LogError(debugPrefix + "The instance will not be returned because it is already destroyed");

                    return null;
                }

                lock (lockObject)
                {
                    if (instance == null)
                    {
                        if (FindInstanceIfNull)
                            instance = FindObjectOfType<T>();

                        if (instance != null)
                        {
                            if (Logs)
                                Debug.Log(debugPrefix + "An instance was found on the scene");

                            return instance;
                        }
                        else if (CreateInstanceIfNull)
                        {
                            if (Logs)
                                Debug.Log(debugPrefix + "An instance is needed on the scene " +
                                          "and no existing instances were found, so a new instance will be created");

                            return new GameObject($"{typeof(T).Name} (Singleton)").AddComponent<T>();
                        }
                    }

                    return instance;
                }
            }
        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = (T) this;

                if (dontDestroyOnLoad)
                {
                    transform.SetParent(null);

                    DontDestroyOnLoad(gameObject);
                }
            }
            else if (instance != this)
            {
                Debug.LogWarning(debugPrefix + "The instance is already exists, so this instance will be destroyed");

                Destroy(gameObject);
            }
        }

        protected virtual void OnApplicationQuit()
        {
            isDestroyed = true;
        }
    }
}