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
        #region Events
        public delegate void Loaded(List<Item> itemTemplates);
        public event Loaded OnLoaded;
        #endregion

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
            // Loading addresable assets by Items label
            Addressables.LoadAssetsAsync<Item>("Items", null).Completed += obj => {
                if (obj.Result?.Count > 0)
                {
                    OnLoaded?.Invoke(obj.Result as List<Item>);
                }
            };

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
    }
}