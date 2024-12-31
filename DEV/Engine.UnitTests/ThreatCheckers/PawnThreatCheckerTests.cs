using FluentAssertions;
using Minakuru.Engine.ThreatCheckers;

namespace Minakuru.Engine.UnitTests.ThreatCheckers;

[TestClass]
public class PawnThreatCheckerTests
{
	[DataRow(ColoredPiece.BlackPawn, "c3", false, DisplayName = "pc3")]
	[DataRow(ColoredPiece.BlackPawn, "c4", false, DisplayName = "pc4")]
	[DataRow(ColoredPiece.BlackPawn, "c5", true, DisplayName = "pc5")]
	[DataRow(ColoredPiece.BlackPawn, "d3", false, DisplayName = "pd3")]
	[DataRow(ColoredPiece.BlackPawn, "d5", false, DisplayName = "pd5")]
	[DataRow(ColoredPiece.BlackPawn, "e3", false, DisplayName = "pe3")]
	[DataRow(ColoredPiece.BlackPawn, "e4", false, DisplayName = "pe4")]
	[DataRow(ColoredPiece.BlackPawn, "e5", true, DisplayName = "pe5")]
	[DataRow(ColoredPiece.BlackPawn, "b2", false, DisplayName = "pb2")]
	[DataRow(ColoredPiece.BlackPawn, "f4", false, DisplayName = "pf4")]
	[DataTestMethod]
	public void WhiteKingOnD4Tests(ColoredPiece coloredPiece, string fieldName, bool expected)
	{
		var fen = "8/8/8/8/3K4/8/8/8 w - - 0 1";
		var board = fen.ToBoard();
		var sut = new PawnThreatChecker();
		byte kingFieldNo = "d4".ToFieldNo();
		byte attackerFieldNo = fieldName.ToFieldNo();
		board.SetColoredPieceAt(attackerFieldNo, coloredPiece);

		var actual = sut.IsUnderAttack(board, kingFieldNo, Color.Black);

		actual.Should().Be(expected);
	}

	[DataRow(ColoredPiece.WhitePawn, "c3", true, DisplayName = "pc3")]
	[DataRow(ColoredPiece.WhitePawn, "c4", false, DisplayName = "pc4")]
	[DataRow(ColoredPiece.WhitePawn, "c5", false, DisplayName = "pc5")]
	[DataRow(ColoredPiece.WhitePawn, "d3", false, DisplayName = "pd3")]
	[DataRow(ColoredPiece.WhitePawn, "d5", false, DisplayName = "pd5")]
	[DataRow(ColoredPiece.WhitePawn, "e3", true, DisplayName = "pe3")]
	[DataRow(ColoredPiece.WhitePawn, "e4", false, DisplayName = "pe4")]
	[DataRow(ColoredPiece.WhitePawn, "e5", false, DisplayName = "pe5")]
	[DataRow(ColoredPiece.WhitePawn, "b2", false, DisplayName = "pb2")]
	[DataRow(ColoredPiece.WhitePawn, "f4", false, DisplayName = "pf4")]
	[DataTestMethod]
	public void BlackKingOnD4Tests(ColoredPiece coloredPiece, string fieldName, bool expected)
	{
		var fen = "8/8/8/8/3k4/8/8/8 w - - 0 1";
		var board = fen.ToBoard();
		var sut = new PawnThreatChecker();
		byte kingFieldNo = "d4".ToFieldNo();
		byte attackerFieldNo = fieldName.ToFieldNo();
		board.SetColoredPieceAt(attackerFieldNo, coloredPiece);

		var actual = sut.IsUnderAttack(board, kingFieldNo, Color.White);

		actual.Should().Be(expected);
	}
}

