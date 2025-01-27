﻿using Minakuru.Engine.ThreatCheckers;

namespace Minakuru.Engine.MoveGenerators;

public class LegalMovesGenerator(PseudoLegalMoveGenerator pseudoLegalMovesGenerator, IThreatChecker threatChecker)
{
	private readonly PseudoLegalMoveGenerator _pseudoLegalMovesGenerator = pseudoLegalMovesGenerator ?? throw new ArgumentNullException(nameof(pseudoLegalMovesGenerator));
	private readonly IThreatChecker _threatChecker = threatChecker ?? throw new ArgumentNullException(nameof(threatChecker));

	public MoveList GenerateMove(Board board)
	{
		MoveList moveList = [];
		MoveList pseudoLegalMoveList = [];
		var color = board.ColorToMove;
		_pseudoLegalMovesGenerator.GenerateMove(board, pseudoLegalMoveList);
		foreach (var pseudoLegalMove in pseudoLegalMoveList)
		{
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
				moveList.Add(pseudoLegalMove);
			}
		}
		return moveList;
	}
}
