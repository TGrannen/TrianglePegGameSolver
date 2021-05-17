namespace LegacyTrianglePegGame
{
    public class PegMove
    {
        public PegLocation fromLocation;
        public PegLocation middleLocation;
        public PegLocation toLocation;

        public PegHole CalcMiddle()
        {
            PegHole temp = new PegHole();
            temp.row = fromLocation.location.row + ((toLocation.location.row - fromLocation.location.row) / 2);
            temp.col = fromLocation.location.col + ((toLocation.location.col - fromLocation.location.col) / 2);
            return temp;
        }

        public void PrintMove()
        {
            Logger.WriteToScreen(fromLocation.location.row + "," + fromLocation.location.col
                                 + "   to    "
                                 + toLocation.location.row + "," + toLocation.location.col);
        }

        public void PrintMoveMoreInfo()
        {
            Logger.WriteToScreen(fromLocation.ToString());
            Logger.WriteToScreen(middleLocation.ToString());
            Logger.WriteToScreen(toLocation.ToString());
        }
    }
}