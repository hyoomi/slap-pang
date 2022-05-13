using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : BallAndBomb
{
	[SerializeField] Sprite[] images;

	Define.BombType _type;
	public Define.BombType Type
	{
		get { return _type; }
	}

	protected override void Init()
	{
		int random = UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(Define.BombType)).Length);
		_type = (Define.BombType)(random);

		GetComponent<Image>().sprite = images[(int)Type];
	}

    public void OnClickBomb()
    {
		State = Define.BallState.Explode;
	}

    public override void Explode()
    {
		Managers.Action.ClickedBomb = this;
		Debug.Log(CellIndex + " Bomb Explode!");
		Space.ballCount--;		
		Destroy(gameObject);	
		Destroy(this);
    }

}
