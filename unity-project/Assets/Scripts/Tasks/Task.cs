using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task : MonoBehaviour {

  public int id;
  public string name_;
  public int[] itens;
  public string textToShow;
  public Photo[] photosToAdd;
  public Item[] itensToAdd;
  public bool status = false;
  public AudioClip intro_dialogue;
  public AudioClip wrong_dialogue;
  public bool dialog;
  public bool action;
  public bool puzzle;
  public Task previous;

}
