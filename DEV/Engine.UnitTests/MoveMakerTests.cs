using FluentAssertions;

namespace Minakuru.Engine.UnitTests;

[TestClass]
public class MoveMakerTests
{
	[TestMethod]
	public void StartWithD2D4()
	{
		var board = Board.Init();

		Move move = new(Field.D2FieldNo, Field.D4FieldNo);
		var newBoard = MoveMaker.MakeMove(board, move);

		var newFen = newBoard.ToFen();
		var expected = "rnbqkbnr/pppppppp/8/8/3P4/8/PPP1PPPP/RNBQKBNR b KQkq d3 0 1";
		newFen.Should().Be(expected);
	}

	[TestMethod]
	public void WhiteKingMovesInhibitsCastling()
	{
		var originalFen = "rnbqkbnr/ppp1pppp/8/3p4/3P4/8/PPP1PPPP/R3K2R w KQkq d6 0 1";
		var board = originalFen.ToBoard();

		Move move = new(Field.E1FieldNo, Field.D2FieldNo);
		var newBoard = MoveMaker.MakeMove(board, move);

		var newFen = newBoard.ToFen();
		var expected = "rnbqkbnr/ppp1pppp/8/3p4/3P4/8/PPPKPPPP/R6R b kq - 1 1";
		newFen.Should().Be(expected);
	}

	[TestMethod]
	public void PawnCapturesEnPassantToHigherColumnTest()
	{
		var originalFen = "7k/8/3p4/3Pp3/8/8/8/K7 w - e6 0 1";
		var originalBoard = originalFen.ToBoard();

		var readableMove = new ReadableMove("d5", "e6", true);
		var move = readableMove.ToMove();

		var newBoard = MoveMaker.MakeMove(originalBoard, move);

		var actualFen = newBoard.ToFen();
		var expectedFen = "7k/8/3pP3/8/8/8/8/K7 b - - 0 1";

		actualFen.Should().Be(expectedFen);
	}
}
