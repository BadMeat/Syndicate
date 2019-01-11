using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Talking : MonoBehaviour {

	public string[] dialog;
	public Text dialogText;

	private bool read;
	private int nextDialog;
	private bool readyNext;

	void Update () {
		if(!read){
			if(nextDialog < dialog.Length){
				StartCoroutine(AnimationText());
			}
		}

		if(nextDialog >= dialog.Length){
			gameObject.SetActive(false);
		}
	}

	private IEnumerator AnimationText(){
		if(read){
			yield break;
		}
		read = true;
		for(int a=0;a<dialog[nextDialog].Length + 1;a++){
			dialogText.text = dialog[nextDialog].Substring(0,a);
			yield return new WaitForSeconds(0.1f);
		}
		nextDialog++;
		read = false;
	}
}
