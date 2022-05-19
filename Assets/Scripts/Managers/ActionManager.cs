using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager
{
	public Action<Define.SlideAction> slideAction; // 슬라이드에 따른 액션
	public Action<Define.BallState> ballsAction; // 전체 공 상태에 따른 액션
	public Action<Bomb> clickedBomb; // 클릭된 폭탄을 알리는 액션
	public Action<int> comboAction; // 콤보를 알리는 액션
	public Action<int> sectionAction; // 구간 변화를 알리는 액션

	public Define.SlideAction _slide; // 지금 발생중인 슬라이드 행위를 체크
	public Define.SlideAction Slide
	{
		get { return _slide; }
		set
		{
			// 슬라이드가 입력됐을 때, None이 아니라면 Action 발생
			if (value != Define.SlideAction.None) slideAction?.Invoke(value);
			if (_slide == value)
				return;
			_slide = value;
		}
	}

	Define.BallState _ballsAction;
	public Define.BallState BallsAction
	{
		get { return _ballsAction; }
		set
		{
			if (_ballsAction == value)
				return;
			_ballsAction = value;
			ballsAction?.Invoke(_ballsAction);
		}
	}

	// 폭탄을 클릭했을때, 클릭한 폭탄을 알린다
	public void ClickedBomb(Bomb bomb)
	{
		clickedBomb?.Invoke(bomb);
	}

	// 콤보가 입력됐을때, 몇 콤보인지 알린다
	public void ComboAction(int combo)
    {
		comboAction?.Invoke(combo);
    }

	// 구간이 변했을때, 어떤 구간인지 알린다
	public void SectionAction(int section)
	{
		sectionAction?.Invoke(section);
	}

	public void Init()
    {
		_slide = Define.SlideAction.None;
		_ballsAction = Define.BallState.Idle;
	}
}
