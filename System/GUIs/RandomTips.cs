using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RandomTips : MonoBehaviour {
    string[,] s =
    {
        {"개발자의 최고기록을 깨면 얻을 수 있는 개발자 메달도 존재합니다!", "there is a developer medal that can be won by breaking the developer's record!"},
        {"리뷰를 써주시면 개발자가 기뻐해요!","The developer is pleased if you write a review!" },
        {"어려워서 클리어가 힘들면 상점이 업데이트 될 때 까지 기다리세요...", "If it is difficult to clear, please wait until the store is updated ..." },
        {"개발자는 곧 군대를 가요!","The developer will soon go to the army"},
        {"군대가기 싫어요...", "I don't want to go army..." },
        {"리뷰좀 써주세요...", "Review my App please..." },
        {"2019년에 만나요!", "See you in 2019!" },
        {">w<",">w<" },
        {"시작 전 진행 방향을 생각하고 손가락을 올리세요!" ,"Before you start, consider your direction and raise your finger!"},
        {"개발자는 롤 다이아 등급이에요", "Developer's LOL tier is Diamond" },
        {"할 말이 없네요", "I have nothing to say" },
        {"로딩이 빨라서 뭐라는 지 안보이죠? 중요한 말은 없어요","Don't you see what is written beacuseloading is fast? There is nothing important" },
        {":D",":D" }
    };
    private void Start()
    {
        if(GameSystem.instance.isKorean)
            GetComponent<Text>().text = s[Random.Range(0, 13), 0];
        else
            GetComponent<Text>().text = s[Random.Range(0, 13), 1];
    }
}
