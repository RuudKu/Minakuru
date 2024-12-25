using FluentAssertions;
using Minakuru.Engine.MoveGenerators;

namespace Minakuru.Engine.UnitTests.MoveGenerators;

[TestClass]
public class LegalMoveGeneratorTests
{
	[TestMethod]

	public void OneMoveBecausePinsTest()
	{
		var fen = "b6r/7B/2R5/8/8/6k1/8/q1N4K w - - 0 1";
		var board = fen.ToBoard();

		var legalMovesGenerator = new LegalMovesGenerator();

		var allMoves = legalMovesGenerator.GenerateMove(board, Color.White);
		var allReadableMoves = allMoves.ToReadableMoves();

		var expected = new ReadableMove[]
		{
			new("h1", "g1")
		};

		allReadableMoves.Should().BeEquivalentTo(expected);

	}
}
