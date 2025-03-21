using NLog.Config;

namespace Client.UI.Factory;

public interface INLogConfigFactory
{
    LoggingConfiguration Create(string baseDir);
}