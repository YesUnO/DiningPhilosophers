// See https://aka.ms/new-console-template for more information
using DiningPhilosophers;
using DiningPhilosophers.ChandyMisra;


RunAll();




void RunDjiskra()
{
    Console.WriteLine("Starting Djiskra");
    var djiskra = new Djiskra(5);
    var task =  djiskra.RunTaskWithOptionalCancelationToken();
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
    CancellationToken cancellationToken = cancellationTokenSource.Token;


    var djiskra = new Djiskra();
    var chandyMisra = new ChandyMisra();

    var chandyMisraTask = chandyMisra.RunTaskWithOptionalCancelationToken(cancellationToken);
    var djiskraTask = djiskra.RunTaskWithOptionalCancelationToken(cancellationToken);
    var elapsedTask = DisplayElapsed(cancellationToken);

    Task.Delay(runTimeInSeconds * 1000).ContinueWith(_ =>
    {
        cancellationTokenSource.Cancel();
    });

    var tasks = new Task[] { chandyMisraTask, djiskraTask, elapsedTask };

    Task.WaitAll(tasks);

    for (int i = 0; i < chandyMisra.EatCount.Length; i++)
    {
        Console.WriteLine($"{i} __ Djiskra: {djiskra.EatCount[i]}; ChandyMisra: {chandyMisra.EatCount[i]} ");
    }
}

static async Task DisplayElapsed(CancellationToken cancellationToken)
{
    int seconds = 0;
    while (!cancellationToken.IsCancellationRequested)
    {
        Console.WriteLine($"{seconds} seconds");
        await Task.Delay(1000);
        seconds++;
    }
    
}