using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelHUD : MonoBehaviour
{
    [Header("LEVEL PARAMS INDICATORS")]
    [SerializeField]
    Indicator livesIndicator;
    [SerializeField]
    Indicator scoreIndicator;

    [Header("HERO PARAMS INDICATORS")]
    [SerializeField]
    Indicator healthIndicator;
    [SerializeField]
    Indicator armorIndicator;
    [SerializeField]
    Indicator ammoIndicator;

    [Header("OTHER STUFF...")]
    public Button attackButton;
    public TouchController swipePanel;

    LevelModel _level;

    public void Init(LevelModel level)
    {
        _level = level;
        EventManager.Get<HeroDeathEvent>().Subscribe(OnHeroDeath);
        EventManager.Get<HeroSpawnEvent>().Subscribe(OnHeroSpawn);
        InitLevelIndicators();
    }

    private void OnHeroDeath()
    {
        ReleaseHeroIndicators();
    }

    private void OnHeroSpawn()
    {
        InitHeroIndicators();
    }

    void InitLevelIndicators()
    {
        livesIndicator.AttachParameter(_level.Lives);
        scoreIndicator.AttachParameter(_level.Score);
    }

    void ReleaseLevelindicators()
    {
        livesIndicator.Release();
        scoreIndicator.Release();
    }

    void InitHeroIndicators()
    {
        healthIndicator.AttachParameter(_level.Hero, ParamName.HEALTH);
        armorIndicator.AttachParameter(_level.Hero, ParamName.ARMOR);
        ammoIndicator.AttachParameter(_level.Hero, ParamName.AMMO);
    }

    void ReleaseHeroIndicators()
    {
        healthIndicator.Release();
        armorIndicator.Release();
        ammoIndicator.Release();
    }

    void Update () {
	
        if (_attack && _level.Hero != null)
        {
            _level.Hero.Attack();
        }
	}

    bool _attack;
    //Calls by LevelHUD/AttackButton/EventTrigger
    public void StartAttack()
    {
        _attack = true;
    }
    //Calls by LevelHUD/AttackButton/EventTrigger
    public void StopAttack()
    {
        _attack = false;
    }

}
