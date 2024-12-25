using FluentAssertions;
using Minakuru.Engine.MoveGenerators;

namespace Minakuru.Engine.UnitTests.MoveGenerators;

[TestClass]
public class BishopMoveGeneratorTests
{
	[TestMethod]
	public void FromD4Test()
	{
		var fen = "k7/8/8/8/3B4/8/8/7K w - - 0 1";
		var board = fen.ToBoard();

		var sut = new BishopMoveGenerator();

		var actual = sut.GenerateMove(board, Color.White).ToArray();

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = new ReadableMove[]
		{
			// to the left-down
			new ("d4", "c3"),
			new ("d4", "b2"),
			new ("d4", "a1"),
			// to the left-up
			new ("d4", "c5"),
			new ("d4", "b6"),
			new ("d4", "a7"),
			// to right-down
			new ("d4", "e3"),
			new ("d4", "f2"),
			new ("d4", "g1"),
			// to right-up
			new ("d4", "e5"),
			new ("d4", "f6"),
			new ("d4", "g7"),
			new ("d4", "h8")
		};

		readableMoves.Should().BeEquivalentTo(expected);
	}

	[TestMethod]
	public void WithOtherPiecesTest()
	{
		var fen = "k7/8/5q2/8/3B4/8/8/6K1 w - - 0 1";
		var board = fen.ToBoard();

		var sut = new BishopMoveGenerator();

		var actual = sut.GenerateMove(board, Color.White).ToArray();

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = new ReadableMove[]
		{
			// to the left-down
			new ("d4", "c3"),
			new ("d4", "b2"),
			new ("d4", "a1"),
			// to the left-up
			new ("d4", "c5"),
			new ("d4", "b6"),
			new ("d4", "a7"),
			// to right-down
			new ("d4", "e3"),
			new ("d4", "f2"),
			// to-right up
			new ("d4", "e5"),
			new ("d4", "f6", true)
		};

		readableMoves.Should().BeEquivalentTo(expected);
	}
}

