using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items", order = 0)]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public Vector2 collBounds;
    public Vector2 aimCollBounds;
    public ItemScene[] scene;
}
