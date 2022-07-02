using UnityEngine;

namespace Main.Level
{
    public class Level : MonoBehaviour, ILevel
    {
        [SerializeField] private LevelInfoSO _levelInfo;
        public LevelInfoSO levelInfo => _levelInfo;


        public ILevelsManager levelsManager { get; private set; }

        public virtual void Initialize(ILevelsManager levelsManager)
        {
            this.levelsManager = levelsManager;
        }
    }
}
