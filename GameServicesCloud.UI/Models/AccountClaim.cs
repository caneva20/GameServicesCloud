namespace GameServicesCloud.UI;

public record AccountClaim(long Id, string Name) {
    public static EqualityComparer Comparer = new();

    public class EqualityComparer : IEqualityComparer<AccountClaim> {
        public bool Equals(AccountClaim? x, AccountClaim? y) {
            if (ReferenceEquals(x, y)) {
                return true;
            }

            if (ReferenceEquals(x, null)) {
                return false;
            }

            if (ReferenceEquals(y, null)) {
                return false;
            }

            if (x.GetType() != y.GetType()) {
                return false;
            }

            return x.Id == y.Id && x.Name == y.Name;
        }

        public int GetHashCode(AccountClaim obj) {
            return HashCode.Combine(obj.Id, obj.Name);
        }
    }
}