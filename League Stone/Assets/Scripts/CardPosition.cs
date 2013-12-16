using UnityEngine;
using System.Collections;
/**
 * Script determining where the card position should be depending on the private state of the card.
 * This will eventually handle the animation of each card as well. 
 * -Jeffrey Chau
 * */
public class CardPosition : MonoBehaviour {

	private enum state {inDeck = 0, underInspection, inHand, onField, inGraveyard};

	//Assign empty GameObjects in the editor to hold positions
	public static GameObject deck, field, hand, inspection, graveyard;
	private state cardState;

	private Vector3 deckPosition, initialFieldPosition, initialHandPosition, inspectionPosition, graveyardPosition;

	// Use this for initialization
	void Start () {
		deck = GameObject.FindWithTag("DeckPosition");
		field = GameObject.FindWithTag("FieldPosition");
		hand = GameObject.FindWithTag("HandPosition");
		inspection = GameObject.FindWithTag("Inspection");
		graveyard = GameObject.FindWithTag("GravePosition");



		deckPosition = deck.transform.position;
		initialFieldPosition = field.transform.position;
		initialHandPosition = hand.transform.position;
		inspectionPosition = inspection.transform.position;
		graveyardPosition = graveyard.transform.position;

		cardState = state.inDeck;
	}
	
	// Update is called once per frame
	void Update () {
		//Need to also update all the rotations;
		if (cardState == state.inDeck) {
			this.transform.position = deckPosition;
		}
		if (cardState == state.inHand) {
			//Need to change to compute against multiple hands
			this.transform.position = initialHandPosition;
		}
		if (cardState == state.underInspection) {
			this.transform.position = inspectionPosition;
		}
		if (cardState == state.onField) {
			this.transform.position = initialFieldPosition;
		}
		if (cardState == state.inGraveyard) {
			this.transform.position = graveyardPosition;
		}
	}



	//Animation utilities
	public void goToDeck() {
		cardState = state.inDeck;
	}

	public void goToInspection() {
		cardState = state.underInspection;
	}

	public void goToGraveyard() {
		cardState = state.inGraveyard;
	}

	public void goToHand() {
		cardState = state.inHand;
	}

	public void goToField() {
		cardState = state.onField;
	}
}
