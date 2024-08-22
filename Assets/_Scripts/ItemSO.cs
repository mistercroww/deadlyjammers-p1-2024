using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item SO")]
public class ItemSO : ScriptableObject {
    public string itemID;
    public string itemName;
    public int itemValue;
    [TextArea]
    public string itemDescription;
    public GameObject itemPrefab;
    public Vector3 inHandLocalPosition;
    public Vector3 inHandLocalRotation;
    public Vector3 inHandScale = new(1f,1f,1f);
}
