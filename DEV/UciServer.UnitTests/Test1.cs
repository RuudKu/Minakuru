using Shouldly;
using Minakuru.Engine;

namespace Minakuru.UciServer.UnitTests;

[TestClass]
public sealed class Test1
{
	[Ignore]
	[TestMethod]
	public async Task TestPositionStartPosAsync()
	{
		var inStream = new MemoryStream();
		var outStream = new MemoryStream();

		var textWriter = new StreamWriter(inStream);
		var textReader = new StreamReader(outStream);

		var uciServer = new UciServer(inStream, outStream);

		Thread thread = new(StartThread!);
		thread.Start();

		await Task.Yield();

		await textWriter.WriteLineAsync("position startpos");
		await textWriter.FlushAsync();

		await Task.Yield();

		await textWriter.WriteLineAsync("quit");
		await textWriter.FlushAsync();

		await Task.Yield();

		string actualFen = uciServer.GameState.Board.ToFen();

		string expectedFen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

		actualFen.ShouldBe(expectedFen);

		async void StartThread(object data)
		{
			await uciServer.RunAsync();
		}
	}
}
