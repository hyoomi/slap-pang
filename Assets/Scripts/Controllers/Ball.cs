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

	/* ------------------ ball Info ------------------*/
	Define.BallType _type;
	public Define.BallType Type
	{
		get { return _type; }
	}

	Define.MoveDir _dir;
	public Define.MoveDir Dir
	{
		get { return _dir; }
		set
		{
			if (_dir == value)
				return;
			_dir = value;
			// UpdateAnimation(); //폭발 애니메이션
		}
	}
	/* -----------------------------------------------*/

	protected override void Init()
	{
		int random = UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(Define.BallType)).Length);
		_type = (Define.BallType)(random);		
		State = Define.BallState.Idle;
		_dir = Define.MoveDir.None;
		GetComponent<Image>().sprite = images[(int)Type];
	}

	public override void Explode()
    {
		Space.ballCount--;
		Destroy(gameObject);
		Destroy(this);
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
