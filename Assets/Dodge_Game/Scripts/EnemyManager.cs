using DG.Tweening;
using QuangDM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    public List<EnemyController> allEnemiesUseInGame;
    private List<GameObject> currentEnemies = new List<GameObject>();
    private List<string> tagAllEnemiesUseInGame = new List<string>();

    private float timeSpawn;
    public float durationSpawn = 1;

    private void Awake()
    {
        Instance = this;
        foreach (EnemyController enemy in allEnemiesUseInGame)
        {
            tagAllEnemiesUseInGame.Add(enemy.gameObject.tag);
        }
    }
    private void Update()
    {
        if (GameManager.Instance.isGameRuning)
        {
            timeSpawn += Time.deltaTime;
            if (timeSpawn > durationSpawn || GameManager.Instance.EnemiesRemain == 0)
            {
                SpawnRandomEnemy();
                //SpwanEnemy(0);
                timeSpawn = 0;
                durationSpawn = Random.Range(3f, 5f);
            }
        }
    }
    public void SpawnRandomEnemy()
    {
        int randomEnemiesRate = Random.Range(0, 100);
        if (0 <= randomEnemiesRate && randomEnemiesRate < 34)
        {
            SpwanEnemy(Random.Range(0, 2));
            return;
        }
        if(35<=randomEnemiesRate && randomEnemiesRate <70)
        {
            SpwanEnemy(Random.Range(2, 4));
            return;
        }
        if(70<=randomEnemiesRate && randomEnemiesRate<90)
        {
            SpwanEnemy(Random.Range(4, 6));
            return;
        }
        if(90 <= randomEnemiesRate && randomEnemiesRate < 100)
        {
            SpwanEnemy(Random.Range(6, 8));
            return;
        }
    }
    public void SpwanEnemy(int enemyID)
    {
        GameObject go_Enemy = ObjectPool.Instance.Spawn(tagAllEnemiesUseInGame[enemyID]);
        if (go_Enemy == null)
            return;
        go_Enemy.transform.position = NewRandomVector3();
        go_Enemy.transform.rotation = Quaternion.identity;
        GameManager.Instance.EnemiesRemain++;
        currentEnemies.Add(go_Enemy);
    }
    public void ClearAllCurrentEnemies()
    {
        foreach (GameObject enemy in currentEnemies)
        {
            enemy.gameObject.SetActive(false);
        }
        currentEnemies.Clear();
        GameManager.Instance.EnemiesRemain = 0;
    }
    private Vector3 NewRandomVector3()
    {
        float x = -1000, y = -1000, z = -1000;
        float top, bot, right, left;
        top = GameManager.Instance.currentMap.TopLimit.transform.position.z;
        bot = GameManager.Instance.currentMap.BotLimit.transform.position.z;
        right = GameManager.Instance.currentMap.RightLimit.transform.position.x;
        left = GameManager.Instance.currentMap.LeftLimit.transform.position.x;


        while (x > right || x < left || x < (PlayerController.Instance.transform.position.x + 10f) && x > (PlayerController.Instance.transform.position.x - 10f))
        {
            x = Random.Range(right, left);
        }
        while (z > top || z < bot || z < (PlayerController.Instance.transform.position.x + 10f) && z > (PlayerController.Instance.transform.position.x - 10f))
        {
            z = Random.Range(top, bot);
        }

        y = 1.94f;

        return new Vector3(x, y, z);
    }
}
