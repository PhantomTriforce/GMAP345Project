using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TurnDisplayer : MonoBehaviour {

	private GameObject gameController;
	private int turn;
	public Text text;

	// Use this for initialization
	void Start () {
		gameController = GameObject.Find("GameController");
	}
	
	// Update is called once per frame
	void Update () {
		turn = gameController.GetComponent<GameController>().turn;
		if(turn == 1) {
			text.text = "Red's Turn";
		}
		else {
			text.text = "Blue's Turn";
		}
	}
}
