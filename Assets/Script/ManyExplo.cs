using UnityEngine;
using System.Collections;

public class ManyExplo : MonoBehaviour {

	public GameObject[] explotion;

	void Start () {
		StartCoroutine(Starting());
	}

	private IEnumerator Starting(){
		for(int a=0;a<explotion.Length;a++){
			explotion[a].SetActive(true);
			yield return new WaitForSeconds(0.2f);
		}
		Destroy(gameObject);
	}
}
