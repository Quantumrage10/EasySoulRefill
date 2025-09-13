using Modding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UObject = UnityEngine.Object;
using Satchel;
using System.Linq;

namespace EasySoulRefill
{
    public class EasySoulRefill : Mod
    {
        internal static EasySoulRefill Instance;

        public override string GetVersion() => "v1.0.0.2";

        private string[] dream_boss_scene_names = {
            "Dream_01_False_Knight",
            "Dream_02_Mage_Lord",
            "Dream_03_Infected_Knight",
            "Dream_04_White_Defender",
            "Dream_Mighty_Zote",
            "dryya overworld",
            "zemer overworld arena",
            "hegemol overworld arena",
            "isma overworld",
            "Grimm_Nightmare"
            };

        public EasySoulRefill() : base("Easy Soul Refill")
        {
            Instance = this;
        }

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            ModHooks.SetPlayerBoolHook += OnSetBool;
            On.GameManager.OnNextLevelReady += OnSceneLoad;
        }

        public bool OnSetBool(string name, bool orig)
        {
            if ((name == "atBench") && orig)
            {
                Satchel.CoroutineHelper.WaitForSecondsBeforeInvoke(0.5f, RefillSoul);
            }
            return orig;
        }

        
        public void OnSceneLoad(On.GameManager.orig_OnNextLevelReady orig, global::GameManager self)
        {
            orig(self);
            string scene_name = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            if (dream_boss_scene_names.Contains(scene_name))
            {
                Satchel.CoroutineHelper.WaitForSecondsBeforeInvoke(0.5f, RefillSoul);
            }
        }

        public void RefillSoul()
        {
            HeroController.instance.AddMPCharge(200);
        }
    }
}
