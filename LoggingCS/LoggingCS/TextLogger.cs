using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TradingEngineServer.Logging.LoggingConfiguration;


namespace TradingEngineServer.Logging   
{
    
    public class TextLogger : AbstractLogger, ITextLogger
    {
        //PRIVATE
        private readonly LoggerConfiguration _loggingConfiguration;
        public TextLogger(IOptions<LoggerConfiguration> loggingConfiguration) : base()
        {
            _loggingConfiguration = loggingConfiguration.Value ?? throw new ArgumentNullException(nameof(loggingConfiguration));
            if (_loggingConfiguration.LoggerType != LoggerType.Text)
                throw new InvalidOperationException($"{nameof(TextLogger)} doesnt match LoggerType of {_loggingConfiguration.LoggerType}");

            var now = DateTime.Now;
            string logDirectory = Path.Combine(_loggingConfiguration.TextLoggerConfiguration.Directory, $"{now:yyyy-MM-dd}");
            Directory.CreateDirectory( logDirectory );
            string uniqueLogName = $"{_loggingConfiguration.TextLoggerConfiguration.Filename}-{now:HH_mm_ss}";
            string baseLogName = Path.ChangeExtension(uniqueLogName, _loggingConfiguration.TextLoggerConfiguration.FileExtension);
            string filepath = Path.Combine(logDirectory, baseLogName);
            _ = Task.Run(() => LogAsync(filepath, _logQueue, _tokenSource.Token));
        }

        private static async Task LogAsync(string filepath, BufferBlock<LogInformation> logQueue, CancellationToken token)
        {
            using var fs = new FileStream(filepath, FileMode.CreateNew, FileAccess.Write, FileShare.Read);
            using var sw = new StreamWriter(fs) { AutoFlush = true,  };
            try
            {
                while (true)
                {
                    var logItem = await logQueue.ReceiveAsync(token).ConfigureAwait(false);
                    string formattedMessage = FormatLogItem(logItem);
                    await sw.WriteAsync(formattedMessage).ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException) 
            { }
        }

        private static string FormatLogItem(LogInformation logItem)
        {
            return $"[{logItem.Now:yyyy-MM-dd HH-mm-ss.fffffff}] [{logItem.ThreadName, -30}:{logItem.ThreadID:000}]" + 
                $"[{logItem.LogLevel}] {logItem.Message}";
        }

        protected override void Log(LogLevel logLevel, string module, string message)
        {
            _logQueue.Post(new LogInformation(logLevel, module, message, DateTime.Now, 
                Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name));
        }

        ~TextLogger()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            lock (_lock)
            {
                if (_disposed)
                    return;
                _disposed = true;
            }
            if (disposing) 
            {
                //Get rid off managed resources
                _tokenSource.Cancel();
                _tokenSource.Dispose();
            
            }
            //Get rid of unmanaged resources
        }

        private readonly BufferBlock<LogInformation> _logQueue = new BufferBlock<LogInformation>();
        private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private readonly object _lock = new object();
        private bool _disposed = false;
        
    }
}
