namespace Minakuru.Engine.MoveGenerators;

public interface IMoveGenerator
{
	public IEnumerable<Move> GenerateMove(Board board, Color color);
}
