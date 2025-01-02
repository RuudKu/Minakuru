using FluentAssertions;
using Minakuru.Engine.MoveGenerators.Experimental;

namespace Minakuru.Engine.UnitTests.MoveGenerators.Experimental;

[TestClass]
public class KingMoveGeneratorTests
{
	[TestMethod]
	public void FromD4Test()
	{
		var fen = "7k/8/8/8/3K4/8/8/8 w - - 0 1";
		var board = fen.ToBoard();

		var sut = new KingMoveGenerator();

		MoveList actual = [];
		sut.GenerateMove(board, actual);

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = new ReadableMove[]
		{
			// to the left-down
			new ("d4", "c3"),
			// to the left
			new ("d4", "c4"),
			// to the left-up
			new ("d4", "c5"),
			// down
			new ("d4", "d3"),
			// up
			new ("d4", "d5"),
			// to right-down
			new ("d4", "e3"),
			// to right
			new ("d4", "e4"),
			// to right-up
			new ("d4", "e5"),
		};

		readableMoves.Should().BeEquivalentTo(expected);
	}

	[TestMethod]
	public void WithOtherPiecesTest()
	{
		var fen = "7k/8/8/2R5/3K4/4r3/8/8 w - - 0 1";
		var board = fen.ToBoard();

		var sut = new KingMoveGenerator();

		MoveList actual = [];
		sut.GenerateMove(board, actual);

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = new ReadableMove[]
		{
			// to the left-down
			new ("d4", "c3"),
			// to the left
			new ("d4", "c4"),
			// to the left-up
			// no moves possible
			// down
			new ("d4", "d3"),
			// up
			new ("d4", "d5"),
			// to right-down
			new ("d4", "e3", true),
			// to right
			new ("d4", "e4"),
			// to right-up
			new ("d4", "e5"),
		};

		readableMoves.Should().BeEquivalentTo(expected);
	}
}
