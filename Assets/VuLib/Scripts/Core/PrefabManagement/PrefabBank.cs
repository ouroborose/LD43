﻿using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif

namespace VuLib
{
    [CreateAssetMenu(fileName = "PrefabBank", menuName = "LD43/Data/PrefabBank")]
    public class PrefabBank : BaseBankScriptableObject
    {
        public bool _updateIds = true;

        public List<BasePrefabIdentifier> _prefabs = new List<BasePrefabIdentifier>();

#if UNITY_EDITOR
        [MenuItem("LD43/Populate All PrefabBanks")]
        public static void RepopulateAll()
        {
            RepopulateBanks<PrefabBank>("LD43/ReferencedResources/Data/PrefabBanks");
        }

        public override string GetRootDataPath()
        {
            return "LD43/ReferencedResources/Prefabs";
        }

        public override void ClearBank()
        {
            base.ClearBank();
            _prefabs.Clear();
        }

        protected override void OnDataFound(string pathToAsset)
        {
            base.OnDataFound(pathToAsset);
            //Debug.Log(pathToAsset);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(pathToAsset);
            if (prefab != null)
            {
                BasePrefabIdentifier identifier = prefab.GetComponent<BasePrefabIdentifier>();
                if (identifier != null)
                {
                    if (_updateIds)
                    {
                        identifier.GenerateId();
                    }
                    _prefabs.Add(identifier);
                }
            }
        }
#endif
    }
}