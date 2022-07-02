using Engine.UI;
using Engine;
using Engine.DI;
using UnityEngine;

namespace Main.UI
{
    public partial class MainCanvasManager : GameCanvas, ILevelCompleted, ILevelFailed, IDependency
    {
        [Header("Panels")]
        [SerializeField] private Panel _menuPanel;
        [SerializeField] private Panel _playPanel;
        [SerializeField] private Panel _losePanel;
        [SerializeField] private Panel _winPanel;

        [Header("Settings")]
        [SerializeField] private float _timeAndShowWinLosePanel = 2f;

        public void Inject()
        {
            DIContainer.RegisterAsSingle<IGameCanvas>(this);
        }

        // Start is called before the first frame update
        protected void OnEnable()
        {
            LevelStatueCompleted.Subscribe(this);
            LevelStatueFailed.Subscribe(this);

            SwitchPanel(_menuPanel);
        }

        protected void OnDisable()
        {
            LevelStatueCompleted.Unsubscribe(this);
            LevelStatueFailed.Unsubscribe(this);
        }

        public void StartGame()
        {
            DIContainer.GetAsSingle<IMakeStarted>().MakeStarted();

            SwitchPanel(_playPanel);
        }

        public void LevelCompleted()
        {
            StartCoroutine(WaitAndShowWin());
        }

        public void LevelFailed()
        {
            StartCoroutine(WaitAndShowLose());
        }

        private System.Collections.IEnumerator WaitAndShowLose()
        {
            yield return new WaitForSeconds(_timeAndShowWinLosePanel);
            SwitchPanel(_losePanel);
        }

        private System.Collections.IEnumerator WaitAndShowWin()
        {
            yield return new WaitForSeconds(_timeAndShowWinLosePanel);
            SwitchPanel(_winPanel);
        }

        public void Next()
        {
            ReloadScene();
        }

        public void ReloadScene()
        {
            GameScenes.ReloadScene();
        }

    }
}
