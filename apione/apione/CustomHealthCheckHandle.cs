using Steeltoe.Discovery.Eureka;
using Steeltoe.Discovery.Eureka.AppInfo;

namespace apione
{
    public class CustomHealthCheckHandle:IHealthCheckHandler
    {
        public InstanceStatus GetStatus(InstanceStatus currentStatus)
        {
            return InstanceStatus.DOWN;
        }
    }
}