using UnityEngine;
using Verse;

namespace DeadRinger
{
    public class PawnRenderNodePropertiesOmni : PawnRenderNodeProperties
    {
        public Color Color = new Color(0.2f, 0.2f, 0.2f);
        public DecalSlot? ExplicitSlot = null;
        public bool autoBodyTypePaths = false;

        public PawnRenderNodePropertiesOmni()
        {
            nodeClass = typeof(PawnRenderNodeDecal);
            workerClass = typeof(PawnRenderNodeWorkerApparel);
        }
    }
}