using Scripts.DataMapper;
using UnityEngine;

namespace Scripts.GameObjects{
  public class Cube {
    private GameObject _cube;
    public Color Color;
    public CubeData CubeData;
    
    public float Influence;
    public float Scale;
    public GameObject MountNode = GameObject.Find("MountNode");
    
    public int Index;
    public string Name;
    public Vector3 Position;

    public Cube(string name, float influence, Color color, float scale, Vector3 position) {
      Name = name;
      Influence = influence;
      Color = color;
      Scale = scale;
      Position = position;
    }

    public float GetInfluence() {
      return Influence;
    }

    public void Render() {
      _cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
  
      CubeData = _cube.AddComponent<CubeData>();

      CubeData.SetName(Name);
      CubeData.SetInfluence(Influence);
      CubeData.SetColor(Color);

      _cube.transform.parent = MountNode.transform;
      _cube.name = Name;
      _cube.transform.position = Position;
      _cube.transform.localScale = new Vector3(0.1F, 0.1F, 0.1F) + new Vector3(Scale, Scale, Scale);
      _cube.GetComponent<Renderer>().material.color = Color;
    }

  }
}
