using Shouldly;

namespace Minakuru.Engine.UnitTests;

[TestClass]
public class MoveMakerTests
{
	[TestMethod]
	public void StartWithD2D4()
	{
		var board = Board.Init();

		Move move = new(Field.D2FieldNo, Field.D4FieldNo);
		var newBoard = MoveMaker.MakeMove(board, move);

		var newFen = newBoard.ToFen();
		var expected = "rnbqkbnr/pppppppp/8/8/3P4/8/PPP1PPPP/RNBQKBNR b KQkq d3 0 1";
		newFen.ShouldBe(expected);
	}

	[TestMethod]
	public void WhiteKingMovesInhibitsCastling()
	{
		var originalFen = "rnbqkbnr/ppp1pppp/8/3p4/3P4/8/PPP1PPPP/R3K2R w KQkq d6 0 1";
		var board = originalFen.ToBoard();

		Move move = new(Field.E1FieldNo, Field.D2FieldNo);
		var newBoard = MoveMaker.MakeMove(board, move);

		var newFen = newBoard.ToFen();
		var expected = "rnbqkbnr/ppp1pppp/8/3p4/3P4/8/PPPKPPPP/R6R b kq - 1 1";
		newFen.ShouldBe(expected);
	}

	[TestMethod]
	public void WhiteRookA1MovesInhibitsLongCastling()
	{
		var originalFen = "7k/7p/8/8/8/8/8/R3K2R w KQ - 0 1";
		var board = originalFen.ToBoard();

		Move move = new(Field.A1FieldNo, Field.B1FieldNo);
		var newBoard = MoveMaker.MakeMove(board, move);

		var newFen = newBoard.ToFen();
		var expected = "7k/7p/8/8/8/8/8/1R2K2R b K - 1 1";
		newFen.ShouldBe(expected);
	}

	[TestMethod]
	public void WhiteRookH1MovesInhibitsLongCastling()
	{
		var originalFen = "7k/7p/8/8/8/8/8/R3K2R w KQ - 0 1";
		var board = originalFen.ToBoard();

		Move move = new(Field.H1FieldNo, Field.G1FieldNo);
		var newBoard = MoveMaker.MakeMove(board, move);

		var newFen = newBoard.ToFen();
		var expected = "7k/7p/8/8/8/8/8/R3K1R1 b Q - 1 1";
		newFen.ShouldBe(expected);
	}

	[TestMethod]
	public void WhiteShortCastlingInhibitsWhiteCastling()
	{
		var originalFen = "7k/7p/8/8/8/8/8/R3K2R w KQ - 0 1";
		var board = originalFen.ToBoard();

		Move move = new(Field.E1FieldNo, Field.G1FieldNo);
		var newBoard = MoveMaker.MakeMove(board, move);

		var newFen = newBoard.ToFen();
		var expected = "7k/7p/8/8/8/8/8/R4RK1 b - - 1 1";
		newFen.ShouldBe(expected);
	}

	[TestMethod]
	public void WhiteLongCastlingInhibitsWhiteCastling()
	{
		var originalFen = "7k/7p/8/8/8/8/8/R3K2R w KQ - 0 1";
		var board = originalFen.ToBoard();

		Move move = new(Field.E1FieldNo, Field.C1FieldNo);
		var newBoard = MoveMaker.MakeMove(board, move);

		var newFen = newBoard.ToFen();
		var expected = "7k/7p/8/8/8/8/8/2KR3R b - - 1 1";
		newFen.ShouldBe(expected);
	}

	[TestMethod]
	public void BlackKingMovesInhibitsCastling()
	{
		var originalFen = "r3k2r/8/8/8/8/8/4P3/4K3 b kq - 1 1";
		var board = originalFen.ToBoard();

		Move move = new(Field.E8FieldNo, Field.E7FieldNo);
		var newBoard = MoveMaker.MakeMove(board, move);

		var newFen = newBoard.ToFen();
		var expected = "r6r/4k3/8/8/8/8/4P3/4K3 w - - 2 2";
		newFen.ShouldBe(expected);
	}

	[TestMethod]
	public void BlackRookA8MovesInhibitsLongCastling()
	{
		var originalFen = "r3k2r/8/8/8/8/8/4P3/4K3 b kq - 1 1";
		var board = originalFen.ToBoard();

		Move move = new(Field.A8FieldNo, Field.A6FieldNo);
		var newBoard = MoveMaker.MakeMove(board, move);

		var newFen = newBoard.ToFen();
		var expected = "4k2r/8/r7/8/8/8/4P3/4K3 w k - 2 2";
		newFen.ShouldBe(expected);
	}

	[TestMethod]
	public void BlackRookH8MovesInhibitsLongCastling()
	{
		var originalFen = "r3k2r/8/8/8/8/8/4P3/4K3 b kq - 1 1";
		var board = originalFen.ToBoard();

		Move move = new(Field.H8FieldNo, Field.H7FieldNo);
		var newBoard = MoveMaker.MakeMove(board, move);

		var newFen = newBoard.ToFen();
		var expected = "r3k3/7r/8/8/8/8/4P3/4K3 w q - 2 2";
		newFen.ShouldBe(expected);
	}

	[TestMethod]
	public void BlackShortCastlingInhibitsCastling()
	{
		var originalFen = "r3k2r/8/8/8/8/8/4P3/4K3 b kq - 1 1";
		var board = originalFen.ToBoard();

		Move move = new(Field.E8FieldNo, Field.G8FieldNo);
		var newBoard = MoveMaker.MakeMove(board, move);

		var newFen = newBoard.ToFen();
		var expected = "r4rk1/8/8/8/8/8/4P3/4K3 w - - 2 2";
		newFen.ShouldBe(expected);
	}

	[TestMethod]
	public void BlackLongCastlingInhibitsCastling()
	{
		var originalFen = "r3k2r/8/8/8/8/8/4P3/4K3 b kq - 1 1";
		var board = originalFen.ToBoard();

		Move move = new(Field.E8FieldNo, Field.C8FieldNo);
		var newBoard = MoveMaker.MakeMove(board, move);

		var newFen = newBoard.ToFen();
		var expected = "2kr3r/8/8/8/8/8/4P3/4K3 w - - 2 2";
		newFen.ShouldBe(expected);
	}

	[TestMethod]
	public void PawnCapturesEnPassantToHigherColumnTest()
	{
		var originalFen = "7k/8/3p4/3Pp3/8/8/8/K7 w - e6 0 1";
		var originalBoard = originalFen.ToBoard();

		var readableMove = new ReadableMove("d5", "e6", true);
		var move = readableMove.ToMove();

		var newBoard = MoveMaker.MakeMove(originalBoard, move);

		var actualFen = newBoard.ToFen();
		var expectedFen = "7k/8/3pP3/8/8/8/8/K7 b - - 0 1";

		actualFen.ShouldBe(expectedFen);
	}

	[TestMethod]
	public void PawnCapturesAndPromotesToQueenToLowerColumnTest()
	{
		var originalFen = "n1n5/PPPk4/8/8/8/8/4Kppp/5N1N w - - 0 1";
		var originalBoard = originalFen.ToBoard();

		var readableMove = new ReadableMove("b7", "a8", true, Piece.Queen);
		var move = readableMove.ToMove();

		var newBoard = MoveMaker.MakeMove(originalBoard, move);

		var actualFen = newBoard.ToFen();
		var expectedFen = "Q1n5/P1Pk4/8/8/8/8/4Kppp/5N1N b - - 0 1";

		actualFen.ShouldBe(expectedFen);
	}

	[TestMethod]
	public void WhiteCapturesA8InhibitsBlackLongCastlingTest()
	{
		var originalFen = "r3k3/1K6/8/8/8/8/8/8 w q - 0 1";
		var originalBoard = originalFen.ToBoard();

		var readableMove = new ReadableMove("b7", "a8", true);
		var move = readableMove.ToMove();

		var newBoard = MoveMaker.MakeMove(originalBoard, move);

		var actualFen = newBoard.ToFen();
		var expectedFen = "K3k3/8/8/8/8/8/8/8 b - - 0 1";

		actualFen.ShouldBe(expectedFen);
	}
}
