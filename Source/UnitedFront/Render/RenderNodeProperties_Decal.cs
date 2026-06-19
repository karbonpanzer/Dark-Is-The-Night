using UnitedFront.Decal;
using UnityEngine;
using Verse;

namespace UnitedFront.Render
{
    public class PawnRenderNodePropertiesDecal : PawnRenderNodeProperties
    {
        public Color Color = new Color(0.2f, 0.2f, 0.2f);
        public DecalSlot? ExplicitSlot = null;
        public bool appendBodyType = false;

        public PawnRenderNodePropertiesDecal()
        {
            nodeClass = typeof(PawnRenderNodeDecal);
            workerClass = typeof(PawnRenderNodeWorkerDecal);
        }
    }
}