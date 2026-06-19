using UnitedFront.Decal;
using UnityEngine;
using Verse;

namespace UnitedFront.Render
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
            var decalProps = Props as PawnRenderNodePropertiesDecal;

            DecalProfile profile = DecalUtil.ReadProfileFrom(pawn, _slot);
            string path = profile.Active ? profile.SymbolPath : GetDefaultPath(pawn);
            Color color = profile.Active ? profile.SymbolColor : (decalProps?.Color ?? new Color(0.2f, 0.2f, 0.2f));

            if (path.NullOrEmpty()) return null;

            if (decalProps?.appendBodyType == true && pawn.story?.bodyType != null)
                path = path + "_" + pawn.story.bodyType.defName;

            return GraphicDatabase.Get<Graphic_Multi>(path, ShaderDatabase.Cutout, Vector2.one, color);
        }

        private static DecalSlot DetermineSlot(PawnRenderNodeProperties props)
        {
            if (props is PawnRenderNodePropertiesDecal decalProps && decalProps.ExplicitSlot.HasValue)
                return decalProps.ExplicitSlot.Value;

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
            if (Props is PawnRenderNodePropertiesDecal decalProps && decalProps.texPaths != null && decalProps.texPaths.Count > 0)
            {
                int seed = pawn.Faction?.loadID ?? pawn.thingIDNumber;
                return decalProps.texPaths[seed % decalProps.texPaths.Count];
            }
            return "";
        }
    }
}