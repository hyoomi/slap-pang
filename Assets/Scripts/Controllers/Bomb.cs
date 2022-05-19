using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : BallAndBomb
{
	[SerializeField] Sprite[] images;
	[SerializeField] protected Sprite[] sameImages;

	// 폭탄 타입(주변칸, 같은종류, 좌우, 위아래)
	Define.BombType _type;
	public Define.BombType Type
	{
		get { return _type; }
	}

	// 같은종류 폭탄이 포함할 공 타입
	Define.BallType _ballType;
	public Define.BallType BallType
	{
		get { return _ballType; }
	}

	protected override void Init()
	{
		// 랜덤한 폭탄 타입 선정
		int randomBomb = Random.Range(0, System.Enum.GetValues(typeof(Define.BombType)).Length);
		_type = (Define.BombType)(randomBomb);
		GetComponent<Image>().sprite = images[(int)_type];

		// Same폭탄이라면 랜덤한 공 타입 선정
		if (_type != Define.BombType.Same) return;
		int sameRandom = Random.Range(0, System.Enum.GetValues(typeof(Define.BallType)).Length);
		_ballType = (Define.BallType)(sameRandom);
		GetComponent<Image>().sprite = sameImages[(int)_ballType];
	}

	// 폭탄을 클릭할때 실행 할 함수
    public void OnClickBomb()
    {
		State = Define.BallState.Explode;
	}

    public override void Explode()
    {
		Managers.Action.ClickedBomb(this);
		Space.ballCount--;		
		Destroy(gameObject);	
		Destroy(this);
    }

}
