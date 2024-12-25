using FluentAssertions;
using Minakuru.Engine.MoveGenerators;

namespace Minakuru.Engine.PerftTests;

[TestClass]
public sealed class FromStartPositionTests
{
	[DataRow(0, 1UL, DisplayName = "Depth 0")]
	[DataRow(1, 20UL, DisplayName = "Depth 1")]
	[DataRow(2, 400UL, DisplayName = "Depth 2")]
	[DataRow(3, 8_902UL, DisplayName = "Depth 3")]
	[DataRow(4, 197_281UL, DisplayName = "Depth 4")]
	// [DataRow(5, 4_865_609UL, DisplayName = "Depth 5")]
	[DataTestMethod]
	public void PerftStartpositionTests(int depth, ulong expectedNodes)
	{
		var board = Board.Init();
		var legalMovesGenerator = new LegalMovesGenerator();

		var actual = CountNodes(board, depth);

		actual.Should().Be(expectedNodes);


		ulong CountNodes(Board board, int remainingDepth)
		{
			var legalMoves = legalMovesGenerator.GenerateMove(board).ToArray();

			if (remainingDepth == 0)
			{
				return 1;
			}

			if (remainingDepth == 1)
			{
				return (ulong)legalMoves.Length;
			}

			ulong count = 0;

			foreach (var legalMove in legalMoves)
			{
				var newBoard = MoveMaker.MakeMove(board, legalMove);
				count += CountNodes(newBoard, remainingDepth - 1);
			}
			return count;
		}
	}
}
