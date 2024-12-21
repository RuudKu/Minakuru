using Engine.MoveGenerators;
using FluentAssertions;

namespace Engine.UnitTests.MoveGenerators;

[TestClass]
public class QueenMoveGeneratorTests
{
	[TestMethod]
	public void FromD4Test()
	{
		var fen = "k7/8/8/8/3Q4/8/8/7K w - - 0 1";
		var board = fen.ToBoard();

		var sut = new QueenMoveGenerator();

		var actual = sut.GenerateMove(board, Color.White).ToArray();

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = new ReadableMove[]
		{
			// to the left-down
			new ("d4", "c3", false, false),
			new ("d4", "b2", false, false),
			new ("d4", "a1", false, false),
			// to the left
			new ("d4", "c4", false, false),
			new ("d4", "b4", false, false),
			new ("d4", "a4", false, false),
			// to the left-up
			new ("d4", "c5", false, false),
			new ("d4", "b6", false, false),
			new ("d4", "a7", false, false),
			// down
			new ("d4", "d3", false, false),
			new ("d4", "d2", false, false),
			new ("d4", "d1", false, false),
			// up
			new ("d4", "d5", false, false),
			new ("d4", "d6", false, false),
			new ("d4", "d7", false, false),
			new ("d4", "d8", false, false),
			// to right-down
			new ("d4", "e3", false, false),
			new ("d4", "f2", false, false),
			new ("d4", "g1", false, false),
			// to right
			new ("d4", "e4", false, false),
			new ("d4", "f4", false, false),
			new ("d4", "g4", false, false),
			new ("d4", "h4", false, false),
			// to right-up
			new ("d4", "e5", false, false),
			new ("d4", "f6", false, false),
			new ("d4", "g7", false, false),
			new ("d4", "h8", false, false)
		};

		readableMoves.Should().BeEquivalentTo(expected);
	}

	[TestMethod]
	public void WithOtherPiecesTest()
	{
		var fen = "k7/6N1/8/8/R2Q2b1/8/1q6/7K w - - 0 1";
		var board = fen.ToBoard();

		var sut = new QueenMoveGenerator();

		var actual = sut.GenerateMove(board, Color.White).ToArray();

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = new ReadableMove[]
		{
			// to the left-down
			new ("d4", "c3", false, false),
			new ("d4", "b2", true, false),
			// to the left
			new ("d4", "c4", false, false),
			new ("d4", "b4", false, false),
			// to the left-up
			new ("d4", "c5", false, false),
			new ("d4", "b6", false, false),
			new ("d4", "a7", false, false),
			// down
			new ("d4", "d3", false, false),
			new ("d4", "d2", false, false),
			new ("d4", "d1", false, false),
			// up
			new ("d4", "d5", false, false),
			new ("d4", "d6", false, false),
			new ("d4", "d7", false, false),
			new ("d4", "d8", false, false),
			// to right-down
			new ("d4", "e3", false, false),
			new ("d4", "f2", false, false),
			new ("d4", "g1", false, false),
			// to right
			new ("d4", "e4", false, false),
			new ("d4", "f4", false, false),
			new ("d4", "g4", true, false),
			// to right-up
			new ("d4", "e5", false, false),
			new ("d4", "f6", false, false),
		};

		readableMoves.Should().BeEquivalentTo(expected);
	}
}

