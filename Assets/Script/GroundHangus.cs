using UnityEngine;
using System.Collections;

public class GroundHangus : MonoBehaviour {

	private SpriteRenderer sr;

	void Start () {
		sr = GetComponent<SpriteRenderer>();
	}

	private IEnumerator FadeBlack(){
		Color tempColor = sr.color;
		float black = 0.2f;
		tempColor.r = black;
		tempColor.g = black;
		tempColor.b = black;
		sr.color = tempColor;

		yield return new WaitForSeconds(2f);
		while(black < 1){
			black += 0.2f * Time.deltaTime;
			tempColor.r = black;
			tempColor.g = black;
			tempColor.b = black;
			sr.color = tempColor;
			yield return null;
		}
	}

	private void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Weapon"){
			StopAllCoroutines();
			StartCoroutine(FadeBlack());
		}
	}
}
