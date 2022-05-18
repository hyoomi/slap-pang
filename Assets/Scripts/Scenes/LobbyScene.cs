using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BaseScene을 상속받음
public class LobbyScene : BaseScene
{
    // 부모 class의 virtual함수 Init 재정의
    protected override void Init()
    {
        // 부모 class(BaseScene)의 Init()을 실행
        base.Init();

        SceneType = Define.Scene.Lobby;
    }
    private void Start() 
    {
        Managers.Sound.master.SetFloat("Master", PlayerPrefs.GetFloat("Volume"));//게임 시작할떄 볼륨을 저장된 값으로 설정, SoundManager Init()에서 실행하면 작동하지 않았습니다
    }
}
