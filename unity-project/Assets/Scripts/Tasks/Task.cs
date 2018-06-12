using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task : MonoBehaviour {

  public int id;
  public string name_;
  public int[] itens;
  public Photo[] photosToAdd;
  public Item[] itensToAdd;
  public bool status = false;
  public AudioClip intro_dialogue;
  public bool dialog;
  public bool action;
  public bool puzzle;

}
