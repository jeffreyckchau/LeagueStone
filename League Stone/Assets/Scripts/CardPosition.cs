using UnityEngine;
using System.Collections;
/**
 * Script determining where the card position should be depending on the private state of the card.
 * This will eventually handle the animation of each card as well. Currently, the game tracks all the
 * cards at once; no garbage collection.
 * -Jeffrey Chau
 * */
public class CardPosition : MonoBehaviour {

	private enum state {inDeck = 0, underInspection, inHand, onField, inGraveyard};
	public float ANIMATION_SPEED = 0.1f;

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
		this.transform.position = deck.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//Need to also update all the rotations;
		if (cardState == state.inDeck) {
			transform.position = Vector3.Lerp(this.transform.position, deckPosition, ANIMATION_SPEED);
			transform.rotation = Quaternion.Lerp(this.transform.rotation, deck.transform.rotation, ANIMATION_SPEED);
		}
		if (cardState == state.inHand) {
			//Need to change to compute against multiple hands
			transform.position = Vector3.Lerp(this.transform.position, initialHandPosition, ANIMATION_SPEED);
			transform.rotation = Quaternion.Lerp(this.transform.rotation, hand.transform.rotation, ANIMATION_SPEED);
		}
		if (cardState == state.underInspection) {
			transform.position = Vector3.Lerp(this.transform.position, inspectionPosition, ANIMATION_SPEED);
			transform.rotation = Quaternion.Lerp(this.transform.rotation, inspection.transform.rotation, ANIMATION_SPEED);
		}
		if (cardState == state.onField) {
			transform.position = Vector3.Lerp(this.transform.position, initialFieldPosition, ANIMATION_SPEED);
			transform.rotation = Quaternion.Lerp(this.transform.rotation, field.transform.rotation, ANIMATION_SPEED);
		}
		if (cardState == state.inGraveyard) {
			transform.position = Vector3.Lerp(this.transform.position, graveyardPosition, ANIMATION_SPEED);
			transform.rotation = Quaternion.Lerp(this.transform.rotation, graveyard.transform.rotation, ANIMATION_SPEED);
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
