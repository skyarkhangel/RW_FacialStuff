﻿using RimWorld;
using UnityEngine;
using Verse;

namespace FacialStuff
{
    public class QuadrupedDrawer : HumanBipedDrawer
    {
        public override void DrawFeet(Vector3 rootLoc, bool portrait)
        {
            if (portrait && !this.CompAnimator.AnimatorOpen)
            {
                return;
            }

            // Fix the position, maybe needs new code in GetJointPositions()?
            if (this.BodyFacing == Rot4.South)
            {
                rootLoc.y -= Offsets.YOffset_HandsFeet * 2f - Offsets.YOffset_Behind;
            }

            this.DrawFrontPaws(rootLoc, portrait);
            base.DrawFeet(rootLoc, portrait);
        }

        public virtual void DrawFrontPaws(Vector3 rootLoc, bool portrait)
        {
            if (!this.CompAnimator.Props.quadruped)
            {
                return;
            }

            Vector3 ground = rootLoc;
            ground.z       += OffsetGround;

            // Basic values
            BodyAnimDef body = this.CompAnimator.BodyAnim;

            Rot4 rot = this.BodyFacing;
            if (body == null)
            {
                return;
            }

            JointLister jointPositions = this.GetJointPositions(
                                                                body.shoulderOffsets[rot.AsInt],
                                                                body.shoulderOffsets[Rot4.North.AsInt].x);

            Vector3 rightFootAnim  = Vector3.zero;
            Vector3 leftFootAnim   = Vector3.zero;
            float   footAngleRight = 0f;

            float footAngleLeft = 0f;
            float offsetJoint   = 0;

            WalkCycleDef cycle = this.CompAnimator.WalkCycle;
            if (cycle != null)
            {
                offsetJoint = cycle.ShoulderOffsetHorizontalX.Evaluate(this.MovedPercent);

                // Center = drawpos of carryThing
                this.DoWalkCycleOffsets(
                                        ref rightFootAnim,
                                        ref leftFootAnim,
                                        ref footAngleRight,
                                        ref footAngleLeft,
                                        ref offsetJoint,
                                        cycle.FrontPawPositionX,
                                        cycle.FrontPawPositionZ,
                                        cycle.FrontPawAngle);
            }

            this.GetMeshesFoot(out Mesh footMeshRight, out Mesh footMeshLeft);
#if develop
            {
 }
#else
            {
            }

            Material matLeft;

            Material matRight;
#endif
            if (!MainTabWindow_Animator.Colored)
            {
                switch (rot.AsInt)
                {
                    default:
                        matRight = this.Flasher.GetDamagedMat(this.CompAnimator.PawnBodyGraphic?.FrontPawGraphicRight
                                                                 ?.MatAt(rot));
                        matLeft = this.Flasher.GetDamagedMat(this.CompAnimator.PawnBodyGraphic?.FrontPawGraphicLeft
                                                                ?.MatAt(rot));
                        break;

                    case 1:
                        matRight = this.Flasher.GetDamagedMat(this.CompAnimator.PawnBodyGraphic?.FrontPawGraphicRight
                                                                 ?.MatAt(rot));
                        matLeft = this.Flasher.GetDamagedMat(this.CompAnimator.PawnBodyGraphic
                                                                ?.FrontPawGraphicLeftShadow?.MatAt(rot));
                        break;

                    case 3:
                        matRight = this.Flasher.GetDamagedMat(this.CompAnimator.PawnBodyGraphic
                                                                 ?.FrontPawGraphicRightShadow?.MatAt(rot));
                        matLeft = this.Flasher.GetDamagedMat(this.CompAnimator.PawnBodyGraphic?.FrontPawGraphicLeft
                                                                ?.MatAt(rot));
                        break;
                }
            }
            else
            {
                matRight = this.CompAnimator.PawnBodyGraphic?.FrontPawGraphicRightCol?.MatAt(rot);
                matLeft  = this.CompAnimator.PawnBodyGraphic?.FrontPawGraphicLeftCol?.MatAt(rot);
            }

            float bodyAngle = 0f;

            Pawn pawn = this.Pawn;
            if (pawn.Downed || pawn.Dead)
            {
                bodyAngle = pawn.Drawer.renderer.wiggler.downedAngle;
            }

            if (matRight != null)
            {
                if (this.CompAnimator.BodyStat.FootRight != PartStatus.Missing)
                {
                    GenDraw.DrawMeshNowOrLater(
                                               footMeshRight,
                                               ground.RotatedBy(bodyAngle) + jointPositions.RightJoint +
                                               rightFootAnim,
                                               Quaternion.AngleAxis(bodyAngle + footAngleRight, Vector3.up),
                                               matRight,
                                               portrait);
                }
            }

            if (matLeft != null)
            {
                if (this.CompAnimator.BodyStat.FootLeft != PartStatus.Missing)
                {
                    GenDraw.DrawMeshNowOrLater(
                                               footMeshLeft,
                                               ground.RotatedBy(bodyAngle)    + jointPositions.LeftJoint + leftFootAnim,
                                               Quaternion.AngleAxis(bodyAngle + footAngleLeft, Vector3.up),
                                               matLeft,
                                               portrait);
                }
            }

            if (MainTabWindow_Animator.Develop)
            {
                // for debug
                Material centerMat = GraphicDatabase
                                    .Get<Graphic_Single>("Hands/Ground", ShaderDatabase.Transparent, Vector2.one,
                                                         Color.cyan).MatSingle;

                GenDraw.DrawMeshNowOrLater(
                                           footMeshLeft,
                                           ground.RotatedBy(bodyAngle) + jointPositions.LeftJoint +
                                           new Vector3(offsetJoint, 0.301f, 0),
                                           Quaternion.AngleAxis(0, Vector3.up),
                                           centerMat,
                                           portrait);

                GenDraw.DrawMeshNowOrLater(
                                           footMeshRight,
                                           ground.RotatedBy(bodyAngle) + jointPositions.RightJoint +
                                           new Vector3(offsetJoint, 0.301f, 0),
                                           Quaternion.AngleAxis(0, Vector3.up),
                                           centerMat,
                                           portrait);

                // UnityEngine.Graphics.DrawMesh(handsMesh, center + new Vector3(0, 0.301f, z),
                // Quaternion.AngleAxis(0, Vector3.up), centerMat, 0);
                // UnityEngine.Graphics.DrawMesh(handsMesh, center + new Vector3(0, 0.301f, z2),
                // Quaternion.AngleAxis(0, Vector3.up), centerMat, 0);
            }
        }
    }
}