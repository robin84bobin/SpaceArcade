using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts.UI.Windows;
using UnityEngine.SceneManagement;

public enum LEVEL_WIN_MODE
{
    TIMER_MODE,
    MAX_SCORE_MODE
}

public class LevelController : MonoBehaviour
{
    [Header("LEVEL SETTINGS:")]
    [SerializeField]
    private LevelData _levelData;
   
    [Header("HERO:")]
    [SerializeField]
    private Transform heroSpawnPoint;
    [SerializeField]
    private string heroPrefab = "Hero";

    public HeroController Hero { get; private set; }
    public event Action OnLevelLoaded = delegate { };

    private LevelModel _model;

    void Awake ()
    {
        Main.Instance.level = this;
        Init();

        //Initialize level UI after level conltroller has initialized.
        LevelHUD view = FindObjectOfType<LevelHUD>();
        view.Init(_model);

        SpawnHero(false);
    }

    void Init()
    {
        _levelData = DataController.Instance.Get<LevelData>(1);
        _model = new LevelModel();
        _model.Init(_levelData);
    }

    private IEnumerator RespawnHero()
    {
        yield return new WaitForSeconds(3f);
        SpawnHero();
    }

    private void SpawnHero(bool respawn = true)
    {
        Hero = InstantiateHero();
        Hero.OnSpawn(heroSpawnPoint.position, respawn);
    }

    private void OnHeroDeathEvent()
    {
        ObjectPool.instance.PoolObject(Hero.gameObject);
        StartCoroutine(RespawnHero());
    }

    /// <summary>
    /// Create new instance of hero (get from pool).
    /// </summary>
    private HeroController InstantiateHero()
    {
        GameObject go = ObjectPool.instance.GetObject(_model.Hero.PrefabName);
        Transform t = go.transform;
        t.position = heroSpawnPoint.position;
        t.rotation = heroSpawnPoint.rotation;
        t.localScale = Vector3.one;
        go.SetActive(true);
        HeroController hero = go.GetComponent<HeroController>();
        return hero;
    }

    public void Win()
    {
        //TODO show something pleasant;
    }

    public void OnGameOver()
    {
        StartCoroutine(ShowGameOverWindow());
    }

    IEnumerator ShowGameOverWindow()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.UnloadScene("level");
        GameOverWindow.Show();
    }


}



