using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Newtonsoft.Json;

namespace VuLib
{
    public static class SerializationUtils
    {
        public static readonly JsonSerializerSettings JSON_SETTINGS = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        public static string SerializeToJsonString<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, JSON_SETTINGS);
        }

        public static T DeserializeFromJsonString<T>(string json)
        {
            return (T)JsonConvert.DeserializeObject(json, JSON_SETTINGS);
        }

        public static byte[] SerializeToBinaryByteArray<T>(T obj)
        {
            byte[] bytes = null;
            
            try
            {
                var formatter = new BinaryFormatter();
                
                using (var ms = new MemoryStream())
                {
                    using (var ds = new DeflateStream(ms, CompressionMode.Compress, true))
                    {
                        formatter.Serialize(ds, obj);
                    }
                    ms.Position = 0;
                    bytes = ms.GetBuffer();
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex);
            }

            return bytes;
        }

        public static T DeserializeFromBinaryByteArray<T>(byte[] bytes) where T:class
        {
            T obj = null;
            try
            {
                var formatter = new BinaryFormatter();
                using (var ms = new MemoryStream(bytes))
                {
                    using (var ds = new DeflateStream(ms, CompressionMode.Decompress, true))
                    {
                        obj = (T)formatter.Deserialize(ds);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex);
            }

            return obj;
        }
    }
}

