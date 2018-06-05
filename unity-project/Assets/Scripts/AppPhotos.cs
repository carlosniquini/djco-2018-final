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
  public GameObject desktop;
  public Photo photo_house;

  // Use this for initialization
  void Start () {
    panel = GameObject.Find("PanelPhotos");
    photoViewer = GameObject.Find("PhotoViewer");
    smartphone = this.GetComponentInParent<Smartphone>();
    AddPhoto(photo_house);
  }

  // Update is called once per frame
  void Update () {

  }

  public void Show() {
    if (isFirst) {
      smartphone.PlayStinger();
      isFirst = false;
    }
    this.GetComponent<Animator>().SetBool("isDisplayed", isDisplayed = !isDisplayed);
    desktop.SetActive(!isDisplayed);
  }

  public void AddPhoto(Photo photo) {
    photos.Add(photo);
    GameObject gameObject = new GameObject();
    gameObject.AddComponent<Photo>().Load(photo, this);
    //gameObject.Load(photo);
    gameObject.transform.parent = panel.transform;
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
