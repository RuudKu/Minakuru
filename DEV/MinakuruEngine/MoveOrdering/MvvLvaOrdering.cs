using Minakuru.Engine.Evaluators;

namespace Minakuru.Engine.MoveOrdering;

/// <summary>
/// Most valuable victim - Least valuable aggressor 
/// </summary>
public class MvvLvaOrdering : IMoveOrdering
{
	public IEnumerable<Move> Order(Board board, IEnumerable<Move> moves)
	{
		var mvvLvaMoves = new List<Move>(moves.Where(m => m.Capture)).Select(m => new MvvLva(m)).ToArray();
		var otherColor = board.ColorToMove.OtherColor();

		foreach (var mvvLvaMove in mvvLvaMoves)
		{
			var coloredPieceAtFrom = board.GetColoredPieceAt(mvvLvaMove.Move.From, board.ColorToMove);
			mvvLvaMove.AggressorValue = ColoredPieceValue(coloredPieceAtFrom);
			var coloredPieceAtTo = board.GetColoredPieceAt(mvvLvaMove.Move.To, otherColor);
			mvvLvaMove.VictimValue = ColoredPieceValue(coloredPieceAtTo);
		}
		foreach (var mvvLvaMove in mvvLvaMoves.OrderByDescending(m => m.VictimValue).ThenBy(m => m.AggressorValue))
		{
			yield return mvvLvaMove.Move;
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
			ColoredPiece.WhiteQueen => EvaluationConstants.QueenWeightFactor,
			ColoredPiece.BlackQueen => EvaluationConstants.QueenWeightFactor,
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
	}
}
