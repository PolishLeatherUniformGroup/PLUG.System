using Microsoft.Extensions.Configuration;

namespace ONPA.ServiceDefaults.ConfigurationModels
{
    public sealed class RetryPolicyConfiguration
    {
        public RetryPolicyConfiguration()
        {
        }

        public RetryPolicyConfiguration(IConfigurationSection retrySection)
        {
            if (retrySection.Exists())
            {
                var maximumRetryAttempts = retrySection.GetValue<int>("MaximumRetryAttempts");
                var waitAndRetryTimeSpans = retrySection.GetSection("WaitAndRetryTimeSpans")
                    .Get<double[]>()?
                    .Select(TimeSpan.FromMilliseconds)
                    .ToList();

                if (waitAndRetryTimeSpans is object && waitAndRetryTimeSpans.Any())
                {
                    this.WaitAndRetryTimeSpans = waitAndRetryTimeSpans;
                }
                else
                {
                    this.MaximumRetryAttempts = maximumRetryAttempts;
                }
            }
        }

        public RetryPolicyConfiguration(int maximumRetryAttempts)
        {
            this.MaximumRetryAttempts = maximumRetryAttempts;
        }

        public RetryPolicyConfiguration(IEnumerable<TimeSpan> waitAndRetryTimeSpans)
        {
            this.WaitAndRetryTimeSpans = waitAndRetryTimeSpans;
        }

        public RetryPolicyConfiguration(IEnumerable<int> waitAndRetryDurationsInMilliseconds)
        {
            this.WaitAndRetryTimeSpans = waitAndRetryDurationsInMilliseconds
                .Select(duration => TimeSpan.FromMilliseconds(duration));
        }


        public int MaximumRetryAttempts { get; } = 3;

        public IEnumerable<TimeSpan> WaitAndRetryTimeSpans { get; } = new TimeSpan[] { };
    }
}
