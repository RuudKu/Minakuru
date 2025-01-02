using Minakuru.Engine.Fields;

namespace Minakuru.Engine.MoveGenerators;

public class KingMoveGenerator : IMoveGenerator
{
	private static readonly byte[][] _fieldNos = KingFieldNo.KingFieldNos;

	public void GenerateMove(Board board, MoveList moveList)
	{
		var color = board.ColorToMove;
		var whitePiecesAt = board.WhitePieces;
		var blackPiecesAt = board.BlackPieces;

		var ownPiecesAt = color == Color.White ? whitePiecesAt : blackPiecesAt;
		var opponentPiecesAt = color == Color.White ? blackPiecesAt : whitePiecesAt;

		byte kingAt = board.KingAt(color);
		var fieldNos = _fieldNos[kingAt];

		AddNormalMoves();
		AddCastling();

		void AddNormalMoves()
		{
			for (int i = 0; i < fieldNos.Length; i++)
			{
				var fieldNo = fieldNos[i];
				ulong toFilter = (ulong)1 << fieldNo;
				if ((ownPiecesAt & toFilter) == 0)
				{
					bool isCapture = (opponentPiecesAt & toFilter) != 0;
					moveList.Add(new Move(kingAt, fieldNo, isCapture));
				}
			}
		}

		void AddCastling()
		{
			switch (color)
			{
				case Color.White:
					if (board.WhiteCanCastleShort && board.IsEmpty(Field.F1FieldNo) && board.IsEmpty(Field.G1FieldNo))
					{
						moveList.Add(new Move(Field.E1FieldNo, Field.G1FieldNo));
					}
					if (board.WhiteCanCastleLong && board.IsEmpty(Field.D1FieldNo) && board.IsEmpty(Field.C1FieldNo) && board.IsEmpty(Field.B1FieldNo))
					{
						moveList.Add(new Move(Field.E1FieldNo, Field.C1FieldNo));
					}
					break;
				case Color.Black:
					if (board.BlackCanCastleShort && board.IsEmpty(Field.F8FieldNo) && board.IsEmpty(Field.G8FieldNo))
					{
						moveList.Add(new Move(Field.E8FieldNo, Field.G8FieldNo));
					}
					if (board.BlackCanCastleLong && board.IsEmpty(Field.D8FieldNo) && board.IsEmpty(Field.C8FieldNo) && board.IsEmpty(Field.B8FieldNo))
					{
						moveList.Add(new Move(Field.E8FieldNo, Field.C8FieldNo));
					}
					break;
				default:
					throw new NotSupportedException();
			}
		}
	}
}
