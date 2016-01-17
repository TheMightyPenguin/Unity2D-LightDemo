using UnityEngine;
using System.Collections;

public class FadeLightController : MonoBehaviour {
	
	public float toggleSpeed;
	
	bool toggle;
	Light l;
	
	void Start(){
		l = GetComponent<Light>();
		toggle = Random.Range(0, 1) < 0.5 ? true : false;
		l.intensity = Random.Range(0f, 0.20f);
	}
	
	void Update(){
		FadeLight();
	}
	
	void FadeLight(){
		if(toggle){
			if(l.intensity < 0.3f){
				l.intensity += Time.deltaTime * toggleSpeed;
			}else{
				toggle = false;
			}
		}else if(l.intensity > 0.25f){
			l.intensity -= Time.deltaTime * toggleSpeed;
		}else{
			toggle = true;
		}
	}
}