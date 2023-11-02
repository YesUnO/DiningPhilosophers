using DiningPhilosophers;
using DiningPhilosophers.ChandyMisra;


RunAll();




void RunDjiskra()
{
    Console.WriteLine("Starting Djiskra");
    var djiskra = new Djiskra(5);
    var task = djiskra.RunTaskWithOptionalCancelationToken();
    task.Wait();
}

void RunChandyMisra()
{
    Console.WriteLine("Starting ChandyMisra");
    var chandyMisra = new ChandyMisra(5);
    var task = chandyMisra.RunTaskWithOptionalCancelationToken();
    task.Wait();
}


void RunAll()
{
    Console.WriteLine("Starting both");
    int runTimeInSeconds = 5;

    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    CancellationToken ct = cancellationTokenSource.Token;


    var djiskra = new Djiskra();
    var chandyMisra = new ChandyMisra();

    var chandyMisraTask = chandyMisra.RunTaskWithOptionalCancelationToken(ct);
    var djiskraTask = djiskra.RunTaskWithOptionalCancelationToken(ct);
    var elapsedTask = DisplayElapsed(ct);

    Task.Delay(runTimeInSeconds * 1000).ContinueWith(_ =>
    {
        cancellationTokenSource.Cancel();
    });

    var tasks = new Task[] { chandyMisraTask, djiskraTask, elapsedTask };

    Task.WaitAll(tasks);

    var djiskraCount = 0;
    var chandyMisraCount = 0;

    for (int i = 0; i < chandyMisra.EatCount.Length; i++)
    {
        djiskraCount += djiskra.EatCount[i];
        chandyMisraCount += chandyMisra.EatCount[i];

        Console.WriteLine($"{i} __ Djiskra: {djiskra.EatCount[i]}; ChandyMisra: {chandyMisra.EatCount[i]} ");
    }
    Console.WriteLine($"total: Djiskra: {djiskraCount}; ChandyMisra: {chandyMisraCount} ");

}

static async Task DisplayElapsed(CancellationToken ct)
{
    int seconds = 0;
    while (!ct.IsCancellationRequested)
    {
        Console.WriteLine($"{seconds++} seconds");
        await Task.Delay(1000);
    }

}