using Shouldly;
using Minakuru.Engine.MoveGenerators.Experimental;

namespace Minakuru.Engine.UnitTests.MoveGenerators.Experimental;

[TestClass]
public class QueenMoveGeneratorTests
{
	[TestMethod]
	public void FromD4Test()
	{
		var fen = "k7/8/8/8/3Q4/8/8/7K w - - 0 1";
		var board = fen.ToBoard();

		var sut = new QueenMoveGenerator();

		MoveList actual = [];
		sut.GenerateMove(board, actual);

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = new ReadableMove[]
		{
			// to the left-down
			new ("d4", "c3"),
			new ("d4", "b2"),
			new ("d4", "a1"),
			// to the left
			new ("d4", "c4"),
			new ("d4", "b4"),
			new ("d4", "a4"),
			// to the left-up
			new ("d4", "c5"),
			new ("d4", "b6"),
			new ("d4", "a7"),
			// down
			new ("d4", "d3"),
			new ("d4", "d2"),
			new ("d4", "d1"),
			// up
			new ("d4", "d5"),
			new ("d4", "d6"),
			new ("d4", "d7"),
			new ("d4", "d8"),
			// to right-down
			new ("d4", "e3"),
			new ("d4", "f2"),
			new ("d4", "g1"),
			// to right
			new ("d4", "e4"),
			new ("d4", "f4"),
			new ("d4", "g4"),
			new ("d4", "h4"),
			// to right-up
			new ("d4", "e5"),
			new ("d4", "f6"),
			new ("d4", "g7"),
			new ("d4", "h8")
		};

		readableMoves.ShouldBeEquivalentTo(expected);
	}

	[TestMethod]
	public void WithOtherPiecesTest()
	{
		var fen = "k7/6N1/8/8/R2Q2b1/8/1q6/7K w - - 0 1";
		var board = fen.ToBoard();

		var sut = new QueenMoveGenerator();

		MoveList actual = [];
		sut.GenerateMove(board, actual);

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = new ReadableMove[]
		{
			// to the left-down
			new ("d4", "c3"),
			new ("d4", "b2", true),
			// to the left
			new ("d4", "c4"),
			new ("d4", "b4"),
			// to the left-up
			new ("d4", "c5"),
			new ("d4", "b6"),
			new ("d4", "a7"),
			// down
			new ("d4", "d3"),
			new ("d4", "d2"),
			new ("d4", "d1"),
			// up
			new ("d4", "d5"),
			new ("d4", "d6"),
			new ("d4", "d7"),
			new ("d4", "d8"),
			// to right-down
			new ("d4", "e3"),
			new ("d4", "f2"),
			new ("d4", "g1"),
			// to right
			new ("d4", "e4"),
			new ("d4", "f4"),
			new ("d4", "g4", true),
			// to right-up
			new ("d4", "e5"),
			new ("d4", "f6"),
		};

		readableMoves.ShouldBeEquivalentTo(expected);
	}

	[TestMethod]
	public void TwoWhiteQueensTest()
	{
		var fen = "6k1/3Q4/5Q2/8/8/8/8/1K6 w - - 0 1";
		var board = fen.ToBoard();

		var sut = new QueenMoveGenerator();

		MoveList actual = [];
		sut.GenerateMove(board, actual);

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = 48;

		readableMoves.Length.ShouldBe(expected);
	}

}

