using Verse;

namespace DeadRinger
{
    public class PawnRenderNodeWorkerApparel : PawnRenderNodeWorker
    {
        public override bool CanDrawNow(PawnRenderNode node, PawnDrawParms parms)
        {
            if (!base.CanDrawNow(node, parms)) return false;
            return parms.flags.FlagSet(PawnRenderFlags.Clothes);
        }
    }

    public class PawnRenderNodeWorkerHeadware : PawnRenderNodeWorkerApparel { }
}