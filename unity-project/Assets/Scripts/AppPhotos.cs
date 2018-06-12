using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppPhotos : MonoBehaviour {

  private bool isDisplayed = false;
  private bool isDisplayedViewer = false;
  private bool isFirst = true;
  private Smartphone smartphone;
  private ArrayList photos = new ArrayList();
  private GameObject panel;
  private GameObject photoViewer;
  private GridLayoutGroup grid;
  public GameObject desktop;
  public Photo photo_house;
  public Photo photo_playground;

  private ArrayList photosList = new ArrayList();

  // Use this for initialization
  void Start () {
    panel = GameObject.Find("PanelPhotos");
    photoViewer = GameObject.Find("PhotoViewer");
    grid = GameObject.Find("PanelPhotos").GetComponent<GridLayoutGroup>();
    smartphone = this.GetComponentInParent<Smartphone>();
    AddPhotoInList(photo_house);
    //AddPhoto(photo_house);
    //AddPhoto(photo_playground);
  }

  // Update is called once per frame
  void Update () {
    grid.enabled = false;
    grid.enabled = true;
  }

  public void Show() {
    if (isFirst) {
      smartphone.PlayStinger();
      isFirst = false;
    }
    AddPhoto();
    this.GetComponent<Animator>().SetBool("isDisplayed", isDisplayed = !isDisplayed);
    desktop.SetActive(!isDisplayed);
  }

  public void AddPhotoInList(Photo photo) {
    photosList.Add(photo);
  }

  public void AddPhoto() {
    Debug.Log("Add foto");
    foreach (Photo p in GameObject.Find("PanelPhotos").GetComponentsInChildren<Photo>()) {
      //DestroyImmediate(p);
      Destroy(p.gameObject);
    }
    foreach(Photo photo in photosList) {
      //photos.Add(photo);
      GameObject gameObject = new GameObject(photo._name);
      gameObject.AddComponent<Photo>().Load(photo, this);
      //gameObject.Load(photo);
      gameObject.transform.parent = panel.transform;
      gameObject.transform.localScale = new Vector3(1, 1, 1);
      //LayoutRebuilder.ForceRebuildLayoutImmediate(grid.gameObject.GetComponent<RectTransform>());
      //Canvas.ForceUpdateCanvases();
    }
  }

  public void ShowImage(Sprite img) {
    photoViewer.GetComponent<Image>().sprite = img;
    photoViewer.GetComponent<Animator>().SetBool("isDisplayed", isDisplayedViewer = !isDisplayedViewer);
  }

  public void HideImage() {
    photoViewer.GetComponent<Animator>().SetBool("isDisplayed", isDisplayedViewer = !isDisplayedViewer);
  }

  public GameObject ImageViewer {
    get {
      return this.ImageViewer;
    }
  }
}
