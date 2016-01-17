﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class Player: MonoBehaviour{

	//Primitive Types
	public float jumpHeight = 4, timeToJumpApex = .4f; //timeToJumpApex basically is the time it takes to reach the highest point.
	float gravity, jumpVelocity;
	float moveSpeed = 6f;
	float velocityXSmoothing;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = 0f;

	//Structs or Classes
	Vector3 velocity;
	Controller2D controller;
	SkeletonAnimation skeletonAnimation;

	void Start(){
		controller = GetComponent<Controller2D>();
		velocity = new Vector3();

		gravity = -2 * jumpHeight / Mathf.Pow(timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		print ("gravity: " + gravity + " jump velocity:" + jumpVelocity + " deltaTime:" + Time.deltaTime);

		//Spine Stuff
		skeletonAnimation = GetComponent<SkeletonAnimation>();
	}

	void Update(){
		//Horizontal Movement
		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		float targetVelocityX = input.x * moveSpeed;

		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below ? accelerationTimeGrounded : accelerationTimeAirborne));

		//Vertical Movement
		if(controller.collisions.below || controller.collisions.above){
			velocity.y = 0;
		}
		if(Input.GetKeyDown(KeyCode.Space) && controller.collisions.below){
			velocity.y = jumpVelocity;
		}
		velocity.y += gravity * Time.deltaTime;

		controller.Move(velocity * Time.deltaTime);

		if(!controller.collisions.above || input.x != 0){
			HandleAnimations(input.x, velocity.y);
		}

		if(Input.GetKeyDown(KeyCode.R)){
			transform.position = new Vector3();
		}
	}

	void HandleAnimations(float x, float y){
		if(controller.collisions.below){
			skeletonAnimation.loop = true;
			if(Mathf.Abs(x) > 0){
				skeletonAnimation.AnimationName = Mathf.Abs(x) > 0.8f ? "Run" : "Walk";
				skeletonAnimation.skeleton.FlipX = x < 0;
			}else{
				skeletonAnimation.AnimationName = "Idle";
			}
		}else{
			skeletonAnimation.loop = false;
			skeletonAnimation.AnimationName =  y > 0 ? "Jump" : "Fall";
		}
	}

	void PrintKey(){

	}
}
