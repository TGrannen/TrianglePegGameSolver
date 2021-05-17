namespace LegacyTrianglePegGame
{
    public class LegacyPegMove
    {
        public LegacyPegLocation fromLocation;
        public LegacyPegLocation middleLocation;
        public LegacyPegLocation toLocation;

        public LegacyPegHole CalcMiddle()
        {
            LegacyPegHole temp = new LegacyPegHole();
            temp.row = fromLocation.location.row + ((toLocation.location.row - fromLocation.location.row) / 2);
            temp.col = fromLocation.location.col + ((toLocation.location.col - fromLocation.location.col) / 2);
            return temp;
        }
    }
}