using System;
using Scripts.DataMapper;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts{
  public class Crosshair : MonoBehaviour {
    bool _showText = false;
    Color _color;
    float _influence;
    float range = Mathf.Infinity;
    int _i = 0;
    
    public Color HitColor = new Color(255F, 255F, 0);
    public Color OldColor;
    
    public GameObject GesturesContainer;
    public GameObject LastCube;
    public GameObject CurrentCube;
    public GameObject MountNode;
    public GameObject InfoCanvas;
    public GameObject InfoCanvasContainer;
    public GameObject RtsNode;
    public GesturesService Gestures;
    
    String _name;
    Text _influenceValue;
    Text _nameValue;
    Vector3 infoCanvasYOffset = new Vector3(0, 0.1F, 0);
    Vector3 infoCanvasZOffset = new Vector3(0, 0, -0.2F);

    void BuildInfoCanvas(string name, float influence, RaycastHit hit) {
      if (InfoCanvas) {
        return;
      }
      else {
        InfoCanvas = Instantiate(Resources.Load("InfoCanvas")) as GameObject;
        if (InfoCanvas != null){
          InfoCanvas.transform.SetParent(InfoCanvasContainer.transform);
          InfoCanvas.transform.position = InfoCanvasContainer.transform.position + infoCanvasYOffset + infoCanvasZOffset;
//          InfoCanvas.transform.rotation = Quaternion.LookRotation(-hit.normal);

          var nameValueObj = InfoCanvas.transform.Find("Panel/NameValue");
          var influenceValueObj = InfoCanvas.transform.Find("Panel/InfluenceValue");

          _nameValue = nameValueObj.GetComponent<Text>();
          _nameValue.text = name;

          _influenceValue = influenceValueObj.GetComponent<Text>();
        }
        _influenceValue.text = influence.ToString();
      }
    }

    void DestroyInfoCanvases(GameObject go) {
      foreach (Transform child in go.transform) {
        Destroy(child.gameObject);
      }
    }

    void FixedUpdate() {
      Vector3 fwd = transform.TransformDirection(Vector3.forward);
      RaycastHit hit;
      Ray ray = new Ray(transform.position, fwd);

      if (Physics.Raycast(ray, out hit, range)) {
        _showText = true;
        if (LastCube && (LastCube.name != null) && (LastCube.name != hit.collider.name)) {
          DestroyInfoCanvases(InfoCanvasContainer);
        }
        _name = hit.collider.name;
        LastCube = hit.collider.gameObject;

        if (LastCube) {
          CubeData cubeData = LastCube.GetComponent<CubeData>();
          _influence = cubeData.Influence;
          _color = cubeData.Color;

          BuildInfoCanvas(_name, _influence, hit);
        }

        foreach (Transform child in RtsNode.transform) {
          child.transform.parent = MountNode.transform;
        }
        LastCube.transform.parent = RtsNode.transform;

      }
      else {
        if (Gestures && Gestures.IsHandPinching) {
          return;
        }
        _showText = false;

        if (LastCube) {
          DestroyInfoCanvases(InfoCanvasContainer);
        }
      }
    }


    void Start() {
      Gestures = GesturesContainer.GetComponent<GesturesService>();
    }

    void OnGUI() {
      if (_showText) {
        GUI.Label(new Rect(10, 10, 300, 20), "Name: " + _name + "   " + "Influence: " + _influence);
      }
    }
  }
}
