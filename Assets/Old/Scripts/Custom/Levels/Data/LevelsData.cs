using Engine;
using Data;
using Engine.Random;
using UnityEngine;
using Engine.Attribute;

namespace Main.Level
{
    [TemplateSettings("Assets/ScriptableObjects/", "Levels Data")]
    [CreateAssetMenu(fileName = "New Levels Data", menuName = "Add/More/Levels Data", order = 7)]
    public partial class LevelsData : ScriptableObject, IData, ILevelsData, IAwake, Engine.DI.IDependency
    {
        [System.Serializable]
        public struct Data
        {
            public int idLevel;
            public int playerLevel;
            public bool randomLevels;

            /// <summary>
            /// Restore the data to the default values.
            /// </summary>
            /// <param name="initScore"> The Init score. When player start play first time. </param>
            public void Reset()
            {
                idLevel = 0;
                playerLevel = 1;
                randomLevels = false;
            }
        }

        [SerializeField] private int _idData;
        [SerializeField] private Data _data;

        [Header("Level Settings")]
        [SerializeField] private bool m_IsTestingMode = false;
        [SerializeField] private int m_IndexTestLevel = 0;
        [SerializeField] private int startRandomLevel = 0;

        public int playerLevel => _data.playerLevel;
        public bool isTestingMode => m_IsTestingMode;
        public int indexTestLevel => m_IndexTestLevel;
        public int idLevel => GetIDLevel();


        private int GetIDLevel()
        {
            if (m_IsTestingMode) return m_IndexTestLevel;
            
            return _data.idLevel;
        }

        public void Inject()
        {
            Engine.DI.DIContainer.RegisterAsSingle<ILevelsData>(this);
        }

        public void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _data = ES3.Load(GetKey(), ObjectSaver.GetSavingPathFile<Data>(GetKey()), _data);
        }

        /// <summary>
        /// This function execute when the anylevel completed .
        /// </summary>
        public void LevelCompleted()
        {
            int totalLevels = Engine.DI.DIContainer.GetAsSingle<ILevelsGroup>().totalLevels;
            if (_data.randomLevels)
            {
                _data.idLevel = GetRandomLevelID(_data.idLevel, totalLevels);
            }
            else
            {
                if (totalLevels - 1 <= _data.idLevel)
                {
                    _data.randomLevels = true;
                    _data.idLevel = GetRandomLevelID(_data.idLevel, totalLevels);
                }
                else
                {
                    _data.idLevel++;
                }
            }
            _data.playerLevel++;

            SaveData();
        }

        public void LevelFailed()
        {

        }

        /// <summary>
        /// Get random level with not repeated the last id level.
        /// </summary>
        public int GetRandomLevelID(int lastIdLevel, int totalLevels)
        {
            if (1 < totalLevels)
            {
                RandomFieldInfo[] fieldInfos;

                if (lastIdLevel <= startRandomLevel)
                {
                    fieldInfos = new RandomFieldInfo[1];
                    fieldInfos[0] = new RandomFieldInfo(startRandomLevel + 1, totalLevels);
                }
                else
                if (totalLevels - 1 <= lastIdLevel)
                {
                    fieldInfos = new RandomFieldInfo[1];
                    fieldInfos[0] = new RandomFieldInfo(startRandomLevel, totalLevels - 1);
                }
                else
                {
                    fieldInfos = new RandomFieldInfo[2];
                    fieldInfos[0] = new RandomFieldInfo(startRandomLevel, lastIdLevel - 1);
                    fieldInfos[1] = new RandomFieldInfo(lastIdLevel + 1, totalLevels);
                }

                return IntelligentRandom.GetRandomWithField(fieldInfos);
            }
            else
                return 0;
        }

        public void SaveData()
        {
            ES3.Save(GetKey(), _data, ObjectSaver.GetSavingPathFile<Data>(GetKey()));
        }

        public string GetKey()
        {
            return "LevelsData." + _idData;
        }

        public void ResetData(int idData)
        {
            _idData = idData;
            _data.Reset();
        }

    }
}