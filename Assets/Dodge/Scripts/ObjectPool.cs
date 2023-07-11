using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace QuangDM.Common
{
    [System.Serializable]
    public class Preallocation
    {
        public GameObject gameObject;
        public int count;
        public bool expandable;
    }

    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool Instance;
        public List<Preallocation> preAllocations;

        [SerializeField]
        List<GameObject> pooledGobjects;

        private void Awake()
        {
            Instance = this;
            pooledGobjects = new List<GameObject>();

            foreach (Preallocation item in preAllocations)
            {
                for (int i = 0; i < item.count; ++i)
                    pooledGobjects.Add(CreateGobject(item.gameObject));
            }
        }

        public GameObject Spawn(string tag)
        {
            for (int i = 0; i < pooledGobjects.Count; ++i)
            {
                if (!pooledGobjects[i].activeSelf && pooledGobjects[i].tag == tag)
                {
                    pooledGobjects[i].SetActive(true);
                    return pooledGobjects[i];
                }
            }

            for (int i = 0; i < preAllocations.Count; ++i)
            {
                if (preAllocations[i].gameObject.tag == tag)
                    if (preAllocations[i].expandable)
                    {
                        GameObject obj = CreateGobject(preAllocations[i].gameObject);
                        pooledGobjects.Add(obj);
                        obj.SetActive(true);
                        return obj;
                    }
            }
            return null;
        }

        GameObject CreateGobject(GameObject item)
        {
            GameObject gobject = Instantiate(item, transform);
            gobject.SetActive(false);
            return gobject;
        }
    }
}