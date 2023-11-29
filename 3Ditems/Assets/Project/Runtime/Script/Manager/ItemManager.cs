using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;   

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    [field: SerializeField] public List<ItemSO> items { get; private set; }

    public void Awake()
    {
        instance = this;
    }
}
