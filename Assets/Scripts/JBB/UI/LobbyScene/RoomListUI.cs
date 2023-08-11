using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JBB
{
    public class RoomListUI : BaseUI
    {
        [SerializeField] GameObject content;
        Dictionary<string, RoomInfo> roomDic;
        protected override void Awake()
        {
            base.Awake();

            roomDic = new Dictionary<string, RoomInfo>();
        }

        public void UpdateRoomList(List<RoomInfo> roomList)
        {
            RoomButtonUI[] rooms = GetComponentsInChildren<RoomButtonUI>();
            foreach (RoomButtonUI room in rooms)
            {
                GameManager.Pool.Release(room);
            }

            // 룸 리스트 update
            foreach (RoomInfo info in roomList)
            {
                // 방이 사라졌으면
                // photon 에서 제공해주는 roominfo 안에 사라질 예정인 방을 마킹해놓음 + 방이 비공개가 되었을 경우 + 방이 닫혔으면(게임시작)
                if (info.RemovedFromList || !info.IsVisible || !info.IsOpen)
                {
                    // 삭제예정인 방이지만 dic에 추가가 안된경우도 존재 할 수 있음.
                    if (roomDic.ContainsKey(info.Name))
                    {
                        roomDic.Remove(info.Name);
                    }
                    continue;
                }

                // 방이 변경됐으면
                // 이름이 있었던 방일경우 최신으로 갱신 dic내에 존재하는 방이면
                if (roomDic.ContainsKey(info.Name))
                {
                    roomDic[info.Name] = info;
                }
                // 방이 생성됐으면
                else
                {
                    roomDic.Add(info.Name, info);
                }
            }

            // create room list
            // Dic이 초기화되었으면 해당 자료형을 사용해서 새로 방구성
            foreach (RoomInfo info in roomDic.Values)
            {
                RoomButtonUI room = GameManager.Pool.GetUI(GameManager.Resource.Load<RoomButtonUI>("UI/RoomButton"));
                room.transform.SetParent(content.transform, false);
                room.SetRoomInfo(info);
            }
        }
    }
}

