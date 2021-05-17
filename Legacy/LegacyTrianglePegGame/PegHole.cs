namespace LegacyTrianglePegGame
{
  public class PegHole
  {
    public PegHole()
    {
      row = -1;
      col = -1;
    }
    public PegHole(int r, int c)
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

}
