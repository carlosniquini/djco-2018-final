using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour {

  private FirstPersonController fpsController;
  private Inventory inventory;
  private Animator options;
  private GameObject carrying;
  private PostProcessingProfile ppp;
  private AudioSource audioSourceSteps;
  private AudioSource audioSourceAmbiente;
  private bool underlake = false;
  private bool walke_lake = false;
  private TaskDefault task_1;
  private TerrainController terrainController;
  private int current_idx;
  private Image panelIntro;
  public AudioClip ambiente_underlake;
  public AudioClip ambiente;

  public AudioClip[] fstp_grass;
  public AudioClip[] fstp_sand;
  public AudioClip[] fstp_dry_leaves;
  public AudioClip[] fstp_concrete;
  public AudioClip[] fstp_water;

  private void Awake() {
    //Options.SetOptios(GameObject.Find("Options").GetComponent<Animator>());
  }

  // Use this for initialization
  void Start () {
    fpsController = this.GetComponent<FirstPersonController>();
    panelIntro = GameObject.Find("PanelIntro").GetComponent<Image>();
    inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    options = GameObject.Find("Options").GetComponent<Animator>();
    ppp = this.GetComponentInChildren<PostProcessingBehaviour>().profile;
    audioSourceSteps = this.GetComponent<AudioSource>();
    audioSourceAmbiente = this.GetComponentsInChildren<AudioSource>()[1];
    task_1 = GameObject.Find("Task1").GetComponent<TaskDefault>();
    terrainController = GameObject.Find("Terrain").GetComponent<TerrainController>();
    ChangeFoostepsSound(0);
    StartCoroutine(Wake());
    //TerrainController.changeEvent += ChangeFoostepsSound;
    //var aux = ppp.depthOfField.settings;
    //aux.focusDistance = 5f;
    //ppp.depthOfField.settings = aux;
  }

  IEnumerator Wake() {
    MuteAudioSteps(true);
    float alpha = 1;
    panelIntro.color = new Color(panelIntro.color.r, panelIntro.color.g, panelIntro.color.b, alpha);
    var aux = ppp.vignette.settings;
    aux.intensity = 1f;
    ppp.vignette.settings = aux;
    var aux_2 = ppp.colorGrading.settings;
    aux_2.basic.saturation = 1f;
    ppp.colorGrading.settings = aux_2;
    yield return new WaitForSeconds(10f);
    MuteAudioSteps(false);
    while (aux.intensity >= 0) {
      aux.intensity -= 0.01f;
      ppp.vignette.settings = aux;
      aux_2.basic.saturation -= 0.01f;
      ppp.colorGrading.settings = aux_2;
      panelIntro.color = new Color(panelIntro.color.r, panelIntro.color.g, panelIntro.color.b, alpha -= 0.2f * Time.deltaTime);
      yield return new WaitForSeconds(0.1f);
    }
  }

  public void ChangeFoostepsSound(int idx) {
    //Debug.Log("Texture_id: " + idx);
    current_idx = idx;
    if (walke_lake) {
      fpsController.FootstepsSounds = fstp_water;
      return;
    }
    if (idx == 0) {
      fpsController.FootstepsSounds = fstp_grass;
    }
    if (idx == 1) {
      fpsController.FootstepsSounds = fstp_sand;
    }
    if (idx == 2) {
      fpsController.FootstepsSounds = fstp_dry_leaves;
    }
    if (idx == 3) {
      fpsController.FootstepsSounds = fstp_concrete;
    }
  }

  // Update is called once per frame
  void Update () {

    //terrainController.GetMainTexture(this.transform.position);
    UpdateAmbienteSound();
    if (Input.GetKeyDown("i")) {
      inventory.ShowInventory();
    }
    var aux = ppp.depthOfField.settings;
    aux.focusDistance = task_1.Dist();
    ppp.depthOfField.settings = aux;
  }

  public void AddItem(Item item) {
    inventory.AddItem(item);
  }

  public void ShowOptions(bool s) {
    options.SetBool("isDisplayed", s);
  }

  public GameObject Carrying {
    get { return carrying; }
    set { this.carrying = value; }
  }

  public bool UnderLake {
    get { return underlake; }
    private set { this.underlake = value; }
  }

  private void OnTriggerExit(Collider other) {
    if (other.gameObject.tag == "lake" && gameObject.transform.position.y < other.gameObject.transform.position.y) {
      audioSourceAmbiente.clip = ambiente_underlake;
      audioSourceSteps.mute = true;
      audioSourceAmbiente.Play();
      UnderLake = true;
    }
    if (other.gameObject.tag == "lake" && gameObject.transform.position.y >= other.gameObject.transform.position.y) {
      audioSourceAmbiente.clip = ambiente;
      audioSourceSteps.mute = false;
      audioSourceAmbiente.Play();
      UnderLake = false;
      walke_lake = false;
      ChangeFoostepsSound(current_idx);
    }
  }

  private void OnTriggerEnter(Collider other) {
    if (other.gameObject.tag == "lake" && gameObject.transform.position.y > other.gameObject.transform.position.y) {
      walke_lake = true;
      ChangeFoostepsSound(current_idx);
    }
  }

  void UpdateAmbienteSound() {
    if (UnderLake && gameObject.transform.position.y >= GameObject.Find("WaterBasicDaytime").transform.position.y) {
      audioSourceAmbiente.clip = ambiente;
      UnderLake = false;
    }
  }

  public void MuteAudioSteps(bool s) {
    this.GetComponent<AudioSource>().mute = s;
  }
}
