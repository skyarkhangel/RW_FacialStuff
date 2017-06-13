﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RW_FacialStuff
{
    using Verse;

    [StaticConstructorOnStartup]
    public static class MeshPoolFs
    {
        public static readonly GraphicMeshSet humanlikeHeadSetAverage;

        public static readonly GraphicMeshSet humanlikeHeadSetNarrow;

        private const float HumanlikeHeadAverageWidth = 1.5f;

        private const float HumanlikeHeadNarrowWidth = 1.3f;

        static MeshPoolFs()
        {
            humanlikeHeadSetAverage = new GraphicMeshSet(HumanlikeHeadAverageWidth);
            humanlikeHeadSetNarrow = new GraphicMeshSet(HumanlikeHeadNarrowWidth, HumanlikeHeadAverageWidth);
        }
    }
}
