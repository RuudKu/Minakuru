
using Engine.MoveGenerators;
using FluentAssertions;

namespace Engine.UnitTests.MoveGenerators
{
	[TestClass]
	public class KnightMoveGeneratorTests
	{
		[TestMethod]
		public void FromE4Test()
		{
			var fen = "7k/8/8/8/3N4/8/8/K7 w - - 0 1";
			var board = fen.ToBoard();

			var sut = new KnightMoveGenerator();

			var actual = sut.GenerateMove(board, Color.White).ToArray();

			var readableMoves = actual.ToReadableMoves().ToArray();
			var expected = new ReadableMove[]
			{
				new ("d4", "b3", false, false),
				new ("d4", "b5", false, false),
				new ("d4", "c2", false, false),
				new ("d4", "c6", false, false),
				new ("d4", "e2", false, false),
				new ("d4", "e6", false, false),
				new ("d4", "f3", false, false),
				new ("d4", "f5", false, false),
			};

			readableMoves.Should().BeEquivalentTo(expected);
		}

		[TestMethod]
		public void WithOtherPiecesTest()
		{
			var fen = "7k/8/4r3/8/3N4/5B2/8/K7 w - - 0 1";
			var board = fen.ToBoard();

			var sut = new KnightMoveGenerator();

			var actual = sut.GenerateMove(board, Color.White).ToArray();

			var readableMoves = actual.ToReadableMoves().ToArray();
			var expected = new ReadableMove[]
			{
				new ("d4", "b3", false, false),
				new ("d4", "b5", false, false),
				new ("d4", "c2", false, false),
				new ("d4", "c6", false, false),
				new ("d4", "e2", false, false),
				new ("d4", "e6", true, false),
				new ("d4", "f5", false, false),
			};

			readableMoves.Should().BeEquivalentTo(expected);
		}
	}
}
