namespace Minakuru.Engine.MoveGenerators;

public interface IMoveGenerator
{
	public void GenerateMove(Board board, MoveList moveList);
}
