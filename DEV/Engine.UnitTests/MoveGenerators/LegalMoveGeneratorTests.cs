using System.Drawing.Text;
using FluentAssertions;
using Minakuru.Engine.MoveGenerators;

namespace Minakuru.Engine.UnitTests.MoveGenerators;

[TestClass]
public class LegalMoveGeneratorTests
{
	[TestMethod]
	public void OneMoveBecausePinsTest()
	{
		var fen = "b6r/7B/2R5/8/8/6k1/8/q1N4K w - - 0 1";
		var board = fen.ToBoard();

		var legalMovesGenerator = new LegalMovesGenerator();

		var allMoves = legalMovesGenerator.GenerateMove(board, Color.White);
		var allReadableMoves = allMoves.ToReadableMoves().ToArray();

		var expected = new ReadableMove[]
		{
			new("h1", "g1")
		};

		allReadableMoves.Should().BeEquivalentTo(expected);
	}

	[TestMethod]
	public void OnlyPromotionsBecausePinsTest()
	{
		var fen = "7K/r4P2/2k5/8/8/8/8/1b4r1 w - - 0 1";
		var board = fen.ToBoard();

		var legalMovesGenerator = new LegalMovesGenerator();

		var allMoves = legalMovesGenerator.GenerateMove(board, Color.White);
		var allReadableMoves = allMoves.ToReadableMoves().ToArray();

		var expected = new ReadableMove[]
		{
			new("f7", "f8", false, Piece.Queen),
			new("f7", "f8", false, Piece.Rook),
			new("f7", "f8", false, Piece.Bishop),
			new("f7", "f8", false, Piece.Knight),
		};

		allReadableMoves.Should().BeEquivalentTo(expected);
	}

	[TestMethod]
	public void OnlyPromotionsWithCapturesBecausePinsTest()
	{
		var fen = "4nnnK/r4P2/2k5/8/8/8/8/1b4r1 w - - 0 1";
		var board = fen.ToBoard();

		var legalMovesGenerator = new LegalMovesGenerator();

		var allMoves = legalMovesGenerator.GenerateMove(board, Color.White);
		var allReadableMoves = allMoves.ToReadableMoves().ToArray();

		var expected = new ReadableMove[]
		{
			new("f7", "e8", true, Piece.Queen),
			new("f7", "e8", true, Piece.Rook),
			new("f7", "e8", true, Piece.Bishop),
			new("f7", "e8", true, Piece.Knight),
			new("f7", "g8", true, Piece.Queen),
			new("f7", "g8", true, Piece.Rook),
			new("f7", "g8", true, Piece.Bishop),
			new("f7", "g8", true, Piece.Knight)
		};

		allReadableMoves.Should().BeEquivalentTo(expected);
	}

	[DataRow(Field.A7FieldNo, true, true, DisplayName = "Rook a7 allows both sides")]
	[DataRow(Field.B7FieldNo, true, true, DisplayName = "Rook b7 allows both sides")]
	[DataRow(Field.C7FieldNo, false, true, DisplayName = "Rook c7 prevents long castling")]
	[DataRow(Field.D7FieldNo, false, true, DisplayName = "Rook d7 prevents long castling")]
	[DataRow(Field.E7FieldNo, false, false, DisplayName = "Rook e7 prevents castling")]
	[DataRow(Field.F7FieldNo, true, false, DisplayName = "Rook f7 prevents short castling")]
	[DataRow(Field.G7FieldNo, true, false, DisplayName = "Rook g7 prevents short castling")]
	[DataRow(Field.H7FieldNo, true, true, DisplayName = "Rook h7 allows both sides")]
	[DataTestMethod]
	public void WhiteCastlingOnlyWithoutSelfCheckTest(byte extraRookFieldNo, bool longCastlingAllowed, bool shortCastlingAllowed)
	{
		var fen = "4k3/8/8/8/8/8/8/R3K2R w KQ - 0 1";
		var board = fen.ToBoard();

		board.SetColoredPieceAt(extraRookFieldNo, ColoredPiece.BlackRook);

		var legalMovesGenerator = new LegalMovesGenerator();

		var allMoves = legalMovesGenerator.GenerateMove(board, Color.White);
		var allReadableMoves = allMoves.ToReadableMoves().ToArray();
		bool actualShortCastling = allReadableMoves.Any(m => m.From == "e1" && m.To == "g1");
		bool actualLongCastling = allReadableMoves.Any(m => m.From == "e1" && m.To == "c1");

		var expectedCastlingOptions = new CastlingOptions(longCastlingAllowed, shortCastlingAllowed);
		var actualCastlingOptions = new CastlingOptions(actualLongCastling, actualShortCastling);
		actualCastlingOptions.Should().BeEquivalentTo(expectedCastlingOptions);
	}

	[DataRow(Field.A7FieldNo, true, true, DisplayName = "Rook a2 allows both sides")]
	[DataRow(Field.B7FieldNo, true, true, DisplayName = "Rook b2 allows both sides")]
	[DataRow(Field.C7FieldNo, false, true, DisplayName = "Rook c2 prevents long castling")]
	[DataRow(Field.D7FieldNo, false, true, DisplayName = "Rook d2 prevents long castling")]
	[DataRow(Field.E7FieldNo, false, false, DisplayName = "Rook e2 prevents castling")]
	[DataRow(Field.F7FieldNo, true, false, DisplayName = "Rook f2 prevents short castling")]
	[DataRow(Field.G7FieldNo, true, false, DisplayName = "Rook g2 prevents short castling")]
	[DataRow(Field.H7FieldNo, true, true, DisplayName = "Rook h2 allows both sides")]
	[DataTestMethod]
	public void BlackCastlingOnlyWithoutSelfCheckTest(byte extraRookFieldNo, bool longCastlingAllowed, bool shortCastlingAllowed)
	{
		var fen = "r3k2r/8/8/8/8/8/8/4K3 b kq - 0 1";
		var board = fen.ToBoard();

		board.SetColoredPieceAt(extraRookFieldNo, ColoredPiece.WhiteRook);

		var legalMovesGenerator = new LegalMovesGenerator();

		var allMoves = legalMovesGenerator.GenerateMove(board, Color.Black);
		var allReadableMoves = allMoves.ToReadableMoves().ToArray();
		bool actualShortCastling = allReadableMoves.Any(m => m.From == "e8" && m.To == "g8");
		bool actualLongCastling = allReadableMoves.Any(m => m.From == "e8" && m.To == "c8");

		var expectedCastlingOptions = new CastlingOptions(longCastlingAllowed, shortCastlingAllowed);
		var actualCastlingOptions = new CastlingOptions(actualLongCastling, actualShortCastling);
		actualCastlingOptions.Should().BeEquivalentTo(expectedCastlingOptions);
	}

	record CastlingOptions(bool Long, bool Short)
	{
	}
}
