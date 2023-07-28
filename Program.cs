namespace ThreadResumeConsole
{
    internal class Program
    {
        static ManualResetEvent mrse = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            Console.WriteLine("Press escape to exit");
            Console.WriteLine("Press S to start thread");
            Console.WriteLine("Press T to stop thread");
            var input = Console.ReadKey();
            Console.WriteLine();

            Thread t = new Thread(async () =>
            {
                await LoopAsync();
            });
            t.IsBackground = true;
            t.Start();

            while (input.Key != ConsoleKey.Escape)
            {
                if (input.Key == ConsoleKey.S)
                {
                    mrse.Set();
                }
                else if (input.Key == ConsoleKey.T)
                {
                    mrse.Reset();
                }
                input = Console.ReadKey();
                Console.WriteLine();
            }
            Console.WriteLine("Bye");
        }

        static async Task LoopAsync()
        {
            while (true)
            {
                mrse.WaitOne();
                Console.WriteLine($"{DateTime.Now}");
                await Task.Delay(500);
            }
        }
    }
}