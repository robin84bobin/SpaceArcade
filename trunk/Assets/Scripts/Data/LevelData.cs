using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


[Serializable][CreateAssetMenu(fileName = "LevelData", menuName = "Data/Level")]
public class LevelData : BaseData
 {
    public LEVEL_WIN_MODE levelMode;
    [Range(0,5)]
    public float heroRespawnDelay = 5f;
    [Range(0, 5)]
    public int livesCount = 3;
    [Range(0, 1000)]
    public int scoreToWin = 100;
    [Range(0, 1000)]
    public int time = 100;
    [SerializeField]
    private string heroPrefab = "Hero";
 }

