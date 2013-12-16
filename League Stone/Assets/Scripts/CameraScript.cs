using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/**
 * Main class for controlling the camera and all user interface actions. 
 * Controls all the mouse click events and keyboard input. 
 * */
public class CameraScript : MonoBehaviour {

	private int counter; 

	//List of cards in hand
	private ArrayList cards;
//	private class transformPair {
//		transformPair(Vector3 position, Quaternion rotation) {
//			cardPos = position;
//			cardRot = rotation;
//		}
//		Vector3 cardPos;
//		Quaternion cardRot;
//	}

//	private Dictionary<int, transformPair> positions; //Initialize?

	void Awake() {
		cards = new ArrayList();
	}

	// Use this for initialization
	void Start () {
		counter = 0; 

	}
	
	// Update is called once per frame
	void Update () {


		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
			
		if (Input.GetMouseButtonDown(0)) {
			//If clicked on card
			Debug.Log("Click");
			if (Physics.Raycast(ray, out hit, 100))  {
				if (hit.collider.gameObject.CompareTag("Card")) {
					Debug.Log("Card");
					for (int ii = 0; ii < cards.Count; ++ii) {
						if (cards[ii] == hit.transform) {

							if (counter % 5 == 0) {
								Debug.Log("Hit");
								hit.transform.GetComponent<CardPosition>().goToDeck();
							}
							else if (counter % 5 == 1) {
								Debug.Log("Hit");
								hit.transform.GetComponent<CardPosition>().goToInspection();
							}
							else if (counter % 5 == 2) {
								Debug.Log("Hit");
								hit.transform.GetComponent<CardPosition>().goToHand((float)cards.Count, ii);
							}
							else if (counter % 5 == 3) {
								Debug.Log("Hit");
								hit.transform.GetComponent<CardPosition>().goToField();
							}
							else if (counter % 5 == 4) {
								Debug.Log("Hit");
								hit.transform.GetComponent<CardPosition>().goToGraveyard();
							}
							counter++;

						}
					}
				}
			}

//			else if (/*object is highlightable*/) {
//				//GUI Highlight object
//			}

		}
	}
	public void addCard(Transform card) {
		Debug.Log(cards.Count + "This is the card" + card);
		cards.Add(card);
		Debug.Log("Card Added");
	}
}
