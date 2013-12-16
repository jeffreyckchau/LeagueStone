using UnityEngine;
using System.Collections;
/**
 * Main class for controlling the camera and all user interface actions. 
 * Controls all the mouse click events and keyboard input. 
 * */
public class CameraScript : MonoBehaviour {

	private int counter; 

	// Use this for initialization
	void Start () {
	counter = 0; 
	}
	
	// Update is called once per frame
	void Update () {

			//If clicked on card
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;	
			Debug.Log("Click");
			if (Physics.Raycast(ray, out hit, 100))  {

				if (hit.collider.gameObject.CompareTag("Card")) {
					Debug.Log("Card");
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
						hit.transform.GetComponent<CardPosition>().goToHand();
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

//			else if (/*object is highlightable*/) {
//				//GUI Highlight object
//			}

		}
	}
	
}
