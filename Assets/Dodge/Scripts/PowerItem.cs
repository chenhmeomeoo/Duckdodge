using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerType
{
    Shield = 0,
    SpeedUp = 1,
    BananaTrap = 2
}
public class PowerItem : MonoBehaviour
{
    public PowerType powerType;
    public void GetItem()
    {
        Destroy(this.gameObject);
        GameManager.Instance.GetItemPower(powerType);
    }
}
