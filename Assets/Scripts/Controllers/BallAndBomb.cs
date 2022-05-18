using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallAndBomb : MonoBehaviour
{
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

	// 슬라이드 액션 구독 
	private void OnEnable()
	{
		Managers.Action.slideAction += OnSlide;
	}
	private void OnDisable()
	{
		Managers.Action.slideAction -= OnSlide;
	}
	// 슬라이드 액션이 발생시 OnSlide함수 실행됨
	void OnSlide(Define.SlideAction slideAction)
	{
		if (slideAction == Define.SlideAction.None) return;
		State = Define.BallState.Move;
		StartCoroutine(Lerp(transform.localPosition));
	}
	// 스르륵 이동하는 코루틴
	IEnumerator Lerp(Vector3 startPos)
	{
		State = Define.BallState.Move;
		float lerpDuration = 0.1f;
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
		yield return new WaitForSeconds(0.1f);

		// 움직이지 않아도 되는 공때문에 다른 공이 도착하기 전에 실행됨
		State = Define.BallState.Idle;
	}

	// ball의 부모를 설정한다
	public void SetParent(Transform parent, int index)
	{
		transform.SetParent(parent); // 공의 부모를 빈칸으로 바꿔준다
		CellIndex = index;
	}

	public virtual void Explode()
    {

    }

}
