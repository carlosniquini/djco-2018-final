using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
  private TaskDefault task_1;
  private TerrainController terrainController;

  public AudioClip lake;
  public AudioClip ambiente;

  public AudioClip[] fstp_grass;
  public AudioClip[] fstp_sand;
  public AudioClip[] fstp_dry_leaves;
  public AudioClip[] fstp_concrete;

  private void Awake() {
    //Options.SetOptios(GameObject.Find("Options").GetComponent<Animator>());
  }

  // Use this for initialization
  void Start () {
    fpsController = this.GetComponent<FirstPersonController>();
    inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    options = GameObject.Find("Options").GetComponent<Animator>();
    ppp = this.GetComponentInChildren<PostProcessingBehaviour>().profile;
    audioSourceSteps = this.GetComponent<AudioSource>();
    audioSourceAmbiente = this.GetComponentsInChildren<AudioSource>()[1];
    task_1 = GameObject.Find("Task1").GetComponent<TaskDefault>();
    terrainController = GameObject.Find("Terrain").GetComponent<TerrainController>();
    ChangeFoostepsSound(0);
    //TerrainController.changeEvent += ChangeFoostepsSound;
    //var aux = ppp.depthOfField.settings;
    //aux.focusDistance = 5f;
    //ppp.depthOfField.settings = aux;
  }

  public void ChangeFoostepsSound(int idx) {
    Debug.Log("Texture_id: " + idx);
    if (idx == 0)
      fpsController.FootstepsSounds = fstp_grass;
    if (idx == 1)
      fpsController.FootstepsSounds = fstp_sand;
    if (idx == 2)
      fpsController.FootstepsSounds = fstp_dry_leaves;
    if (idx == 3)
      fpsController.FootstepsSounds = fstp_concrete;
  }

  // Update is called once per frame
  void Update () {

    //terrainController.GetMainTexture(this.transform.position);

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
      audioSourceAmbiente.clip = lake;
      audioSourceSteps.mute = true;
      audioSourceAmbiente.Play();
      UnderLake = true;
    }
    if (other.gameObject.tag == "lake" && gameObject.transform.position.y > other.gameObject.transform.position.y) {
      audioSourceAmbiente.clip = ambiente;
      audioSourceSteps.mute = false;
      audioSourceAmbiente.Play();
      UnderLake = false;
    }
  }
}
