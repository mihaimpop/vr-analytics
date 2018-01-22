using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Leap.Unity.InputModule {
  public class ToggleToggler : MonoBehaviour {
    public Text text;
    public UnityEngine.UI.Image image;
    public Color OnColor;
    public Color OffColor;
    public string CustomTextOn;
    public string CustomTextOff;

    public void SetToggle(Toggle toggle) {
      if (toggle.isOn) {
        if (CustomTextOn != ""){
          text.text = CustomTextOn;
        } else{
          text.text = "On";
        }
        text.color = Color.white;
        image.color = OnColor;
      } else {
        if (CustomTextOff != ""){
          text.text = CustomTextOff;
        } else{
          text.text = "Off";
        }
        text.color = new Color(0.3f, 0.3f, 0.3f);
        image.color = OffColor;
      }
    }
  }
}