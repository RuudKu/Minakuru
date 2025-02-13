using Shouldly;
using Minakuru.Engine.MoveGenerators.Experimental;

namespace Minakuru.Engine.UnitTests.MoveGenerators.Experimental;

[TestClass]
public class KnightMoveGeneratorTests
{
	[TestMethod]
	public void FromD4Test()
	{
		var fen = "7k/8/8/8/3N4/8/8/K7 w - - 0 1";
		var board = fen.ToBoard();

		var sut = new KnightMoveGenerator();

		MoveList actual = [];
		sut.GenerateMove(board, actual);

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = new ReadableMove[]
		{
			new ("d4", "b3"),
			new ("d4", "b5"),
			new ("d4", "c2"),
			new ("d4", "c6"),
			new ("d4", "e2"),
			new ("d4", "e6"),
			new ("d4", "f3"),
			new ("d4", "f5"),
		};

		readableMoves.ShouldBe(expected, ignoreOrder: true	);
	}

	[TestMethod]
	public void WithOtherPiecesTest()
	{
		var fen = "7k/8/4r3/8/3N4/5B2/8/K7 w - - 0 1";
		var board = fen.ToBoard();

		var sut = new KnightMoveGenerator();

		MoveList actual = [];
		sut.GenerateMove(board, actual);

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = new ReadableMove[]
		{
			new ("d4", "b3"),
			new ("d4", "b5"),
			new ("d4", "c2"),
			new ("d4", "c6"),
			new ("d4", "e2"),
			new ("d4", "e6", true),
			new ("d4", "f5"),
		};

		readableMoves.ShouldBe(expected, ignoreOrder: true);
	}

	[TestMethod]
	public void WithTwoWhiteKnightsTest()
	{
		var fen = "7k/8/4r3/5N2/3N4/5B2/8/K7 w - - 0 1";
		var board = fen.ToBoard();

		var sut = new KnightMoveGenerator();

		MoveList actual = [];
		sut.GenerateMove(board, actual);

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = 13;

		readableMoves.Length.ShouldBe(expected);
	}
}
