using UnityEngine;
using UnityEngine.UI;

namespace Main.UI
{
    public class PanelPlay : Panel
    {
        [SerializeField] private Text _textLevel;

        private Level.ILevelsData m_LevelsData;

        protected void Start()
        {
            InitializedTextLevel();
        }

        private void InitializedTextLevel()
        {
            Level.ILevelsData m_LevelsData = Engine.DI.DIContainer.GetAsSingle<Level.ILevelsData>();

            int clevel = m_LevelsData.playerLevel;
            if (clevel < 10)
            {
                _textLevel.text = "LEVEL 0" + clevel;
            }
            else
            {
                _textLevel.text = "LEVEL " + clevel;
            }
        }

        public void ReloadScene()
        {
            GameScenes.ReloadScene();
        }
    }
}
