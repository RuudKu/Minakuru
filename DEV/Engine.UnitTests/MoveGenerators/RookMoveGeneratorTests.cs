using Engine.MoveGenerators;
using FluentAssertions;

namespace Engine.UnitTests.MoveGenerators;

[TestClass]
public class RookMoveGeneratorTests
{
	[TestMethod]
	public void FromD4Test()
	{
		var fen = "7k/8/8/8/3R4/8/8/K7 w - - 0 1";
		var board = fen.ToBoard();

		var sut = new RookMoveGenerator();

		var actual = sut.GenerateMove(board, Color.White).ToArray();

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = new ReadableMove[]
		{
			// to the left
			new ("d4", "c4", false, false),
			new ("d4", "b4", false, false),
			new ("d4", "a4", false, false),
			// to the right
			new ("d4", "e4", false, false),
			new ("d4", "f4", false, false),
			new ("d4", "g4", false, false),
			new ("d4", "h4", false, false),
			// down
			new ("d4", "d3", false, false),
			new ("d4", "d2", false, false),
			new ("d4", "d1", false, false),
			// up
			new ("d4", "d5", false, false),
			new ("d4", "d6", false, false),
			new ("d4", "d7", false, false),
			new ("d4", "d8", false, false)
		};

		readableMoves.Should().BeEquivalentTo(expected);
	}

	[TestMethod]
	public void WithOtherPiecesTest()
	{
		var fen = "7k/3r4/8/8/3R2B1/8/8/K7 w - - 0 1";
		var board = fen.ToBoard();

		var sut = new RookMoveGenerator();

		var actual = sut.GenerateMove(board, Color.White).ToArray();

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = new ReadableMove[]
		{
			// to the left
			new ("d4", "c4", false, false),
			new ("d4", "b4", false, false),
			new ("d4", "a4", false, false),
			// to the right
			new ("d4", "e4", false, false),
			new ("d4", "f4", false, false),
			// down
			new ("d4", "d3", false, false),
			new ("d4", "d2", false, false),
			new ("d4", "d1", false, false),
			// up
			new ("d4", "d5", false, false),
			new ("d4", "d6", false, false),
			new ("d4", "d7", true, false),
		};

		readableMoves.Should().BeEquivalentTo(expected);
	}
}

