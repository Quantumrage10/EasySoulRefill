using Modding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UObject = UnityEngine.Object;
using Satchel;
using System.Drawing.Printing;

namespace EasySoulRefill
{
    public class EasySoulRefill : Mod
    {
        internal static EasySoulRefill Instance;

        public override string GetVersion() => "v0.1.1.0";

        public EasySoulRefill() : base("FullSoulRespawn")
        {
            Instance = this;
        }

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            ModHooks.SetPlayerBoolHook += OnSetBool;
        }

        public bool OnSetBool(string name, bool orig)
        {
            if ((name == "atBench") && orig)
            {
                Log("Benched");
                RefillSoul();
            }
            return orig;
        }

        public void RefillSoul()
        {
            Log("Refilling");
            HeroController.instance.AddMPCharge(200);
        }
    }
}