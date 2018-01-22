using Scripts;
using UnityEngine;

public class UiLogic : MonoBehaviour {
	public GameObject LeftPalm;
	public GameObject Canvas;
	public Vector3 UiOffset = new Vector3(0, 0.00001f, 0.00001f);
	public GameObject[] Clones;

	public GameObject HeadMountGameObject;
	public GameObject GesturesContainer;
	public GesturesService Gestures;

	public void FindDestroyClones() {
		var clones = GameObject.FindGameObjectsWithTag("cloneUi");
		if(clones.Length != 0) {
			foreach(GameObject clone in clones) {
				GameObject.Destroy(clone);
			}
		}
	}
	public void AddUi() {
		Vector3 palmPosition = LeftPalm.transform.position;
		Canvas = (GameObject)Instantiate(Resources.Load("Canvas"), palmPosition + UiOffset, Quaternion.identity);
		Canvas.tag = "cloneUi";
		Canvas.transform.parent = LeftPalm.transform;
	}

	public void DestroyUi() {
		FindDestroyClones();
	}

	public void AddQueryCanvas(){
		Vector3 lookOffset = new Vector3(0, 0.5f, 1f);
		Canvas = (GameObject) Instantiate(Resources.Load("QueryCanvas"), HeadMountGameObject.transform.position + lookOffset,
			Quaternion.identity);
		Canvas.transform.parent = HeadMountGameObject.transform;
	}

	void Start(){
		GesturesContainer = GameObject.Find("GesturesContainer");
		Gestures = GesturesContainer.GetComponent<GesturesService>();
	}
  
	void Update(){
		if(Gestures.IsLeftHandInFrame && Gestures.IsLeftHandInFrame == false && Clones.Length != 0){
			FindDestroyClones();
		}
	}

}