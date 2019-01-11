using UnityEngine;
using System.Collections;

public class DestroySelf : MonoBehaviour {
	
	public SpriteRenderer[] bodyPart;

	private float colorTime;

	void Start () {
		colorTime = 1f;
		StartCoroutine(Dead());
	}

	IEnumerator Dead(){
		while(colorTime > 0){
			colorTime -= 0.5f * Time.deltaTime;
			for(int a=0;a<bodyPart.Length;a++){
				bodyPart[a].color = new Color(bodyPart[a].color.r,bodyPart[a].color.g,bodyPart[a].color.b,colorTime);
			}
			yield return null;
		}
		Destroy(gameObject);
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.L)){
			StartCoroutine(Dead());
		}
	}
}
