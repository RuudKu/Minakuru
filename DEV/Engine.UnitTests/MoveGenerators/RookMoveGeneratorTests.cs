using FluentAssertions;
using Minakuru.Engine.MoveGenerators;

namespace Minakuru.Engine.UnitTests.MoveGenerators;

[TestClass]
public class RookMoveGeneratorTests
{
	[TestMethod]
	public void FromD4Test()
	{
		var fen = "7k/8/8/8/3R4/8/8/K7 w - - 0 1";
		var board = fen.ToBoard();

		var sut = new RookMoveGenerator();

		var actual = sut.GenerateMove(board).ToArray();

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = new ReadableMove[]
		{
			// to the left
			new ("d4", "c4"),
			new ("d4", "b4"),
			new ("d4", "a4"),
			// to the right
			new ("d4", "e4"),
			new ("d4", "f4"),
			new ("d4", "g4"),
			new ("d4", "h4"),
			// down
			new ("d4", "d3"),
			new ("d4", "d2"),
			new ("d4", "d1"),
			// up
			new ("d4", "d5"),
			new ("d4", "d6"),
			new ("d4", "d7"),
			new ("d4", "d8")
		};

		readableMoves.Should().BeEquivalentTo(expected);
	}

	[TestMethod]
	public void WithOtherPiecesTest()
	{
		var fen = "7k/3r4/8/8/3R2B1/8/8/K7 w - - 0 1";
		var board = fen.ToBoard();

		var sut = new RookMoveGenerator();

		var actual = sut.GenerateMove(board).ToArray();

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = new ReadableMove[]
		{
			// to the left
			new ("d4", "c4"),
			new ("d4", "b4"),
			new ("d4", "a4"),
			// to the right
			new ("d4", "e4"),
			new ("d4", "f4"),
			// down
			new ("d4", "d3"),
			new ("d4", "d2"),
			new ("d4", "d1"),
			// up
			new ("d4", "d5"),
			new ("d4", "d6"),
			new ("d4", "d7", true),
		};

		readableMoves.Should().BeEquivalentTo(expected);
	}
}

