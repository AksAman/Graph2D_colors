using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class Grapher : MonoBehaviour {

	public GameObject cube;


	int max = 200;
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
		parentOfCubes = GameObject.Find ("ParentOfCubes");
		Range = 20f;
		cam.orthographicSize = Range / 2;
		funcList = new List<String> (Enum.GetNames (typeof(Functions)));
		funcDropDown.AddOptions (funcList);


		cubes = new GameObject[max];
		writer  = new StreamWriter (path, true);

		float step = Range/ max;
		Vector3 currentPos = Vector3.zero;
		Vector3 currentScale = Vector3.one * step;
		for (int i = 0; i < max; i++) {
			currentPos.x = (i + 0.5f) * step - Range/2;
			go = Instantiate (cube, currentPos, Quaternion.identity);
			go.transform.localScale = currentScale;
			go.transform.SetParent (parentOfCubes.transform);
			cubes [i] = go;
		}
	}

	void Update(){
		for (int i = 0; i < max; i++) {
			Vector3 point = cubes [i].transform.position;
			point.y = function (point.x);
			cubes [i].transform.position = point;
		}
	}


	float function(float input){
		input = input + speedOfGraph*Time.time;
		int currentValue = funcDropDown.value;
		switch (currentValue) {
		case 0:
			return Mathf.Sin (input);
		case 1:
			return Mathf.Cos (input);
		case 2:
			return Mathf.Tan (input);
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
}
