using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public int currentWave;
    public float remainingTime = 60;
    public static GameManager GM;
    public int point, highScore;
    public PlayerController player;

    [SerializeField] private int pointMulti;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int enemySpawnRangeValue;
    [SerializeField] private int enemyInitialCount;
    [SerializeField] private int enemyCurrentCount;
    [SerializeField] private UIManager uiManager;


    private void Awake()
    {
        GM = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyCurrentCount = enemyInitialCount;
        currentWave = 1;
        uiManager.waveText.text = "Wave: " + currentWave;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        uiManager.highScore.text = highScore.ToString();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Invoke(nameof(StartWave),1f);
    }

    // Update is called once per frame
    void Update()
    {
        //remainingTime -= Time.deltaTime;
        uiManager.UpdateTimer();
    }

    private void WaveChanged()
    {
        currentWave++;
        uiManager.waveText.text = "Wave: " + currentWave;
        enemyCurrentCount++;
        player.WeaponUpgrade();
        Invoke(nameof(StartWave),1);
    }

    public void StartWave()
    {
        for (int i = 0; i < enemyCurrentCount; i++)
        {
            var enemyPos = new Vector3(Random.Range(-enemySpawnRangeValue, enemySpawnRangeValue), 1,
                Random.Range(-enemySpawnRangeValue, enemySpawnRangeValue));
            Instantiate(enemyPrefab,enemyPos,Quaternion.identity);
        }
    }

    public void EnemyDead(GameObject enemy)
    {
        point += (int)Vector3.Distance(player.activeWeapon.shootPos, enemy.transform.position) * pointMulti;
        if (point > highScore)
        {
            highScore = point;
            PlayerPrefs.SetInt("HighScore",highScore);
            uiManager.highScore.text = highScore.ToString();
        }
        enemyCurrentCount--;
        Destroy(enemy);
        var addTime = Random.Range(2, 6);
        remainingTime += addTime;
        if (enemyCurrentCount == 0)
        {
            WaveChanged();
        }
    }
    
}
