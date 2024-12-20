using FluentAssertions;

namespace Engine.UnitTests
{
	[TestClass]
	public sealed class FenTests
	{
		[TestMethod]
		public void TestStartPosition()
		{
			// Arrange
			var board = BoardHelper.StartPosition();

			// Act
			var actual = board.ToFen();

			// Assert
			var expected = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
			actual.Should().Be(expected);
		}
	}
}
