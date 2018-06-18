using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Smartphone : MonoBehaviour {

  private FirstPersonController fpsController;
  private Player player;
  private AudioSource audioSource;
  private bool isDisplayed = false;
  private bool isFirst = true;
  private ScreenTips screenTips;
  private GameController gameController;
  private bool active = false;
  public GameObject desktop;
  public AudioClip audioClip;

  // Use this for initialization
  void Start () {
    fpsController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
    gameController = GameObject.Find("GameController").GetComponent<GameController>();
    screenTips = GameObject.Find("ScreenTips").GetComponent<ScreenTips>();
    player = GameObject.Find("FPSController").GetComponent<Player>();
    desktop.SetActive(true);
    audioSource = this.GetComponent<AudioSource>();
    StartCoroutine(ShowSmartphoneTip());
  }

  private IEnumerator ShowSmartphoneTip() {
    yield return new WaitForSeconds(10);
    while (gameController.gameObject.GetComponent<AudioSource>().isPlaying) {
      yield return new WaitForSeconds(0);
    }
    active = true;
    screenTips.ShowTipUntil("Press Q to open your smartphone.", "q");
  }

  // Update is called once per frame
  void Update () {
    if (Input.GetKeyDown("q") && active && !gameController.IsOver) {
      if (isFirst) {
        StartCoroutine(player.VisualStingerSaturation());
        StartCoroutine(player.VisualStingerFocus());
        PlayStinger();
      }
      this.GetComponent<Animator>().SetBool("isDisplayed", isDisplayed = !isDisplayed);
      fpsController.m_MouseLook.SetCursorLock(!isDisplayed);
    }
  }

  public void Display() {
    this.GetComponent<Animator>().SetBool("isDisplayed", isDisplayed = !isDisplayed);
    fpsController.m_MouseLook.SetCursorLock(!isDisplayed);
  }

  public void PlayStinger() {
    isFirst = false;
    StartCoroutine(Stinger());
  }

  private IEnumerator Stinger() {
    yield return new WaitForSeconds(1f);
    audioSource.clip = audioClip;
    audioSource.Play();
  }
  
  public bool IsDisplayed{
	  get{
		  return this.isDisplayed;
	  }
  }
}
