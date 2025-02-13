using Shouldly;
using Minakuru.Engine.MoveGenerators.Experimental;

namespace Minakuru.Engine.UnitTests.MoveGenerators.Experimental;

[TestClass]
public class PseudoLegalMoveGeneratorTests
{
	[TestMethod]
	public void GenerateFromStartingPosition()
	{
		var board = Board.Init();
		var sut = new PseudoLegalMoveGenerator();

		MoveList allMoves = [];
		sut.GenerateMove(board, allMoves);

		int expected = 20;

		allMoves.Count().ShouldBe(expected);
	}

	[DataRow("rnbqkbnr/ppp2ppp/4p3/3pP3/8/8/PPPP1PPP/RNBQKBNR w KQkq d6 0 3", 30, 1, 0, DisplayName = "1.e4 e5 2.e6 d5")]
	[DataTestMethod]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S1854:Unused assignments should be removed", Justification = "<Pending>")]
	public void PerftLevel1Tests(string fen, int expectedCount, int expectedCaptures, int expectedPromotions)
	{
		var expectedPerftStats = new PerftStats(expectedCount, expectedCaptures, expectedPromotions);
		var board = fen.ToBoard();
		var sut = new PseudoLegalMoveGenerator();

		MoveList allMoves = [];
		sut.GenerateMove(board, allMoves);

		var actualPerftStats = new PerftStats(
			allMoves.Count(),
			allMoves.Count(m => m.Capture),
			allMoves.Count(m => m.PromotedTo != Piece.None));

		ReadableMove[] readableAllMoves = [];
		ReadableMove[] readableCaptureMoves = [];
		ReadableMove[] readablePromotionMoves = [];

		if (expectedPerftStats != actualPerftStats)
		{
			readableAllMoves = allMoves.ToReadableMoves().ToArray();
			readableCaptureMoves = allMoves.Where(m => m.Capture).ToReadableMoves().ToArray();
			readablePromotionMoves = allMoves.Where(m => m.PromotedTo != Piece.None).ToReadableMoves().ToArray();
		}
		actualPerftStats.ShouldBe(expectedPerftStats);
	}

	private record PerftStats(int Nodes, int Captures, int Promotions)
	{
	}
}
