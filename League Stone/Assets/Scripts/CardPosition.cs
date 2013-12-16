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

	//Assign empty GameObjects in the editor to hold positions and rotations
	public static GameObject deck, field, hand, endHand, inspection, graveyard;
	private state cardState;

	private Vector3 deckPosition, initialFieldPosition, initialHandPosition, finalHandPosition, inspectionPosition, graveyardPosition;

	private Vector3 handPosition;
	private Quaternion handRotation;

	// Use this for initialization
	void Start () {
		Camera.main.GetComponent<CameraScript>().addCard(transform);

		deck = GameObject.FindWithTag("DeckPosition");
		field = GameObject.FindWithTag("FieldPosition");
		hand = GameObject.FindWithTag("HandPosition");
		endHand = GameObject.FindWithTag("End Hand");
		inspection = GameObject.FindWithTag("Inspection");
		graveyard = GameObject.FindWithTag("GravePosition");


		deckPosition = deck.transform.position;
		initialFieldPosition = field.transform.position;
		initialHandPosition = hand.transform.position;
		finalHandPosition = endHand.transform.position;
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
			transform.position = Vector3.Lerp(this.transform.position, handPosition, ANIMATION_SPEED);
			transform.rotation = Quaternion.Lerp(this.transform.rotation, handRotation, ANIMATION_SPEED);
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

	public void goToHand(float capacity, float progress) {
		cardState = state.inHand;
		handPosition = Vector3.Slerp(initialHandPosition, finalHandPosition, (1/(capacity+1))+((1/capacity)*progress));
		handRotation = Quaternion.Slerp(hand.transform.rotation, endHand.transform.rotation, (1/(capacity+1))+((1/capacity)*progress));
//		handPosition = Vector3.Slerp(initialHandPosition, finalHandPosition, 0.5f);
//		handRotation = Quaternion.Slerp(hand.transform.rotation, endHand.transform.rotation, 0.5f);
	}

	public void goToField() {
		cardState = state.onField;
	}
}
