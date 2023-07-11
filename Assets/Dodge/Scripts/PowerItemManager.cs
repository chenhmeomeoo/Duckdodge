using UnityEngine;
using System.Collections;

public class PowerItemManager: MonoBehaviour
{
    public static PowerItemManager Instance;
    public float SpeedUpSpeed = 9f;
    public float SpeedUpDurationTime = 3f;


    public PowerItem[] powerItems;
    private void Awake()
    {
        Instance = this;
    }
    public void GetItemPower(PowerType type)
    {
        if(type == PowerType.SpeedUp)
        {
            PlayerController.Instance.Speed += SpeedUpSpeed;
            StartCoroutine(SpeedUpDuration(SpeedUpDurationTime));
        }
    }
    public void SpawnRandomItemPower()
    {
        int randomItemID = Random.Range(0, powerItems.Length);
        SpwanItemPower(randomItemID);
    }
    public void SpwanItemPower(int ItemIndex)
    {
        Instantiate(powerItems[ItemIndex], NewRandomVector3(), Quaternion.identity);
    }
    private Vector3 NewRandomVector3()
    {
        float x = Random.Range(-15f, 15f);
        float y = 0;
        float z = Random.Range(-15f, 15f);

        return new Vector3(x, y, z);
    }

    IEnumerator SpeedUpDuration(float time)
    {
        yield return new WaitForSeconds(time);
        PlayerController.Instance.Speed -= SpeedUpSpeed;
    }
}
