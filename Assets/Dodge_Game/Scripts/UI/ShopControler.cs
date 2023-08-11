using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopControler : MonoBehaviour
{
    public Button btn_Back,btn_Select;
    public SkinPlayer prefabsSkin;
    public Transform postion_Spawn;
    public List<SkinPlayer> listItems = new List<SkinPlayer>();
    public int idSkinTarget;
    public Transform posPreview;
    void Start()
    {
        btn_Back.onClick.AddListener(CloseShop);
        btn_Select.onClick.AddListener(Onclick_UseCharacter);
        SpawnItemSkin();
        LoadData();
    }
    private void OnEnable()
    {
    }
    void CloseShop()
    {
        UIManager.Instance.ShowBeginGame();
    }
    public void SpawnItemSkin()
    {

        if (listItems.Count== GameManager.Instance.skinPlayer.Count)
            return;
        int count = GameManager.Instance.skinPlayer.Count;
        for (int i = 0; i < count; i++)
        {
            SkinPlayer c = Instantiate(prefabsSkin, postion_Spawn); // spawn ra 6 cai skin
            c.gameObject.SetActive(true);
            c.Init(this,i);
            listItems.Add(c);
        }
    }
    public void LoadData()
    {
        for (int i = 0; i < listItems.Count; i++)
        {
            listItems[i].LoadData();
        }

    }
    public void SelectItem(int id)
    {

        for (int i = 0; i < listItems.Count; i++)
        {
            listItems[i].isSelect = false;
        }
        foreach (Transform item in posPreview)
        {
            Destroy(item.gameObject);
        }
        listItems[id].isSelect = true;
        idSkinTarget = id;
        LoadData();
    }
    public void Onclick_UseCharacter()
    {
        foreach (Transform item in PlayerController.Instance.posModelCharacter)
        {
            Destroy(item.gameObject);
        }
        Instantiate(listItems[idSkinTarget].modelCharacter, PlayerController.Instance.posModelCharacter);
    }
}
