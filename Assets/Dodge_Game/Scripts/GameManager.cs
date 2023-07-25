using UnityEngine;
using System.Collections;
using TMPro;
using QuangDM.Common;
using System.Collections.Generic;

public enum GAME_STATE
{
    init,
    playing,
    pause,
    over
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] GAME_STATE GameState = GAME_STATE.init;
    public bool gameStarted => GameState == GAME_STATE.playing; // get dữ liệu gamestarted trả về trạng thái playing
   
    public bool isGameRuning;

    private bool CanSpawnEnemy;
    private bool CanSpawnPowerItem;

    public Map currentMap;

    private int _CurrentScore;
    public int CurrentScore
    {
        get
        {
            return _CurrentScore;
        }
        set
        {
            _CurrentScore = value;
            UIManager.Instance.inGame.SetScore(_CurrentScore);
            UIManager.Instance.gameOver.SetScore(_CurrentScore);
            if (_CurrentScore > PlayerData.Instance.BestScore)
            {
                PlayerData.Instance.BestScore = _CurrentScore;
                UIManager.Instance.gameOver.SetBestScore(PlayerData.Instance.BestScore);
            }
        }
    }
    private int _EnemiesRemain;
    public int EnemiesRemain
    {
        get
        {
            return _EnemiesRemain;
        }
        set
        {
            _EnemiesRemain = value;
            UIManager.Instance.inGame.SetEnemiesRemain(_EnemiesRemain);
        }
    }

    public TMP_Text fpsText;
    private float deltaTime;
    [Header("--------Skin----------")]
    public List<DataSkin> skinPlayer;

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = "FPS: " + Mathf.Ceil(fps).ToString();
    }

    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 120;
        PlayerData.Instance.Load();
    }
    void Start()
    {
        UIManager.Instance.gameOver.SetBestScore(PlayerData.Instance.BestScore);
        UIManager.Instance.ShowBeginGame();
        SoundManager.Instance.PlayBG(SoundTag.Bgm_home);
        SkinInGame();
    }
    public void PlayerDead()
    {
        isGameRuning = false;
    }
    public void EnemyDie(int Score)
    {
        CurrentScore += Score;
        EnemiesRemain--;
    }
    public void GetItemPower(PowerType type)
    {
        PowerItemManager.Instance.GetItemPower(type);
    }
    public void GameOver()
    {
        GameManager.Instance.isGameRuning = false;
        PlayerData.Instance.Save();
        SoundManager.Instance.PlaySFX(SoundTag.SFX_Lose_Screen);
        UIManager.Instance.ShowGameOver();
    }
    public void Revive()
    {
        CameraController.Instance.ZoomOut();
        GameManager.Instance.isGameRuning = true;
        EnemyManager.Instance.ClearAllCurrentEnemies();
        EnemyManager.Instance.durationSpawn = 1;
        UIManager.Instance.ShowInGame();
        SoundManager.Instance.PlayBG(SoundTag.Bgm_ingame);
        PlayerController.Instance.isLive = true;
        PlayerController.Instance.anim.SetTrigger("run");
        PlayerController.Instance.GetComponent<Collider>().enabled = true;
    }
    public void RelayGame()
    {
        EnemyManager.Instance.ClearAllCurrentEnemies();
        //StartGame();
        UIManager.Instance.ShowBeginGame();
        SoundManager.Instance.PlayBG(SoundTag.Bgm_home);
    }
    public void StartGame()
    {
        GameState = GAME_STATE.playing;

        CameraController.Instance.ZoomOut();
        UIManager.Instance.ShowInGame();
        SoundManager.Instance.PlayBG(SoundTag.Bgm_ingame);
        CurrentScore = 0;
        EnemiesRemain = 0;
        isGameRuning = true;
        PlayerController.Instance.gameObject.transform.position = PlayerController.Instance.basePos;
        PlayerController.Instance.isLive = true;
        PlayerController.Instance.anim.SetTrigger("run");
        PlayerController.Instance.GetComponent<Collider>().enabled = true;
        EnemyManager.Instance.durationSpawn = 1;
    }
    public void SkinInGame()
    {
        StartCoroutine(waitTimeSpawn());    
    }
    IEnumerator waitTimeSpawn()
    {
        yield return new WaitForEndOfFrame();
        Instantiate(skinPlayer[0].modelCharacter, PlayerController.Instance.posModelCharacter);
    }
}
