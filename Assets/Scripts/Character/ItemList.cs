using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemList",menuName = "Item List")]
public class ItemList : ScriptableObject
{
    public List<BaseItem> items;
}
