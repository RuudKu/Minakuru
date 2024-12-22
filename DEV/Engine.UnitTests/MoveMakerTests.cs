
using FluentAssertions;

namespace Engine.UnitTests
{
	[TestClass]
	public class MoveMakerTests
	{
		[TestMethod]
		public void StartWithD2D4()
		{
			var board = BoardHelper.StartPosition();
			Move move = new(Field.ColumnDFieldNo + Field.Row2FieldNo, Field.ColumnDFieldNo + Field.Row4FieldNo);
			var newBoard = MoveMaker.MakeMove(board, move);

			var newFen = FenConverter.ToFen(newBoard);

			var expected = "rnbqkbnr/pppppppp/8/8/3P4/8/PPP1PPPP/RNBQKBNR b KQkq d3 0 1";
			newFen.Should().Be(expected);
		}
	}
}
