using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO_", menuName = "SO/item")]
public class ItemSO : ScriptableObject
{
    [field: SerializeField] public SlotType ItemType { get; private set; }

    [field: Space(20)]
    [field: SerializeField] public int id { get; private set; }
    [field: SerializeField] public string itemName {get; private set;}
    [field: SerializeField] public Sprite itemImage { get; private set; }
}
