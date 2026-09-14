# Turbo Lap 🏁

**자동으로 가속하는 차량을 조종해 트랙을 완주하고, 자신의 최고 기록에 도전하는 Unity 3D 레이싱 게임입니다.**

좌우 조향으로 코너를 공략하고 체크포인트를 통과하며 레이스를 진행합니다. 완주 후에는 소요 시간, 최고 속도, 벽 충돌 횟수를 확인할 수 있습니다.

## 주요 기능

- **자동 가속과 물리 기반 조향** — Rigidbody를 이용한 전진, 회전, 횡방향 미끄러짐 제어
- **키보드 및 화면 버튼 입력** — A/D 또는 방향키와 누르고 있는 동안 동작하는 조향 UI
- **체크포인트와 랩 판정** — 체크포인트 통과 후 결승선을 지나면 랩을 인정하고, 목표 랩에 도달하면 레이스 종료
- **체크포인트 복귀** — R 키로 마지막에 저장된 체크포인트의 위치와 방향으로 복귀
- **레이스 연출** — 시작 카운트다운, 엔진 사운드, 완주 표시, 페이드 전환
- **주행 기록** — 완주 시간, 개인 최고 기록, 최고 속도, 벽 충돌 횟수를 결과 화면에 표시
- **차량·맵 미리보기** — 준비 화면에서 이름과 이미지를 순환하며 확인

> 차량·맵 선택은 현재 미리보기 UI 단계입니다. `SelectionManager`에는 선택한 항목을 게임 씬의 차량이나 트랙에 적용하는 로직이 포함되어 있지 않습니다.

## 기술 스택

| 항목 | 구성 |
| --- | --- |
| 엔진 | Unity 6000.3.10f1 |
| 언어 | C# |
| 렌더링 | Universal Render Pipeline 17.3.0 |
| 입력 | Input System 1.18.0 및 기존 Input API |
| UI | Unity UI, TextMesh Pro |
| 물리 | Rigidbody, Collider |
| 기록 저장 | PlayerPrefs |

버전 정보는 `ProjectSettings/ProjectVersion.txt`와 `Packages/manifest.json`을 기준으로 합니다.

## 실행 방법

1. Unity Hub에서 **Unity 6000.3.10f1**을 설치합니다.
2. 저장소를 내려받습니다.

   ```bash
   git clone https://github.com/Hye-Hyun/Turbo-Lap.git
   ```

3. Unity Hub에서 **Add → Add project from disk**로 `Turbo-Lap` 폴더를 추가합니다.
4. 프로젝트를 열고 패키지 복원과 에셋 임포트가 완료될 때까지 기다립니다.
5. `Assets/Title Scene.unity`를 열고 에디터의 **Play** 버튼을 누릅니다.
6. 타이틀 화면을 클릭하거나 터치해 준비 화면으로 이동한 뒤 게임을 시작합니다.

### 입력 설정

현재 프로젝트는 새 Input System과 기존 Input API를 함께 사용합니다. 입력 관련 오류가 발생하면 **Edit → Project Settings → Player → Other Settings → Active Input Handling**이 **Both**인지 확인합니다. 저장소의 설정도 Both로 지정되어 있습니다.

### 빌드 씬

`File → Build Profiles`에서 다음 씬이 순서대로 활성화되어 있는지 확인합니다. 저장소의 `EditorBuildSettings.asset`에도 이 순서로 등록되어 있습니다.

| 순서 | 씬 | 역할 |
| --- | --- | --- |
| 1 | `Assets/Title Scene.unity` | 타이틀 및 진입 화면 |
| 2 | `Assets/Ready Scene.unity` | 차량·맵 미리보기 및 레이스 시작 |
| 3 | `Assets/Game Scene.unity` | 주행, 체크포인트, 랩 및 시간 측정 |
| 4 | `Assets/Result Scene.unity` | 주행 결과 및 최고 기록 표시 |

## 조작 방법

| 입력 | 동작 |
| --- | --- |
| A / ← | 왼쪽 조향 |
| D / → | 오른쪽 조향 |
| 화면의 좌우 조향 버튼 누르기 | 해당 방향으로 조향 |
| R | 마지막에 저장된 체크포인트로 복귀 |
| 타이틀 화면 클릭 / 터치 | 준비 화면으로 이동 |

차량은 카운트다운이 끝나면 자동으로 가속합니다. 별도의 가속·브레이크·후진 키는 구현되어 있지 않으며, 저장된 체크포인트가 없으면 R 키 복귀가 동작하지 않습니다.

## 게임 진행

```text
Title Scene → Ready Scene → Game Scene → Result Scene
                               ↑              │
                               └── 다시 시작 ─┘
```

1. 준비 화면에서 레이스를 시작합니다.
2. 카운트다운이 끝나면 차량 이동과 시간 측정이 시작됩니다.
3. 체크포인트를 통과한 뒤 결승선을 지나 랩을 완료합니다.
4. 목표 랩에 도달하면 기록을 저장하고 결과 화면으로 이동합니다.
5. 결과 화면에서 다시 주행하거나 준비 화면으로 돌아갈 수 있습니다.

목표 랩 수는 `RaceManager.targetLap`으로 관리하며, 코드의 기본값은 **2랩**입니다. 실제 씬에서는 Inspector에 저장된 값이 적용됩니다.

## 기록 시스템

레이스 종료 시 다음 정보를 `PlayerPrefs`에 기록합니다.

| 키 | 내용 |
| --- | --- |
| `FinalTime` | 마지막으로 완주한 레이스의 총 소요 시간 |
| `BestTime` | 가장 짧은 레이스 완주 시간 |
| `TopSpeed` | 해당 레이스에서 측정한 최고 속도(km/h) |
| `CollisionCount` | `Wall` 태그를 가진 오브젝트와의 충돌 횟수 |

시간은 `분:초.소수점 두 자리` 형식으로 표시합니다. 최고 기록은 개별 랩이 아닌 **레이스 전체 완주 시간**을 기준으로 비교하며, 차량·맵별 구분 없이 하나의 `BestTime` 키를 사용합니다.

## 프로젝트 구조

```text
Turbo-Lap/
├── Assets/
│   ├── Title Scene.unity
│   ├── Ready Scene.unity
│   ├── Game Scene.unity
│   ├── Result Scene.unity
│   ├── CarController.cs        # 차량 물리, 입력, 체크포인트 복귀
│   ├── RaceManager.cs          # 랩과 주행 통계 관리
│   ├── RaceStartUI.cs          # 카운트다운과 레이스 사운드
│   ├── RaceTimer.cs            # 주행 시간 측정
│   ├── CheckPoint.cs           # 체크포인트 통과 판정
│   ├── FinishLine.cs           # 랩 완료, 기록 저장, 결과 씬 전환
│   ├── ResultUI.cs             # 결과 표시
│   ├── SelectionManager.cs     # 차량·맵 미리보기
│   ├── UI_TITLE/              # 화면별 UI 리소스
│   ├── UI_READY/
│   ├── UI_GAME/
│   ├── UI_RESULT/
│   └── ...                    # 모델, 도로, 도시, 오디오 등
├── Packages/                  # 패키지 의존성
└── ProjectSettings/           # 에디터 버전, 입력, 빌드 씬 설정
```

## 개발 시 참고 사항

- **재시작 상태 초기화:** `RaceManager`는 `DontDestroyOnLoad`로 유지되며, 현재 재시작 코드는 게임 씬만 다시 로드합니다. 반복 플레이를 개선하려면 현재 랩, 체크포인트 통과 여부, 최고 속도, 충돌 횟수의 초기화 처리가 필요합니다.
- **속도 관련 설정:** `CarController`에 최고 속도와 후진 관련 필드가 선언되어 있지만, 현재 가속 로직에는 속도 제한과 후진 제어가 연결되어 있지 않습니다.
- **디버그 출력:** 차량 스크립트에는 매 물리 프레임 실행되는 로그가 포함되어 있어, 배포 전에 정리할 수 있습니다.

## 에셋

저장소에는 Race Car Package, Simple Roads, Urban Skyscrapers, Versatile Studio Assets 및 차량·음악 관련 리소스가 포함되어 있습니다. 개별 에셋의 사용 및 재배포 조건은 각 원본 에셋의 라이선스를 따릅니다.
