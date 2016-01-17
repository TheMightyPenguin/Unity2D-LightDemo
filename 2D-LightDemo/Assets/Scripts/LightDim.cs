using UnityEngine;
using System.Collections;

public class LightDim : MonoBehaviour{

	Light l;
	float min = 2f, max = 6f;
	bool incrementing = false, decrementing = false;

	void Start(){
		l = GetComponent<Light>();
		InvokeRepeating("RandomLight", 0.0f, 0.3f);
	}

	void Update(){

	}

	void RandomLight(){
		float val = l.intensity;
		val += Random.Range(0.0f, 1.0f) * RandomSign();
		l.intensity = Mathf.Clamp(val, min, max);		 
		print(l.intensity);
	}

	IEnumerator IncrementLight(){
		incrementing = true;
		while(l.intensity < max){
			l.intensity = Mathf.Lerp(l.intensity, max, 0.1f);
			yield return null;
		}
		incrementing = false;
	}

	IEnumerator DecremenLight(){
		decrementing = true;
		while(l.intensity > min){
			l.intensity = Mathf.Lerp(l.intensity, min, 0.1f);
			yield return null;
		}
		decrementing = false;
	}

	int RandomSign(){
		return Random.Range(0, 2) == 0 ? -1 : 1;
	}
}
