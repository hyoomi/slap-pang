using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
		_state = Define.BallState.Idle;
		GetComponent<Image>().sprite = images[(int)Type];
		_anim = GetComponent<Animator>();
	}

	public override void Explode()
    {
		Space.ballCount--;
		_anim.enabled = true;
		_anim.Play(Type.ToString());
		StartCoroutine(DestroyBall());
        Managers.Sound.explodeSound(Type);
	}

	// 애니메이션이 끝나면 Destroy
	IEnumerator DestroyBall()
    {
		while(_anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
			yield return null;
        }
		Destroy(gameObject);
		Destroy(this);
		Managers.Sound.explodeInit();
	}
}
