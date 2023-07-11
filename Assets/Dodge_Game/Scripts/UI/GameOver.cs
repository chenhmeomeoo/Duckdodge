using QuangDM.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
	public Button btnNothanks;
	public Button btnRevive;

    public TextMeshProUGUI txtScore;
    public TextMeshProUGUI txtBestScore;
    private void Awake()
    {
        btnNothanks.onClick.AddListener(Nothanks);
        btnRevive.onClick.AddListener(Revive);
    }

    private void Revive()
    {
        SoundManager.Instance.PlaySFX(SoundTag.SFX_Button);
        GameManager.Instance.Revive();
    }

    private void Nothanks()
    {
        SoundManager.Instance.PlaySFX(SoundTag.SFX_Button);
        GameManager.Instance.RelayGame();
    }
    public void SetScore(int Score)
    {
        txtScore.text = "Score: " + Score;
    }
    public void SetBestScore(int Score)
    {
        txtBestScore.text = "Best Score: " + Score;
    }
    private void OnEnable()
    {
        btnNothanks.gameObject.SetActive(false);
        StartCoroutine(EnableBtnNothanks());
    }
    IEnumerator EnableBtnNothanks()
    {
        yield return new WaitForSeconds(3f);
        btnNothanks.gameObject.SetActive(true);
    }
}
