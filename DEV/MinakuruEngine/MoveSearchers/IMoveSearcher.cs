namespace Minakuru.Engine.MoveSearchers;

public interface IMoveSearcher
{
	Tuple<Move, int> Search(Board board);
}
