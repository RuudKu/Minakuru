﻿using FluentAssertions;

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
		newFen.Should().Be(expected);
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
		newFen.Should().Be(expected);
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
		newFen.Should().Be(expected);
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
		newFen.Should().Be(expected);
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
		newFen.Should().Be(expected);
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
		newFen.Should().Be(expected);
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
		newFen.Should().Be(expected);
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

		actualFen.Should().Be(expectedFen);
	}
}
