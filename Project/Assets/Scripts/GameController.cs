using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
    public int turn = 1;
    public bool targetting = false;
    public bool moving = false;
	public Button fireButton, moveButton;
	public float distanceRed, distanceBlue;

	private GameObject red1, blue1;

	void Start() {
		red1 = GameObject.Find("Red_1");
		blue1 = GameObject.Find("Blue_1");

        red1.GetComponent<TankMovementTracker>().totalDistance = -17f;
        blue1.GetComponent<TankMovementTracker>().totalDistance = -17f;
    }

	void Update() {
		distanceRed = red1.GetComponent<TankMovementTracker>().totalDistance;
		distanceBlue = blue1.GetComponent<TankMovementTracker>().totalDistance;
		if(distanceRed > 20 || distanceBlue > 20) {
			moveButton.interactable = false;
		}
    }

    public void OnClickEndTurn() {
        if(turn == 1) {
            turn = 2;
        }
        else {
            turn = 1;
        }
		fireButton.interactable = true;
		moveButton.interactable = true;
		moving = false;
		targetting = false;

        red1.GetComponent<TankMovementTracker>().totalDistance = 0f;
        blue1.GetComponent<TankMovementTracker>().totalDistance = 0f;
    }

    public void OnClickFire() {
        targetting = true;
        moving = false;
		fireButton.interactable = false;
    }

    public void OnClickMove() {
        moving = true;
        targetting = false;
    }
}
