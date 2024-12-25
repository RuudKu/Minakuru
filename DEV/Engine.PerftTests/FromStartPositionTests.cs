namespace Minakuru.Engine.PerftTests;

[TestClass]
public sealed class FromStartPositionTests
{
	[DataRow(0, 1L)]
	[DataRow(1, 20L)]
	[DataRow(2, 400L)]
	[DataRow(3, 8902L)]
	[DataTestMethod]
	public void PerftStartpositionTests(int depth, long expectedNodes)
	{
	}
}
