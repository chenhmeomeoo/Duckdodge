using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace QuangDM.Common
{
    public class HealthBar : MonoBehaviour
    {
        public Transform HealthBarPos;
        public Image healthBarImage;
        void Update()
        {
            EnemyController enemy = GetComponentInParent<EnemyController>();
            float healthRatio = (float)(enemy.lifeDuration - enemy.timeLifeDuration) / enemy.lifeDuration;
            healthBarImage.rectTransform.localScale = new Vector3(healthRatio, 1, 1);
            transform.position = Camera.main.WorldToScreenPoint(HealthBarPos.position) + new Vector3(0f, 1f);
        }
        private void OnEnable()
        {
            healthBarImage.enabled = false;
            StartCoroutine(EnableHealthbar());
        }
        private IEnumerator EnableHealthbar()
        {
            yield return new WaitForSeconds(0.2f);
            healthBarImage.enabled = true;
        }
    }
    
}