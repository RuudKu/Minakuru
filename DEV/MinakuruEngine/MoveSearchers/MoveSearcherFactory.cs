using Minakuru.Engine.Evaluators;
using Minakuru.Engine.MoveGenerators;

namespace Minakuru.Engine.MoveSearchers;
public static class MoveSearcherFactory
{
	public static IMoveSearcher Create()
	{
		return new AlphaBeta(EvaluatorFactory.Create(), MoveGeneratorFactory.Create());
	}
}
