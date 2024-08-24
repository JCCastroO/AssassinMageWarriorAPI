namespace AssassinMageWarrior.Data.Repository.Room.CancelInvite;

public interface ICancelInviteRepository
{
    Task RemoveInvite(long id);
}
