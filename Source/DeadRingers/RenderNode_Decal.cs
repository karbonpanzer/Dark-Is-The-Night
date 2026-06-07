using UnityEngine;
using Verse;

namespace DeadRinger
{
    public class PawnRenderNodeDecal : PawnRenderNode
    {
        private readonly DecalSlot _slot;

        public PawnRenderNodeDecal(Pawn pawn, PawnRenderNodeProperties props, PawnRenderTree tree)
            : base(pawn, props, tree)
        {
            _slot = DetermineSlot(props);
        }

        public override Graphic? GraphicFor(Pawn pawn)
        {
            var deadRingerProps = Props as PawnRenderNodePropertiesOmni;

            DecalProfile profile = DecalUtil.ReadProfileFrom(pawn, _slot);
            string path = profile.Active ? profile.SymbolPath : GetDefaultPath(pawn);
            Color color = profile.Active ? profile.SymbolColor : (deadRingerProps?.Color ?? new Color(0.2f, 0.2f, 0.2f));

            if (path.NullOrEmpty()) return null;

            if (deadRingerProps?.autoBodyTypePaths == true && pawn.story?.bodyType != null)
                path = path + "_" + pawn.story.bodyType.defName;

            return GraphicDatabase.Get<Graphic_Multi>(path, ShaderDatabase.Cutout, Vector2.one, color);
        }

        private static DecalSlot DetermineSlot(PawnRenderNodeProperties props)
        {
            if (props is PawnRenderNodePropertiesOmni deadRingerProps && deadRingerProps.ExplicitSlot.HasValue)
                return deadRingerProps.ExplicitSlot.Value;

            if (props.parentTagDef != null)
            {
                string tagName = props.parentTagDef.defName;
                if (tagName.Contains("Head") || tagName.Contains("Headgear") || tagName.Contains("Helmet"))
                    return DecalSlot.Helmet;
            }

            return DecalSlot.Armor;
        }

        private string GetDefaultPath(Pawn pawn)
        {
            if (Props is PawnRenderNodePropertiesOmni deadRingerProps && deadRingerProps.texPaths != null && deadRingerProps.texPaths.Count > 0)
            {
                int seed = pawn.Faction?.loadID ?? pawn.thingIDNumber;
                return deadRingerProps.texPaths[seed % deadRingerProps.texPaths.Count];
            }
            return "";
        }
    }
}