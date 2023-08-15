using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace JBB
{
    public class StatusUI : BaseUI
    {

        public static UnityEvent<float> OnHPChanged = new UnityEvent<float>();
        public static UnityEvent<int, int> OnBulletCountChanged = new UnityEvent<int, int>();

        protected override void Awake()
        {
            base.Awake();
        }

        private void OnEnable()
        {
            OnHPChanged.AddListener(SetHP);
            OnBulletCountChanged.AddListener(SetBullets);
        }

        private void OnDisable()
        {
            OnHPChanged.RemoveListener(SetHP);
            OnBulletCountChanged.RemoveListener(SetBullets);
        }
        public void SetWeaponName(string weaponName)
        {
            texts["WeaponNameText"].text = weaponName;
        }

        public void SetHP(float hp)
        {
            texts["HPText"].text = hp.ToString();
        }

        public void SetBullets(int currentBullet, int remainBullet)
        {
            texts["CurrentBullet"].text = currentBullet.ToString();
            texts["RemainBullet"].text = remainBullet.ToString();
        }
    }
}

