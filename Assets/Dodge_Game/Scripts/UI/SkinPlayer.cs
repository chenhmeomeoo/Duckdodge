using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinPlayer : MonoBehaviour
{
    public Image imgFrame,imgSkin;
    public Sprite sprite_Select, sprite_Noselect, spriteAvatar;
    public GameObject modelCharacter,modelPreivew;
    public Button buttonItem;
    ShopControler shopControler;
    public int idSKin;
    public bool isSelect;
    public string nameSkin;
    public TMP_Text txtNameSkin;
    DataSkin skin;
    // Start is called before the first frame update
    void Start()
    {
        buttonItem.onClick.AddListener(OnclickItem);
        if (idSKin == 0)
        {
            isSelect = true;
            //Instantiate(modelCharacter, PlayerController.Instance.posModelCharacter);

        }
        else
        {
            isSelect = false;
        }
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init(ShopControler shopControler,int id)
    {
        this.idSKin = id;
        this.shopControler = shopControler;
        OnInit(id);


    }
    public void OnInit(int id)
    {
        nameSkin = GameManager.Instance.skinPlayer[id].nameCharacter;
        spriteAvatar = GameManager.Instance.skinPlayer[id].sprite_AvaModel;
        modelCharacter = GameManager.Instance.skinPlayer[id].modelCharacter;
        modelPreivew = GameManager.Instance.skinPlayer[id].modelPreview;
    }
    public void OnInitItem()
    {

    }
    public void LoadData()
    {
        if (isSelect)
        {
            imgFrame.sprite = sprite_Select;
            GameObject c = Instantiate(modelPreivew,shopControler.posPreview);
        }
        else
        {
            imgFrame.sprite = sprite_Noselect;
        }
        txtNameSkin.text = nameSkin.ToString();
        imgSkin.sprite = spriteAvatar;
    }
    public void OnclickItem()
    {
        shopControler.SelectItem(idSKin);
    }
}
