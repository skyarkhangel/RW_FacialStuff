﻿// ReSharper disable StyleCop.SA1401

using RimWorld;
using UnityEngine;
using Verse;

namespace FacialStuff
{
    public class Settings : ModSettings
    {
        #region Private Fields

        private bool _filterHats = true;

        private bool _hideHatInBed = true;

        private bool _hideHatWhileRoofed;

        private bool _hideShellWhileRoofed = true;
        private bool _ignoreRenderBody;
        private bool _ignoreWhileDrafted;
        private MaxLayerToShow _layerInBed = MaxLayerToShow.OnSkin;
        private MaxLayerToShow _layerInOwnedBed = MaxLayerToShow.Naked;
        private MaxLayerToShow _layerInPrivateRoom = MaxLayerToShow.OnSkin;
        private MaxLayerToShow _layerInRoom = MaxLayerToShow.Middle;
        private bool _makeThemBlink = true;

        private bool _mergeHair = true;

        private bool _sameBeardColor;
        private bool _showBodyChange;

        private bool _showExtraParts = true;

        private bool _showGenderAgeChange;

        private bool _useCaching;

        private bool _useHeadRotator = true;
        private bool _useMouth = true;
        private bool _useNastyGrin;
        private bool _useWeirdHairChoices = true;

        private bool _useWrinkles = true;

        public bool UseHands => this._useHands;
        public bool UseFeet => this._useFeet;

        private bool _useHands = true;

        private bool _useFeet = true;

        #endregion Private Fields

        #region Public Properties

        public bool FilterHats
        {
            get => this._filterHats;
            set
            {
                this._filterHats = value;
                PortraitsCache.Clear();
            }
        }

        public bool HideHatInBed => this._hideHatInBed;

        public bool HideHatWhileRoofed => this._hideHatWhileRoofed;

        public bool HideShellWhileRoofed
        {
            get
            {
                return this._hideShellWhileRoofed;
            }
        }
        public bool IgnoreRenderBody => this._ignoreRenderBody;
        public bool IgnoreWhileDrafted => this._ignoreWhileDrafted;
        public MaxLayerToShow LayerInBed => this._layerInBed;
        public MaxLayerToShow LayerInOwnedBed => this._layerInOwnedBed;
        public MaxLayerToShow LayerInPrivateRoom => this._layerInPrivateRoom;
        public MaxLayerToShow LayerInRoom => this._layerInRoom;
        public bool MakeThemBlink => this._makeThemBlink;
        public bool MergeHair => this._mergeHair;
        public bool SameBeardColor => this._sameBeardColor;
        public bool ShowBodyChange => this._showBodyChange;

        public bool ShowExtraParts => this._showExtraParts;

        public bool ShowGenderAgeChange => this._showGenderAgeChange;

        public bool UseCaching => this._useCaching;

        public bool UseDnaByFaction { get; } = false;
        public bool UseFreeWill { get; }

        public bool UseHeadRotator => this._useHeadRotator;
        public bool UseMouth => this._useMouth;
        public bool UseNastyGrin => this._useNastyGrin;
        public bool UseWeirdHairChoices => this._useWeirdHairChoices;

        public bool UseWrinkles => this._useWrinkles;

        #endregion Public Properties

        #region Public Methods

        public void DoWindowContents(Rect inRect)
        {
            Rect rect = inRect.ContractedBy(15f);
            Listing_Standard list = new Listing_Standard(GameFont.Small) { ColumnWidth = (rect.width / 2) - 17f };

            list.Begin(rect);

            list.Label("Settings.PawnFeaturesLabel".Translate());
            list.GapLine();

            list.CheckboxLabeled(
                "Settings.UseHeadRotator".Translate(),
                ref this._useHeadRotator,
                "Settings.UseHeadRotatorTooltip".Translate());
            list.CheckboxLabeled(
                "Settings.MakeThemBlink".Translate(),
                ref this._makeThemBlink,
                "Settings.MakeThemBlinkTooltip".Translate());
            list.CheckboxLabeled(
                "Settings.UseMouth".Translate(),
                ref this._useMouth,
                "Settings.UseMouthTooltip".Translate());
            list.CheckboxLabeled(
                "Settings.UseWrinkles".Translate(),
                ref this._useWrinkles,
                "Settings.UseWrinklesTooltip".Translate());
            list.CheckboxLabeled(
                "Settings.MergeHair".Translate(),
                ref this._mergeHair,
                "Settings.MergeHairTooltip".Translate());
            list.CheckboxLabeled(
                "Settings.SameBeardColor".Translate(),
                ref this._sameBeardColor,
                "Settings.SameBeardColorTooltip".Translate());

            // list.CheckboxLabeled(
            // "Settings.HideHatWhileRoofed".Translate(),
            // ref this.hideHatWhileRoofed,
            // "Settings.HideHatWhileRoofedTooltip".Translate());
            // list.CheckboxLabeled(
            // "Settings.HideHatInBed".Translate(),
            // ref this.hideHatInBed,
            // "Settings.HideHatInBedTooltip".Translate());
            list.CheckboxLabeled(
                "Settings.ShowExtraParts".Translate(),
                ref this._showExtraParts,
                "Settings.ShowExtraPartsTooltip".Translate());
            list.CheckboxLabeled(
                "Settings.UseWeirdHairChoices".Translate(),
                ref this._useWeirdHairChoices,
                "Settings.UseWeirdHairChoicesTooltip".Translate());
            list.CheckboxLabeled(
                "Settings.UseNastyGrin".Translate(),
                ref this._useNastyGrin,
                "Settings.UseNastyGrinTooltip".Translate());

            list.Gap();
            list.Label("Settings.HandsFeetLabel".Translate());
            list.GapLine();
            list.CheckboxLabeled(
                "Settings.UseHands".Translate(),
                ref this._useHands,
                "Settings.UseHandsTooltip".Translate());
            list.CheckboxLabeled(
                "Settings.UseFeet".Translate(),
                ref this._useFeet,
                "Settings.UseFeetTooltip".Translate());

            list.Gap();
            list.Label("Settings.EditorLabel".Translate());
            list.GapLine();
            list.CheckboxLabeled("FacialStuffEditor.ShowBodyChange".Translate(), ref this._showBodyChange);
            list.CheckboxLabeled("FacialStuffEditor.ShowGenderChange".Translate(), ref this._showGenderAgeChange);

            list.Gap();
            list.Label("Settings.ExperimentalLabel".Translate());
            list.GapLine();
            list.CheckboxLabeled(
                "Settings.UseCaching".Translate(),
                ref this._useCaching,
                "Settings.UseCachingTooltip".Translate());

            list.NewColumn();

            list.Label("Settings.VisibilityHeadgearLabel".Translate());
            list.GapLine();



            list.Gap();
            list.Label("Hats");

            list.CheckboxLabeled(
                "Settings.HideHatWhileRoofed".Translate(),
                ref this._hideHatWhileRoofed,
                "Settings.HideHatWhileRoofedTooltip".Translate());

            if (!this.HideHatWhileRoofed)
            {
                list.CheckboxLabeled(
                    "Settings.HideHatInBed".Translate(),
                    ref this._hideHatInBed,
                    "Settings.HideHatInBedTooltip".Translate());
            }

            list.CheckboxLabeled(
                "Settings.FilterHats".Translate(),
                ref this._filterHats,
                "Settings.FilterHatsTooltip".Translate());


            list.Gap();
            list.Label("Body");

            list.CheckboxLabeled(
                "Settings.HideShellWhileRoofed".Translate(),
                ref this._hideShellWhileRoofed,
                "Settings.HideShellWhileRoofedTooltip".Translate());

            if (this.HideShellWhileRoofed)
            {
                list.Label("Apparel layers to show:");
                list.LabelDouble("In a room: ", this.LayerInRoom.ToString());
                this._layerInRoom = (MaxLayerToShow)list.Slider((float) this._layerInRoom, 0, 2);

                list.LabelDouble("In private room: ", this.LayerInPrivateRoom.ToString());
                this._layerInPrivateRoom = (MaxLayerToShow)list.Slider((float) this._layerInPrivateRoom, 0, 2);
            }

            list.Gap();
            list.Label("Visibility Of Bodies");
            list.GapLine();

            list.CheckboxLabeled(
                "Settings.IgnoreRenderBody".Translate(),
                ref this._ignoreRenderBody,
                "Settings.IgnoreRenderBodyTooltip".Translate());

            if (this.IgnoreRenderBody)
            {
                if (this.HideShellWhileRoofed)
                {
                    list.LabelDouble("Apparel layers in bed: ", this.LayerInBed.ToString());
                    this._layerInBed = (MaxLayerToShow)list.Slider((float) this._layerInBed, 0, 2);
                    list.LabelDouble("Apparel layers in owned bed: ", this.LayerInOwnedBed.ToString());
                    this._layerInOwnedBed = (MaxLayerToShow)list.Slider((float) this._layerInOwnedBed, 0, 2);
                }
            }

            if (this.HideHatWhileRoofed || this.HideShellWhileRoofed)
            {
                list.GapLine(24f);
                list.CheckboxLabeled(
                    "Settings.IgnoreWhileDrafted".Translate(),
                    ref this._ignoreWhileDrafted,
                    "Settings.IgnoreWhileDraftedTooltip".Translate());
            }

            // if (list.ButtonText("Reset"))
            // {
            // Controller.settings = new Settings();
            // }


            // list.CheckboxLabeled(
            // "Settings.UseFreeWill".Translate(),
            // ref this.useFreeWill,
            // "Settings.UseFreeWillTooltip".Translate());

            // this.useDNAByFaction = Toggle(this.useDNAByFaction, "Settings.UseDNAByFaction".Translate());
            list.End();

            if (GUI.changed)
            {
                this.Mod.WriteSettings();
            }

            // FlexibleSpace();
            // BeginVertical();
            // if (Button("Settings.Apply".Translate()))
            // {
            // foreach (Pawn pawn in PawnsFinder.AllMapsAndWorld_Alive)
            // {
            // if (pawn.RaceProps.Humanlike)
            // {
            // CompFace faceComp = pawn.TryGetComp<CompFace>();
            // if (faceComp != null)
            // {
            // this.WriteSettings();
            // faceComp.sessionOptimized = false;
            // pawn.PawnDrawer.renderer.graphics.ResolveAllGraphics();
            // }
            // }
            // }
            // }
            // EndVertical();
            // FlexibleSpace();
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref this._hideShellWhileRoofed, "hideShellWhileRoofed", false, true);
            Scribe_Values.Look(ref this._useWrinkles, "useWrinkles", false, true);
            Scribe_Values.Look(ref this._useMouth, "useMouth", false, true);
            Scribe_Values.Look(ref this._useHands, "useHands");
            Scribe_Values.Look(ref this._useFeet, "useFeet");
            Scribe_Values.Look(ref this._mergeHair, "mergeHair", false, true);
            Scribe_Values.Look(ref this._hideHatWhileRoofed, "hideHatWhileRoofed", false, true);
            Scribe_Values.Look(ref this._hideHatInBed, "hideHatInBed", false, true);
            Scribe_Values.Look(ref this._showExtraParts, "showExtraParts", false, true);
            Scribe_Values.Look(ref this._useWeirdHairChoices, "useWeirdHairChoices", false, true);
            Scribe_Values.Look(ref this._filterHats, "filterHats", false, true);
            Scribe_Values.Look(ref this._sameBeardColor, "sameBeardColor", false, true);

            // Scribe_Values.Look(ref this.useDNAByFaction, "useDNAByFaction", false, true);
            Scribe_Values.Look(ref this._makeThemBlink, "makeThemBlink", false, true);
            Scribe_Values.Look(ref this._useCaching, "useCaching", false, true);
            Scribe_Values.Look(ref this._useNastyGrin, "useNastyGrin", false, true);
            Scribe_Values.Look(ref this._useHeadRotator, "useHeadRotator", false, true);
            Scribe_Values.Look(ref this._showBodyChange, "showBodyChange", false, true);
            Scribe_Values.Look(ref this._showGenderAgeChange, "showGenderAgeChange", false, true);

            Scribe_Values.Look(ref this._ignoreWhileDrafted, "ignoreWhileDrafted", false, true);
            Scribe_Values.Look(ref this._ignoreRenderBody, "ignoreRenderBody", false, true);

            Scribe_Values.Look(ref this._layerInRoom, "layerInRoom");
            Scribe_Values.Look(ref this._layerInPrivateRoom, "layerInPrivateRoom");
            Scribe_Values.Look(ref this._layerInBed, "layerInBed");
            Scribe_Values.Look(ref this._layerInOwnedBed, "layerInOwnedBed");
        }

        #endregion Public Methods
    }
}