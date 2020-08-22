using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Blastphemy
{
    public class BlastphemyModule : ETGModule
    {
        public override void Init()
        {
        }

        public override void Start()
        {
            Gun blasphemy = (Gun)ETGMod.Databases.Items["blasphemy"];
            Gun d = (Gun)ETGMod.Databases.Items["excaliber_green"];
            ETGModConsole.Log("switch: " + d.gunSwitchGroup + ", override fire sound: " + d.OverrideNormalFireAudioEvent);
            blasphemy.HeroSwordDoesntBlank = true;
            blasphemy.gameObject.AddComponent<BlastphemyBehaviour>();
        }

        public override void Exit()
        {
        }

        internal class BlastphemyBehaviour : GunBehaviour
        {
            public void Update()
            {
                if(this.CustomSwordCooldown > 0f)
                {
                    this.CustomSwordCooldown -= BraveTime.DeltaTime;
                }
            }

            public override void PostProcessProjectile(Projectile projectile)
            {
                projectile.baseData.damage /= 2;
            }

            public override void OnPostFired(PlayerController player, Gun gun)
            {
                if(this.CustomSwordCooldown <= 0)
                {
                    gun.ForceFireProjectile(gun.DefaultModule.GetCurrentProjectile());
                    this.CustomSwordCooldown = 0.6f;
                }
            }

            private float CustomSwordCooldown = 0;
        }
    }
}
