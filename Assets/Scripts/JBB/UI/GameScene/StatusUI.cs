using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JBB
{
    public class StatusUI : BaseUI
    {
        protected override void Awake()
        {
            base.Awake();
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
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

