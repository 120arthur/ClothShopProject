using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "ItemData")]
public class ScriptableItem : ScriptableObject
{
    public Sprite ItemIcon;
    public string ItemName;
    public int ItemPrice;
    public int Discount;
    public ItemType ItemType;
    public RuntimeAnimatorController ItemControllerAnimator;
}
