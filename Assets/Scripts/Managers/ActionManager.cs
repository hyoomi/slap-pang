using System;

public class ActionManager
{
	public Action<Define.SlideDir> slideAction; // 슬라이드에 따른 액션
	public Action<Bomb> clickedBomb; // 클릭된 폭탄을 알리는 액션
	public Action<int> comboAction; // 콤보를 알리는 액션
	public Action<int> sectionAction; // 구간 변화를 알리는 액션
	public bool popupAction; // 팝업이 떴다는 것을 감지

	Define.GameState _gameState;
	public Define.GameState GameState
    {
		get { return _gameState; }
		set 
		{
			if (_gameState == value)
				return;
			_gameState = value;
		}
    }

	Define.GameState _pastState;
	public Define.GameState PastState
	{
		get { return _pastState; }
		set
		{
			if (_pastState == value)
				return;
			_pastState = value;
		}
	}

	public void SlideAction(Define.SlideDir slide)
    {
		if (popupAction) return;
		if (slide != Define.SlideDir.None) slideAction?.Invoke(slide);
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
		popupAction = false;
	}
}
