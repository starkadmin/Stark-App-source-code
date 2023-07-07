using UnityEngine;

public class Tweener : MonoBehaviour
{
	[SerializeField] Transform target;
	[SerializeField] float speed = 1f;
	
	public bool position = true, rotation = true, scale = false;
	
	bool autoSleep = false;
	float autoSleepDuration = 5f;
	
	float autoSleepTimer;
	
	Transform _t;
	Transform _transform
	{
		get
		{
			_t ??= transform;
			return _t;
		}
	}
	
	void Update(){
		if(!target) return;
		
		float deltaTime = Time.deltaTime;
		
		//if(autoSleep){
		//	enabled = autoSleepTimer > 0f;
		//	autoSleepTimer -= deltaTime;
		//}
		
		float deltaSpeed = this.speed * deltaTime;
		
		if(position) _transform.position = Vector3.Lerp(_transform.position, target.position, deltaSpeed);
		if(rotation) _transform.rotation = Quaternion.Lerp(_transform.rotation, target.rotation, deltaSpeed);
		if(scale)	 _transform.localScale = Vector3.Lerp(_transform.localScale, target.localScale, deltaSpeed);
	}
	
	public void SetTarget(Transform target){
		if(this.target == target) return;
		this.target = target;
		
		autoSleepTimer = autoSleepDuration;
		enabled = true;
	}
	
	public Transform CreateDefaultPoint(out Transform newPoint){
		newPoint = new GameObject(name).transform;
		newPoint.position = _transform.position;
		newPoint.rotation = _transform.rotation;
		newPoint.localScale = _transform.localScale;
		
		SetTarget(newPoint);
		return newPoint;
	}
}