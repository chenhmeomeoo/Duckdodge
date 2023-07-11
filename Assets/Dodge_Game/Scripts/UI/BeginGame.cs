using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using QuangDM.Common;

public class BeginGame : MonoBehaviour
{
    public Button startGame;
    public TextMeshProUGUI txtGameName;
    private void Awake()
    {
    }
    public void Start()
    {
        startGame.onClick.AddListener(StartGame);
    }
    private void StartGame()
    {
        SoundManager.Instance.PlaySFX(SoundTag.SFX_Button);
        GameManager.Instance.StartGame();
    }
}
