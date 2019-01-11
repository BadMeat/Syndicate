using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

	public float speed;

	//batas maksimal atas
	public float maxY;


	void Start(){
		StartCoroutine(Ending());
	}

	IEnumerator Ending(){
		while(transform.position.y < maxY){
			transform.Translate(Vector3.up * Time.deltaTime * speed);
			yield return null;
		}
		SceneManager.LoadScene("LoadingBack");
	}
}
