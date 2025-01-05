namespace Minakuru.UciServer;

public static class Program
{
	public static async Task Main(string[] args)
	{
		UciServer uciServer = new(Console.OpenStandardInput(), Console.OpenStandardOutput());
		await uciServer.RunAsync();
	}
}
