using Core.Map.GridItem;

namespace BattleMap.Pawn
{
    public class Pawn : GridItem<IPawnData, PawnSO>
    {

        protected override void InitTransformPosition(GridItemOptions options) {
            base.InitTransformPosition(options);
            var adjustedPosition = gameObject.transform.position;
            adjustedPosition.y += 0;
            gameObject.transform.position = adjustedPosition;
        }

    }
}