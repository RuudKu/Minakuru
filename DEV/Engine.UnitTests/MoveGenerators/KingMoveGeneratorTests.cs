using Engine.MoveGenerators;
using FluentAssertions;

namespace Engine.UnitTests.MoveGenerators;

[TestClass]
public class KingMoveGeneratorTests
{
	[TestMethod]
	public void FromD4Test()
	{
		var fen = "7k/8/8/8/3K4/8/8/8 w - - 0 1";
		var board = fen.ToBoard();

		var sut = new KingMoveGenerator();

		var actual = sut.GenerateMove(board, Color.White).ToArray();

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = new ReadableMove[]
		{
			// to the left-down
			new ("d4", "c3", false, false),
			// to the left
			new ("d4", "c4", false, false),
			// to the left-up
			new ("d4", "c5", false, false),
			// down
			new ("d4", "d3", false, false),
			// up
			new ("d4", "d5", false, false),
			// to right-down
			new ("d4", "e3", false, false),
			// to right
			new ("d4", "e4", false, false),
			// to right-up
			new ("d4", "e5", false, false),
		};

		readableMoves.Should().BeEquivalentTo(expected);
	}

	[TestMethod]
	public void WithOtherPiecesTest()
	{
		var fen = "7k/8/8/2R5/3K4/4r3/8/8 w - - 0 1";
		var board = fen.ToBoard();

		var sut = new KingMoveGenerator();

		var actual = sut.GenerateMove(board, Color.White).ToArray();

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = new ReadableMove[]
		{
			// to the left-down
			new ("d4", "c3", false, false),
			// to the left
			new ("d4", "c4", false, false),
			// to the left-up
			// no moves possible
			// down
			new ("d4", "d3", false, false),
			// up
			new ("d4", "d5", false, false),
			// to right-down
			new ("d4", "e3", true, false),
			// to right
			new ("d4", "e4", false, false),
			// to right-up
			new ("d4", "e5", false, false),
		};

		readableMoves.Should().BeEquivalentTo(expected);
	}
}

