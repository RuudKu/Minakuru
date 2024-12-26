namespace Minakuru.Engine;

public static class MoveMaker
{
	public static Board MakeMove(Board board, Move move)
	{
		var newBoard = board with { };

		var colorToMove = board.ColorToMove;

		var fromPiece = UpdatePiecePlacement(newBoard, board, colorToMove, move);

		UpdateSideToMove(newBoard);

		UpdateCastlingAbility(newBoard, move);

		UpdateEnPassantPossibility(newBoard, fromPiece, move);

		UpdateHalfMoveClock(newBoard, move, fromPiece);

		UpdateFullMoveCounter(newBoard, colorToMove);

		return newBoard;
	}

	private static void UpdateFullMoveCounter(Board newBoard, Color colorToMove)
	{
		if (colorToMove == Color.Black)
		{
			newBoard.FullMoveCounter++;
		}
	}

	private static void UpdateHalfMoveClock(Board newBoard, Move move, ColoredPiece fromPiece)
	{
		if (fromPiece == ColoredPiece.WhitePawn || fromPiece == ColoredPiece.BlackPawn || move.Capture)
		{
			newBoard.HalfMoveClock = 0;
		}
		else
		{
			newBoard.HalfMoveClock++;
		}
	}

	private static void UpdateEnPassantPossibility(Board newBoard, ColoredPiece fromPiece, Move move)
	{
		int fromRow = move.From / 8;
		int toRow = move.To / 8;

		if (fromPiece == ColoredPiece.WhitePawn && fromRow == 1 && toRow == 3 ||
			fromPiece == ColoredPiece.BlackPawn && fromRow == 6 && toRow == 4)
		{
			newBoard.EnPassantPossible = true;
			newBoard.EnPassantTargetColumn = (byte)(move.From % 8);
		}
		else
		{
			newBoard.EnPassantPossible = false;
		}
	}

	private static void UpdateCastlingAbility(Board newBoard, Move move)
	{
		switch (move.From)
		{
			case Field.A1FieldNo:
				newBoard.WhiteCanCastleLong = false;
				break;
			case Field.E1FieldNo:
				newBoard.WhiteCanCastleShort = false;
				newBoard.WhiteCanCastleLong = false;
				break;
			case Field.H1FieldNo:
				newBoard.WhiteCanCastleShort = false;
				break;
			case Field.A8FieldNo:
				newBoard.BlackCanCastleLong = false;
				break;
			case Field.E8FieldNo:
				newBoard.BlackCanCastleShort = false;
				newBoard.BlackCanCastleLong = false;
				break;
			case Field.H8FieldNo:
				newBoard.BlackCanCastleShort = false;
				break;
			default:
				break;
		}
		switch (move.To)
		{
			case Field.A1FieldNo:
				newBoard.WhiteCanCastleLong = false;
				break;
			case Field.E1FieldNo:
				newBoard.WhiteCanCastleShort = false;
				newBoard.WhiteCanCastleLong = false;
				break;
			case Field.H1FieldNo:
				newBoard.WhiteCanCastleShort = false;
				break;
			case Field.A8FieldNo:
				newBoard.BlackCanCastleLong = false;
				break;
			case Field.E8FieldNo:
				newBoard.BlackCanCastleShort = false;
				newBoard.BlackCanCastleLong = false;
				break;
			case Field.H8FieldNo:
				newBoard.BlackCanCastleShort = false;
				break;
			default:
				break;
		}
	}

	private static void UpdateSideToMove(Board newBoard)
	{
		newBoard.ToggleColorToMove();
	}

	private static ColoredPiece UpdatePiecePlacement(Board newBoard, Board board, Color colorToMove, Move move)
	{
		var coloredPieceFrom = board.GetColoredPieceAt(move.From, colorToMove);

		ColoredPiece coloredPieceTo;
		if (coloredPieceFrom == ColoredPiece.WhitePawn && move.Capture)
		{
			coloredPieceTo = board.GetColoredPieceAt(move.To, colorToMove.OtherColor());
			if (coloredPieceTo == ColoredPiece.Empty)
			{
				// en passant capture by white
				byte enPassantFieldNo = (byte)((byte)(move.From & (~7)) | (byte)(move.To % Field.TotalColumns));
				newBoard.EmptyField(enPassantFieldNo, ColoredPiece.BlackPawn);
			}
		}
		else if (coloredPieceFrom == ColoredPiece.BlackPawn && move.Capture)
		{
			coloredPieceTo = board.GetColoredPieceAt(move.To, colorToMove.OtherColor());
			if (coloredPieceTo == ColoredPiece.Empty)
			{
				// en passant capture by black
				byte enPassantFieldNo = (byte)((byte)(move.From & (~7)) | (byte)(move.To % Field.TotalColumns));
				newBoard.EmptyField(enPassantFieldNo, ColoredPiece.WhitePawn);
			}
		}
		else if (coloredPieceFrom == ColoredPiece.WhiteKing && move.From == Field.E1FieldNo && move.To == Field.G1FieldNo)
		{
			if (board.WhiteCanCastleShort)
			{
				newBoard.EmptyField(Field.H1FieldNo, ColoredPiece.WhiteRook);
				newBoard.SetColoredPieceAt(Field.F1FieldNo, ColoredPiece.WhiteRook);
			}
			else
			{
				throw new IllegalMoveException();
			}
		}
		else if (coloredPieceFrom == ColoredPiece.WhiteKing && move.From == Field.E1FieldNo && move.To == Field.C1FieldNo)
		{
			if (board.WhiteCanCastleLong)
			{
				newBoard.EmptyField(Field.A1FieldNo, ColoredPiece.WhiteRook);
				newBoard.SetColoredPieceAt(Field.D1FieldNo, ColoredPiece.WhiteRook);
			}
			else
			{
				throw new IllegalMoveException();
			}
		}
		else if (coloredPieceFrom == ColoredPiece.BlackKing && move.From == Field.E8FieldNo && move.To == Field.G8FieldNo)
		{
			if (board.BlackCanCastleShort)
			{
				newBoard.EmptyField(Field.H8FieldNo, ColoredPiece.BlackRook);
				newBoard.SetColoredPieceAt(Field.F8FieldNo, ColoredPiece.BlackRook);
			}
			else
			{
				throw new IllegalMoveException();
			}
		}
		else if (coloredPieceFrom == ColoredPiece.BlackKing && move.From == Field.E8FieldNo && move.To == Field.C8FieldNo)
		{
			if (board.BlackCanCastleLong)
			{
				newBoard.EmptyField(Field.A8FieldNo, ColoredPiece.BlackRook);
				newBoard.SetColoredPieceAt(Field.D8FieldNo, ColoredPiece.BlackRook);
			}
			else
			{
				throw new IllegalMoveException();
			}
		}

		if (coloredPieceFrom == ColoredPiece.Empty)
		{
			throw new IllegalMoveException("No Piece at from-Field");
		}


		newBoard.EmptyField(move.From, coloredPieceFrom);
		if (move.Capture)
		{
			newBoard.EmptyField(move.To, colorToMove.OtherColor());
		}

		var pieceToPlace = move.PromotedTo == Piece.None ? coloredPieceFrom : move.PromotedTo.ToColoredPiece(colorToMove);
		newBoard.SetColoredPieceAt(move.To, pieceToPlace);

		return coloredPieceFrom;
	}
}
