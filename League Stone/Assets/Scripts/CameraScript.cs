using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/**
 * Main class for controlling the camera and all user interface actions. 
 * Controls all the mouse click events and keyboard input. 
 * */
public class CameraScript : MonoBehaviour {


	//List of cards in hand
	private List<myCard> cards;
//	private class transformPair {
//		transformPair(Vector3 position, Quaternion rotation) {
//			cardPos = position;
//			cardRot = rotation;
//		}
//		Vector3 cardPos;
//		Quaternion cardRot;
//	}

//	private Dictionary<int, transformPair> positions; //Initialize?

	private class myCard {
		public myCard(Transform newCard) {
			card = newCard;
		}
		public Transform card;
		public int counter = 0;
	}

	void Awake() {
		cards = new List<myCard>();
	}

	// Use this for initialization
	void Start () {

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
						if (cards[ii].card == hit.transform) {

							if (cards[ii].counter % 5 == 0) {
								Debug.Log("Hit");
								hit.transform.GetComponent<CardPosition>().goToDeck();
							}
							else if (cards[ii].counter % 5 == 1) {
								Debug.Log("Hit");
								hit.transform.GetComponent<CardPosition>().goToInspection();
							}
							else if (cards[ii].counter % 5 == 2) {
								Debug.Log("Hit");
								hit.transform.GetComponent<CardPosition>().goToHand((float)cards.Count, ii);
							}
							else if (cards[ii].counter % 5 == 3) {
								Debug.Log("Hit");
								hit.transform.GetComponent<CardPosition>().goToField();
							}
							else if (cards[ii].counter % 5 == 4) {
								Debug.Log("Hit");
								hit.transform.GetComponent<CardPosition>().goToGraveyard();
							}
							cards[ii].counter++;

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
		cards.Add(new myCard(card));
		Debug.Log("Card Added");
	}
}
