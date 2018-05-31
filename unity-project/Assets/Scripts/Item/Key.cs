using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : ItemDefault {

  /*private void OnBecameVisible() {
    Debug.Log("To te vendo.");
  }*/
  private Player player;
  private Transform playerCam;
  private Options options;
  private bool hasPlayer = false;
  private bool beingCarried = false;

  // Use this for initialization
  void Start() {
    player = GameObject.Find("FPSController").GetComponent<Player>();
    playerCam = GameObject.Find("FirstPersonCharacter").GetComponent<Transform>();
    options = GameObject.Find("Options").GetComponent<Options>();
  }

  // Update is called once per frame
  void Update() {
    if (player.Carrying != null && player.Carrying != this.gameObject && !IsVisible()) return;
    IsNear();
    if (inventoryItem && hasPlayer) {
      if (Input.GetMouseButton(1)) {
        player.AddItem(this);
        player.GetComponent<AudioSource>().clip = sound;
        player.GetComponent<AudioSource>().Play();
        this.gameObject.SetActive(false);
        //player.ShowOptions(false);
        //ShowAnimation(false);
        options.HideOptions(this.gameObject);
      }
    }
    if (isPickable && hasPlayer && !IsVisible()) {
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

  private bool IsVisible() {
    int layerMask = 1 << 8;
    layerMask = ~layerMask;
    bool isVisible;
    RaycastHit hit;
    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1f, layerMask))
      /*if (!Physics.Raycast(transform.position, Vector3.up))*/
    {
      //bug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
      isVisible = true;
    } else {
      Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
      isVisible = false;
    }
    //bug.Log(this._name + " " + isVisible);
    return isVisible;
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
}
