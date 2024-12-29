using System.Diagnostics;
using Minakuru.Engine.MoveGenerators;

namespace Minakuru.Engine.PerftTests;

public static class PerftHelper
{
	public static readonly LegalMovesGenerator LegalMovesGenerator = (LegalMovesGenerator)MoveGeneratorFactory.Create();

	public static ulong CountNodes(Board board, int remainingDepth)
	{
		var legalMoves = LegalMovesGenerator.GenerateMove(board).ToArray();

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
			var nodesCurrentMove = CountNodes(newBoard, remainingDepth - 1);
			count += nodesCurrentMove;
			Debug.WriteLine(ToString(legalMove) + " : " + nodesCurrentMove);
		}
		return count;
	}

	public static string ToString(Move result)
	{
		var @string = string.Empty;
		byte columnFrom = (byte)(result.From % 8);
		byte rowFrom = (byte)(result.From / 8);
		byte columnTo = (byte)(result.To % 8);
		byte rowTo = (byte)(result.To / 8);
		@string += (char)('a' + columnFrom);
		@string += (char)('1' + rowFrom);
		@string += (char)('a' + columnTo);
		@string += (char)('1' + rowTo);
		return @string;
	}
}
