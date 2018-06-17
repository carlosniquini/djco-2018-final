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

  void Start () {
    player = GameObject.Find("FPSController").GetComponent<Player>();
    playerCam = GameObject.Find("FirstPersonCharacter").GetComponent<Transform>();
    options = GameObject.Find("Options").GetComponent<Options>();
  }

  void Update () {
    IsNear();
    if (inventoryItem && hasPlayer) {
      if (Input.GetKeyDown("e")) {
        player.AddItem(this);
        player.GetComponent<AudioSource>().clip = sound;
        player.GetComponent<AudioSource>().Play();
        this.gameObject.SetActive(false);
        options.HideOptions(this.gameObject);
      }
    }
    if (beingCarried) {
      if (Input.GetKeyDown("e")) {
        player.GetComponent<AudioSource>().clip = sound;
        player.GetComponent<AudioSource>().Play();
        GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
        beingCarried = false;
        GetComponent<Rigidbody>().AddForce(playerCam.forward * 10);
        player.Carrying = null;
      }
      return;
    }
    if (isPickable && hasPlayer) {
      if (Input.GetKeyDown("e")) {
        player.GetComponent<AudioSource>().clip = sound;
        player.GetComponent<AudioSource>().Play();
        GetComponent<Rigidbody>().isKinematic = true;
        transform.parent = playerCam;
        beingCarried = true;
        player.Carrying = this.gameObject;
        return;
      }
    }

  }

  private void IsNear() {
    float dist = Vector3.Distance(gameObject.transform.position, player.GetComponent<Transform>().position);
    if ((dist <= 2.5f && player.LookAt != null && player.LookAt.name == this.name) || beingCarried) {
      hasPlayer = true;
      options.SetTarget(this);
      options.ShowOptions();
    } else {
      hasPlayer = false;
      options.HideOptions(this.gameObject);
    }
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
}
