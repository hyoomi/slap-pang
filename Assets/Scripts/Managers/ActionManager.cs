using System;

public class ActionManager
{
	public Action<Define.SlideDir> slideAction; // 슬라이드에 따른 액션
	public Action<Bomb> clickedBomb; // 클릭된 폭탄을 알리는 액션
	public Action<int> comboAction; // 콤보를 알리는 액션
	public Action<int> sectionAction; // 구간 변화를 알리는 액션

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

	public void SlideAction(Define.SlideDir slide)
    {
		// 슬라이드가 입력됐을 때, None이 아니라면 Action 발생
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

	}
}
