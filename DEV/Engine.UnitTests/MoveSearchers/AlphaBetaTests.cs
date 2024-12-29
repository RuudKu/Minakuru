﻿using FluentAssertions;
using Minakuru.Engine.Evaluators;
using Minakuru.Engine.MoveGenerators;
using Minakuru.Engine.MoveSearchers;

namespace Minakuru.Engine.UnitTests.MoveSearchers;

[TestClass]
public class AlphaBetaTests
{
	[TestMethod]
	public void WhiteCheckmatesOverMaterialGain()
	{
		var fen = "2bkr3/ppp2ppp/4p3/8/1b6/8/PPPB1PP1/2KR3q w - - 0 1";
		var board = fen.ToBoard();

		var alphaBeta = new AlphaBeta(
			new MoveSearchOptions(4),
			new Evaluator([new MateStalemateEvaluator(), new SimpleMaterialEvaluator()]),
			new LegalMovesGenerator());
		var (actualMove, actualScore) = alphaBeta.Search(board);

		var expectedMove = new Move(Field.D2FieldNo, Field.G5FieldNo);
		var expectedScore = EvaluationConstants.Mate;

		actualMove.Should().Be(expectedMove);
		actualScore.Should().Be(expectedScore);
	}

	[TestMethod]
	public void WhiteMinorPromotionCheckmates()
	{
		var fen = "QQQQn1nb/5Pnk/6bn/8/8/8/8/K7 w - - 0 1";
		var board = fen.ToBoard();

		var alphaBeta = new AlphaBeta(
			new MoveSearchOptions(3),
			new Evaluator([new MateStalemateEvaluator(), new SimpleMaterialEvaluator()]),
			new LegalMovesGenerator());
		var (actualMove, actualScore) = alphaBeta.Search(board);

		var expectedMove = new Move(Field.F7FieldNo, Field.F8FieldNo, false, Piece.Knight);
		var expectedScore = EvaluationConstants.Mate;

		actualMove.Should().Be(expectedMove);
		actualScore.Should().Be(expectedScore);
	}

	[TestMethod]
	public void WhitePawnCheckmates()
	{
		var fen = "r1b3nr/ppqk1B1p/2pp4/4PnB1/8/3P4/PPP3PP/R4RK1 w - - 0 1";
		var board = fen.ToBoard();

		var alphaBeta = new AlphaBeta(
			new MoveSearchOptions(3),
			new Evaluator([new MateStalemateEvaluator(), new SimpleMaterialEvaluator()]),
			new LegalMovesGenerator());
		var (actualMove, actualScore) = alphaBeta.Search(board);

		var expectedMove = new Move(Field.E5FieldNo, Field.E6FieldNo);
		var expectedScore = EvaluationConstants.Mate;

		actualMove.Should().Be(expectedMove);
		actualScore.Should().Be(expectedScore);
	}

	// [TestMethod]
	public void WhiteKnightSacrificeLeadsToCheckmates()
	{
		var fen = "r2qkb1r/pp2nppp/3p4/2pNN1B1/2BnP3/3P4/PPP2PPP/R2bK2R w KQkq - 1 0";
		var board = fen.ToBoard();

		var alphaBeta = new AlphaBeta(
			new MoveSearchOptions(4),
			new Evaluator([new MateStalemateEvaluator(), new SimpleMaterialEvaluator()]),
			new LegalMovesGenerator());
		var (actualMove, actualScore) = alphaBeta.Search(board);

		var expectedMove = new Move(Field.D5FieldNo, Field.F6FieldNo);
		var expectedScore = EvaluationConstants.Mate;

		actualMove.Should().Be(expectedMove);
		actualScore.Should().Be(expectedScore);
	}

	[TestMethod]
	public void WhiteRookSacrificeLeadsToCheckmates()
	{
		var fen = "5Kbk/6pp/6P1/8/8/8/8/7R w - - 0 1";
		var board = fen.ToBoard();

		var alphaBeta = new AlphaBeta(
			new MoveSearchOptions(4),
			new Evaluator([new MateStalemateEvaluator(), new SimpleMaterialEvaluator()]),
			new LegalMovesGenerator());
		var (actualMove, actualScore) = alphaBeta.Search(board);

		var expectedMove = new Move(Field.H1FieldNo, Field.H6FieldNo);
		var expectedScore = EvaluationConstants.Mate;

		actualMove.Should().Be(expectedMove);
		actualScore.Should().Be(expectedScore);
	}

	[TestMethod]
	public void WhiteSilentQueenMoveLeadsToMate()
	{
		var fen = "rk2K3/NPR5/8/8/8/8/8/4Q3 w - - 0 1";
		var board = fen.ToBoard();

		var alphaBeta = new AlphaBeta(
			new MoveSearchOptions(4),
			new Evaluator([new MateStalemateEvaluator(), new SimpleMaterialEvaluator()]),
			new LegalMovesGenerator());
		var (actualMove, actualScore) = alphaBeta.Search(board);

		var expectedMove = new Move(Field.E1FieldNo, Field.B4FieldNo);
		var expectedScore = EvaluationConstants.Mate;

		actualMove.Should().Be(expectedMove);
		actualScore.Should().Be(expectedScore);
	}
}