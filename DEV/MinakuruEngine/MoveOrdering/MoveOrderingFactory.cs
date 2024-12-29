namespace Minakuru.Engine.MoveOrdering;

public static class MoveOrderingFactory
{
	public static IMoveOrdering Create()
	{
		return new MoveOrdering();
	}
}
