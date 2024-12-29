using System.Runtime.CompilerServices;
using Minakuru.Engine.Evaluators;

namespace Minakuru.Engine.MoveOrdering;

/// <summary>
/// Most valuable victim - Least valuable aggressor 
/// </summary>
public class MvvLvaOrdering : IMoveOrdering
{
	public IEnumerable<Move> Order(Board board, IEnumerable<Move> moves)
	{
		var capturingMoves = new List<Move>(moves.Where(m => m.Capture)).Select(m => new MvvLva(m));
		var otherColor = board.ColorToMove.OtherColor();

		foreach (var move in capturingMoves)
		{
			var coloredPieceAtFrom = board.GetColoredPieceAt(move.Move.From, otherColor);
			move.AggressorValue = ColoredPieceValue(coloredPieceAtFrom);
			var coloredPieceAtTo = board.GetColoredPieceAt(move.Move.To, otherColor);
			move.VictimValue = ColoredPieceValue(coloredPieceAtTo);
		}
		foreach (var move in capturingMoves.OrderByDescending(m=>m.Difference).Select(m=>m.Move))
		{
			yield return move;
		}
		foreach (var move in moves.Where(m => !m.Capture))
		{
			yield return move;
		}
	}

	private static int ColoredPieceValue(ColoredPiece coloredPiece)
	{
		return coloredPiece switch
		{
			ColoredPiece.WhiteKing => EvaluationConstants.KingWeightFactor,
			ColoredPiece.BlackKing => EvaluationConstants.KingWeightFactor,
			ColoredPiece.WhiteQueen =>  EvaluationConstants.QueenWeightFactor,
			ColoredPiece.BlackQueen =>  EvaluationConstants.QueenWeightFactor,
			ColoredPiece.WhiteRook => EvaluationConstants.RookWeightFactor,
			ColoredPiece.BlackRook => EvaluationConstants.RookWeightFactor,
			ColoredPiece.WhiteBishop => EvaluationConstants.BishopWeightFactor,
			ColoredPiece.BlackBishop => EvaluationConstants.BishopWeightFactor,
			ColoredPiece.WhiteKnight => EvaluationConstants.KnightWeightFactor,
			ColoredPiece.BlackKnight => EvaluationConstants.KnightWeightFactor,
			ColoredPiece.WhitePawn => EvaluationConstants.PawnWeightFactor,
			ColoredPiece.BlackPawn => EvaluationConstants.PawnWeightFactor,
			_ => throw new NotImplementedException()
		};
	}

	record MvvLva(Move Move)
	{
		public int VictimValue { get; set; }
		public int AggressorValue { get; set; }

		public int Difference => VictimValue - AggressorValue;
	}
}
