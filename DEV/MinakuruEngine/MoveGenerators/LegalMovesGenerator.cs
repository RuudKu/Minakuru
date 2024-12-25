using Minakuru.Engine.ThreatCheckers;

namespace Minakuru.Engine.MoveGenerators;

public class LegalMovesGenerator(PseudoLegalMoveGenerator pseudoLegalMovesGenerator, IThreatChecker threatChecker) : IMoveGenerator
{
	private readonly PseudoLegalMoveGenerator _pseudoLegalMovesGenerator = pseudoLegalMovesGenerator ?? throw new ArgumentNullException(nameof(pseudoLegalMovesGenerator));
	private readonly IThreatChecker _threatChecker = threatChecker ?? throw new ArgumentNullException(nameof(threatChecker));

	public LegalMovesGenerator() : this(new PseudoLegalMoveGenerator(), new ThreatChecker())
	{
	}

	public IEnumerable<Move> GenerateMove(Board board)
	{
		return GenerateMove(board, board.ColorToMove);
	}

	public IEnumerable<Move> GenerateMove(Board board, Color color)
	{
		var enumerator = _pseudoLegalMovesGenerator.GenerateMove(board, color).GetEnumerator();
		while (enumerator.MoveNext())
		{
			var pseudoLegalMove = enumerator.Current;
			Color opponentColor = color.OtherColor();

			var newBoard = MoveMaker.MakeMove(board, pseudoLegalMove);
			if (pseudoLegalMove.From == Field.E1FieldNo && pseudoLegalMove.To == Field.C1FieldNo)
			{
				if (board.HasColoredPieceAt(Field.E1FieldNo, ColoredPiece.WhiteKing) &&
					(_threatChecker.IsUnderAttack(newBoard, Field.E1FieldNo, opponentColor) ||
						_threatChecker.IsUnderAttack(newBoard, Field.D1FieldNo, opponentColor)))
				{
					continue;
				}
			}
			else if (pseudoLegalMove.From == Field.E1FieldNo && pseudoLegalMove.To == Field.G1FieldNo)
			{
				if (board.HasColoredPieceAt(Field.E1FieldNo, ColoredPiece.WhiteKing) &&
					(_threatChecker.IsUnderAttack(newBoard, Field.E1FieldNo, opponentColor) ||
						_threatChecker.IsUnderAttack(newBoard, Field.F1FieldNo, opponentColor)))
				{
					continue;
				}
			}
			else if(pseudoLegalMove.From == Field.E8FieldNo && pseudoLegalMove.To == Field.C8FieldNo)
			{
				if (board.HasColoredPieceAt(Field.E8FieldNo, ColoredPiece.BlackKing) &&
					(_threatChecker.IsUnderAttack(newBoard, Field.E8FieldNo, opponentColor) ||
						_threatChecker.IsUnderAttack(newBoard, Field.D8FieldNo, opponentColor)))
				{
					continue;
				}
			}
			else if (pseudoLegalMove.From == Field.E8FieldNo && pseudoLegalMove.To == Field.G8FieldNo
				&& board.HasColoredPieceAt(Field.E8FieldNo, ColoredPiece.BlackKing)
					&& (_threatChecker.IsUnderAttack(newBoard, Field.E8FieldNo, opponentColor) ||
						_threatChecker.IsUnderAttack(newBoard, Field.F8FieldNo, opponentColor)))
			{
				continue;
			}

			Color colorToMove = board.ColorToMove;
			byte kingFieldNo = newBoard.KingAt(colorToMove);

			bool check = _threatChecker.IsUnderAttack(newBoard, kingFieldNo, colorToMove.OtherColor());
			if (!check)
			{
				yield return pseudoLegalMove;
			}
		}
	}
}
