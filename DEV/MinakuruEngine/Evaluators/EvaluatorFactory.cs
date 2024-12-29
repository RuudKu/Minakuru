using Minakuru.Engine.MoveGenerators;
using Minakuru.Engine.ThreatCheckers;

namespace Minakuru.Engine.Evaluators;

public static class EvaluatorFactory
{
	public static IEvaluator Create()
	{
		return new Evaluator([
			new MateStalemateEvaluator(
				new LegalMovesGenerator(new PseudoLegalMoveGenerator(), ThreatCheckerFactory.Create()), ThreatCheckerFactory.Create()),
			new SimpleMaterialEvaluator()]);
	}
}
