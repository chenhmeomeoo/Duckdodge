using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public BeginGame beginGame;
    public InGame inGame;
    public GameOver gameOver;
    public ShopControler ShopControl;
    private void Awake()
    {
        Instance = this;
    }
    private void CloseAllPanel()
    {
        beginGame.gameObject.SetActive(false);
        inGame.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);
        ShopControl.gameObject.SetActive(false);
    }
    public void ShowBeginGame()
    {
        CloseAllPanel();
        beginGame.gameObject.SetActive(true);
    }
    public void ShowInGame()
    {
        CloseAllPanel();
        inGame.gameObject.SetActive(true);
    }
    public void ShowGameOver()
    {
        CloseAllPanel();
        gameOver.gameObject.SetActive(true);
    }
    public void ShowShop()
    {
        CloseAllPanel();
        ShopControl.gameObject.SetActive(true);
    }
}
