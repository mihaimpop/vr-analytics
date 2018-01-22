using UnityEngine;

namespace Scripts.DataMapper{
  public class CubeData : MonoBehaviour {
    public Color Color;
    public float Influence;
    public string Name;

    public CubeData(string _Name, float _Influence, Color _Color) {
      Name = _Name;
      Influence = _Influence;
      Color = _Color;
    }
    public string GetName() {
      return Name;
    }
    public void SetName(string _Name) {
      Name = _Name;
    }
    public float GetInfluence() {
      return Influence;
    }
    public void SetInfluence(float _Influence) {
      Influence = _Influence;
    }
    public Color GetColor() {
      return Color;
    }
    public void SetColor(Color _Color) {
      Color = _Color;
    }
  }
}