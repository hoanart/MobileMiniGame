# Mobile_2D
 
## 개요

플레이어가 오브젝트에 부딪혀 점수를 올리는 미니게임이다.

## 플레이 방식

- 아래의 오브젝트를 터치를 하여 플레이어를 오브젝트의 위치로 이동.

![image](https://github.com/hoanart/MobileMiniGame/assets/56676158/6f6ec267-e190-42fe-9b8b-d7eef9f69d24)


- 부딪히면 점수 상승
- 해당 오브젝트가 많이 생성되면, 게이지 상승으로 인해 게임 종료.

## 사용 기술

- navmeshplus를 활용하여, 터치된 오브젝트 위치로 찾아가는 플레이어 AI를 구현.
- GameManager를 싱글톤으로 구현하여, 전체적인 게임플레이 제어.
- 여러 모바일 화면에 대응할 수 있도록, 게임오브젝트에  앵커 기능 제작.
- InputSystem을 활용하여 터치 입력 구현.

## 결과 화면


https://github.com/hoanart/MobileMiniGame/assets/56676158/2eef78c7-2edb-4061-bc8b-7e3b4e2d3f34

