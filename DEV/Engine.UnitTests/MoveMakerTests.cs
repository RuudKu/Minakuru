
using FluentAssertions;

namespace Engine.UnitTests
{
	[TestClass]
	public class MoveMakerTests
	{
		[TestMethod]
		public void StartWithD2D4()
		{
			var board = Board.Init();

			Move move = new(Field.D2FieldNo, Field.D4FieldNo);
			var newBoard = MoveMaker.MakeMove(board, move);

			var newFen = FenConverter.ToFen(newBoard);
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

			var newFen = FenConverter.ToFen(newBoard);
			var expected = "rnbqkbnr/ppp1pppp/8/3p4/3P4/8/PPPKPPPP/R6R b kq - 1 1";
			newFen.Should().Be(expected);
		}
	}
}
