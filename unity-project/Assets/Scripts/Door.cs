using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Door : MonoBehaviour {

  private Player player;
  private GameController gameController;
  private AudioSource audioSource;
  private bool isOpen = false;
  public AudioClip[] audioClip;
  public int key_id;
  public AudioClip dialog;

  private void Awake() {

  }

  // Use this for initialization
  void Start () {
    player = GameObject.Find("FPSController").GetComponent<Player>();
    gameController = GameObject.Find("GameController").GetComponent<GameController>();
    audioSource = this.GetComponent<AudioSource>();
  }

  // Update is called once per frame
  void Update () {
    if (player.LookAt != null && player.LookAt.name == "Door_front") {
      if (Input.GetKeyDown("e")) {
        OpenDoor();
      }
    }
  }

  void OpenDoor() {
    if (player.HasItem(key_id)) {
      this.GetComponent<Animator>().SetBool("isOpen", isOpen = !isOpen);
      audioSource.clip = isOpen ? audioClip[0] : audioClip[1];
      audioSource.Play();
    } else {
      gameController.PlayDialogue(dialog);
    }
  }
}
