using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item SO")]
public class ItemSO : ScriptableObject {
    public string itemID;
    public string itemName;
    [TextArea]
    public string itemDescription;
    public GameObject itemPrefab;
}
