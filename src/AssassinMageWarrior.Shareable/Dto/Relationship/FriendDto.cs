namespace AssassinMageWarrior.Shareable.Dto.Relationship;

public class FriendDto
{
    public long Id { get; set; } = default;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool Active { get; set; } = default!;
}
