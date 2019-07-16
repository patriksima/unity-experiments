using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Dupa
{
    public class AssetManager : MonoBehaviour
    {
        public delegate void OnLoaded(List<Item> itemTemplates);
        public event OnLoaded onLoaded;

        #region Singleton
        private static readonly object iLock = new object();
        private static AssetManager instance;

        public static AssetManager Instance
        {
            get {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = FindObjectOfType<AssetManager>();
                    }
                }
                return instance;
            }
        }
        #endregion

        private void Awake()
        {
            Debug.Log("AssetManager::Awake");

            //List<object> items = new List<object> { "AK47", "Torch" };
            Addressables.LoadAssetsAsync<Item>("Items", null).Completed += OnLoadDone;
            //Addressables.LoadAssetAsync<Item>("AK47").Completed += OnLoadDone;


            /*
             * // loading configu
               var req = Addressables.LoadAssetAsync<TextAsset>("Assets/BlackBoard/Data/" + Name + "/Default.bytes");
                        req.Completed += op =>
                        {
                            if (req.Result != null)
                            {
                                var s = new MemoryStream(req.Result.bytes);
                                entries = (BlackBoardEntry[]) binaryFormatter.Deserialize(s);
                                Addressables.Release(req);
                    
                                for (var i = 0; i < entries.Length && i < m_Entries.Capacity; i++)
                                {
                                    m_Entries.TryAdd(i, entries[i]);
                                }
                                
                                OnEntriesLoaded?.Invoke();
                            }
                        };
             */
        }

        private void OnLoadDone(AsyncOperationHandle<IList<Item>> obj)
        {
            Debug.Log("AssetManager::OnLoadDone");
            Debug.Log($"objs found {obj.Result?.Count}");

            if (obj.Result?.Count > 0)
                onLoaded?.Invoke(obj.Result as List<Item>);

            // TODO: asi nekde na OnDestroy, jinak to nefunguje
            //Addressables.Release(obj);
        }
    }
}