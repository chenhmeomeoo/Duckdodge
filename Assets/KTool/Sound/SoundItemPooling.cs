using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTool.Sound
{
    public class SoundItemPooling : MonoBehaviour
    {
        #region Properties

        [SerializeField]
        private SoundItem prefabItem = null;
        [SerializeField]
        [Min(0)]
        private int trashMax = 20;

        private List<SoundItem> items = null,
            trash = null;

        public bool Mute
        {
            set
            {
                prefabItem.Mute = value;
                for (int i = 0; items != null && i < items.Count; i++)
                    items[i].Mute = value;
                for (int i = 0; trash != null && i < trash.Count; i++)
                    trash[i].Mute = value;
            }
            get
            {
                return prefabItem.Mute;
            }
        }
        #endregion Properties
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public SoundItem CreateItem()
        {
            if (trash == null)
                trash = new List<SoundItem>();
            SoundItem newItem = null;
            if (trash.Count == 0)
            {
                newItem = Instantiate(prefabItem, Vector3.zero, Quaternion.identity, transform);
            }
            else
            {
                newItem = trash[0];
                trash.RemoveAt(0);
            }
            if (items == null)
                items = new List<SoundItem>();
            items.Add(newItem);
            newItem.ToDefault();
            newItem.gameObject.SetActive(true);
            return newItem;
        }
        public void DestroyItem(SoundItem item)
        {
            if (trash == null)
                trash = new List<SoundItem>();
            items.Remove(item);
            if (trash.Count < trashMax)
            {
                item.ToDefault();
                item.gameObject.SetActive(false);
                item.transform.SetParent(transform);
                trash.Add(item);
            }
            else
            {
                Destroy(item.gameObject);
            }
        }
    }
}
