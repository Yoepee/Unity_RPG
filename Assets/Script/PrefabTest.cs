using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTest : MonoBehaviour
{
    public GameObject prefab;

    GameObject tank;
    void Start()
    {
        // Object.Instantiate();
        // prefab = Resources.Load<GameObject>("Prefabs/Tank");
        // tank = Instantiate(prefab);

        // Destroy(tank, 3.0f);

        tank = Managers.Resource.Instantiate("Tank");

        // Managers.Resource.Destroy(tank, 3.0f);
    }
}
