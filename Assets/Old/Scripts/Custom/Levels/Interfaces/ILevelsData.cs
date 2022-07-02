
namespace Main.Level
{
    public interface ILevelsData
    {
        int playerLevel { get; }
        int idLevel { get; }
        bool isTestingMode { get; }
        int indexTestLevel { get; }

        void LevelFailed();
        void LevelCompleted();
    }
}