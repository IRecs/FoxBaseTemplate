using Engine.Attribute;
using UnityEngine;

namespace editor
{
    [TemplateSettings(k_AssetsSettingsPath, k_AssetsSettingsName)]
    public class AssetsSettings : ScriptableObject
    {
        internal const string k_AssetsSettingsPath = "Assets/HC-Engine/Editor/Settings/";
        internal const string k_AssetsSettingsName = "AssetsSettings";

        [Header("Build Settings")]
        public bool resetData = false;
        public bool validate = false;
        public bool saveAssets = false;
    }
}
