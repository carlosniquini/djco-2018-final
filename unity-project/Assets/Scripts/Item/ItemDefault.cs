﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDefault : Item {

  /*
  public int _id;
  public int _name;
  public Sprite image;
  public AudioClip sound;
  public bool inventoryItem;
  public bool isPickable;
  */

  private Player player;
  private Transform playerCam;
  private Options options;
  private bool hasPlayer = false;
  private bool beingCarried = false;

  // Use this for initialization
  void Start () {
    player = GameObject.Find("FPSController").GetComponent<Player>();
    playerCam = GameObject.Find("FirstPersonCharacter").GetComponent<Transform>();
    options = GameObject.Find("Options").GetComponent<Options>();
  }

  // Update is called once per frame
  void Update () {
    if (player.Carrying != null && player.Carrying != this.gameObject) return;
    IsNear();
    if (inventoryItem && hasPlayer) {
      if (Input.GetMouseButtonDown(1)) {
        player.AddItem(this);
        player.GetComponent<AudioSource>().clip = sound;
        player.GetComponent<AudioSource>().Play();
        this.gameObject.SetActive(false);
        //player.ShowOptions(false);
        //ShowAnimation(false);
        options.HideOptions(this.gameObject);
      }
    }
    if (isPickable && hasPlayer) {
      if (Input.GetMouseButtonDown(0)) {
        GetComponent<Rigidbody>().isKinematic = true;
        transform.parent = playerCam;
        beingCarried = true;
        player.Carrying = this.gameObject;
      }
      if (beingCarried) {
        if (Input.GetMouseButtonDown(2)) {
          GetComponent<Rigidbody>().isKinematic = false;
          transform.parent = null;
          beingCarried = false;
          GetComponent<Rigidbody>().AddForce(playerCam.forward * 10);
          player.Carrying = null;
        }
      }
    }

  }

  private void IsNear() {
    float dist = Vector3.Distance(gameObject.transform.position, player.GetComponent<Transform>().position);
    if (dist <= 2.5f) {
      hasPlayer = true;
      options.SetTarget(this.GetComponent<Transform>());
      options.ShowOptions();
      //player.ShowOptions(true);
    } else {
      hasPlayer = false;
      //if (isEnable) {
      options.HideOptions(this.gameObject);
      //isEnable = false;
      //}
    }
    //player.ShowOptions(false);
  }

  /*
  private void ShowAnimation(bool s) {
  this.GetComponent<Animator>().SetBool("isDisplayed", s);
  }*/
}