using Modding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UObject = UnityEngine.Object;
using Satchel;

namespace EasySoulRefill
{
    public class EasySoulRefill : Mod
    {
        internal static EasySoulRefill Instance;

        //public override List<ValueTuple<string, string>> GetPreloadNames()
        //{
        //    return new List<ValueTuple<string, string>>
        //    {
        //        new ValueTuple<string, string>("White_Palace_18", "White Palace Fly")
        //    };
        //}

        public override string GetVersion() => "v0.1.0.6";

        public EasySoulRefill() : base("FullSoulRespawn")
        {
            Instance = this;
        }

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            //On.GameManager.OnNextLevelReady += OnSceneLoad;
            On.HeroController.Update += OnHeroUpdate;
            On.HeroController.Respawn += OnRespawn;
        }

        public IEnumerator OnRespawn(On.HeroController.orig_Respawn orig, global::HeroController self)
        {
            IEnumerator ienum = orig(self);
            Log("Respawned");
            Satchel.CoroutineHelper.WaitForSecondsBeforeInvoke(0.5f, RefillSoul);

            return ienum;
        }

        /*public void OnSceneLoad(On.GameManager.orig_OnNextLevelReady orig, global::GameManager self)
        {
            orig(self);
            Log("Scene Loaded");
            if (PlayerData.instance.atBench)
            {
                Log("At Bench");
                RefillSoul();
            }
        }*/

        public void OnHeroUpdate(On.HeroController.orig_Update orig, global::HeroController self)
        {
            orig(self);
            if (Input.GetKeyDown(KeyCode.L))
            {
                RefillSoul();
            }
        }

        public void RefillSoul()
        {
            Log("Refilling");
            HeroController.instance.AddMPCharge(200);
        }
    }
}