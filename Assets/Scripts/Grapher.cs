using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class Grapher : MonoBehaviour {

	public GameObject cube;

	bool isAnimated;
	public Toggle animToggle;


	int max = 100;
	int speedOfGraph = 4;
	GameObject[] cubes;
	GameObject go;
	GameObject parentOfCubes;

	//Logger
	string path = "Assets/Log.txt";
	StreamWriter writer;
	string logText = DateTime.Now + "\n";
	//

	//Function list//
	public enum Functions 
	{
		Sin,Cos,Tan,X,X2,X3
	}
	public Functions functions;
	public Dropdown funcDropDown;
	List<String> funcList;
	//

	//Camera Adjustment
	public Camera cam;
	//
	public float Range;

	void Awake()
	{
//		animToggle.onValueChanged.AddListener(delegate {
//			ToggleValueChanged(animToggle);
//		});

		funcDropDown.onValueChanged.AddListener (delegate {
			FuncChanged ();
		});

//		funcDropDown.value = 5;
//		funcDropDown.onValueChanged
		parentOfCubes = GameObject.Find ("ParentOfCubes");
		Range = 2f;
		cam.orthographicSize = Range / 2 + 0.1f *Range;
		funcList = new List<String> (Enum.GetNames (typeof(Functions)));
		funcDropDown.AddOptions (funcList);


		cubes = new GameObject[max];
		writer  = new StreamWriter (path, true);

		float step = Range/ max;
		Vector3 currentPos = Vector3.zero;
		Vector3 currentScale = Vector3.one * step;
		for (int i = 0; i < max; i++) {
			currentPos.x = (i + 0.5f) * step - Range/2;
			currentPos.y = function (currentPos.x);
			go = Instantiate (cube, currentPos, Quaternion.identity); 
			go.transform.localScale = currentScale;
			go.transform.SetParent (parentOfCubes.transform);
			cubes [i] = go;
		}
	}


	void Update(){
		isAnimated = animToggle.isOn;
		if(isAnimated)
		{
			speedOfGraph = 4;
			for (int i = 0; i < max; i++) {
				Vector3 point = cubes [i].transform.position;
				point.y = function (point.x);
				cubes [i].transform.position = point;
			}
		}
		else{
			speedOfGraph = 0;
		}
	}


	float function(float input){

		int currentValue = funcDropDown.value;
		switch (currentValue) {
		case 0:
			return Mathf.Sin (Mathf.PI *input + speedOfGraph*Time.time);
		case 1:
			return Mathf.Cos (Mathf.PI *input + speedOfGraph*Time.time);
		case 2:
			return Mathf.Tan (Mathf.PI *input + speedOfGraph*Time.time);
		case 3:
			return input;
		case 4:
			return input * input;
		case 5:
			return input * input*input;
		default:
			return 0.0f;
		}
	}


//	public void ToggleValueChanged(Toggle change)
//	{
//		for (int i = 0; i < max; i++) {
//			Vector3 point = cubes [i].transform.position;
//			point.y = function (point.x);
//			cubes [i].transform.position = point;
//		}
//		Debug.Log (" Toggle Changed");
//	}

	public void FuncChanged()
	{
		for (int i = 0; i < max; i++) {
			Vector3 point = cubes [i].transform.position;
			point.y = function (point.x);
			cubes [i].transform.position = point;
		}
//		Debug.Log ("Func Changed");
	}
}
