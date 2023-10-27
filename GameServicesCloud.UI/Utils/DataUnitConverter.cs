namespace GameServicesCloud.UI;

public static class DataUnitConverter {
    private static readonly string[] SizeUnits = { "Bytes", "KiB", "MiB", "GiB", "TiB" };

    public static string Convert(double bytes) {
        if (bytes <= 0) {
            return "0 " + SizeUnits[0];
        }

        var digitGroups = (int)(Math.Log10(bytes) / Math.Log10(1024));

        return $"{bytes / Math.Pow(1024, digitGroups):F1} {SizeUnits[digitGroups]}";
    }
}