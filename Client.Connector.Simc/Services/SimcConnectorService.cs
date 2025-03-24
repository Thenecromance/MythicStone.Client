using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Windows.Threading;
using Microsoft.Extensions.Logging;

namespace Client.Connector.Simc.Services;

public class SimcConnectorService
{
    private readonly ILogger<SimcConnectorService> _logger;

    private NamedPipeServerStream _serverpipe;

    public SimcConnectorService(ILogger<SimcConnectorService> logger)
    {
        _logger = logger;
        _logger.LogInformation("SimcConnectorService created");
    }


    private ProcessStartInfo CreateStartInfo(string inputData)
    {
        return new ProcessStartInfo
        {
            FileName = "./bin/simc.exe",
            Arguments = inputData,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
    }

    private async void Run(string inputData)
    {
        var _process = new Process
        {
            StartInfo = CreateStartInfo(inputData)
        };
        _process.OutputDataReceived += (sender, args) =>
        {
            if (!string.IsNullOrEmpty(args.Data))
                _logger.LogInformation(args.Data);
        };
        _process.ErrorDataReceived += (sender, args) =>
        {
            if (!string.IsNullOrEmpty(args.Data))
                _logger.LogInformation(args.Data);
        };
        _process.EnableRaisingEvents = true;

        _process.Exited += (sender, args) => Dispatcher.CurrentDispatcher.BeginInvoke(() =>
        {
            _process.CancelOutputRead(); // 停止异步读取
            _process.CancelErrorRead();
            _logger.LogInformation("Process exited");
            _process.Dispose();
            File.Delete(inputData);
        });


        _process.Start();

        _process.BeginOutputReadLine();
        _process.BeginErrorReadLine();

        await Task.CompletedTask;
    }

    public void Execute(string inputData)
    {
        // build pipe and wait for simc connection
        _serverpipe = new NamedPipeServerStream(
            "simcPipe",
            PipeDirection.InOut,
            NamedPipeServerStream.MaxAllowedServerInstances,
            PipeTransmissionMode.Message, // 消息模式
            PipeOptions.Asynchronous);

        _serverpipe.BeginWaitForConnection(OnClientConnected, null);


        Run(inputData);
    }


    private void OnClientConnected(IAsyncResult result)
    {
        var server = (NamedPipeServerStream)result.AsyncState!;
        server.EndWaitForConnection(result);
        ThreadPool.QueueUserWorkItem(_ => HandleClient(server));
    }

    private void HandleClient(NamedPipeServerStream server)
    {
        try
        {
            var buffer = new byte[4096];
            var encoding = Encoding.UTF8;
            while (server.IsConnected)
            {
                int bytesRead = server.Read(buffer, 0, buffer.Length);

                if (bytesRead > 0)
                {
                    string received = encoding.GetString(buffer, 0, bytesRead);
                    _logger.LogDebug($"Received: {received}");
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Connect Error with simc.exe: {ex.Message}");
        }
        finally
        {
            server.Close();
        }
    }
}