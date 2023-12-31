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
    public Button startGame,shop;
    public TextMeshProUGUI txtGameName;
    private void Awake()
    {
    }
    public void Start()
    {
        startGame.onClick.AddListener(StartGame);
        shop.onClick.AddListener(ShowShop);
    }
    private void StartGame()
    {
        PlayerController.Instance.SetAnimation();
        SoundManager.Instance.PlaySFX(SoundTag.SFX_Button);
        GameManager.Instance.StartGame();
    }
    void ShowShop()
    {
        UIManager.Instance.ShowShop();
    }
}
