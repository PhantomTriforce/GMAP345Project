using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TotalMovementDisplay : MonoBehaviour {

	public Text text;

	private GameObject red1, blue1, gameController;
	private int distanceRed, distanceBlue, turn;

	// Use this for initialization
	void Start () {
		red1 = GameObject.Find("Red_1");
		blue1 = GameObject.Find("Blue_1");
		gameController = GameObject.Find("GameController");
	}
	
	// Update is called once per frame
	void Update () {
		turn = gameController.GetComponent<GameController>().turn;
		distanceRed = (int) red1.GetComponent<TankMovementTracker>().totalDistance;
		distanceBlue = (int) blue1.GetComponent<TankMovementTracker>().totalDistance;

		if(distanceRed > 20) {
			distanceRed = 20;
		}
		if(distanceBlue > 20) {
			distanceBlue = 20;
		}

		if(turn == 1) {
			text.text = "Total movement: " + distanceRed;
		}
		else {
			text.text = "Total movement: " + distanceBlue;
		}
	}
}
