using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;

public class GameController : MonoBehaviour {

  private Player player;
  private AudioSource audioSourceAmbiente;
  public AudioSource[] lakeSounds;
  public AudioClip ambiente;
  public AudioClip[] specialSounds;


  // Use this for initialization
  void Start () {
    player = GameObject.Find("FPSController").GetComponent<Player>();
    audioSourceAmbiente = GameObject.Find("FirstPersonCharacter").GetComponent<AudioSource>();
    StartCoroutine(GamePlaySounds());
  }

  IEnumerator GamePlaySounds() {
    while (true) {
      yield return new WaitForSeconds(180f);
      if (UnityEngine.Random.Range(1, 100) > 50) {
        int idx = Random.Range(0, specialSounds.Length);
        AudioClip clip = specialSounds[idx];
        audioSourceAmbiente.clip = clip;
        audioSourceAmbiente.Play();
        yield return new WaitForSeconds(idx == 0 ? 3 * clip.length : clip.length);
        audioSourceAmbiente.clip = ambiente;
        audioSourceAmbiente.Play();
      }
    }
  }

  void SwitchLakeSounds() {
    foreach (AudioSource a in lakeSounds) {
      foreach (AudioSource b in lakeSounds) {
        if (a != b && Vector3.Distance(a.gameObject.transform.position, player.transform.position) < Vector3.Distance(b.gameObject.transform.position, player.transform.position)) {
          a.mute = false;
          b.mute = true;
        } else {
          a.mute = true;
          b.mute = false;
        }
      }
    }
  }

  // Update is called once per frame
  void Update () {
    SwitchLakeSounds();
  }

  public void PlayDialogue(AudioClip a) {
    this.GetComponent<AudioSource>().clip = a;
    this.GetComponent<AudioSource>().Play();
  }
}
