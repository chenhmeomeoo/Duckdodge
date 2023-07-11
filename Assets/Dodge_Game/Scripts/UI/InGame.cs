using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class InGame : MonoBehaviour
{
    public TextMeshProUGUI txtScore;
    public TextMeshProUGUI txtEnemiesRemain;
    public void SetScore(int Score)
    {
        txtScore.text = "Score: " + Score.ToString();
    }
    public void SetEnemiesRemain(int number)
    {
        txtEnemiesRemain.text = "Enemies remain: " + number.ToString();
    }
}
