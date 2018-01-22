using Leap;
using Leap.Unity;
using UnityEngine;

namespace Scripts{
  public class GesturesService : MonoBehaviour {
    LeapProvider _provider;
    public bool IsHandPinching = false;
    public bool IsLeftHandInFrame;
    
    void Start() {
      _provider = FindObjectOfType<LeapProvider>() as LeapProvider;
    }

    void ComputePinching(Hand hand) {
      if (hand.PinchStrength > 0.9) {
        IsHandPinching = true;
      } else {
        IsHandPinching = false;
      }
    }

    void ComputeLeftHandInFrame(Hand hand) {
      if (hand.IsLeft) {
        IsLeftHandInFrame = true;
      } else {
        IsLeftHandInFrame = false;
      }
    }
    void Update() {
      Frame frame = _provider.CurrentFrame;
      foreach (Hand hand in frame.Hands) {
        ComputePinching(hand);
        ComputeLeftHandInFrame(hand);
      }
    }
  }
}