using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [field: SerializeField] private GameObject prefab { get; set; }
    [field: SerializeField] public GameObject selectObejct { get; set; }

    public void Start()
    {
        InventoryView.instance.InitInventorys();
    }
    public void CreateObejct(int id)
    {
        var tempObject = Instantiate(prefab);
        tempObject.GetComponent<ObjectItem>().itemID = id;

        float randX = Random.Range(-5,5);
        float randZ = Random.Range(-5, 5);

        tempObject.transform.position = new Vector3(randX,1,randZ);
    }

    public void DeleteObject()
    {
        if (selectObejct == null) return;

        Destroy(selectObejct);
        selectObejct = null;
    }
}
