using Steeltoe.Common.HealthChecks;

namespace apione
{
    public class CustomHealthContributor : IHealthContributor
    {
        public string Id => "health";

        public HealthCheckResult Health()
        {
            var result = new HealthCheckResult
            {
                // this is used as part of the aggregate, it is not directly part of the middleware response
                Status = HealthStatus.DOWN,
                Description = "This health check does not check anything"
            };
            result.Details.Add("status", HealthStatus.DOWN.ToString());
            return result;
        }
    }
}