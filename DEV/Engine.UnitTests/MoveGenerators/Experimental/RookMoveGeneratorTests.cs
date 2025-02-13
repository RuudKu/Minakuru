using Shouldly;
using Minakuru.Engine.MoveGenerators.Experimental;

namespace Minakuru.Engine.UnitTests.MoveGenerators.Experimental;

[TestClass]
public class RookMoveGeneratorTests
{
	[TestMethod]
	public void FromD4Test()
	{
		var fen = "7k/8/8/8/3R4/8/8/K7 w - - 0 1";
		var board = fen.ToBoard();

		var sut = new RookMoveGenerator();

		MoveList actual = [];
		sut.GenerateMove(board, actual);

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

		readableMoves.ShouldBe(expected, ignoreOrder: true);
	}

	[TestMethod]
	public void WithOtherPiecesTest()
	{
		var fen = "7k/3r4/8/8/3R2B1/8/8/K7 w - - 0 1";
		var board = fen.ToBoard();

		var sut = new RookMoveGenerator();

		MoveList actual = [];
		sut.GenerateMove(board, actual);

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

		readableMoves.ShouldBe(expected, ignoreOrder: true	);
	}


	[TestMethod]
	public void WithTwoWhiteRooks()
	{
		var fen = "7k/2R5/8/8/3R4/8/8/K7 w - - 0 1";
		var board = fen.ToBoard();

		var sut = new RookMoveGenerator();

		MoveList actual = [];
		sut.GenerateMove(board, actual);

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = 28;

		readableMoves.Length.ShouldBe(expected);
	}
}

