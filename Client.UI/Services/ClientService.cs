using Client.Core;
using Client.UI.Model.BlackList;
using Client.UI.Model.Dungeon;
using Client.UI.Model.PlayerModel;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Client.UI.Services;

public class ClientService
{
    private ControlAppearance _snackbarAppearance = ControlAppearance.Secondary;
    private int _snackbarTimeout = 2;

    private MythicStoneClientService cli;
    private ISnackbarService snackbarService;

    public ClientService(MythicStoneClientService cli, ISnackbarService snackbarService)
    {
        this.cli = cli;
        this.snackbarService = snackbarService;
    }


    public async Task<PlayerInfo?> GetPlayerInfoAsync(string name, string server,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await cli.GetPlayerInfoAsync(name, server, cancellationToken);
            ShowMessage(response.Message);

            return response.Data;
        }
        catch (Exception e)
        {
            ShowException(e);
            return null;
        }
    }

    public void GetPlayerBestScoreAsync(string name, string server, CancellationToken cancellationToken = default)
    {
        // var response = await cli.GetPlayerBestScoreAsync(name, server, cancellationToken);
        // ShowMessage(response.Message);
        //
        // return response.Data;
    }

    public void GetRoleBestScoreAsync(string name, string server, CancellationToken cancellationToken = default)
    {
        // var response = await cli.GetRoleBestScoreAsync(name, server, cancellationToken);
        // ShowMessage(response.Message);
        //
        // return response.Data;
    }

    public void GetRoleBestScoreDetailAsync(string name, string server, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<PeriodRating?> GetRoleThisPeriodScoreAsync(string name, string server,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await cli.GetRoleThisPeriodScoreAsync(name, server, cancellationToken);
            ShowMessage(response.Message);

            return response.Data;
        }
        catch (Exception e)
        {
            ShowException(e);
            return null;
        }
    }

    public async Task<List<DungeonRating?>> GetRoleDungeonInfoAsync(string name, string server,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await cli.GetRoleDungeonInfoAsync(name, server, cancellationToken);
            ShowMessage(response.Message);

            return response.Data;
        }
        catch (Exception e)
        {
            ShowException(e);
            return null;
        }
    }

    public async Task<List<string>> GetServerListAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<List<DungeonInfo>?> GetDungeonListAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await cli.GetDungeonListAsync(cancellationToken);

            ShowMessage(response.Message);

            return response.Data;
        }
        catch (Exception e)
        {
            ShowException(e);
            return null;
        }
    }

    #region Black list system

    public async Task<List<SuspendPlayer>?> GetUserBlackListAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await cli.GetUserBlackListAsync(cancellationToken);

            ShowMessage(response.Message);

            return response.Data;
        }
        catch (Exception e)
        {
            ShowException(e);
            return null;
        }
    }

    public async void AddUserToBlackListAsync(string name, string realm, string reason,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await cli.AddUserToBlackListAsync(name, realm, reason, cancellationToken);
            ShowMessage(response);
        }
        catch (Exception e)
        {
            ShowException(e);
            return;
        }
    }

    #endregion


    private void ShowMessage(Message? message)
    {
        if (message == null) return;
        if (message.Level == 0) return;
        UpdateSnackbarAppearance(message.Level);
        snackbarService.Show(
            "MythicStonePlus:",
            message.Content,
            _snackbarAppearance,
            null,
            TimeSpan.FromSeconds(_snackbarTimeout)
        );
    }

    private void UpdateSnackbarAppearance(int appearanceIndex)
    {
        _snackbarAppearance = appearanceIndex switch
        {
            1 => ControlAppearance.Info,
            2 => ControlAppearance.Success,
            3 => ControlAppearance.Caution,
            4 => ControlAppearance.Danger,
            _ => ControlAppearance.Primary,
        };
    }


    private void ShowException(Exception e)
    {
        snackbarService.Show(
            "MythicStonePlus:",
            e.Message,
            ControlAppearance.Danger,
            null,
            TimeSpan.FromSeconds(_snackbarTimeout)
        );
    }
}