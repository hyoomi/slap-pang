using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallController : MonoBehaviour
{
	//이미지를 랜덤하게 바꿔주기
	private void Start()
	{

	}
   

    // 슬라이드 액션 구독 (일단 무시해주세요)
    private void OnEnable()
	{
		SpaceController.slideAction += OnSlide;
	}

	private void OnDisable()
	{
		SpaceController.slideAction -= OnSlide;
	}

	void OnSlide(Define.SlideAction slideAction)
	{
		//Debug.Log(this.name + slideAction);
	}


	// 아래 주석은 무시해주세요!

	/*
	protected Animator _animator;
	protected SpriteRenderer _sprite;

	protected bool _updated = false;
	bool _moveKeyPressed = false;
	float _speed = 2.0f;

	public Define.MoveDir Dir
	{
		get { return BallInfo.MoveDir; }
		set
		{
			if (BallInfo.MoveDir == value)
				return;

			BallInfo.MoveDir = value;

			// UpdateAnimation(); //폭발 애니메이션
			_updated = true;
		}
	}

	public Define.MoveDir GetDirFromVec(Vector3Int dir)
	{
		if (dir.x > 0)
			return Define.MoveDir.Right;
		else if (dir.x < 0)
			return Define.MoveDir.Left;
		else if (dir.y > 0)
			return Define.MoveDir.Up;
		else
			return Define.MoveDir.Down;
	}

	public Define.BallState State
	{
		get { return BallInfo.State; }
		set
		{
			if (BallInfo.State == value)
				return;

			BallInfo.State = value;
			// UpdateAnimation();
			_updated = true;
		}
	}

	public void SyncPos()
	{
		Vector3 destPos = Managers.Map.CurrentGrid.CellToWorld(CellPos) + new Vector3(0.5f, 0.5f);
		transform.position = destPos;
	}

	public Vector3Int CellPos
	{
		get
		{
			return new Vector3Int(BallInfo.PosX, BallInfo.PosY, 0);
		}

		set
		{
			if (BallInfo.PosX == value.x && BallInfo.PosY == value.y)
				return;

			BallInfo.PosX = value.x;
			BallInfo.PosY = value.y;
			_updated = true;
        }
    }

    public class Info { 
		public int PosX;
		public int PosY;
		public Define.BallState State;
		public Define.MoveDir MoveDir;
		public bool Equals(Info info)
        {
			if (ReferenceEquals(info, null))
			{
				return false;
			}
			if (ReferenceEquals(info, this))
			{
				return true;
			}
			if (State != info.State) return false;
			if (MoveDir != info.MoveDir) return false;
			if (PosX != info.PosX) return false;
			if (PosY != info.PosY) return false;
			return false;
		}
	}

	Info _ballInfo = new Info();
    public Info BallInfo
	{
		get { return _ballInfo; }
		set
		{
			if (_ballInfo.Equals(value))
				return;

			CellPos = new Vector3Int(value.PosX, value.PosY, 0);
			State = value.State;
			Dir = value.MoveDir;
		}
	}

	void Start()
	{
		Init();
	}

	void Update()
	{
		UpdateController();
	}

	protected virtual void Init()
	{
		_animator = GetComponent<Animator>();
		_sprite = GetComponent<SpriteRenderer>();
		Vector3 pos = Managers.Map.CurrentGrid.CellToWorld(CellPos) + new Vector3(0.5f, 0.5f);
		transform.position = pos;

		UpdateAnimation();
	}

	protected void UpdateAnimation()
	{
		if (_animator == null || _sprite == null)
			return;

		if (State == Define.BallState.Idle)
		{

		}
		else if (State == Define.BallState.Move)
		{
			switch (Dir)
			{
				case Define.MoveDir.Up:
					_animator.Play("UP");
					_sprite.flipX = false;
					break;
				case Define.MoveDir.Down:
					_animator.Play("Down");
					_sprite.flipX = false;
					break;
				case Define.MoveDir.Left:
					_animator.Play("Left");
					_sprite.flipX = true;
					break;
				case Define.MoveDir.Right:
					_animator.Play("Right");
					_sprite.flipX = false;
					break;
			}
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

	// 키보드 입력
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

	protected void MoveToNextPos()
	{
		if (_moveKeyPressed == false)
		{
			State = Define.BallState.Idle;
			return;
		}

		Vector3Int destPos = CellPos;

		switch (Dir)
		{
			case Define.MoveDir.Up:
				destPos += Vector3Int.up;
				break;
			case Define.MoveDir.Down:
				destPos += Vector3Int.down;
				break;
			case Define.MoveDir.Left:
				destPos += Vector3Int.left;
				break;
			case Define.MoveDir.Right:
				destPos += Vector3Int.right;
				break;
		}

		if (Managers.Map.CanGo(destPos))
		{
			if (Managers.Object.FindCreature(destPos) == null)
			{
				CellPos = destPos;
			}
		}

	}

	// 스르륵 이동하는 것을 처리
	protected virtual void UpdateMove()
	{
		Vector3 destPos = Managers.Map.CurrentGrid.CellToWorld(CellPos) + new Vector3(0.5f, 0.5f);
		Vector3 moveDir = destPos - transform.position;

		// 도착 여부 체크
		float dist = moveDir.magnitude;
		if (dist < _speed * Time.deltaTime)
		{
			transform.position = destPos;
			MoveToNextPos();
		}
		else
		{
			transform.position += moveDir.normalized * _speed * Time.deltaTime;
			State = Define.BallState.Move;
		}
	}
	protected void UpdateExplode() { }

	public virtual void OnExplode()
	{
		State = Define.BallState.Explode;

		GameObject effect = Managers.Resource.Instantiate("Effect/DieEffect");
		effect.transform.position = transform.position;
		effect.GetComponent<Animator>().Play("START");
		GameObject.Destroy(effect, 0.5f);
	}
*/


}
