using Engine;
using Engine.Input;
using Main;
using Engine.DI;
using Main.Level;

public class GameManager : CoreManager<GameManager>, IMakeStarted, IMakeFailed, IMakeCompleted, IDependency
{
    public static bool isStarted { get; internal set; }
    public static bool isCompleted { get; internal set; }
    public static bool isFailed { get; internal set; }

    public static bool isFinished { get { return isFailed || isCompleted; } }
    public static bool isPlaying { get { return !isFinished && isStarted; } }


    private IGameStatue _startStatue = new LevelStatueStarted();
    private IGameStatue _failedStatue = new LevelStatueFailed();
    private IGameStatue _completedStatue = new LevelStatueCompleted();

    private ILevelsData m_LevelData;

    public void Inject()
    {
        DIContainer.RegisterAsSingle<IMakeStarted>(this);
        DIContainer.RegisterAsSingle<IMakeFailed>(this);
        DIContainer.RegisterAsSingle<IMakeCompleted>(this);
    }

    protected override void OnInitialize()
    {
        isStarted = false;
        isCompleted = false;
        isFailed = false;

#if Support_SDK
        apps.ADSManager.DisplayBanner();
#endif
    }

    public void MakeStarted()
    {
        m_LevelData = DIContainer.GetAsSingle<ILevelsData>();

        isStarted = true;

        int playerLevel = m_LevelData.playerLevel;
#if Support_SDK
        apps.ProgressEvents.OnLevelStarted(playerLevel);
#endif

        SwitchToStatue(_startStatue);
    }

    public void MakeFailed()
    {
        if (m_LevelData == null) throw new System.NullReferenceException("m_LevelData has null value, maybe the game didn't started");

        if (isFinished) return;

        isFailed = true;

        ControllerInputs.s_EnableInputs = false;

        int playerLevel = m_LevelData.playerLevel;

#if Support_SDK
        apps.ProgressEvents.OnLevelFieled(playerLevel);
#endif

        m_LevelData.LevelFailed();
        SwitchToStatue(_failedStatue);
    }

    public void MakeCompleted()
    {
        if (m_LevelData == null) throw new System.NullReferenceException("m_LevelData has null value, maybe the game didn't started");

        if (isFinished) return;

        isCompleted = true;

        ControllerInputs.s_EnableInputs = false;

        int playerLevel = m_LevelData.playerLevel;

#if Support_SDK
        apps.ProgressEvents.OnLevelCompleted(playerLevel);
#endif

        m_LevelData.LevelCompleted();
        SwitchToStatue(_completedStatue);
    }

}