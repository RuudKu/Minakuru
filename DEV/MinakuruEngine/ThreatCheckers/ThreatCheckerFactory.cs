
namespace Minakuru.Engine.ThreatCheckers;
public static class ThreatCheckerFactory
{
	public static IThreatChecker Create()
	{
		return new ThreatChecker();
	}
}
