using UnityEngine;
using UnityEngine.UI;

namespace Scripts{
	public class GetSliderValue : MonoBehaviour{

		public GameObject Slider1;
		public GameObject Slider2;
		public GameObject Slider3;
		public Slider SliderScript1;
		public Slider SliderScript2;
		public Slider SliderScript3;
		public GameObject Text1;
		public GameObject Text2;
		public GameObject Text3;
		public Text TextScript1;
		public Text TextScript2;
		public Text TextScript3;

		public void SetButton1Text(){
			TextScript1.text = SliderScript1.value.ToString();
		}
	
		public void SetButton2Text(){
			TextScript2.text = SliderScript2.value.ToString();
		}
	
		public void SetButton3Text(){
			TextScript3.text = SliderScript3.value.ToString();
		}
	
	
		void Start (){
			SliderScript1 = Slider1.GetComponent<Slider>();
			TextScript1 = Text1.GetComponent<Text>();
		
			SliderScript2 = Slider2.GetComponent<Slider>();
			TextScript2 = Text2.GetComponent<Text>();
		
			SliderScript3 = Slider3.GetComponent<Slider>();
			TextScript3 = Text3.GetComponent<Text>();
		}
	}
}
