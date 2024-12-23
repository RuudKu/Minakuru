﻿namespace Engine
{
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

			if ((fromPiece == ColoredPiece.WhitePawn && fromRow == 1 && toRow == 3) ||
				(fromPiece == ColoredPiece.BlackPawn && fromRow == 6 && toRow == 4))
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
		}

		private static void UpdateSideToMove(Board newBoard)
		{
			newBoard.ToggleColorToMove();
		}

		private static ColoredPiece UpdatePiecePlacement(Board newBoard, Board board, Color colorToMove, Move move)
		{
			var coloredPieceFrom = board.GetColoredPieceAt(move.From, colorToMove);

			if (!coloredPieceFrom.HasValue)
			{
				throw new Exception("No Piece at from-Field");
			}

			newBoard.EmptyField(move.From, coloredPieceFrom.Value);
			newBoard.SetColoredPieceAt(move.To, coloredPieceFrom.Value);

			return coloredPieceFrom.Value;
		}
	}
}
