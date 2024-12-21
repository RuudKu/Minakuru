using Engine.MoveGenerators;
using FluentAssertions;

namespace Engine.UnitTests.MoveGenerators;

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
			new ("d4", "c3", false, false),
			new ("d4", "b2", false, false),
			new ("d4", "a1", false, false),
			// to the left-up
			new ("d4", "c5", false, false),
			new ("d4", "b6", false, false),
			new ("d4", "a7", false, false),
			// to right-down
			new ("d4", "e3", false, false),
			new ("d4", "f2", false, false),
			new ("d4", "g1", false, false),
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
		var fen = "k7/8/5q2/8/3B4/8/8/6K1 w - - 0 1";
		var board = fen.ToBoard();

		var sut = new BishopMoveGenerator();

		var actual = sut.GenerateMove(board, Color.White).ToArray();

		var readableMoves = actual.ToReadableMoves().ToArray();
		var expected = new ReadableMove[]
		{
			// to the left-down
			new ("d4", "c3", false, false),
			new ("d4", "b2", false, false),
			new ("d4", "a1", false, false),
			// to the left-up
			new ("d4", "c5", false, false),
			new ("d4", "b6", false, false),
			new ("d4", "a7", false, false),
			// to right-down
			new ("d4", "e3", false, false),
			new ("d4", "f2", false, false),
			// to-right up
			new ("d4", "e5", false, false),
			new ("d4", "f6", true, false)
		};

		readableMoves.Should().BeEquivalentTo(expected);
	}
}

