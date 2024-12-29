namespace Minakuru.Engine.MoveOrdering;

public interface IMoveOrdering
{
	IEnumerable<Move> Order(Board board, IEnumerable<Move> moves);
}
