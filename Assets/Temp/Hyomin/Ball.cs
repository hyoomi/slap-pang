using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Move->Idle: 이동완료했으니 콤보체크해야함
// Idle->Move or Check->Move: 공이동 시작. 다른 입력 막아놔야함
// Idle->Explode or Idle->Check: 콤보체크완료. 터뜨리기+애니메이션

public class Ball : BallAndBomb
{
	[SerializeField] Sprite[] images;
	Animator _animator;
	bool _updated = false;	

	/* ------------------ ball Info ------------------*/
	public Define.BallType Type
	{
		get { return _ballInfo.type; }
	}
	public CellController ParentCell
	{
		get
		{ return _ballInfo.parentCell; }
		set
		{
			if (_ballInfo.parentCell == value)
				return;

			_ballInfo.parentCell = value;
		}
	}
	public Vector3 LocalPos
	{
		get
		{ return _ballInfo.localPos; }
		set
		{
			if (_ballInfo.localPos == value)
				return;

			_ballInfo.localPos = value;
			_updated = true;
		}
	}
	public Define.BallState State
	{
		get { return _ballInfo.state; }
		set
		{
			if (_ballInfo.state == value)
				return;

			_ballInfo.state = value;
			Managers.Action.BallsAction = value;
			// UpdateAnimation();
			_updated = true;
		}
	}
	public Define.MoveDir Dir
	{
		get { return _ballInfo.moveDir; }
		set
		{
			if (_ballInfo.moveDir == value)
				return;

			_ballInfo.moveDir = value;

			// UpdateAnimation(); //폭발 애니메이션
			_updated = true;
		}
	}
	private class BallInfo
	{
		public Define.BallType type; // 컵 잔 화분 접시 돌
		public CellController parentCell;
		public Vector3 localPos;
		public Define.BallState state; // 정지 이동 폭발
		public Define.MoveDir moveDir; // 위 아래 오른쪽 왼쪽

		public void Init()
		{
			int random = UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(Define.BallType)).Length);
			type = (Define.BallType)(random);
			parentCell = null;
			localPos = Vector3.zero;
			state = Define.BallState.Idle;
			moveDir = Define.MoveDir.None;
		}
	}
	BallInfo _ballInfo = new BallInfo();
	/* -----------------------------------------------*/

	void Start()
	{
		//Init();
		_ballInfo.Init();
		GetComponent<Image>().sprite = images[(int)Type];
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
		float lerpDuration = 0.5f;
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
		State = Define.BallState.Idle;
	}

	// ball의 부모를 설정한다
	public void SetParent(Transform parent)
    {
		transform.SetParent(parent); // 공의 부모를 빈칸으로 바꿔준다
	}

	

	

/*	void Update()
	{
		//UpdateController();
	}
*/

/*
 * bool _moveKeyPressed = false;
	float _speed = 2.0f;
	protected void Init()
	{
		_animator = GetComponent<Animator>();

		UpdateAnimation();
	}

	protected void UpdateAnimation()
	{
		if (_animator == null)
			return;

		if (State == Define.BallState.Idle)
		{

		}
		else if (State == Define.BallState.Move)
		{

		}
		else if (State == Define.BallState.Explode)
		{
			_animator.Play("Explode");
		}
		else
		{

		}
	}

	protected void UpdateController()
	{
		switch (State)
		{
			case Define.BallState.Idle:
				GetDirInput();
				break;
			case Define.BallState.Move:
				GetDirInput();
				break;
		}

		switch (State)
		{
			case Define.BallState.Idle:
				UpdateIdle();
				break;
			case Define.BallState.Move:
				UpdateMove();
				break;
			case Define.BallState.Explode:
				UpdateExplode();
				break;
		}
	}

	// 키보드 입력: _moveKeyPressed
	void GetDirInput()
	{
		_moveKeyPressed = true;

		if (Input.GetKey(KeyCode.W))
		{
			Dir = Define.MoveDir.Up;
		}
		else if (Input.GetKey(KeyCode.S))
		{
			Dir = Define.MoveDir.Down;
		}
		else if (Input.GetKey(KeyCode.A))
		{
			Dir = Define.MoveDir.Left;
		}
		else if (Input.GetKey(KeyCode.D))
		{
			Dir = Define.MoveDir.Right;
		}
		else
		{
			_moveKeyPressed = false;
		}
	}

	// Idle->Move, Idle->Explode 체크
	protected void UpdateIdle()
	{
		// 이동 상태로 갈지 확인
		if (_moveKeyPressed)
		{
			State = Define.BallState.Move;
			return;
		}

		// 폭탄을 터트렸는지 확인
		if (Input.GetKey(KeyCode.Space))
		{
			Debug.Log("Skill !");
		}
	}

	// 스르륵 이동. Move끝나고 Move->Idle
	protected virtual void UpdateMove()
	{
		// 부모를 변경한 이후에
		Vector3 destPos = Vector3.zero;
		Vector3 moveDir = destPos - transform.position;

		// 도착 여부 체크
		float dist = moveDir.magnitude;
		if (dist < _speed * Time.deltaTime) // 도착 판정
		{
			transform.position = destPos;
			MoveToNextPos();
		}
		else // 조금씩 이동
		{
			transform.position += moveDir.normalized * _speed * Time.deltaTime;
			State = Define.BallState.Move;
		}
	}

	protected void UpdateExplode() { }



	protected void MoveToNextPos()
	{
		// Move -> Idle
		if (_moveKeyPressed == false)
		{
			State = Define.BallState.Idle;
			return;
		}
	}


	public virtual void OnExplode()
	{
		State = Define.BallState.Explode;

		GameObject effect = null;// Managers.Resource.Instantiate("Effect/DieEffect");
		effect.transform.position = transform.position;
		effect.GetComponent<Animator>().Play("START");
		GameObject.Destroy(effect, 0.5f);
	}


	*/
}
