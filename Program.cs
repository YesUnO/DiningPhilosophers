using DiningPhilosophers;
using DiningPhilosophers.Old;

//RunDjiskra();
//RunChandyMisra();
RunAllX();
//RunChandyMisraOld();


void RunDjiskra()
{
    Console.WriteLine("Starting Djiskra");
    var solutionFactory = new SolutionFactory();
    var djiskra = solutionFactory.CreateDjiskraSolution(true, 5);
    djiskra.Run().Wait();
}

void RunChandyMisra()
{
    Console.WriteLine("Starting ChandyMisra");
    var solutionFactory = new SolutionFactory();
    var djiskra = solutionFactory.CreateChandyMisraSolution(true, 5);
    djiskra.Run().Wait();
}

void RunChandyMisraOld()
{
    Console.WriteLine("Starting ChandyMisra");
    var solutionFactory = new SolutionFactory();
    var chandy = new ChandyMisra(5);
    chandy.RunTaskWithOptionalCancelationToken().Wait();
}


void RunAll()
{
    Console.WriteLine("Starting both");
    var solutionFactory = new SolutionFactory();
    var tasks = solutionFactory.CreateRunAllTask();
    Task.WaitAll(tasks);
}

void RunAllX()
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