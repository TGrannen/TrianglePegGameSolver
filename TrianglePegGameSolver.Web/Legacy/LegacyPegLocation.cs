using System.Collections.Generic;

namespace TrianglePegGameSolver.Web.Legacy;

public class LegacyPegLocation
{
    public LegacyPegLocation()
    {
        places_can_jump_to = new List<LegacyPegMove>();
    }

    public bool filled;
    public bool isValid;
    public LegacyPegHole location;
    public List<LegacyPegMove> places_can_jump_to;

    public string ToStringShort()
    {
        string message = "";
        if (isValid)
        {
            if (filled)
            {
                message = "X";
            }
            else
            {
                message = "O";
            }
        }
        else
        {
            message = "-";
        }
        return message;
    }

    public string ToString()
    {
        string temp = "";
        temp += location.ToString() + "\n";
        temp += "Valid: " + isValid + "\n";
        temp += "Filled: " + filled + "\n";
        return temp;
    }

    public bool CompareCoordsToLocation(int row, int col)
    {
        return (location.row == row && location.col == col);
    }
}