namespace Minakuru.Engine.MoveOrdering;

public class MoveOrdering : IMoveOrdering
{
	public IEnumerable<Move> Order(Board board, IEnumerable<Move> moves)
	{
		foreach (var move in moves)
		{
			yield return move;
		}
	}
}
