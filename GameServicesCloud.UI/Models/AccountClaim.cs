namespace GameServicesCloud.UI;

public class AccountClaim {
    public long Id { get; init; }
    public string Name { get; set; } = null!;
    public bool IsDefault { get; set; }

    protected bool Equals(AccountClaim other) {
        return Id == other.Id;
    }

    public override bool Equals(object? obj) {
        if (ReferenceEquals(null, obj)) {
            return false;
        }

        if (ReferenceEquals(this, obj)) {
            return true;
        }

        if (obj.GetType() != GetType()) {
            return false;
        }

        return Equals((AccountClaim)obj);
    }

    public override int GetHashCode() {
        return Id.GetHashCode();
    }
}