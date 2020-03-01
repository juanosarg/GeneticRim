using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using RimWorld;
using Verse;
using UnityEngine;

namespace ModCheck
{
    public class isThisModLoaded : PatchOperation
    {
        private string modName;
        private string yourMod;

        protected override bool ApplyWorker(XmlDocument xml)
        {
            if (modName.NullOrEmpty())
            {
                return false;
            }
            return ModsConfig.ActiveModsInLoadOrder.Any(m => m.Name == modName);
        }
    }
}