namespace TrianglePegGameSolver.Web.Legacy;

public class LegacyPegHole
{
  public LegacyPegHole()
  {
    row = -1;
    col = -1;
  }
  public LegacyPegHole(int r, int c)
  {
    row = r;
    col = c;
  }
  public int row;
  public int col;
  public string ToString()
  {
    return (row + "," + col);
  }
}