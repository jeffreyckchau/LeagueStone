using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/**
 * Main class for controlling the camera and all user interface actions. 
 * Controls all the mouse click events and keyboard input. 
 * */
public class CameraScript : MonoBehaviour {


	//List of cards in hand
	private List<myCard> Deck, Hand, Field, Inspect, Graveyard;


	private class myCard {
		public myCard(Transform newCard) {
			card = newCard;
		}
		public Transform card;
		public int counter = 1;
	}

	void Awake() {
		Deck = new List<myCard>();
		Hand = new List<myCard>();
		Field = new List<myCard>();
		Inspect = new List<myCard>();
		Graveyard = new List<myCard>();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {


		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
			
		if (Physics.Raycast(ray, out hit, 100))  {
		
			//If clicked 
			if (Input.GetMouseButtonDown(0)) {
				Debug.Log("Click");

				//If it's a card
				//TODO need to revise this to reflect new functionality in cards
				if (hit.collider.gameObject.CompareTag("Card")) {
					Debug.Log("Card");
					/*
					for (int ii = 0; ii < Deck.Count; ++ii) {
						if (Deck[ii].card == hit.transform) {

							if (Deck[ii].counter % 5 == 0) {
								Debug.Log("Hit");
								hit.transform.GetComponent<CardPosition>().goToDeck();
							}
							else if (Deck[ii].counter % 5 == 1) {
								Debug.Log("Hit");
								hit.transform.GetComponent<CardPosition>().goToInspection((float)Inspect.Count, ii);
							}
							else if (Deck[ii].counter % 5 == 2) {
								Debug.Log("Hit");
								hit.transform.GetComponent<CardPosition>().goToHand((float)Deck.Count, ii);
							}
							else if (Deck[ii].counter % 5 == 3) {
								Debug.Log("Hit");
								hit.transform.GetComponent<CardPosition>().goToField();
							}
							else if (Deck[ii].counter % 5 == 4) {
								Debug.Log("Hit");
								hit.transform.GetComponent<CardPosition>().goToGraveyard();
							}
							Deck[ii].counter++;

						}
					}
				}
				*/
					//Will most likely throw null pointer because c is deleted
					if (hit.collider.gameObject.CompareTag("Card")) {
						foreach (myCard c in Deck) {
							if (hit.transform == c.card) {
								hit.transform.GetComponent<CardPosition>().goToInspection();
								hit.transform.GetComponent<CardPosition>().updatePosition(Inspect.Count, Inspect.IndexOf(c));
								return;
							}
						}
						foreach (myCard c in Inspect) {
							if (hit.transform == c.card) {
								hit.transform.GetComponent<CardPosition>().goToHand();
								hit.transform.GetComponent<CardPosition>().updatePosition(Hand.Count, Hand.IndexOf(c));
								return;
							}
						}
						foreach (myCard c in Hand) {
							if (hit.transform == c.card) {
								hit.transform.GetComponent<CardPosition>().goToField();
								hit.transform.GetComponent<CardPosition>().updatePosition(Field.Count, Field.IndexOf(c));
								return;
							}
						}
						foreach (myCard c in Field) {
							if (hit.transform == c.card) {
								hit.transform.GetComponent<CardPosition>().goToGraveyard();
								hit.transform.GetComponent<CardPosition>().updatePosition();
								return;
							}
						}
						foreach (myCard c in Graveyard) {
							if (hit.transform == c.card) {
								hit.transform.GetComponent<CardPosition>().goToDeck();
								hit.transform.GetComponent<CardPosition>().updatePosition();
								return;
							}
						}
					}
					Debug.Log("Blak");
					//If it's the deck
				}
				else if (hit.collider.gameObject.CompareTag("Deck")) {
					Debug.Log("Deck");
					Instantiate(Resources.Load("Prefabs/Common Card"));
				}
			}

//			else if (/*object is highlightable*/) {
//				//GUI Highlight object
//			}

		}
	}
	public void addCardToList(Transform card, int option) {
		//Debug.Log(cards.Count + "This is the card" + card);
		switch(option) {
		case 0:
			Deck.Add(new myCard(card));
			Debug.Log("Card added to Deck");
			break;

		case 1:
			Hand.Add(new myCard(card));
			Debug.Log("Card added to Hand");
			break;

		case 2: 
			Field.Add(new myCard(card));
			Debug.Log("Card added to Field");
			break;

		case 3:
			Inspect.Add(new myCard(card));
			Debug.Log("Card added to Inspect");
			break;

		case 4:
			Graveyard.Add(new myCard(card));
			Debug.Log("Card added to Graveyard");
			break;

		default:
			break;
		}
	
	}

	public void removeCardFromList(Transform card, int option) {

		switch(option) {
		case 0:
			foreach (myCard c in Deck) {
				if (c.card == card) {
					Deck.Remove(c);
					Debug.Log("Card removed from Deck");
					break;
				}
			}
			break;
			
		case 1:
			foreach (myCard c in Hand) {
				if (c.card == card) {
					Hand.Remove(c);
					Debug.Log("Card removed from Hand");
					break;
				}
			}
			break;
			
		case 2: 
			foreach (myCard c in Field) {
				if (c.card == card) {
					Field.Remove(c);
					Debug.Log("Card removed from Field");
					break;
				}
			}
			break;
			
		case 3:
			foreach (myCard c in Inspect) {
				if (c.card == card) {
					Inspect.Remove(c);
					Debug.Log("Card removed from Inspect");
					break;
				}
			}
			break;
			
		case 4:
			foreach (myCard c in Graveyard) {
				if (c.card == card) {
					Inspect.Remove(c);
					Debug.Log("Card removed from Graveyard");
					break;
				}
			}
			break;
			
		default:
			Debug.LogError("Trying to remove a card from the wrong list");
			break;
		}
	}
}
