using Shouldly;
using Minakuru.Engine.Evaluators;
using Minakuru.Engine.MoveGenerators;
using Minakuru.Engine.MoveSearchers;

namespace Minakuru.Engine.UnitTests.MoveSearchers;

[TestClass]
public class AlphaBetaTests
{
	private IMoveSearcher _moveSearcher;
	private readonly MoveSearchOptions options3 = new(3);
	private readonly MoveSearchOptions options4 = new(4);
	private readonly MoveSearchOptions options6 = new(6);

	[TestInitialize]
	public void TestInitialize()
	{
		_moveSearcher = new AlphaBeta(
			EvaluatorFactory.Create(),
			MoveGeneratorFactory.Create());
	}

	[TestMethod]
	public void WhiteCheckmatesOverMaterialGain()
	{
		var fen = "2bkr3/ppp2ppp/4p3/8/1b6/8/PPPB1PP1/2KR3q w - - 0 1";
		var board = fen.ToBoard();

		var (actualMove, actualScore) = _moveSearcher.Search(options3, board);

		var expectedMove = new Move(Field.D2FieldNo, Field.G5FieldNo);
		var expectedScore = EvaluationConstants.Mate;

		actualMove.ShouldBe(expectedMove);
		actualScore.ShouldBe(expectedScore);
	}

	[TestMethod]
	public void WhiteMinorPromotionCheckmates()
	{
		var fen = "QQQQn1nb/5Pnk/6bn/8/8/8/8/K7 w - - 0 1";
		var board = fen.ToBoard();

		var (actualMove, actualScore) = _moveSearcher.Search(options3, board);

		var expectedMove = new Move(Field.F7FieldNo, Field.F8FieldNo, false, Piece.Knight);
		var expectedScore = EvaluationConstants.Mate;

		actualMove.ShouldBe(expectedMove);
		actualScore.ShouldBe(expectedScore);
	}

	[TestMethod]
	public void WhitePawnCheckmates()
	{
		var fen = "r1b3nr/ppqk1B1p/2pp4/4PnB1/8/3P4/PPP3PP/R4RK1 w - - 0 1";
		var board = fen.ToBoard();

		var (actualMove, actualScore) = _moveSearcher.Search(options3, board);

		var expectedMove = new Move(Field.E5FieldNo, Field.E6FieldNo);
		var expectedScore = EvaluationConstants.Mate;

		actualMove.ShouldBe(expectedMove);
		actualScore.ShouldBe(expectedScore);
	}

	[TestMethod]
	public void BlackQueenMoveAndPromotionCheckmates()
	{
		var fen = "7k/8/8/8/8/4q3/4p3/4K3 b - - 0 1";
		var board = fen.ToBoard();

		var (actualMove, actualScore) = _moveSearcher.Search(options4, board);

		var expectedMove = new Move(Field.E3FieldNo, Field.E4FieldNo);
		var expectedScore = EvaluationConstants.Mate;

		actualMove.ShouldBe(expectedMove);
		actualScore.ShouldBe(expectedScore);
	}

	// [TestMethod]
	public void WhiteKnightSacrificeLeadsToCheckmates()
	{
		var fen = "r2qkb1r/pp2nppp/3p4/2pNN1B1/2BnP3/3P4/PPP2PPP/R2bK2R w KQkq - 1 0";
		var board = fen.ToBoard();

		var (actualMove, actualScore) = _moveSearcher.Search(options3, board);

		var expectedMove = new Move(Field.D5FieldNo, Field.F6FieldNo);
		var expectedScore = EvaluationConstants.Mate;

		actualMove.ShouldBe(expectedMove);
		actualScore.ShouldBe(expectedScore);
	}

	[TestMethod]
	public void WhiteRookSacrificeLeadsToCheckmates()
	{
		var fen = "5Kbk/6pp/6P1/8/8/8/8/7R w - - 0 1";
		var board = fen.ToBoard();

		var (actualMove, actualScore) = _moveSearcher.Search(options4, board);

		var expectedMove = new Move(Field.H1FieldNo, Field.H6FieldNo);
		var expectedScore = EvaluationConstants.Mate;

		actualMove.ShouldBe(expectedMove);
		actualScore.ShouldBe(expectedScore);
	}

	[TestMethod]
	public void WhiteSilentQueenMoveLeadsToMate()
	{
		var fen = "rk2K3/NPR5/8/8/8/8/8/4Q3 w - - 0 1";
		var board = fen.ToBoard();

		var (actualMove, actualScore) = _moveSearcher.Search(options4, board);

		var expectedMove = new Move(Field.E1FieldNo, Field.B4FieldNo);
		var expectedScore = EvaluationConstants.Mate;

		actualMove.ShouldBe(expectedMove);
		actualScore.ShouldBe(expectedScore);
	}

	[TestMethod]
	public void WhiteSilentQueenMoveLeadsToMate2()
	{
		var fen = "8/8/8/8/K7/8/pp1Q4/k7 w - - 0 1";
		var board = fen.ToBoard();

		var (actualMove, actualScore) = _moveSearcher.Search(options4, board);

		var expectedMove = new Move(Field.D2FieldNo, Field.D4FieldNo);
		var expectedScore = EvaluationConstants.Mate;

		actualMove.ShouldBe(expectedMove);
		actualScore.ShouldBe(expectedScore);
	}

	[TestMethod]
	public void WhiteRookSacrificeLeadsToMate()
	{
		var fen = "k1K5/qp5p/2p3pP/2Pp1pP1/B2PpP2/4P3/8/RR6 w - - 0 1";
		var board = fen.ToBoard();

		var (actualMove, actualScore) = _moveSearcher.Search(options4, board);

		var expectedMove = new Move(Field.A4FieldNo, Field.C6FieldNo, true);
		var expectedScore = EvaluationConstants.Mate;

		actualMove.ShouldBe(expectedMove);
		actualScore.ShouldBe(expectedScore);
	}

	// [TestMethod]
	public void BlackRookSacrificeLeadsToDoubleCheckMate()
	{
		var fen = "6k1/pp4p1/2p5/2bp4/8/P5Pb/1P3rrP/2BRRN1K b - - 0 1";
		var board = fen.ToBoard();

		var (actualMove, actualScore) = _moveSearcher.Search(options3, board);

		var expectedMove = new Move(Field.G2FieldNo, Field.G1FieldNo, false);
		var expectedScore = EvaluationConstants.Mate;

		actualMove.ShouldBe(expectedMove);
		actualScore.ShouldBe(expectedScore);
	}

	[TestMethod]
	public void WhiteBishopSacrificeLeadsToDoubleCheckMate()
	{
		var fen = "Q5bk/6p1/6P1/8/8/8/8/2B4K w - - 0 1";
		var board = fen.ToBoard();

		var (actualMove, actualScore) = _moveSearcher.Search(options4, board);

		var expectedMove = new Move(Field.C1FieldNo, Field.H6FieldNo, false);
		var expectedScore = EvaluationConstants.Mate;

		actualMove.ShouldBe(expectedMove);
		actualScore.ShouldBe(expectedScore);
	}

	[TestMethod]
	public void WhiteRookSacrificeLeadsToDoubleCheckMate()
	{
		var fen = "8/8/8/8/8/7N/6Rp/5Kbk w - - 0 1";
		var board = fen.ToBoard();

		var (actualMove, actualScore) = _moveSearcher.Search(options4, board);

		var expectedMove = new Move(Field.G2FieldNo, Field.F2FieldNo, false);
		var expectedScore = EvaluationConstants.Mate;

		actualMove.ShouldBe(expectedMove);
		actualScore.ShouldBe(expectedScore);
	}

	// [TestMethod]
	public void WhiteQueenSacrificeLeadsToCheckMate()
	{
		var fen = "r2qkbnr/1bppppp1/1pn5/p3P3/8/1QPB1N1P/PP1P1P2/RNB1R1K1 w kq - 0 1";
		var board = fen.ToBoard();

		var (actualMove, actualScore) = _moveSearcher.Search(options6, board);

		var expectedMove = new Move(Field.B3FieldNo, Field.F7FieldNo, false);
		var expectedScore = EvaluationConstants.Mate;

		actualMove.ShouldBe(expectedMove);
		actualScore.ShouldBe(expectedScore);
	}

}
