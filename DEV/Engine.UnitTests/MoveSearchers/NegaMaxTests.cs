using FluentAssertions;
using Minakuru.Engine.Evaluators;
using Minakuru.Engine.MoveGenerators;
using Minakuru.Engine.MoveSearchers;

namespace Minakuru.Engine.UnitTests.MoveSearchers;

[TestClass]
public class NegaMaxTests
{
	[TestMethod]
	public void WhiteCheckmatesOverMaterialGain()
	{
		var fen = "2bkr3/ppp2ppp/4p3/8/1b6/8/PPPB1PP1/2KR3q w - - 0 1";
		var board = fen.ToBoard();

		var negaMax = new NegaMax(new Evaluator([new MateStalemateEvaluator(), new SimpleMaterialEvaluator()]), new LegalMovesGenerator());
		var (actualMove, actualScore) = negaMax.Search(board);

		var expectedMove = new Move(Field.D2FieldNo, Field.G5FieldNo);
		var expectedScore = EvaluationConstants.Mate;

		actualMove.Should().Be(expectedMove);
		actualScore.Should().Be(expectedScore);
	}
}
