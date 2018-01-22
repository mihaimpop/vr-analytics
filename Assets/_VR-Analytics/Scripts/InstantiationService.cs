using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Schema;
using Leap;
using Scripts.GameObjects;
using SimpleJSON;
using UnityEngine;
using VRAnalytics;

namespace Scripts{
  public class InstantiationService : MonoBehaviour {

    float _offset = 0.0004F;
    public List<ResourceNode> resourceNodes;
    
    public bool AreNodesCreated = false;
    public bool IsDataReceived = false;
    public int DataTotal = 0;
    public int ElementsOnX = 0, ElementsOnY = 0, ElementsOnZ = 0;
    public Vector3 V3Center = Vector3.zero;
    public List<Vector3> CubePositions;

    public GameObject MountNode;
    public float PaddingFactor;

    public string BaseUrl = "http://localhost:3000/api/data";
    public string RequestUrl = "http://localhost:3000/api/data";
    private string _requestBuffer;

    public Vector3 StartingPosition = new Vector3(.1f, .1f, .1f);
    string _dataResponse = null;
    
    Vector3 _currentPosition;
    Vector3 _error = new Vector3(-Mathf.Infinity, -Mathf.Infinity, -Mathf.Infinity);
    Vector3 xOffset = new Vector3(.6f, 0, 0);
    Vector3 yOffset = new Vector3(0, .6f, 0);
    Vector3 zOffset = new Vector3(0, 0, .6f);

    public void SetRequestUrl(string url) {
      RequestUrl = url;
    }

    bool IsPerfectCube(int n) {
      int root = Mathf.RoundToInt(Mathf.Pow(n, 1f / 3f));
      return n == root * root * root;
    }
    
    public void GenerateCubePositions(int total){
      float elementsComputation = Mathf.Pow(total, 1f / 3f);
      int cubeEdge;
      bool isCube = IsPerfectCube(total);
      if (isCube){
        cubeEdge = Mathf.RoundToInt(elementsComputation);
      } else{
        cubeEdge = Mathf.RoundToInt(elementsComputation + .5F);
      }
      for (var i = 0; i < cubeEdge; i++) {
        for (var j = 0; j < cubeEdge; j++) {
          for (var k = 0; k < cubeEdge; k++) {
            var x = V3Center.x - cubeEdge / 2.0 + i;
            var y = V3Center.y + cubeEdge / 2.0 - j;
            var z = V3Center.z - cubeEdge / 2.0 + k;

            Vector3 cubePosition = new Vector3((float) x / PaddingFactor, (float) y / PaddingFactor,(float) z / PaddingFactor);
            Vector3 mountNodePosition = MountNode.transform.position;
            CubePositions.Add(mountNodePosition + cubePosition);
          }
        }
      }
    }
        
    void CreateNodes(List<ResourceNode> results, int total) {
      for (int i = 0; i < total; i++) {
        string nodeName = "node-" + results[i].name;
        float influence = results[i].influence;
        Color cubeColor;
        ColorUtility.TryParseHtmlString ("#"+results[i].color, out cubeColor);

        float scale = results[i].influence / 1000F;
        GenerateCubePositions(total);
        Cube dataCube = new Cube(nodeName, influence, cubeColor, scale, CubePositions[i]);
      
        dataCube.Render();
      }
      AreNodesCreated = true;
    }

    void BuildData(string data) {
      var results = JSON.Parse(data).AsArray;
      int total = results.Count;
      
      resourceNodes = new List<ResourceNode>();
      
      DataTotal = total;
      for (int i = 0; i <= total; i++) {
        resourceNodes.Add(
          new ResourceNode(
            results[i]["influence"].AsFloat,
            results[i]["name"].Value,
            results[i]["color"].Value)
        );
      }
      CreateNodes(resourceNodes, total);
    }

    IEnumerator WaitForRequest(WWW www) {
      yield return www;

      if (www.error == null) {
        _dataResponse = www.text;

        BuildData(www.text);
        IsDataReceived = true;
        Debug.Log("WWW Ok!: " + www.text);
      }
      else {
        IsDataReceived = false;
        Debug.Log("WWW Error: " + www.error);
      }
    }
    
    public void GetData(){
      WWW www = new WWW(RequestUrl);
      StartCoroutine(WaitForRequest(www));
    }

    void Start() {
      GetData();
    }

    void DestroyCubes(){
      foreach (Transform child in MountNode.transform){
        Destroy(child.gameObject);
      }
    }
    
    void Update() {
      if (RequestUrl != _requestBuffer){
        _requestBuffer = RequestUrl;
        DestroyCubes();
        GetData();
      }
    }
  }
}
