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

            // �� ����Ʈ update
            foreach (RoomInfo info in roomList)
            {
                // ���� ���������
                // photon ���� �������ִ� roominfo �ȿ� ����� ������ ���� ��ŷ�س��� + ���� ������� �Ǿ��� ��� + ���� ��������(���ӽ���)
                if (info.RemovedFromList || !info.IsVisible || !info.IsOpen)
                {
                    // ���������� �������� dic�� �߰��� �ȵȰ�쵵 ���� �� �� ����.
                    if (roomDic.ContainsKey(info.Name))
                    {
                        roomDic.Remove(info.Name);
                    }
                    continue;
                }

                // ���� ���������
                // �̸��� �־��� ���ϰ�� �ֽ����� ���� dic���� �����ϴ� ���̸�
                if (roomDic.ContainsKey(info.Name))
                {
                    roomDic[info.Name] = info;
                }
                // ���� ����������
                else
                {
                    roomDic.Add(info.Name, info);
                }
            }

            // create room list
            // Dic�� �ʱ�ȭ�Ǿ����� �ش� �ڷ����� ����ؼ� ���� �汸��
            foreach (RoomInfo info in roomDic.Values)
            {
                RoomButtonUI room = GameManager.Pool.GetUI(GameManager.Resource.Load<RoomButtonUI>("UI/RoomButton"));
                room.transform.SetParent(content.transform, false);
                room.SetRoomInfo(info);
            }
        }
    }
}

