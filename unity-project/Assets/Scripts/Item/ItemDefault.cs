using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDefault : Item {

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
    //if (player.Carrying != null && player.Carrying != this.gameObject) return;
    //Debug.Log(hasPlayer);
    IsNear();
    if (inventoryItem && hasPlayer) {
      ///if (Input.GetMouseButton(1)) {
      if (Input.GetKeyDown("e")) {
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
      //if (Input.GetMouseButtonDown(0)) {
      if (Input.GetKeyDown("e")) {
        GetComponent<Rigidbody>().isKinematic = true;
        transform.parent = playerCam;
        beingCarried = true;
        player.Carrying = this.gameObject;
        return;
      }
      if (beingCarried) {
        //Debug.Log("AQUI ó");
        //if (Input.GetMouseButtonDown(2)) {
        if (Input.GetKeyUp("e")) {
          //Debug.Log("AQUI ó");
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
    //Debug.Log(this.name);
    if (dist <= 2.5f && player.LookAt != null && player.LookAt.name == this.name) {
      hasPlayer = true;
      //options.SetTarget(this.GetComponent<Transform>());
      options.SetTarget(this);
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

  public Player Player {
    get {
      return this.player;
    }
    set {
      this.player = value;
    }
  }

  public Transform PlayerCam {
    get {
      return this.playerCam;
    }
    set {
      this.playerCam = value;
    }
  }

  public bool HasPlayer {
    set {
      this.hasPlayer = value;
    }
    get {
      return this.hasPlayer;
    }
  }

  public bool BeingCarried {
    set
    {
      this.beingCarried = value;
    }
    get
    {
      return this.beingCarried;
    }
  }

  /*
  private void ShowAnimation(bool s) {
  this.GetComponent<Animator>().SetBool("isDisplayed", s);
  }*/
}
