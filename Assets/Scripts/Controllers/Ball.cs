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

	// 공 타입(컵, 잔, 화분, 접시, 돌)
	Define.BallType _type;
	public Define.BallType Type
	{
		get { return _type; }
	}

	// 랜덤한 공 이미지를 설정, 변수 초기화
	protected override void Init()
	{
		int random = UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(Define.BallType)).Length);
		_type = (Define.BallType)(random);		
		State = Define.BallState.Idle;
		GetComponent<Image>().sprite = images[(int)Type];
	}

	public override void Explode()
    {
		Space.ballCount--;
		Destroy(gameObject);
		Destroy(this);
	}
}
