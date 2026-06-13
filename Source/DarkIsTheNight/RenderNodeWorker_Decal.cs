using Verse;

namespace DarkIsTheNight
{
    public class PawnRenderNodeWorkerDecal : PawnRenderNodeWorker
    {
        public override bool CanDrawNow(PawnRenderNode node, PawnDrawParms parms)
        {
            if (!base.CanDrawNow(node, parms)) return false;
            return parms.flags.FlagSet(PawnRenderFlags.Clothes);
        }
    }

    public class PawnRenderNodeWorkerDecalHead : PawnRenderNodeWorkerDecal { }
}