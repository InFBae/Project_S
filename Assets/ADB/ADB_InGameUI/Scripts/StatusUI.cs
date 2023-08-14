using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace ahndabi
{
    public class StatusUI : SceneUI
    {
        // ���⿡ ����Ƽ�̺�Ʈ ���� ����
        // 1. Hp �̺�Ʈ   2. Kill �̺�Ʈ   3. ź�� �̺�Ʈ
        public static UnityEvent<float> OnHPChanged = new UnityEvent<float>();
        public static UnityEvent<int, int> OnBulletCountChanged = new UnityEvent<int, int>();     // curAvailavleBullet / remainBullet

        protected override void Awake()
        {
            base.Awake();
        }

        private void OnEnable()
        {
            OnHPChanged.AddListener(SetHP);
            OnBulletCountChanged.AddListener(SetBullets);
        }

        public void SetWeaponName(string weaponName)
        {
            texts["WeaponNameText"].text = weaponName;
        }

        public void SetHP(float hp)
        {
            texts["HPValue"].text = hp.ToString();
        }

        public void SetBullets(int currentBullet, int remainBullet)
        {
            texts["CurrentBullet"].text = currentBullet.ToString();
            texts["RemainBullet"].text = remainBullet.ToString();
        }

    }
}