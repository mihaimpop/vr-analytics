using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Scripts{
	public class QueryUi : MonoBehaviour{

		public GameObject QueryCanvas;
		public GameObject InstatiationServiceContainer;
		
		public string RequestUrl;
		public string FilterSection;
		public string FilterBy;
		public List<string> FiltersBy;
		public string FilterValue;
		public List<string> FilterValues;

		public GameObject UiService;
		
		public string BaseUrl = "http://localhost:3000/api/data";

		public float Slider2ValueBuffer, Slider3ValueBuffer;

		public void SetFilterSection(string filterSection){
			FilterSection = filterSection;
			RequestUrl = "";
			FilterBy = "";
			FiltersBy.Clear();
			FilterValue = "";
			FilterValues.Clear();
		}

		void ValidateEmptyResource(){
			if (FilterSection == ""){
				return;
			}
			if (FilterBy == ""){
				return;
			}
		}

		public void SetFilterBy(string filterBy){
			ValidateEmptyResource();
			FilterBy = filterBy;
		}

		public void SetFiltersBy(string filterBy){
			ValidateEmptyResource();
			if (FiltersBy.Contains(filterBy)){
				return;
			} else{
				FiltersBy.Add(filterBy);
			}
		}

		public void SetFilterValue(string value){
			ValidateEmptyResource();
			FilterValue = value;
		}
		
		public void AddFilterValues(string value){			
			ValidateEmptyResource();
			if (FilterValues.Contains(value)){
				return;
			} 
			FilterValues.Add(value);
		}

		public void AddColorFilter(string colorCode){			
			ValidateEmptyResource();
			if (FilterValues.Contains(colorCode)){
				return;
			} 
			
			if (FilterBy == "?color="){
				FilterValue = colorCode;
			} else{
				FilterValues.Add(colorCode);
			}
		}

		public void AddInfluenceFilter(float value){
			ValidateEmptyResource();
			if (FilterBy == "?influence="){
				FilterValue = value.ToString();
			} else{
				FilterValues.Add(value.ToString());
			}
		}

		public void AddSlider1Filter(){
			var sliderScript = UiService.GetComponent<GetSliderValue>();
			FilterValue = sliderScript.SliderScript1.value.ToString();
		}
		
		public void AddSlider2Filter(){
			FilterBy = "FiltersBy";
			var sliderScript = UiService.GetComponent<GetSliderValue>();
				FilterValues.Insert(0, sliderScript.SliderScript2.value.ToString());
		}
		
		public void AddSlider3Filter(){
			FilterBy = "FiltersBy";
			var sliderScript = UiService.GetComponent<GetSliderValue>();
				FilterValues.Insert(1, sliderScript.SliderScript3.value.ToString());
		}

		public void GenerateRequestUrl(){
			if (FilterSection == null){
				return;
			}
			if (FilterBy == "?color="){
				RequestUrl = BaseUrl + FilterBy + FilterValue;
			} else if (FilterBy == "?colorIn="){
				RequestUrl = BaseUrl + FilterBy;
				for (int i = 0; i < FilterValues.Count; i++){
					if (i == FilterValues.Count){
						RequestUrl += FilterValues[i];
					} else{
						RequestUrl += FilterValues[i] + ",";
					}
				}
			} else if (FilterBy == "?influenceLt="){
				RequestUrl = BaseUrl + FilterBy + FilterValue;
			} else if (FilterBy == "?influenceGt="){
				RequestUrl = BaseUrl + FilterBy + FilterValue;
			} else if (FilterBy == "FiltersBy"){
				RequestUrl = BaseUrl + "?influenceGt=" + FilterValues[0] + "&influenceLt=" + FilterValues[1];
			}
			var instatiationService = InstatiationServiceContainer.GetComponent<InstantiationService>();
			instatiationService.SetRequestUrl(RequestUrl);
		}

		public void Cancel(){
			Destroy(QueryCanvas);
			var instatiationService = InstatiationServiceContainer.GetComponent<InstantiationService>();
			instatiationService.SetRequestUrl(BaseUrl);
		}

		void Start () {
	
		}
	
		void Update () {
	
		}
	}
}
