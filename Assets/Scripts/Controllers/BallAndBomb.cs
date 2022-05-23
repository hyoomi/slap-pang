using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallAndBomb : MonoBehaviour
{
	public const float MOVE_SPEED = 0.2f;
	protected Animator _anim;

	// 구슬의 상태를 표시합니다. Explode입력시 Explode()함수 실행
	Define.BallState _state;
	public Define.BallState State
	{
		get { return _state; }
		set
		{
			if (_state == value)
				return;

			_state = value;
			Managers.Action.BallsAction = _state;
			if (_state == Define.BallState.Explode)
				Explode();
		}
	}

	// Cell index를 저장 
	int _cellIndex;
	public int CellIndex
	{
		get
		{ return _cellIndex; }
		set
		{
			if (_cellIndex == value)
				return;

			_cellIndex = value;
		}
	}

	void Start()
    {
		Init();
    }

	protected virtual void Init()
	{

	}

	// 스르륵 이동하는 코루틴
	IEnumerator Lerp(Vector3 startPos)
	{
		float lerpDuration = MOVE_SPEED;
		Vector3 startValue = startPos;
		Vector3 endValue = Vector3.zero;
		float timeElapsed = 0;

		while (timeElapsed < lerpDuration)
		{
			transform.localPosition = Vector3.Lerp(startValue, endValue, timeElapsed / lerpDuration);
			timeElapsed += Time.deltaTime;
			yield return null;
		}

		transform.localPosition = Vector3.zero;
		State = Define.BallState.Idle;
	}

	// ball의 부모를 설정한다
	public void Move(Transform parent, int index)
	{
		State = Define.BallState.Move;

		transform.SetParent(parent); // 공의 부모를 빈칸으로 바꿔준다
		CellIndex = index;

		StartCoroutine(Lerp(transform.localPosition));
	}

	public virtual void Explode()
    {

    }

}
