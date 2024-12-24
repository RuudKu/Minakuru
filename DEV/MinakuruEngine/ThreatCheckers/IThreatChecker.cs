
namespace Engine.ThreatCheckers
{
	public interface IThreatChecker
	{
		bool IsUnderAttack(Board board, byte targetFieldNo, Color color);
	}
}
