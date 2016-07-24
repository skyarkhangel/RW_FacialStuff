﻿using System.Reflection;
using RimWorld;
using UnityEngine;
using Verse;

namespace RW_FacialStuff
{

    public class PawnGraphicSetModded : PawnGraphicSet
    {
#pragma warning disable CS0824 // Konstruktor ist extern markiert
        public extern PawnGraphicSetModded();
#pragma warning restore CS0824 // Konstruktor ist extern markiert

        public PawnGraphicSet graphics;

        public void ResolveAllGraphicsModded()
        {
            ClearCache();
            if (pawn.RaceProps.Humanlike)
            {

                nakedGraphic = GraphicGetter_NakedHumanlike.GetNakedBodyGraphic(pawn.story.BodyType, ShaderDatabase.CutoutSkin, pawn.story.SkinColor);
                rottingGraphic = GraphicGetter_NakedHumanlike.GetNakedBodyGraphic(pawn.story.BodyType, ShaderDatabase.CutoutSkin, RottingColor * pawn.story.SkinColor);
                dessicatedGraphic = GraphicDatabase.Get<Graphic_Multi>("Things/Pawn/Humanlike/HumanoidDessicated", ShaderDatabase.Cutout);

                var pawnSave = MapComponent_FacialStuff.GetCache(pawn);

                if (!pawnSave.optimized)
                    GraphicDatabaseHeadRecordsModded.AddCustomizedHead(pawn, pawn.story.SkinColor, pawn.story.hairColor, pawn.story.HeadGraphicPath);

                headGraphic = GraphicDatabaseHeadRecordsModded.GetModdedHeadNamed(pawn, pawn.story.HeadGraphicPath, pawn.story.SkinColor, pawn.story.hairColor);
                // Original: headGraphic = GraphicDatabaseHeadRecords.GetHeadNamed(this.pawn.story.HeadGraphicPath, this.pawn.story.SkinColor);
                desiccatedHeadGraphic = GraphicDatabaseHeadRecordsModded.GetModdedHeadNamed(pawn, pawn.story.HeadGraphicPath, RottingColor);
                skullGraphic = GraphicDatabaseHeadRecords.GetSkull();
                hairGraphic = GraphicDatabase.Get<Graphic_Multi>(pawn.story.hairDef.texPath, ShaderDatabase.Cutout, Vector2.one, pawn.story.hairColor);
                ResolveApparelGraphics();
                PortraitsCache.Clear();

                Texture2D temptexturefront = new Texture2D(128, 128);
                Texture2D temptextureside = new Texture2D(128, 128);
                Texture2D temptextureback = new Texture2D(128, 128);

                Texture2D newhairfront = new Texture2D(128,128);
                Texture2D newhairside = new Texture2D(128, 128);
                Texture2D newhairback = new Texture2D(128, 128);

                GraphicDatabaseHeadRecordsModded.MakeReadable(hairGraphic.MatFront.mainTexture as Texture2D, ref newhairfront);
                GraphicDatabaseHeadRecordsModded.MakeReadable(hairGraphic.MatSide.mainTexture as Texture2D, ref newhairside);
                GraphicDatabaseHeadRecordsModded.MakeReadable(hairGraphic.MatBack.mainTexture as Texture2D, ref newhairback);

                GraphicDatabaseHeadRecordsModded.MergeHeadWithHair(headGraphic.MatFront.mainTexture as Texture2D, newhairfront, pawn.story.hairColor, ref temptexturefront);
                GraphicDatabaseHeadRecordsModded.MergeHeadWithHair(headGraphic.MatSide.mainTexture as Texture2D, newhairside, pawn.story.hairColor, ref temptextureside);
                GraphicDatabaseHeadRecordsModded.MergeHeadWithHair(headGraphic.MatBack.mainTexture as Texture2D, newhairback, pawn.story.hairColor, ref temptextureback);

                temptexturefront.Compress(true);
                temptextureside.Compress(true);
                temptextureback.Compress(true);

                headGraphic.MatFront.mainTexture = temptexturefront;
                headGraphic.MatSide.mainTexture = temptextureside;
                headGraphic.MatBack.mainTexture = temptextureback;

                Object.DestroyImmediate(newhairfront);
                Object.DestroyImmediate(newhairside);
                Object.DestroyImmediate(newhairback);

                //overwrites the crown type so that manually merged hair looks good again.
          //      pawn.story.crownType = CrownType.Average;

                /*
                for (int j = 0; j < apparelGraphics.Count; j++)
                {
                    if (apparelGraphics[j].sourceApparel.def.apparel.LastLayer == ApparelLayer.Overhead)
                    {
                        // INTERESTING                
                        pawn.Drawer.renderer.graphics.headGraphic = hairGraphic;
                    }
                }
                */

            }
            else
            {
                    PawnKindLifeStage curKindLifeStage = pawn.ageTracker.CurKindLifeStage;
                    if (pawn.gender != Gender.Female || curKindLifeStage.femaleGraphicData == null)
                    {
                        nakedGraphic = curKindLifeStage.bodyGraphicData.Graphic;
                    }
                    else
                    {
                        nakedGraphic = curKindLifeStage.femaleGraphicData.Graphic;
                    }
                    rottingGraphic = nakedGraphic.GetColoredVersion(ShaderDatabase.CutoutSkin, RottingColor, RottingColor);
                    if (curKindLifeStage.dessicatedBodyGraphicData != null)
                    {
                        dessicatedGraphic = curKindLifeStage.dessicatedBodyGraphicData.GraphicColoredFor(pawn);
                    }
                }
            }

        // Verse.PawnGraphicSet
        public GraphicMeshSet HairMeshSetModded
        {
            get
            {
         //       return MeshPool.humanlikeHairSetAverage;

                if (pawn.story.crownType == CrownType.Average)
                {
                    return MeshPool.humanlikeHairSetAverage;
                }
                if (pawn.story.crownType == CrownType.Narrow)
                {
                    return MeshPool.humanlikeHairSetNarrow;
                }
                Log.Error("Unknown crown type: " + pawn.story.crownType);
                return MeshPool.humanlikeHairSetAverage;
            }
        }


    }
}