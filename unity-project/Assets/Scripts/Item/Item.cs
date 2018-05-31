using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {

  public int _id;
  public string _name;
  public Sprite image;
  public AudioClip sound;
  public bool inventoryItem;
  public bool isPickable;
  public bool useRaycast;
}
