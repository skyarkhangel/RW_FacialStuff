﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacialStuff
{
    using System.IO;

    using FacialStuff.Graphics;

    using RimWorld;

    using UnityEngine;

    using Verse;
    public class GameComponent_FacialStuff : GameComponent
    {
        public GameComponent_FacialStuff() { }

        public GameComponent_FacialStuff(Game game)
        {
            // Kill the damn beards - xml patching not reliable enough.
            foreach (HairDef hairDef in DefDatabase<HairDef>.AllDefsListForReading)
            {
                CheckReplaceHairTexPath(hairDef);

                if (Controller.settings.UseCaching)
                {
            string name = Path.GetFileNameWithoutExtension(hairDef.texPath);
                    CutHairDB.ExportHairCut(hairDef, name);
                }
            }

        }

        private static List<string> spoonTex = new List<string> { "SPSBeard", "SPSScot", "SPSViking" };

        private static List<string> nackbladTex =
            new List<string> { "bushy", "crisis", "erik", "jr", "guard", "karl", "olof", "ruff", "trimmed" };

        private static void CheckReplaceHairTexPath(HairDef hairDef)
        {
            string folder;
            List<string> collection;
            if (hairDef.defName.Contains("SPS"))
            {
                collection = spoonTex;
                folder = "Spoon/";
            }
            else
            {
                collection = nackbladTex;
                folder = "Nackblad/";
            }

            foreach (string hairname in collection)
            {
                if (!hairDef.defName.Equals(hairname))
                {
                    continue;
                }
                hairDef.texPath = "Hair/" + folder + hairname;
                break;
            }
        }
    }
}
