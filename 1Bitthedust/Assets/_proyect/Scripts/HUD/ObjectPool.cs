using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    [SerializeField]
    private int initialPoolSize = 10;
    private Dictionary<GameObject, List<GameObject>> objectDicc = new Dictionary<GameObject, List<GameObject>>();

    public void Awake()
    {
        Instance = this;
    }

    public GameObject GetObject(GameObject objectRef, Transform transformRef, bool activate = true)
    {
        var result = GetInternalGO(objectRef);
        result.transform.SetPositionAndRotation(transformRef.position, transformRef.rotation);
        result.SetActive(activate);
        return result;
    }


    public GameObject GetObject(GameObject objectRef, bool activate = true)
    {
        var result = GetInternalGO(objectRef);
        result.SetActive(activate);
        return result;
    }

    private GameObject GetInternalGO(GameObject objectRef)
    {
        if (!objectRef)
        {
            Debug.LogError("You try to access a pool of a null Gameobject");
            return null;
        }

        if (!objectDicc.ContainsKey(objectRef))
            CreatePool(objectRef);

        var list = objectDicc[objectRef];
        GameObject result = null;
        //get an inactive object from the pool
        for (int i = 0; i < list.Count; i++)
        {
            if (!list[i].activeSelf)
            {
                result = list[i];
                break;
            }
        }

        //create one if there is no one available
        if (!result)
        {
            result = CreateObject(objectRef);
            list.Add(result);
        }
        return result;
    }


    public void CreatePool(GameObject objectPref)
    {
        List<GameObject> objs = new List<GameObject>();
        for (int i = 0; i < initialPoolSize; i++)
            objs.Add(CreateObject(objectPref));

        objectDicc.Add(objectPref, objs);
    }

    private GameObject CreateObject(GameObject objectPref)
    {
        var gmObj = Instantiate(objectPref);
        gmObj.transform.SetParent(transform);
        gmObj.SetActive(false);
        return gmObj;
    }

}
