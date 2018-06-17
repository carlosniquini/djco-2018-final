using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class GameController : MonoBehaviour {

  private Player player;
  private FirstPersonController fpsController;
  private Image panelIntro;
  private AudioSource audioSourceAmbiente;
  private bool isOver = false;
  public BITalinoReader reader;
  public AudioSource[] lakeSounds;
  public AudioClip ambiente;
  public AudioClip[] specialSounds;

  private Button power, restart;
  private Toggle bitalino, mute;
  private bool usingBitalino = false;


  // Use this for initialization
  void Start () {
    player = GameObject.Find("FPSController").GetComponent<Player>();
    fpsController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
    panelIntro = GameObject.Find("PanelIntro").GetComponent<Image>();
    audioSourceAmbiente = GameObject.Find("FirstPersonCharacter").GetComponent<AudioSource>();
    //panelIntro.GetComponentInChildren<Text>().gameObject.SetActive(false);
    power = GameObject.Find("ExitBtn").GetComponent<Button>();
    restart = GameObject.Find("RestartBtn").GetComponent<Button>();
    mute = GameObject.Find("MuteToggle").GetComponent<Toggle>();
    bitalino = GameObject.Find("BitalinoToggle").GetComponent<Toggle>();

    mute.onValueChanged.AddListener(delegate {
      foreach (AudioSource a in GameObject.FindObjectsOfType<AudioSource>()) {
        a.mute = mute.isOn;
      }
    });

    bitalino.onValueChanged.AddListener(delegate {
      usingBitalino = bitalino.isOn;
    });

    power.onClick.AddListener(delegate {
      SceneManager.LoadScene("menu");
    });

    restart.onClick.AddListener(delegate {
      SceneManager.LoadScene("main");
    });

    StartCoroutine(GamePlaySounds());
    StartCoroutine(Wind());
  }

  IEnumerator GamePlaySounds() {
    while (true) {
      yield return new WaitForSeconds(180f);
      if (UnityEngine.Random.Range(0, 100) > 50) {
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
    AudioSource source = this.GetComponents<AudioSource>()[0];
    if (!source.isPlaying) {
      source.clip = a;
      source.Play();
    }
  }

  private IEnumerator Wind() {
    AudioSource source = this.GetComponents<AudioSource>()[1];
    while (true) {
      yield return new WaitForSeconds(60f);
      if (!player.UnderLake && !player.WalkeFloor && UnityEngine.Random.Range(0, 100) > 60) {
        if (!source.isPlaying) {
          source.Play();
        }
      }
    }
  }

  public void GameOver() {
    StartCoroutine(OverCoroutine("You've been forgotten...", false));
  }

  public void Completed() {
    StartCoroutine(OverCoroutine("to be continued...", true));
  }

  private IEnumerator OverCoroutine(string str, bool s) {
    Destroy(fpsController);
    isOver = true;
    float alpha = 0;
    panelIntro.color = new Color(panelIntro.color.r, panelIntro.color.g, panelIntro.color.b, alpha);
    while (alpha <= 1) {
      panelIntro.color = new Color(panelIntro.color.r, panelIntro.color.g, panelIntro.color.b, alpha += 0.2f * Time.deltaTime);
      yield return new WaitForSeconds(0.1f);
    }
    if(s)
      panelIntro.GetComponentInChildren<AudioSource>().Play();
    panelIntro.GetComponentInChildren<Text>().gameObject.SetActive(true);
    Text text = panelIntro.GetComponentInChildren<Text>();
    text.text = str;
    alpha = 0;
    text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
    while (alpha <= 1) {
      text.color = new Color(text.color.r, text.color.g, text.color.b, alpha += 0.2f * Time.deltaTime);
      yield return new WaitForSeconds(0.1f);
    }
    yield return new WaitForSeconds(1f);
    while (alpha >= 0) {
      text.color = new Color(text.color.r, text.color.g, text.color.b, alpha -= 0.2f * Time.deltaTime);
      yield return new WaitForSeconds(0.1f);
    }
    if(s)
      SceneManager.LoadScene("end");
    else
      SceneManager.LoadScene("menu");
  }

  public bool IsOver {
    get
    {
      return this.isOver;
    }
  }

  public bool UsingBitalino {
    get {
      return this.usingBitalino;
    }
  }

}
