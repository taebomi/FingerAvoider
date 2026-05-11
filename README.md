# FingerAvoider

> 피하기 어드벤처 | Unity 2D, C# | 1인 | Android

[![플레이 영상](https://img.youtube.com/vi/I_hj-IzfqiQ/0.jpg)](https://youtu.be/I_hj-IzfqiQ)

---

## 기술 스택

- **Unity 2D, C#**
- **DOTween** — 카메라 전환, 오브젝트 연출
- **AdMob · Unity Ads** — 전면·배너 광고
- **GPGS** — 리더보드, 업적

---

## 핵심 구현

| 파일 | 역할 |
|------|------|
| [GamePlay.cs](System/System/GamePlay.cs) | 스테이지 타이머, 메달 판정, 클리어/게임오버 |
| [RhythmStage.cs](Stage/RhythmStage.cs) | 리듬게임 특수 스테이지 |
| [DodgeMode.cs](System/CompetitivePlay/DodgeMode.cs) | 닷지 특수 스테이지 |
| [CharacterScript.cs](Character/CharacterScript.cs) | 터치 델타 이동, 감도 조절 |
| [GameSystem.cs](System/System/GameSystem.cs) | 싱글톤, 씬 전환·오디오·설정 전역 관리 |

### 특수 스테이지 기믹

각 스테이지 세트의 마지막(15번·25번)은 일반 피하기와 다른 규칙으로 변주.

- **리듬게임** — 노트 타임라인에 맞춰 폭발 위치를 피함 (`RhythmStage.cs`)
- **닷지** — 무작위 방향에서 날아오는 적을 피해 생존 (`Stage2_5.cs`)
- **폭탄 피하기** — 패턴에 따라 순서대로 터지는 폭발을 타이밍에 맞춰 회피 (`ExplosionManager.cs`)

### 메달 시스템

클리어 시간에 따라 타이머 색상·아이콘이 실시간으로 변경 (동→은→금→개발자 4단계).
신기록 달성 시 GPGS 리더보드에 자동 등록.

---

## 코드 구조

```
├── System/System/
│   ├── GameSystem.cs       # 싱글톤, 씬 전환·오디오·설정 전역 관리
│   ├── GamePlay.cs         # 스테이지 타이머·메달·클리어/게임오버
│   └── StoryManager.cs     # 스토리 컷신 동적 로딩
├── Character/
│   └── CharacterScript.cs  # 터치 델타 이동, 감도 조절
├── Stage/
│   └── RhythmStage.cs      # 리듬게임 특수 스테이지
├── System/CompetitivePlay/
│   └── DodgeMode.cs        # 닷지 특수 스테이지
├── Boss/                   # 보스 행동 패턴
└── Object/Obstacle/        # 장애물 이동 패턴 모음
```
