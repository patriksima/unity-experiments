using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Runtime.Serialization.Formatters.Binary;

namespace Dupa
{
    public class InventoryLoader
    {
        public void Save(ArrayList items)
        {
            FileStream fileStream = null;
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            try
            {
                fileStream = new FileStream(Application.persistentDataPath + "/Quickbar.bin", FileMode.OpenOrCreate);
                binaryFormatter.Serialize(fileStream, items);
            } catch
            {
                Debug.LogError("I/O error - Inventory cannot be saved.");
            } finally
            {
                if (fileStream != null)
                    fileStream.Close();
            }
        }

        public ArrayList Load()
        {
            ArrayList items = new ArrayList();
            FileStream fileStream = null;
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            try
            {
                fileStream = new FileStream(Application.persistentDataPath + "/Quickbar.bin", FileMode.Open);
                items = binaryFormatter.Deserialize(fileStream) as ArrayList;
            }
            catch
            {
                Debug.LogWarning("I/O error - Inventory cannot be loaded.");
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Close();
            }

            return items;
        }
    }
}