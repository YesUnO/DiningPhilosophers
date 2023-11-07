using DiningPhilosophers;
using DiningPhilosophers.Old;

//RunDjiskra();
//RunChandyMisra();
RunAllX();
//RunAll();
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
    int runTimeInSeconds = 10;

    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    CancellationToken ct = cancellationTokenSource.Token;

    var solutionFactory = new SolutionFactory();


    var djiskra = solutionFactory.CreateDjiskraSolution(false, 50);
    var chandyMisra = new ChandyMisra(5);

    var newChandy = solutionFactory.CreateChandyMisraSolution(false, 5);
    //var newChandyTask = newChandy.Run(ct);

    //var chandyMisraTask = chandyMisra.RunTaskWithOptionalCancelationToken(ct);
    var djiskraTask = djiskra.Run(ct);
    var elapsedTask = DisplayElapsed(ct);

    Task.Delay(runTimeInSeconds * 1000).ContinueWith(_ =>
    {
        cancellationTokenSource.Cancel();
    });

    var tasks = new Task[] {
        //chandyMisraTask,
        djiskraTask,
        //newChandyTask,
        elapsedTask 
    };

    Task.WaitAll(tasks);

    var djiskraCount = 0;
    var chandyMisraCount = 0;
    var newCount = 0;

    for (int i = 0; i < djiskra.EatCounter.Length; i++)
    {
        djiskraCount += djiskra.EatCounter[i];
        //chandyMisraCount += chandyMisra.EatCount[i];
        //newCount += newChandy.EatCounter[i];

        Console.WriteLine($"{i} __ " +
            $"Djiskra: {djiskra.EatCounter[i]}; " +
            //$"ChandyMisra: {chandyMisra.EatCount[i]} " +
            //$"New: {newChandy.EatCounter[i]} " +
            $"");
    }
    Console.WriteLine($"total: " +
        $"New: {newCount}; " +
        $"Djiskra: {djiskraCount}; " +
        $"ChandyMisra: {chandyMisraCount} ");

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