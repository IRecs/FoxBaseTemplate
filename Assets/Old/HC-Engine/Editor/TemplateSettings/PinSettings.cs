using Engine.Attribute;
using UnityEngine;

namespace editor.pin
{
    [TemplateSettings(k_PinSettingsPath, k_PinSettingsName)]
    public partial class PinSettings : ScriptableObject
    {
        internal const string k_PinSettingsPath = "Assets/HC-Engine/Editor/Settings/";
        internal const string k_PinSettingsName = "PinSettings";

        [SerializeField] internal Vector2 m_ButtonScale = new Vector2(80, 20);
        [SerializeField] internal PinInfo[] m_Pins;


        internal static PinSettings GetPinSettings()
        {
            return AssetUtility.GetOrCreateAsset<PinSettings>(k_PinSettingsPath, k_PinSettingsName + ".asset");
        }

        internal void OnPinInfoUpdated(PinInfo[] pinInfo)
        {
            m_Pins = pinInfo;
        }

        private void OnValidate()
        {
            PinListInfo.UpdatePinsInfo(m_Pins);
        }
    }
}