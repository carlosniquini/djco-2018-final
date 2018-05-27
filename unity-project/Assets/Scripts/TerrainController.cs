using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour {

  private Terrain mainterrain;
  private TerrainData terrainData;
  private Player player;
  private int terrainIdx;
  private Vector3 pos;

  //public delegate void ChangeEvent(int idx);
  //public static event ChangeEvent changeEvent;

  // Use this for initialization
  void Start () {
    mainterrain = Terrain.activeTerrain;
    terrainData = mainterrain.terrainData;
    pos = mainterrain.transform.position;
    player = GameObject.Find("FPSController").GetComponent<Player>();
    //player.ChangeFoostepsSound(0);

  }

  // Update is called once per frame
  void Update () {
    GetMainTexture(player.transform.position);
  }

  private float[] GetTextureMix(Vector3 pos_) {
    int mapX = (int) (((pos_.x - pos.x) / terrainData.size.x) * terrainData.alphamapWidth);
    int mapZ = (int) (((pos_.z - pos.z) / terrainData.size.z) * terrainData.alphamapHeight);
    float[,,] splatmapData = terrainData.GetAlphamaps(mapX, mapZ, 1, 1);

    float[] cellMix = new float[splatmapData.GetUpperBound(2) + 1];

    for (int n = 0; n < cellMix.Length; n++ ) {
      cellMix[n] = splatmapData[0, 0, n];
    }
    return cellMix;
  }

  private void GetMainTexture(Vector3 pos_) {
    float[] mix  = GetTextureMix(pos_);

    float maxMix = 0;
    int maxIndex = 0;

    for (int n = 0; n < mix.Length; n++ ) {
      if (mix[n] > maxMix) {
        maxIndex = n;
        maxMix = mix[n];
      }
    }
    if (maxIndex != terrainIdx) {
      terrainIdx = maxIndex;
      player.ChangeFoostepsSound(terrainIdx);
    }
    /*if (changeEvent != null)
      changeEvent(terrainIdx);*/
    //return maxIndex;
  }
}
