using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour, IGameObject {
	[SerializeField]
	private float _startPositionX = 0.05f;
	[SerializeField]
	private float _endPositionX = -0.014f;

	virtual public void GameUpdate () {
		Move ();
	}

	private void Move() {
		Vector3 position = transform.position;
		position.x -= Manager.Instance.Speed;
		if (position.x < _endPositionX) {
			FinishEndPosition ();
		} else {
			transform.position = position;
		}
	}

	virtual protected void FinishEndPosition() {
		transform.position = new Vector3 (_startPositionX, transform.position.y, 0);
	}
}