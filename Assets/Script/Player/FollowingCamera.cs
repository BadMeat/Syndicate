using UnityEngine;
using System.Collections;

public class FollowingCamera : MonoBehaviour {

	private Camera mainCam;
	public float minX{set;get;}
	public float maxX{set;get;}
	public float minY{set;get;}
	public float maxY{set;get;}

	public float rangeBack;
	public float dampTime = 0.5f;
	private Vector3 velo;

	void Start () {
		mainCam = Camera.main;
	}

	void FixedUpdate () {
		if(transform.position.x <= minX && transform.position.y <= minY ){
			mainCam.transform.position = Vector3.SmoothDamp(mainCam.transform.position,new Vector3(minX,minY,mainCam.transform.position.z),ref velo,dampTime);
		}
		else if(transform.position.x >= maxX -rangeBack && transform.position.y <= minY){
			mainCam.transform.position = Vector3.SmoothDamp(mainCam.transform.position,new Vector3(maxX,minY,mainCam.transform.position.z),ref velo,dampTime);
		}
		else if(transform.position.x <= minX && transform.position.y >= maxY){
			mainCam.transform.position = Vector3.SmoothDamp(mainCam.transform.position,new Vector3(minX,maxY,mainCam.transform.position.z),ref velo,dampTime);
		}
		else if(transform.position.x >= maxX && transform.position.y >= maxY){
			mainCam.transform.position = Vector3.SmoothDamp(mainCam.transform.position,new Vector3(maxX ,maxY,mainCam.transform.position.z),ref velo,dampTime);
		}
		else if(transform.position.x <= minX){
			mainCam.transform.position = Vector3.SmoothDamp(mainCam.transform.position,new Vector3(minX,transform.position.y,mainCam.transform.position.z),ref velo,dampTime);
		}
		else if(transform.position.x >= maxX - rangeBack){
			mainCam.transform.position = Vector3.SmoothDamp(mainCam.transform.position,new Vector3(maxX,transform.position.y,mainCam.transform.position.z),ref velo,dampTime);
		}
		else if(transform.position.y >= maxY){
			if(transform.localScale.x > 0){
				mainCam.transform.position = Vector3.SmoothDamp(mainCam.transform.position,new Vector3(transform.position.x + rangeBack,maxY,mainCam.transform.position.z),ref velo,dampTime);
			}
			else if(transform.localScale.x < 0){
				mainCam.transform.position = Vector3.SmoothDamp(mainCam.transform.position,new Vector3(transform.position.x - rangeBack,maxY,mainCam.transform.position.z),ref velo,dampTime);
			}
		}
		else if(transform.position.y <= minY){
			if(transform.localScale.x > 0){
				mainCam.transform.position = Vector3.SmoothDamp(mainCam.transform.position,new Vector3(transform.position.x + rangeBack,minY,mainCam.transform.position.z),ref velo,dampTime);
			}
			else if(transform.localScale.x < 0){
				mainCam.transform.position = Vector3.SmoothDamp(mainCam.transform.position,new Vector3(transform.position.x - rangeBack,minY,mainCam.transform.position.z),ref velo,dampTime);
			}
		}else{
			if(transform.localScale.x > 0){
				mainCam.transform.position = Vector3.SmoothDamp(mainCam.transform.position,new Vector3(transform.position.x + rangeBack,transform.position.y,mainCam.transform.position.z),ref velo,dampTime);
			}
			else if(transform.localScale.x < 0){
				mainCam.transform.position = Vector3.SmoothDamp(mainCam.transform.position,new Vector3(transform.position.x - rangeBack,transform.position.y,mainCam.transform.position.z),ref velo,dampTime);
			}
		}
	}
}
