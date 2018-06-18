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
  private ScreenTips screenTips;
  private GameObject carrying;
  private PostProcessingProfile ppp;
  private AudioSource audioSourceSteps;
  private AudioSource audioSourceAmbiente;
  private bool underlake = false;
  private bool walke_lake = false;
  private bool walke_floor = false;
  private bool inPath = true;
  private bool warning = false;
  private TerrainController terrainController;
  private int current_idx;
  private Image panelIntro;
  private GameObject lookAt;
  private GameController gameController;
  private float currentSaturation = 0;
  private float currentFocus = 1.5f;
  private bool isWaking = true;
  private bool sting = false;

  public AudioClip[] dialogs;
  public AudioClip ambiente_underlake;
  public AudioClip ambiente;
  public AudioClip[] fstp_grass;
  public AudioClip[] fstp_sand;
  public AudioClip[] fstp_dry_leaves;
  public AudioClip[] fstp_concrete;
  public AudioClip[] fstp_water;
  public AudioClip[] fstp_floor;

  private void Awake() {
    //Options.SetOptios(GameObject.Find("Options").GetComponent<Animator>());
  }

  // Use this for initialization
  void Start () {
    fpsController = this.GetComponent<FirstPersonController>();
    panelIntro = GameObject.Find("PanelIntro").GetComponent<Image>();
    inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    options = GameObject.Find("Options").GetComponent<Animator>();
    screenTips = GameObject.Find("ScreenTips").GetComponent<ScreenTips>();
    gameController = GameObject.Find("GameController").GetComponent<GameController>();
    ppp = this.GetComponentInChildren<PostProcessingBehaviour>().profile;
    audioSourceSteps = this.GetComponent<AudioSource>();
    audioSourceAmbiente = this.GetComponentsInChildren<AudioSource>()[1];
    terrainController = GameObject.Find("Terrain").GetComponent<TerrainController>();
    
	var focus = ppp.depthOfField.settings;
    focus.focusDistance = 1.5f;
    ppp.depthOfField.settings = focus;
    var saturation = ppp.colorGrading.settings;
    saturation.basic.saturation = 1f;
    ppp.colorGrading.settings = saturation;
	
	ChangeFoostepsSound(0);
    StartCoroutine(Wake());
    StartCoroutine(Path());
  }

  IEnumerator Wake() {
    MuteAudioSteps(true);
    float alpha = 1;
    panelIntro.color = new Color(panelIntro.color.r, panelIntro.color.g, panelIntro.color.b, alpha);
    var focus = ppp.depthOfField.settings;
    focus.focusDistance = 1.5f;
    ppp.depthOfField.settings = focus;
    var saturation = ppp.colorGrading.settings;
    saturation.basic.saturation = 1f;
    ppp.colorGrading.settings = saturation;
    yield return new WaitForSeconds(10f);
    MuteAudioSteps(false);
    while (alpha >= 0 || saturation.basic.saturation >= 0f) {
      saturation.basic.saturation -= 0.01f;
      ppp.colorGrading.settings = saturation;
      panelIntro.color = new Color(panelIntro.color.r, panelIntro.color.g, panelIntro.color.b, alpha -= 0.2f * Time.deltaTime);
      yield return new WaitForSeconds(0.1f);
    }
    currentFocus = 1.5f;
    currentSaturation = 0f;
    isWaking = false;
    //gameController.Completed();
    //gameController.GameOver();
    //screenTips.ShowTipUntil("Press Q to open your smartphone.", "q");
  }

  public void ChangeFoostepsSound(int idx) {
    current_idx = idx;
    if (walke_floor) {
      fpsController.FootstepsSounds = fstp_floor;
      return;
    }
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
    RayCast();
    if (Input.GetKeyDown("i") && !gameController.IsOver) {
      inventory.ShowInventory();
    }
	Falling();
  }
  private float timeinwater = 0;
  private float deathtimer_water = 10;
  private float timeinair = 0;
  private float deathtimer = 2;
  private bool die = false;
  private void Falling() {
    if (!this.GetComponent<CharacterController>().isGrounded) {
      timeinair += Time.deltaTime;
    } else {
      timeinair = 0;
    }
    if (timeinair >= deathtimer) {
      die = true;
    }
    if (die && this.GetComponent<CharacterController>().isGrounded && !gameController.IsOver) {
      gameController.GameOver();
    }
	if(underlake){
		timeinwater += Time.deltaTime;
	}else{
		timeinwater = 0;
	}
	if(timeinwater >= deathtimer_water && !gameController.IsOver){
		gameController.GameOver();
	}
  }

  private IEnumerator Path() {
    while (true) {
      if (!inPath) {
        if (UnityEngine.Random.Range(0, 100) > 70) {
          int idx = Random.Range(0, dialogs.Length);
          gameController.PlayDialogue(dialogs[idx]);
          StartCoroutine(Damage());
          yield return new WaitForSeconds(dialogs[idx].length);
        }
      }
      yield return new WaitForSeconds(10);
    }
  }

  private void RayCast() {
    int layerMask = 1 << 8;
    layerMask = ~layerMask;
    Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, 2f)) {
      lookAt = hit.collider.gameObject;
      //print("I'm looking at " + hit.collider.name);
    } else {
      lookAt = null;
    }

    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 2f)) {
      if (hit.collider.tag == "Floor") {
        walke_floor = true;
        ChangeFoostepsSound(current_idx);
      } else {
        walke_floor = false;
        ChangeFoostepsSound(current_idx);
      }
    }
  }

  public void AddItem(Item item) {
    screenTips.ShowTip("Item added to inventory.");
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

  public IEnumerator VisualStingerSaturation() {
    //var aux_1 = ppp.colorGrading.settings;
    sting = true;
    var aux_2 = ppp.colorGrading.settings;
    while (aux_2.basic.saturation <= 1f) {
      aux_2.basic.saturation += 0.1f;
      ppp.colorGrading.settings = aux_2;
      yield return new WaitForSeconds(0.1f);
    }
    while (aux_2.basic.saturation >= currentSaturation) {
      aux_2.basic.saturation -= 0.1f;
      ppp.colorGrading.settings = aux_2;
      yield return new WaitForSeconds(0.1f);
    }
    sting = false;
  }

  public IEnumerator VisualStingerFocus() {
    sting = true;
    //var aux_1 = ppp.depthOfField.settings;
    var aux_2 = ppp.depthOfField.settings;
    while (aux_2.focusDistance <= 3f) {
      aux_2.focusDistance += 0.1f;
      ppp.depthOfField.settings = aux_2;
      yield return new WaitForSeconds(0.1f);
    }
    while (aux_2.focusDistance >= currentFocus) {
      aux_2.focusDistance -= 0.1f;
      ppp.depthOfField.settings = aux_2;
      yield return new WaitForSeconds(0.1f);
    }
    sting = false;
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
    if (other.gameObject.tag == "Path") {
      inPath = false;
    }
  }

  private void OnTriggerEnter(Collider other) {
    if (other.gameObject.tag == "lake" && gameObject.transform.position.y > other.gameObject.transform.position.y) {
      walke_lake = true;
      ChangeFoostepsSound(current_idx);
    }
    if (other.gameObject.tag == "Path") {
      inPath = true;
      if(!isWaking) StartCoroutine(Heal());
    }
  }

  private void OnTriggerStay(Collider other) {
    if (other.gameObject.tag == "Path") {
      inPath = true;
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

  public GameObject LookAt {
    get {
      return this.lookAt;
    }
  }

  public bool HasItem(int id) {
    return inventory.CheckItem(id);
  }

  public void ImproveMemory() {
    StartCoroutine(Improve());
  }

  private IEnumerator Damage() {
    Debug.Log("Damage");
    float gain = 0;
    var aux_1 = ppp.depthOfField.settings;
    var aux_2 = ppp.colorGrading.settings;
    while (gain <= 0.3) {
      gain += 0.01f;
      aux_1.focusDistance -= 0.01f;
      aux_2.basic.saturation -= 0.01f;
      ppp.depthOfField.settings = aux_1;
      ppp.colorGrading.settings = aux_2.basic.saturation >= 0 ? aux_2: ppp.colorGrading.settings;
      yield return new WaitForSeconds(0.1f);
    }
    if (ppp.depthOfField.settings.focusDistance <= 0.1f && !die) gameController.GameOver();
  }

  private IEnumerator Heal() {
    if (!sting) {
      Debug.Log("Heal");
      var aux_1 = ppp.depthOfField.settings;
      var aux_2 = ppp.colorGrading.settings;
      while (ppp.depthOfField.settings.focusDistance <= currentFocus) {
        aux_1.focusDistance += 0.01f;
        ppp.depthOfField.settings = aux_1;
        yield return new WaitForSeconds(0.1f);
      }
      while (ppp.colorGrading.settings.basic.saturation <= currentSaturation) {
        aux_2.basic.saturation += 0.01f;
        ppp.colorGrading.settings = aux_2;
        yield return new WaitForSeconds(0.1f);
      }
      aux_1.focusDistance = currentFocus;
      aux_2.basic.saturation = currentSaturation;
      ppp.depthOfField.settings = aux_1;
      ppp.colorGrading.settings = aux_2;
      //currentFocus = ppp.depthOfField.settings.focusDistance;
      //currentSaturation = ppp.colorGrading.settings.basic.saturation;
    }
  }

  private IEnumerator Improve() {
    float gain = 0;
    var aux_1 = ppp.depthOfField.settings;
    var aux_2 = ppp.colorGrading.settings;
    while (gain <= 0.3) {
      gain += 0.01f;
      aux_1.focusDistance += 0.01f;
      aux_2.basic.saturation += 0.01f;
      ppp.depthOfField.settings = aux_1;
      ppp.colorGrading.settings = aux_2;
      yield return new WaitForSeconds(0.1f);
    }
    currentFocus = ppp.depthOfField.settings.focusDistance;
    currentSaturation = ppp.colorGrading.settings.basic.saturation;
  }

  public bool WalkeFloor {
    get {
      return this.walke_floor;
    }
  }

}
