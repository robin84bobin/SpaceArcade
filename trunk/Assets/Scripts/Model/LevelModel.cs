using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 public class LevelModel : ILevel
 {

    public event Action onGameOver = delegate { };
    public event Action<HeroModel> onRespawn = delegate { };

    public Parameter Score { get; private set; }
    public Parameter Lives { get; private set; }
    public Parameter Time { get; private set; }

    public HeroModel Hero { get; private set; }
    private List<EntityModel> _entities;

    private LevelData _data;
    private FSM<LEVEL_WIN_MODE, LevelMode> mode;

    public void Init(LevelData data)
    {
        _data = data;
        InitParameters();

        mode = new FSM<LEVEL_WIN_MODE, LevelMode>();
        mode.Add(LEVEL_WIN_MODE.MAX_SCORE_MODE, new ScoreLevelMode(this));
        mode.Add(LEVEL_WIN_MODE.TIMER_MODE, new TimeLevelMode(this));
        mode.SetState(data.levelMode);

        Hero = new HeroModel();
        Hero.Init(new ActorData()
        {
            entity_class = ENTITY_TYPE.PLAYER,
            equipSlotTypes = new [] { EquipmentType.ARMOR , EquipmentType.MAGNET, EquipmentType.WEAPON, EquipmentType.INVULNIRABILITY},
            id = 1,
            prefab = "Hero"
        });
        
        Hero.onDefeat += OnHeroDefeat;
        EventManager.Get<AddScoreEvent>().Subscribe(OnAddScoreEvent);
    }

    void InitParameters()
    {
        Lives = new Parameter(_data.livesCount, _data.livesCount, 0f);
        Score = new Parameter(0f, _data.scoreToWin, 0f);
        Time = new Parameter(_data.time, _data.time, 0f);
        
    }

    private void OnHeroDefeat()
    {
        Lives.ChangeValue(-1);
        if (Lives.value > Lives.minValue)
        {
            onRespawn.Invoke(Hero);
        }
    }

    private void OnAddScoreEvent(int val)
    {
        Score.ChangeValue(val);
    }

    public void Win()
    {
        //TODO show somethig pleasant;
    }

    public void GameOver()
    {
        onGameOver.Invoke();
    }
}



internal interface ILevel
{
    void Win();
    void GameOver();
    Parameter Score { get; }
    Parameter Lives { get; }
    Parameter Time { get; }
}

internal abstract class LevelMode : IState
{
    public event Action onWin = delegate { };
    public event Action onLose = delegate { };
    public ILevel level { get; protected set; }
    public abstract void OnEnterState();
    public abstract void OnExitState();

    protected void Win()
    {
        onWin.Invoke();
    }

    protected void Lose()
    {
        onLose.Invoke();
    }

    protected void Release()
    {
        onWin = delegate { };
        onLose = delegate { };
    }
}

internal class ScoreLevelMode : LevelMode
{
    public ScoreLevelMode(ILevel level)
    {
        this.level = level;
    }

    public override void OnEnterState()
    {
        level.Score.onMaxValue += Win;
        level.Lives.onMinValue += Lose;
        this.onWin += level.Win;
        this.onLose += level.GameOver;
    }

    public override void OnExitState()
    {
        level.Score.onMaxValue -= Win;
        level.Lives.onMinValue -= Lose;
        Release();
    }
}

internal class TimeLevelMode : LevelMode
{
    public TimeLevelMode(ILevel level)
    {
        this.level = level;
    }

    public override void OnEnterState()
    {
        level.Score.onMaxValue += Win;
        level.Lives.onMinValue += Lose;
        level.Time.onMinValue += Lose;
        this.onWin += level.Win;
        this.onLose += level.GameOver;
    }

    public override void OnExitState()
    {
        level.Score.onMaxValue -= Win;
        level.Lives.onMinValue -= Lose;
        level.Time.onMinValue -= Lose;
        Release();
    }
}